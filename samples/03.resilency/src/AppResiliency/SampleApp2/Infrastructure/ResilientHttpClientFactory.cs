using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Polly;
using Resilience.Http;

namespace SampleApp2.Infrastructure
{
    public class ResilientHttpClientFactory:IResilientHttpClientFactory
    {
        private readonly ILogger<ResilientHttpClient> _logger;
        private readonly int _retryCount;
        private readonly int _exceptionsAllowedBeforeBreaking;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ResilientHttpClientFactory(ILogger<ResilientHttpClient> logger, IHttpContextAccessor httpContextAccessor, int exceptionsAllowedBeforeBreaking = 5, int retryCount = 6)
        {
            _logger = logger;
            _exceptionsAllowedBeforeBreaking = exceptionsAllowedBeforeBreaking;
            _retryCount = retryCount;
            _httpContextAccessor = httpContextAccessor;
        }


        public ResilientHttpClient CreateResilientHttpClient()
            => new ResilientHttpClient((origin) => CreatePolicies(), _logger, _httpContextAccessor);

        private Policy[] CreatePolicies()
            => new Policy[]
            {
                Policy.Handle<HttpRequestException>()
                .WaitAndRetryAsync(
                    // number of retries
                    _retryCount,
                    // exponential backofff
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    // on retry
                    (exception, timeSpan, retryCount, context) =>
                    {
                        var msg = $"Retry {retryCount} implemented with Polly's RetryPolicy " +
                            $"of {context.PolicyKey} " +
                            $"at {context.ExecutionKey}, " +
                            $"due to: {exception}.";
                      //  _logger.LogWarning(msg);


                        string debugMsg = $"Retry {retryCount} " +
                                          $"of {context.PolicyKey} " +
                                          $"at {context.ExecutionKey} " +
                                          $"timeSpan {timeSpan} ";

                        _logger.LogDebug(debugMsg);
                    }),
                Policy.Handle<HttpRequestException>()
                .CircuitBreakerAsync( 
                   // number of exceptions before breaking circuit
                   _exceptionsAllowedBeforeBreaking,
                   // time circuit opened before retry
                   TimeSpan.FromMinutes(1),
                   (exception, duration) =>
                   {
                        // on circuit opened
                       _logger.LogDebug("*** Circuit breaker opened ***");
                        _logger.LogTrace("Circuit breaker opened");
                   },
                   () =>
                   {
                        // on circuit closed
                       _logger.LogDebug("*** Circuit breaker closed ***");
                        _logger.LogTrace("Circuit breaker reset");
                   })
            };
    }
}

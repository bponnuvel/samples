using Resilience.Http;

namespace SampleApp2
{
    public interface IResilientHttpClientFactory
    {
        ResilientHttpClient CreateResilientHttpClient();
    }
}

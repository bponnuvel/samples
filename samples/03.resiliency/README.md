# App Resiliency
This project is the demo for http resiliency using Polly.

## Resiliency & Circuit Breaker pattern
**Circuit breaker states**

![Circuit breaker states](docs/cb-flow.png "circuit breaker states")

**Circuit breaker flow**

![circuit breaker flow](docs/circuit-breaker.png "circuit breaker flow")

### Sample App 1
The API services which has the failing middleware.
```
/failing?enable
(or)
/failing?disable
```


### Sample App 2
The consumer application which uses Polly. Launch and load the following url
```
http://localhost:30200/api/mobile
```
which intern consumes the `Sample App1` using http.

### Resources
* https://martinfowler.com/bliki/CircuitBreaker.html
* https://github.com/App-vNext/Polly/wiki/Circuit-Breaker

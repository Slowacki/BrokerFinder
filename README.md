# BrokerFinder

## Description:

The solution allows the user to find the most popular real estate brokers based on the location and properties of their listings.

It queries Funda's API and temporarily stores the results in a Redis cache. 

The HTTP client requesting the data from Funda's API is rate limited to 100 requests per minute. 
If the limit is hit, requests will be retried with a total maximum backoff of slightly over a minute to mitigate the load on Funda's API
while trying to fulfill the user's request.

## Prerequisites:
- .NET 8
- WSL 2 for Windows (https://learn.microsoft.com/en-us/windows/wsl/install)
- Docker Desktop (https://www.docker.com/products/docker-desktop/)

## How to run
- Clone the repository from github
- Make sure ports 5000 and 6379 are available
- Run `docker compose --profile run up` in the main solution folder
- Navigate to `http://localhost:5000/top-brokers`

## How to debug
- Clone the repository from github
- Make sure ports 5000 and 6379 are available
- Run `docker compose --profile debug up` in the main solution folder to only start Redis
- Open the `.sln` file in IDE of choosing
- Start debugging
- Navigate to `http://localhost:5000/top-brokers`

## Utilized packages

- [Scrutor](https://github.com/khellang/Scrutor) to decorate the `IListingsService` that uses Funda's API with a cached version that uses Redis
- [Refit](https://github.com/reactiveui/refit) to create a type-safe interface to utilize Funda's API
- [Polly v8](https://github.com/App-vNext/Polly) to rate limiting requests to Funda's API and add resilience
- [AutoFixture](https://github.com/AutoFixture/AutoFixture) and [Moq](https://github.com/moq) - tools to simplify testing

## Notes and observations

- More unit tests could definitely be added, but a couple are there as a demonstration of how they could be done. Ideally, since the executable is a Blazor application, some UI tests should be added too,
  for example using [Playwright](https://playwright.dev/dotnet/docs/intro)

- Since Funda's API is rate limited by the API key used, this application can still easily overload it e.g. if application is run in multiple instances. The best way to combat this, 
  would be to precache all the listings available and schedule a job to refresh that cache on a given interval. However, a problem arises. To my best knowledge, 
  there's no way of knowing if the listing has a garden or not. As a result, it's not possible to search through the precached results when looking for listings with a garden. 
  I suspect when querying for a `/location/tuin`, what actually happens is the word `tuin` is simply searched in the description, which seems rather flimsy as someone could
  describe his house as `beautiful but sadly without a garden :(` which would still show up when using the original query.
  
  As a workaround, there's no precaching done, but instead when listing brokers from specific location with a specific query, the results are cached with a key indicating the query used.

- Pagination on Funda's API is dangerously broken. It happily accepts values above 25 and even responds with a modified pagination object, however, the maximum number of results is always 25.

- It would also be nice if it returned `429` with a `retry-after` header when rate limiting occurs.

- New version of Polly offers way more power than before allowing to create an actually working rate limiter. In previous versions rate limiter policy is barely usable.

- I hope the API key provided is somewhat ok to be publicized since it's now engrained in the `appsettings` files and the commit history 😅 In a real life scenario that kind of data would be stored in a key vault
  and securely injected.
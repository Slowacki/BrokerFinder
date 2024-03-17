# BrokerFinder

## Prerequisites:
- .NET 8
- WSL 2 for Windows (https://learn.microsoft.com/en-us/windows/wsl/install)
- Docker Desktop (https://www.docker.com/products/docker-desktop/)

## How to run
- Clone the solution from github
- Make sure ports 5000 and 6379 are available
- Run `docker compose up` in the main solution folder
- Navigate to `../BrokerFinder/src/BrokerFinder.Web`
- Run `dotnet run -c development` 
- Alternatively open the `.sln` file in the chosen IDE (e.g. Rider, VS) and run there
- Navigate to `https://localhost:5000/top-brokers`


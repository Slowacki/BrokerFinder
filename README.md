# BrokerFinder

## Prerequisites:
- .NET 8
- WSL 2 for Windows (https://learn.microsoft.com/en-us/windows/wsl/install)
- Docker Desktop (https://www.docker.com/products/docker-desktop/)

## How to run
- Clone the solution from github
- Make sure ports 5000 and 6379 are available
- Run `docker compose --profile run up` in the main solution folder
- Navigate to `https://localhost:5000/top-brokers`

## How to debug
- Clone the solution from github
- Make sure ports 5000 and 6379 are available
- Run `docker compose --profile debug up` in the main solution folder
- Navigate to `https://localhost:5000/top-brokers`
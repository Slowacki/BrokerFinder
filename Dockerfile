FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/BrokerFinder.Web/BrokerFinder.Web.csproj", "src/BrokerFinder.Web/"]
RUN dotnet restore "src/BrokerFinder.Web/BrokerFinder.Web.csproj"
COPY . .
WORKDIR "/src/src/BrokerFinder.Web"
RUN dotnet build "BrokerFinder.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BrokerFinder.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BrokerFinder.Web.dll"]

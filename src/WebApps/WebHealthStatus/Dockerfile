FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Services/WebApps/WebHealthStatus/WebHealthStatus.csproj", "Services/WebApps/WebHealthStatus/"]
COPY ["BuildingBlocks/Common.Logging/Common.Logging.csproj", "BuildingBlocks/Common.Logging/"]
COPY ["BuildingBlocks/Contracts/Contracts.csproj", "BuildingBlocks/Contracts/"]
COPY ["BuildingBlocks/Infrastructure/Infrastructure.csproj", "BuildingBlocks/Infrastructure/"]
COPY ["BuildingBlocks/Shared/Shared.csproj", "BuildingBlocks/Shared/"]
RUN dotnet restore "Services/WebApps/WebHealthStatus/WebHealthStatus.csproj"
COPY . .
WORKDIR "/src/Services/WebApps/WebHealthStatus"
RUN dotnet build "WebHealthStatus.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebHealthStatus.csproj" -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebHealthStatus.dll"]

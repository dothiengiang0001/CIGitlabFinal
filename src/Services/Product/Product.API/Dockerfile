FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Services/Product/Product.API/Product.API.csproj", "Services/Product/Product.API/"]
COPY ["Services/Product/Product.Application/Product.Application.csproj", "Services/Product/Product.Application/"]
COPY ["Services/Product/Product.Domain/Product.Domain.csproj", "Services/Product/Product.Domain/"]
COPY ["Services/Product/Product.Infrastructure/Product.Infrastructure.csproj", "Services/Product/Product.Infrastructure/"]
COPY ["BuildingBlocks/Common.Logging/Common.Logging.csproj", "BuildingBlocks/Common.Logging/"]
COPY ["BuildingBlocks/Contracts/Contracts.csproj", "BuildingBlocks/Contracts/"]
COPY ["BuildingBlocks/Infrastructure/Infrastructure.csproj", "BuildingBlocks/Infrastructure/"]
COPY ["BuildingBlocks/Shared/Shared.csproj", "BuildingBlocks/Shared/"]
RUN dotnet restore "Services/Product/Product.API/Product.API.csproj"
RUN dotnet restore "Services/Product/Product.Application/Product.Application.csproj"
RUN dotnet restore "Services/Product/Product.Domain/Product.Domain.csproj"
RUN dotnet restore "Services/Product/Product.Infrastructure/Product.Infrastructure.csproj"
COPY . .
WORKDIR "/src/Services/Product/Product.API"
RUN dotnet build "Product.API.csproj" -c Release -o /app/build/Product.API
WORKDIR "/src/Services/Product/Product.Infrastructure"
RUN dotnet build "Product.Infrastructure.csproj" -c Release -o /app/build/Product.Infrastructure
WORKDIR "/src/Services/Product/Product.Domain"
RUN dotnet build "Product.Domain.csproj" -c Release -o /app/build/Product.Domain
WORKDIR "/src/Services/Product/Product.API"
RUN dotnet build "Product.API.csproj" -c Release -o /app/build/Product.API
WORKDIR "/src/Services/Product/Product.Application"
RUN dotnet build "Product.Application.csproj" -c Release -o /app/build/Product.Application

FROM build AS publish
WORKDIR "/src/Services/Product/Product.API"
RUN dotnet publish "Product.API.csproj" -c Release -o /app/publish
#WORKDIR "/src/Services/Product/Product.Infrastructure"
#RUN dotnet publish "Product.Infrastructure.csproj" -c Release -o /app/publish
#WORKDIR "/src/Services/Product/Product.Domain"
#RUN dotnet publish "Product.Domain.csproj" -c Release -o /app/publish
#WORKDIR "/src/Services/Product/Product.API"
#RUN dotnet publish "Product.API.csproj" -c Release -o /app/publish
#WORKDIR "/src/Services/Product/Product.Application"
#RUN dotnet publish "Product.Application.csproj" -c Release -o /app/publish


FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Product.API.dll"]
#ENTRYPOINT ["dotnet", "Product.Infrastructure.dll"]
#ENTRYPOINT ["dotnet", "Product.Domain.dll"]
#ENTRYPOINT ["dotnet", "Product.API.dll"]
#ENTRYPOINT ["dotnet", "Product.Application.dll"]


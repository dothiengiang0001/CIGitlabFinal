## Docker Commands: (cd into folder contain file `docker-compose.yml`, `docker-compose.override.yml`)

- Up & running:
```Powershell
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans --build
```
- Stop & Removing:
```Powershell
docker-compose down
```

# Remove all containers (including stopped ones)
docker rm -f $(docker ps -aq)

# Remove all volumes
docker volume prune -f

# Remove all images (without remove volumes)
docker rmi -f $(docker images -aq)

## Useful commands:

- ASPNETCORE_ENVIRONMENT=Production dotnet ef database update
- dotnet watch run --environment "Development"
- dotnet restore
- dotnet build
- Migration commands for Ordering API:
  - cd into Ordering folder
  - dotnet ef migrations add "SampleMigration" -p Ordering.Infrastructure --startup-project Ordering.API -o Persistence/Migrations
  - dotnet ef migrations remove -p Ordering.Infrastructure --startup-project Ordering.API
  - dotnet ef database update -p Ordering.Infrastructure --startup-project Ordering.API

## Other
cd into Product folder
dotnet ef migrations add "SampleMigration1" -p ./src/Services/Product/Product.Infrastructure --startup-project ./src/Services/Product/Product.Infrastructure -o ./src/Services/Product/Product.Infrastructure/Persistence/Migrations
dotnet ef database update -p ./src/Services/Product/Product.Infrastructure --startup-project ./src/Services/Product/Product.Infrastructure
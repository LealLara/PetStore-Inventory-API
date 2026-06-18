FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
RUN mkdir -p /data && chown -R $APP_UID /data
USER $APP_UID

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PetStore.Inventory.Api/PetStore.Inventory.Api.csproj", "PetStore.Inventory.Api/"]
COPY ["PetStore.Inventory.Application/PetStore.Inventory.Application.csproj", "PetStore.Inventory.Application/"]
COPY ["PetStore.Inventory.Domain/PetStore.Inventory.Domain.csproj", "PetStore.Inventory.Domain/"]
COPY ["PetStore.Inventory.Infrastructure/PetStore.Inventory.Infrastructure.csproj", "PetStore.Inventory.Infrastructure/"]
RUN dotnet restore "./PetStore.Inventory.Api/PetStore.Inventory.Api.csproj"
COPY . .
WORKDIR "/src/PetStore.Inventory.Api"
RUN dotnet build "./PetStore.Inventory.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PetStore.Inventory.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Docker
ENV ASPNETCORE_HTTP_PORTS=8080
ENV ConnectionStrings__DefaultConnection="Data Source=/data/PetStore.db"
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetStore.Inventory.Api.dll"]

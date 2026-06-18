# ===== ESTÁGIO 1: BASE (Runtime) =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Criar diretório para o banco SQLite com permissões
RUN mkdir -p /data && chmod 777 /data

# ===== ESTÁGIO 2: BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar os arquivos .csproj
COPY ["PetStore.Inventory.Api/PetStore.Inventory.Api.csproj", "PetStore.Inventory.Api/"]
COPY ["PetStore.Inventory.Application/PetStore.Inventory.Application.csproj", "PetStore.Inventory.Application/"]
COPY ["PetStore.Inventory.Domain/PetStore.Inventory.Domain.csproj", "PetStore.Inventory.Domain/"]
COPY ["PetStore.Inventory.Infrastructure/PetStore.Inventory.Infrastructure.csproj", "PetStore.Inventory.Infrastructure/"]

# Restaurar pacotes
RUN dotnet restore "PetStore.Inventory.Api/PetStore.Inventory.Api.csproj"

# Copiar o resto do código
COPY . .

# Compilar a aplicação
WORKDIR "/src/PetStore.Inventory.Api"
RUN dotnet build "PetStore.Inventory.Api.csproj" -c Release -o /app/build

# ===== ESTÁGIO 3: PUBLISH =====
FROM build AS publish
RUN dotnet publish "PetStore.Inventory.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ===== ESTÁGIO 4: FINAL =====
FROM base AS final
WORKDIR /app

# Variáveis de ambiente
ENV ASPNETCORE_ENVIRONMENT=Docker
ENV ASPNETCORE_URLS=http://+:8080
ENV ConnectionStrings__DefaultConnection="Data Source=/data/PetStore.db"

# Copiar arquivos publicados
COPY --from=publish /app/publish .

# Ponto de entrada
ENTRYPOINT ["dotnet", "PetStore.Inventory.Api.dll"]
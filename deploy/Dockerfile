# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copia arquivos de projeto primeiro para aproveitar cache do Docker
COPY src/Api/Api.csproj src/Api/
COPY src/Domain/Domain.csproj src/Domain/
COPY src/Infrastructure/Infrastructure.csproj src/Infrastructure/
COPY src/Application/Application.csproj src/Application/
COPY src/UnitTests/UnitTests.csproj src/UnitTests/
COPY src/IntegrationTests/IntegrationTests.csproj src/IntegrationTests/

# Restaura dependências
WORKDIR /app/src/Api
RUN dotnet restore

# Copia o restante do código
COPY src/ ./src/

# Publica a aplicação
RUN dotnet publish -c Release -o /app/out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copia o build da etapa anterior
COPY --from=build /app/out .

# Expor a porta padrão
EXPOSE 80

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "Api.dll"]

# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copia arquivos de projeto para o cache
COPY src/Api/Api.csproj src/Api/
COPY src/Domain/Domain.csproj src/Domain/
COPY src/Infrastructure/Infrastructure.csproj src/Infrastructure/
COPY src/Application/Application.csproj src/Application/
# Não copie os projetos de teste!

# Restaura dependências
WORKDIR /app/src/Api
RUN dotnet restore

# Copia apenas o código dos projetos necessários
COPY src/Api/ src/Api/
COPY src/Domain/ src/Domain/
COPY src/Infrastructure/ src/Infrastructure/
COPY src/Application/ src/Application/
# Não copie os diretórios dos testes

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

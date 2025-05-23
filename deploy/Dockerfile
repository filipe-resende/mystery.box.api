# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copia arquivos de projeto e restaura dependências
COPY ./src/Api/Api.csproj ./
RUN dotnet restore

# Copia o restante do código e compila
COPY ./src/Api/ ./
RUN dotnet publish -c Release -o out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copia o build da etapa anterior
COPY --from=build /app/out ./

# Expor a porta padrão
EXPOSE 80

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "Api.dll"]

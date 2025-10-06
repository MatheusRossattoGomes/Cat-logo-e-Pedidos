# Estágio 1: Build da aplicação .NET
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copia os arquivos de projeto e restaura as dependências primeiro
COPY *.sln .
COPY src/Api/*.csproj src/Api/
COPY src/Application/*.csproj src/Application/
COPY src/Domain/*.csproj src/Domain/
COPY src/Infrastructure/*.csproj src/Infrastructure/
RUN dotnet restore "CatalogoPedidos.sln"

# Copia o resto do código fonte e publica a aplicação
COPY . .
WORKDIR "/source/src/Api"
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

# Estágio 2: Imagem final de execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
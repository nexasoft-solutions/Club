# Etapa 1: Build y publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia todo el código fuente al contenedor
COPY . .

# Restaurar dependencias
RUN dotnet restore

# Publicar el proyecto en modo Release
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar archivos publicados desde la etapa de build
COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 5050

ENTRYPOINT ["dotnet", "NexaSoft.Agro.Api.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Instalar herramientas útiles
RUN apk add --no-cache curl jq

# Salud más robusta
HEALTHCHECK --interval=30s --timeout=10s --start-period=60s --retries=3 \
  CMD vault status -format=json || exit 1

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 5050
ENTRYPOINT ["dotnet", "NexaSoft.Club.Api.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# ✅ Instalar curl en imagen runtime
RUN apt-get update && apt-get install -y curl

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 5050
ENTRYPOINT ["dotnet", "NexaSoft.Club.Api.dll"]

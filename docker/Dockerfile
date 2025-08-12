FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY publish/ .

ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 5050

ENTRYPOINT ["dotnet", "NexaSoft.Agro.Api.dll"]

# Base dotnet image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Add curl to template.
# CDP PLATFORM HEALTHCHECK REQUIREMENT
RUN apt update && \
    apt install curl -y && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY PhaImportNotifications/PhaImportNotifications.csproj PhaImportNotifications/PhaImportNotifications.csproj
COPY PhaImportNotifications.Test/PhaImportNotifications.Test.csproj PhaImportNotifications.Test/PhaImportNotifications.Test.csproj
COPY PhaImportNotifications.sln PhaImportNotifications.sln

RUN dotnet restore

COPY PhaImportNotifications PhaImportNotifications
COPY PhaImportNotifications.Test PhaImportNotifications.Test

ENV PATH="$PATH:/root/.dotnet/tools"
COPY .csharpierrc .csharpierrc
RUN dotnet tool install csharpier -g && \
    dotnet csharpier --check .

RUN dotnet test --no-restore PhaImportNotifications.Test

FROM build AS publish

RUN dotnet publish PhaImportNotifications -c Release -o /app/publish /p:UseAppHost=false

ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

EXPOSE 8085
ENTRYPOINT ["dotnet", "PhaImportNotifications.dll"]

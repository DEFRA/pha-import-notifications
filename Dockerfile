# Base dotnet image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Add curl to template.
# CDP PLATFORM HEALTHCHECK REQUIREMENT
RUN apt update && \
    apt upgrade -y && \
    apt install curl -y && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG VACUUM_VERSION=0.14.2
WORKDIR /tmp/vacuum
RUN wget "https://github.com/daveshanley/vacuum/releases/download/v${VACUUM_VERSION}/vacuum_${VACUUM_VERSION}_linux_x86_64.tar.gz" -q -O vacuum.tar.gz && \
    tar zxvf "vacuum.tar.gz" && \
    mv vacuum /usr/bin/vacuum
 
WORKDIR /src

ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet tool install -g csharpier && \
    dotnet tool install -g Swashbuckle.AspNetCore.Cli
 
COPY .csharpierrc .csharpierrc
COPY .vacuum.yml .vacuum.yml

COPY PhaImportNotifications/PhaImportNotifications.csproj PhaImportNotifications/PhaImportNotifications.csproj
COPY PhaImportNotifications.Tests/PhaImportNotifications.Tests.csproj PhaImportNotifications.Tests/PhaImportNotifications.Tests.csproj
COPY PhaImportNotifications.IntegrationTests/PhaImportNotifications.IntegrationTests.csproj PhaImportNotifications.IntegrationTests/PhaImportNotifications.IntegrationTests.csproj
COPY PhaImportNotifications.sln PhaImportNotifications.sln

RUN dotnet restore

COPY PhaImportNotifications PhaImportNotifications
COPY PhaImportNotifications.Tests PhaImportNotifications.Tests
COPY PhaImportNotifications.IntegrationTests PhaImportNotifications.IntegrationTests

RUN dotnet csharpier --check . 

RUN dotnet build --no-restore -c Release
RUN swagger tofile --output openapi.json ./PhaImportNotifications/bin/Release/net8.0/PhaImportNotifications.dll v1
RUN vacuum lint -d -r .vacuum.yml openapi.json

RUN dotnet test --no-restore PhaImportNotifications.Tests
RUN dotnet test --no-restore PhaImportNotifications.IntegrationTests

FROM build AS publish

RUN dotnet publish PhaImportNotifications -c Release -o /app/publish /p:UseAppHost=false

ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

EXPOSE 8085
ENTRYPOINT ["dotnet", "PhaImportNotifications.dll"]

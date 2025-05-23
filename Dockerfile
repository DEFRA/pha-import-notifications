# Base dotnet image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Add curl to template.
# CDP PLATFORM HEALTHCHECK REQUIREMENT
RUN apt update && \
    apt upgrade -y && \
    apt --no-install-recommends install curl -y && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

ARG VACUUM_VERSION=0.14.2
WORKDIR /tmp/vacuum
ADD "https://github.com/daveshanley/vacuum/releases/download/v${VACUUM_VERSION}/vacuum_${VACUUM_VERSION}_linux_x86_64.tar.gz" vacuum.tar.gz
RUN tar zxvf "vacuum.tar.gz" && \
    mv vacuum /usr/bin/vacuum

WORKDIR /src

ENV PATH="$PATH:/root/.dotnet/tools"

COPY .config/dotnet-tools.json .config/dotnet-tools.json
COPY .csharpierrc .csharpierrc
COPY .vacuum.yml .vacuum.yml

RUN dotnet tool restore

COPY src/Api/Api.csproj src/Api/Api.csproj
COPY src/Contracts/Contracts.csproj src/Contracts/Contracts.csproj
COPY tests/PhaImportNotifcations.Tests.csproj tests/PhaImportNotifcations.Tests.csproj
COPY tools/SchemaToCSharp/SchemaToCSharp.csproj tools/SchemaToCSharp/SchemaToCSharp.csproj
COPY Defra.PhaImportNotifications.sln Defra.PhaImportNotifications.sln
COPY Directory.Build.props Directory.Build.props

RUN dotnet restore

COPY src/Api src/Api
COPY src/Contracts src/Contracts
COPY tests tests

RUN dotnet csharpier check .

RUN dotnet build src/Api/Api.csproj --no-restore -c Release
RUN dotnet swagger tofile --output openapi.json ./src/Api/bin/Release/net9.0/Defra.PhaImportNotifications.Api.dll v1
RUN vacuum lint -d -r .vacuum.yml openapi.json

RUN dotnet test --no-restore tests/PhaImportNotifcations.Tests.csproj

FROM build AS publish

RUN dotnet publish src/Api -c Release -o /app/publish /p:UseAppHost=false

ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true

FROM base AS final

USER app

WORKDIR /app

COPY --from=publish /app/publish .

EXPOSE 8085
ENTRYPOINT ["dotnet", "Defra.PhaImportNotifications.Api.dll"]

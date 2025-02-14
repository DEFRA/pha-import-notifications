# Defra PHA Import Notifications

This service provides Port Health Authorities (PHAs) access to a mixture of IPAFFS, ALVS, CDS and GVMS data.

The solution supplies an endpoint for querying for updated records within a certain time period
and then provides the ability to retrieve all the data for those records.

## Prerequisites

The solution requires:

- .NET 9

  ```bash
  brew tap isen-ng/dotnet-sdk-versions
  brew install --cask dotnet-sdk9
  ```

- Docker (optional)

## Installation

1. Clone this repository
2. Install the required tools with `dotnet tool restore`
3. Check the solution builds with `dotnet build`
4. Check the service builds with `docker build .`

## Running

1. Run the application via your favourite IDE or via Docker:
   ```
   docker build -t pha-import-notifications .
   docker run -p 8080:8080 \
    -e ASPNETCORE_ENVIRONMENT=Development
    -e ENVIRONMENT=Local
    pha-import-notifications
   ```
2. Navigate to http://localhost:8080/redoc

## Endpoints

The endpoints are detailed in the OpenAPI spec which is available either via the Redoc URL,
or alternatively via the [OpenAPI Spec URL](http://localhost:8080/.well-known/openapi/v1/openapi.json).

A JWT is required to authenticate the user when making requests.  
A local one can be generated by running `./scripts/generate-jwt.sh` and then including it as a `Bearer` token in the `Authorization` header.

Get all updated import notifications within a time period:
```http request
http://localhost:8080/import-notifications?bcp=BCP1&from=2025-02-12T12:00:00+00:00&to=2025-02-12T12:30:00+00:00
```

Get an import notification:
```http request
http://localhost:8080/import-notifications/CHEDA.GB.2024.4792831
```

## Testing

The unit and integration tests can either be run via your IDE or alternatively with `dotnet test .`

A SonarCloud setup will run static analysis on the code when raising a PR.

# Linting

We use [CSharpier](https://csharpier.com) to lint our code.

You can run the linter with `dotnet csharpier .`

## License

This project is licensed under The Open Government Licence (OGL) Version 3.  
See the [LICENSE](./LICENSE) for more details.
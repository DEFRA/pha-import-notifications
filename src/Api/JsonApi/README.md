# Json API

Types and `System.Text.Json` converters taken from https://github.com/json-api-dotnet/JsonApiDotNetCore

We do not want to reference the entire package is it pulls in too many dependencies we don't need and increases our exposure to potential security vulnerabilities.

`JsonApiClient` is our own simple wrapper to encapsulate Json API behaviour and should be extended/changed appropriately.
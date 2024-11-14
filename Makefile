dependencies:
	dotnet tool install -g Swashbuckle.AspNetCore.Cli --version 6.9.0

lint-openapi-spec: dependencies
	dotnet build -c Release --no-restore
	swagger tofile --output openapi.json ./src/Api/bin/Release/net8.0/Defra.PhaImportNotifications.Api.dll v1
	docker run --rm -v "$(PWD):/work:ro" dshanley/vacuum lint -d -r .vacuum.yml openapi.json

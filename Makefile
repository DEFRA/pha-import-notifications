dependencies:
	dotnet tool install -g Swashbuckle.AspNetCore.Cli

lint-openapi-spec: dependencies
	dotnet build -c Release --no-restore
	swagger tofile --output openapi.json ./PhaImportNotifications/bin/Release/net8.0/PhaImportNotifications.dll v1
	docker run --rm -v "$(PWD):/work:ro" dshanley/vacuum lint -d -r .vacuum.yml openapi.json

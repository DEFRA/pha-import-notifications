lint-openapi-spec:
	dotnet build -c Release --no-restore
	docker run --rm -v "$(PWD):/work:ro" dshanley/vacuum lint -d -r .vacuum.yml openapi.json

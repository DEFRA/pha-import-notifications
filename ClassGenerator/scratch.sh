
#curl -o cdms-public-openapi-v0.1.json  http://localhost:5000/swagger/public-v0.1/swagger.json
#Nswag
#cdms 
#npx @openapitools/openapi-generator-cli generate -i cdms-public-openapi-v0.1.json -g csharp -o ./clients/cdms
#openapi-generator generate -i cdms-public-openapi-v0.1.json -g csharp -o ./clients/cdms --skip-validate-spec 
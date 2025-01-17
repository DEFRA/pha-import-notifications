# Schema to CSharp

When the BTMS schema changes, we need to generate our types again.

Temporary changes within BTMS have been captured in branch https://github.com/DEFRA/btms-backend/tree/openapi-for-phaapi

The BTMS branch above should checked out and rebased onto origin/main.

BTMS should be started and the Open API spec path should be requested via `http://localhost:5002/swagger/public-v0.1/swagger.json`

The resultant spec JSON should be saved within this console app as `btms-public-openapi-v0.1.json`

The console app should be run and any schema changes assessed.

There may be changes to types or naming differences that means our local Ignored, Meta and Rename files need to be updated.

We should be producing the contract types that limit a level of change for our PHAs, otherwise we will need to publish a schema update of our own.
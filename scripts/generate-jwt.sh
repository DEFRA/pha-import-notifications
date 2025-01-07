#!/usr/bin/env bash

client_id=${1:-'1l0fkp1saladf1ng3r520r0012'}

header='{ "alg": "RS256", "kid": "0001" }'

payload='{
    "token_use": "access",
    "scope": "pha-import-notifications-resource-srv/access",
    "auth_time": 1735901021,
    "iss": "https://cognito-idp.eu-west-2.amazonaws.com/eu-west-2_LOCAL",
    "exp": 1735904621,
    "iat": 1735901021,
    "version": 2
}'

payload=$(
    echo "$payload" | \
        jq --arg client_id "$client_id" --arg time_str "$(date +%s)" --arg jti "$(uuidgen)" \
    '
    ($time_str | tonumber) as $time_num
    | .sub=$client_id
    | .client_id=$client_id
    | .iat=$time_num
    | .auth_time=$time_num
    | .exp=($time_num + 600)
    | .jti=$jti
    '
)

base64_encode()
{
	printf '%s' "$(</dev/stdin)" | base64 | tr -d '=' | tr '/+' '_-' | tr -d '\n'
}

hmacsha256_sign()
{
	printf '%s' "$(</dev/stdin)" | openssl dgst -binary -sha256 -hmac "pha-import-notifications"
}

header_base64=$(echo "${header}" | jq -c . | base64_encode)
payload_base64=$(echo "${payload}" | jq -c . | base64_encode)

header_payload="${header_base64}.${payload_base64}"
signature=$(echo "${header_payload}" | hmacsha256_sign | base64_encode)

echo "${header_payload}.${signature}"

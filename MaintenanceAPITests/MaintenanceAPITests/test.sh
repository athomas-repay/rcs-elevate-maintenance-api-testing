# Set environment variables
apiGatewayUrl={API_GATEWAY_URL:https://test.trisourcesolutions.com/maintenanceapi-uat/v1/}
repaySsoUrl={REPAY_SSO_URL:https://sso.sandbox.repay.com/auth/realms/repay/protocol/openid-connect/token}
clientSecret={SSO_CLIENT_SECRET:__REQUIRED__}

# Replace tokens in appsettings.json
sed \
    -e "s~{api-gateway-url}~$apiGatewayUrl~g" \
    -e "s~{svc-client-secret}~$clientSecret~g" \
    -e "s~{repay-sso-url}~$repaySsoUrl~g" \
    appsettings.tokens.json > appsettings.json

if grep -q __REQUIRED__ appsettings.json; then
    cat appsettings.json
    echo "** Required environment variables not present **"
    exit 1
fi

cp /certs/ca/tss.pem /usr/local/share/ca-certificates/tss.certificates

update-ca-certificates

dotnet test
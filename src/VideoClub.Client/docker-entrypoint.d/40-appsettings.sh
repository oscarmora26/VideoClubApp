#!/bin/sh
# Genera appsettings.json con la URL de la API desde variable de entorno
API_URL="${API_URL:-http://localhost:5144}"

cat > /usr/share/nginx/html/appsettings.json <<EOF
{
  "ApiUrl": "$API_URL"
}
EOF


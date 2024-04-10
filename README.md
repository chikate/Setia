## Base WebAPP

Right click on **Base** project > select **"Manage User Secrets"** and paste this code in there but dont forget to set your
- **Connection Strings**,
- **Audience** link, 
- **Issuer** link, 
- **Key** code,
- **Origin** link

```json
{
  "ConnectionStrings": {
    "Setia": "Server=DRAGOS;Database=Setia;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Email": {
    "Password": "",
    "User": ""
  },
  "JWT": {
    "Audience": "http://localhost:CLIENT_PORT",
    "Issuer": "https://localhost:SERVER_PORT",
    "Key": "Generate a key and insert it here"
  },
  "Origin": "http://localhost:CLIENT_PORT"
}
```

For adding aditional CRUD functionalities: 
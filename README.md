## Base WebAPP

Right click on base project > select "Manage User Secrets" and paste this code in there but dont forget to set your
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
  "JWT": {
    "Audience": "https://localhost:PORT",
    "Issuer": "https://localhost:PORT",
    "Key": "Generate a key here"
  },
  "Origin": "http://localhost:PORT"
}
```

For adding aditional CRUD functionalities: 
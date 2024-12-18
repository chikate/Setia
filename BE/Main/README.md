# Base WebAPP

Right click on **Base** project > select **"Manage User Secrets"** and paste this code in there but dont forget to set your

- **Connection Strings**,
- **Audience** link,
- **Issuer** link,
- **Key** code,
- **Origin** link

```json
{
  "ConnectionStrings": {
    "MsSql": {
      "Setia": "Server=Server;Database=Setia;Trusted_Connection=True;TrustServerCertificate=True;"
    },
    "PostgreSql": {
      "Setia": "User ID=postgres;Password=Password;Host=localhost;Port=5432;Database=Setia;"
    }
  },
  "Email": {
    "User": "",
    "Password": ""
  },
  "JWT": {
    "Audience": "https://localhost:PORT",
    "Issuer": "https://localhost:PORT",
    "Key": "LbV39BSC079qcfUCG0Wwusl31S8Uu0Qo"
  },
  "Origin": "http://localhost:OTHER_PORT",
  "SA": {
    "Username": "",
    "Password": ""
  }
}
```

# Migrations

To add a new migration:

```bash
Add-Migration 1 -Context BaseContext -Output Data/Migrations/Base
```

# CRUDs Service

Create a new model in the **Models** folder
Then go to **Program.cs** and to //CRUDs section (you can ctrl + f search for //CRUDs)
add this scope and adjust it properly

```csharp
builder.Services.AddScoped<ICRUD</*ModelName*/>, CRUDService</*ModelName*/, /*DbContext*/>>();
```

then in the **Gateway** folder in **CRUDsController.cs**
add this new class and adjust it properly

```csharp
public class /*CRUDName*/Controller(ICRUD</*CRUDModel*/> CRUD) : CRUDController</*CRUDModel*/>(CRUD);
```

and you are done :)

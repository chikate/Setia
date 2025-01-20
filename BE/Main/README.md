# Configure secret settings

Right click on **Main** project > select **"Manage User Secrets"** and paste this code in there but dont forget to configure it corespondently

```json
{
  "AdminAccount": {
    "Username": "Dragos",
    "Password": "Password"
  },
  "CryptKey": "", // 32 alphanumeric string
  "DBConnectionStrings": "Host=localhost:5432;DataBase=Setia;User ID=postgres;Password=Dragos123;", // Data base connection string
  "DBTech": "PgSQL",
  "SmtpSettings": {
    "Server": "smtp.example.com",
    "Port": 587,
    "SenderName": "Your Name",
    "SenderEmail": "your.email@example.com",
    "Username": "your.email@example.com",
    "Password": "yourpassword",
    "EnableSsl": true
  },
  "Origin": "http://localhost:3000",
  "Server": "https://localhost:5000",
  "StoragePath": "D:\\Dragos\\Downloads\\Drive"
}
```

# Migrations

To add a new migration:

```bash
Add-Migration 1 -Context BaseContext -Output Standards/Data/Migrations
```
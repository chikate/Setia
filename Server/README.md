# 🧩 Base Web App Setup Guide

**Status:** 🚧 _Work in Progress_

# Configure secret settings

Right click on **Main** project file > select **"Manage User Secrets"** and paste this code in there but dont forget to configure it corespondently

```json
// dotnet user-secrets set "Email-Service-Account:Username" "email-username"
{
  "AdminAccount": {
    "Username": "yourusername",
    "Password": "yourpassword"
  },
  "CryptKey": "uwXk6g6Kxr166TGji80ICeI0udlzm7hT", // 32 alphanumeric string like its shown, recomanded to change it for every instance
  "DBConnectionStrings": "Server=localhost:5432;DataBase=Setia;User ID=postgres;Password=dragos;",
  "DBTech": "PgSQL",
  "HOST_Client": "http://localhost:3000",
  "HOST_Server": "https://localhost:5000",
  "SMTPSettings": {
    "EnableSsl": true,
    "Password": "yourpassword",
    "Port": 587,
    "SenderEmail": "your.email@example.com",
    "SenderName": "Your Name",
    "Server": "smtp.example.com",
    "Username": "your.email@example.com"
  },
  "StoragePath": "D:\\Dragos\\Downloads\\drive"
}
```

# Migrations

To add a new migration:

```bash
EntityFrameworkCore\Add-Migration 1 -Context BaseContext -Output Data/Migrations
```

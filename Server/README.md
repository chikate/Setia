# 🧩 Base Web App Setup Guide

**Status:** 🚧 _Work in Progress_

# Configure secret settings

Right click on **Main** project file > select **"Manage User Secrets"** and paste this code in there but dont forget to configure it corespondently

```json
// dotnet user-secrets set "Email-Service-Account:Username" "email-username"
  "DB": {
    "Tech": "PgSQL",
    "Database": "Setia",
    "User": "postgres",
    "Password": "password"
  },
  "Email": {
    "Username": "",
    "Password": ""
  },
  "Audience": "http://localhost:3000",
  "Origin": "http://localhost:5000",
  "CryptKey": "LbV39BSCG0Wwusl31S8C079qcfUUu0Qo",
  "SuperUser": {
    "Username": "",
    "Password": ""
  },
```

# Migrations

To add a new migration:

```bash
EntityFrameworkCore\Add-Migration 1 -Context BaseContext -Output Data/Migrations
```

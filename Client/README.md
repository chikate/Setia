# 🧩 Base Web App Setup Guide

**Status:** 🚧 _Work in Progress_

## 🚀 1. Update Dependencies

Update all packages in **`package.json`** to their latest versions:

```bash
ncu -u
```

to replace the last version string in the package.json

then npm install to updated dependencies:

```bash
npm i
```

## 🧹 2. Check Deprecated Dependencies

To inspect which dependency is using the deprecated rimraf package:

```bash
npm ls rimraf
```

If there are dependency conflicts during installation, use:

```bash
npm install --legacy-peer-deps
```

## ⚙️ 3. Auto-Generate API Clients (OpenAPI)

Generate TypeScript Fetch clients directly from the OpenAPI schema:

```bash
openapi-generator-cli generate -g typescript-fetch -i https://localhost:5000/openapi/v1.json -o C:\Users\Dragos\Desktop\Setia\Client\src\composables
```

If you experience SSL validation issues while generating APIs, set the following PowerShell environment variable:

```bash
$env:JAVA_TOOL_OPTIONS="-Dcom.sun.net.ssl.checkRevocation=false -Djavax.net.ssl.trustStoreType=Windows-ROOT"
```

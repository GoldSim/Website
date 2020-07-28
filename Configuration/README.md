## Instructions

### Connection Strings
To connect to a database locally, establish a `secrets.json` at e.g.:
```
\Users\USERNAME\AppData\Roaming\Microsoft\UserSecrets\1db4df2e-af3f-456c-ac4a-b8861b0c21a9\secrets.js
```

Then add the connection string using the following format:

```json
{
  "ConnectionStrings": {
    "OnTopic": "CONNECTION_STRING"
  }
}
```

### Database
To import a `dacpac` file from Azure, go to the following folder in the Visual Studio installation directory:
```
\Common7\IDE\Extensions\Microsoft\SQLDB\DAC\140
```
And execute the following command:
```
SqlPackage.exe /a:import /tcs:"{ConnectionString}" /sf:"{Filename}.bacpac"
```
Instructions
============

Connection Strings
------------------
To connect to a database, create a `connectionStrings.config` with the following format:

```
<?xml version="1.0"?>
<connectionStrings>
  <clear />
  <add name="TopicsServer" connectionString="…" />
  <add name="Membership" connectionString="…" />
</connectionStrings>
```

Then create a `connectionStrings.Debug.config` and `connectionStrings.Release.config` with the following format:

```
<?xml version="1.0"?>
<connectionStrings xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <add name="TopicsServer" connectionString="…" xdt:Transform="SetAttributes" xdt:Locator="Match(name)" />
  <add name="Membership" connectionString="…" xdt:Transform="SetAttributes" xdt:Locator="Match(name)" />
</connectionStrings>
```
Database
--------
To import a `dacpac` file from Azure, go to the following folder in the Visual Studio installation directory:
```
\Common7\IDE\Extensions\Microsoft\SQLDB\DAC\140
```
And execute the following command:
```
SqlPackage.exe /a:import /tcs:"{ConnectionString}" /sf:"{Filename}.bacpac"
```
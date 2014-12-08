Forum Bash
=========

### Tips you may need
-- StackOverflowAPI : self-implemented API to get data from StackOverflow

-- Utils : Operations with database

-- Moelds/ODataOpenIssues : DbContext

-- This project uses MVC to build the web site.


### Setting Up
This project using EF code first, so you just need to create an database and modify the database config in web.config.
```
<connectionStrings>
    <add name="ODataOpenIssues" connectionString="Data Source=ServerName;Initial Catalog=DbName;Persist Security Info=False;User ID=username;Password=pwd" providerName="System.Data.SqlClient" />
</connectionStrings>
```
In web.config, you also need to set up a file path for log file.
```
  <appSettings>
    ...
    <add key="LogFilePath" value="D:\log\log.txt" />
  </appSettings>
```

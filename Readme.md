# Sample Project

Demonstrating some features of 
- dotnet 6 (DateOnly, top level statements, minimal web api, ...)
- HotChocolate GraphQL Server

Project has been created using:

``` console
dotnet new web
dotnet add package HotChocolate.AspNetCore
dotnet add package HotChocolate.Data.EntityFramework
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
```

and then building models, repositories etc accordingly.

# Common API

## Common Libraries  

> dotnet new classlib --name  Common.Libraries  
> cd ../.. && dotnet sln add ./Services/Common/Common.Libraries/Common.Libraries.csproj  

## Common Libraries Test  

> dotnet new classlib --name  Common.LibrariesTest  
> cd ../.. && dotnet sln add ./Services/Common/Common.LibrariesTest/Common.LibrariesTest.csproj  

## Path: **microservice-sample > Services > Common > Common.API**  

> mkdir Common && cd Common  
> dotnet new webapi --name Common.API --use-minimal-apis true --no-https true  
> cd ../.. && dotnet sln add ./Services/Common/Common.API/Common.API.csproj  
> cd Services/Common/Common.API  
> dotnet add reference ../../Common/Common.Libraries/Common.Libraries.csproj  
> dotnet new mstest --name Common.API.Test  
> cd ../.. && dotnet sln add ./Services/Common/Common.API.Test/Common.API.Test.csproj  

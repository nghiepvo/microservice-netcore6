# Microservice netcore6

### Solution structure: **microservice-sample**  

> mkdir src && cd src  
> dotnet new sln  
> mv src.sln microservice-sample.sln  
> donet new globaljson --sdk-version 6.0.201  

Generate tasks.json and launch.json  

### Catalog API Project: **microservice-sample > Services > Catalog > Catalog.API**  
> mkdir Services && cd Services  
> mkdir Catalog && cd Catalog  
> dotnet new webapi --name Catalog.API --use-minimal-apis true --no-https true  
> cd ../.. && dotnet sln add ./Services/Catalog/Catalog.API/Catalog.API.csproj  
> cd Services/Catalog/Catalog.API  

Generate tasks.json and launch.json for run or debug.
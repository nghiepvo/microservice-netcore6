# Basket API

## Path: **microservice-sample > Services > Catalog > Catalog.API**  

> mkdir Services && cd Services  
> mkdir Basket && cd Basket  
> dotnet new webapi --name Basket.API --use-minimal-apis true --no-https true  
> cd ../.. && dotnet sln add ./Services/Basket/Basket.API/Basket.API.csproj  
> cd Services/Basket/Basket.API  
> dotnet add reference ../../Common/Common.Libraries/Common.Libraries.csproj  
> dotnet new mstest --name Basket.API.Test  
> cd ../.. && dotnet sln add ./Services/Basket/Basket.API.Test/Basket.API.Test.csproj  

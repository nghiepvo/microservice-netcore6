# Catalog API  

*On Windows, please use git bash because the all commend line below which was developed on linux environment and VSCode. **I still don't verify on Windows yet. :D**
And update hosts file with content below:*  

```text
127.0.0.1       catalog.db
```  

*For run Catalog.API project please run tasks (Ctr+Shift+B) > select **Docker Compose Catalog Development Up** and stop **Docker Compose Catalog Development Down***  

## Path: **microservice-sample > Services > Catalog > Catalog.API**  

> mkdir Services && cd Services  
> mkdir Catalog && cd Catalog  
> dotnet new webapi --name Catalog.API --use-minimal-apis true --no-https true  
> cd ../.. && dotnet sln add ./Services/Catalog/Catalog.API/Catalog.API.csproj  
> cd Services/Catalog/Catalog.API  
> dotnet add reference ../../Common/Common.Libraries/Common.Libraries.csproj

- Generate tasks.json and launch.json for run or debug.  
- Generate docker-compose file.  
  *Look on task setting (.vscode/task.json) for build docker image. **Docker build Catalog API***  
- Using Mongodb on docker.  
    > docker pull mongo  
    > docker run -d -p 27017:27017 --name shopping-mongo mongo  
    > docker logs -f shopping-mongo  
    > docker exec -it shopping-mongo /bin/bash  
    Mongo CLI:  
        > mongo  
        > show dbs  
        > use CatalogDb
        > db.createCollection('Products')  
        > db.Products.insertMany([{ 'Name':'Asus Laptop','Category':'Computers', 'Summary':'Summary', 'Description':'Description', 'ImageFile':'ImageFile', 'Price':54.93 }, { 'Name':'HP Laptop','Category':'Computers', 'Summary':'Summary', 'Description':'Description', 'ImageFile':'ImageFile', 'Price':88.93 } ])
        > db.Products.find({}).pretty()  
        > db.Products.remove({})  
        > show databases  
        > show collections  
        > db.Products.find({}).pretty()  
    Data:  

    ```json
    [
        {
            "Name": "Asus Laptop",
            "Category": "Computers",
            "Summary": "Summary",
            "Description": "Description",
            "ImageFile": "ImageFile",
            "Price": 54.93
        },
        {
            "Name": "HP Laptop",
            "Category": "Computers",
            "Summary": "Summary",
            "Description": "Description",
            "ImageFile": "ImageFile",
            "Price": 88.93
        }
    ]
    ```  

- Document:  

| Method     | URL                                             | User case                        |
| ---------- | ----------------------------------------------- | -------------------------------- |
| **GET**    | api/v1/Catalog                                  | List of Product and Categories   |
| **GET**    | api/v1/Catalog{id}                              | Get Product with product Id      |
| **GET**    | api/v1/Catalog/GetProductsByCategory/{category} | Get Products with Category       |
| **POST**   | api/v1/Catalog                                  | Create new a Product             |
| **PUST**   | api/v1/Catalog                                  | Update a Product with product Id |
| **DELETE** | api/v1/Catalog                                  | Delete a Product with product Id |

| Permission | Value |
| ---------- | ----- |
| **CREATE** | 1000  |
| **READ**   | 1001  |
| **UPDATE** | 1002  |
| **DELETE** | 1003  |

- Use DDD Archictecture  
- Library  
  - [Mongodb Entities](https://mongodb-entities.com/)  
  - [Fast Endpoints](https://fast-endpoints.com/)  
- Test with MSTest.  
  *go to root folder for run catalog.db in docker-compose*  
  > docker-compose -f docker-compose.yml -f docker-compose.debug.yml up -d catalog.db  
  > cd src/Services/Catalog/Catalog.API.Test  
  > dotnet test /p:CollectCoverage=true  

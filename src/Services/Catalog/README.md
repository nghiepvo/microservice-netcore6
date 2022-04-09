# Catalog API 

### Path: **microservice-sample > Services > Catalog > Catalog.API**  
> mkdir Services && cd Services  
> mkdir Catalog && cd Catalog  
> dotnet new webapi --name Catalog.API --use-minimal-apis true --no-https true  
> cd ../.. && dotnet sln add ./Services/Catalog/Catalog.API/Catalog.API.csproj  
> cd Services/Catalog/Catalog.API  

Generate tasks.json and launch.json for run or debug.
- Using Mongodb on docker.  
    > docker pull mongo  
    > docker run -d -p 27017:27017 --name shopping-mongo mongo  
    > docker logs -f shopping-mongo  
    > docker exec -it shopping-mongo /bin/bash      
    - Mongo CLI:  
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
    - Data: 
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

- Setting: 
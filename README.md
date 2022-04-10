# Microservice netcore6

### Solution structure: **microservice-sample**  

> mkdir src && cd src  
> dotnet new sln  
> mv src.sln microservice-sample.sln  
> donet new globaljson --sdk-version 6.0.201 

### Docker note for linux  
> sudo usermod -aG docker nv  
> sudo setfacl -m user:nv:rw /var/run/docker.sock  
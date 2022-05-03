# Microservice netcore6  

### Solution structure: **microservice-sample**  

> mkdir src && cd src  
> dotnet new sln  
> mv src.sln microservice-sample.sln  
> donet new globaljson --sdk-version 6.0.201 

### Docker note for linux  
> sudo usermod -aG docker nv  
> sudo setfacl -m user:nv:rw /var/run/docker.sock  

### Setting for VSCode  
  Please look on .vscode folder for all.  

### Testing note  
  For setup with Integration test, you should fill the config below on project which is need to testing.  

```xml
<PropertyGroup>
  ...
  <ProduceReferenceAssemblyInOutDir>true</ProduceReferenceAssemblyInOutDir>
</PropertyGroup>
...
<ItemGroup>
  <InternalsVisibleTo Include="$(AssemblyName).Test" />
</ItemGroup>
```  

### Sonar  
  Run sonar on docker and setup with name "Microservice-Net6", of course you also can change it :).
> docker-compose -f docker-compose.sonar.yml up -d  

  Go to src folder for run the command line bellow:  
> dotnet tool install --global dotnet-sonarscanner  

  Setting to task.json file as well. with some command line bellow  
> dotnet sonarscanner begin /k:"Microservice-NET6" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="admin" /d:sonar.password="123456" /d:sonar.cpd.exclusions="**/Migrations/*.cs" /d:sonar.cs.opencover.it.reportsPaths="/app/src/coverage.opencover.xml"  
> dotnet build  
> dotnet test --collect:"XPlat Code Coverage"  

> dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=$PWD/coverage.opencover.xml  

> dotnet sonarscanner end /d:sonar.login="admin" /d:sonar.password="123456"  

  Go to SonarQube Web UI > Select Project > Project Seting > Language > C# > OpenCover Integration Tests Reports Paths > /home/nv/repos/microservice-netcore6/src/coverage.opencover.xml  

  Set Environment on linux for run sonarqube-runner.sh  

> export Sonar_Project="Microservice-NET6"  
> export Sonar_Url="https://aaf2-115-74-83-192.ap.ngrok.io"  
> export Sonar_Login="admin"  
> export Sonar_Password="123456"  
> chmod +x sonarqube-runner.sh  

### NGROK  
  For sonar publish to buid
> sudo snap install ngrok  
> rm -f ngrok.log && ngrok http 9000 --log=stdout > ngrok.log &  
> cat ngrok.log  

### Docker Build
> docker build -t net6-java11-sonarscanner-build -f ./Docker-Build/Dockerfile .  
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
> dotnet sonarscanner begin /k:"Microservice-Net6" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="5c2db1d37a208fd594aae6625f6a844f15f6dd78"  
> dotnet build  
> dotnet test --collect:"XPlat Code Coverage"  

> dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=./coverage.opencover.xml  

> dotnet sonarscanner end /d:sonar.login="5c2db1d37a208fd594aae6625f6a844f15f6dd78"  

  Go to SonarQube Web UI > Select Project > Project Seting > Language > C# > OpenCover Integration Tests Reports Paths > /home/nv/repos/microservice-netcore6/src/coverage.opencover.xml  

> chmod +x sonarqube-runner.sh   
cd src
dotnet sonarscanner begin /k:"Microservice-NET6" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="ea5b4c1ec79e73ae9d74a3ce5aad14fba711bb77"
dotnet build
dotnet test --collect:"XPlat Code Coverage"
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=$PWD/coverage.opencover.xml
dotnet sonarscanner end /d:sonar.login="ea5b4c1ec79e73ae9d74a3ce5aad14fba711bb77"
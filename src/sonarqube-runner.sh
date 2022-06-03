cd src
export Ogr="nghiepvo"

export Sonar_Project="Microservice-NET6"

export Sonar_Url="https://sonarcloud.io"

export SONAR_TOKEN=bc9b677608f5219d8306598ee4a937d727ae4bd2

export PATH="$PATH:$HOME/.dotnet/tools"

dotnet sonarscanner begin /o:$Ogr /k:$Sonar_Project /d:sonar.host.url=$Sonar_Url  /d:sonar.coverage.exclusions="**/Migrations/*.cs,**/**Test" /d:sonar.cs.opencover.it.reportsPaths="$PWD/coverage.opencover.xml"
dotnet build
dotnet test --collect:"XPlat Code Coverage"
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=$PWD/coverage.opencover.xml
dotnet sonarscanner end
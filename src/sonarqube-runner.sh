cd src
export Ogr="nghiepvo"

export Sonar_Project="Microservice-NET6"

export Sonar_Url="https://sonarcloud.io"

export SONAR_TOKEN=bc9b677608f5219d8306598ee4a937d727ae4bd2

export PATH="$PATH:$HOME/.dotnet/tools"

dotnet sonarscanner begin /o:$Ogr /k:$Sonar_Project /d:sonar.host.url=$Sonar_Url /d:sonar.coverage.exclusions="**/Migrations/**" /d:sonar.cpd.exclusions="**/Migrations/**" /d:sonar.cs.opencover.reportsPaths="$PWD/.coverage/**"
dotnet build
dotnet test ./Services/Common/Common.API.Test/Common.API.Test.csproj --collect:"XPlat Code Coverage" /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=$PWD/.coverage/common.api.opencover.xml
dotnet test ./Services/Catalog/Catalog.API.Test/Catalog.API.Test.csproj --collect:"XPlat Code Coverage" /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=$PWD/.coverage/catalog.api.opencover.xml
dotnet test ./Services/Basket/Basket.API.Test/Basket.API.Test.csproj --collect:"XPlat Code Coverage" /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=$PWD/.coverage/basket.api.opencover.xml
dotnet sonarscanner end
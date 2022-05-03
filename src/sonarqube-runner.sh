cd src
export Sonar_Project="Microservice-Net6"
export Sonar_Url="https://caa6-115-74-83-192.ap.ngrok.io"
export Sonar_Login="admin"
export Sonar_Password="123456"
dotnet sonarscanner begin /k:"$Sonar_Project" /d:sonar.host.url="$Sonar_Url" /d:sonar.login="$Sonar_Login" /d:sonar.password="$Sonar_Password"
dotnet build
dotnet test --collect:"XPlat Code Coverage"
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=$PWD/coverage.opencover.xml
dotnet sonarscanner end /d:sonar.login="$Sonar_Login" /d:sonar.password="$Sonar_Password"
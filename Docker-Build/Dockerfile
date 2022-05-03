FROM mcr.microsoft.com/dotnet/sdk:6.0-focal

RUN apt-get update \
    && apt-get install -y openjdk-11-jdk \
    && rm -rf /var/lib/apt/lists/* \
    && export PATH="$PATH:/root/.dotnet/tools" \
    && dotnet tool install --global dotnet-sonarscanner
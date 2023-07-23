FROM mcr.microsoft.com/dotnet/sdk:6.0

COPY pyCosmosInstaller pyCosmosInstaller
COPY cosmos.json cosmos.json

RUN apt-get update && \
    apt-get -y install build-essential python3 && \
    python3 -m pyCosmosInstaller install

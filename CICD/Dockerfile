FROM mcr.microsoft.com/dotnet/sdk:6.0

RUN apt-get update && \
    apt-get -y install build-essential python3 && \
    python3 -m pyCosmosInstaller install

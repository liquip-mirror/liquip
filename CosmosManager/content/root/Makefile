cosmos-install:
	cosmos-manager install

build: clean
	dotnet build

clean:
	dotnet clean

package: build
	dotnet package

update-cosmos_manager:
	dotnet tool update --global CosmosManager --prerelease --no-cache

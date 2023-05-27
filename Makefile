
install-cosmos:
	python3 -m CosmosInstaller install

build: clean
	dotnet build

clean:
	dotnet clean

package: build
	dotent package

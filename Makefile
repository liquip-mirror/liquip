
install-cosmos:
	python3 -m pyCosmosInstaller install

build: clean
	dotnet build

clean:
	dotnet clean

package: build
	dotent package

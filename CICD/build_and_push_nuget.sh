#!/bin/sh

cd $1

dotnet pack -c Release
dotnet nuget push "bin/Release/*.nupkg"

cd ../
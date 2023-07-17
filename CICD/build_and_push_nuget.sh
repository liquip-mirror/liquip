#!/bin/sh
set -e 

cd $1

dotnet nuget add source "${CI_API_V4_URL}/projects/42768959/packages/nuget/index.json" --name gitlab --username gitlab-ci-token --password $CI_JOB_TOKEN --store-password-in-clear-text

dotnet build /p:Version="0.1.${CI_PIPELINE_IID:-local}-alpha"
dotnet pack /p:Version="0.1.${CI_PIPELINE_IID:-local}-alpha"
dotnet pack /p:Version="0.1.${CI_PIPELINE_IID:-local}-alpha" /p:IncludeSymbols=true /p:SymbolPackageFormat=snupkg
dotnet nuget push "bin/Debug/*.nupkg" --source gitlab
dotnet nuget push "bin/Debug/*.snupkg" --source gitlab

cd ../

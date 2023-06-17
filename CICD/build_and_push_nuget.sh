#!/bin/sh

cd $1

dotnet nuget add source "${CI_API_V4_URL}/projects/42768959/packages/nuget/index.json" --name gitlab --username gitlab-ci-token --password $CI_JOB_TOKEN --store-password-in-clear-text

dotnet build /p:Version="0.1.0-alpha.0${CI_JOB_ID}"
dotnet pack /p:Version="0.1.0-alpha.0${CI_JOB_ID}"
dotnet nuget push "bin/Debug/*.nupkg" --source gitlab

cd ../
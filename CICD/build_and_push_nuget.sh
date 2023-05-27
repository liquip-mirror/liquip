#!/bin/sh

cd $1

dotnet pack /p:Version="0.1.0.${CI_JOB_ID}"
dotnet nuget push "bin/Debug/*.nupkg"

cd ../
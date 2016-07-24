IF "%1" == "%2" (

dotnet pack --no-build --configuration %1 -o ../%3

xcopy %4 ".../%3/lib/%5/" /Y

)
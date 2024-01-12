CALL cd PackStudio/
CALL dotnet publish -c Release --sc false -r osx-arm64 -o ../Build/MacOS/PackStudio/
CALL dotnet publish -c Release --sc false -r linux-x64 -o ../Build/Linux/PackStudio/
CALL dotnet publish -c Release --sc false -r win-x64 -o ../Build/Windows/PackStudio/
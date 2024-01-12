CALL cd PackStudio/
CALL dotnet publish -c Release --sc true -r osx-arm64 -o ../Build/MacOS/PackStudio/
CALL dotnet publish -c Release --sc true -r linux-x64 -o ../Build/Linux/PackStudio/
CALL dotnet publish -c Release --sc true -r win-x64 -o ../Build/Linux/PackStudio/
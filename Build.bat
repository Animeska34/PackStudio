@ECHO OFF

ECHO Preparing

set /p version=Please enter a desired version string: 

CALL cd PackStudio/

ECHO Running Windows Build

CALL dotnet publish -c Release --sc true -r win-x64 -o ../Build/Windows/PackStudio/
CALL xcopy /s /y "../.BuildResources/Windows" "../Build/Windows"
CALL powershell -Command "&{Compress-Archive -Path ../Build/Windows/PackStudio -DestinationPath ../Build/PackStudio_win-x64@%version%.zip}" 

ECHO Running Linux Build

CALL dotnet publish -c Release --sc true -r linux-x64 -o ../Build/Linux/PackStudio/
CALL xcopy /s /y "../.BuildResources/Linux" "../Build/Linux"
CALL powershell -Command "&{Compress-Archive -Path ../Build/Linux/PackStudio -DestinationPath ../Build/PackStudio_linux-x64@%version%.zip}"

ECHO Running MacOS Build

CALL dotnet publish -c Release --sc true -r osx-arm64 -o ../Build/MacOS/PackStudio.app/Contents/MacOS/
CALL xcopy /s /y "../.BuildResources/MacOS" "../Build/MacOS"
CALL powershell -Command "&{Compress-Archive -Path ../Build/MacOS/PackStudio.app -DestinationPath ../Build/PackStudio_osx-arm64@%version%.zip}"

ECHO Done
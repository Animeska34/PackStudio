CALL cd PackStudio/
CALL dotnet build -c Release --sc false -o ../Build/Universal/PackStudio/
CALL xcopy /s /y "../.UNI" "../Build/Universal/PackStudio"
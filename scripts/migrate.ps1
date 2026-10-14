$ErrorActionPreference = "Stop"

$projectPath = Join-Path $PSScriptRoot "..\AspNetMvcLogin.csproj"
$projectDirectory = Split-Path -Parent $projectPath

Push-Location $projectDirectory
try {
    dotnet tool run dotnet-ef database update --project $projectPath
}
finally {
    Pop-Location
}

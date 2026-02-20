param(
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Debug"
)

$ErrorActionPreference = "Stop"
$projectPath = Join-Path $PSScriptRoot "Tests.csproj"

function Get-MSBuildPath {
    $pf = $env:ProgramFiles
    $pf86 = ${env:ProgramFiles(x86)}

    $candidates = @(
        "$pf\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe",
        "$pf\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe",
        "$pf\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe",
        "$pf\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\MSBuild.exe",
        "$pf86\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe",
        "$pf86\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe",
        "$pf86\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe",
        "$pf86\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\MSBuild.exe"
    )

    foreach ($candidate in $candidates) {
        if (Test-Path $candidate) {
            return $candidate
        }
    }

    $vsWhere = "$env:ProgramFiles(x86)\Microsoft Visual Studio\Installer\vswhere.exe"
    if (Test-Path $vsWhere) {
        $installPath = & $vsWhere -latest -requires Microsoft.Component.MSBuild -property installationPath
        if ($LASTEXITCODE -eq 0 -and -not [string]::IsNullOrWhiteSpace($installPath)) {
            $msbuild = Join-Path $installPath "MSBuild\Current\Bin\MSBuild.exe"
            if (Test-Path $msbuild) {
                return $msbuild
            }
        }
    }

    throw "MSBuild.exe not found. Install Visual Studio 2022 (or Build Tools) with MSBuild."
}

$msbuildPath = Get-MSBuildPath
Write-Host "Using MSBuild: $msbuildPath"

& $msbuildPath $projectPath /t:Restore,Build /p:Configuration=$Configuration
if ($LASTEXITCODE -ne 0) {
    throw "Restore/Build failed with exit code $LASTEXITCODE."
}

Write-Host "Running tests with dotnet test --no-build..."
dotnet test $projectPath --no-build --configuration $Configuration
exit $LASTEXITCODE

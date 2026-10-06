# Use this script to build all the projects in the solution
Write-Host ""
Write-Host "Starting build process..."
Write-Host ""

if (-not (Get-Command "dotnet" -ErrorAction SilentlyContinue)) {
    Write-Host "Error: dotnet SDK is not installed or not in PATH." -ForegroundColor Red
    Write-Host ""
    exit 1
}

$dotnetVersionStr = dotnet --version
$majorVersion = [int]($dotnetVersionStr.Split('.')[0])

if ($majorVersion -lt 10) {
    Write-Host "Error: .NET SDK 10 or higher is required. Found: $dotnetVersionStr" -ForegroundColor Red
    Write-Host ""
    exit 1
}

$outDir = "$($PSScriptRoot)\Release"
New-Item -ItemType Directory -Force -Path $outDir | Out-Null
Remove-Item $outDir\* -Recurse -Force

Set-Location -Path "$($PSScriptRoot)\src"

# Build the core project
Write-Host "================================"
Write-Host "Building Core project..."
Write-Host "================================"
Write-Host ""
dotnet clean "SleepyPawn.Core\SleepyPawn.Core.csproj"
dotnet publish "SleepyPawn.Core\SleepyPawn.Core.csproj" -c Release -o $outDir -p:DebugType=None -p:GenerateDependencyFile=false

# Build the CLI project
Write-Host ""
Write-Host "================================"
Write-Host "Starting build process for CLI project..."
Write-Host "================================"
Write-Host ""

Write-Host ""
Write-Host "--------------------------------"
Write-Host "Building CLI for Windows x64..."
Write-Host "--------------------------------"
Write-Host ""
dotnet clean "SleepyPawn.Cli\SleepyPawn.Cli.csproj"
dotnet publish "SleepyPawn.Cli\SleepyPawn.Cli.csproj" -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:DebugType=None -o $outDir
Rename-Item -Path "$outDir\SleepyPawn.Cli.exe" -NewName "sleepy-pawn-cli-win-x64.exe" -Force

Write-Host ""
Write-Host "--------------------------------"
Write-Host "Building CLI for Linux x64..."
Write-Host "--------------------------------"
Write-Host ""
dotnet clean "SleepyPawn.Cli\SleepyPawn.Cli.csproj"
dotnet publish "SleepyPawn.Cli\SleepyPawn.Cli.csproj" -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -p:DebugType=None -o $outDir
Rename-Item -Path "$outDir\SleepyPawn.Cli" -NewName "sleepy-pawn-cli-linux-x64" -Force

Write-Host ""
Write-Host "--------------------------------"
Write-Host "Building CLI for macOS x64..."
Write-Host "--------------------------------"
Write-Host ""
dotnet clean "SleepyPawn.Cli\SleepyPawn.Cli.csproj"
dotnet publish "SleepyPawn.Cli\SleepyPawn.Cli.csproj" -c Release -r osx-x64 --self-contained true -p:PublishSingleFile=true -p:DebugType=None -o $outDir
Rename-Item -Path "$outDir\SleepyPawn.Cli" -NewName "sleepy-pawn-cli-osx-x64" -Force

# Build the UCI project
Write-Host ""
Write-Host "================================"
Write-Host "Starting build process for UCI project..."
Write-Host "================================"
Write-Host ""

Write-Host ""
Write-Host "--------------------------------"
Write-Host "Building UCI for Windows x64..."
Write-Host "--------------------------------"
Write-Host ""
dotnet clean "SleepyPawn.Uci\SleepyPawn.Uci.csproj"
dotnet publish "SleepyPawn.Uci\SleepyPawn.Uci.csproj" -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:DebugType=None -o $outDir
Rename-Item -Path "$outDir\SleepyPawn.Uci.exe" -NewName "sleepy-pawn-uci-win-x64.exe" -Force

Write-Host ""
Write-Host "--------------------------------"
Write-Host "Building UCI for Linux x64..."
Write-Host "--------------------------------"
Write-Host ""
dotnet clean "SleepyPawn.Uci\SleepyPawn.Uci.csproj"
dotnet publish "SleepyPawn.Uci\SleepyPawn.Uci.csproj" -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -p:DebugType=None -o $outDir
Rename-Item -Path "$outDir\SleepyPawn.Uci" -NewName "sleepy-pawn-uci-linux-x64" -Force

Write-Host ""
Write-Host "--------------------------------"
Write-Host "Building UCI for macOS x64..."
Write-Host "--------------------------------"
Write-Host ""
dotnet clean "SleepyPawn.Uci\SleepyPawn.Uci.csproj"
dotnet publish "SleepyPawn.Uci\SleepyPawn.Uci.csproj" -c Release -r osx-x64 --self-contained true -p:PublishSingleFile=true -p:DebugType=None -o $outDir
Rename-Item -Path "$outDir\SleepyPawn.Uci" -NewName "sleepy-pawn-uci-osx-x64" -Force

# Build the WASM project
Write-Host ""
Write-Host "================================"
Write-Host "Starting build process for WASM project..."
Write-Host "================================"
Write-Host ""
dotnet clean "SleepyPawn.Wasm\SleepyPawn.Wasm.csproj"
dotnet publish "SleepyPawn.Wasm\SleepyPawn.Wasm.csproj" -c Release -p:DebugType=None -o $outDir
Remove-Item $outDir\web.config -Force
Remove-Item $outDir\dotnet.js -Force
Remove-Item $outDir\SleepyPawn.Wasm.runtimeconfig.json -Force
Remove-Item $outDir\SleepyPawn.Wasm.staticwebassets.endpoints.json -Force
Remove-Item $outDir\wwwroot\*.br -Recurse -Force
Remove-Item $outDir\wwwroot\*.gz -Recurse -Force
Remove-Item $outDir\wwwroot\_Framework\*.br -Recurse -Force
Remove-Item $outDir\wwwroot\_Framework\*.gz -Recurse -Force

Write-Host ""
Write-Host "--------------------------------"
Write-Host "Compressing WebAssembly build..."
Write-Host "--------------------------------"
Write-Host ""
$ProgressPreference = 'SilentlyContinue'
Compress-Archive -Path "$outDir\wwwroot\*" -DestinationPath "$outDir\sleepy-pawn-webassembly.zip" -Force
$ProgressPreference = 'Continue'
Remove-Item -Path "$outDir\wwwroot" -Recurse -Force

Set-Location -Path "$($PSScriptRoot)"

Write-Host "Script finished. All builds should be located in the 'Release' folder."
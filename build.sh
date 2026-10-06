# Use this script to build all the projects in the solution
echo ""
echo "Starting build process..."
echo ""

if ! command -v dotnet &> /dev/null; then
    echo "Error: dotnet SDK is not installed or not in PATH."
    echo "Install it using: sudo apt-get install -y dotnet-sdk-10.0"
    echo ""
    exit 1
fi

DOTNET_VERSION=$(dotnet --version | cut -d'.' -f1)
if [ -z "$DOTNET_VERSION" ] || [ "$DOTNET_VERSION" -lt 10 ]; then
    echo "Error: .NET SDK 10 or higher is required. Found: $(dotnet --version)"
    echo ""
    exit 1
fi

if ! command -v zip &> /dev/null; then
    echo "Error: 'zip' utility is not installed."
    echo "Install it using: sudo apt-get install zip"
    echo ""
    exit 1
fi

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
OUT_DIR="$SCRIPT_DIR/Release"
mkdir -p "$OUT_DIR"
rm -rf "$OUT_DIR"/*

cd "$SCRIPT_DIR/src" || exit

# Build the core project
echo "================================"
echo "Building Core project..."
echo "================================"
echo ""
dotnet clean "SleepyPawn.Core/SleepyPawn.Core.csproj"
dotnet publish "SleepyPawn.Core/SleepyPawn.Core.csproj" -c Release -o $OUT_DIR -p:DebugType=None -p:GenerateDependencyFile=false

# Build the CLI project
echo ""
echo "================================"
echo "Starting build process for CLI project..."
echo "================================"
echo ""

echo ""
echo "--------------------------------"
echo "Building CLI for Windows x64..."
echo "--------------------------------"
echo ""
dotnet clean "SleepyPawn.Cli/SleepyPawn.Cli.csproj"
dotnet publish "SleepyPawn.Cli/SleepyPawn.Cli.csproj" -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:DebugType=None -o $OUT_DIR
mv "$OUT_DIR/SleepyPawn.Cli.exe" "$OUT_DIR/sleepy-pawn-cli-win-x64.exe"

echo ""
echo "--------------------------------"
echo "Building CLI for Linux x64..."
echo "--------------------------------"
echo ""
dotnet clean "SleepyPawn.Cli/SleepyPawn.Cli.csproj"
dotnet publish "SleepyPawn.Cli/SleepyPawn.Cli.csproj" -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -p:DebugType=None -o $OUT_DIR
mv "$OUT_DIR/SleepyPawn.Cli" "$OUT_DIR/sleepy-pawn-cli-linux-x64"

echo ""
echo "--------------------------------"
echo "Building CLI for macOS x64..."
echo "--------------------------------"
echo ""
dotnet clean "SleepyPawn.Cli/SleepyPawn.Cli.csproj"
dotnet publish "SleepyPawn.Cli/SleepyPawn.Cli.csproj" -c Release -r osx-x64 --self-contained true -p:PublishSingleFile=true -p:DebugType=None -o $OUT_DIR
mv "$OUT_DIR/SleepyPawn.Cli" "$OUT_DIR/sleepy-pawn-cli-osx-x64"

# Build the UCI project
echo ""
echo "================================"
echo "Starting build process for UCI project..."
echo "================================"
echo ""

echo ""
echo "--------------------------------"
echo "Building UCI for Windows x64..."
echo "--------------------------------"
echo ""
dotnet clean "SleepyPawn.Uci/SleepyPawn.Uci.csproj"
dotnet publish "SleepyPawn.Uci/SleepyPawn.Uci.csproj" -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:DebugType=None -o $OUT_DIR
mv "$OUT_DIR/SleepyPawn.Uci.exe" "$OUT_DIR/sleepy-pawn-uci-win-x64.exe"

echo ""
echo "--------------------------------"
echo "Building UCI for Linux x64..."
echo "--------------------------------"
echo ""
dotnet clean "SleepyPawn.Uci/SleepyPawn.Uci.csproj"
dotnet publish "SleepyPawn.Uci/SleepyPawn.Uci.csproj" -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -p:DebugType=None -o $OUT_DIR
mv "$OUT_DIR/SleepyPawn.Uci" "$OUT_DIR/sleepy-pawn-uci-linux-x64"

echo ""
echo "--------------------------------"
echo "Building UCI for macOS x64..."
echo "--------------------------------"
echo ""
dotnet clean "SleepyPawn.Uci/SleepyPawn.Uci.csproj"
dotnet publish "SleepyPawn.Uci/SleepyPawn.Uci.csproj" -c Release -r osx-x64 --self-contained true -p:PublishSingleFile=true -p:DebugType=None -o $OUT_DIR
mv "$OUT_DIR/SleepyPawn.Uci" "$OUT_DIR/sleepy-pawn-uci-osx-x64"

# Build the WASM project
echo ""
echo "================================"
echo "Starting build process for WASM project..."
echo "================================"
echo ""

dotnet clean "SleepyPawn.Wasm/SleepyPawn.Wasm.csproj"
dotnet publish "SleepyPawn.Wasm/SleepyPawn.Wasm.csproj" -c Release -p:DebugType=None -o $OUT_DIR
rm -f $OUT_DIR/web.config
rm -f $OUT_DIR/dotnet.js
rm -f $OUT_DIR/SleepyPawn.Wasm.runtimeconfig.json
rm -f $OUT_DIR/SleepyPawn.Wasm.staticwebassets.endpoints.json
rm -f $OUT_DIR/wwwroot/*.br
rm -f $OUT_DIR/wwwroot/*.gz
rm -f $OUT_DIR/wwwroot/_Framework/*.br
rm -f $OUT_DIR/wwwroot/_Framework/*.gz

echo ""
echo "--------------------------------"
echo "Compressing WebAssembly build..."
echo "--------------------------------"
echo ""

cd "$OUT_DIR/wwwroot" || exit
zip -r -q "../sleepy-pawn-webassembly.zip"
rm -rf "$OUT_DIR/wwwroot"

cd "$SCRIPT_DIR" || exit

echo "Script finished. All builds should be located in the 'Release' folder."
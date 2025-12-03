# ClickDesk Siticone Build Verification Script
# This script verifies the Siticone framework integration and builds the project
# Run this on Windows with PowerShell

Write-Host "====================================" -ForegroundColor Cyan
Write-Host "ClickDesk Build Verification Script" -ForegroundColor Cyan
Write-Host "====================================" -ForegroundColor Cyan
Write-Host ""

# Function to check if a command exists
function Test-Command {
    param($Command)
    try {
        if (Get-Command $Command -ErrorAction Stop) {
            return $true
        }
    }
    catch {
        return $false
    }
}

# Check Prerequisites
Write-Host "Step 1: Checking Prerequisites..." -ForegroundColor Yellow
Write-Host ""

# Check for MSBuild
if (Test-Command "msbuild") {
    Write-Host "[OK] MSBuild is available" -ForegroundColor Green
    $msbuildPath = (Get-Command msbuild).Source
    Write-Host "    Path: $msbuildPath" -ForegroundColor Gray
} else {
    Write-Host "[ERROR] MSBuild not found!" -ForegroundColor Red
    Write-Host "Please run this from a Visual Studio Developer Command Prompt or install Visual Studio" -ForegroundColor Yellow
    exit 1
}

# Check for NuGet
if (Test-Command "nuget") {
    Write-Host "[OK] NuGet is available" -ForegroundColor Green
} else {
    Write-Host "[WARNING] NuGet CLI not found (optional)" -ForegroundColor Yellow
    Write-Host "    Package restore will use MSBuild instead" -ForegroundColor Gray
}

Write-Host ""

# Check Project Files
Write-Host "Step 2: Verifying Project Files..." -ForegroundColor Yellow
Write-Host ""

if (Test-Path "ClickDesk.sln") {
    Write-Host "[OK] Solution file found: ClickDesk.sln" -ForegroundColor Green
} else {
    Write-Host "[ERROR] Solution file not found!" -ForegroundColor Red
    exit 1
}

if (Test-Path "ClickDesk.csproj") {
    Write-Host "[OK] Project file found: ClickDesk.csproj" -ForegroundColor Green
} else {
    Write-Host "[ERROR] Project file not found!" -ForegroundColor Red
    exit 1
}

if (Test-Path "packages.config") {
    Write-Host "[OK] Package configuration found: packages.config" -ForegroundColor Green
    
    # Check Siticone package in packages.config
    $packagesContent = Get-Content "packages.config"
    if ($packagesContent -match 'Siticone.Desktop.UI') {
        Write-Host "[OK] Siticone.Desktop.UI package referenced in packages.config" -ForegroundColor Green
    } else {
        Write-Host "[ERROR] Siticone.Desktop.UI not found in packages.config!" -ForegroundColor Red
        exit 1
    }
} else {
    Write-Host "[ERROR] packages.config not found!" -ForegroundColor Red
    exit 1
}

Write-Host ""

# Clean Previous Build
Write-Host "Step 3: Cleaning Previous Build..." -ForegroundColor Yellow
Write-Host ""

if (Test-Path "bin") {
    Remove-Item -Path "bin" -Recurse -Force
    Write-Host "[OK] Cleaned bin directory" -ForegroundColor Green
}

if (Test-Path "obj") {
    Remove-Item -Path "obj" -Recurse -Force
    Write-Host "[OK] Cleaned obj directory" -ForegroundColor Green
}

Write-Host ""

# Restore NuGet Packages
Write-Host "Step 4: Restoring NuGet Packages..." -ForegroundColor Yellow
Write-Host ""

& msbuild ClickDesk.sln /t:Restore /v:minimal
if ($LASTEXITCODE -ne 0) {
    Write-Host "[ERROR] Package restore failed!" -ForegroundColor Red
    exit 1
}

Write-Host ""

# Verify Packages
Write-Host "Step 5: Verifying Package Installation..." -ForegroundColor Yellow
Write-Host ""

if (Test-Path "packages/Siticone.Desktop.UI.2.1.1") {
    Write-Host "[OK] Siticone.Desktop.UI package folder exists" -ForegroundColor Green
    
    $dllPath = "packages/Siticone.Desktop.UI.2.1.1/lib/net40/Siticone.Desktop.UI.dll"
    if (Test-Path $dllPath) {
        Write-Host "[OK] Siticone.Desktop.UI.dll found" -ForegroundColor Green
        $dllSize = (Get-Item $dllPath).Length / 1MB
        Write-Host "    Size: $([math]::Round($dllSize, 2)) MB" -ForegroundColor Gray
    } else {
        Write-Host "[ERROR] Siticone.Desktop.UI.dll not found!" -ForegroundColor Red
        exit 1
    }
} else {
    Write-Host "[ERROR] Siticone.Desktop.UI package not installed!" -ForegroundColor Red
    Write-Host "Run: nuget restore ClickDesk.sln" -ForegroundColor Yellow
    exit 1
}

if (Test-Path "packages/Newtonsoft.Json.13.0.3") {
    Write-Host "[OK] Newtonsoft.Json package folder exists" -ForegroundColor Green
} else {
    Write-Host "[ERROR] Newtonsoft.Json package not installed!" -ForegroundColor Red
    exit 1
}

Write-Host ""

# Build Solution
Write-Host "Step 6: Building Solution..." -ForegroundColor Yellow
Write-Host ""

& msbuild ClickDesk.sln /p:Configuration=Debug /v:minimal /clp:ErrorsOnly
$buildResult = $LASTEXITCODE

Write-Host ""

if ($buildResult -eq 0) {
    Write-Host "====================================" -ForegroundColor Green
    Write-Host "BUILD SUCCESSFUL!" -ForegroundColor Green
    Write-Host "====================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "The ClickDesk application has been built successfully." -ForegroundColor Green
    Write-Host "All Siticone framework references are correctly configured." -ForegroundColor Green
    Write-Host ""
    
    if (Test-Path "bin/Debug/ClickDesk.exe") {
        Write-Host "Executable location: bin/Debug/ClickDesk.exe" -ForegroundColor Cyan
        Write-Host "You can now run the application!" -ForegroundColor Cyan
    }
    
    Write-Host ""
    Write-Host "Next Steps:" -ForegroundColor Yellow
    Write-Host "1. Run the application: .\bin\Debug\ClickDesk.exe" -ForegroundColor White
    Write-Host "2. Test the Siticone UI components" -ForegroundColor White
    Write-Host "3. Verify both light and dark themes work" -ForegroundColor White
    Write-Host ""
    
} else {
    Write-Host "====================================" -ForegroundColor Red
    Write-Host "BUILD FAILED!" -ForegroundColor Red
    Write-Host "====================================" -ForegroundColor Red
    Write-Host ""
    Write-Host "Please review the error messages above." -ForegroundColor Red
    Write-Host ""
    Write-Host "Common Issues:" -ForegroundColor Yellow
    Write-Host "1. Missing Siticone reference - Run: nuget restore" -ForegroundColor White
    Write-Host "2. Wrong .NET Framework version - Check TargetFrameworkVersion in .csproj" -ForegroundColor White
    Write-Host "3. Missing using directive - Add: using Siticone.Desktop.UI.WinForms;" -ForegroundColor White
    Write-Host ""
    exit 1
}

Write-Host ""
Write-Host "Verification complete!" -ForegroundColor Cyan
Write-Host ""

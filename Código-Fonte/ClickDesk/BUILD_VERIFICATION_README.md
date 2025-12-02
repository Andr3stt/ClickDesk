# Build Verification Guide

## Overview

This directory contains scripts to verify the Siticone framework integration and build the ClickDesk application.

## Verification Scripts

### For Windows: `verify-build.ps1`

A comprehensive PowerShell script that:
- Checks all prerequisites (MSBuild, NuGet)
- Verifies project files and configuration
- Restores NuGet packages
- Cleans previous builds
- Builds the solution
- Reports detailed status

**Usage:**
```powershell
# From Visual Studio Developer PowerShell
cd Código-Fonte\ClickDesk
.\verify-build.ps1
```

**Requirements:**
- Windows OS
- Visual Studio 2019+ or MSBuild
- PowerShell 5.0+
- .NET Framework 4.8 SDK

**What it does:**
1. ✅ Checks for MSBuild and NuGet
2. ✅ Verifies ClickDesk.sln and ClickDesk.csproj exist
3. ✅ Validates packages.config has Siticone reference
4. ✅ Cleans bin/ and obj/ directories
5. ✅ Restores NuGet packages
6. ✅ Verifies Siticone.Desktop.UI.dll exists
7. ✅ Builds the solution
8. ✅ Reports success/failure with helpful messages

**Output:**
- Green messages: Success
- Yellow messages: Warnings or informational
- Red messages: Errors that need attention

### For Linux/Mac: `verify-config.sh`

A bash script that verifies configuration without building:
- Checks project files exist
- Validates package references
- Verifies using directives
- Checks if packages are installed
- Validates documentation

**Usage:**
```bash
# From terminal
cd Código-Fonte/ClickDesk
bash verify-config.sh
```

**Requirements:**
- Bash shell
- Basic Unix utilities (grep, find, sed)

**What it does:**
1. ✅ Verifies solution and project files
2. ✅ Checks packages.config for Siticone
3. ✅ Validates .csproj configuration
4. ✅ Checks target framework (.NET 4.8)
5. ✅ Finds files using Siticone
6. ✅ Verifies using directives
7. ✅ Checks if packages are installed
8. ✅ Validates documentation exists

**Note:** This script cannot build the project on Linux/Mac since Windows Forms and Siticone are Windows-specific. It only validates the configuration.

## Quick Start

### Windows Developers

1. **Clone the repository**
2. **Open Visual Studio Developer PowerShell**
3. **Navigate to project:**
   ```powershell
   cd path\to\ClickDesk\Código-Fonte\ClickDesk
   ```
4. **Run verification script:**
   ```powershell
   .\verify-build.ps1
   ```
5. **If successful, run the application:**
   ```powershell
   .\bin\Debug\ClickDesk.exe
   ```

### Linux/Mac Developers (Configuration Check Only)

1. **Clone the repository**
2. **Navigate to project:**
   ```bash
   cd path/to/ClickDesk/Código-Fonte/ClickDesk
   ```
3. **Run configuration check:**
   ```bash
   bash verify-config.sh
   ```
4. **Review the output** to ensure configuration is valid

## Manual Verification

If you prefer to verify manually:

### 1. Check Package Configuration

**packages.config should contain:**
```xml
<package id="Siticone.Desktop.UI" version="2.1.1" targetFramework="net48" />
```

### 2. Check Project Reference

**ClickDesk.csproj should contain:**
```xml
<Reference Include="Siticone.Desktop.UI, Version=2.1.1.0, ...">
  <HintPath>packages\Siticone.Desktop.UI.2.1.1\lib\net40\Siticone.Desktop.UI.dll</HintPath>
  <Private>True</Private>
</Reference>
```

### 3. Check Using Directives

Files using Siticone controls should have:
```csharp
using Siticone.Desktop.UI.WinForms;
```

**Files to check:**
- Forms/Auth/FormLogin.cs
- Forms/Auth/FormRegistro.cs
- Forms/Dashboard/FormDashboard.cs
- Utils/UIHelper.cs

### 4. Verify Package Installation

Check that these directories exist:
```
packages/
├── Siticone.Desktop.UI.2.1.1/
│   └── lib/
│       └── net40/
│           └── Siticone.Desktop.UI.dll (should be ~4MB)
└── Newtonsoft.Json.13.0.3/
```

### 5. Build the Solution

Using Visual Studio:
- Open ClickDesk.sln
- Press Ctrl+Shift+B (Build Solution)
- Check Output window for errors

Using MSBuild:
```powershell
msbuild ClickDesk.sln /p:Configuration=Debug
```

## Expected Results

### ✅ Successful Build

When everything is configured correctly:
- No build errors
- Siticone.Desktop.UI.dll copied to bin/Debug/
- ClickDesk.exe created successfully
- Application runs with modern Siticone UI

### ❌ Common Issues

#### Issue: "The type or namespace name 'Siticone' could not be found"

**Causes:**
- Package not restored
- Missing using directive
- Incorrect reference path

**Solutions:**
1. Restore packages: `nuget restore ClickDesk.sln`
2. Add using directive: `using Siticone.Desktop.UI.WinForms;`
3. Clean and rebuild solution

#### Issue: "Could not resolve reference 'Siticone.Desktop.UI'"

**Causes:**
- Package not installed
- Incorrect HintPath
- Missing DLL file

**Solutions:**
1. Delete packages/ folder
2. Restore packages again
3. Verify DLL exists at HintPath location

#### Issue: Build fails on Linux/Mac

**Cause:**
- Windows Forms and Siticone are Windows-specific

**Solution:**
- This project must be built on Windows
- Use verify-config.sh to check configuration only

## Continuous Integration

For CI/CD pipelines on Windows:

```yaml
# Example for GitHub Actions
steps:
  - name: Setup MSBuild
    uses: microsoft/setup-msbuild@v1
  
  - name: Restore NuGet packages
    run: nuget restore Código-Fonte/ClickDesk/ClickDesk.sln
  
  - name: Build solution
    run: msbuild Código-Fonte/ClickDesk/ClickDesk.sln /p:Configuration=Release
  
  - name: Run application tests
    run: # Add your test commands here
```

## Documentation

Additional documentation available:
- **SITICONE_SETUP_GUIDE.md** - Complete setup instructions
- **SITICONE_INTEGRATION_COMPLETE.md** - Integration completion report
- **SITICONE_VERIFICATION_REPORT.md** - Detailed verification report
- **MODERNIZATION_GUIDE.md** - UI modernization guide
- **QUICK_MODERNIZATION_TEMPLATE.md** - Quick reference template

## Support

If verification scripts report errors:

1. **Check Prerequisites**
   - Ensure Visual Studio or MSBuild is installed
   - Verify .NET Framework 4.8 SDK is installed
   - Confirm you're on Windows (for building)

2. **Review Error Messages**
   - Scripts provide detailed error messages
   - Follow suggested solutions
   - Check common issues section

3. **Manual Verification**
   - Follow the manual verification steps
   - Check each component individually
   - Ensure all files are in correct locations

4. **Clean Build**
   - Delete bin/, obj/, and packages/ folders
   - Restore packages
   - Build solution from scratch

## Version Information

- **Siticone.Desktop.UI**: v2.1.1
- **Newtonsoft.Json**: v13.0.3
- **Target Framework**: .NET Framework 4.8
- **Build Tools**: MSBuild 15.0+ / Visual Studio 2019+

## Last Updated

December 2, 2025

---

**Note**: These scripts are designed to help developers verify the Siticone framework integration is correctly configured. They provide detailed feedback and help identify configuration issues early in the development process.

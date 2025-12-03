#!/bin/bash
# ClickDesk Siticone Configuration Verification Script
# This script verifies the Siticone framework configuration
# NOTE: This cannot build on Linux, but can verify configuration

echo "========================================"
echo "ClickDesk Configuration Verification"
echo "========================================"
echo ""

# Color codes
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
CYAN='\033[0;36m'
NC='\033[0m' # No Color

# Check if we're in the right directory
if [ ! -f "ClickDesk.sln" ]; then
    echo -e "${RED}[ERROR] ClickDesk.sln not found!${NC}"
    echo "Please run this script from the Código-Fonte/ClickDesk directory"
    exit 1
fi

echo -e "${YELLOW}Step 1: Checking Project Files...${NC}"
echo ""

# Check solution file
if [ -f "ClickDesk.sln" ]; then
    echo -e "${GREEN}[OK] Solution file found: ClickDesk.sln${NC}"
else
    echo -e "${RED}[ERROR] Solution file not found!${NC}"
    exit 1
fi

# Check project file
if [ -f "ClickDesk.csproj" ]; then
    echo -e "${GREEN}[OK] Project file found: ClickDesk.csproj${NC}"
else
    echo -e "${RED}[ERROR] Project file not found!${NC}"
    exit 1
fi

# Check packages.config
if [ -f "packages.config" ]; then
    echo -e "${GREEN}[OK] Package configuration found: packages.config${NC}"
    
    # Check for Siticone package
    if grep -q "Siticone.Desktop.UI" packages.config; then
        echo -e "${GREEN}[OK] Siticone.Desktop.UI referenced in packages.config${NC}"
        VERSION=$(grep "Siticone.Desktop.UI" packages.config | sed -n 's/.*version="\([^"]*\)".*/\1/p')
        echo "    Version: $VERSION"
    else
        echo -e "${RED}[ERROR] Siticone.Desktop.UI not found in packages.config!${NC}"
        exit 1
    fi
else
    echo -e "${RED}[ERROR] packages.config not found!${NC}"
    exit 1
fi

echo ""
echo -e "${YELLOW}Step 2: Checking Project Configuration...${NC}"
echo ""

# Check .csproj for Siticone reference
if grep -q "Siticone.Desktop.UI" ClickDesk.csproj; then
    echo -e "${GREEN}[OK] Siticone.Desktop.UI referenced in .csproj${NC}"
    
    # Extract HintPath
    HINTPATH=$(grep -A 1 "Siticone.Desktop.UI" ClickDesk.csproj | grep "HintPath" | sed 's/.*<HintPath>\(.*\)<\/HintPath>.*/\1/')
    echo "    HintPath: $HINTPATH"
    
    # Check if Private is set to True
    if grep -A 2 "Siticone.Desktop.UI" ClickDesk.csproj | grep -q "<Private>True</Private>"; then
        echo -e "${GREEN}[OK] Private=True (DLL will be copied to output)${NC}"
    else
        echo -e "${YELLOW}[WARNING] Private not set to True${NC}"
    fi
else
    echo -e "${RED}[ERROR] Siticone.Desktop.UI reference not found in .csproj!${NC}"
    exit 1
fi

# Check target framework
if grep -q "TargetFrameworkVersion.*v4.8" ClickDesk.csproj; then
    echo -e "${GREEN}[OK] Target Framework: .NET Framework 4.8${NC}"
else
    echo -e "${YELLOW}[WARNING] Target Framework may not be .NET Framework 4.8${NC}"
fi

echo ""
echo -e "${YELLOW}Step 3: Checking Source Files...${NC}"
echo ""

# Find files that use Siticone
SITICONE_FILES=$(find Forms Utils -name "*.cs" -type f -exec grep -l "using Siticone" {} \; 2>/dev/null)

if [ -z "$SITICONE_FILES" ]; then
    echo -e "${YELLOW}[WARNING] No files with 'using Siticone' directive found${NC}"
else
    FILE_COUNT=$(echo "$SITICONE_FILES" | wc -l)
    echo -e "${GREEN}[OK] Found $FILE_COUNT files using Siticone${NC}"
    echo ""
    echo "Files with Siticone using directive:"
    echo "$SITICONE_FILES" | while read -r file; do
        echo "    - $file"
    done
fi

echo ""
echo -e "${YELLOW}Step 4: Verifying Using Directives...${NC}"
echo ""

# Check that files using Siticone types have the using directive
MISSING_USING=0

for file in Forms/Auth/FormLogin.cs Forms/Auth/FormRegistro.cs Forms/Dashboard/FormDashboard.cs Utils/UIHelper.cs; do
    if [ -f "$file" ]; then
        if grep -q "SiticoneButton\|SiticonePanel\|SiticoneTextBox" "$file"; then
            if grep -q "using Siticone.Desktop.UI.WinForms" "$file"; then
                echo -e "${GREEN}[OK] $file has correct using directive${NC}"
            else
                echo -e "${RED}[ERROR] $file uses Siticone but missing using directive!${NC}"
                MISSING_USING=1
            fi
        fi
    fi
done

if [ $MISSING_USING -eq 1 ]; then
    echo ""
    echo -e "${RED}Some files are missing the Siticone using directive!${NC}"
    echo "Add this line at the top of affected files:"
    echo "using Siticone.Desktop.UI.WinForms;"
    exit 1
fi

echo ""
echo -e "${YELLOW}Step 5: Checking NuGet Packages...${NC}"
echo ""

# Check if packages directory exists
if [ -d "packages" ]; then
    echo -e "${GREEN}[OK] packages/ directory exists${NC}"
    
    # Check Siticone package
    if [ -d "packages/Siticone.Desktop.UI.2.1.1" ]; then
        echo -e "${GREEN}[OK] Siticone.Desktop.UI.2.1.1 package installed${NC}"
        
        # Check for DLL
        DLL_PATH="packages/Siticone.Desktop.UI.2.1.1/lib/net40/Siticone.Desktop.UI.dll"
        if [ -f "$DLL_PATH" ]; then
            echo -e "${GREEN}[OK] Siticone.Desktop.UI.dll found${NC}"
            SIZE=$(du -h "$DLL_PATH" | cut -f1)
            echo "    Size: $SIZE"
        else
            echo -e "${RED}[ERROR] Siticone.Desktop.UI.dll not found!${NC}"
            exit 1
        fi
    else
        echo -e "${YELLOW}[WARNING] Siticone.Desktop.UI package not installed${NC}"
        echo "Run on Windows: nuget restore ClickDesk.sln"
    fi
    
    # Check Newtonsoft.Json package
    if [ -d "packages/Newtonsoft.Json.13.0.3" ]; then
        echo -e "${GREEN}[OK] Newtonsoft.Json.13.0.3 package installed${NC}"
    else
        echo -e "${YELLOW}[WARNING] Newtonsoft.Json package not installed${NC}"
    fi
else
    echo -e "${YELLOW}[WARNING] packages/ directory not found${NC}"
    echo "This is normal if packages haven't been restored yet."
    echo "Run on Windows: nuget restore ClickDesk.sln"
fi

echo ""
echo -e "${YELLOW}Step 6: Documentation Check...${NC}"
echo ""

# Check for documentation files
for doc in SITICONE_SETUP_GUIDE.md SITICONE_INTEGRATION_COMPLETE.md SITICONE_VERIFICATION_REPORT.md; do
    if [ -f "$doc" ]; then
        echo -e "${GREEN}[OK] $doc found${NC}"
    else
        echo -e "${YELLOW}[INFO] $doc not found${NC}"
    fi
done

echo ""
echo "========================================"
echo -e "${CYAN}Configuration Verification Complete!${NC}"
echo "========================================"
echo ""

echo -e "${GREEN}Configuration Status: VALID${NC}"
echo ""
echo "Summary:"
echo "  - Project files are correctly configured"
echo "  - Siticone package is referenced in packages.config and .csproj"
echo "  - Source files have correct using directives"
if [ -d "packages/Siticone.Desktop.UI.2.1.1" ]; then
    echo "  - NuGet packages are installed"
else
    echo "  - NuGet packages need to be restored (run on Windows)"
fi
echo ""
echo -e "${YELLOW}IMPORTANT:${NC}"
echo "This project requires Windows to build and run."
echo "The Siticone framework and Windows Forms are Windows-specific."
echo ""
echo "To build on Windows:"
echo "  1. Open ClickDesk.sln in Visual Studio"
echo "  2. Restore NuGet packages (automatic)"
echo "  3. Build solution (Ctrl+Shift+B)"
echo "  4. Run application (F5)"
echo ""
echo "Or use the PowerShell script: ./verify-build.ps1"
echo ""

# Siticone Framework Setup Guide

## 📋 Overview

This guide provides complete instructions for setting up and working with the Siticone.Desktop.UI framework in the ClickDesk Windows Forms application.

## ✅ Current Status

The Siticone framework is **already configured** in this project:

- ✅ Siticone.Desktop.UI v2.1.1 is referenced in `packages.config`
- ✅ Siticone.Desktop.UI is referenced in `ClickDesk.csproj`
- ✅ Package reference path is correctly configured
- ✅ Three forms have been modernized (FormLogin, FormRegistro, FormDashboard)
- ✅ UIHelper utility has Siticone helper methods
- ✅ ThemeManager supports light/dark themes

## 🔧 Prerequisites

### Required Software

1. **Visual Studio 2019 or later** (Community, Professional, or Enterprise)
   - Workload: .NET desktop development
   - Component: .NET Framework 4.8 SDK

2. **Windows Operating System**
   - Windows 7 or later
   - Siticone controls are Windows-specific and won't work on Linux/macOS

3. **NuGet Package Manager**
   - Included with Visual Studio
   - Alternatively: NuGet CLI

### System Requirements

- **OS**: Windows 7 or later
- **Resolution**: 1200x700 minimum (1920x1080 recommended)
- **RAM**: 4GB minimum, 8GB recommended
- **.NET Framework**: 4.8 or later

## 🚀 Setup Instructions

### Option 1: Using Visual Studio (Recommended)

1. **Open Solution**
   ```
   File → Open → Project/Solution
   Navigate to: Código-Fonte/ClickDesk/ClickDesk.sln
   ```

2. **Restore NuGet Packages**
   - Visual Studio should automatically restore packages on solution open
   - If not, right-click on solution → "Restore NuGet Packages"
   - Or: `Tools → NuGet Package Manager → Package Manager Console`
     ```powershell
     Update-Package -Reinstall
     ```

3. **Verify Package Installation**
   - Check that `packages/` folder contains:
     - `Siticone.Desktop.UI.2.1.1/`
     - `Newtonsoft.Json.13.0.3/`
   - In Solution Explorer, expand "References"
   - Verify "Siticone.Desktop.UI" is listed (no warning icon)

4. **Build Solution**
   ```
   Build → Build Solution (Ctrl+Shift+B)
   ```
   - Should build without errors
   - Warnings are acceptable

5. **Run Application**
   ```
   Debug → Start Debugging (F5)
   ```

### Option 2: Using Command Line

1. **Install NuGet CLI** (if not already installed)
   ```powershell
   # Download nuget.exe from https://www.nuget.org/downloads
   # Place it in your PATH or project directory
   ```

2. **Navigate to Project Directory**
   ```powershell
   cd Código-Fonte\ClickDesk
   ```

3. **Restore Packages**
   ```powershell
   nuget restore ClickDesk.sln
   ```

4. **Build with MSBuild**
   ```powershell
   # Use Developer Command Prompt for VS 2019/2022
   msbuild ClickDesk.sln /p:Configuration=Debug
   ```

5. **Run Application**
   ```powershell
   .\bin\Debug\ClickDesk.exe
   ```

## 📦 Package Configuration

### packages.config

Located at: `Código-Fonte/ClickDesk/packages.config`

```xml
<?xml version="1.0" encoding="utf-8"?>
<packages>
  <package id="Newtonsoft.Json" version="13.0.3" targetFramework="net48" />
  <package id="Siticone.Desktop.UI" version="2.1.1" targetFramework="net48" />
</packages>
```

### ClickDesk.csproj

The Siticone reference in the project file:

```xml
<Reference Include="Siticone.Desktop.UI, Version=2.1.1.0, Culture=neutral, PublicKeyToken=9e14b0e35321fa3d, processorArchitecture=MSIL">
  <HintPath>packages\Siticone.Desktop.UI.2.1.1\lib\net40\Siticone.Desktop.UI.dll</HintPath>
  <Private>True</Private>
</Reference>
```

## 🎨 Using Siticone Controls

### Required Using Directives

Add these to any form that uses Siticone controls:

```csharp
using Siticone.Desktop.UI.WinForms;
```

Note: `Siticone.Desktop.UI.Common` is NOT needed for basic usage. The Common namespace is included within WinForms namespace.

### Example: Basic Form with Siticone

```csharp
using System;
using System.Drawing;
using System.Windows.Forms;
using Siticone.Desktop.UI.WinForms;
using ClickDesk.Utils;

namespace ClickDesk.Forms.Example
{
    public partial class FormExample : Form
    {
        // Siticone control declarations
        private SiticonePanel mainPanel;
        private SiticoneButton btnAction;
        private SiticoneTextBox txtInput;

        public FormExample()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            // Create modern panel
            mainPanel = new SiticonePanel
            {
                Size = new Size(400, 300),
                Location = new Point(50, 50),
                BorderRadius = 15,
                FillColor = ThemeManager.CardBackground,
                ShadowDecoration = { Enabled = true }
            };

            // Create modern button
            btnAction = new SiticoneButton
            {
                Text = "Click Me",
                Size = new Size(150, 45),
                Location = new Point(125, 200),
                BorderRadius = 8,
                FillColor = ThemeManager.Brand,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            // Create modern textbox
            txtInput = new SiticoneTextBox
            {
                Size = new Size(300, 36),
                Location = new Point(50, 100),
                BorderRadius = 8,
                PlaceholderText = "Enter text here..."
            };

            // Add controls
            mainPanel.Controls.Add(btnAction);
            mainPanel.Controls.Add(txtInput);
            this.Controls.Add(mainPanel);
        }
    }
}
```

### Available Siticone Controls

The following Siticone controls are available in this project:

- **SiticonePanel** - Modern panel with shadow and rounded corners
- **SiticoneButton** - Styled button with hover effects
- **SiticoneTextBox** - Modern input field with placeholders
- **SiticoneComboBox** - Styled dropdown
- **SiticoneCheckBox** - Modern checkbox
- **SiticoneRadioButton** - Styled radio button
- **SiticoneDataGridView** - Modern data grid
- **SiticoneShadow** - Shadow decoration for controls

### UIHelper Methods

The `UIHelper` class provides helper methods for creating Siticone controls:

```csharp
// Create modern button
var button = UIHelper.CreateModernButton("Text", ThemeManager.Brand);

// Create modern textbox
var textbox = UIHelper.CreateModernTextBox("Placeholder");

// Create modern panel/card
var panel = UIHelper.CreateModernCard(400, 300);

// Create modern combobox
var combo = UIHelper.CreateModernComboBox(new[] { "Option 1", "Option 2" });

// Create theme toggle button
var toggle = UIHelper.CreateModernThemeToggle();
```

## 🎨 Theme Integration

All Siticone controls should use ThemeManager colors for consistency:

```csharp
// Primary brand color
button.FillColor = ThemeManager.Brand;

// Background colors
this.BackColor = ThemeManager.BackgroundApp;
panel.FillColor = ThemeManager.CardBackground;

// Text colors
label.ForeColor = ThemeManager.TextPrimary;
secondaryLabel.ForeColor = ThemeManager.TextSecondary;

// Subscribe to theme changes
ThemeManager.ThemeChanged += (s, e) =>
{
    ApplyTheme();
};

private void ApplyTheme()
{
    // Update all control colors based on current theme
    mainPanel.FillColor = ThemeManager.CardBackground;
    btnAction.FillColor = ThemeManager.Brand;
    // ... etc
}
```

## 🔍 Troubleshooting

### Issue: "The type or namespace name 'Siticone' could not be found"

**Solutions:**
1. Restore NuGet packages: Right-click solution → "Restore NuGet Packages"
2. Clean and rebuild: `Build → Clean Solution`, then `Build → Rebuild Solution`
3. Verify reference: Check that `Siticone.Desktop.UI` appears in References without a warning icon
4. Check HintPath: Verify `packages\Siticone.Desktop.UI.2.1.1\lib\net40\Siticone.Desktop.UI.dll` exists

### Issue: "Could not resolve reference 'Siticone.Desktop.UI'"

**Solutions:**
1. Delete `packages/` folder
2. Delete `bin/` and `obj/` folders
3. Restore packages again
4. Rebuild solution

### Issue: "Package restore failed"

**Solutions:**
1. Check internet connection
2. Clear NuGet cache:
   ```powershell
   nuget locals all -clear
   ```
3. Verify NuGet sources:
   ```powershell
   nuget sources list
   ```
4. Ensure https://api.nuget.org/v3/index.json is in sources

### Issue: Build errors after adding Siticone controls

**Solutions:**
1. Add using directive: `using Siticone.Desktop.UI.WinForms;`
2. Check control declarations match Siticone types
3. Ensure .NET Framework 4.8 is targeted
4. Rebuild project

### Issue: Controls not displaying correctly

**Solutions:**
1. Verify Windows Forms Designer is using correct framework
2. Check that form's `InitializeComponent()` is called in constructor
3. Ensure parent controls are added before child controls
4. Verify control sizes and locations are set

## 📚 Additional Resources

### Documentation
- [Siticone Official Documentation](https://siticone.com/docs) (if available)
- [MODERNIZATION_GUIDE.md](MODERNIZATION_GUIDE.md) - Complete modernization guide
- [QUICK_MODERNIZATION_TEMPLATE.md](QUICK_MODERNIZATION_TEMPLATE.md) - Quick reference
- [IMPLEMENTATION_SUMMARY.md](IMPLEMENTATION_SUMMARY.md) - Implementation details

### Example Forms
Study these modernized forms as reference:
- **Forms/Auth/FormLogin.cs** - Simple form with login
- **Forms/Auth/FormRegistro.cs** - Multi-field form
- **Forms/Dashboard/FormDashboard.cs** - Complex layout with sidebar

### Utility Classes
- **Utils/ThemeManager.cs** - Theme management system
- **Utils/UIHelper.cs** - Helper methods for UI creation
- **Utils/ClickDeskStyles.cs** - Legacy color constants

## ✅ Verification Checklist

After setup, verify the following:

- [ ] Solution opens without errors in Visual Studio
- [ ] NuGet packages are restored (check `packages/` folder)
- [ ] Siticone.Desktop.UI reference shows in References (no warning)
- [ ] Solution builds successfully (Ctrl+Shift+B)
- [ ] Application runs and shows modernized forms
- [ ] FormLogin displays with Siticone controls
- [ ] Theme toggle works (light/dark mode switch)
- [ ] No missing reference errors in Error List

## 🎯 Next Steps

After successful setup:

1. **Review Modernized Forms**: Study the 3 completed forms to understand the pattern
2. **Read Documentation**: Review MODERNIZATION_GUIDE.md for detailed instructions
3. **Modernize Remaining Forms**: Use QUICK_MODERNIZATION_TEMPLATE.md as reference
4. **Test Thoroughly**: Verify both light and dark themes
5. **Maintain Consistency**: Follow established patterns and use UIHelper methods

## 📝 Notes

- **Windows Only**: This project requires Windows to build and run
- **No Linux/Mac Support**: Siticone controls are Windows Forms-specific
- **Build Tools**: Visual Studio or MSBuild required
- **Package Management**: Uses packages.config (legacy) not PackageReference
- **Target Framework**: .NET Framework 4.8 (not .NET Core/5+/6+)

## 🆘 Getting Help

If you encounter issues not covered in this guide:

1. Check the existing documentation in this directory
2. Review the modernized forms for working examples
3. Ensure you're using Visual Studio on Windows
4. Verify .NET Framework 4.8 SDK is installed
5. Try cleaning and rebuilding the solution

---

**Last Updated**: December 2025  
**Siticone Version**: 2.1.1  
**Target Framework**: .NET Framework 4.8  
**Project**: ClickDesk Desktop Application

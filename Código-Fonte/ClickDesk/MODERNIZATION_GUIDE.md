# ClickDesk UI Modernization Guide

## Overview
This guide documents the modernization of the ClickDesk Windows Forms application using the Siticone UI Framework. The modernization maintains the ClickDesk brand identity while providing a modern, responsive user experience with dark/light theme support.

## Framework Selection
**Selected Framework:** Siticone.Desktop.UI v2.1.1

### Why Siticone?
- Modern, flat design aesthetic
- Built-in shadow and animation effects
- Comprehensive control library
- Easy theme customization
- Active development and support
- Compatible with .NET Framework 4.8

## Installation

### NuGet Package
```bash
Install-Package Siticone.Desktop.UI -Version 2.1.1
```

### Manual Installation
1. Right-click on project → Manage NuGet Packages
2. Search for "Siticone.Desktop.UI"
3. Install version 2.1.1

## Theme System

### ThemeManager Class
Location: `Utils/ThemeManager.cs`

The `ThemeManager` provides centralized theme management with the following features:

- **Light Theme**: Maintains ClickDesk's signature beige (#EDE6D9) background
- **Dark Theme**: Modern dark mode with consistent color palette
- **Dynamic Switching**: Live theme updates without restarting
- **Persistence**: Saves user theme preference
- **Event-Driven**: `ThemeChanged` event for reactive updates

### Using ThemeManager

```csharp
// Load theme preference on startup (in Program.cs)
ThemeManager.LoadThemePreference();

// Toggle theme
ThemeManager.ToggleTheme();

// Check current theme
bool isDark = ThemeManager.IsDarkMode;

// Get theme colors
Color background = ThemeManager.BackgroundApp;
Color cardBg = ThemeManager.CardBackground;
Color brandColor = ThemeManager.Brand;

// Subscribe to theme changes
ThemeManager.ThemeChanged += (s, e) => {
    // Update your UI
    ApplyTheme();
};

// Save theme preference
ThemeManager.SaveThemePreference();
```

## Control Migration Guide

### Standard Controls → Siticone Controls

| Standard Control | Siticone Control | Key Properties |
|-----------------|------------------|----------------|
| `Panel` | `SiticonePanel` | `BorderRadius`, `ShadowDecoration` |
| `Button` | `SiticoneButton` | `BorderRadius`, `HoverState` |
| `TextBox` | `SiticoneTextBox` | `BorderRadius`, `PlaceholderText` |
| `ComboBox` | `SiticoneComboBox` | `BorderRadius`, `ItemsAppearance` |
| `DataGridView` | `SiticoneDataGridView` | Enhanced styling |
| `CheckBox` | `SiticoneCheckBox` | Modern appearance |
| `RadioButton` | `SiticoneRadioButton` | Modern appearance |
| `TrackBar` | `SiticoneTrackBar` | Smooth styling |

### Button Migration Example

**Before (Standard Button):**
```csharp
var button = new Button
{
    Text = "ENTRAR",
    Size = new Size(330, 50),
    FlatStyle = FlatStyle.Flat,
    BackColor = ClickDeskColors.Brand,
    ForeColor = Color.White,
    Font = ClickDeskStyles.FontLG,
    Cursor = Cursors.Hand
};
button.FlatAppearance.BorderSize = 0;
button.MouseEnter += (s, e) => button.BackColor = ClickDeskColors.BrandDark;
button.MouseLeave += (s, e) => button.BackColor = ClickDeskColors.Brand;
```

**After (Siticone Button):**
```csharp
var button = new SiticoneButton
{
    Text = "ENTRAR",
    Size = new Size(330, 50),
    BorderRadius = ClickDeskStyles.RadiusMD,
    FillColor = ThemeManager.Brand,
    ForeColor = Color.White,
    Font = ClickDeskStyles.FontLG,
    Cursor = Cursors.Hand,
    HoverState = { FillColor = ThemeManager.BrandHover }
};
```

### TextBox Migration Example

**Before (Standard TextBox):**
```csharp
var textBox = new TextBox
{
    Size = new Size(330, 40),
    Font = ClickDeskStyles.FontBase,
    BorderStyle = BorderStyle.FixedSingle,
    BackColor = Color.White,
    ForeColor = ClickDeskColors.TextPrimary
};
```

**After (Siticone TextBox):**
```csharp
var textBox = new SiticoneTextBox
{
    Size = new Size(330, 45),
    Font = ClickDeskStyles.FontBase,
    BorderRadius = ClickDeskStyles.RadiusSM,
    BorderThickness = 1,
    BorderColor = ThemeManager.Border,
    FillColor = ThemeManager.CardBackground,
    ForeColor = ThemeManager.TextPrimary,
    PlaceholderText = "Digite aqui...",
    TextOffset = new Point(10, 0)
};
```

### Panel Migration Example

**Before (Standard Panel with Custom Paint):**
```csharp
var panel = new Panel
{
    Size = new Size(450, 550),
    BackColor = Color.White
};
panel.Paint += (s, e) => {
    // Custom rounded corner painting
    var rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
    var path = ClickDeskStyles.GetRoundedRectangle(rect, ClickDeskStyles.RadiusXL);
    e.Graphics.FillPath(new SolidBrush(Color.White), path);
};
```

**After (Siticone Panel):**
```csharp
var panel = new SiticonePanel
{
    Size = new Size(450, 580),
    BackColor = ThemeManager.CardBackground,
    BorderRadius = ClickDeskStyles.RadiusXL,
    ShadowDecoration = { 
        Enabled = true, 
        Shadow = new SiticoneShadow() { Depth = 20 } 
    }
};
```

## Implementing Theme Toggle

### Creating a Theme Toggle Button

```csharp
private SiticoneButton btnThemeToggle;

// In form initialization
btnThemeToggle = new SiticoneButton
{
    Text = ThemeManager.IsDarkMode ? "🌙" : "☀️",
    Size = new Size(50, 50),
    BorderRadius = 25,
    FillColor = ThemeManager.CardBackground,
    ForeColor = ThemeManager.TextPrimary,
    Font = new Font("Segoe UI", 18f),
    Cursor = Cursors.Hand,
    HoverState = { FillColor = ThemeManager.Surface }
};

btnThemeToggle.Click += (s, e) =>
{
    ThemeManager.ToggleTheme();
    btnThemeToggle.Text = ThemeManager.IsDarkMode ? "🌙" : "☀️";
    ThemeManager.SaveThemePreference();
};
```

### Applying Theme to Form

```csharp
public partial class MyForm : Form
{
    public MyForm()
    {
        InitializeComponent();
        
        // Subscribe to theme changes
        ThemeManager.ThemeChanged += (s, e) =>
        {
            this.BackColor = ThemeManager.BackgroundApp;
            ApplyTheme();
        };
        
        CreateInterface();
    }
    
    private void ApplyTheme()
    {
        // Update panel colors
        mainPanel.FillColor = ThemeManager.CardBackground;
        
        // Update text boxes
        txtInput.FillColor = ThemeManager.CardBackground;
        txtInput.ForeColor = ThemeManager.TextPrimary;
        txtInput.BorderColor = ThemeManager.Border;
        
        // Update buttons
        btnSubmit.FillColor = ThemeManager.Brand;
        btnSubmit.HoverState.FillColor = ThemeManager.BrandHover;
        
        // Update labels
        foreach (Control control in Controls)
        {
            if (control is Label label)
            {
                label.ForeColor = label.Font.Bold 
                    ? ThemeManager.TextPrimary 
                    : ThemeManager.TextSecondary;
            }
        }
        
        // Force redraw
        this.Invalidate();
    }
}
```

## Brand Color Consistency

### ClickDesk Brand Colors

The theme system maintains brand consistency across both themes:

**Light Theme:**
- Primary Brand: #F28A1A (Orange)
- Brand Hover: #D97706 (Dark Orange)
- Background: #EDE6D9 (Beige)
- Surface: #F5EFE6 (Light Beige)

**Dark Theme:**
- Primary Brand: #FF9830 (Lighter Orange for better contrast)
- Brand Hover: #FF851B (Orange)
- Background: #121212 (Very Dark Gray)
- Surface: #1E1E1E (Dark Gray)

### Using Brand Colors

```csharp
// Always use ThemeManager for brand colors
button.FillColor = ThemeManager.Brand;
button.HoverState.FillColor = ThemeManager.BrandHover;

// For status colors, use ClickDeskColors directly
labelSuccess.ForeColor = ClickDeskColors.Success;
labelError.ForeColor = ClickDeskColors.Danger;
labelWarning.ForeColor = ClickDeskColors.Warning;
```

## Responsive Design

### Screen Resolution Handling

```csharp
protected override void OnResize(EventArgs e)
{
    base.OnResize(e);
    
    // Adjust layout based on form size
    if (this.Width < 1200)
    {
        // Compact layout
        sidebarPanel.Width = 200;
    }
    else
    {
        // Standard layout
        sidebarPanel.Width = 260;
    }
    
    // Re-center panels
    CenterPanel(mainPanel);
}

private void CenterPanel(Control panel)
{
    panel.Location = new Point(
        (this.ClientSize.Width - panel.Width) / 2,
        (this.ClientSize.Height - panel.Height) / 2
    );
}
```

### Minimum Form Sizes

Set appropriate minimum sizes to maintain usability:

```csharp
this.MinimumSize = new Size(1200, 700);
```

## Best Practices

### 1. Always Use ThemeManager Colors

❌ **Don't:**
```csharp
panel.BackColor = Color.White;
label.ForeColor = Color.Black;
```

✅ **Do:**
```csharp
panel.BackColor = ThemeManager.CardBackground;
label.ForeColor = ThemeManager.TextPrimary;
```

### 2. Subscribe to Theme Changes

❌ **Don't:**
```csharp
// Static colors that don't update
panel.BackColor = ThemeManager.CardBackground;
```

✅ **Do:**
```csharp
ThemeManager.ThemeChanged += (s, e) =>
{
    panel.BackColor = ThemeManager.CardBackground;
};
```

### 3. Use Consistent Border Radius

```csharp
// Use predefined radius values from ClickDeskStyles
button.BorderRadius = ClickDeskStyles.RadiusMD;  // 10px
textBox.BorderRadius = ClickDeskStyles.RadiusSM;  // 8px
panel.BorderRadius = ClickDeskStyles.RadiusXL;   // 16px
```

### 4. Maintain Accessibility

```csharp
// Ensure sufficient contrast
// Use TextPrimary for main content
// Use TextSecondary for less important content
label.ForeColor = ThemeManager.TextPrimary;
labelSubtext.ForeColor = ThemeManager.TextSecondary;
```

### 5. Test Both Themes

Always test your forms in both light and dark modes:

```csharp
// Toggle theme for testing
ThemeManager.IsDarkMode = true;  // Test dark mode
ThemeManager.IsDarkMode = false; // Test light mode
```

## Common Patterns

### Form Template

```csharp
using System;
using System.Drawing;
using System.Windows.Forms;
using ClickDesk.Utils;
using Siticone.Desktop.UI.WinForms;

namespace ClickDesk.Forms.YourModule
{
    public partial class YourForm : Form
    {
        private SiticonePanel mainPanel;
        private SiticoneButton btnThemeToggle;
        
        public YourForm()
        {
            InitializeComponent();
            InitializeTheme();
            CreateInterface();
        }
        
        private void InitializeTheme()
        {
            this.BackColor = ThemeManager.BackgroundApp;
            
            ThemeManager.ThemeChanged += (s, e) =>
            {
                this.BackColor = ThemeManager.BackgroundApp;
                ApplyTheme();
            };
        }
        
        private void CreateInterface()
        {
            // Create UI controls here
            mainPanel = new SiticonePanel
            {
                Size = new Size(800, 600),
                BorderRadius = ClickDeskStyles.RadiusXL,
                FillColor = ThemeManager.CardBackground
            };
            
            this.Controls.Add(mainPanel);
        }
        
        private void ApplyTheme()
        {
            mainPanel.FillColor = ThemeManager.CardBackground;
            // Update other controls
            this.Invalidate();
        }
    }
}
```

## Troubleshooting

### Issue: Controls not updating on theme change

**Solution:** Ensure you've subscribed to `ThemeManager.ThemeChanged` and call `ApplyTheme()`.

### Issue: Colors look wrong in dark mode

**Solution:** Make sure you're using `ThemeManager` colors, not hardcoded colors.

### Issue: Siticone controls not appearing in designer

**Solution:** Rebuild the project. If issue persists, manually add controls in code.

### Issue: Theme preference not saving

**Solution:** Ensure `Settings.settings` is included in the project and `ThemeManager.SaveThemePreference()` is called.

## Testing Checklist

- [ ] Form displays correctly in light mode
- [ ] Form displays correctly in dark mode
- [ ] Theme toggle button works
- [ ] Theme preference persists across sessions
- [ ] All interactive controls are responsive
- [ ] Brand colors are consistent
- [ ] Text is readable in both themes
- [ ] No visual glitches when switching themes
- [ ] Form scales appropriately for different resolutions
- [ ] All functionality works as expected

## Migration Status

### Completed Forms
- [x] FormLogin

### Pending Forms
- [ ] FormRegistro
- [ ] FormRecuperarSenha
- [ ] FormTermosUso
- [ ] FormDashboard
- [ ] FormDashboardAdmin
- [ ] FormNovoChamado
- [ ] FormMeusChamados
- [ ] FormDetalhesChamado
- [ ] FormDetalhesChamadoTecnico
- [ ] FormListaChamados
- [ ] FormAprovarChamados
- [ ] FormFAQ
- [ ] FormFAQAdmin
- [ ] FormPerfil
- [ ] FormEditarPerfil
- [ ] FormCriarUsuario

## Resources

- [Siticone Documentation](https://siticone.com/docs)
- [ClickDesk Style Guide](README.md)
- [Color Palette Reference](Utils/ClickDeskStyles.cs)
- [Theme Manager Reference](Utils/ThemeManager.cs)

# Quick Form Modernization Template

This template provides a quick reference for modernizing Windows Forms to use Siticone controls.

## Step 1: Update Using Statements

```csharp
// Add these imports
using ClickDesk.Utils;
using Siticone.Desktop.UI.WinForms;
```

## Step 2: Replace Field Declarations

```csharp
// OLD → NEW
private Panel panel;          → private SiticonePanel panel;
private TextBox textBox;      → private SiticoneTextBox textBox;
private Button button;        → private SiticoneButton button;
private ComboBox comboBox;    → private SiticoneComboBox comboBox;
private CheckBox checkBox;    → private SiticoneCheckBox checkBox;
private DataGridView dgv;     → private SiticoneDataGridView dgv;
```

## Step 3: Update Form Initialization

```csharp
private void SetupForm()
{
    // OLD
    this.BackColor = ClickDeskColors.BackgroundApp;
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    
    // NEW
    this.BackColor = ThemeManager.BackgroundApp;
    this.FormBorderStyle = FormBorderStyle.None; // For modern look
    
    // Add theme subscription
    ThemeManager.ThemeChanged += (s, e) =>
    {
        this.BackColor = ThemeManager.BackgroundApp;
        ApplyTheme();
    };
}
```

## Step 4: Replace Control Creation

### Panel → SiticonePanel
```csharp
// OLD
var panel = new Panel
{
    BackColor = Color.White,
    Size = new Size(400, 500)
};
panel.Paint += CustomPaintMethod;

// NEW
var panel = new SiticonePanel
{
    FillColor = ThemeManager.CardBackground,
    Size = new Size(400, 500),
    BorderRadius = ClickDeskStyles.RadiusXL,
    ShadowDecoration = { Enabled = true, Shadow = new SiticoneShadow() { Depth = 20 } }
};
```

### TextBox → SiticoneTextBox
```csharp
// OLD
var textBox = new TextBox
{
    BorderStyle = BorderStyle.FixedSingle,
    Size = new Size(300, 30)
};

// NEW
var textBox = new SiticoneTextBox
{
    Size = new Size(300, 45),
    BorderRadius = ClickDeskStyles.RadiusSM,
    BorderThickness = 1,
    BorderColor = ThemeManager.Border,
    FillColor = ThemeManager.CardBackground,
    ForeColor = ThemeManager.TextPrimary,
    PlaceholderText = "Enter text...",
    TextOffset = new Point(10, 0)
};
```

### Button → SiticoneButton
```csharp
// OLD
var button = new Button
{
    Text = "Submit",
    FlatStyle = FlatStyle.Flat,
    BackColor = ClickDeskColors.Primary,
    ForeColor = Color.White
};
button.FlatAppearance.BorderSize = 0;

// NEW
var button = new SiticoneButton
{
    Text = "Submit",
    BorderRadius = ClickDeskStyles.RadiusMD,
    FillColor = ThemeManager.Brand,
    ForeColor = Color.White,
    Font = ClickDeskStyles.FontLG,
    Cursor = Cursors.Hand,
    HoverState = { FillColor = ThemeManager.BrandHover }
};
```

### ComboBox → SiticoneComboBox
```csharp
// OLD
var comboBox = new ComboBox
{
    DropDownStyle = ComboBoxStyle.DropDownList
};

// NEW
var comboBox = new SiticoneComboBox
{
    Size = new Size(300, 40),
    BorderRadius = ClickDeskStyles.RadiusSM,
    BorderColor = ThemeManager.Border,
    FillColor = ThemeManager.CardBackground,
    ForeColor = ThemeManager.TextPrimary
};
```

## Step 5: Replace Color References

```csharp
// OLD → NEW
ClickDeskColors.BackgroundApp    → ThemeManager.BackgroundApp
ClickDeskColors.White            → ThemeManager.CardBackground
ClickDeskColors.CardBackground   → ThemeManager.CardBackground
ClickDeskColors.TextPrimary      → ThemeManager.TextPrimary
ClickDeskColors.TextSecondary    → ThemeManager.TextSecondary
ClickDeskColors.Brand            → ThemeManager.Brand
ClickDeskColors.SidebarBackground → ThemeManager.SidebarBackground
ClickDeskColors.Border           → ThemeManager.Border
```

## Step 6: Add ApplyTheme Method

```csharp
/// <summary>
/// Applies the current theme to all controls.
/// </summary>
private void ApplyTheme()
{
    // Update main panel
    if (mainPanel != null)
    {
        mainPanel.FillColor = ThemeManager.CardBackground;
    }
    
    // Update text boxes
    foreach (Control control in this.Controls)
    {
        UpdateControlTheme(control);
    }
    
    this.Invalidate();
}

private void UpdateControlTheme(Control control)
{
    if (control is SiticoneTextBox textBox)
    {
        textBox.FillColor = ThemeManager.CardBackground;
        textBox.ForeColor = ThemeManager.TextPrimary;
        textBox.BorderColor = ThemeManager.Border;
    }
    else if (control is SiticoneButton button && button.FillColor != ThemeManager.Brand)
    {
        // Update neutral buttons only
        button.FillColor = ThemeManager.CardBackground;
        button.ForeColor = ThemeManager.TextPrimary;
    }
    else if (control is Label label)
    {
        label.ForeColor = label.Font.Bold ? 
            ThemeManager.TextPrimary : ThemeManager.TextSecondary;
    }
    
    // Recursively update child controls
    if (control.HasChildren)
    {
        foreach (Control child in control.Controls)
        {
            UpdateControlTheme(child);
        }
    }
}
```

## Step 7: Add Theme Toggle (Optional)

```csharp
// For forms with navigation/sidebar
var btnThemeToggle = new SiticoneButton
{
    Text = ThemeManager.IsDarkMode ? "🌙" : "☀️",
    Size = new Size(50, 50),
    BorderRadius = 25,
    FillColor = ThemeManager.CardBackground,
    ForeColor = ThemeManager.TextPrimary,
    HoverState = { FillColor = ThemeManager.Surface }
};

btnThemeToggle.Click += (s, e) =>
{
    ThemeManager.ToggleTheme();
    btnThemeToggle.Text = ThemeManager.IsDarkMode ? "🌙" : "☀️";
    ThemeManager.SaveThemePreference();
};
```

## Step 8: Update Labels

```csharp
// OLD
var label = new Label
{
    ForeColor = ClickDeskColors.TextPrimary
};

// NEW
var label = new Label
{
    ForeColor = ThemeManager.TextPrimary,
    BackColor = Color.Transparent // Important for panels
};
```

## Checklist for Each Form

- [ ] Add Siticone using statement
- [ ] Replace all control types in field declarations
- [ ] Update form initialization with theme subscription
- [ ] Replace Panel → SiticonePanel
- [ ] Replace TextBox → SiticoneTextBox
- [ ] Replace Button → SiticoneButton
- [ ] Replace other controls as needed
- [ ] Replace ClickDeskColors with ThemeManager
- [ ] Add BackColor = Color.Transparent to labels
- [ ] Add ApplyTheme() method
- [ ] Test light theme
- [ ] Test dark theme
- [ ] Test theme switching
- [ ] Verify all functionality still works

## Common Gotchas

1. **Labels**: Must set `BackColor = Color.Transparent` or they'll hide panel backgrounds
2. **Buttons**: Brand-colored buttons should use `ThemeManager.Brand`, not neutral colors
3. **DataGridView**: Use `SiticoneDataGridView` for automatic theming
4. **Custom Paint**: Remove custom Paint event handlers, Siticone handles it
5. **Border Radius**: Docked panels should have BorderRadius = 0

## Performance Tips

1. Cache theme colors instead of calling ThemeManager repeatedly in loops
2. Use `SuspendLayout()` and `ResumeLayout()` during theme updates
3. Batch control updates in ApplyTheme to minimize redraws
4. Avoid creating new controls during theme changes

## Example: Complete Minimal Form

```csharp
using System;
using System.Drawing;
using System.Windows.Forms;
using ClickDesk.Utils;
using Siticone.Desktop.UI.WinForms;

namespace ClickDesk.Forms.Example
{
    public partial class ExampleForm : Form
    {
        private SiticonePanel mainPanel;
        private SiticoneTextBox txtInput;
        private SiticoneButton btnSubmit;
        
        public ExampleForm()
        {
            InitializeComponent();
            SetupForm();
            CreateControls();
        }
        
        private void SetupForm()
        {
            this.Text = "Example Form";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = ThemeManager.BackgroundApp;
            
            ThemeManager.ThemeChanged += (s, e) =>
            {
                this.BackColor = ThemeManager.BackgroundApp;
                ApplyTheme();
            };
        }
        
        private void CreateControls()
        {
            mainPanel = new SiticonePanel
            {
                Size = new Size(400, 300),
                Location = new Point(100, 50),
                FillColor = ThemeManager.CardBackground,
                BorderRadius = ClickDeskStyles.RadiusXL,
                ShadowDecoration = { Enabled = true }
            };
            this.Controls.Add(mainPanel);
            
            var lblTitle = new Label
            {
                Text = "Example Form",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = ThemeManager.Brand,
                Location = new Point(80, 30),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            mainPanel.Controls.Add(lblTitle);
            
            txtInput = new SiticoneTextBox
            {
                Location = new Point(50, 100),
                Size = new Size(300, 45),
                BorderRadius = ClickDeskStyles.RadiusSM,
                BorderColor = ThemeManager.Border,
                FillColor = ThemeManager.CardBackground,
                PlaceholderText = "Enter text..."
            };
            mainPanel.Controls.Add(txtInput);
            
            btnSubmit = new SiticoneButton
            {
                Text = "Submit",
                Location = new Point(125, 180),
                Size = new Size(150, 45),
                BorderRadius = ClickDeskStyles.RadiusMD,
                FillColor = ThemeManager.Brand,
                ForeColor = Color.White,
                HoverState = { FillColor = ThemeManager.BrandHover }
            };
            btnSubmit.Click += BtnSubmit_Click;
            mainPanel.Controls.Add(btnSubmit);
        }
        
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"You entered: {txtInput.Text}");
        }
        
        private void ApplyTheme()
        {
            mainPanel.FillColor = ThemeManager.CardBackground;
            txtInput.FillColor = ThemeManager.CardBackground;
            txtInput.BorderColor = ThemeManager.Border;
            btnSubmit.FillColor = ThemeManager.Brand;
            
            foreach (Control control in mainPanel.Controls)
            {
                if (control is Label label)
                {
                    label.ForeColor = label.Font.Size > 14 ? 
                        ThemeManager.Brand : ThemeManager.TextPrimary;
                }
            }
            
            this.Invalidate();
        }
    }
}
```

## Quick Reference: Control Properties

| Property | Standard | Siticone Equivalent |
|----------|----------|---------------------|
| BackColor | BackColor | FillColor |
| BorderStyle | BorderStyle | BorderRadius + BorderThickness |
| FlatStyle | FlatStyle | (Not needed) |
| FlatAppearance.BorderSize | FlatAppearance.BorderSize | BorderThickness |
| UseSystemPasswordChar | UseSystemPasswordChar | PasswordChar = '●' |
| - | - | PlaceholderText |
| - | - | ShadowDecoration |
| - | - | HoverState |

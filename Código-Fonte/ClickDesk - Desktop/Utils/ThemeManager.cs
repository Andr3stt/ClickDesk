using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClickDesk.Utils
{
    /// <summary>
    /// Gerencia temas claro e escuro da aplicação ClickDesk.
    /// Mantém a identidade visual da marca enquanto oferece alternância de tema.
    /// </summary>
    public static class ThemeManager
    {
        private static bool _isDarkMode = false;

        /// <summary>
        /// Define se o modo escuro está ativo.
        /// </summary>
        public static bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                if (_isDarkMode != value)
                {
                    _isDarkMode = value;
                    ThemeChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Evento disparado quando o tema é alterado.
        /// </summary>
        public static event EventHandler ThemeChanged;

        /// <summary>
        /// Alterna entre modo claro e escuro.
        /// </summary>
        public static void ToggleTheme()
        {
            IsDarkMode = !IsDarkMode;
        }

        // ========================================
        // CORES DO TEMA CLARO
        // ========================================
        
        public static class LightTheme
        {
            // Background principal - bege característico
            public static Color BackgroundApp => ClickDeskColors.BackgroundApp; // #EDE6D9
            
            // Surface para cards e painéis
            public static Color Surface => ClickDeskColors.Surface; // #F5EFE6
            
            // Card background branco
            public static Color CardBackground => ClickDeskColors.White;
            
            // Sidebar
            public static Color SidebarBackground => ClickDeskColors.Gray800;
            public static Color SidebarText => ClickDeskColors.TextLight;
            public static Color SidebarHover => ClickDeskColors.Gray700;
            
            // Textos
            public static Color TextPrimary => ClickDeskColors.Gray900;
            public static Color TextSecondary => ClickDeskColors.Gray500;
            public static Color TextOnPrimary => ClickDeskColors.White;
            
            // Brand - mantém laranja
            public static Color Brand => ClickDeskColors.Brand; // #F28A1A
            public static Color BrandHover => ClickDeskColors.BrandDark;
            
            // Bordas e separadores
            public static Color Border => ClickDeskColors.Gray300;
            public static Color BorderLight => ClickDeskColors.Gray200;
        }

        // ========================================
        // CORES DO TEMA ESCURO
        // ========================================
        
        public static class DarkTheme
        {
            // Background principal - cinza escuro
            public static Color BackgroundApp => Color.FromArgb(18, 18, 18); // #121212
            
            // Surface para cards e painéis
            public static Color Surface => Color.FromArgb(30, 30, 30); // #1E1E1E
            
            // Card background
            public static Color CardBackground => Color.FromArgb(37, 37, 37); // #252525
            
            // Sidebar
            public static Color SidebarBackground => Color.FromArgb(24, 24, 24); // #181818
            public static Color SidebarText => ClickDeskColors.Gray200;
            public static Color SidebarHover => Color.FromArgb(45, 45, 45); // #2D2D2D
            
            // Textos
            public static Color TextPrimary => ClickDeskColors.Gray100;
            public static Color TextSecondary => ClickDeskColors.Gray400;
            public static Color TextOnPrimary => ClickDeskColors.White;
            
            // Brand - laranja mais suave no escuro
            public static Color Brand => Color.FromArgb(255, 152, 48); // #FF9830
            public static Color BrandHover => Color.FromArgb(255, 133, 27); // #FF851B
            
            // Bordas e separadores
            public static Color Border => Color.FromArgb(60, 60, 60); // #3C3C3C
            public static Color BorderLight => Color.FromArgb(48, 48, 48); // #303030
        }

        // ========================================
        // GETTERS DE CORES DINÂMICAS
        // ========================================
        
        /// <summary>Background principal da aplicação</summary>
        public static Color BackgroundApp => IsDarkMode ? DarkTheme.BackgroundApp : LightTheme.BackgroundApp;
        
        /// <summary>Surface para cards e painéis</summary>
        public static Color Surface => IsDarkMode ? DarkTheme.Surface : LightTheme.Surface;
        
        /// <summary>Card background</summary>
        public static Color CardBackground => IsDarkMode ? DarkTheme.CardBackground : LightTheme.CardBackground;
        
        /// <summary>Background da sidebar</summary>
        public static Color SidebarBackground => IsDarkMode ? DarkTheme.SidebarBackground : LightTheme.SidebarBackground;
        
        /// <summary>Texto da sidebar</summary>
        public static Color SidebarText => IsDarkMode ? DarkTheme.SidebarText : LightTheme.SidebarText;
        
        /// <summary>Hover da sidebar</summary>
        public static Color SidebarHover => IsDarkMode ? DarkTheme.SidebarHover : LightTheme.SidebarHover;
        
        /// <summary>Texto principal</summary>
        public static Color TextPrimary => IsDarkMode ? DarkTheme.TextPrimary : LightTheme.TextPrimary;
        
        /// <summary>Texto secundário</summary>
        public static Color TextSecondary => IsDarkMode ? DarkTheme.TextSecondary : LightTheme.TextSecondary;
        
        /// <summary>Texto sobre cor primária</summary>
        public static Color TextOnPrimary => IsDarkMode ? DarkTheme.TextOnPrimary : LightTheme.TextOnPrimary;
        
        /// <summary>Cor da marca (laranja)</summary>
        public static Color Brand => IsDarkMode ? DarkTheme.Brand : LightTheme.Brand;
        
        /// <summary>Cor da marca no hover</summary>
        public static Color BrandHover => IsDarkMode ? DarkTheme.BrandHover : LightTheme.BrandHover;
        
        /// <summary>Cor de borda</summary>
        public static Color Border => IsDarkMode ? DarkTheme.Border : LightTheme.Border;
        
        /// <summary>Cor de borda clara</summary>
        public static Color BorderLight => IsDarkMode ? DarkTheme.BorderLight : LightTheme.BorderLight;

        // ========================================
        // APLICAÇÃO DE TEMA
        // ========================================
        
        /// <summary>
        /// Aplica o tema atual a um formulário e todos seus controles.
        /// </summary>
        public static void ApplyTheme(Form form)
        {
            if (form == null) return;

            form.BackColor = BackgroundApp;
            ApplyThemeToControls(form.Controls);
        }

        /// <summary>
        /// Aplica o tema recursivamente a uma coleção de controles.
        /// </summary>
        private static void ApplyThemeToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                // Aplica cores baseadas no tipo de controle
                if (control is Panel panel)
                {
                    // Identifica se é sidebar, card ou content area
                    if (panel.Dock == DockStyle.Left && panel.Width <= 300)
                    {
                        // Sidebar
                        panel.BackColor = SidebarBackground;
                    }
                    else if (panel.BackColor == ClickDeskColors.White || 
                             panel.BackColor == ClickDeskColors.CardBackground)
                    {
                        // Card
                        panel.BackColor = CardBackground;
                    }
                    else
                    {
                        // Content area
                        panel.BackColor = BackgroundApp;
                    }
                }
                else if (control is Label label)
                {
                    // Mantém cores especiais (success, danger, etc)
                    if (label.ForeColor == ClickDeskColors.TextPrimary ||
                        label.ForeColor == ClickDeskColors.Gray900)
                    {
                        label.ForeColor = TextPrimary;
                    }
                    else if (label.ForeColor == ClickDeskColors.TextSecondary ||
                             label.ForeColor == ClickDeskColors.Gray500)
                    {
                        label.ForeColor = TextSecondary;
                    }
                    else if (label.ForeColor == ClickDeskColors.TextLight ||
                             label.ForeColor == ClickDeskColors.Gray100)
                    {
                        label.ForeColor = SidebarText;
                    }
                    else if (label.ForeColor == ClickDeskColors.Brand)
                    {
                        label.ForeColor = Brand;
                    }
                }
                else if (control is TextBox textBox)
                {
                    textBox.BackColor = CardBackground;
                    textBox.ForeColor = TextPrimary;
                }
                else if (control is Button button)
                {
                    // Mantém cores de botões coloridos, atualiza apenas os neutros
                    if (button.BackColor == ClickDeskColors.SidebarBackground ||
                        button.BackColor == ClickDeskColors.Gray800)
                    {
                        button.BackColor = SidebarBackground;
                        button.ForeColor = SidebarText;
                    }
                    else if (button.BackColor == ClickDeskColors.Brand)
                    {
                        button.BackColor = Brand;
                    }
                }
                else if (control is DataGridView dgv)
                {
                    dgv.BackgroundColor = CardBackground;
                    dgv.GridColor = Border;
                    dgv.DefaultCellStyle.BackColor = CardBackground;
                    dgv.DefaultCellStyle.ForeColor = TextPrimary;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = SidebarBackground;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = SidebarText;
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = Surface;
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.BackColor = CardBackground;
                    comboBox.ForeColor = TextPrimary;
                }

                // Aplica recursivamente aos controles filhos
                if (control.HasChildren)
                {
                    ApplyThemeToControls(control.Controls);
                }
            }
        }

        /// <summary>
        /// Cria um botão de alternância de tema.
        /// </summary>
        public static Button CreateThemeToggleButton()
        {
            var btnToggle = new Button
            {
                Text = IsDarkMode ? "🌙 Modo Escuro" : "☀️ Modo Claro",
                Size = new Size(260, 50),
                FlatStyle = FlatStyle.Flat,
                BackColor = SidebarBackground,
                ForeColor = SidebarText,
                Font = new Font("Segoe UI", 11F),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                Cursor = Cursors.Hand,
                Tag = "theme-toggle"
            };

            btnToggle.FlatAppearance.BorderSize = 0;

            // Efeito hover
            btnToggle.MouseEnter += (s, e) => btnToggle.BackColor = SidebarHover;
            btnToggle.MouseLeave += (s, e) => btnToggle.BackColor = SidebarBackground;

            // Ação de toggle
            btnToggle.Click += (s, e) =>
            {
                ToggleTheme();
                btnToggle.Text = IsDarkMode ? "🌙 Modo Escuro" : "☀️ Modo Claro";
            };

            return btnToggle;
        }

        /// <summary>
        /// Força atualização visual de um controle específico.
        /// </summary>
        public static void RefreshControl(Control control)
        {
            control?.Invalidate();
            control?.Refresh();
        }

        /// <summary>
        /// Salva a preferência de tema do usuário.
        /// </summary>
        public static void SaveThemePreference()
        {
            try
            {
                Properties.Settings.Default.IsDarkMode = IsDarkMode;
                Properties.Settings.Default.Save();
            }
            catch
            {
                // Ignora erros ao salvar preferência
            }
        }

        /// <summary>
        /// Carrega a preferência de tema do usuário.
        /// </summary>
        public static void LoadThemePreference()
        {
            try
            {
                IsDarkMode = Properties.Settings.Default.IsDarkMode;
            }
            catch
            {
                // Se falhar, usa tema claro como padrão
                IsDarkMode = false;
            }
        }
    }
}

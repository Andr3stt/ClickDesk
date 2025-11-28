using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ClickDesk.Utils
{
    /// <summary>
    /// Classe auxiliar para criação e estilização de componentes de UI.
    /// Centraliza a criação de controles estilizados para manter consistência visual.
    /// </summary>
    public static class UIHelper
    {
        // ========================================
        // CRIAÇÃO DE BOTÕES
        // ========================================

        /// <summary>
        /// Cria um botão estilizado com a cor primária.
        /// </summary>
        public static Button CreatePrimaryButton(string text, int width = 150, int height = 40)
        {
            return CreateStyledButton(text, AppColors.Primary, AppColors.White, width, height);
        }

        /// <summary>
        /// Cria um botão estilizado com a cor de sucesso.
        /// </summary>
        public static Button CreateSuccessButton(string text, int width = 150, int height = 40)
        {
            return CreateStyledButton(text, AppColors.Success, AppColors.White, width, height);
        }

        /// <summary>
        /// Cria um botão estilizado com a cor de perigo.
        /// </summary>
        public static Button CreateDangerButton(string text, int width = 150, int height = 40)
        {
            return CreateStyledButton(text, AppColors.Danger, AppColors.White, width, height);
        }

        /// <summary>
        /// Cria um botão estilizado com a cor de aviso.
        /// </summary>
        public static Button CreateWarningButton(string text, int width = 150, int height = 40)
        {
            return CreateStyledButton(text, AppColors.Warning, AppColors.White, width, height);
        }

        /// <summary>
        /// Cria um botão secundário (outline).
        /// </summary>
        public static Button CreateSecondaryButton(string text, int width = 150, int height = 40)
        {
            var button = CreateStyledButton(text, AppColors.White, AppColors.Primary, width, height);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = AppColors.Primary;
            button.FlatAppearance.BorderSize = 2;
            return button;
        }

        /// <summary>
        /// Cria um botão genérico estilizado.
        /// </summary>
        private static Button CreateStyledButton(string text, Color backColor, Color foreColor, int width, int height)
        {
            var button = new Button
            {
                Text = text,
                Size = new Size(width, height),
                BackColor = backColor,
                ForeColor = foreColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            button.FlatAppearance.BorderSize = 0;

            // Efeito hover
            Color hoverColor = ControlPaint.Dark(backColor, 0.1f);
            button.MouseEnter += (s, e) => button.BackColor = hoverColor;
            button.MouseLeave += (s, e) => button.BackColor = backColor;

            return button;
        }

        // ========================================
        // CRIAÇÃO DE TEXTBOXES
        // ========================================

        /// <summary>
        /// Cria um TextBox estilizado com placeholder.
        /// </summary>
        public static TextBox CreateStyledTextBox(string placeholder = "", int width = 300, bool isPassword = false)
        {
            var textBox = new TextBox
            {
                Size = new Size(width, 35),
                Font = new Font("Segoe UI", 11F),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = AppColors.White
            };

            if (isPassword)
            {
                textBox.UseSystemPasswordChar = true;
            }

            // Placeholder implementation
            if (!string.IsNullOrEmpty(placeholder))
            {
                textBox.Text = placeholder;
                textBox.ForeColor = AppColors.Gray400;

                textBox.Enter += (s, e) =>
                {
                    if (textBox.Text == placeholder)
                    {
                        textBox.Text = "";
                        textBox.ForeColor = AppColors.TextPrimary;
                    }
                };

                textBox.Leave += (s, e) =>
                {
                    if (string.IsNullOrEmpty(textBox.Text))
                    {
                        textBox.Text = placeholder;
                        textBox.ForeColor = AppColors.Gray400;
                    }
                };
            }

            return textBox;
        }

        /// <summary>
        /// Cria um TextBox multi-linha estilizado.
        /// </summary>
        public static TextBox CreateMultilineTextBox(int width = 300, int height = 100)
        {
            return new TextBox
            {
                Size = new Size(width, height),
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = AppColors.White,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
        }

        // ========================================
        // CRIAÇÃO DE LABELS
        // ========================================

        /// <summary>
        /// Cria um Label estilizado para títulos.
        /// </summary>
        public static Label CreateTitleLabel(string text, int fontSize = 18)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", fontSize, FontStyle.Bold),
                ForeColor = AppColors.TextPrimary,
                AutoSize = true
            };
        }

        /// <summary>
        /// Cria um Label estilizado para texto normal.
        /// </summary>
        public static Label CreateLabel(string text, int fontSize = 10)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", fontSize),
                ForeColor = AppColors.TextPrimary,
                AutoSize = true
            };
        }

        /// <summary>
        /// Cria um Label estilizado para texto secundário.
        /// </summary>
        public static Label CreateSecondaryLabel(string text, int fontSize = 9)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", fontSize),
                ForeColor = AppColors.TextSecondary,
                AutoSize = true
            };
        }

        // ========================================
        // CRIAÇÃO DE PANELS
        // ========================================

        /// <summary>
        /// Cria um Panel estilizado como card.
        /// </summary>
        public static Panel CreateCard(int width = 300, int height = 200)
        {
            var panel = new Panel
            {
                Size = new Size(width, height),
                BackColor = AppColors.CardBackground,
                Padding = new Padding(15)
            };

            // Adiciona sombra simulada com borda
            panel.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle,
                    AppColors.Gray200, 1, ButtonBorderStyle.Solid,
                    AppColors.Gray200, 1, ButtonBorderStyle.Solid,
                    AppColors.Gray200, 1, ButtonBorderStyle.Solid,
                    AppColors.Gray200, 1, ButtonBorderStyle.Solid);
            };

            return panel;
        }

        /// <summary>
        /// Cria um Panel para a sidebar.
        /// </summary>
        public static Panel CreateSidebar(int width = 260)
        {
            return new Panel
            {
                Width = width,
                Dock = DockStyle.Left,
                BackColor = AppColors.SidebarBackground,
                Padding = new Padding(0)
            };
        }

        /// <summary>
        /// Cria um Panel para a área de conteúdo.
        /// </summary>
        public static Panel CreateContentArea()
        {
            return new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AppColors.ContentBackground,
                Padding = new Padding(20),
                AutoScroll = true
            };
        }

        // ========================================
        // CRIAÇÃO DE MENU DA SIDEBAR
        // ========================================

        /// <summary>
        /// Cria um botão de menu para a sidebar.
        /// </summary>
        public static Button CreateMenuButton(string text, string icon = "")
        {
            var button = new Button
            {
                Text = (icon + "  " + text).Trim(),
                Size = new Size(260, 50),
                FlatStyle = FlatStyle.Flat,
                BackColor = AppColors.SidebarBackground,
                ForeColor = AppColors.TextLight,
                Font = new Font("Segoe UI", 11F),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                Cursor = Cursors.Hand
            };

            button.FlatAppearance.BorderSize = 0;

            // Efeito hover
            button.MouseEnter += (s, e) => button.BackColor = AppColors.Gray700;
            button.MouseLeave += (s, e) =>
            {
                if (!button.Focused)
                    button.BackColor = AppColors.SidebarBackground;
            };

            return button;
        }

        /// <summary>
        /// Destaca um botão de menu como selecionado.
        /// </summary>
        public static void SetMenuButtonActive(Button button, bool active)
        {
            if (active)
            {
                button.BackColor = AppColors.Gray700;
                button.ForeColor = AppColors.Primary;
            }
            else
            {
                button.BackColor = AppColors.SidebarBackground;
                button.ForeColor = AppColors.TextLight;
            }
        }

        // ========================================
        // CRIAÇÃO DE DATAGRIDVIEW
        // ========================================

        /// <summary>
        /// Configura um DataGridView com estilo padrão.
        /// </summary>
        public static void StyleDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = AppColors.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = AppColors.Gray200;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersHeight = 45;

            // Estilo do cabeçalho
            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = AppColors.Gray800,
                ForeColor = AppColors.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };
            dgv.EnableHeadersVisualStyles = false;

            // Estilo das células
            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = AppColors.White,
                ForeColor = AppColors.TextPrimary,
                Font = new Font("Segoe UI", 10F),
                SelectionBackColor = AppColors.PrimaryLight,
                SelectionForeColor = AppColors.TextPrimary,
                Padding = new Padding(10, 5, 5, 5)
            };

            // Estilo de linhas alternadas
            dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = AppColors.Gray50
            };

            dgv.RowTemplate.Height = 40;
        }

        // ========================================
        // CRIAÇÃO DE COMBOBOX
        // ========================================

        /// <summary>
        /// Cria um ComboBox estilizado.
        /// </summary>
        public static ComboBox CreateStyledComboBox(int width = 300)
        {
            return new ComboBox
            {
                Size = new Size(width, 35),
                Font = new Font("Segoe UI", 11F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = AppColors.White,
                FlatStyle = FlatStyle.Flat
            };
        }

        // ========================================
        // BADGES DE STATUS
        // ========================================

        /// <summary>
        /// Cria um badge de status colorido.
        /// </summary>
        public static Label CreateStatusBadge(string text, Color backColor)
        {
            var label = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = AppColors.White,
                BackColor = backColor,
                AutoSize = false,
                Size = new Size(100, 24),
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(5, 2, 5, 2)
            };

            return label;
        }

        // ========================================
        // MENSAGENS E DIÁLOGOS
        // ========================================

        /// <summary>
        /// Exibe uma mensagem de erro.
        /// </summary>
        public static void ShowError(string message, string title = "Erro")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Exibe uma mensagem de sucesso.
        /// </summary>
        public static void ShowSuccess(string message, string title = "Sucesso")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Exibe uma mensagem de aviso.
        /// </summary>
        public static void ShowWarning(string message, string title = "Aviso")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Exibe um diálogo de confirmação.
        /// </summary>
        public static bool ShowConfirmation(string message, string title = "Confirmar")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        // ========================================
        // LOADING STATE
        // ========================================

        /// <summary>
        /// Define o estado de loading em um botão.
        /// </summary>
        public static void SetButtonLoading(Button button, bool loading, string loadingText = "Aguarde...")
        {
            button.Enabled = !loading;
            button.Text = loading ? loadingText : button.Tag?.ToString() ?? button.Text;

            if (!loading && button.Tag == null)
            {
                button.Tag = button.Text;
            }
        }

        /// <summary>
        /// Define o cursor de espera no formulário.
        /// </summary>
        public static void SetFormLoading(Form form, bool loading)
        {
            form.Cursor = loading ? Cursors.WaitCursor : Cursors.Default;
            form.UseWaitCursor = loading;
        }
    }
}

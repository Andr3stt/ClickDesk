using ClickDesk.Utils;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickDesk.Models;
using ClickDesk.Services.API;
using Siticone.Desktop.UI.WinForms;

namespace ClickDesk.Forms.Perfil
{
    /// <summary>
    /// Formulário para visualização do perfil do usuário.
    /// </summary>
    public partial class FormPerfil : Form
    {
        private User usuario;
        private SiticonePanel mainPanel;

        /// <summary>
        /// Construtor do formulário.
        /// </summary>
        public FormPerfil()
        {
            InitializeComponent();
            
            // ===== PADRÃO FULLSCREEN MAXIMIZADO =====
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = StyleManager.BgPage;
            
            SetupForm();
        }

        /// <summary>
        /// Configura o formulário.
        /// </summary>
        private void SetupForm()
        {
            this.Text = "ClickDesk - Meu Perfil";
            this.Size = new Size(660, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = ThemeManager.BackgroundApp;

            // Subscribe to theme changes
            ThemeManager.ThemeChanged += (s, e) =>
            {
                this.BackColor = ThemeManager.BackgroundApp;
                ApplyTheme();
            };

            // Main panel with Siticone
            mainPanel = new SiticonePanel
            {
                Size = new Size(600, 540),
                Location = new Point((this.Width - 600) / 2, 30),
                FillColor = ThemeManager.CardBackground,
                BorderRadius = 22
            };
            mainPanel.ShadowDecoration.Enabled = true;
            mainPanel.ShadowDecoration.Depth = 20;
            this.Controls.Add(mainPanel);

            int y = 30;
            int leftMargin = 40;

            // Título
            Label lblTitle = new Label
            {
                Text = "👤 Meu Perfil",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = ThemeManager.Brand,
                Location = new Point(leftMargin, y),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            mainPanel.Controls.Add(lblTitle);

            y += 60;

            // Avatar/Iniciais
            SiticonePanel avatarPanel = new SiticonePanel
            {
                Size = new Size(80, 80),
                Location = new Point(leftMargin, y),
                FillColor = ThemeManager.Brand,
                BorderRadius = 40
            };
            mainPanel.Controls.Add(avatarPanel);

            Label lblAvatar = new Label
            {
                Name = "lblAvatar",
                Text = "U",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(80, 80),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            avatarPanel.Controls.Add(lblAvatar);

            // Nome
            Label lblNome = new Label
            {
                Name = "lblNome",
                Text = "Carregando...",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = ThemeManager.TextPrimary,
                Location = new Point(leftMargin + 100, y + 10),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            mainPanel.Controls.Add(lblNome);

            // Role badge
            Label lblRole = new Label
            {
                Name = "lblRole",
                Text = "USER",
                Font = StyleManager.FontRegular,
                ForeColor = Color.White,
                BackColor = ThemeManager.Brand,
                Location = new Point(leftMargin + 100, y + 50),
                Size = new Size(60, 22),
                TextAlign = ContentAlignment.MiddleCenter
            };
            mainPanel.Controls.Add(lblRole);

            y += 110;

            // Linha separadora
            Panel separator = new Panel
            {
                Size = new Size(520, 1),
                Location = new Point(leftMargin, y),
                BackColor = ThemeManager.Border
            };
            mainPanel.Controls.Add(separator);

            y += 20;

            // Informações
            AddInfoRow("Username:", "lblUsername", leftMargin, y);
            y += 40;
            AddInfoRow("E-mail:", "lblEmail", leftMargin, y);
            y += 40;
            AddInfoRow("Departamento:", "lblDepartamento", leftMargin, y);
            y += 40;
            AddInfoRow("Telefone:", "lblTelefone", leftMargin, y);
            y += 40;
            AddInfoRow("Membro desde:", "lblDataCriacao", leftMargin, y);
            y += 50;

            // Botões
            var btnEditar = new SiticoneButton
            {
                Text = "✏️ Editar Perfil",
                Size = new Size(150, 45),
                Location = new Point(leftMargin, y),
                BorderRadius = 12,
                FillColor = ThemeManager.Brand,
                ForeColor = Color.White,
                Font = StyleManager.FontRegular,
                Cursor = Cursors.Hand,
                HoverState = { FillColor = ThemeManager.BrandHover }
            };
            btnEditar.Click += BtnEditar_Click;
            mainPanel.Controls.Add(btnEditar);

            var btnFechar = new SiticoneButton
            {
                Text = "Fechar",
                Size = new Size(100, 45),
                Location = new Point(leftMargin + 370, y),
                BorderRadius = 12,
                FillColor = ClickDeskColors.Gray300,
                ForeColor = ClickDeskColors.Gray700,
                Font = StyleManager.FontRegular,
                Cursor = Cursors.Hand,
                HoverState = { FillColor = ClickDeskColors.Gray400 }
            };
            btnFechar.Click += (s, e) => this.Close();
            mainPanel.Controls.Add(btnFechar);

            // Carrega dados
            this.Load += async (s, e) => await CarregarPerfil();
        }

        /// <summary>
        /// Adiciona uma linha de informação.
        /// </summary>
        private void AddInfoRow(string label, string valueName, int x, int y)
        {
            var lblLabel = new Label
            {
                Text = label,
                Font = StyleManager.FontRegular,
                ForeColor = ThemeManager.TextSecondary,
                Location = new Point(x, y),
                Size = new Size(120, 20),
                BackColor = Color.Transparent
            };
            mainPanel.Controls.Add(lblLabel);

            var lblValue = new Label
            {
                Name = valueName,
                Text = "-",
                Font = StyleManager.FontRegularStrong,
                ForeColor = ThemeManager.TextPrimary,
                Location = new Point(x + 130, y),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            mainPanel.Controls.Add(lblValue);
        }

        /// <summary>
        /// Aplica o tema atual aos controles do formulário.
        /// </summary>
        private void ApplyTheme()
        {
            mainPanel.FillColor = ThemeManager.CardBackground;

            // Update all labels
            foreach (Control control in mainPanel.Controls)
            {
                if (control is Label label)
                {
                    if (label.Font.Bold)
                    {
                        label.ForeColor = ThemeManager.TextPrimary;
                    }
                    else
                    {
                        label.ForeColor = ThemeManager.TextSecondary;
                    }
                }
                else if (control is Panel separator && separator.Height == 1)
                {
                    separator.BackColor = ThemeManager.Border;
                }
                else if (control is SiticonePanel avatarPanel && avatarPanel.Size.Width == 80)
                {
                    avatarPanel.FillColor = ThemeManager.Brand;
                }
            }

            mainPanel.Invalidate();
            this.Invalidate();
        }

        /// <summary>
        /// Carrega os dados do perfil.
        /// </summary>
        private async Task CarregarPerfil()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Usa dados da sessão primeiro
                usuario = SessionManager.CurrentUser;

                // Tenta atualizar do servidor
                try
                {
                    usuario = await UserService.GetMyProfileAsync();
                    SessionManager.UpdateUser(usuario);
                }
                catch
                {
                    // Usa dados da sessão se falhar
                }

                if (usuario != null)
                {
                    PreencherDados();
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Erro ao carregar perfil: {ex.Message}");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Preenche os dados na tela.
        /// </summary>
        private void PreencherDados()
        {
            // Avatar - Find avatar panel in mainPanel
            SiticonePanel avatarPanel = null;
            foreach (Control control in mainPanel.Controls)
            {
                if (control is SiticonePanel panel && panel.Size.Width == 80)
                {
                    avatarPanel = panel;
                    break;
                }
            }

            if (avatarPanel != null)
            {
                var lblAvatar = avatarPanel.Controls["lblAvatar"] as Label;
                if (lblAvatar != null && !string.IsNullOrEmpty(usuario.Nome))
                {
                    lblAvatar.Text = usuario.Nome.Substring(0, 1).ToUpper();
                }
            }

            // Informações - Find controls in mainPanel
            var lblNome = mainPanel.Controls["lblNome"] as Label;
            var lblRole = mainPanel.Controls["lblRole"] as Label;
            var lblUsername = mainPanel.Controls["lblUsername"] as Label;
            var lblEmail = mainPanel.Controls["lblEmail"] as Label;
            var lblDepartamento = mainPanel.Controls["lblDepartamento"] as Label;
            var lblTelefone = mainPanel.Controls["lblTelefone"] as Label;
            var lblDataCriacao = mainPanel.Controls["lblDataCriacao"] as Label;

            if (lblNome != null) lblNome.Text = usuario.Nome ?? usuario.Username;
            if (lblRole != null)
            {
                lblRole.Text = usuario.Role ?? "USER";
                lblRole.BackColor = ClickDeskColors.GetRoleColor(usuario.Role);
            }

            if (lblUsername != null) lblUsername.Text = usuario.Username ?? "-";
            if (lblEmail != null) lblEmail.Text = usuario.Email ?? "-";
            if (lblDepartamento != null) lblDepartamento.Text = usuario.Departamento ?? "-";
            if (lblTelefone != null) lblTelefone.Text = usuario.Telefone ?? "-";
            if (lblDataCriacao != null) lblDataCriacao.Text = usuario.DataCriacao?.ToString("dd/MM/yyyy") ?? "-";
        }

        /// <summary>
        /// Abre o formulário de edição.
        /// </summary>
        private void BtnEditar_Click(object sender, EventArgs e)
        {
            var formEditar = new FormEditarPerfil(usuario);
            if (formEditar.ShowDialog(this) == DialogResult.OK)
            {
                _ = CarregarPerfil();
            }
        }
    }
}

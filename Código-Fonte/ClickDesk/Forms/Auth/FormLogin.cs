using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading.Tasks;
using ClickDesk.Models;
using ClickDesk.Services.API;
using ClickDesk.Utils;
using Siticone.Desktop.UI.WinForms;

namespace ClickDesk.Forms.Auth
{
    public partial class FormLogin : Form
    {
        private SiticonePanel panelCentral;
        private SiticoneTextBox txtUsuario;
        private SiticoneTextBox txtSenha;
        private SiticoneButton btnLogin;
        private Label lblMensagem;
        private SiticoneButton btnThemeToggle;

        public FormLogin()
        {
            InitializeComponent();
            CriarInterface();
        }

        private void CriarInterface()
        {
            // Configurações do Form
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None; // Remove borda para look moderno
            this.MaximizeBox = false;
            this.BackColor = ThemeManager.BackgroundApp;
            this.Text = "ClickDesk - Login";

            // Subscreve ao evento de mudança de tema
            ThemeManager.ThemeChanged += (s, e) =>
            {
                this.BackColor = ThemeManager.BackgroundApp;
                ApplyTheme();
            };

            // Painel Central com Siticone
            panelCentral = new SiticonePanel
            {
                Size = new Size(450, 580),
                Location = new Point((this.Width - 450) / 2, (this.Height - 580) / 2),
                BackColor = ThemeManager.CardBackground,
                BorderRadius = ClickDeskStyles.RadiusXL,
                ShadowDecoration = { Enabled = true, Shadow = new SiticoneShadow() { Depth = 20 } }
            };
            this.Controls.Add(panelCentral);

            // Logo/Título
            var lblLogo = new Label
            {
                Text = "🖥️ ClickDesk",
                Font = ClickDeskStyles.Font3XL,
                ForeColor = ThemeManager.Brand,
                AutoSize = true,
                Location = new Point(85, 40),
                BackColor = Color.Transparent
            };
            panelCentral.Controls.Add(lblLogo);

            var lblSubtitulo = new Label
            {
                Text = "Sistema de Helpdesk Inteligente",
                Location = new Point(105, 105),
                Size = new Size(240, 30),
                Font = ClickDeskStyles.FontSM,
                ForeColor = ThemeManager.TextSecondary,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            panelCentral.Controls.Add(lblSubtitulo);

            // Label Usuário
            var lblUsuario = new Label
            {
                Text = "Usuário",
                Location = new Point(60, 170),
                Size = new Size(330, 25),
                Font = ClickDeskStyles.FontBaseStrong,
                ForeColor = ThemeManager.TextPrimary,
                BackColor = Color.Transparent
            };
            panelCentral.Controls.Add(lblUsuario);

            // TextBox Usuário (Siticone)
            txtUsuario = new SiticoneTextBox
            {
                Location = new Point(60, 200),
                Size = new Size(330, 45),
                Font = ClickDeskStyles.FontBase,
                BorderRadius = ClickDeskStyles.RadiusSM,
                BorderThickness = 1,
                BorderColor = ThemeManager.Border,
                FillColor = ThemeManager.CardBackground,
                ForeColor = ThemeManager.TextPrimary,
                PlaceholderText = "Digite seu usuário",
                TextOffset = new Point(10, 0)
            };
            panelCentral.Controls.Add(txtUsuario);

            // Label Senha
            var lblSenha = new Label
            {
                Text = "Senha",
                Location = new Point(60, 265),
                Size = new Size(330, 25),
                Font = ClickDeskStyles.FontBaseStrong,
                ForeColor = ThemeManager.TextPrimary,
                BackColor = Color.Transparent
            };
            panelCentral.Controls.Add(lblSenha);

            // TextBox Senha (Siticone)
            txtSenha = new SiticoneTextBox
            {
                Location = new Point(60, 295),
                Size = new Size(330, 45),
                Font = ClickDeskStyles.FontBase,
                BorderRadius = ClickDeskStyles.RadiusSM,
                BorderThickness = 1,
                BorderColor = ThemeManager.Border,
                FillColor = ThemeManager.CardBackground,
                ForeColor = ThemeManager.TextPrimary,
                PasswordChar = '●',
                PlaceholderText = "Digite sua senha",
                TextOffset = new Point(10, 0)
            };
            txtSenha.KeyPress += TxtSenha_KeyPress;
            panelCentral.Controls.Add(txtSenha);

            // Botão Login (Siticone - LARANJA)
            btnLogin = new SiticoneButton
            {
                Text = "ENTRAR",
                Location = new Point(60, 365),
                Size = new Size(330, 50),
                BorderRadius = ClickDeskStyles.RadiusMD,
                FillColor = ThemeManager.Brand,
                ForeColor = Color.White,
                Font = ClickDeskStyles.FontLG,
                Cursor = Cursors.Hand,
                HoverState = { FillColor = ThemeManager.BrandHover }
            };
            btnLogin.Click += BtnLogin_Click;
            panelCentral.Controls.Add(btnLogin);

            // Mensagem de Erro
            lblMensagem = new Label
            {
                Location = new Point(60, 435),
                Size = new Size(330, 40),
                Font = ClickDeskStyles.FontSM,
                ForeColor = ClickDeskColors.StatusError,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false,
                BackColor = Color.Transparent
            };
            panelCentral.Controls.Add(lblMensagem);

            // Link Esqueceu a Senha
            var linkEsqueceuSenha = new LinkLabel
            {
                Text = "Esqueceu sua senha?",
                Location = new Point(160, 485),
                AutoSize = true,
                LinkColor = ThemeManager.Brand,
                Font = ClickDeskStyles.FontSM,
                BackColor = Color.Transparent
            };
            linkEsqueceuSenha.LinkClicked += (s, e) =>
            {
                var formRecuperar = new FormRecuperarSenha();
                formRecuperar.ShowDialog();
            };
            panelCentral.Controls.Add(linkEsqueceuSenha);

            // Link Criar Conta
            var linkCriarConta = new LinkLabel
            {
                Text = "Criar Nova Conta",
                Location = new Point(175, 510),
                AutoSize = true,
                LinkColor = ThemeManager.Brand,
                Font = ClickDeskStyles.FontSM,
                BackColor = Color.Transparent
            };
            linkCriarConta.LinkClicked += (s, e) =>
            {
                // Mostra termos de uso antes de criar conta
                var formTermos = new FormTermosUso();
                if (formTermos.ShowDialog() == DialogResult.OK && formTermos.TermosAceitos)
                {
                    var formRegistro = new FormRegistro();
                    formRegistro.ShowDialog();
                }
            };
            panelCentral.Controls.Add(linkCriarConta);

            // Botão de alternância de tema
            btnThemeToggle = new SiticoneButton
            {
                Text = ThemeManager.IsDarkMode ? "🌙" : "☀️",
                Size = new Size(50, 50),
                Location = new Point(this.Width - 120, 15),
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
            this.Controls.Add(btnThemeToggle);

            // Botão Fechar
            var btnFechar = new SiticoneButton
            {
                Text = "✕",
                Size = new Size(50, 50),
                Location = new Point(this.Width - 60, 15),
                BorderRadius = 25,
                FillColor = ThemeManager.CardBackground,
                ForeColor = ThemeManager.TextSecondary,
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                HoverState = { FillColor = ClickDeskColors.DangerLight, ForeColor = ClickDeskColors.Danger }
            };
            btnFechar.Click += (s, e) => Application.Exit();
            this.Controls.Add(btnFechar);
        }

        /// <summary>
        /// Aplica o tema atual aos controles do formulário.
        /// </summary>
        private void ApplyTheme()
        {
            panelCentral.FillColor = ThemeManager.CardBackground;
            txtUsuario.FillColor = ThemeManager.CardBackground;
            txtUsuario.ForeColor = ThemeManager.TextPrimary;
            txtUsuario.BorderColor = ThemeManager.Border;
            txtSenha.FillColor = ThemeManager.CardBackground;
            txtSenha.ForeColor = ThemeManager.TextPrimary;
            txtSenha.BorderColor = ThemeManager.Border;
            btnLogin.FillColor = ThemeManager.Brand;
            btnLogin.HoverState.FillColor = ThemeManager.BrandHover;
            btnThemeToggle.FillColor = ThemeManager.CardBackground;
            btnThemeToggle.ForeColor = ThemeManager.TextPrimary;

            // Atualiza labels
            foreach (Control control in panelCentral.Controls)
            {
                if (control is Label label && label != lblMensagem)
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
                else if (control is LinkLabel link)
                {
                    link.LinkColor = ThemeManager.Brand;
                }
            }

            panelCentral.Invalidate();
            this.Invalidate();
        }

        private void TxtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                BtnLogin_Click(sender, e);
            }
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            await RealizarLogin();
        }

        private async Task RealizarLogin()
        {
            lblMensagem.Visible = false;

            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MostrarErro("Digite seu usuário");
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MostrarErro("Digite sua senha");
                txtSenha.Focus();
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "Aguarde...";
            btnLogin.FillColor = ClickDeskColors.StatusClosed;

            try
            {
                // Utiliza o AuthService para fazer o login
                var resposta = await AuthService.LoginAsync(txtUsuario.Text.Trim(), txtSenha.Text);

                if (resposta != null && !string.IsNullOrEmpty(resposta.Token))
                {
                    // Sessão já é salva pelo AuthService
                    // Redireciona para o dashboard apropriado
                    this.Hide();
                    
                    // Verifica se é admin/tech para abrir dashboard administrativo
                    Form dashboard;
                    if (SessionManager.HasAdminAccess)
                    {
                        dashboard = new Dashboard.FormDashboardAdmin();
                    }
                    else
                    {
                        dashboard = new Dashboard.FormDashboard();
                    }
                    
                    dashboard.FormClosed += (s, e) => this.Close();
                    dashboard.Show();
                }
                else
                {
                    MostrarErro("Usuário ou senha inválidos");
                    ReabilitarBotao();
                }
            }
            catch (ApiException ex)
            {
                MostrarErro(ex.Message);
                ReabilitarBotao();
            }
            catch (Exception ex)
            {
                MostrarErro($"Erro: {ex.Message}");
                ReabilitarBotao();
            }
        }

        private void MostrarErro(string mensagem)
        {
            lblMensagem.Text = mensagem;
            lblMensagem.Visible = true;
        }

        private void ReabilitarBotao()
        {
            btnLogin.Enabled = true;
            btnLogin.Text = "ENTRAR";
            btnLogin.FillColor = ThemeManager.Brand;
        }
    }
}
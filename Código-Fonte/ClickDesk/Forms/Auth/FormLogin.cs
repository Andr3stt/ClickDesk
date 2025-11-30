using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading.Tasks;
using ClickDesk.Services.API;
using ClickDesk.Utils;
using Newtonsoft.Json.Linq;

namespace ClickDesk.Forms.Auth
{
    public partial class FormLogin : Form
    {
        private Panel panelCentral;
        private TextBox txtUsuario;
        private TextBox txtSenha;
        private Button btnLogin;
        private Label lblMensagem;

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
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = ClickDeskColors.BackgroundApp; // Bege #EDE6D9
            this.Text = "ClickDesk - Login";

            // Painel Central Branco
            panelCentral = new Panel
            {
                Size = new Size(450, 550),
                Location = new Point((this.Width - 450) / 2, (this.Height - 550) / 2),
                BackColor = Color.White
            };
            panelCentral.Paint += PanelCentral_Paint;
            this.Controls.Add(panelCentral);

            // Logo/Título
            var lblLogo = new Label
            {
                Text = "🖥️ ClickDesk",
                Font = ClickDeskStyles.Font3XL,
                ForeColor = ClickDeskColors.Brand, // Laranja #F28A1A
                AutoSize = true,
                Location = new Point(85, 40)
            };
            panelCentral.Controls.Add(lblLogo);

            var lblSubtitulo = new Label
            {
                Text = "Sistema de Helpdesk",
                Location = new Point(125, 105),
                Size = new Size(200, 30),
                Font = ClickDeskStyles.FontSM,
                ForeColor = ClickDeskColors.TextSecondary,
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelCentral.Controls.Add(lblSubtitulo);

            // Label Usuário
            var lblUsuario = new Label
            {
                Text = "Usuário",
                Location = new Point(60, 170),
                Size = new Size(330, 25),
                Font = ClickDeskStyles.FontBaseStrong,
                ForeColor = ClickDeskColors.TextPrimary
            };
            panelCentral.Controls.Add(lblUsuario);

            // TextBox Usuário
            txtUsuario = new TextBox
            {
                Location = new Point(60, 200),
                Size = new Size(330, 40),
                Font = ClickDeskStyles.FontBase,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                ForeColor = ClickDeskColors.TextPrimary
            };
            panelCentral.Controls.Add(txtUsuario);

            // Label Senha
            var lblSenha = new Label
            {
                Text = "Senha",
                Location = new Point(60, 260),
                Size = new Size(330, 25),
                Font = ClickDeskStyles.FontBaseStrong,
                ForeColor = ClickDeskColors.TextPrimary
            };
            panelCentral.Controls.Add(lblSenha);

            // TextBox Senha
            txtSenha = new TextBox
            {
                Location = new Point(60, 290),
                Size = new Size(330, 40),
                Font = ClickDeskStyles.FontBase,
                BorderStyle = BorderStyle.FixedSingle,
                PasswordChar = '●',
                BackColor = Color.White,
                ForeColor = ClickDeskColors.TextPrimary
            };
            txtSenha.KeyPress += TxtSenha_KeyPress;
            panelCentral.Controls.Add(txtSenha);

            // Botão Login (LARANJA)
            btnLogin = new Button
            {
                Text = "ENTRAR",
                Location = new Point(60, 360),
                Size = new Size(330, 50),
                FlatStyle = FlatStyle.Flat,
                BackColor = ClickDeskColors.Brand, // LARANJA #F28A1A
                ForeColor = Color.White,
                Font = ClickDeskStyles.FontLG,
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            // Hover effect laranja escuro
            btnLogin.MouseEnter += (s, e) => btnLogin.BackColor = ClickDeskColors.BrandDark;
            btnLogin.MouseLeave += (s, e) => btnLogin.BackColor = ClickDeskColors.Brand;
            btnLogin.Click += BtnLogin_Click;
            panelCentral.Controls.Add(btnLogin);

            // Mensagem de Erro
            lblMensagem = new Label
            {
                Location = new Point(60, 425),
                Size = new Size(330, 40),
                Font = ClickDeskStyles.FontSM,
                ForeColor = ClickDeskColors.StatusError,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };
            panelCentral.Controls.Add(lblMensagem);

            // Link Criar Conta
            var linkCriarConta = new LinkLabel
            {
                Text = "Criar Nova Conta",
                Location = new Point(175, 480),
                AutoSize = true,
                LinkColor = ClickDeskColors.Brand,
                Font = ClickDeskStyles.FontSM
            };
            linkCriarConta.LinkClicked += (s, e) =>
            {
                var formRegistro = new FormRegistro();
                formRegistro.ShowDialog();
            };
            panelCentral.Controls.Add(linkCriarConta);

            // Botão Fechar
            var btnFechar = new Button
            {
                Text = "✕",
                Size = new Size(40, 40),
                Location = new Point(this.Width - 50, 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = ClickDeskColors.TextSecondary,
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnFechar.FlatAppearance.BorderSize = 0;
            btnFechar.Click += (s, e) => Application.Exit();
            this.Controls.Add(btnFechar);
        }

        private void PanelCentral_Paint(object sender, PaintEventArgs e)
        {
            var rect = new Rectangle(0, 0, panelCentral.Width - 1, panelCentral.Height - 1);
            var path = ClickDeskStyles.GetRoundedRectangle(rect, ClickDeskStyles.RadiusXL);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (var brush = new SolidBrush(Color.White))
            {
                e.Graphics.FillPath(brush, path);
            }

            using (var pen = new Pen(ClickDeskColors.Border, 1))
            {
                e.Graphics.DrawPath(pen, path);
            }
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
            btnLogin.BackColor = ClickDeskColors.StatusClosed;

            try
            {
                var credenciais = new
                {
                    username = txtUsuario.Text.Trim(),
                    password = txtSenha.Text
                };

                var resposta = await ApiService.Post<JObject>("/auth/login", credenciais);

                if (resposta != null && resposta["token"] != null)
                {
                    ApiConfig.Token = resposta["token"].ToString();
                    SessionManager.Username = txtUsuario.Text.Trim();
                    SessionManager.CurrentUser = new { Nome = txtUsuario.Text.Trim() };

                    this.Hide();
                    var dashboard = new Dashboard.FormDashboard();
                    dashboard.FormClosed += (s, e) => this.Close();
                    dashboard.Show();
                }
                else
                {
                    MostrarErro("Usuário ou senha inválidos");
                    ReabilitarBotao();
                }
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
            btnLogin.BackColor = ClickDeskColors.Brand;
        }
    }
}
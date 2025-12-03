using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickDesk.Utils;
using ClickDesk.Services.API;
using Siticone.Desktop.UI.WinForms;

namespace ClickDesk.Forms.Auth
{
    public partial class FormLogin : Form
    {
        private bool _showPassword = false;

        public FormLogin()
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

            // ===== CENTRALIZAÇÃO DINÂMICA DO MAINCARD =====
            this.Resize += (s, e) =>
            {
                if (mainCard != null)
                {
                    mainCard.Left = (this.ClientSize.Width - mainCard.Width) / 2;
                    mainCard.Top = (this.ClientSize.Height - mainCard.Height) / 2;
                }
            };

            WireEvents();
        }

        private void WireEvents()
        {
            this.btnTogglePassword.Click += BtnTogglePassword_Click;
            txtUser.TextChanged += (s, e) => ValidateLogin();
            txtPassword.TextChanged += (s, e) => ValidateLogin();
            btnLogin.Click += async (s, e) => await RealizarLogin();

            btnCreateAccount.Click += (s, e) =>
            {
                this.Hide();
                var frm = new FormRegistro();
                frm.FormClosed += (a, b) => this.Show();
                frm.Show();
            };
        }

        private void BtnTogglePassword_Click(object sender, EventArgs e)
        {
            _showPassword = !_showPassword;
            txtPassword.PasswordChar = _showPassword ? '\0' : '●';
            btnTogglePassword.Text = _showPassword ? "Ocultar" : "Mostrar";
        }

        private void ValidateLogin()
        {
            btnLogin.Enabled = !string.IsNullOrWhiteSpace(txtUser.Text)
                                && !string.IsNullOrWhiteSpace(txtPassword.Text);
        }

        private async Task RealizarLogin()
        {
            lblMensagem.Visible = false;

            if (string.IsNullOrWhiteSpace(txtUser.Text))
            {
                MostrarErro("Digite seu usuário.");
                txtUser.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MostrarErro("Digite sua senha.");
                txtPassword.Focus();
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "Aguarde...";

            try
            {
                var resposta = await AuthService.LoginAsync(txtUser.Text.Trim(), txtPassword.Text);

                if (resposta != null && !string.IsNullOrEmpty(resposta.Token))
                {
                    this.Hide();

                    Form dashboard = SessionManager.HasAdminAccess
                        ? new Dashboard.FormDashboardAdmin()
                        : new Dashboard.FormDashboard();

                    dashboard.FormClosed += (s, e) => this.Close();
                    dashboard.Show();
                }
                else
                {
                    MostrarErro("Usuário ou senha incorretos.");
                    ResetBtn();
                }
            }
            catch (ApiException ex)
            {
                MostrarErro(ex.Message);
                ResetBtn();
            }
            catch (Exception ex)
            {
                MostrarErro("Erro inesperado: " + ex.Message);
                ResetBtn();
            }
        }

        private void ResetBtn()
        {
            btnLogin.Enabled = true;
            btnLogin.Text = "ENTRAR";
        }

        private void MostrarErro(string msg)
        {
            lblMensagem.Text = msg;
            lblMensagem.Visible = true;
        }
    }
}

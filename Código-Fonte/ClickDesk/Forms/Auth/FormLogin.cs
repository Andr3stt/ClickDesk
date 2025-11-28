using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickDesk.Forms.Dashboard;
using ClickDesk.Services.API;
using ClickDesk.Utils;

namespace ClickDesk.Forms.Auth
{
    /// <summary>
    /// Formulário de login do sistema ClickDesk.
    /// Permite autenticação via usuário/senha com integração JWT.
    /// </summary>
    public partial class FormLogin : Form
    {
        // Campos do formulário
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnRegistrar;
        private Label lblError;

        /// <summary>
        /// Construtor do formulário de login.
        /// </summary>
        public FormLogin()
        {
            InitializeComponent();
            SetupForm();
        }

        /// <summary>
        /// Configura a aparência e os controles do formulário.
        /// </summary>
        private void SetupForm()
        {
            // Configurações básicas do form
            this.Text = "ClickDesk - Login";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = AppColors.Gray800;

            // Painel central branco
            Panel panelLogin = new Panel
            {
                Size = new Size(400, 500),
                BackColor = AppColors.White,
                Location = new Point((this.ClientSize.Width - 400) / 2, (this.ClientSize.Height - 500) / 2)
            };

            // Bordas arredondadas para o painel de login
            panelLogin.Paint += (s, e) =>
            {
                var rect = new Rectangle(0, 0, panelLogin.Width - 1, panelLogin.Height - 1);
                var path = ClickDeskStyles.GetRoundedRectangle(rect, ClickDeskStyles.RadiusXL);

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                using (var brush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillPath(brush, path);
                }

                using (var pen = new Pen(ClickDeskColors.Border, 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            };

            this.Controls.Add(panelLogin);

            // Logo/Título
            Label lblLogo = new Label
            {
                Text = "🖥️ ClickDesk",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = AppColors.Primary,
                AutoSize = true,
                Location = new Point(85, 40)
            };
            panelLogin.Controls.Add(lblLogo);

            // Subtítulo
            Label lblSubtitle = new Label
            {
                Text = "Sistema de Helpdesk",
                Font = new Font("Segoe UI", 12),
                ForeColor = AppColors.Gray500,
                AutoSize = true,
                Location = new Point(125, 90)
            };
            panelLogin.Controls.Add(lblSubtitle);

            // Label Usuário
            Label lblUsername = new Label
            {
                Text = "Usuário",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = AppColors.Gray700,
                Location = new Point(50, 150),
                AutoSize = true
            };
            panelLogin.Controls.Add(lblUsername);

            // Campo Usuário
            txtUsername = new TextBox
            {
                Size = new Size(300, 40),
                Location = new Point(50, 175),
                Font = new Font("Segoe UI", 12),
                BorderStyle = BorderStyle.FixedSingle
            };
            panelLogin.Controls.Add(txtUsername);

            // Label Senha
            Label lblPassword = new Label
            {
                Text = "Senha",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = AppColors.Gray700,
                Location = new Point(50, 230),
                AutoSize = true
            };
            panelLogin.Controls.Add(lblPassword);

            // Campo Senha
            txtPassword = new TextBox
            {
                Size = new Size(300, 40),
                Location = new Point(50, 255),
                Font = new Font("Segoe UI", 12),
                BorderStyle = BorderStyle.FixedSingle,
                UseSystemPasswordChar = true
            };
            panelLogin.Controls.Add(txtPassword);

            // Label de erro
            lblError = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 9),
                ForeColor = AppColors.Danger,
                Location = new Point(50, 305),
                Size = new Size(300, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };
            panelLogin.Controls.Add(lblError);

            // Botão Login
            btnLogin = new Button
            {
                Text = "ENTRAR",
                Size = new Size(300, 45),
                Location = new Point(50, 335),
                BackColor = AppColors.Primary,
                ForeColor = AppColors.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;
            panelLogin.Controls.Add(btnLogin);

            // Link para registro
            Label lblNoAccount = new Label
            {
                Text = "Não tem uma conta?",
                Font = new Font("Segoe UI", 9),
                ForeColor = AppColors.Gray500,
                Location = new Point(100, 400),
                AutoSize = true
            };
            panelLogin.Controls.Add(lblNoAccount);

            // Botão Registrar
            btnRegistrar = new Button
            {
                Text = "Criar conta",
                Size = new Size(100, 25),
                Location = new Point(230, 396),
                BackColor = AppColors.White,
                ForeColor = AppColors.Primary,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Underline),
                Cursor = Cursors.Hand
            };
            btnRegistrar.FlatAppearance.BorderSize = 0;
            btnRegistrar.Click += BtnRegistrar_Click;
            panelLogin.Controls.Add(btnRegistrar);

            // Rodapé
            Label lblFooter = new Label
            {
                Text = "© 2024 ClickDesk - Todos os direitos reservados",
                Font = new Font("Segoe UI", 8),
                ForeColor = AppColors.Gray400,
                Location = new Point(80, 460),
                AutoSize = true
            };
            panelLogin.Controls.Add(lblFooter);

            // Eventos de teclado
            txtUsername.KeyDown += TxtField_KeyDown;
            txtPassword.KeyDown += TxtField_KeyDown;

            // Foco inicial
            this.ActiveControl = txtUsername;
        }

        /// <summary>
        /// Trata o pressionamento de Enter nos campos.
        /// </summary>
        private void TxtField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BtnLogin_Click(sender, e);
            }
        }

        /// <summary>
        /// Evento de clique no botão de login.
        /// </summary>
        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            await RealizarLogin();
        }

        /// <summary>
        /// Realiza o processo de login.
        /// </summary>
        private async Task RealizarLogin()
        {
            // Limpa erro anterior
            lblError.Visible = false;

            // Validação dos campos
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username))
            {
                ShowError("Por favor, informe o usuário.");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                ShowError("Por favor, informe a senha.");
                txtPassword.Focus();
                return;
            }

            // Desabilita o formulário durante o login
            SetFormEnabled(false);

            try
            {
                // Tenta fazer login
                var response = await AuthService.LoginAsync(username, password);

                if (response != null && !string.IsNullOrEmpty(response.Token))
                {
                    // Login bem-sucedido - abre o dashboard
                    this.Hide();

                    // Verifica se é admin para abrir o dashboard correto
                    Form dashboard;
                    if (SessionManager.IsAdmin || SessionManager.IsTech)
                    {
                        dashboard = new FormDashboardAdmin();
                    }
                    else
                    {
                        dashboard = new FormDashboard();
                    }

                    dashboard.FormClosed += (s, args) => this.Close();
                    dashboard.Show();
                }
                else
                {
                    ShowError("Credenciais inválidas. Verifique usuário e senha.");
                }
            }
            catch (ApiException ex)
            {
                ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                ShowError($"Erro ao conectar: {ex.Message}");
            }
            finally
            {
                SetFormEnabled(true);
            }
        }

        /// <summary>
        /// Exibe mensagem de erro no label.
        /// </summary>
        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
        }

        /// <summary>
        /// Habilita/desabilita o formulário.
        /// </summary>
        private void SetFormEnabled(bool enabled)
        {
            txtUsername.Enabled = enabled;
            txtPassword.Enabled = enabled;
            btnLogin.Enabled = enabled;
            btnRegistrar.Enabled = enabled;
            btnLogin.Text = enabled ? "ENTRAR" : "AGUARDE...";
            this.Cursor = enabled ? Cursors.Default : Cursors.WaitCursor;
        }

        /// <summary>
        /// Evento de clique no botão de registro.
        /// </summary>
        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            // Abre o formulário de registro
            var formRegistro = new FormRegistro();
            formRegistro.ShowDialog(this);
        }
    }
}

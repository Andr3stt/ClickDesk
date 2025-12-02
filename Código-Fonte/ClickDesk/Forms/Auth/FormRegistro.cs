using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickDesk.Models;
using ClickDesk.Services.API;
using ClickDesk.Utils;
using Siticone.Desktop.UI.WinForms;

namespace ClickDesk.Forms.Auth
{
    /// <summary>
    /// Formulário de registro de novos usuários.
    /// Permite criar conta com validação de dados.
    /// </summary>
    public partial class FormRegistro : Form
    {
        // Campos do formulário (modernizados com Siticone)
        private SiticoneTextBox txtNome;
        private SiticoneTextBox txtUsername;
        private SiticoneTextBox txtEmail;
        private SiticoneTextBox txtDepartamento;
        private SiticoneTextBox txtTelefone;
        private SiticoneTextBox txtPassword;
        private SiticoneTextBox txtConfirmPassword;
        private SiticoneButton btnRegistrar;
        private SiticoneButton btnCancelar;
        private Label lblError;
        private SiticonePanel mainPanel;

        /// <summary>
        /// Construtor do formulário de registro.
        /// </summary>
        public FormRegistro()
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
            this.Text = "ClickDesk - Criar Conta";
            this.Size = new Size(550, 720);
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

            // Main container panel with shadow
            mainPanel = new SiticonePanel
            {
                Size = new Size(500, 680),
                Location = new Point(25, 20),
                BorderRadius = ClickDeskStyles.RadiusXL,
                FillColor = ThemeManager.CardBackground
            };
            mainPanel.ShadowDecoration.Enabled = true;
            mainPanel.ShadowDecoration.Depth = 20;
            this.Controls.Add(mainPanel);

            int startY = 30;
            int spacing = 70;
            int labelOffset = 0;
            int inputOffset = 25;
            int leftMargin = 60;
            int inputWidth = 380;

            // Título
            Label lblTitle = new Label
            {
                Text = "Criar Nova Conta",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = ThemeManager.Brand,
                Location = new Point(leftMargin, startY),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            mainPanel.Controls.Add(lblTitle);

            startY += 50;

            // Nome Completo
            mainPanel.Controls.Add(CreateLabel("Nome Completo *", leftMargin, startY + labelOffset));
            txtNome = CreateSiticoneTextBox(leftMargin, startY + inputOffset, inputWidth, "Digite seu nome completo");
            mainPanel.Controls.Add(txtNome);

            startY += spacing;

            // Username
            mainPanel.Controls.Add(CreateLabel("Nome de Usuário *", leftMargin, startY + labelOffset));
            txtUsername = CreateSiticoneTextBox(leftMargin, startY + inputOffset, inputWidth, "Digite seu usuário");
            mainPanel.Controls.Add(txtUsername);

            startY += spacing;

            // Email
            mainPanel.Controls.Add(CreateLabel("E-mail *", leftMargin, startY + labelOffset));
            txtEmail = CreateSiticoneTextBox(leftMargin, startY + inputOffset, inputWidth, "seu.email@exemplo.com");
            mainPanel.Controls.Add(txtEmail);

            startY += spacing;

            // Departamento
            mainPanel.Controls.Add(CreateLabel("Departamento", leftMargin, startY + labelOffset));
            txtDepartamento = CreateSiticoneTextBox(leftMargin, startY + inputOffset, inputWidth, "Ex: TI, RH, Financeiro");
            mainPanel.Controls.Add(txtDepartamento);

            startY += spacing;

            // Telefone
            mainPanel.Controls.Add(CreateLabel("Telefone", leftMargin, startY + labelOffset));
            txtTelefone = CreateSiticoneTextBox(leftMargin, startY + inputOffset, inputWidth, "(00) 00000-0000");
            mainPanel.Controls.Add(txtTelefone);

            startY += spacing;

            // Senha
            mainPanel.Controls.Add(CreateLabel("Senha *", leftMargin, startY + labelOffset));
            txtPassword = CreateSiticoneTextBox(leftMargin, startY + inputOffset, inputWidth, "Mínimo 6 caracteres", true);
            mainPanel.Controls.Add(txtPassword);

            startY += spacing;

            // Confirmar Senha
            mainPanel.Controls.Add(CreateLabel("Confirmar Senha *", leftMargin, startY + labelOffset));
            txtConfirmPassword = CreateSiticoneTextBox(leftMargin, startY + inputOffset, inputWidth, "Digite a senha novamente", true);
            mainPanel.Controls.Add(txtConfirmPassword);

            startY += spacing + 10;

            // Label de erro
            lblError = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 9),
                ForeColor = ClickDeskColors.Danger,
                Location = new Point(leftMargin, startY),
                Size = new Size(inputWidth, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false,
                BackColor = Color.Transparent
            };
            mainPanel.Controls.Add(lblError);

            startY += 30;

            // Botões
            btnCancelar = new SiticoneButton
            {
                Text = "Cancelar",
                Size = new Size(180, 45),
                Location = new Point(leftMargin, startY),
                BorderRadius = ClickDeskStyles.RadiusMD,
                FillColor = ClickDeskColors.Gray300,
                ForeColor = ClickDeskColors.Gray700,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand,
                HoverState = { FillColor = ClickDeskColors.Gray400 }
            };
            btnCancelar.Click += BtnCancelar_Click;
            mainPanel.Controls.Add(btnCancelar);

            btnRegistrar = new SiticoneButton
            {
                Text = "CRIAR CONTA",
                Size = new Size(180, 45),
                Location = new Point(leftMargin + 200, startY),
                BorderRadius = ClickDeskStyles.RadiusMD,
                FillColor = ThemeManager.Brand,
                ForeColor = ClickDeskColors.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand,
                HoverState = { FillColor = ThemeManager.BrandHover }
            };
            btnRegistrar.Click += BtnRegistrar_Click;
            mainPanel.Controls.Add(btnRegistrar);

            // Foco inicial
            this.ActiveControl = txtNome;
        }

        /// <summary>
        /// Cria um label estilizado.
        /// </summary>
        private Label CreateLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ThemeManager.TextPrimary,
                Location = new Point(x, y),
                AutoSize = true,
                BackColor = Color.Transparent
            };
        }

        /// <summary>
        /// Cria um textbox Siticone estilizado.
        /// </summary>
        private SiticoneTextBox CreateSiticoneTextBox(int x, int y, int width, string placeholder, bool isPassword = false)
        {
            return new SiticoneTextBox
            {
                Size = new Size(width, 40),
                Location = new Point(x, y),
                Font = ClickDeskStyles.FontBase,
                BorderRadius = ClickDeskStyles.RadiusSM,
                BorderThickness = 1,
                BorderColor = ThemeManager.Border,
                FillColor = ThemeManager.CardBackground,
                ForeColor = ThemeManager.TextPrimary,
                PlaceholderText = placeholder,
                PasswordChar = isPassword ? '●' : '\0',
                TextOffset = new Point(10, 0)
            };
        }

        /// <summary>
        /// Aplica o tema atual aos controles.
        /// </summary>
        private void ApplyTheme()
        {
            mainPanel.FillColor = ThemeManager.CardBackground;
            
            // Update all textboxes
            foreach (Control control in mainPanel.Controls)
            {
                if (control is SiticoneTextBox textBox)
                {
                    textBox.FillColor = ThemeManager.CardBackground;
                    textBox.ForeColor = ThemeManager.TextPrimary;
                    textBox.BorderColor = ThemeManager.Border;
                }
                else if (control is Label label && label != lblError)
                {
                    label.ForeColor = ThemeManager.TextPrimary;
                }
            }

            btnRegistrar.FillColor = ThemeManager.Brand;
            btnRegistrar.HoverState.FillColor = ThemeManager.BrandHover;

            mainPanel.Invalidate();
            this.Invalidate();
        }

        /// <summary>
        /// Evento de clique no botão cancelar.
        /// </summary>
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Evento de clique no botão registrar.
        /// </summary>
        private async void BtnRegistrar_Click(object sender, EventArgs e)
        {
            await RealizarRegistro();
        }

        /// <summary>
        /// Realiza o processo de registro.
        /// </summary>
        private async Task RealizarRegistro()
        {
            // Limpa erro anterior
            lblError.Visible = false;

            // Validação dos campos
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                ShowError("Por favor, informe seu nome completo.");
                txtNome.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                ShowError("Por favor, informe um nome de usuário.");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !IsValidEmail(txtEmail.Text))
            {
                ShowError("Por favor, informe um e-mail válido.");
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text.Length < 6)
            {
                ShowError("A senha deve ter pelo menos 6 caracteres.");
                txtPassword.Focus();
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                ShowError("As senhas não conferem.");
                txtConfirmPassword.Focus();
                return;
            }

            // Desabilita o formulário
            SetFormEnabled(false);

            try
            {
                var request = new RegisterRequest
                {
                    Nome = txtNome.Text.Trim(),
                    Username = txtUsername.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Departamento = txtDepartamento.Text.Trim(),
                    Telefone = txtTelefone.Text.Trim(),
                    Password = txtPassword.Text,
                    ConfirmPassword = txtConfirmPassword.Text,
                    Role = "USER"
                };

                var response = await AuthService.RegisterAsync(request);

                if (response != null && response.Success)
                {
                    UIHelper.ShowSuccess("Conta criada com sucesso! Você já pode fazer login.", "Sucesso");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    ShowError(response?.Message ?? "Erro ao criar conta. Tente novamente.");
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
        /// Valida formato de email.
        /// </summary>
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Exibe mensagem de erro.
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
            txtNome.Enabled = enabled;
            txtUsername.Enabled = enabled;
            txtEmail.Enabled = enabled;
            txtDepartamento.Enabled = enabled;
            txtTelefone.Enabled = enabled;
            txtPassword.Enabled = enabled;
            txtConfirmPassword.Enabled = enabled;
            btnRegistrar.Enabled = enabled;
            btnCancelar.Enabled = enabled;
            btnRegistrar.Text = enabled ? "CRIAR CONTA" : "AGUARDE...";
            this.Cursor = enabled ? Cursors.Default : Cursors.WaitCursor;
        }
    }
}

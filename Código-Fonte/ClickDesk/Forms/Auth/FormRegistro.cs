using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickDesk.Models;
using ClickDesk.Services.API;
using ClickDesk.Utils;

namespace ClickDesk.Forms.Auth
{
    /// <summary>
    /// Formulário de registro de novos usuários.
    /// Permite criar conta com validação de dados.
    /// </summary>
    public partial class FormRegistro : Form
    {
        // Campos do formulário
        private TextBox txtNome;
        private TextBox txtUsername;
        private TextBox txtEmail;
        private TextBox txtDepartamento;
        private TextBox txtTelefone;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private Button btnRegistrar;
        private Button btnCancelar;
        private Label lblError;

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
            this.Size = new Size(500, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = AppColors.White;

            int startY = 20;
            int spacing = 60;
            int labelOffset = 0;
            int inputOffset = 22;
            int leftMargin = 50;
            int inputWidth = 380;

            // Título
            Label lblTitle = new Label
            {
                Text = "Criar Nova Conta",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = AppColors.Primary,
                Location = new Point(leftMargin, startY),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            startY += 50;

            // Nome Completo
            this.Controls.Add(CreateLabel("Nome Completo *", leftMargin, startY + labelOffset));
            txtNome = CreateTextBox(leftMargin, startY + inputOffset, inputWidth);
            this.Controls.Add(txtNome);

            startY += spacing;

            // Username
            this.Controls.Add(CreateLabel("Nome de Usuário *", leftMargin, startY + labelOffset));
            txtUsername = CreateTextBox(leftMargin, startY + inputOffset, inputWidth);
            this.Controls.Add(txtUsername);

            startY += spacing;

            // Email
            this.Controls.Add(CreateLabel("E-mail *", leftMargin, startY + labelOffset));
            txtEmail = CreateTextBox(leftMargin, startY + inputOffset, inputWidth);
            this.Controls.Add(txtEmail);

            startY += spacing;

            // Departamento
            this.Controls.Add(CreateLabel("Departamento", leftMargin, startY + labelOffset));
            txtDepartamento = CreateTextBox(leftMargin, startY + inputOffset, inputWidth);
            this.Controls.Add(txtDepartamento);

            startY += spacing;

            // Telefone
            this.Controls.Add(CreateLabel("Telefone", leftMargin, startY + labelOffset));
            txtTelefone = CreateTextBox(leftMargin, startY + inputOffset, inputWidth);
            this.Controls.Add(txtTelefone);

            startY += spacing;

            // Senha
            this.Controls.Add(CreateLabel("Senha *", leftMargin, startY + labelOffset));
            txtPassword = CreateTextBox(leftMargin, startY + inputOffset, inputWidth, true);
            this.Controls.Add(txtPassword);

            startY += spacing;

            // Confirmar Senha
            this.Controls.Add(CreateLabel("Confirmar Senha *", leftMargin, startY + labelOffset));
            txtConfirmPassword = CreateTextBox(leftMargin, startY + inputOffset, inputWidth, true);
            this.Controls.Add(txtConfirmPassword);

            startY += spacing;

            // Label de erro
            lblError = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 9),
                ForeColor = AppColors.Danger,
                Location = new Point(leftMargin, startY),
                Size = new Size(inputWidth, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };
            this.Controls.Add(lblError);

            startY += 30;

            // Botões
            btnCancelar = new Button
            {
                Text = "Cancelar",
                Size = new Size(180, 40),
                Location = new Point(leftMargin, startY),
                BackColor = AppColors.Gray200,
                ForeColor = AppColors.Gray700,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += BtnCancelar_Click;
            this.Controls.Add(btnCancelar);

            btnRegistrar = new Button
            {
                Text = "CRIAR CONTA",
                Size = new Size(180, 40),
                Location = new Point(leftMargin + 200, startY),
                BackColor = AppColors.Primary,
                ForeColor = AppColors.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRegistrar.FlatAppearance.BorderSize = 0;
            btnRegistrar.Click += BtnRegistrar_Click;
            this.Controls.Add(btnRegistrar);

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
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = AppColors.Gray700,
                Location = new Point(x, y),
                AutoSize = true
            };
        }

        /// <summary>
        /// Cria um textbox estilizado.
        /// </summary>
        private TextBox CreateTextBox(int x, int y, int width, bool isPassword = false)
        {
            return new TextBox
            {
                Size = new Size(width, 30),
                Location = new Point(x, y),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                UseSystemPasswordChar = isPassword
            };
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

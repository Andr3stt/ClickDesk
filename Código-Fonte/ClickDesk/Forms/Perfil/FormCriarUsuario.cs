using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickDesk.Models;
using ClickDesk.Services.API;
using ClickDesk.Utils;

namespace ClickDesk.Forms.Perfil
{
    /// <summary>
    /// Formulário para criação de novos usuários (admin).
    /// </summary>
    public partial class FormCriarUsuario : Form
    {
        private TextBox txtNome;
        private TextBox txtUsername;
        private TextBox txtEmail;
        private TextBox txtDepartamento;
        private TextBox txtTelefone;
        private TextBox txtSenha;
        private TextBox txtConfirmarSenha;
        private ComboBox cmbRole;
        private Button btnCriar;

        /// <summary>
        /// Construtor do formulário.
        /// </summary>
        public FormCriarUsuario()
        {
            InitializeComponent();
            SetupForm();
        }

        /// <summary>
        /// Configura o formulário.
        /// </summary>
        private void SetupForm()
        {
            this.Text = "ClickDesk - Criar Usuário";
            this.Size = new Size(550, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = AppColors.White;

            int y = 20;
            int leftMargin = 40;
            int inputWidth = 450;

            // Título
            Label lblTitle = new Label
            {
                Text = "👤 Criar Novo Usuário",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = AppColors.Primary,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            y += 50;

            // Nome
            AddField("Nome Completo *", ref txtNome, leftMargin, ref y, inputWidth);

            // Username
            AddField("Nome de Usuário *", ref txtUsername, leftMargin, ref y, inputWidth);

            // Email
            AddField("E-mail *", ref txtEmail, leftMargin, ref y, inputWidth);

            // Departamento
            AddField("Departamento", ref txtDepartamento, leftMargin, ref y, inputWidth);

            // Telefone
            AddField("Telefone", ref txtTelefone, leftMargin, ref y, inputWidth);

            // Role
            var lblRole = new Label
            {
                Text = "Papel/Função *",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = AppColors.Gray700,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            this.Controls.Add(lblRole);

            y += 22;

            cmbRole = new ComboBox
            {
                Size = new Size(200, 30),
                Location = new Point(leftMargin, y),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbRole.Items.AddRange(new object[] { "USER", "TECH", "ADMIN" });
            cmbRole.SelectedIndex = 0;
            this.Controls.Add(cmbRole);

            y += 45;

            // Senha
            AddField("Senha *", ref txtSenha, leftMargin, ref y, inputWidth, true);

            // Confirmar Senha
            AddField("Confirmar Senha *", ref txtConfirmarSenha, leftMargin, ref y, inputWidth, true);

            y += 20;

            // Botões
            var btnCancelar = UIHelper.CreateSecondaryButton("Cancelar", 120, 40);
            btnCancelar.Location = new Point(leftMargin + inputWidth - 260, y);
            btnCancelar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancelar);

            btnCriar = UIHelper.CreatePrimaryButton("Criar Usuário", 130, 40);
            btnCriar.Location = new Point(leftMargin + inputWidth - 130, y);
            btnCriar.Click += BtnCriar_Click;
            this.Controls.Add(btnCriar);
        }

        /// <summary>
        /// Adiciona um campo ao formulário.
        /// </summary>
        private void AddField(string label, ref TextBox textBox, int x, ref int y, int width, bool isPassword = false)
        {
            var lbl = new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = AppColors.Gray700,
                Location = new Point(x, y),
                AutoSize = true
            };
            this.Controls.Add(lbl);

            y += 22;

            textBox = new TextBox
            {
                Size = new Size(width, 30),
                Location = new Point(x, y),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                UseSystemPasswordChar = isPassword
            };
            this.Controls.Add(textBox);

            y += 45;
        }

        /// <summary>
        /// Cria o usuário.
        /// </summary>
        private async void BtnCriar_Click(object sender, EventArgs e)
        {
            // Validações
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                UIHelper.ShowError("O nome é obrigatório.");
                txtNome.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                UIHelper.ShowError("O nome de usuário é obrigatório.");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                UIHelper.ShowError("O e-mail é obrigatório.");
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSenha.Text) || txtSenha.Text.Length < 6)
            {
                UIHelper.ShowError("A senha deve ter pelo menos 6 caracteres.");
                txtSenha.Focus();
                return;
            }

            if (txtSenha.Text != txtConfirmarSenha.Text)
            {
                UIHelper.ShowError("As senhas não conferem.");
                txtConfirmarSenha.Focus();
                return;
            }

            // Desabilita form
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
                    Password = txtSenha.Text,
                    ConfirmPassword = txtConfirmarSenha.Text,
                    Role = cmbRole.SelectedItem.ToString()
                };

                await UserService.CriarAsync(request);

                UIHelper.ShowSuccess($"Usuário '{txtUsername.Text}' criado com sucesso!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ApiException ex)
            {
                UIHelper.ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Erro ao criar usuário: {ex.Message}");
            }
            finally
            {
                SetFormEnabled(true);
            }
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
            txtSenha.Enabled = enabled;
            txtConfirmarSenha.Enabled = enabled;
            cmbRole.Enabled = enabled;
            btnCriar.Enabled = enabled;
            btnCriar.Text = enabled ? "Criar Usuário" : "Criando...";
            this.Cursor = enabled ? Cursors.Default : Cursors.WaitCursor;
        }
    }
}

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
    /// Formulário para edição do perfil do usuário.
    /// </summary>
    public partial class FormEditarPerfil : Form
    {
        private User usuario;
        private TextBox txtNome;
        private TextBox txtEmail;
        private TextBox txtDepartamento;
        private TextBox txtTelefone;
        private TextBox txtSenhaAtual;
        private TextBox txtNovaSenha;
        private TextBox txtConfirmarSenha;
        private Button btnSalvar;

        /// <summary>
        /// Construtor do formulário.
        /// </summary>
        /// <param name="user">Usuário a ser editado</param>
        public FormEditarPerfil(User user)
        {
            usuario = user ?? SessionManager.CurrentUser;
            InitializeComponent();
            SetupForm();
        }

        /// <summary>
        /// Configura o formulário.
        /// </summary>
        private void SetupForm()
        {
            this.Text = "ClickDesk - Editar Perfil";
            this.Size = new Size(550, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = ClickDeskColors.White;

            int y = 20;
            int leftMargin = 40;
            int inputWidth = 450;

            // Título
            Label lblTitle = new Label
            {
                Text = "✏️ Editar Perfil",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = ClickDeskColors.Primary,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            y += 50;

            // Nome
            AddField("Nome Completo", ref txtNome, leftMargin, ref y, inputWidth);
            txtNome.Text = usuario?.Nome ?? "";

            // Email
            AddField("E-mail", ref txtEmail, leftMargin, ref y, inputWidth);
            txtEmail.Text = usuario?.Email ?? "";

            // Departamento
            AddField("Departamento", ref txtDepartamento, leftMargin, ref y, inputWidth);
            txtDepartamento.Text = usuario?.Departamento ?? "";

            // Telefone
            AddField("Telefone", ref txtTelefone, leftMargin, ref y, inputWidth);
            txtTelefone.Text = usuario?.Telefone ?? "";

            y += 10;

            // Separador
            var separator = new Panel
            {
                Size = new Size(inputWidth, 1),
                Location = new Point(leftMargin, y),
                BackColor = ClickDeskColors.Gray200
            };
            this.Controls.Add(separator);

            y += 20;

            // Seção de alteração de senha
            Label lblSenha = new Label
            {
                Text = "Alterar Senha (opcional)",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ClickDeskColors.Gray700,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            this.Controls.Add(lblSenha);

            y += 30;

            // Senha atual
            AddField("Senha Atual", ref txtSenhaAtual, leftMargin, ref y, inputWidth, true);

            // Nova senha
            AddField("Nova Senha", ref txtNovaSenha, leftMargin, ref y, inputWidth, true);

            // Confirmar senha
            AddField("Confirmar Nova Senha", ref txtConfirmarSenha, leftMargin, ref y, inputWidth, true);

            y += 20;

            // Botões
            var btnCancelar = UIHelper.CreateSecondaryButton("Cancelar", 120, 40);
            btnCancelar.Location = new Point(leftMargin + inputWidth - 260, y);
            btnCancelar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancelar);

            btnSalvar = UIHelper.CreatePrimaryButton("Salvar", 120, 40);
            btnSalvar.Location = new Point(leftMargin + inputWidth - 120, y);
            btnSalvar.Click += BtnSalvar_Click;
            this.Controls.Add(btnSalvar);
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
                ForeColor = ClickDeskColors.Gray700,
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
        /// Salva as alterações do perfil.
        /// </summary>
        private async void BtnSalvar_Click(object sender, EventArgs e)
        {
            // Validações
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                UIHelper.ShowError("O nome é obrigatório.");
                txtNome.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                UIHelper.ShowError("O e-mail é obrigatório.");
                txtEmail.Focus();
                return;
            }

            // Validação de alteração de senha
            bool alterarSenha = !string.IsNullOrEmpty(txtNovaSenha.Text);
            if (alterarSenha)
            {
                if (string.IsNullOrEmpty(txtSenhaAtual.Text))
                {
                    UIHelper.ShowError("Informe a senha atual para alterar a senha.");
                    txtSenhaAtual.Focus();
                    return;
                }

                if (txtNovaSenha.Text.Length < 6)
                {
                    UIHelper.ShowError("A nova senha deve ter pelo menos 6 caracteres.");
                    txtNovaSenha.Focus();
                    return;
                }

                if (txtNovaSenha.Text != txtConfirmarSenha.Text)
                {
                    UIHelper.ShowError("As senhas não conferem.");
                    txtConfirmarSenha.Focus();
                    return;
                }
            }

            // Desabilita form
            SetFormEnabled(false);

            try
            {
                // Atualiza perfil
                var perfilAtualizado = new User
                {
                    Id = usuario.Id,
                    Username = usuario.Username,
                    Nome = txtNome.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Departamento = txtDepartamento.Text.Trim(),
                    Telefone = txtTelefone.Text.Trim(),
                    Role = usuario.Role
                };

                await UserService.AtualizarPerfilAsync(perfilAtualizado);

                // Altera senha se solicitado
                if (alterarSenha)
                {
                    await UserService.AlterarSenhaAsync(txtSenhaAtual.Text, txtNovaSenha.Text);
                }

                // Atualiza sessão
                SessionManager.UpdateUser(perfilAtualizado);

                UIHelper.ShowSuccess("Perfil atualizado com sucesso!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ApiException ex)
            {
                UIHelper.ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Erro ao salvar: {ex.Message}");
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
            txtEmail.Enabled = enabled;
            txtDepartamento.Enabled = enabled;
            txtTelefone.Enabled = enabled;
            txtSenhaAtual.Enabled = enabled;
            txtNovaSenha.Enabled = enabled;
            txtConfirmarSenha.Enabled = enabled;
            btnSalvar.Enabled = enabled;
            btnSalvar.Text = enabled ? "Salvar" : "Salvando...";
            this.Cursor = enabled ? Cursors.Default : Cursors.WaitCursor;
        }
    }
}

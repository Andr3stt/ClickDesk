using ClickDesk.Utils;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickDesk.Models;
using ClickDesk.Services.API;
using Siticone.Desktop.UI.WinForms;

namespace ClickDesk.Forms.Auth
{
    /// <summary>
    /// Formulário de registro de novos usuários.
    /// Permite criar conta com validação de dados.
    /// </summary>
    public partial class FormRegistro : Form
    {
        /// <summary>
        /// Construtor do formulário de registro.
        /// </summary>
        public FormRegistro()
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
        }

        /// <summary>
        /// Realiza o processo de registro.
        /// </summary>
        private async void BtnCriarConta_Click(object sender, EventArgs e)
        {
            await RealizarRegistro();
        }

        /// <summary>
        /// Realiza o processo de registro.
        /// </summary>
        private async Task RealizarRegistro()
        {
            // Validação dos campos
            if (string.IsNullOrWhiteSpace(this.txtNome.Text))
            {
                MessageBox.Show("Por favor, informe seu nome.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtNome.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(this.txtSobrenome.Text))
            {
                MessageBox.Show("Por favor, informe seu sobrenome.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtSobrenome.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(this.txtEmail.Text) || !IsValidEmail(this.txtEmail.Text))
            {
                MessageBox.Show("Por favor, informe um e-mail válido.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(this.txtTelefone.Text))
            {
                MessageBox.Show("Por favor, informe um telefone.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtTelefone.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(this.txtSenha.Text) || this.txtSenha.Text.Length < 6)
            {
                MessageBox.Show("A senha deve ter pelo menos 6 caracteres.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtSenha.Focus();
                return;
            }

            if (this.txtSenha.Text != this.txtConfirmSenha.Text)
            {
                MessageBox.Show("As senhas não conferem.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtConfirmSenha.Focus();
                return;
            }

            if (!this.chkTermos.Checked)
            {
                MessageBox.Show("Você deve concordar com os Termos de Serviço.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Desabilita o botão
            this.btnCriarConta.Enabled = false;
            this.btnCriarConta.Text = "Criando...";

            try
            {
                var request = new RegisterRequest
                {
                    // Nome completo juntando nome + sobrenome
                    Nome = $"{this.txtNome.Text.Trim()} {this.txtSobrenome.Text.Trim()}",

                    // Login / usuário será o próprio e-mail
                    Username = this.txtEmail.Text.Trim(),
                    Email = this.txtEmail.Text.Trim(),

                    // Por enquanto deixa departamento em branco
                    Departamento = string.Empty,

                    Telefone = this.txtTelefone.Text.Trim(),

                    Password = this.txtSenha.Text,
                    ConfirmPassword = this.txtConfirmSenha.Text,

                    Role = "USER"
                };

                var response = await RegisterService.RegisterAsync(request.Nome, request.Sobrenome, request.Email);

                if (response != null && response.Success)
                {
                    MessageBox.Show("Conta criada com sucesso! Você já pode fazer login.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(response?.Message ?? "Erro ao criar conta. Tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ApiException ex)
            {
                MessageBox.Show($"Erro de API: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao conectar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.btnCriarConta.Enabled = true;
                this.btnCriarConta.Text = "Criar Conta";
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
        /// Alterna visibilidade da senha.
        /// </summary>
        private void BtnToggleSenha_Click(object sender, EventArgs e)
        {
            if (this.txtSenha.UseSystemPasswordChar)
            {
                this.txtSenha.UseSystemPasswordChar = false;
                this.btnToggleSenha.Text = "👁‍🗨";
            }
            else
            {
                this.txtSenha.UseSystemPasswordChar = true;
                this.btnToggleSenha.Text = "👁";
            }
        }

        /// <summary>
        /// Abre o formulário de login.
        /// </summary>
        private void LinkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            var frm = new FormLogin();
            frm.FormClosed += (s, args) => this.Show();
            frm.Show();
        }

        private void AbrirTermos()
        {
            var termos = new FormTermosUso();
            this.Hide();
            termos.ShowDialog();
            this.Show();

            if (termos.TermosAceitos)
                chkTermos.Checked = true;
        }

        private void BtnFazerLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            var frm = new FormLogin();
            frm.FormClosed += (s, args) => this.Show();
            frm.Show();
        }
    }
}
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
    /// Formulário para visualização do perfil do usuário.
    /// </summary>
    public partial class FormPerfil : Form
    {
        private User usuario;

        /// <summary>
        /// Construtor do formulário.
        /// </summary>
        public FormPerfil()
        {
            InitializeComponent();
            SetupForm();
        }

        /// <summary>
        /// Configura o formulário.
        /// </summary>
        private void SetupForm()
        {
            this.Text = "ClickDesk - Meu Perfil";
            this.Size = new Size(600, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = AppColors.White;

            int y = 30;
            int leftMargin = 40;

            // Título
            Label lblTitle = new Label
            {
                Text = "👤 Meu Perfil",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = AppColors.Primary,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            y += 60;

            // Avatar/Iniciais
            Panel avatarPanel = new Panel
            {
                Size = new Size(80, 80),
                Location = new Point(leftMargin, y),
                BackColor = AppColors.Primary
            };
            this.Controls.Add(avatarPanel);

            Label lblAvatar = new Label
            {
                Name = "lblAvatar",
                Text = "U",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = AppColors.White,
                Size = new Size(80, 80),
                TextAlign = ContentAlignment.MiddleCenter
            };
            avatarPanel.Controls.Add(lblAvatar);

            // Nome
            Label lblNome = new Label
            {
                Name = "lblNome",
                Text = "Carregando...",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = AppColors.TextPrimary,
                Location = new Point(leftMargin + 100, y + 10),
                AutoSize = true
            };
            this.Controls.Add(lblNome);

            // Role badge
            Label lblRole = new Label
            {
                Name = "lblRole",
                Text = "USER",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = AppColors.White,
                BackColor = AppColors.Primary,
                Location = new Point(leftMargin + 100, y + 50),
                Size = new Size(60, 22),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblRole);

            y += 110;

            // Linha separadora
            Panel separator = new Panel
            {
                Size = new Size(500, 1),
                Location = new Point(leftMargin, y),
                BackColor = AppColors.Gray200
            };
            this.Controls.Add(separator);

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
            var btnEditar = UIHelper.CreatePrimaryButton("✏️ Editar Perfil", 140, 40);
            btnEditar.Location = new Point(leftMargin, y);
            btnEditar.Click += BtnEditar_Click;
            this.Controls.Add(btnEditar);

            var btnFechar = UIHelper.CreateSecondaryButton("Fechar", 100, 40);
            btnFechar.Location = new Point(leftMargin + 380, y);
            btnFechar.Click += (s, e) => this.Close();
            this.Controls.Add(btnFechar);

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
                Font = new Font("Segoe UI", 10),
                ForeColor = AppColors.Gray500,
                Location = new Point(x, y),
                Size = new Size(120, 20)
            };
            this.Controls.Add(lblLabel);

            var lblValue = new Label
            {
                Name = valueName,
                Text = "-",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = AppColors.TextPrimary,
                Location = new Point(x + 130, y),
                AutoSize = true
            };
            this.Controls.Add(lblValue);
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
                    usuario = await UserService.ObterPerfilAsync();
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
            // Avatar
            var avatarPanel = this.Controls[1] as Panel;
            var lblAvatar = avatarPanel.Controls["lblAvatar"] as Label;
            if (!string.IsNullOrEmpty(usuario.Nome))
            {
                lblAvatar.Text = usuario.Nome.Substring(0, 1).ToUpper();
            }

            // Informações
            var lblNome = this.Controls["lblNome"] as Label;
            var lblRole = this.Controls["lblRole"] as Label;
            var lblUsername = this.Controls["lblUsername"] as Label;
            var lblEmail = this.Controls["lblEmail"] as Label;
            var lblDepartamento = this.Controls["lblDepartamento"] as Label;
            var lblTelefone = this.Controls["lblTelefone"] as Label;
            var lblDataCriacao = this.Controls["lblDataCriacao"] as Label;

            lblNome.Text = usuario.Nome ?? usuario.Username;
            lblRole.Text = usuario.Role ?? "USER";
            lblRole.BackColor = AppColors.GetRoleColor(usuario.Role);

            lblUsername.Text = usuario.Username ?? "-";
            lblEmail.Text = usuario.Email ?? "-";
            lblDepartamento.Text = usuario.Departamento ?? "-";
            lblTelefone.Text = usuario.Telefone ?? "-";
            lblDataCriacao.Text = usuario.DataCriacao?.ToString("dd/MM/yyyy") ?? "-";
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

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickDesk.Services.API;
using ClickDesk.Utils;

namespace ClickDesk.Forms.Auth
{
    /// <summary>
    /// Formulário de recuperação de senha.
    /// Permite ao usuário solicitar recuperação de senha via email.
    /// </summary>
    public partial class FormRecuperarSenha : Form
    {
        // Componentes do formulário
        private Panel panelPrincipal;
        private TextBox txtEmail;
        private TextBox txtCodigo;
        private TextBox txtNovaSenha;
        private TextBox txtConfirmarSenha;
        private Button btnEnviarCodigo;
        private Button btnConfirmar;
        private Button btnVoltar;
        private Label lblMensagem;
        private Panel panelEtapa1;
        private Panel panelEtapa2;
        
        // Estado do formulário
        private int etapaAtual = 1;
        private string emailSolicitacao;

        /// <summary>
        /// Construtor do formulário de recuperação de senha.
        /// </summary>
        public FormRecuperarSenha()
        {
            InitializeComponent();
            CriarInterface();
        }

        /// <summary>
        /// Cria a interface do formulário de recuperação de senha.
        /// </summary>
        private void CriarInterface()
        {
            // Configurações do formulário
            this.Text = "ClickDesk - Recuperar Senha";
            this.Size = new Size(550, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = ClickDeskColors.BackgroundApp;

            // Painel principal
            panelPrincipal = new Panel
            {
                Size = new Size(450, 420),
                Location = new Point((this.ClientSize.Width - 450) / 2, 20),
                BackColor = ClickDeskColors.White
            };
            panelPrincipal.Paint += PanelPrincipal_Paint;
            this.Controls.Add(panelPrincipal);

            // Criar as duas etapas
            CriarEtapa1();
            CriarEtapa2();

            // Mostrar apenas etapa 1 inicialmente
            panelEtapa1.Visible = true;
            panelEtapa2.Visible = false;
        }

        /// <summary>
        /// Cria os controles da Etapa 1 (Solicitar código por email).
        /// </summary>
        private void CriarEtapa1()
        {
            panelEtapa1 = new Panel
            {
                Size = panelPrincipal.Size,
                Location = new Point(0, 0),
                BackColor = Color.Transparent
            };
            panelPrincipal.Controls.Add(panelEtapa1);

            int y = 30;
            int leftMargin = 50;
            int inputWidth = 350;

            // Ícone de cadeado
            Label lblIcone = new Label
            {
                Text = "🔑",
                Font = new Font("Segoe UI", 40),
                Location = new Point(leftMargin + (inputWidth - 80) / 2, y),
                Size = new Size(80, 60),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelEtapa1.Controls.Add(lblIcone);

            y += 70;

            // Título
            Label lblTitulo = new Label
            {
                Text = "Recuperar Senha",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(leftMargin, y),
                Size = new Size(inputWidth, 35),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelEtapa1.Controls.Add(lblTitulo);

            y += 45;

            // Subtítulo
            Label lblSubtitulo = new Label
            {
                Text = "Digite seu email para receber um código de verificação.",
                Font = new Font("Segoe UI", 10),
                ForeColor = ClickDeskColors.TextSecondary,
                Location = new Point(leftMargin, y),
                Size = new Size(inputWidth, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelEtapa1.Controls.Add(lblSubtitulo);

            y += 50;

            // Campo de email
            Label lblEmail = new Label
            {
                Text = "Email",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            panelEtapa1.Controls.Add(lblEmail);

            y += 25;

            txtEmail = new TextBox
            {
                Location = new Point(leftMargin, y),
                Size = new Size(inputWidth, 35),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle
            };
            txtEmail.KeyPress += (s, e) => { if (e.KeyChar == (char)Keys.Enter) BtnEnviarCodigo_Click(s, e); };
            panelEtapa1.Controls.Add(txtEmail);

            y += 50;

            // Mensagem de feedback
            lblMensagem = new Label
            {
                Location = new Point(leftMargin, y),
                Size = new Size(inputWidth, 40),
                Font = new Font("Segoe UI", 9),
                ForeColor = ClickDeskColors.Danger,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };
            panelEtapa1.Controls.Add(lblMensagem);

            y += 50;

            // Botão Enviar Código
            btnEnviarCodigo = new Button
            {
                Text = "ENVIAR CÓDIGO",
                Size = new Size(inputWidth, 45),
                Location = new Point(leftMargin, y),
                BackColor = ClickDeskColors.Brand,
                ForeColor = ClickDeskColors.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnEnviarCodigo.FlatAppearance.BorderSize = 0;
            btnEnviarCodigo.Click += BtnEnviarCodigo_Click;
            panelEtapa1.Controls.Add(btnEnviarCodigo);

            y += 60;

            // Link Voltar ao Login
            LinkLabel linkVoltar = new LinkLabel
            {
                Text = "← Voltar ao Login",
                Font = new Font("Segoe UI", 10),
                LinkColor = ClickDeskColors.Brand,
                Location = new Point(leftMargin + (inputWidth - 100) / 2, y),
                AutoSize = true
            };
            linkVoltar.LinkClicked += (s, e) => this.Close();
            panelEtapa1.Controls.Add(linkVoltar);
        }

        /// <summary>
        /// Cria os controles da Etapa 2 (Inserir código e nova senha).
        /// </summary>
        private void CriarEtapa2()
        {
            panelEtapa2 = new Panel
            {
                Size = panelPrincipal.Size,
                Location = new Point(0, 0),
                BackColor = Color.Transparent
            };
            panelPrincipal.Controls.Add(panelEtapa2);

            int y = 20;
            int leftMargin = 50;
            int inputWidth = 350;

            // Título
            Label lblTitulo = new Label
            {
                Text = "Redefinir Senha",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(leftMargin, y),
                Size = new Size(inputWidth, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelEtapa2.Controls.Add(lblTitulo);

            y += 35;

            // Subtítulo
            Label lblSubtitulo = new Label
            {
                Text = "Insira o código recebido e sua nova senha.",
                Font = new Font("Segoe UI", 10),
                ForeColor = ClickDeskColors.TextSecondary,
                Location = new Point(leftMargin, y),
                Size = new Size(inputWidth, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelEtapa2.Controls.Add(lblSubtitulo);

            y += 40;

            // Campo de código
            Label lblCodigo = new Label
            {
                Text = "Código de Verificação",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            panelEtapa2.Controls.Add(lblCodigo);

            y += 25;

            txtCodigo = new TextBox
            {
                Location = new Point(leftMargin, y),
                Size = new Size(inputWidth, 35),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = HorizontalAlignment.Center,
                MaxLength = 6
            };
            panelEtapa2.Controls.Add(txtCodigo);

            y += 50;

            // Campo de nova senha
            Label lblNovaSenha = new Label
            {
                Text = "Nova Senha",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            panelEtapa2.Controls.Add(lblNovaSenha);

            y += 25;

            txtNovaSenha = new TextBox
            {
                Location = new Point(leftMargin, y),
                Size = new Size(inputWidth, 35),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle,
                UseSystemPasswordChar = true
            };
            panelEtapa2.Controls.Add(txtNovaSenha);

            y += 50;

            // Campo confirmar senha
            Label lblConfirmar = new Label
            {
                Text = "Confirmar Nova Senha",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            panelEtapa2.Controls.Add(lblConfirmar);

            y += 25;

            txtConfirmarSenha = new TextBox
            {
                Location = new Point(leftMargin, y),
                Size = new Size(inputWidth, 35),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle,
                UseSystemPasswordChar = true
            };
            panelEtapa2.Controls.Add(txtConfirmarSenha);

            y += 50;

            // Botão Voltar
            btnVoltar = new Button
            {
                Text = "← Voltar",
                Size = new Size(100, 45),
                Location = new Point(leftMargin, y),
                BackColor = ClickDeskColors.Gray200,
                ForeColor = ClickDeskColors.Gray700,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnVoltar.FlatAppearance.BorderSize = 0;
            btnVoltar.Click += BtnVoltar_Click;
            panelEtapa2.Controls.Add(btnVoltar);

            // Botão Confirmar
            btnConfirmar = new Button
            {
                Text = "REDEFINIR SENHA",
                Size = new Size(230, 45),
                Location = new Point(leftMargin + 120, y),
                BackColor = ClickDeskColors.Brand,
                ForeColor = ClickDeskColors.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnConfirmar.FlatAppearance.BorderSize = 0;
            btnConfirmar.Click += BtnConfirmar_Click;
            panelEtapa2.Controls.Add(btnConfirmar);
        }

        /// <summary>
        /// Desenha bordas arredondadas no painel principal.
        /// </summary>
        private void PanelPrincipal_Paint(object sender, PaintEventArgs e)
        {
            var rect = new Rectangle(0, 0, panelPrincipal.Width - 1, panelPrincipal.Height - 1);
            var path = ClickDeskStyles.GetRoundedRectangle(rect, ClickDeskStyles.RadiusXL);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (var brush = new SolidBrush(ClickDeskColors.White))
            {
                e.Graphics.FillPath(brush, path);
            }

            using (var pen = new Pen(ClickDeskColors.Border, 1))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        /// <summary>
        /// Evento de clique no botão Enviar Código.
        /// </summary>
        private async void BtnEnviarCodigo_Click(object sender, EventArgs e)
        {
            await EnviarCodigoRecuperacao();
        }

        /// <summary>
        /// Envia o código de recuperação para o email informado.
        /// </summary>
        private async Task EnviarCodigoRecuperacao()
        {
            lblMensagem.Visible = false;

            // Validação de email
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MostrarMensagem("Por favor, informe seu email.", true);
                txtEmail.Focus();
                return;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MostrarMensagem("Por favor, informe um email válido.", true);
                txtEmail.Focus();
                return;
            }

            // Desabilita form
            SetFormEnabled(false, 1);

            try
            {
                // TODO: Integrar com /auth/forgot-password
                // Por enquanto, simula a requisição
                await Task.Delay(1500);

                // Salva email para usar na etapa 2
                emailSolicitacao = txtEmail.Text.Trim();

                // Avança para etapa 2
                MostrarEtapa(2);

                UIHelper.ShowSuccess(
                    "Se o email estiver cadastrado, você receberá um código de verificação.\n\n" +
                    "Verifique sua caixa de entrada e spam.",
                    "Código Enviado"
                );
            }
            catch (ApiException ex)
            {
                MostrarMensagem(ex.Message, true);
            }
            catch (Exception ex)
            {
                MostrarMensagem($"Erro ao enviar código: {ex.Message}", true);
            }
            finally
            {
                SetFormEnabled(true, 1);
            }
        }

        /// <summary>
        /// Evento de clique no botão Voltar.
        /// </summary>
        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            MostrarEtapa(1);
        }

        /// <summary>
        /// Evento de clique no botão Confirmar.
        /// </summary>
        private async void BtnConfirmar_Click(object sender, EventArgs e)
        {
            await RedefinirSenha();
        }

        /// <summary>
        /// Redefine a senha do usuário.
        /// </summary>
        private async Task RedefinirSenha()
        {
            // Validações
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                UIHelper.ShowError("Por favor, informe o código de verificação.");
                txtCodigo.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNovaSenha.Text) || txtNovaSenha.Text.Length < 6)
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

            // Desabilita form
            SetFormEnabled(false, 2);

            try
            {
                // TODO: Integrar com /auth/reset-password
                // var request = new ResetPasswordRequest
                // {
                //     Email = emailSolicitacao,
                //     Codigo = txtCodigo.Text.Trim(),
                //     NovaSenha = txtNovaSenha.Text
                // };
                // await AuthService.ResetPasswordAsync(request);

                // Por enquanto, simula a requisição
                await Task.Delay(1500);

                UIHelper.ShowSuccess(
                    "Sua senha foi redefinida com sucesso!\n\nVocê já pode fazer login com a nova senha.",
                    "Senha Alterada"
                );

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ApiException ex)
            {
                UIHelper.ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Erro ao redefinir senha: {ex.Message}");
            }
            finally
            {
                SetFormEnabled(true, 2);
            }
        }

        /// <summary>
        /// Mostra uma etapa específica do formulário.
        /// </summary>
        private void MostrarEtapa(int etapa)
        {
            etapaAtual = etapa;
            panelEtapa1.Visible = (etapa == 1);
            panelEtapa2.Visible = (etapa == 2);

            if (etapa == 2)
            {
                txtCodigo.Focus();
            }
            else
            {
                txtEmail.Focus();
            }
        }

        /// <summary>
        /// Mostra uma mensagem de feedback.
        /// </summary>
        private void MostrarMensagem(string mensagem, bool erro = false)
        {
            lblMensagem.Text = mensagem;
            lblMensagem.ForeColor = erro ? ClickDeskColors.Danger : ClickDeskColors.Success;
            lblMensagem.Visible = true;
        }

        /// <summary>
        /// Habilita/desabilita os controles do formulário.
        /// </summary>
        private void SetFormEnabled(bool enabled, int etapa)
        {
            if (etapa == 1)
            {
                txtEmail.Enabled = enabled;
                btnEnviarCodigo.Enabled = enabled;
                btnEnviarCodigo.Text = enabled ? "ENVIAR CÓDIGO" : "AGUARDE...";
                btnEnviarCodigo.BackColor = enabled ? ClickDeskColors.Brand : ClickDeskColors.Gray400;
            }
            else
            {
                txtCodigo.Enabled = enabled;
                txtNovaSenha.Enabled = enabled;
                txtConfirmarSenha.Enabled = enabled;
                btnVoltar.Enabled = enabled;
                btnConfirmar.Enabled = enabled;
                btnConfirmar.Text = enabled ? "REDEFINIR SENHA" : "AGUARDE...";
                btnConfirmar.BackColor = enabled ? ClickDeskColors.Brand : ClickDeskColors.Gray400;
            }

            this.Cursor = enabled ? Cursors.Default : Cursors.WaitCursor;
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
    }
}

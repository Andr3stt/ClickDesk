using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickDesk.Services.API;
using ClickDesk.Utils;
using Siticone.Desktop.UI.WinForms;

namespace ClickDesk.Forms.Auth
{
    /// <summary>
    /// Formulário de recuperação de senha.
    /// Permite ao usuário solicitar recuperação de senha via email.
    /// </summary>
    public partial class FormRecuperarSenha : Form
    {
        // Componentes do formulário
        private SiticonePanel panelPrincipal;
        private SiticoneTextBox txtEmail;
        private SiticoneTextBox txtCodigo;
        private SiticoneTextBox txtNovaSenha;
        private SiticoneTextBox txtConfirmarSenha;
        private SiticoneButton btnEnviarCodigo;
        private SiticoneButton btnConfirmar;
        private SiticoneButton btnVoltar;
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
            this.Size = new Size(550, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
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

            // Painel principal com Siticone
            panelPrincipal = new SiticonePanel
            {
                Size = new Size(480, 460),
                Location = new Point((this.Width - 480) / 2, 30),
                FillColor = ThemeManager.CardBackground,
                BorderRadius = ClickDeskStyles.RadiusXL
            };
            panelPrincipal.ShadowDecoration.Enabled = true;
            panelPrincipal.ShadowDecoration.Depth = 20;
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
            int leftMargin = 65;
            int inputWidth = 350;

            // Ícone de cadeado
            Label lblIcone = new Label
            {
                Text = "🔑",
                Font = new Font("Segoe UI", 40),
                Location = new Point(leftMargin + (inputWidth - 80) / 2, y),
                Size = new Size(80, 60),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            panelEtapa1.Controls.Add(lblIcone);

            y += 70;

            // Título
            Label lblTitulo = new Label
            {
                Text = "Recuperar Senha",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = ThemeManager.TextPrimary,
                Location = new Point(leftMargin, y),
                Size = new Size(inputWidth, 35),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            panelEtapa1.Controls.Add(lblTitulo);

            y += 45;

            // Subtítulo
            Label lblSubtitulo = new Label
            {
                Text = "Digite seu email para receber um código de verificação.",
                Font = ClickDeskStyles.FontBase,
                ForeColor = ThemeManager.TextSecondary,
                Location = new Point(leftMargin, y),
                Size = new Size(inputWidth, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            panelEtapa1.Controls.Add(lblSubtitulo);

            y += 50;

            // Campo de email
            Label lblEmail = new Label
            {
                Text = "Email",
                Font = ClickDeskStyles.FontBaseStrong,
                ForeColor = ThemeManager.TextPrimary,
                Location = new Point(leftMargin, y),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            panelEtapa1.Controls.Add(lblEmail);

            y += 25;

            txtEmail = new SiticoneTextBox
            {
                Location = new Point(leftMargin, y),
                Size = new Size(inputWidth, 45),
                Font = ClickDeskStyles.FontBase,
                BorderRadius = ClickDeskStyles.RadiusSM,
                BorderThickness = 1,
                BorderColor = ThemeManager.Border,
                FillColor = ThemeManager.CardBackground,
                ForeColor = ThemeManager.TextPrimary,
                PlaceholderText = "seu.email@exemplo.com",
                TextOffset = new Point(10, 0)
            };
            txtEmail.KeyPress += (s, e) => { if (e.KeyChar == (char)Keys.Enter) BtnEnviarCodigo_Click(s, e); };
            panelEtapa1.Controls.Add(txtEmail);

            y += 55;

            // Mensagem de feedback
            lblMensagem = new Label
            {
                Location = new Point(leftMargin, y),
                Size = new Size(inputWidth, 40),
                Font = ClickDeskStyles.FontSM,
                ForeColor = ClickDeskColors.Danger,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false,
                BackColor = Color.Transparent
            };
            panelEtapa1.Controls.Add(lblMensagem);

            y += 50;

            // Botão Enviar Código
            btnEnviarCodigo = new SiticoneButton
            {
                Text = "ENVIAR CÓDIGO",
                Size = new Size(inputWidth, 50),
                Location = new Point(leftMargin, y),
                BorderRadius = ClickDeskStyles.RadiusMD,
                FillColor = ThemeManager.Brand,
                ForeColor = Color.White,
                Font = ClickDeskStyles.FontLG,
                Cursor = Cursors.Hand,
                HoverState = { FillColor = ThemeManager.BrandHover }
            };
            btnEnviarCodigo.Click += BtnEnviarCodigo_Click;
            panelEtapa1.Controls.Add(btnEnviarCodigo);

            y += 60;

            // Link Voltar ao Login
            LinkLabel linkVoltar = new LinkLabel
            {
                Text = "← Voltar ao Login",
                Font = ClickDeskStyles.FontBase,
                LinkColor = ThemeManager.Brand,
                Location = new Point(leftMargin + (inputWidth - 120) / 2, y),
                AutoSize = true,
                BackColor = Color.Transparent
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

            int y = 30;
            int leftMargin = 65;
            int inputWidth = 350;

            // Título
            Label lblTitulo = new Label
            {
                Text = "Redefinir Senha",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = ThemeManager.TextPrimary,
                Location = new Point(leftMargin, y),
                Size = new Size(inputWidth, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            panelEtapa2.Controls.Add(lblTitulo);

            y += 40;

            // Subtítulo
            Label lblSubtitulo = new Label
            {
                Text = "Insira o código recebido e sua nova senha.",
                Font = ClickDeskStyles.FontBase,
                ForeColor = ThemeManager.TextSecondary,
                Location = new Point(leftMargin, y),
                Size = new Size(inputWidth, 25),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            panelEtapa2.Controls.Add(lblSubtitulo);

            y += 40;

            // Campo de código
            Label lblCodigo = new Label
            {
                Text = "Código de Verificação",
                Font = ClickDeskStyles.FontBaseStrong,
                ForeColor = ThemeManager.TextPrimary,
                Location = new Point(leftMargin, y),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            panelEtapa2.Controls.Add(lblCodigo);

            y += 25;

            txtCodigo = new SiticoneTextBox
            {
                Location = new Point(leftMargin, y),
                Size = new Size(inputWidth, 45),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                BorderRadius = ClickDeskStyles.RadiusSM,
                BorderThickness = 1,
                BorderColor = ThemeManager.Border,
                FillColor = ThemeManager.CardBackground,
                ForeColor = ThemeManager.TextPrimary,
                PlaceholderText = "000000",
                TextOffset = new Point(10, 0),
                MaxLength = 6
            };
            panelEtapa2.Controls.Add(txtCodigo);

            y += 55;

            // Campo de nova senha
            Label lblNovaSenha = new Label
            {
                Text = "Nova Senha",
                Font = ClickDeskStyles.FontBaseStrong,
                ForeColor = ThemeManager.TextPrimary,
                Location = new Point(leftMargin, y),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            panelEtapa2.Controls.Add(lblNovaSenha);

            y += 25;

            txtNovaSenha = new SiticoneTextBox
            {
                Location = new Point(leftMargin, y),
                Size = new Size(inputWidth, 45),
                Font = ClickDeskStyles.FontBase,
                BorderRadius = ClickDeskStyles.RadiusSM,
                BorderThickness = 1,
                BorderColor = ThemeManager.Border,
                FillColor = ThemeManager.CardBackground,
                ForeColor = ThemeManager.TextPrimary,
                PasswordChar = '●',
                PlaceholderText = "Digite sua nova senha",
                TextOffset = new Point(10, 0)
            };
            panelEtapa2.Controls.Add(txtNovaSenha);

            y += 55;

            // Campo confirmar senha
            Label lblConfirmar = new Label
            {
                Text = "Confirmar Nova Senha",
                Font = ClickDeskStyles.FontBaseStrong,
                ForeColor = ThemeManager.TextPrimary,
                Location = new Point(leftMargin, y),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            panelEtapa2.Controls.Add(lblConfirmar);

            y += 25;

            txtConfirmarSenha = new SiticoneTextBox
            {
                Location = new Point(leftMargin, y),
                Size = new Size(inputWidth, 45),
                Font = ClickDeskStyles.FontBase,
                BorderRadius = ClickDeskStyles.RadiusSM,
                BorderThickness = 1,
                BorderColor = ThemeManager.Border,
                FillColor = ThemeManager.CardBackground,
                ForeColor = ThemeManager.TextPrimary,
                PasswordChar = '●',
                PlaceholderText = "Digite a senha novamente",
                TextOffset = new Point(10, 0)
            };
            panelEtapa2.Controls.Add(txtConfirmarSenha);

            y += 60;

            // Botão Voltar
            btnVoltar = new SiticoneButton
            {
                Text = "← Voltar",
                Size = new Size(100, 45),
                Location = new Point(leftMargin, y),
                BorderRadius = ClickDeskStyles.RadiusMD,
                FillColor = ClickDeskColors.Gray300,
                ForeColor = ClickDeskColors.Gray700,
                Font = ClickDeskStyles.FontBase,
                Cursor = Cursors.Hand,
                HoverState = { FillColor = ClickDeskColors.Gray400 }
            };
            btnVoltar.Click += BtnVoltar_Click;
            panelEtapa2.Controls.Add(btnVoltar);

            // Botão Confirmar
            btnConfirmar = new SiticoneButton
            {
                Text = "REDEFINIR SENHA",
                Size = new Size(230, 45),
                Location = new Point(leftMargin + 120, y),
                BorderRadius = ClickDeskStyles.RadiusMD,
                FillColor = ThemeManager.Brand,
                ForeColor = Color.White,
                Font = ClickDeskStyles.FontLG,
                Cursor = Cursors.Hand,
                HoverState = { FillColor = ThemeManager.BrandHover }
            };
            btnConfirmar.Click += BtnConfirmar_Click;
            panelEtapa2.Controls.Add(btnConfirmar);
        }

        /// <summary>
        /// Aplica o tema atual aos controles do formulário.
        /// </summary>
        private void ApplyTheme()
        {
            panelPrincipal.FillColor = ThemeManager.CardBackground;
            
            if (panelEtapa1.Visible)
            {
                txtEmail.FillColor = ThemeManager.CardBackground;
                txtEmail.ForeColor = ThemeManager.TextPrimary;
                txtEmail.BorderColor = ThemeManager.Border;
                btnEnviarCodigo.FillColor = ThemeManager.Brand;
                btnEnviarCodigo.HoverState.FillColor = ThemeManager.BrandHover;
            }
            
            if (panelEtapa2.Visible)
            {
                txtCodigo.FillColor = ThemeManager.CardBackground;
                txtCodigo.ForeColor = ThemeManager.TextPrimary;
                txtCodigo.BorderColor = ThemeManager.Border;
                txtNovaSenha.FillColor = ThemeManager.CardBackground;
                txtNovaSenha.ForeColor = ThemeManager.TextPrimary;
                txtNovaSenha.BorderColor = ThemeManager.Border;
                txtConfirmarSenha.FillColor = ThemeManager.CardBackground;
                txtConfirmarSenha.ForeColor = ThemeManager.TextPrimary;
                txtConfirmarSenha.BorderColor = ThemeManager.Border;
                btnConfirmar.FillColor = ThemeManager.Brand;
                btnConfirmar.HoverState.FillColor = ThemeManager.BrandHover;
            }

            // Update all labels
            foreach (Control panel in panelPrincipal.Controls)
            {
                foreach (Control control in panel.Controls)
                {
                    if (control is Label label && label != lblMensagem)
                    {
                        if (label.Font.Bold)
                        {
                            label.ForeColor = ThemeManager.TextPrimary;
                        }
                        else
                        {
                            label.ForeColor = ThemeManager.TextSecondary;
                        }
                    }
                    else if (control is LinkLabel link)
                    {
                        link.LinkColor = ThemeManager.Brand;
                    }
                }
            }

            panelPrincipal.Invalidate();
            this.Invalidate();
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
                btnEnviarCodigo.FillColor = enabled ? ThemeManager.Brand : ClickDeskColors.Gray400;
            }
            else
            {
                txtCodigo.Enabled = enabled;
                txtNovaSenha.Enabled = enabled;
                txtConfirmarSenha.Enabled = enabled;
                btnVoltar.Enabled = enabled;
                btnConfirmar.Enabled = enabled;
                btnConfirmar.Text = enabled ? "REDEFINIR SENHA" : "AGUARDE...";
                btnConfirmar.FillColor = enabled ? ThemeManager.Brand : ClickDeskColors.Gray400;
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

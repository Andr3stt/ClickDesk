using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickDesk.Models;
using ClickDesk.Services.API;
using ClickDesk.Utils;
using Siticone.Desktop.UI.WinForms;

namespace ClickDesk.Forms.Chamados
{
    /// <summary>
    /// Formulário para criação de novo chamado.
    /// Integra com a IA para resolução automática de problemas.
    /// </summary>
    public partial class FormNovoChamado : Form
    {
        // Campos do formulário
        private SiticonePanel mainPanel;
        private SiticoneTextBox txtTitulo;
        private SiticoneTextBox txtDescricao;
        private SiticoneComboBox cmbCategoria;
        private SiticoneComboBox cmbSeveridade;
        private SiticoneButton btnEnviar;
        private SiticoneButton btnCancelar;
        private SiticonePanel panelResultadoIA;
        private Label lblResultadoIA;
        private TextBox txtSolucaoIA;

        // Resultado do chamado criado
        private ChamadoResponse ultimoResultado;

        /// <summary>
        /// Construtor do formulário.
        /// </summary>
        public FormNovoChamado()
        {
            InitializeComponent();
            SetupForm();
        }

        /// <summary>
        /// Configura o formulário.
        /// </summary>
        private void SetupForm()
        {
            // Configurações do form
            this.Text = "ClickDesk - Novo Chamado";
            this.Size = new Size(760, 700);
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

            // Main panel with Siticone
            mainPanel = new SiticonePanel
            {
                Size = new Size(700, 640),
                Location = new Point((this.Width - 700) / 2, 30),
                FillColor = ThemeManager.CardBackground,
                BorderRadius = ClickDeskStyles.RadiusXL
            };
            mainPanel.ShadowDecoration.Enabled = true;
            mainPanel.ShadowDecoration.Depth = 20;
            this.Controls.Add(mainPanel);

            int leftMargin = 40;
            int inputWidth = 620;
            int y = 30;

            // Título do formulário
            Label lblTitle = new Label
            {
                Text = "Abrir Novo Chamado",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = ThemeManager.Brand,
                Location = new Point(leftMargin, y),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            mainPanel.Controls.Add(lblTitle);

            y += 50;

            // Subtítulo
            Label lblSubtitle = new Label
            {
                Text = "Descreva seu problema. Nossa IA tentará resolver automaticamente!",
                Font = ClickDeskStyles.FontBase,
                ForeColor = ThemeManager.TextSecondary,
                Location = new Point(leftMargin, y),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            mainPanel.Controls.Add(lblSubtitle);

            y += 40;

            // Título do chamado
            mainPanel.Controls.Add(CreateLabel("Título *", leftMargin, y));
            y += 25;
            txtTitulo = new SiticoneTextBox
            {
                Size = new Size(inputWidth, 45),
                Location = new Point(leftMargin, y),
                Font = ClickDeskStyles.FontBase,
                BorderRadius = ClickDeskStyles.RadiusSM,
                BorderThickness = 1,
                BorderColor = ThemeManager.Border,
                FillColor = ThemeManager.CardBackground,
                ForeColor = ThemeManager.TextPrimary,
                PlaceholderText = "Ex: Computador não liga",
                TextOffset = new Point(10, 0)
            };
            mainPanel.Controls.Add(txtTitulo);

            y += 55;

            // Categoria
            mainPanel.Controls.Add(CreateLabel("Categoria *", leftMargin, y));
            y += 25;
            cmbCategoria = new SiticoneComboBox
            {
                Size = new Size(290, 40),
                Location = new Point(leftMargin, y),
                Font = ClickDeskStyles.FontBase,
                BorderRadius = ClickDeskStyles.RadiusSM,
                BorderColor = ThemeManager.Border,
                FillColor = ThemeManager.CardBackground,
                ForeColor = ThemeManager.TextPrimary,
                Style = Siticone.Desktop.UI.WinForms.Enums.TextBoxStyle.Material
            };
            cmbCategoria.Items.AddRange(new object[] {
                "Hardware",
                "Software",
                "Rede",
                "E-mail",
                "Sistema",
                "Acesso",
                "Impressora",
                "Outros"
            });
            cmbCategoria.SelectedIndex = 0;
            mainPanel.Controls.Add(cmbCategoria);

            // Severidade
            mainPanel.Controls.Add(CreateLabel("Severidade", leftMargin + 330, y - 25));
            cmbSeveridade = new SiticoneComboBox
            {
                Size = new Size(290, 40),
                Location = new Point(leftMargin + 330, y),
                Font = ClickDeskStyles.FontBase,
                BorderRadius = ClickDeskStyles.RadiusSM,
                BorderColor = ThemeManager.Border,
                FillColor = ThemeManager.CardBackground,
                ForeColor = ThemeManager.TextPrimary,
                Style = Siticone.Desktop.UI.WinForms.Enums.TextBoxStyle.Material
            };
            cmbSeveridade.Items.AddRange(new object[] {
                "Baixa",
                "Média",
                "Alta",
                "Crítica"
            });
            cmbSeveridade.SelectedIndex = 0;
            mainPanel.Controls.Add(cmbSeveridade);

            y += 50;

            // Descrição
            mainPanel.Controls.Add(CreateLabel("Descrição do Problema *", leftMargin, y));
            y += 25;
            txtDescricao = new SiticoneTextBox
            {
                Size = new Size(inputWidth, 160),
                Location = new Point(leftMargin, y),
                Font = ClickDeskStyles.FontBase,
                BorderRadius = ClickDeskStyles.RadiusSM,
                BorderThickness = 1,
                BorderColor = ThemeManager.Border,
                FillColor = ThemeManager.CardBackground,
                ForeColor = ThemeManager.TextPrimary,
                PlaceholderText = "Descreva o problema em detalhes...",
                TextOffset = new Point(10, 0),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            mainPanel.Controls.Add(txtDescricao);

            y += 170;

            // Painel de resultado da IA (inicialmente oculto)
            panelResultadoIA = new SiticonePanel
            {
                Size = new Size(inputWidth, 120),
                Location = new Point(leftMargin, y),
                FillColor = ClickDeskColors.SuccessLight,
                BorderRadius = ClickDeskStyles.RadiusMD,
                Visible = false
            };
            mainPanel.Controls.Add(panelResultadoIA);

            lblResultadoIA = new Label
            {
                Text = "🤖 Solução encontrada pela IA:",
                Font = ClickDeskStyles.FontBaseStrong,
                ForeColor = ClickDeskColors.Success,
                Location = new Point(15, 10),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            panelResultadoIA.Controls.Add(lblResultadoIA);

            txtSolucaoIA = new TextBox
            {
                Size = new Size(inputWidth - 30, 70),
                Location = new Point(15, 35),
                Font = ClickDeskStyles.FontSM,
                Multiline = true,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = ClickDeskColors.SuccessLight
            };
            panelResultadoIA.Controls.Add(txtSolucaoIA);

            y += 130;

            // Botões
            btnCancelar = new SiticoneButton
            {
                Text = "Cancelar",
                Size = new Size(150, 45),
                Location = new Point(leftMargin + inputWidth - 330, y),
                BorderRadius = ClickDeskStyles.RadiusMD,
                FillColor = ClickDeskColors.Gray300,
                ForeColor = ClickDeskColors.Gray700,
                Font = ClickDeskStyles.FontLG,
                Cursor = Cursors.Hand,
                HoverState = { FillColor = ClickDeskColors.Gray400 }
            };
            btnCancelar.Click += BtnCancelar_Click;
            mainPanel.Controls.Add(btnCancelar);

            btnEnviar = new SiticoneButton
            {
                Text = "ENVIAR CHAMADO",
                Size = new Size(160, 45),
                Location = new Point(leftMargin + inputWidth - 160, y),
                BorderRadius = ClickDeskStyles.RadiusMD,
                FillColor = ThemeManager.Brand,
                ForeColor = Color.White,
                Font = ClickDeskStyles.FontLG,
                Cursor = Cursors.Hand,
                HoverState = { FillColor = ThemeManager.BrandHover }
            };
            btnEnviar.Click += BtnEnviar_Click;
            mainPanel.Controls.Add(btnEnviar);

            // Foco inicial
            this.ActiveControl = txtTitulo;
        }

        /// <summary>
        /// Cria um label estilizado.
        /// </summary>
        private Label CreateLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Font = ClickDeskStyles.FontBaseStrong,
                ForeColor = ThemeManager.TextPrimary,
                Location = new Point(x, y),
                AutoSize = true,
                BackColor = Color.Transparent
            };
        }

        /// <summary>
        /// Aplica o tema atual aos controles do formulário.
        /// </summary>
        private void ApplyTheme()
        {
            mainPanel.FillColor = ThemeManager.CardBackground;
            txtTitulo.FillColor = ThemeManager.CardBackground;
            txtTitulo.ForeColor = ThemeManager.TextPrimary;
            txtTitulo.BorderColor = ThemeManager.Border;
            txtDescricao.FillColor = ThemeManager.CardBackground;
            txtDescricao.ForeColor = ThemeManager.TextPrimary;
            txtDescricao.BorderColor = ThemeManager.Border;
            cmbCategoria.FillColor = ThemeManager.CardBackground;
            cmbCategoria.ForeColor = ThemeManager.TextPrimary;
            cmbCategoria.BorderColor = ThemeManager.Border;
            cmbSeveridade.FillColor = ThemeManager.CardBackground;
            cmbSeveridade.ForeColor = ThemeManager.TextPrimary;
            cmbSeveridade.BorderColor = ThemeManager.Border;
            btnEnviar.FillColor = ThemeManager.Brand;
            btnEnviar.HoverState.FillColor = ThemeManager.BrandHover;

            // Update all labels
            foreach (Control control in mainPanel.Controls)
            {
                if (control is Label label)
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
            }

            mainPanel.Invalidate();
            this.Invalidate();
        }

        /// <summary>
        /// Evento de cancelar.
        /// </summary>
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Evento de enviar chamado.
        /// </summary>
        private async void BtnEnviar_Click(object sender, EventArgs e)
        {
            await EnviarChamado();
        }

        /// <summary>
        /// Envia o chamado para a API.
        /// </summary>
        private async Task EnviarChamado()
        {
            // Validação
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                UIHelper.ShowError("Por favor, informe o título do chamado.");
                txtTitulo.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                UIHelper.ShowError("Por favor, descreva o problema.");
                txtDescricao.Focus();
                return;
            }

            // Desabilita form
            SetFormEnabled(false);

            try
            {
                var request = new ChamadoRequest
                {
                    Titulo = txtTitulo.Text.Trim(),
                    Descricao = txtDescricao.Text.Trim(),
                    Categoria = cmbCategoria.SelectedItem.ToString(),
                    Severidade = cmbSeveridade.SelectedItem.ToString().ToUpper()
                };

                var response = await ChamadoService.CriarAsync(request);
                ultimoResultado = response;

                if (response != null)
                {
                    // Verifica se foi resolvido pela IA
                    if (response.ResolvidoPorIA && !string.IsNullOrEmpty(response.SolucaoIA))
                    {
                        // Mostra a solução da IA
                        MostrarSolucaoIA(response);
                    }
                    else
                    {
                        // Chamado criado normalmente
                        string mensagem = response.Chamado != null
                            ? $"Chamado #{response.Chamado.Id} criado com sucesso!"
                            : "Chamado criado com sucesso!";

                        if (response.Chamado?.Status == "ESCALADO")
                        {
                            mensagem += "\n\nSeu chamado foi escalado para um técnico devido à severidade.";
                        }

                        UIHelper.ShowSuccess(mensagem, "Sucesso");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (ApiException ex)
            {
                UIHelper.ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Erro ao criar chamado: {ex.Message}");
            }
            finally
            {
                SetFormEnabled(true);
            }
        }

        /// <summary>
        /// Mostra a solução da IA e solicita feedback.
        /// </summary>
        private void MostrarSolucaoIA(ChamadoResponse response)
        {
            // Exibe o painel de solução
            panelResultadoIA.Visible = true;
            txtSolucaoIA.Text = response.SolucaoIA;

            // Pergunta se resolveu
            var result = MessageBox.Show(
                $"Nossa IA encontrou uma solução para seu problema:\n\n{response.SolucaoIA}\n\nIsso resolveu seu problema?",
                "🤖 Solução da IA",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Usuário satisfeito - envia feedback positivo
                EnviarFeedbackIA(true);
            }
            else
            {
                // Usuário não satisfeito - pergunta se quer escalar
                var escalar = MessageBox.Show(
                    "Deseja escalar este chamado para um técnico humano?",
                    "Escalar Chamado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                EnviarFeedbackIA(false, escalar == DialogResult.Yes);
            }
        }

        /// <summary>
        /// Envia o feedback sobre a solução da IA.
        /// </summary>
        private async void EnviarFeedbackIA(bool resolveu, bool escalar = false)
        {
            try
            {
                if (ultimoResultado?.Chamado != null)
                {
                    var feedback = new FeedbackRequest
                    {
                        SolucaoUtil = resolveu,
                        Avaliacao = resolveu ? 5 : 2,
                        EscalarParaTecnico = escalar
                    };

                    await ChamadoService.EnviarFeedbackAsync(ultimoResultado.Chamado.Id, feedback);
                }

                if (resolveu)
                {
                    UIHelper.ShowSuccess("Ótimo! Ficamos felizes em ajudar. O chamado foi marcado como resolvido.", "Sucesso");
                }
                else if (escalar)
                {
                    UIHelper.ShowSuccess("Seu chamado foi escalado para um técnico. Você será contatado em breve.", "Chamado Escalado");
                }
                else
                {
                    UIHelper.ShowSuccess("Obrigado pelo feedback! O chamado permanece aberto.", "Feedback Enviado");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Erro ao enviar feedback: {ex.Message}");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Habilita/desabilita o formulário.
        /// </summary>
        private void SetFormEnabled(bool enabled)
        {
            txtTitulo.Enabled = enabled;
            txtDescricao.Enabled = enabled;
            cmbCategoria.Enabled = enabled;
            cmbSeveridade.Enabled = enabled;
            btnEnviar.Enabled = enabled;
            btnCancelar.Enabled = enabled;
            btnEnviar.Text = enabled ? "ENVIAR CHAMADO" : "AGUARDE...";
            this.Cursor = enabled ? Cursors.Default : Cursors.WaitCursor;
        }
    }
}

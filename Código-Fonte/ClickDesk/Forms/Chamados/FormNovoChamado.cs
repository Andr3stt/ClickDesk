using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickDesk.Models;
using ClickDesk.Services.API;
using ClickDesk.Utils;

namespace ClickDesk.Forms.Chamados
{
    /// <summary>
    /// Formulário para criação de novo chamado.
    /// Integra com a IA para resolução automática de problemas.
    /// </summary>
    public partial class FormNovoChamado : Form
    {
        // Campos do formulário
        private TextBox txtTitulo;
        private TextBox txtDescricao;
        private ComboBox cmbCategoria;
        private ComboBox cmbSeveridade;
        private Button btnEnviar;
        private Button btnCancelar;
        private Panel panelResultadoIA;
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
            this.Size = new Size(700, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = AppColors.White;

            int leftMargin = 40;
            int inputWidth = 600;
            int y = 30;

            // Título do formulário
            Label lblTitle = new Label
            {
                Text = "Abrir Novo Chamado",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = AppColors.Primary,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            y += 50;

            // Subtítulo
            Label lblSubtitle = new Label
            {
                Text = "Descreva seu problema. Nossa IA tentará resolver automaticamente!",
                Font = new Font("Segoe UI", 10),
                ForeColor = AppColors.Gray500,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            this.Controls.Add(lblSubtitle);

            y += 40;

            // Título do chamado
            this.Controls.Add(CreateLabel("Título *", leftMargin, y));
            y += 22;
            txtTitulo = new TextBox
            {
                Size = new Size(inputWidth, 35),
                Location = new Point(leftMargin, y),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(txtTitulo);

            y += 50;

            // Categoria
            this.Controls.Add(CreateLabel("Categoria *", leftMargin, y));
            y += 22;
            cmbCategoria = new ComboBox
            {
                Size = new Size(280, 35),
                Location = new Point(leftMargin, y),
                Font = new Font("Segoe UI", 11),
                DropDownStyle = ComboBoxStyle.DropDownList
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
            this.Controls.Add(cmbCategoria);

            // Severidade
            this.Controls.Add(CreateLabel("Severidade", leftMargin + 320, y - 22));
            cmbSeveridade = new ComboBox
            {
                Size = new Size(280, 35),
                Location = new Point(leftMargin + 320, y),
                Font = new Font("Segoe UI", 11),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbSeveridade.Items.AddRange(new object[] {
                "Baixa",
                "Média",
                "Alta",
                "Crítica"
            });
            cmbSeveridade.SelectedIndex = 0;
            this.Controls.Add(cmbSeveridade);

            y += 50;

            // Descrição
            this.Controls.Add(CreateLabel("Descrição do Problema *", leftMargin, y));
            y += 22;
            txtDescricao = new TextBox
            {
                Size = new Size(inputWidth, 150),
                Location = new Point(leftMargin, y),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            this.Controls.Add(txtDescricao);

            y += 170;

            // Painel de resultado da IA (inicialmente oculto)
            panelResultadoIA = new Panel
            {
                Size = new Size(inputWidth, 120),
                Location = new Point(leftMargin, y),
                BackColor = AppColors.SuccessLight,
                Visible = false
            };
            this.Controls.Add(panelResultadoIA);

            lblResultadoIA = new Label
            {
                Text = "🤖 Solução encontrada pela IA:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = AppColors.Success,
                Location = new Point(15, 10),
                AutoSize = true
            };
            panelResultadoIA.Controls.Add(lblResultadoIA);

            txtSolucaoIA = new TextBox
            {
                Size = new Size(inputWidth - 30, 70),
                Location = new Point(15, 35),
                Font = new Font("Segoe UI", 9),
                Multiline = true,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = AppColors.SuccessLight
            };
            panelResultadoIA.Controls.Add(txtSolucaoIA);

            y += 130;

            // Botões
            btnCancelar = new Button
            {
                Text = "Cancelar",
                Size = new Size(150, 45),
                Location = new Point(leftMargin + inputWidth - 320, y),
                BackColor = AppColors.Gray200,
                ForeColor = AppColors.Gray700,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += BtnCancelar_Click;
            this.Controls.Add(btnCancelar);

            btnEnviar = new Button
            {
                Text = "ENVIAR CHAMADO",
                Size = new Size(160, 45),
                Location = new Point(leftMargin + inputWidth - 160, y),
                BackColor = AppColors.Primary,
                ForeColor = AppColors.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnEnviar.FlatAppearance.BorderSize = 0;
            btnEnviar.Click += BtnEnviar_Click;
            this.Controls.Add(btnEnviar);

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
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = AppColors.Gray700,
                Location = new Point(x, y),
                AutoSize = true
            };
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

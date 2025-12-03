using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickDesk.Models;
using ClickDesk.Services.API;
using ClickDesk.Utils;

namespace ClickDesk.Forms.Chamados
{
    /// <summary>
    /// Formulário de detalhes do chamado para técnicos.
    /// Permite visualizar, editar, atribuir e atualizar status de chamados.
    /// </summary>
    public partial class FormDetalhesChamadoTecnico : Form
    {
        // Identificador do chamado
        private int chamadoId;
        private Chamado chamado;

        // Componentes principais
        private Panel panelPrincipal;
        private ComboBox cmbStatus;
        private ComboBox cmbSeveridade;
        private ComboBox cmbTecnico;
        private TextBox txtTitulo;
        private TextBox txtDescricao;
        private TextBox txtSolucao;
        private TextBox txtComentario;
        private FlowLayoutPanel panelComentarios;
        private Button btnSalvar;
        private Button btnFechar;
        private CheckBox chkResolvidoPorIA;
        private Label lblSolucaoIA;
        private TextBox txtSolucaoIA;

        /// <summary>
        /// Construtor do formulário.
        /// </summary>
        /// <param name="id">ID do chamado a ser visualizado/editado</param>
        public FormDetalhesChamadoTecnico(int id)
        {
            chamadoId = id;
            InitializeComponent();
            SetupForm();
        }

        /// <summary>
        /// Configura o formulário.
        /// </summary>
        private void SetupForm()
        {
            // Configurações do form
            this.Text = "ClickDesk - Detalhes do Chamado (Técnico)";
            this.Size = new Size(1000, 750);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = ClickDeskColors.BackgroundApp;

            // Painel principal com scroll
            panelPrincipal = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(30),
                BackColor = ClickDeskColors.BackgroundApp
            };
            this.Controls.Add(panelPrincipal);

            int y = 20;
            int leftMargin = 30;
            int contentWidth = 900;

            // Cabeçalho
            Label lblTituloPagina = new Label
            {
                Text = "⚙️ Gerenciar Chamado",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            panelPrincipal.Controls.Add(lblTituloPagina);

            y += 45;

            // ID e data
            Label lblInfo = new Label
            {
                Name = "lblInfo",
                Text = "Carregando...",
                Font = new Font("Segoe UI", 10),
                ForeColor = ClickDeskColors.Gray500,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            panelPrincipal.Controls.Add(lblInfo);

            y += 40;

            // ========================================
            // SEÇÃO DE INFORMAÇÕES BÁSICAS
            // ========================================

            // Card de informações
            Panel cardInfo = CriarCard(leftMargin, y, contentWidth, 320);
            panelPrincipal.Controls.Add(cardInfo);

            int cardY = 15;
            int cardLeft = 20;
            int fieldWidth = 400;

            // Título da seção
            Label lblSecaoInfo = new Label
            {
                Text = "📋 Informações do Chamado",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(cardLeft, cardY),
                AutoSize = true
            };
            cardInfo.Controls.Add(lblSecaoInfo);

            cardY += 35;

            // Título do chamado
            Label lblTituloLabel = new Label
            {
                Text = "Título:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ClickDeskColors.Gray700,
                Location = new Point(cardLeft, cardY),
                AutoSize = true
            };
            cardInfo.Controls.Add(lblTituloLabel);

            cardY += 25;

            txtTitulo = new TextBox
            {
                Location = new Point(cardLeft, cardY),
                Size = new Size(contentWidth - 60, 30),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = ClickDeskColors.White
            };
            cardInfo.Controls.Add(txtTitulo);

            cardY += 45;

            // Status e Severidade lado a lado
            Label lblStatusLabel = new Label
            {
                Text = "Status:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ClickDeskColors.Gray700,
                Location = new Point(cardLeft, cardY),
                AutoSize = true
            };
            cardInfo.Controls.Add(lblStatusLabel);

            Label lblSeveridadeLabel = new Label
            {
                Text = "Severidade:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ClickDeskColors.Gray700,
                Location = new Point(cardLeft + 220, cardY),
                AutoSize = true
            };
            cardInfo.Controls.Add(lblSeveridadeLabel);

            Label lblTecnicoLabel = new Label
            {
                Text = "Técnico Responsável:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ClickDeskColors.Gray700,
                Location = new Point(cardLeft + 440, cardY),
                AutoSize = true
            };
            cardInfo.Controls.Add(lblTecnicoLabel);

            cardY += 25;

            cmbStatus = new ComboBox
            {
                Location = new Point(cardLeft, cardY),
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbStatus.Items.AddRange(new object[] {
                "ABERTO",
                "EM_ANDAMENTO",
                "RESOLVIDO",
                "FECHADO",
                "ESCALADO"
            });
            cardInfo.Controls.Add(cmbStatus);

            cmbSeveridade = new ComboBox
            {
                Location = new Point(cardLeft + 220, cardY),
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbSeveridade.Items.AddRange(new object[] {
                "BAIXA",
                "MEDIA",
                "ALTA",
                "CRITICA"
            });
            cardInfo.Controls.Add(cmbSeveridade);

            cmbTecnico = new ComboBox
            {
                Location = new Point(cardLeft + 440, cardY),
                Size = new Size(280, 30),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbTecnico.Items.Add("Não atribuído");
            cardInfo.Controls.Add(cmbTecnico);

            cardY += 45;

            // Descrição
            Label lblDescricaoLabel = new Label
            {
                Text = "Descrição do Problema:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ClickDeskColors.Gray700,
                Location = new Point(cardLeft, cardY),
                AutoSize = true
            };
            cardInfo.Controls.Add(lblDescricaoLabel);

            cardY += 25;

            txtDescricao = new TextBox
            {
                Location = new Point(cardLeft, cardY),
                Size = new Size(contentWidth - 60, 80),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = ClickDeskColors.Gray50,
                ReadOnly = true
            };
            cardInfo.Controls.Add(txtDescricao);

            y += 340;

            // ========================================
            // SEÇÃO DE IA (se houver)
            // ========================================

            Panel cardIA = CriarCard(leftMargin, y, contentWidth, 120);
            cardIA.Name = "cardIA";
            cardIA.BackColor = ClickDeskColors.SuccessLight;
            cardIA.Visible = false;
            panelPrincipal.Controls.Add(cardIA);

            lblSolucaoIA = new Label
            {
                Text = "🤖 Solução da IA:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ClickDeskColors.Success,
                Location = new Point(20, 15),
                AutoSize = true
            };
            cardIA.Controls.Add(lblSolucaoIA);

            txtSolucaoIA = new TextBox
            {
                Name = "txtSolucaoIA",
                Location = new Point(20, 40),
                Size = new Size(contentWidth - 60, 60),
                Font = new Font("Segoe UI", 9),
                BorderStyle = BorderStyle.None,
                Multiline = true,
                ReadOnly = true,
                BackColor = ClickDeskColors.SuccessLight,
                ScrollBars = ScrollBars.Vertical
            };
            cardIA.Controls.Add(txtSolucaoIA);

            // (Incrementamos Y depois de verificar se cardIA é visível)
            y += 0; // Será ajustado após carregar dados

            // ========================================
            // SEÇÃO DE RESOLUÇÃO DO TÉCNICO
            // ========================================

            Panel cardSolucao = CriarCard(leftMargin, y, contentWidth, 160);
            cardSolucao.Name = "cardSolucao";
            panelPrincipal.Controls.Add(cardSolucao);

            Label lblSolucaoLabel = new Label
            {
                Text = "💡 Solução / Resolução do Técnico:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(20, 15),
                AutoSize = true
            };
            cardSolucao.Controls.Add(lblSolucaoLabel);

            txtSolucao = new TextBox
            {
                Location = new Point(20, 45),
                Size = new Size(contentWidth - 60, 90),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = ClickDeskColors.White
            };
            cardSolucao.Controls.Add(txtSolucao);

            y += 180;

            // ========================================
            // SEÇÃO DE COMENTÁRIOS
            // ========================================

            Panel cardComentarios = CriarCard(leftMargin, y, contentWidth, 200);
            panelPrincipal.Controls.Add(cardComentarios);

            Label lblComentariosLabel = new Label
            {
                Text = "💬 Comentários e Histórico:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(20, 15),
                AutoSize = true
            };
            cardComentarios.Controls.Add(lblComentariosLabel);

            panelComentarios = new FlowLayoutPanel
            {
                Location = new Point(20, 45),
                Size = new Size(contentWidth - 60, 80),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = ClickDeskColors.Gray50
            };
            cardComentarios.Controls.Add(panelComentarios);

            // Adicionar comentário
            txtComentario = new TextBox
            {
                Location = new Point(20, 135),
                Size = new Size(contentWidth - 180, 45),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                Multiline = true
            };
            cardComentarios.Controls.Add(txtComentario);

            Button btnAdicionarComentario = UIHelper.CreatePrimaryButton("Adicionar", 120, 45);
            btnAdicionarComentario.Location = new Point(contentWidth - 140, 135);
            btnAdicionarComentario.Click += BtnAdicionarComentario_Click;
            cardComentarios.Controls.Add(btnAdicionarComentario);

            y += 220;

            // ========================================
            // BOTÕES DE AÇÃO
            // ========================================

            btnFechar = new Button
            {
                Text = "Fechar",
                Size = new Size(120, 45),
                Location = new Point(leftMargin + contentWidth - 280, y),
                BackColor = ClickDeskColors.Gray200,
                ForeColor = ClickDeskColors.Gray700,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnFechar.FlatAppearance.BorderSize = 0;
            btnFechar.Click += (s, e) => this.Close();
            panelPrincipal.Controls.Add(btnFechar);

            btnSalvar = new Button
            {
                Text = "💾 SALVAR ALTERAÇÕES",
                Size = new Size(150, 45),
                Location = new Point(leftMargin + contentWidth - 150, y),
                BackColor = ClickDeskColors.Brand,
                ForeColor = ClickDeskColors.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSalvar.FlatAppearance.BorderSize = 0;
            btnSalvar.Click += BtnSalvar_Click;
            panelPrincipal.Controls.Add(btnSalvar);

            // Carrega dados
            this.Load += async (s, e) => await CarregarChamado();
        }

        /// <summary>
        /// Cria um card com fundo branco e bordas.
        /// </summary>
        private Panel CriarCard(int x, int y, int width, int height)
        {
            var panel = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = ClickDeskColors.White,
                Padding = new Padding(15)
            };

            panel.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle,
                    ClickDeskColors.Gray200, 1, ButtonBorderStyle.Solid,
                    ClickDeskColors.Gray200, 1, ButtonBorderStyle.Solid,
                    ClickDeskColors.Gray200, 1, ButtonBorderStyle.Solid,
                    ClickDeskColors.Gray200, 1, ButtonBorderStyle.Solid);
            };

            return panel;
        }

        /// <summary>
        /// Carrega os dados do chamado.
        /// </summary>
        private async Task CarregarChamado()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                chamado = await ChamadoService.ObterAsync(chamadoId);

                if (chamado != null)
                {
                    PreencherDados();
                    await CarregarTecnicos();
                }
                else
                {
                    UIHelper.ShowError("Chamado não encontrado.");
                    this.Close();
                }
            }
            catch (ApiException ex)
            {
                UIHelper.ShowError(ex.Message);
                this.Close();
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Erro ao carregar chamado: {ex.Message}");
                this.Close();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Preenche os campos do formulário com os dados do chamado.
        /// </summary>
        private void PreencherDados()
        {
            this.Text = $"ClickDesk - Chamado #{chamado.Id} (Técnico)";

            // Informações básicas
            var lblInfo = panelPrincipal.Controls["lblInfo"] as Label;
            if (lblInfo != null)
            {
                lblInfo.Text = $"Chamado #{chamado.Id} | Aberto em {chamado.DataAbertura:dd/MM/yyyy HH:mm} | Por: {chamado.UsuarioNome ?? "N/A"}";
            }

            // Campos editáveis
            txtTitulo.Text = chamado.Titulo;
            txtDescricao.Text = chamado.Descricao;

            // Status
            for (int i = 0; i < cmbStatus.Items.Count; i++)
            {
                if (cmbStatus.Items[i].ToString() == chamado.Status)
                {
                    cmbStatus.SelectedIndex = i;
                    break;
                }
            }

            // Severidade
            for (int i = 0; i < cmbSeveridade.Items.Count; i++)
            {
                if (cmbSeveridade.Items[i].ToString() == chamado.Severidade)
                {
                    cmbSeveridade.SelectedIndex = i;
                    break;
                }
            }

            // Card de IA (se houver solução)
            var cardIA = panelPrincipal.Controls["cardIA"] as Panel;
            if (cardIA != null && !string.IsNullOrEmpty(chamado.SolucaoIA))
            {
                cardIA.Visible = true;
                txtSolucaoIA.Text = chamado.SolucaoIA;
                
                // Ajusta posição dos outros cards
                AjustarPosicaoCards(true);
            }

            // Comentários
            CarregarComentarios();
        }

        /// <summary>
        /// Ajusta a posição dos cards quando a seção de IA está visível.
        /// </summary>
        private void AjustarPosicaoCards(bool iaVisivel)
        {
            int offset = iaVisivel ? 140 : 0;
            
            var cardSolucao = panelPrincipal.Controls["cardSolucao"] as Panel;
            if (cardSolucao != null)
            {
                cardSolucao.Location = new Point(cardSolucao.Location.X, 360 + offset);
            }

            // Ajusta outros elementos conforme necessário
        }

        /// <summary>
        /// Carrega a lista de técnicos disponíveis.
        /// </summary>
        private async Task CarregarTecnicos()
        {
            try
            {
                // TODO: Integrar com /api/users?role=TECH
                // Por enquanto, adiciona alguns exemplos
                cmbTecnico.Items.Clear();
                cmbTecnico.Items.Add("Não atribuído");
                cmbTecnico.Items.Add("Técnico 1");
                cmbTecnico.Items.Add("Técnico 2");
                cmbTecnico.Items.Add("Admin");

                // Seleciona o técnico atual
                if (!string.IsNullOrEmpty(chamado.TecnicoNome))
                {
                    for (int i = 0; i < cmbTecnico.Items.Count; i++)
                    {
                        if (cmbTecnico.Items[i].ToString() == chamado.TecnicoNome)
                        {
                            cmbTecnico.SelectedIndex = i;
                            return;
                        }
                    }
                }
                
                cmbTecnico.SelectedIndex = 0;
            }
            catch
            {
                cmbTecnico.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Carrega e exibe os comentários do chamado.
        /// </summary>
        private void CarregarComentarios()
        {
            panelComentarios.Controls.Clear();

            if (chamado.Comentarios != null && chamado.Comentarios.Count > 0)
            {
                foreach (var comentario in chamado.Comentarios)
                {
                    var panel = CriarPanelComentario(comentario);
                    panelComentarios.Controls.Add(panel);
                }
            }
            else
            {
                var lblVazio = new Label
                {
                    Text = "Nenhum comentário registrado.",
                    Font = new Font("Segoe UI", 9),
                    ForeColor = ClickDeskColors.Gray500,
                    Padding = new Padding(10),
                    AutoSize = true
                };
                panelComentarios.Controls.Add(lblVazio);
            }
        }

        /// <summary>
        /// Cria um painel de comentário.
        /// </summary>
        private Panel CriarPanelComentario(Comentario comentario)
        {
            var panel = new Panel
            {
                Size = new Size(panelComentarios.Width - 30, 45),
                BackColor = ClickDeskColors.White,
                Margin = new Padding(3)
            };

            var lblAutor = new Label
            {
                Text = $"{comentario.UsuarioNome} • {comentario.DataCriacao:dd/MM/yyyy HH:mm}",
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = ClickDeskColors.Gray500,
                Location = new Point(8, 5),
                AutoSize = true
            };
            panel.Controls.Add(lblAutor);

            var lblTexto = new Label
            {
                Text = comentario.Texto,
                Font = new Font("Segoe UI", 9),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(8, 22),
                MaximumSize = new Size(panel.Width - 20, 0),
                AutoSize = true
            };
            panel.Controls.Add(lblTexto);

            return panel;
        }

        /// <summary>
        /// Evento de adicionar comentário.
        /// </summary>
        private async void BtnAdicionarComentario_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtComentario.Text))
            {
                UIHelper.ShowError("Digite um comentário.");
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                var request = new ComentarioRequest
                {
                    Texto = txtComentario.Text.Trim()
                };

                var comentario = await ChamadoService.AdicionarComentarioAsync(chamadoId, request);

                if (comentario != null)
                {
                    chamado.Comentarios.Add(comentario);
                    CarregarComentarios();
                    txtComentario.Clear();
                }
            }
            catch (ApiException ex)
            {
                UIHelper.ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Erro ao adicionar comentário: {ex.Message}");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Evento de salvar alterações.
        /// </summary>
        private async void BtnSalvar_Click(object sender, EventArgs e)
        {
            await SalvarAlteracoes();
        }

        /// <summary>
        /// Salva as alterações do chamado.
        /// </summary>
        private async Task SalvarAlteracoes()
        {
            // Validações
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                UIHelper.ShowError("O título é obrigatório.");
                txtTitulo.Focus();
                return;
            }

            // Desabilita form
            SetFormEnabled(false);

            try
            {
                // Prepara a requisição
                var request = new ChamadoRequest
                {
                    Titulo = txtTitulo.Text.Trim(),
                    Descricao = txtDescricao.Text.Trim(),
                    Severidade = cmbSeveridade.SelectedItem?.ToString() ?? "BAIXA"
                };

                // Atualiza o chamado
                var chamadoAtualizado = await ChamadoService.AtualizarAsync(chamadoId, request);

                // Atualiza o status se mudou
                if (cmbStatus.SelectedItem?.ToString() != chamado.Status)
                {
                    var statusRequest = new StatusUpdateRequest
                    {
                        Status = cmbStatus.SelectedItem?.ToString(),
                        Motivo = "Atualização pelo técnico"
                    };
                    await ChamadoService.AtualizarStatusAsync(chamadoId, statusRequest);
                }

                // Se foi adicionada uma solução e status é RESOLVIDO, registra
                if (!string.IsNullOrWhiteSpace(txtSolucao.Text) && cmbStatus.SelectedItem?.ToString() == "RESOLVIDO")
                {
                    var comentario = new ComentarioRequest
                    {
                        Texto = $"[SOLUÇÃO] {txtSolucao.Text.Trim()}",
                        Solucao = true
                    };
                    await ChamadoService.AdicionarComentarioAsync(chamadoId, comentario);
                }

                UIHelper.ShowSuccess("Chamado atualizado com sucesso!", "Sucesso");
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
            txtTitulo.Enabled = enabled;
            cmbStatus.Enabled = enabled;
            cmbSeveridade.Enabled = enabled;
            cmbTecnico.Enabled = enabled;
            txtSolucao.Enabled = enabled;
            txtComentario.Enabled = enabled;
            btnSalvar.Enabled = enabled;
            btnFechar.Enabled = enabled;
            btnSalvar.Text = enabled ? "💾 SALVAR ALTERAÇÕES" : "SALVANDO...";
            btnSalvar.BackColor = enabled ? ClickDeskColors.Brand : ClickDeskColors.Gray400;
            this.Cursor = enabled ? Cursors.Default : Cursors.WaitCursor;
        }
    }
}

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
    /// Formulário para visualizar detalhes de um chamado.
    /// Permite interação, comentários e feedback.
    /// </summary>
    public partial class FormDetalhesChamado : Form
    {
        private int chamadoId;
        private Chamado chamado;
        private TextBox txtComentario;
        private FlowLayoutPanel panelComentarios;
        private Label lblStatus;
        private Label lblSeveridade;

        /// <summary>
        /// Construtor do formulário.
        /// </summary>
        /// <param name="id">ID do chamado a ser exibido</param>
        public FormDetalhesChamado(int id)
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
            this.Text = "ClickDesk - Detalhes do Chamado";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = ClickDeskColors.White;

            // Container principal com scroll
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(30)
            };
            this.Controls.Add(mainPanel);

            int y = 20;
            int leftMargin = 20;

            // Título (será preenchido depois)
            Label lblTitle = new Label
            {
                Name = "lblTitle",
                Text = "Carregando...",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            mainPanel.Controls.Add(lblTitle);

            y += 50;

            // Badges de status e severidade
            lblStatus = new Label
            {
                Text = "STATUS",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = ClickDeskColors.White,
                BackColor = ClickDeskColors.Primary,
                Location = new Point(leftMargin, y),
                Size = new Size(100, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            mainPanel.Controls.Add(lblStatus);

            lblSeveridade = new Label
            {
                Text = "SEVERIDADE",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = ClickDeskColors.White,
                BackColor = ClickDeskColors.Warning,
                Location = new Point(leftMargin + 110, y),
                Size = new Size(100, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            mainPanel.Controls.Add(lblSeveridade);

            // ID do chamado
            Label lblId = new Label
            {
                Name = "lblId",
                Text = $"#",
                Font = new Font("Segoe UI", 10),
                ForeColor = ClickDeskColors.Gray500,
                Location = new Point(leftMargin + 230, y + 3),
                AutoSize = true
            };
            mainPanel.Controls.Add(lblId);

            y += 50;

            // Informações
            Panel infoPanel = new Panel
            {
                Location = new Point(leftMargin, y),
                Size = new Size(800, 100),
                BackColor = ClickDeskColors.Gray50
            };
            mainPanel.Controls.Add(infoPanel);

            Label lblCategoria = new Label
            {
                Name = "lblCategoria",
                Text = "Categoria: -",
                Font = new Font("Segoe UI", 10),
                ForeColor = ClickDeskColors.Gray700,
                Location = new Point(15, 15),
                AutoSize = true
            };
            infoPanel.Controls.Add(lblCategoria);

            Label lblData = new Label
            {
                Name = "lblData",
                Text = "Aberto em: -",
                Font = new Font("Segoe UI", 10),
                ForeColor = ClickDeskColors.Gray700,
                Location = new Point(250, 15),
                AutoSize = true
            };
            infoPanel.Controls.Add(lblData);

            Label lblUsuario = new Label
            {
                Name = "lblUsuario",
                Text = "Por: -",
                Font = new Font("Segoe UI", 10),
                ForeColor = ClickDeskColors.Gray700,
                Location = new Point(500, 15),
                AutoSize = true
            };
            infoPanel.Controls.Add(lblUsuario);

            Label lblTecnico = new Label
            {
                Name = "lblTecnico",
                Text = "Técnico: -",
                Font = new Font("Segoe UI", 10),
                ForeColor = ClickDeskColors.Gray700,
                Location = new Point(15, 50),
                AutoSize = true
            };
            infoPanel.Controls.Add(lblTecnico);

            Label lblResolvidoIA = new Label
            {
                Name = "lblResolvidoIA",
                Text = "",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ClickDeskColors.Success,
                Location = new Point(250, 50),
                AutoSize = true
            };
            infoPanel.Controls.Add(lblResolvidoIA);

            y += 120;

            // Descrição
            Label lblDescTitle = new Label
            {
                Text = "Descrição:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            mainPanel.Controls.Add(lblDescTitle);

            y += 30;

            TextBox txtDescricao = new TextBox
            {
                Name = "txtDescricao",
                Size = new Size(800, 100),
                Location = new Point(leftMargin, y),
                Font = new Font("Segoe UI", 10),
                Multiline = true,
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = ClickDeskColors.Gray50,
                ScrollBars = ScrollBars.Vertical
            };
            mainPanel.Controls.Add(txtDescricao);

            y += 120;

            // Solução da IA (se houver)
            Panel panelSolucaoIA = new Panel
            {
                Name = "panelSolucaoIA",
                Location = new Point(leftMargin, y),
                Size = new Size(800, 100),
                BackColor = ClickDeskColors.SuccessLight,
                Visible = false
            };
            mainPanel.Controls.Add(panelSolucaoIA);

            Label lblSolucaoIATitle = new Label
            {
                Text = "🤖 Solução da IA:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ClickDeskColors.Success,
                Location = new Point(15, 10),
                AutoSize = true
            };
            panelSolucaoIA.Controls.Add(lblSolucaoIATitle);

            TextBox txtSolucaoIA = new TextBox
            {
                Name = "txtSolucaoIA",
                Size = new Size(770, 60),
                Location = new Point(15, 35),
                Font = new Font("Segoe UI", 9),
                Multiline = true,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = ClickDeskColors.SuccessLight
            };
            panelSolucaoIA.Controls.Add(txtSolucaoIA);

            y += 110;

            // Comentários
            Label lblComentariosTitle = new Label
            {
                Text = "Comentários:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            mainPanel.Controls.Add(lblComentariosTitle);

            y += 30;

            panelComentarios = new FlowLayoutPanel
            {
                Location = new Point(leftMargin, y),
                Size = new Size(800, 120),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = ClickDeskColors.Gray50
            };
            mainPanel.Controls.Add(panelComentarios);

            y += 130;

            // Adicionar comentário
            Label lblNovoComentario = new Label
            {
                Text = "Adicionar Comentário:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ClickDeskColors.Gray700,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            mainPanel.Controls.Add(lblNovoComentario);

            y += 25;

            txtComentario = new TextBox
            {
                Size = new Size(640, 60),
                Location = new Point(leftMargin, y),
                Font = new Font("Segoe UI", 10),
                Multiline = true,
                BorderStyle = BorderStyle.FixedSingle
            };
            mainPanel.Controls.Add(txtComentario);

            Button btnEnviarComentario = UIHelper.CreatePrimaryButton("Enviar", 140, 60);
            btnEnviarComentario.Location = new Point(leftMargin + 660, y);
            btnEnviarComentario.Click += BtnEnviarComentario_Click;
            mainPanel.Controls.Add(btnEnviarComentario);

            y += 80;

            // Botão Fechar
            Button btnFechar = UIHelper.CreateSecondaryButton("Fechar", 120, 40);
            btnFechar.Location = new Point(leftMargin + 680, y);
            btnFechar.Click += (s, e) => this.Close();
            mainPanel.Controls.Add(btnFechar);

            // Carrega dados
            this.Load += async (s, e) => await CarregarChamado();
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
        /// Preenche os dados do chamado na tela.
        /// </summary>
        private void PreencherDados()
        {
            this.Text = $"ClickDesk - Chamado #{chamado.Id}";

            // Encontra controles
            var mainPanel = this.Controls[0] as Panel;
            var lblTitle = mainPanel.Controls["lblTitle"] as Label;
            var lblId = mainPanel.Controls["lblId"] as Label;
            var lblCategoria = mainPanel.Controls[3].Controls["lblCategoria"] as Label;
            var lblData = mainPanel.Controls[3].Controls["lblData"] as Label;
            var lblUsuario = mainPanel.Controls[3].Controls["lblUsuario"] as Label;
            var lblTecnico = mainPanel.Controls[3].Controls["lblTecnico"] as Label;
            var lblResolvidoIA = mainPanel.Controls[3].Controls["lblResolvidoIA"] as Label;
            var txtDescricao = mainPanel.Controls["txtDescricao"] as TextBox;
            var panelSolucaoIA = mainPanel.Controls["panelSolucaoIA"] as Panel;
            var txtSolucaoIA = panelSolucaoIA.Controls["txtSolucaoIA"] as TextBox;

            // Preenche
            lblTitle.Text = chamado.Titulo;
            lblId.Text = $"#{chamado.Id}";
            lblStatus.Text = chamado.Status;
            lblStatus.BackColor = ClickDeskColors.GetStatusColor(chamado.Status);
            lblSeveridade.Text = chamado.Severidade;
            lblSeveridade.BackColor = ClickDeskColors.GetSeveridadeColor(chamado.Severidade);

            lblCategoria.Text = $"Categoria: {chamado.Categoria}";
            lblData.Text = $"Aberto em: {chamado.DataAbertura:dd/MM/yyyy HH:mm}";
            lblUsuario.Text = $"Por: {chamado.UsuarioNome ?? "N/A"}";
            lblTecnico.Text = $"Técnico: {chamado.TecnicoNome ?? "Não atribuído"}";

            if (chamado.ResolvidoPorIA)
            {
                lblResolvidoIA.Text = "✓ Resolvido pela IA";
            }

            txtDescricao.Text = chamado.Descricao;

            // Solução da IA
            if (!string.IsNullOrEmpty(chamado.SolucaoIA))
            {
                panelSolucaoIA.Visible = true;
                txtSolucaoIA.Text = chamado.SolucaoIA;
            }

            // Comentários
            CarregarComentarios();
        }

        /// <summary>
        /// Carrega e exibe os comentários.
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
                    Text = "Nenhum comentário ainda.",
                    Font = new Font("Segoe UI", 10),
                    ForeColor = ClickDeskColors.Gray500,
                    Padding = new Padding(10),
                    AutoSize = true
                };
                panelComentarios.Controls.Add(lblVazio);
            }
        }

        /// <summary>
        /// Cria o painel de um comentário.
        /// </summary>
        private Panel CriarPanelComentario(Comentario comentario)
        {
            var panel = new Panel
            {
                Size = new Size(760, 50),
                BackColor = ClickDeskColors.White,
                Margin = new Padding(5)
            };

            var lblAutor = new Label
            {
                Text = $"{comentario.UsuarioNome} - {comentario.DataCriacao:dd/MM/yyyy HH:mm}",
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = ClickDeskColors.Gray500,
                Location = new Point(10, 5),
                AutoSize = true
            };
            panel.Controls.Add(lblAutor);

            var lblTexto = new Label
            {
                Text = comentario.Texto,
                Font = new Font("Segoe UI", 9),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(10, 25),
                MaximumSize = new Size(740, 0),
                AutoSize = true
            };
            panel.Controls.Add(lblTexto);

            return panel;
        }

        /// <summary>
        /// Envia um novo comentário.
        /// </summary>
        private async void BtnEnviarComentario_Click(object sender, EventArgs e)
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
                    // Adiciona à lista e atualiza
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
                UIHelper.ShowError($"Erro ao enviar comentário: {ex.Message}");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}

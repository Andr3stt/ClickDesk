using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickDesk.Models;
using ClickDesk.Services.API;
using ClickDesk.Utils;

namespace ClickDesk.Forms.FAQ
{
    /// <summary>
    /// Formulário para visualização da FAQ (Base de Conhecimento).
    /// Permite buscar e consultar perguntas frequentes.
    /// </summary>
    public partial class FormFAQ : Form
    {
        private TextBox txtBusca;
        private FlowLayoutPanel panelFAQs;
        private List<Models.FAQ> todasFAQs;

        /// <summary>
        /// Construtor do formulário.
        /// </summary>
        public FormFAQ()
        {
            InitializeComponent();
            SetupForm();
        }

        /// <summary>
        /// Configura o formulário.
        /// </summary>
        private void SetupForm()
        {
            this.Text = "ClickDesk - FAQ";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = ClickDeskColors.ContentBackground;

            int y = 20;
            int leftMargin = 30;

            // Título
            Label lblTitle = new Label
            {
                Text = "📚 Base de Conhecimento (FAQ)",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            Label lblSubtitle = new Label
            {
                Text = "Encontre respostas para as perguntas mais frequentes",
                Font = new Font("Segoe UI", 11),
                ForeColor = ClickDeskColors.Gray500,
                Location = new Point(leftMargin, y + 40),
                AutoSize = true
            };
            this.Controls.Add(lblSubtitle);

            y += 80;

            // Campo de busca
            txtBusca = new TextBox
            {
                Size = new Size(600, 40),
                Location = new Point(leftMargin, y),
                Font = new Font("Segoe UI", 12),
                BorderStyle = BorderStyle.FixedSingle
            };
            txtBusca.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) FiltrarFAQs(); };
            this.Controls.Add(txtBusca);

            var btnBuscar = UIHelper.CreatePrimaryButton("🔍 Buscar", 140, 40);
            btnBuscar.Location = new Point(leftMargin + 620, y);
            btnBuscar.Click += (s, e) => FiltrarFAQs();
            this.Controls.Add(btnBuscar);

            y += 60;

            // Painel de FAQs
            panelFAQs = new FlowLayoutPanel
            {
                Location = new Point(leftMargin, y),
                Size = new Size(this.ClientSize.Width - 60, this.ClientSize.Height - y - 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = ClickDeskColors.ContentBackground
            };
            this.Controls.Add(panelFAQs);

            // Carrega dados
            this.Load += async (s, e) => await CarregarFAQs();
        }

        /// <summary>
        /// Carrega as FAQs.
        /// </summary>
        private async Task CarregarFAQs()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                todasFAQs = await FAQService.ListarAsync();
                ExibirFAQs(todasFAQs);
            }
            catch (ApiException ex)
            {
                UIHelper.ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Erro ao carregar FAQs: {ex.Message}");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Exibe as FAQs no painel.
        /// </summary>
        private void ExibirFAQs(List<Models.FAQ> faqs)
        {
            panelFAQs.Controls.Clear();

            if (faqs == null || faqs.Count == 0)
            {
                var lblVazio = new Label
                {
                    Text = "Nenhuma FAQ encontrada.",
                    Font = new Font("Segoe UI", 12),
                    ForeColor = ClickDeskColors.Gray500,
                    AutoSize = true,
                    Padding = new Padding(20)
                };
                panelFAQs.Controls.Add(lblVazio);
                return;
            }

            foreach (var faq in faqs)
            {
                var card = CriarCardFAQ(faq);
                panelFAQs.Controls.Add(card);
            }
        }

        /// <summary>
        /// Cria um card de FAQ.
        /// </summary>
        private Panel CriarCardFAQ(Models.FAQ faq)
        {
            var panel = new Panel
            {
                Size = new Size(panelFAQs.Width - 40, 120),
                BackColor = ClickDeskColors.White,
                Margin = new Padding(0, 0, 0, 15),
                Padding = new Padding(15),
                Cursor = Cursors.Hand
            };

            // Categoria
            var lblCategoria = new Label
            {
                Text = faq.Categoria ?? "Geral",
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = ClickDeskColors.White,
                BackColor = ClickDeskColors.Primary,
                Location = new Point(15, 10),
                Size = new Size(80, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panel.Controls.Add(lblCategoria);

            // Pergunta
            var lblPergunta = new Label
            {
                Text = faq.Pergunta,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(15, 40),
                MaximumSize = new Size(panel.Width - 40, 0),
                AutoSize = true
            };
            panel.Controls.Add(lblPergunta);

            // Resposta (preview)
            string respostaPreview = faq.Resposta?.Length > 150
                ? faq.Resposta.Substring(0, 150) + "..."
                : faq.Resposta;

            var lblResposta = new Label
            {
                Text = respostaPreview,
                Font = new Font("Segoe UI", 10),
                ForeColor = ClickDeskColors.Gray600,
                Location = new Point(15, 70),
                MaximumSize = new Size(panel.Width - 40, 0),
                AutoSize = true
            };
            panel.Controls.Add(lblResposta);

            // Evento de clique para expandir
            panel.Click += (s, e) => MostrarFAQCompleta(faq);
            lblPergunta.Click += (s, e) => MostrarFAQCompleta(faq);
            lblResposta.Click += (s, e) => MostrarFAQCompleta(faq);

            return panel;
        }

        /// <summary>
        /// Mostra a FAQ completa em um dialog.
        /// </summary>
        private void MostrarFAQCompleta(Models.FAQ faq)
        {
            var form = new Form
            {
                Text = "FAQ - Detalhes",
                Size = new Size(700, 500),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = ClickDeskColors.White,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var lblCategoria = new Label
            {
                Text = faq.Categoria ?? "Geral",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = ClickDeskColors.White,
                BackColor = ClickDeskColors.Primary,
                Location = new Point(30, 20),
                Size = new Size(100, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            form.Controls.Add(lblCategoria);

            var lblPergunta = new Label
            {
                Text = faq.Pergunta,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(30, 60),
                MaximumSize = new Size(620, 0),
                AutoSize = true
            };
            form.Controls.Add(lblPergunta);

            var txtResposta = new TextBox
            {
                Text = faq.Resposta,
                Font = new Font("Segoe UI", 11),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(30, 120),
                Size = new Size(620, 280),
                Multiline = true,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = ClickDeskColors.Gray50,
                ScrollBars = ScrollBars.Vertical
            };
            form.Controls.Add(txtResposta);

            var btnFechar = UIHelper.CreatePrimaryButton("Fechar", 100, 35);
            btnFechar.Location = new Point(550, 410);
            btnFechar.Click += (s, e) => form.Close();
            form.Controls.Add(btnFechar);

            form.ShowDialog(this);
        }

        /// <summary>
        /// Filtra as FAQs por termo de busca.
        /// </summary>
        private void FiltrarFAQs()
        {
            if (todasFAQs == null) return;

            string termo = txtBusca.Text.ToLower().Trim();

            if (string.IsNullOrEmpty(termo))
            {
                ExibirFAQs(todasFAQs);
                return;
            }

            var filtradas = todasFAQs.FindAll(f =>
                (f.Pergunta?.ToLower().Contains(termo) ?? false) ||
                (f.Resposta?.ToLower().Contains(termo) ?? false) ||
                (f.Categoria?.ToLower().Contains(termo) ?? false) ||
                (f.PalavrasChave?.ToLower().Contains(termo) ?? false)
            );

            ExibirFAQs(filtradas);
        }
    }
}

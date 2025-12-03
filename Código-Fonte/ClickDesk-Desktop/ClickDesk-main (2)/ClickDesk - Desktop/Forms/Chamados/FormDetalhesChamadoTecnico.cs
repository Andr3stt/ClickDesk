using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ClickDesk.Forms.Chamados
{
    /// <summary>
    /// Formulário de detalhes do chamado para técnicos.
    /// Interface moderna seguindo o padrão Web Clean.
    /// </summary>
    public partial class FormDetalhesChamadoTecnico : Form
    {
        private Ticket ticketAtual;

        /// <summary>
        /// Construtor padrão do formulário.
        /// </summary>
        public FormDetalhesChamadoTecnico()
        {
            InitializeComponent();
            ConfigurarFormulario();
            
            // Wire events
            if (this.btnVoltar != null) this.btnVoltar.Click += (s, e) => this.Close();
            if (this.btnAtualizar != null) this.btnAtualizar.Click += (s, e) => AtualizarDados();
            if (this.btnFecharChamado != null) this.btnFecharChamado.Click += (s, e) => FecharChamado();
            if (this.panelSidebar != null) this.panelSidebar.Paint += PanelSidebar_Paint;
            if (this.panelDetailCard != null) this.panelDetailCard.Paint += PanelDetailCard_Paint;
        }

        /// <summary>
        /// Construtor que recebe um ticket.
        /// </summary>
        public FormDetalhesChamadoTecnico(Ticket ticket) : this()
        {
            if (ticket != null)
            {
                CarregarChamado(ticket);
            }
        }

        /// <summary>
        /// Configura propriedades iniciais do formulário.
        /// </summary>
        private void ConfigurarFormulario()
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#EDE6D9");
            this.Text = "ClickDesk - Detalhes do Chamado";
        }

        /// <summary>
        /// Carrega e preenche os dados do chamado.
        /// </summary>
        public void CarregarChamado(Ticket ticket)
        {
            if (ticket == null) return;

            ticketAtual = ticket;

            // ID
            this.lblTicketId.Text = ticket.Id;
            this.lblTicketId.BackColor = ColorTranslator.FromHtml("#F28A1A");
            this.lblTicketId.ForeColor = Color.White;

            // Data
            this.lblTicketDate.Text = $"📅 {ticket.Data}";

            // Status com cor dinâmica
            this.lblTicketStatus.Text = ticket.Status.ToUpper();
            this.lblTicketStatus.BackColor = GetStatusBackColor(ticket.Status);
            this.lblTicketStatus.ForeColor = GetStatusTextColor(ticket.Status);

            // Informações em 4 colunas
            this.lblSolicitanteValor.Text = ticket.Solicitante;
            this.lblDepartamentoValor.Text = ticket.Departamento;
            this.lblCategoriaValor.Text = ticket.Categoria;
            this.lblPrioridadeValor.Text = ticket.Prioridade;
            this.lblPrioridadeValor.ForeColor = GetPrioridadeColor(ticket.Prioridade);

            // Título
            this.lblTituloChamado.Text = ticket.Titulo;

            // Descrição
            this.lblDescricaoChamado.Text = ticket.Descricao;
            this.lblDescricaoChamado.BackColor = ColorTranslator.FromHtml("#FBF6ED");

            // Anexos
            CarregarAnexos(ticket.Anexos);

            // Titulo da janela
            this.Text = $"ClickDesk - {ticket.Id} - {ticket.Titulo}";
        }

        /// <summary>
        /// Carrega os anexos do chamado.
        /// </summary>
        private void CarregarAnexos(List<dynamic> anexos)
        {
            this.panelAnexos.Controls.Clear();

            if (anexos == null || anexos.Count == 0)
            {
                Label lblVazio = new Label
                {
                    Text = "Nenhum anexo",
                    Font = new Font("Poppins", 9F),
                    ForeColor = ColorTranslator.FromHtml("#9CA3AF"),
                    AutoSize = true,
                    Margin = new Padding(5)
                };
                this.panelAnexos.Controls.Add(lblVazio);
                return;
            }

            foreach (var anexo in anexos)
            {
                Panel cardAnexo = CriarCardAnexo(anexo);
                this.panelAnexos.Controls.Add(cardAnexo);
            }
        }

        /// <summary>
        /// Cria um card de anexo.
        /// </summary>
        private Panel CriarCardAnexo(dynamic anexo)
        {
            Panel card = new Panel
            {
                Size = new Size(150, 100),
                BackColor = ColorTranslator.FromHtml("#F5F5F5"),
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblNome = new Label
            {
                Text = $"📄 {anexo.Nome}",
                Font = new Font("Poppins", 8F, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#1E2A22"),
                Location = new Point(5, 5),
                Size = new Size(140, 45),
                AutoSize = false,
                TextAlign = ContentAlignment.TopLeft,
                Dock = DockStyle.Top
            };
            card.Controls.Add(lblNome);

            Label lblTamanho = new Label
            {
                Text = anexo.Tamanho,
                Font = new Font("Poppins", 7F),
                ForeColor = ColorTranslator.FromHtml("#9CA3AF"),
                Location = new Point(5, 50),
                Size = new Size(140, 20),
                AutoSize = false
            };
            card.Controls.Add(lblTamanho);

            Button btnBaixar = new Button
            {
                Text = "⬇ Baixar",
                Font = new Font("Poppins", 7F, FontStyle.Bold),
                BackColor = ColorTranslator.FromHtml("#F28A1A"),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(5, 75),
                Size = new Size(140, 20),
                Cursor = Cursors.Hand
            };
            btnBaixar.FlatAppearance.BorderSize = 0;
            card.Controls.Add(btnBaixar);

            return card;
        }

        /// <summary>
        /// Retorna a cor de fundo do status.
        /// </summary>
        private Color GetStatusBackColor(string status)
        {
            switch (status?.ToLower())
            {
                case "aberto":
                    return ColorTranslator.FromHtml("#E8F5E9");
                case "em andamento":
                case "em análise":
                    return ColorTranslator.FromHtml("#E3F2FD");
                case "aguardando":
                    return ColorTranslator.FromHtml("#F3F0FF");
                case "fechado":
                    return ColorTranslator.FromHtml("#F5F5F5");
                default:
                    return ColorTranslator.FromHtml("#F5F5F5");
            }
        }

        /// <summary>
        /// Retorna a cor do texto do status.
        /// </summary>
        private Color GetStatusTextColor(string status)
        {
            switch (status?.ToLower())
            {
                case "aberto":
                    return ColorTranslator.FromHtml("#2E7D32");
                case "em andamento":
                case "em análise":
                    return ColorTranslator.FromHtml("#1565C0");
                case "aguardando":
                    return ColorTranslator.FromHtml("#6A1B9A");
                case "fechado":
                    return ColorTranslator.FromHtml("#424242");
                default:
                    return ColorTranslator.FromHtml("#424242");
            }
        }

        /// <summary>
        /// Retorna a cor da prioridade.
        /// </summary>
        private Color GetPrioridadeColor(string prioridade)
        {
            switch (prioridade?.ToLower())
            {
                case "urgente":
                case "crítica":
                    return ColorTranslator.FromHtml("#DC2626");
                case "alta":
                    return ColorTranslator.FromHtml("#F59E0B");
                case "normal":
                case "média":
                    return ColorTranslator.FromHtml("#10B981");
                case "baixa":
                    return ColorTranslator.FromHtml("#6B7280");
                default:
                    return ColorTranslator.FromHtml("#6B7280");
            }
        }

        /// <summary>
        /// Atualiza os dados do chamado.
        /// </summary>
        private void AtualizarDados()
        {
            MessageBox.Show("Dados atualizados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Fecha o chamado.
        /// </summary>
        private void FecharChamado()
        {
            var result = MessageBox.Show(
                "Tem certeza que deseja fechar este chamado?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                if (ticketAtual != null)
                {
                    ticketAtual.Status = "Fechado";
                }
                MessageBox.Show("Chamado fechado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        /// <summary>
        /// Desenha o card com bordas arredondadas.
        /// </summary>
        private void PanelDetailCard_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel)sender;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
            GraphicsPath path = GetRoundedRectPath(rect, 12);

            // Desenha a borda
            using (Pen pen = new Pen(ColorTranslator.FromHtml("#E5E5E5"), 1))
            {
                e.Graphics.DrawPath(pen, path);
            }

            // Sombra suave
            rect = new Rectangle(2, 2, panel.Width - 2, panel.Height - 2);
            path = GetRoundedRectPath(rect, 12);
            using (Brush shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
            {
                e.Graphics.FillPath(shadowBrush, path);
            }

            path.Dispose();
        }

        /// <summary>
        /// Desenha o sidebar com gradiente.
        /// </summary>
        private void PanelSidebar_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel)sender;
            
            // Gradiente suave
            Color cor1 = ColorTranslator.FromHtml("#F2EEE7");
            Color cor2 = ColorTranslator.FromHtml("#EFEAE2");
            
            using (LinearGradientBrush brush = new LinearGradientBrush(
                new Point(0, 0),
                new Point(panel.Width, 0),
                cor1,
                cor2))
            {
                e.Graphics.FillRectangle(brush, panel.ClientRectangle);
            }
        }

        /// <summary>
        /// Cria um GraphicsPath para retângulo com bordas arredondadas.
        /// </summary>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(rect.X + rect.Width - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(rect.X + rect.Width - radius * 2, rect.Y + rect.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - radius * 2, radius * 2, radius * 2, 90, 90);

            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// Classe que representa um ticket/chamado.
        /// </summary>
        public class Ticket
        {
            public string Id { get; set; }
            public string Titulo { get; set; }
            public string Categoria { get; set; }
            public string Solicitante { get; set; }
            public string Data { get; set; }
            public string Status { get; set; }
            public string Departamento { get; set; }
            public string Prioridade { get; set; }
            public string Descricao { get; set; }
            public List<dynamic> Anexos { get; set; } = new List<dynamic>();
        }
    }
}

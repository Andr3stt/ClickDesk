using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace ClickDesk.Forms.Chamados
{
    public enum TicketStatus
    {
        Aberto,
        EmAndamento,
        Resolvido,
        EmEspera
    }

    public class TicketItem
    {
        public string Numero { get; set; }
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public string Usuario { get; set; }
        public string Hora { get; set; }
        public TicketStatus Status { get; set; }
    }

    /// <summary>
    /// Formulário para listar todos os chamados com cards grandes estilo Web Clean.
    /// </summary>
    public partial class FormListaChamados : Form
    {
        private readonly List<TicketItem> _todosTickets = new List<TicketItem>();

        public FormListaChamados()
        {
            InitializeComponent();
            ConfigurarFormulario();
            CarregarDadosFake();
            WireEvents();
            AplicarFiltroERender();
        }

        /// <summary>
        /// Configura propriedades iniciais do formulário.
        /// </summary>
        private void ConfigurarFormulario()
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "ClickDesk - Lista de Chamados";
            this.BackColor = ColorTranslator.FromHtml("#EDE6D9");
        }

        /// <summary>
        /// Conecta os eventos dos controles.
        /// </summary>
        private void WireEvents()
        {
            this.cmbStatus.SelectedIndexChanged += (s, e) => AplicarFiltroERender();
            this.cmbCategoria.SelectedIndexChanged += (s, e) => AplicarFiltroERender();
            this.btnAtualizar.Click += (s, e) => AplicarFiltroERender();
        }

        /// <summary>
        /// Carrega dados fake dos chamados.
        /// </summary>
        private void CarregarDadosFake()
        {
            _todosTickets.Clear();

            _todosTickets.Add(new TicketItem 
            { 
                Numero = "#0156", 
                Titulo = "Sistema lento ao acessar relatórios", 
                Categoria = "Sistema",
                Usuario = "Carlos Silva",
                Hora = "Há 14:30",
                Status = TicketStatus.EmAndamento
            });

            _todosTickets.Add(new TicketItem 
            { 
                Numero = "#0155", 
                Titulo = "Erro ao gerar relatório de vendas", 
                Categoria = "Software",
                Usuario = "Maria Santos",
                Hora = "Há 12:15",
                Status = TicketStatus.Aberto
            });

            _todosTickets.Add(new TicketItem 
            { 
                Numero = "#0154", 
                Titulo = "Problema de conexão com impressora", 
                Categoria = "Hardware",
                Usuario = "João Oliveira",
                Hora = "Há 11:00",
                Status = TicketStatus.EmEspera
            });

            _todosTickets.Add(new TicketItem 
            { 
                Numero = "#0153", 
                Titulo = "Usuário não consegue fazer login", 
                Categoria = "Sistema",
                Usuario = "Ana Costa",
                Hora = "Há 09:45",
                Status = TicketStatus.Aberto
            });

            _todosTickets.Add(new TicketItem 
            { 
                Numero = "#0152", 
                Titulo = "Solicitação de nova licença de software", 
                Categoria = "Software",
                Usuario = "Pedro Alves",
                Hora = "Há 08:20",
                Status = TicketStatus.EmEspera
            });

            _todosTickets.Add(new TicketItem 
            { 
                Numero = "#0151", 
                Titulo = "Computador não liga - Verificar fonte", 
                Categoria = "Hardware",
                Usuario = "Lucia Ferreira",
                Hora = "Há 07:30",
                Status = TicketStatus.Resolvido
            });

            _todosTickets.Add(new TicketItem 
            { 
                Numero = "#0150", 
                Titulo = "E-mail não está sincronizando", 
                Categoria = "Rede",
                Usuario = "Roberto Lima",
                Hora = "Há 06:15",
                Status = TicketStatus.EmAndamento
            });

            _todosTickets.Add(new TicketItem 
            { 
                Numero = "#0149", 
                Titulo = "Solicitar instalação de novo software", 
                Categoria = "Software",
                Usuario = "Fernanda Silva",
                Hora = "Há 05:00",
                Status = TicketStatus.Aberto
            });
        }

        /// <summary>
        /// Filtra os tickets de acordo com os filtros selecionados.
        /// </summary>
        private IEnumerable<TicketItem> FiltrarTickets()
        {
            var tickets = _todosTickets.AsEnumerable();

            // Filtrar por status
            string statusSelecionado = this.cmbStatus.SelectedItem?.ToString() ?? "Todos os Status";
            if (statusSelecionado != "Todos os Status")
            {
                TicketStatus statusEnum = (TicketStatus)Enum.Parse(typeof(TicketStatus), statusSelecionado.Replace(" ", ""));
                tickets = tickets.Where(t => t.Status == statusEnum);
            }

            // Filtrar por categoria
            string categoriaSelecionada = this.cmbCategoria.SelectedItem?.ToString() ?? "Todas as Categorias";
            if (categoriaSelecionada != "Todas as Categorias")
            {
                tickets = tickets.Where(t => t.Categoria == categoriaSelecionada);
            }

            return tickets;
        }

        /// <summary>
        /// Aplica os filtros e renderiza os dados.
        /// </summary>
        private void AplicarFiltroERender()
        {
            var ticketsFiltrados = FiltrarTickets().ToList();
            RenderizarKPIs(ticketsFiltrados);
            RenderizarCards(ticketsFiltrados);
        }

        /// <summary>
        /// Renderiza os KPIs (cards de estatísticas).
        /// </summary>
        private void RenderizarKPIs(List<TicketItem> tickets)
        {
            // Total de chamados
            var lblTotal = this.Controls.OfType<Panel>()
                .FirstOrDefault(p => p.Name == "panelKpis")?
                .Controls.OfType<Label>()
                .FirstOrDefault(l => l.Name == "lblTotal");
            if (lblTotal != null) lblTotal.Text = _todosTickets.Count.ToString();

            // Chamados atendidos (Resolvido)
            var lblAtendidos = this.Controls.OfType<Panel>()
                .FirstOrDefault(p => p.Name == "panelKpis")?
                .Controls.OfType<Label>()
                .FirstOrDefault(l => l.Name == "lblAtendidos");
            if (lblAtendidos != null) 
                lblAtendidos.Text = _todosTickets.Count(t => t.Status == TicketStatus.Resolvido).ToString();

            // Chamados em espera
            var lblEspera = this.Controls.OfType<Panel>()
                .FirstOrDefault(p => p.Name == "panelKpis")?
                .Controls.OfType<Label>()
                .FirstOrDefault(l => l.Name == "lblEspera");
            if (lblEspera != null) 
                lblEspera.Text = _todosTickets.Count(t => t.Status == TicketStatus.EmEspera).ToString();

            // Chamados em progresso
            var lblProgresso = this.Controls.OfType<Panel>()
                .FirstOrDefault(p => p.Name == "panelKpis")?
                .Controls.OfType<Label>()
                .FirstOrDefault(l => l.Name == "lblProgresso");
            if (lblProgresso != null) 
                lblProgresso.Text = _todosTickets.Count(t => t.Status == TicketStatus.EmAndamento).ToString();
        }

        /// <summary>
        /// Renderiza os cards de chamados.
        /// </summary>
        private void RenderizarCards(List<TicketItem> tickets)
        {
            this.flowTickets.Controls.Clear();

            foreach (var ticket in tickets)
            {
                var card = CriarCardTicket(ticket);
                this.flowTickets.Controls.Add(card);
            }
        }

        /// <summary>
        /// Cria um card de ticket.
        /// </summary>
        private Control CriarCardTicket(TicketItem ticket)
        {
            var card = new Panel
            {
                Size = new Size(380, 180),
                BackColor = Color.White,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Paint event para bordas arredondadas
            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var path = RoundedRect(new Rectangle(0, 0, card.Width - 1, card.Height - 1), 8);
                using (var pen = new Pen(ColorTranslator.FromHtml("#E5E5E5"), 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            };

            // ===== HEADER (Número + Status) =====
            var panelHeader = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(360, 40),
                BackColor = Color.White
            };

            var lblNumero = new Label
            {
                Text = ticket.Numero,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = ColorTranslator.FromHtml("#F28A1A"),
                Location = new Point(0, 0),
                Size = new Size(80, 35),
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = "numero"
            };
            lblNumero.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var path = RoundedRect(new Rectangle(0, 0, lblNumero.Width - 1, lblNumero.Height - 1), 4);
                using (var brush = new SolidBrush(ColorTranslator.FromHtml("#F28A1A")))
                {
                    e.Graphics.FillPath(brush, path);
                }
                using (var brush = new SolidBrush(Color.White))
                {
                    e.Graphics.DrawString(lblNumero.Text, lblNumero.Font, brush, 
                        new RectangleF(0, 0, lblNumero.Width, lblNumero.Height),
                        new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }
            };
            panelHeader.Controls.Add(lblNumero);

            // Status badge
            var statusColors = new Dictionary<TicketStatus, (string bg, string text)>
            {
                { TicketStatus.Aberto, ("#FEF3C7", "#92400E") },
                { TicketStatus.EmAndamento, ("#DBEAFE", "#1E40AF") },
                { TicketStatus.Resolvido, ("#DCFCE7", "#166534") },
                { TicketStatus.EmEspera, ("#FEE2E2", "#991B1B") }
            };

            var (bgColor, textColor) = statusColors[ticket.Status];

            var lblStatus = new Label
            {
                Text = ticket.Status.ToString().Replace("EmAndamento", "Em andamento").Replace("EmEspera", "Em espera"),
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml(textColor),
                BackColor = ColorTranslator.FromHtml(bgColor),
                Location = new Point(300, 5),
                Size = new Size(60, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            lblStatus.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var path = RoundedRect(new Rectangle(0, 0, lblStatus.Width - 1, lblStatus.Height - 1), 4);
                using (var brush = new SolidBrush(ColorTranslator.FromHtml(bgColor)))
                {
                    e.Graphics.FillPath(brush, path);
                }
                using (var brush = new SolidBrush(ColorTranslator.FromHtml(textColor)))
                {
                    e.Graphics.DrawString(lblStatus.Text, lblStatus.Font, brush,
                        new RectangleF(0, 0, lblStatus.Width, lblStatus.Height),
                        new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }
            };
            panelHeader.Controls.Add(lblStatus);
            card.Controls.Add(panelHeader);

            // ===== TÍTULO =====
            var lblTitulo = new Label
            {
                Text = ticket.Titulo,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#1A1A1A"),
                Location = new Point(10, 55),
                Size = new Size(360, 40),
                AutoSize = false,
                TextAlign = ContentAlignment.TopLeft
            };
            card.Controls.Add(lblTitulo);

            // ===== LINHA DE META =====
            var lblMeta = new Label
            {
                Text = $"🏷️ {ticket.Categoria}   •   ⏰ {ticket.Hora}   •   👤 {ticket.Usuario}",
                Font = new Font("Segoe UI", 8F),
                ForeColor = ColorTranslator.FromHtml("#666666"),
                Location = new Point(10, 100),
                Size = new Size(360, 20),
                AutoSize = false
            };
            card.Controls.Add(lblMeta);

            // ===== BOTÃO "VER DETALHES" =====
            var btnVerDetalhes = new Button
            {
                Text = "Ver detalhes →",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = ColorTranslator.FromHtml("#F28A1A"),
                Location = new Point(10, 140),
                Size = new Size(360, 30),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = ticket
            };
            btnVerDetalhes.FlatAppearance.BorderSize = 0;
            btnVerDetalhes.Click += (s, e) =>
            {
                new FormDetalhesChamadoTecnico(ConvertToDesignerTicket(ticket)).ShowDialog();
            };
            card.Controls.Add(btnVerDetalhes);

            return card;
        }

        /// <summary>
        /// Converte um Ticket local em Ticket do Designer.
        /// </summary>
        private FormDetalhesChamadoTecnico.Ticket ConvertToDesignerTicket(TicketItem ticket)
        {
            return new FormDetalhesChamadoTecnico.Ticket
            {
                Id = ticket.Numero,
                Titulo = ticket.Titulo,
                Categoria = ticket.Categoria,
                Solicitante = ticket.Usuario,
                Data = ticket.Hora,
                Status = ticket.Status.ToString(),
                Departamento = "TI",
                Prioridade = "Normal",
                Descricao = $"Chamado {ticket.Numero}: {ticket.Titulo}",
                Anexos = new List<dynamic>()
            };
        }

        /// <summary>
        /// Cria um GraphicsPath para retângulo com bordas arredondadas.
        /// </summary>
        private static GraphicsPath RoundedRect(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.X + rect.Width - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.X + rect.Width - d, rect.Y + rect.Height - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - d, d, d, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}



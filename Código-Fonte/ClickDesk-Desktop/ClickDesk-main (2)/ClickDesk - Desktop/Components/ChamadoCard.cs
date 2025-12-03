using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ClickDesk.Components
{
    /// <summary>
    /// UserControl representando um card de chamado/ticket.
    /// Design moderno com hover effects e ações integradas.
    /// </summary>
    public partial class ChamadoCard : UserControl
    {
        // ===== EVENTOS PÚBLICOS =====
        public event EventHandler<Ticket> OnAprovar;
        public event EventHandler<Ticket> OnRecusar;
        public event EventHandler<Ticket> OnVisualizar;

        // ===== PROPRIEDADES =====
        private Ticket ticket;
        private bool isHovered = false;

        /// <summary>
        /// Ticket associado a este card.
        /// </summary>
        public Ticket Ticket 
        { 
            get => ticket; 
            set 
            { 
                ticket = value; 
                PreencherDados(); 
            } 
        }

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public ChamadoCard()
        {
            InitializeComponent();
            SetupCardStyle();
            WireEvents();
        }

        /// <summary>
        /// Construtor com ticket.
        /// </summary>
        public ChamadoCard(Ticket ticket) : this()
        {
            this.Ticket = ticket;
        }

        /// <summary>
        /// Configura o estilo visual do card.
        /// </summary>
        private void SetupCardStyle()
        {
            // Card dimensions and styling
            this.Size = new Size(350, 200);
            this.BackColor = Color.White;
            this.Margin = new Padding(10);
            this.Padding = new Padding(0);

            // Enable double buffering for smooth rendering
            SetStyle(ControlStyles.AllPaintingInWmPaint | 
                    ControlStyles.UserPaint | 
                    ControlStyles.DoubleBuffer | 
                    ControlStyles.ResizeRedraw, true);
        }

        /// <summary>
        /// Conecta eventos dos controles.
        /// </summary>
        private void WireEvents()
        {
            // Mouse hover effects
            this.MouseEnter += (s, e) => { isHovered = true; this.Invalidate(); };
            this.MouseLeave += (s, e) => { isHovered = false; this.Invalidate(); };

            // Propagar eventos de mouse para todos os controles filhos
            foreach (Control control in this.Controls)
            {
                PropagateMouseEvents(control);
            }

            // Button click events
            this.btnAprovar.Click += (s, e) => OnAprovar?.Invoke(this, this.Ticket);
            this.btnRecusar.Click += (s, e) => OnRecusar?.Invoke(this, this.Ticket);
            
            // Card click for details
            this.Click += (s, e) => OnVisualizar?.Invoke(this, this.Ticket);
        }

        /// <summary>
        /// Propaga eventos de mouse para controles filhos.
        /// </summary>
        private void PropagateMouseEvents(Control control)
        {
            control.MouseEnter += (s, e) => { isHovered = true; this.Invalidate(); };
            control.MouseLeave += (s, e) => { isHovered = false; this.Invalidate(); };
            
            foreach (Control child in control.Controls)
            {
                PropagateMouseEvents(child);
            }
        }

        /// <summary>
        /// Preenche os dados do ticket no card.
        /// </summary>
        private void PreencherDados()
        {
            if (ticket == null) return;

            // ID Chip
            lblIdChip.Text = ticket.Id;

            // Status Chip com cor baseada no status
            lblStatusChip.Text = ticket.Status;
            lblStatusChip.BackColor = GetStatusColor(ticket.Status);
            lblStatusChip.ForeColor = GetStatusTextColor(ticket.Status);

            // Título
            lblTitulo.Text = ticket.Titulo;

            // Descrição (limitada)
            string desc = ticket.Descricao;
            if (desc.Length > 80)
            {
                desc = desc.Substring(0, 80) + "...";
            }
            lblDescricao.Text = desc;

            // Meta information
            lblCategoria.Text = ticket.Categoria;
            lblData.Text = ticket.Data.ToString("dd/MM/yyyy");
            lblPrioridade.Text = ticket.Prioridade;
            lblResponsavel.Text = ticket.Responsavel ?? "Não atribuído";

            // Prioridade color
            lblPrioridade.ForeColor = GetPrioridadeColor(ticket.Prioridade);
        }

        /// <summary>
        /// Retorna a cor do status.
        /// </summary>
        private Color GetStatusColor(string status)
        {
            switch (status?.ToLower())
            {
                case "pendente":
                    return ColorTranslator.FromHtml("#FEF3C7"); // Yellow background
                case "em análise":
                case "em analise":
                    return ColorTranslator.FromHtml("#DBEAFE"); // Blue background
                case "aguardando":
                    return ColorTranslator.FromHtml("#FCE7F3"); // Pink background
                case "aprovado":
                    return ColorTranslator.FromHtml("#D1FAE5"); // Green background
                case "recusado":
                    return ColorTranslator.FromHtml("#FEE2E2"); // Red background
                default:
                    return ColorTranslator.FromHtml("#F3F4F6"); // Gray background
            }
        }

        /// <summary>
        /// Retorna a cor do texto do status.
        /// </summary>
        private Color GetStatusTextColor(string status)
        {
            switch (status?.ToLower())
            {
                case "pendente":
                    return ColorTranslator.FromHtml("#92400E"); // Yellow text
                case "em análise":
                case "em analise":
                    return ColorTranslator.FromHtml("#1E40AF"); // Blue text
                case "aguardando":
                    return ColorTranslator.FromHtml("#BE185D"); // Pink text
                case "aprovado":
                    return ColorTranslator.FromHtml("#065F46"); // Green text
                case "recusado":
                    return ColorTranslator.FromHtml("#B91C1C"); // Red text
                default:
                    return ColorTranslator.FromHtml("#374151"); // Gray text
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
                    return ColorTranslator.FromHtml("#DC2626"); // Red
                case "alta":
                    return ColorTranslator.FromHtml("#F59E0B"); // Orange
                case "normal":
                    return ColorTranslator.FromHtml("#10B981"); // Green
                case "baixa":
                    return ColorTranslator.FromHtml("#6B7280"); // Gray
                default:
                    return ColorTranslator.FromHtml("#6B7280"); // Gray
            }
        }

        /// <summary>
        /// Desenha o card com bordas arredondadas e sombra.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Card background with rounded corners
            Rectangle cardRect = new Rectangle(2, 2, this.Width - 4, this.Height - 4);
            
            // Shadow effect (only when hovered)
            if (isHovered)
            {
                Rectangle shadowRect = new Rectangle(4, 4, this.Width - 4, this.Height - 4);
                using (Brush shadowBrush = new SolidBrush(ColorTranslator.FromHtml("#E5E7EB")))
                {
                    g.FillPath(shadowBrush, GetRoundedRectPath(shadowRect, 8));
                }
            }

            // Main card background
            using (Brush cardBrush = new SolidBrush(Color.White))
            {
                g.FillPath(cardBrush, GetRoundedRectPath(cardRect, 8));
            }

            // Border
            Color borderColor = isHovered ? 
                ColorTranslator.FromHtml("#F28A1A") : // Orange border on hover
                ColorTranslator.FromHtml("#E5E7EB");  // Light gray border normally
                
            using (Pen borderPen = new Pen(borderColor, isHovered ? 2 : 1))
            {
                g.DrawPath(borderPen, GetRoundedRectPath(cardRect, 8));
            }

            // Orange accent bar at top (tarja)
            Rectangle tarjaRect = new Rectangle(cardRect.X, cardRect.Y, cardRect.Width, 4);
            using (Brush tarjaBrush = new SolidBrush(ColorTranslator.FromHtml("#F28A1A")))
            {
                GraphicsPath tarjaPath = new GraphicsPath();
                tarjaPath.AddPath(GetRoundedRectPath(tarjaRect, 8), false);
                g.FillPath(tarjaBrush, tarjaPath);
            }
        }

        /// <summary>
        /// Cria um GraphicsPath para retângulo com bordas arredondadas.
        /// </summary>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            
            // Top-left corner
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            
            // Top-right corner
            path.AddArc(rect.X + rect.Width - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            
            // Bottom-right corner
            path.AddArc(rect.X + rect.Width - radius * 2, rect.Y + rect.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            
            // Bottom-left corner
            path.AddArc(rect.X, rect.Y + rect.Height - radius * 2, radius * 2, radius * 2, 90, 90);
            
            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// Override do método de disposição de recursos.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose managed resources
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }

    /// <summary>
    /// Classe Ticket para usar com o ChamadoCard (caso não exista).
    /// </summary>
    public class Ticket
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public DateTime Data { get; set; }
        public string Prioridade { get; set; }
        public string Responsavel { get; set; }
        public string Status { get; set; }
    }
}
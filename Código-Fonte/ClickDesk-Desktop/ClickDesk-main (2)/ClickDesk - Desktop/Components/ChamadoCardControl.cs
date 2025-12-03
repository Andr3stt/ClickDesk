using System;
using System.Drawing;
using System.Windows.Forms;
using ClickDesk.Models;

namespace ClickDesk.Components
{
    public partial class ChamadoCardControl : UserControl
    {
        public Chamado ChamadoAtual { get; private set; }

        public event EventHandler<int> OnAprovar;
        public event EventHandler<int> OnRecusar;
        public event EventHandler<int> OnVisualizar;

        private bool _hover = false;

        public ChamadoCardControl()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            this.Padding = new Padding(20);
            this.Cursor = Cursors.Hand;

            this.MouseEnter += (s, e) => { _hover = true; this.Invalidate(); };
            this.MouseLeave += (s, e) => { _hover = false; this.Invalidate(); };

            btnAprovar.Click += (s, e) => OnAprovar?.Invoke(this, ChamadoAtual.Id);
            btnRecusar.Click += (s, e) => OnRecusar?.Invoke(this, ChamadoAtual.Id);
            this.Click += (s, e) => OnVisualizar?.Invoke(this, ChamadoAtual.Id);
        }

        public void Preencher(Chamado c)
        {
            ChamadoAtual = c;

            lblId.Text = $"CD-{c.Id}";
            lblTitulo.Text = c.Assunto;
            lblDescricao.Text = c.DescricaoCurta;
            lblCategoria.Text = c.Categoria.ToString();
            lblData.Text = c.DataCriacao.ToString("dd/MM/yyyy");
            lblPrioridade.Text = c.Prioridade.ToString();

            switch (c.Status)
            {
                case ChamadoStatus.ABERTO:
                    status.BackColor = Color.FromArgb(232, 245, 233);
                    status.ForeColor = Color.FromArgb(56, 142, 60);
                    break;
                case ChamadoStatus.EM_ATENDIMENTO:
                    status.BackColor = Color.FromArgb(227, 242, 253);
                    status.ForeColor = Color.FromArgb(25, 118, 210);
                    break;
                case ChamadoStatus.PENDENTE:
                    status.BackColor = Color.FromArgb(255, 243, 224);
                    status.ForeColor = Color.FromArgb(239, 108, 0);
                    break;
                case ChamadoStatus.FECHADO:
                    status.BackColor = Color.FromArgb(245, 245, 245);
                    status.ForeColor = Color.FromArgb(117, 117, 117);
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int radius = 20;

            g.FillRectangle(new SolidBrush(Color.FromArgb(_hover ? 40 : 20, 0, 0, 0)),
                new Rectangle(4, 4, Width - 8, Height - 8));

            g.FillRoundedRectangle(
                new SolidBrush(Color.White),
                new Rectangle(0, 0, Width - 8, Height - 8),
                radius);

            g.FillRectangle(
                new SolidBrush(Color.FromArgb(242, 138, 26)),
                new Rectangle(0, 0, Width - 8, 4));
        }
    }
}

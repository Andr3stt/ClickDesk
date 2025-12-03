using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClickDesk.Forms
{
    public partial class UCCategoriaFAQ : UserControl
    {
        private string categoriaTitulo;
        private bool isExpanded = true;

        public UCCategoriaFAQ(string titulo)
        {
            InitializeComponent();
            categoriaTitulo = titulo;
            lblTitle.Text = titulo;
            btnToggle.Click += BtnToggle_Click;
        }

        public void AddPergunta(string pergunta, string resposta)
        {
            CriarPergunta(pergunta, resposta);
        }

        private void CriarPergunta(string pergunta, string resposta)
        {
            // Panel para cada pergunta/resposta
            Panel pnlPergunta = new Panel();
            pnlPergunta.BackColor = Color.White;
            pnlPergunta.Margin = new Padding(0, 5, 0, 5);
            pnlPergunta.BorderStyle = BorderStyle.FixedSingle;
            pnlPergunta.Height = 50;
            pnlPergunta.Width = contentPanel.Width - 20;

            // Botão para a pergunta
            Button btnPergunta = new Button();
            btnPergunta.BackColor = Color.White;
            btnPergunta.FlatStyle = FlatStyle.Flat;
            btnPergunta.FlatAppearance.BorderSize = 0;
            btnPergunta.Text = "▼ " + pergunta;
            btnPergunta.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnPergunta.ForeColor = Color.FromArgb(30, 42, 34);
            btnPergunta.Dock = DockStyle.Top;
            btnPergunta.Height = 50;
            btnPergunta.TextAlign = ContentAlignment.MiddleLeft;
            btnPergunta.Padding = new Padding(10, 0, 0, 0);
            btnPergunta.Cursor = Cursors.Hand;

            // Panel para a resposta
            Panel pnlResposta = new Panel();
            pnlResposta.BackColor = Color.FromArgb(245, 239, 230);
            pnlResposta.Dock = DockStyle.Fill;
            pnlResposta.Padding = new Padding(15, 10, 15, 10);
            pnlResposta.AutoScroll = true;
            pnlResposta.Visible = true;

            // Label da resposta
            Label lblResposta = new Label();
            lblResposta.Text = resposta;
            lblResposta.Font = new Font("Segoe UI", 10F);
            lblResposta.ForeColor = Color.FromArgb(111, 111, 111);
            lblResposta.Dock = DockStyle.Fill;
            lblResposta.AutoSize = false;
            lblResposta.WordWrap = true;

            pnlResposta.Controls.Add(lblResposta);
            pnlPergunta.Controls.Add(pnlResposta);
            pnlPergunta.Controls.Add(btnPergunta);

            // Event handler para toggle
            btnPergunta.Click += (s, e) =>
            {
                pnlResposta.Visible = !pnlResposta.Visible;
                btnPergunta.Text = (pnlResposta.Visible ? "▼ " : "▶ ") + pergunta;
                AtualizarAltura();
            };

            contentPanel.Controls.Add(pnlPergunta);
            AtualizarAltura();
        }

        private void AtualizarAltura()
        {
            int totalHeight = headerPanel.Height + 20;
            foreach (Control control in contentPanel.Controls)
            {
                if (control is Panel)
                {
                    totalHeight += control.Height + 10;
                }
            }
            this.Height = totalHeight;
        }

        private void BtnToggle_Click(object sender, EventArgs e)
        {
            isExpanded = !isExpanded;
            contentPanel.Visible = isExpanded;
            btnToggle.Text = isExpanded ? "▼" : "▶";
            this.Height = isExpanded ? this.Height : headerPanel.Height;
        }
    }
}

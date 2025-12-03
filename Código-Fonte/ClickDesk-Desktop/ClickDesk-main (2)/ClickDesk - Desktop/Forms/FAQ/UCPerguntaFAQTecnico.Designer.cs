using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClickDesk.Forms
{
    partial class UCPerguntaFAQTecnico
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelPergunta;
        private Label lblPergunta;
        private Button btnPergunta;
        private Label lblResposta;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            this.SuspendLayout();
            this.BackColor = Color.FromArgb(247, 243, 236);
            this.Size = new Size(780, 90);

            // Pergunta (header)
            panelPergunta = new Panel();
            panelPergunta.Dock = DockStyle.Top;
            panelPergunta.Height = 45;
            panelPergunta.BackColor = Color.FromArgb(234, 229, 215);
            panelPergunta.Cursor = Cursors.Hand;
            panelPergunta.Click += btnPergunta_Click;
            this.Controls.Add(panelPergunta);

            lblPergunta = new Label();
            lblPergunta.Text = "Pergunta";
            lblPergunta.AutoSize = true;
            lblPergunta.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblPergunta.ForeColor = Color.Black;
            lblPergunta.Location = new Point(20, 12);
            lblPergunta.Click += btnPergunta_Click;
            panelPergunta.Controls.Add(lblPergunta);

            btnPergunta = new Button();
            btnPergunta.Text = "▶";
            btnPergunta.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnPergunta.Size = new Size(40, 32);
            btnPergunta.Location = new Point(720, 7);
            btnPergunta.BackColor = Color.Transparent;
            btnPergunta.FlatStyle = FlatStyle.Flat;
            btnPergunta.FlatAppearance.BorderSize = 0;
            btnPergunta.Click += btnPergunta_Click;
            panelPergunta.Controls.Add(btnPergunta);

            // Resposta
            lblResposta = new Label();
            lblResposta.Text = "Resposta";
            lblResposta.Font = new Font("Segoe UI", 10);
            lblResposta.ForeColor = Color.FromArgb(45, 45, 45);
            lblResposta.MaximumSize = new Size(740, 0);
            lblResposta.Location = new Point(20, 55);
            lblResposta.AutoSize = true;
            lblResposta.Visible = false;
            this.Controls.Add(lblResposta);

            this.ResumeLayout(false);
        }
    }
}

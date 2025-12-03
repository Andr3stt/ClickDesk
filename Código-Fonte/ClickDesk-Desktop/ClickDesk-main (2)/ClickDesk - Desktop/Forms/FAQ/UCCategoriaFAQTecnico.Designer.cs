using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClickDesk.Forms
{
    partial class UCCategoriaFAQTecnico
    {
        private System.ComponentModel.IContainer components = null;

        private Panel header;
        private Label lblCategoria;
        private Button btnToggle;
        private FlowLayoutPanel flowPerguntas;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            this.SuspendLayout();
            this.BackColor = Color.White;
            this.Size = new Size(820, 80);

            // HEADER
            header = new Panel();
            header.Dock = DockStyle.Top;
            header.Height = 50;
            header.BackColor = Color.FromArgb(242, 138, 26);
            header.Cursor = Cursors.Hand;
            header.Click += btnHeader_Click;
            this.Controls.Add(header);

            lblCategoria = new Label();
            lblCategoria.Text = "Categoria";
            lblCategoria.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblCategoria.ForeColor = Color.White;
            lblCategoria.AutoSize = true;
            lblCategoria.Location = new Point(20, 14);
            lblCategoria.Click += btnHeader_Click;
            header.Controls.Add(lblCategoria);

            btnToggle = new Button();
            btnToggle.Text = "▲";
            btnToggle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnToggle.FlatStyle = FlatStyle.Flat;
            btnToggle.FlatAppearance.BorderSize = 0;
            btnToggle.Size = new Size(40, 40);
            btnToggle.Location = new Point(760, 5);
            btnToggle.Click += btnHeader_Click;
            header.Controls.Add(btnToggle);

            // PERGUNTAS
            flowPerguntas = new FlowLayoutPanel();
            flowPerguntas.Dock = DockStyle.Fill;
            flowPerguntas.FlowDirection = FlowDirection.TopDown;
            flowPerguntas.WrapContents = false;
            flowPerguntas.AutoSize = true;
            flowPerguntas.AutoScroll = false;
            flowPerguntas.Visible = false;
            flowPerguntas.Padding = new Padding(15);
            this.Controls.Add(flowPerguntas);

            this.ResumeLayout(false);
        }
    }
}

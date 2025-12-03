using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClickDesk.Forms
{
    partial class UCCategoriaFAQ
    {
        private System.ComponentModel.IContainer components = null;

        private Panel headerPanel;
        private Label lblTitle;
        private Button btnToggle;
        private Panel contentPanel;

        private void InitializeComponent()
        {
            this.headerPanel = new Panel();
            this.lblTitle = new Label();
            this.btnToggle = new Button();
            this.contentPanel = new Panel();

            this.SuspendLayout();

            // Header Panel
            this.headerPanel.BackColor = Color.FromArgb(242, 138, 26);
            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Height = 50;
            this.headerPanel.Padding = new Padding(15, 10, 15, 10);

            // Title Label
            this.lblTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Text = "Categoria";
            this.lblTitle.Dock = DockStyle.Left;
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            this.lblTitle.AutoSize = false;
            this.lblTitle.Width = 400;

            // Toggle Button
            this.btnToggle.BackColor = Color.Transparent;
            this.btnToggle.FlatStyle = FlatStyle.Flat;
            this.btnToggle.FlatAppearance.BorderSize = 0;
            this.btnToggle.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.btnToggle.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.btnToggle.Font = new Font("Arial", 10F, FontStyle.Bold);
            this.btnToggle.ForeColor = Color.White;
            this.btnToggle.Text = "▼";
            this.btnToggle.Dock = DockStyle.Right;
            this.btnToggle.Width = 40;
            this.btnToggle.Cursor = Cursors.Hand;

            this.headerPanel.Controls.Add(this.lblTitle);
            this.headerPanel.Controls.Add(this.btnToggle);

            // Content Panel
            this.contentPanel.BackColor = Color.WhiteSmoke;
            this.contentPanel.Dock = DockStyle.Fill;
            this.contentPanel.AutoScroll = true;
            this.contentPanel.Padding = new Padding(10);

            // Form Settings
            this.AutoSize = true;
            this.AutoScroll = false;
            this.Size = new Size(800, 100);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPanel);

            this.ResumeLayout(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

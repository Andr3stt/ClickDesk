using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClickDesk
{
    partial class FormFAQTecnico
    {
        private System.ComponentModel.IContainer components = null;

        private Panel topbar;
        private Label title;
        private FlowLayoutPanel faqAccordion;
        private Button expandAll;
        private Button collapseAll;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.topbar = new Panel();
            this.title = new Label();
            this.expandAll = new Button();
            this.collapseAll = new Button();
            this.faqAccordion = new FlowLayoutPanel();

            this.SuspendLayout();

            // TOPBAR
            this.topbar.Dock = DockStyle.Top;
            this.topbar.Height = 60;
            this.topbar.BackColor = Color.WhiteSmoke;
            this.topbar.Padding = new Padding(20);

            // TITLE
            this.title.Text = "FAQ - Área Técnica";
            this.title.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            this.title.AutoSize = true;

            // EXPAND ALL
            this.expandAll.Text = "Expandir tudo";
            this.expandAll.Width = 140;
            this.expandAll.Height = 28;
            this.expandAll.BackColor = Color.White;
            this.expandAll.FlatStyle = FlatStyle.Flat;
            this.expandAll.Location = new Point(600, 15);
            this.expandAll.Click += new EventHandler(expandAll_Click);

            // COLLAPSE ALL
            this.collapseAll.Text = "Recolher tudo";
            this.collapseAll.Width = 140;
            this.collapseAll.Height = 28;
            this.collapseAll.BackColor = Color.White;
            this.collapseAll.FlatStyle = FlatStyle.Flat;
            this.collapseAll.Location = new Point(750, 15);
            this.collapseAll.Click += new EventHandler(collapseAll_Click);

            this.topbar.Controls.Add(this.title);
            this.topbar.Controls.Add(this.expandAll);
            this.topbar.Controls.Add(this.collapseAll);

            // FAQ ACCORDION
            this.faqAccordion.Dock = DockStyle.Fill;
            this.faqAccordion.Padding = new Padding(20);
            this.faqAccordion.AutoScroll = true;
            this.faqAccordion.FlowDirection = FlowDirection.TopDown;
            this.faqAccordion.WrapContents = false;
            this.faqAccordion.BackColor = Color.White;

            // FORM
            this.Controls.Add(this.faqAccordion);
            this.Controls.Add(this.topbar);
            this.Text = "FAQ Técnico ADM";
            this.BackColor = Color.White;
            this.MinimumSize = new Size(900, 600);

            this.ResumeLayout(false);
        }
    }
}

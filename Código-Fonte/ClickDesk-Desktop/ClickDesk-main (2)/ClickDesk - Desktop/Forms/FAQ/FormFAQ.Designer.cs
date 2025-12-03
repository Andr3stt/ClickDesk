using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClickDesk
{
    partial class FormFAQ
    {
        private Panel sidebar;
        private Panel topbar;
        private Label pageTitle;
        private FlowLayoutPanel faqContainer;
        private Button btnExpandAll;
        private Button btnCollapseAll;
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.sidebar = new Panel();
            this.topbar = new Panel();
            this.pageTitle = new Label();
            this.faqContainer = new FlowLayoutPanel();
            this.btnExpandAll = new Button();
            this.btnCollapseAll = new Button();

            this.SuspendLayout();

            // SIDEBAR
            this.sidebar.BackColor = Color.FromArgb(237, 230, 217);
            this.sidebar.Dock = DockStyle.Left;
            this.sidebar.Width = 240;

            // TOPBAR
            this.topbar.BackColor = Color.White;
            this.topbar.Dock = DockStyle.Top;
            this.topbar.Height = 70;
            this.topbar.Controls.Add(this.pageTitle);

            // PAGE TITLE
            this.pageTitle.Text = "Perguntas Frequentes";
            this.pageTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.pageTitle.ForeColor = Color.FromArgb(30, 42, 34);
            this.pageTitle.Location = new Point(20, 15);
            this.pageTitle.AutoSize = true;

            // FAQ CONTAINER
            this.faqContainer.Dock = DockStyle.Fill;
            this.faqContainer.FlowDirection = FlowDirection.TopDown;
            this.faqContainer.AutoScroll = true;
            this.faqContainer.WrapContents = false;
            this.faqContainer.Padding = new Padding(20);
            this.faqContainer.BackColor = Color.FromArgb(245, 239, 230);

            // BOTÃO EXPANDIR
            this.btnExpandAll.Text = "Expandir tudo";
            this.btnExpandAll.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.btnExpandAll.BackColor = Color.White;
            this.btnExpandAll.FlatStyle = FlatStyle.Flat;
            this.btnExpandAll.Size = new Size(120, 35);
            this.btnExpandAll.Location = new Point(260, 80);

            // BOTÃO RECOLHER
            this.btnCollapseAll.Text = "Recolher tudo";
            this.btnCollapseAll.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.btnCollapseAll.BackColor = Color.White;
            this.btnCollapseAll.FlatStyle = FlatStyle.Flat;
            this.btnCollapseAll.Size = new Size(120, 35);
            this.btnCollapseAll.Location = new Point(390, 80);

            // FORM
            this.Controls.Add(this.faqContainer);
            this.Controls.Add(this.topbar);
            this.Controls.Add(this.sidebar);
            this.Controls.Add(this.btnExpandAll);
            this.Controls.Add(this.btnCollapseAll);

            this.Text = "FAQ";
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "FormFAQ";
            
            this.ResumeLayout(false);
        }

        #endregion
    }
}

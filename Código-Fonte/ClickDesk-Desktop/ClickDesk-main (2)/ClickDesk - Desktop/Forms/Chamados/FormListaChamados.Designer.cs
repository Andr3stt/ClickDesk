namespace ClickDesk.Forms.Chamados
{
    partial class FormListaChamados
    {
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
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();

            // ========== SIDEBAR ==========
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Width = 240;
            this.panelSidebar.BackColor = System.Drawing.ColorTranslator.FromHtml("#F4EFE6");
            this.panelSidebar.AutoScroll = true;

            // Logo
            this.lblLogo = new System.Windows.Forms.Label();
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Text = "🔷 ClickDesk";
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1A1A1A");
            this.lblLogo.Location = new System.Drawing.Point(15, 20);
            this.lblLogo.Size = new System.Drawing.Size(210, 40);
            this.lblLogo.AutoSize = false;
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.panelSidebar.Controls.Add(this.lblLogo);

            // Menu buttons
            int btnY = 80;
            string[] menuLabels = { "📊 Dashboard", "➕ Novo Chamado", "👤 Meus Chamados", "📋 Lista de Chamados", "❓ FAQ" };
            string[] menuNames = { "btnDashboard", "btnNovoChamado", "btnMeusChamados", "btnListaChamados", "btnFaq" };

            this.menuButtons = new System.Windows.Forms.Button[5];

            for (int i = 0; i < menuLabels.Length; i++)
            {
                this.menuButtons[i] = new System.Windows.Forms.Button();
                this.menuButtons[i].Name = menuNames[i];
                this.menuButtons[i].Text = menuLabels[i];
                this.menuButtons[i].Location = new System.Drawing.Point(10, btnY);
                this.menuButtons[i].Size = new System.Drawing.Size(220, 45);
                this.menuButtons[i].Font = new System.Drawing.Font("Segoe UI", 10F);
                this.menuButtons[i].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.menuButtons[i].FlatAppearance.BorderSize = 0;
                this.menuButtons[i].Cursor = System.Windows.Forms.Cursors.Hand;
                this.menuButtons[i].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.menuButtons[i].Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);

                // Item ativo (Lista de Chamados)
                if (i == 3)
                {
                    this.menuButtons[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#F4F0E8");
                    this.menuButtons[i].ForeColor = System.Drawing.ColorTranslator.FromHtml("#F28A1A");
                    this.menuButtons[i].Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                }
                else
                {
                    this.menuButtons[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#F4EFE6");
                    this.menuButtons[i].ForeColor = System.Drawing.ColorTranslator.FromHtml("#666666");
                }

                this.panelSidebar.Controls.Add(this.menuButtons[i]);
                btnY += 50;
            }

            this.Controls.Add(this.panelSidebar);

            // ========== TOPBAR ==========
            this.panelTopbar = new System.Windows.Forms.Panel();
            this.panelTopbar.Name = "panelTopbar";
            this.panelTopbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopbar.Height = 60;
            this.panelTopbar.BackColor = System.Drawing.Color.White;

            // Título
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "Todos os Chamados";
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1A1A1A");
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Size = new System.Drawing.Size(400, 30);
            this.panelTopbar.Controls.Add(this.lblTitulo);

            this.Controls.Add(this.panelTopbar);

            // ========== MAIN CONTENT PANEL ==========
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelContent.Name = "panelContent";
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.BackColor = System.Drawing.ColorTranslator.FromHtml("#EDE6D9");
            this.panelContent.AutoScroll = true;
            this.panelContent.Padding = new System.Windows.Forms.Padding(20);

            // ===== FILTROS =====
            this.panelFilters = new System.Windows.Forms.Panel();
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Height = 50;
            this.panelFilters.BackColor = System.Drawing.ColorTranslator.FromHtml("#EDE6D9");

            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Location = new System.Drawing.Point(0, 10);
            this.cmbStatus.Size = new System.Drawing.Size(150, 30);
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Items.AddRange(new object[] { "Todos os Status", "Aberto", "Em andamento", "Resolvido", "Em espera" });
            this.cmbStatus.SelectedIndex = 0;
            this.panelFilters.Controls.Add(this.cmbStatus);

            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Location = new System.Drawing.Point(160, 10);
            this.cmbCategoria.Size = new System.Drawing.Size(150, 30);
            this.cmbCategoria.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoria.Items.AddRange(new object[] { "Todas as Categorias", "Sistema", "Software", "Hardware", "Rede", "Outro" });
            this.cmbCategoria.SelectedIndex = 0;
            this.panelFilters.Controls.Add(this.cmbCategoria);

            this.btnAtualizar = new System.Windows.Forms.Button();
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Text = "🔄";
            this.btnAtualizar.Location = new System.Drawing.Point(320, 10);
            this.btnAtualizar.Size = new System.Drawing.Size(40, 30);
            this.btnAtualizar.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnAtualizar.BackColor = System.Drawing.ColorTranslator.FromHtml("#F28A1A");
            this.btnAtualizar.ForeColor = System.Drawing.Color.White;
            this.btnAtualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtualizar.FlatAppearance.BorderSize = 0;
            this.btnAtualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelFilters.Controls.Add(this.btnAtualizar);

            this.panelContent.Controls.Add(this.panelFilters);

            // ===== KPI CARDS =====
            this.panelKpis = new System.Windows.Forms.Panel();
            this.panelKpis.Name = "panelKpis";
            this.panelKpis.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelKpis.Height = 150;
            this.panelKpis.BackColor = System.Drawing.ColorTranslator.FromHtml("#EDE6D9");

            int kpiX = 0;
            string[] kpiTitles = { "Total de Chamados", "Chamados Atendidos", "Chamados em Espera", "Chamados em Progresso" };
            string[] kpiNames = { "kpiTotal", "kpiAtendidos", "kpiEspera", "kpiProgresso" };

            for (int i = 0; i < 4; i++)
            {
                var kpiCard = new System.Windows.Forms.Panel();
                kpiCard.Name = kpiNames[i];
                kpiCard.Location = new System.Drawing.Point(kpiX, 10);
                kpiCard.Size = new System.Drawing.Size(200, 120);
                kpiCard.BackColor = System.Drawing.Color.White;
                kpiCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                var lblTitle = new System.Windows.Forms.Label();
                lblTitle.Text = kpiTitles[i];
                lblTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
                lblTitle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#666666");
                lblTitle.Location = new System.Drawing.Point(10, 10);
                lblTitle.Size = new System.Drawing.Size(180, 20);
                kpiCard.Controls.Add(lblTitle);

                var lblValue = new System.Windows.Forms.Label();
                lblValue.Name = "lbl" + kpiNames[i].Substring(3);
                lblValue.Text = "0";
                lblValue.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
                lblValue.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1A1A1A");
                lblValue.Location = new System.Drawing.Point(10, 35);
                lblValue.Size = new System.Drawing.Size(180, 50);
                lblValue.TextAlign = System.Drawing.ContentAlignment.TopLeft;
                kpiCard.Controls.Add(lblValue);

                this.panelKpis.Controls.Add(kpiCard);
                kpiX += 210;
            }

            this.panelContent.Controls.Add(this.panelKpis);

            // ===== FLOW LAYOUT PANEL PARA CARDS =====
            this.flowTickets = new System.Windows.Forms.FlowLayoutPanel();
            this.flowTickets.Name = "flowTickets";
            this.flowTickets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowTickets.AutoScroll = true;
            this.flowTickets.WrapContents = true;
            this.flowTickets.Padding = new System.Windows.Forms.Padding(10);
            this.flowTickets.BackColor = System.Drawing.ColorTranslator.FromHtml("#EDE6D9");
            this.flowTickets.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;

            this.panelContent.Controls.Add(this.flowTickets);

            this.Controls.Add(this.panelContent);

            // ========== FORM PROPERTIES ==========
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Name = "FormListaChamados";
            this.Text = "ClickDesk - Lista de Chamados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#EDE6D9");
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;

            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Button[] menuButtons;
        private System.Windows.Forms.Panel panelTopbar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.Panel panelKpis;
        private System.Windows.Forms.FlowLayoutPanel flowTickets;
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClickDesk.Forms.Dashboard
{
    partial class FormDashboard
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // === COMPONENTES PRINCIPAIS ===
        private Panel panelAppShell;
        private Panel panelSidebar;
        private Panel panelContent;
        private Panel panelTopBar;
        private Panel panelMainArea;

        // === SIDEBAR ===
        private Label lblLogo;
        private Panel panelNav;
        private Panel panelUserFooter;
        private Button btnUserAvatar;

        // === TOPBAR ===
        private Label lblPageTitle;
        private Panel panelTitleUnderline;
        private Button btnMinimize;
        private Button btnMaximize;
        private Button btnClose;

        // === TOOLBAR ===
        private Panel panelToolbar;
        private Label lblPeriodo;
        private ComboBox cmbPeriodo;
        private Button btnRefresh;

        // === KPI CARDS ===
        private Panel panelKpis;
        private TableLayoutPanel tableKpis;
        private Panel cardTotal;
        private Panel cardAtendidos;
        private Panel cardEspera;
        private Panel cardProgresso;
        private Label lblKpiTotalTitulo;
        private Label lblKpiTotalValor;
        private Label lblKpiTotalDelta;
        private Label lblKpiAtendidosTitulo;
        private Label lblKpiAtendidosValor;
        private Label lblKpiAtendidosDelta;
        private Label lblKpiEsperaTitulo;
        private Label lblKpiEsperaValor;
        private Label lblKpiEsperaDelta;
        private Label lblKpiProgressoTitulo;
        private Label lblKpiProgressoValor;
        private Label lblKpiProgressoDelta;

        // === QUICK ACTIONS ===
        private Panel panelQuickActions;
        private TableLayoutPanel tableQuickActions;
        private Panel cardQuickAprovacao;
        private Panel cardQuickMeusChamados;
        private Panel cardQuickFaq;

        // === CHARTS ===
        private Panel panelCharts;
        private TableLayoutPanel tableCharts;
        private Panel cardChartCategoria;
        private Panel cardChartStatus;
        private Label lblChartCategoriaTitulo;
        private Label lblChartStatusTitulo;
        private TableLayoutPanel tableChartCategorias;
        private TableLayoutPanel tableChartStatus;

        // === APPROVALS ===
        private Panel panelApprovals;
        private Label lblApprovalsTitulo;
        private FlowLayoutPanel flowApprovals;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se recursos gerenciados devem ser descartados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();

            // === FORM PROPERTIES ===
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Name = "FormDashboard";
            this.Text = "ClickDesk - Dashboard Técnico";
            this.BackColor = Color.FromArgb(0xED, 0xE6, 0xD9);
            this.Font = new Font("Segoe UI", 9F);
            this.StartPosition = FormStartPosition.CenterScreen;

            // === APP SHELL (ROOT PANEL) ===
            this.panelAppShell = new Panel();
            this.panelAppShell.Dock = DockStyle.Fill;
            this.panelAppShell.BackColor = Color.FromArgb(0xED, 0xE6, 0xD9);
            this.panelAppShell.Padding = new Padding(0);

            // === SIDEBAR ===
            this.panelSidebar = new Panel();
            this.panelSidebar.Dock = DockStyle.Left;
            this.panelSidebar.Width = 240;
            this.panelSidebar.BackColor = Color.FromArgb(0xF2, 0xEE, 0xE7);
            this.panelSidebar.Padding = new Padding(16, 18, 16, 12);
            this.panelSidebar.AutoScroll = false;

            // SIDEBAR - Logo
            this.lblLogo = new Label();
            this.lblLogo.Text = "Clickdesk";
            this.lblLogo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblLogo.ForeColor = Color.FromArgb(0xF2, 0x8A, 0x1A);
            this.lblLogo.AutoSize = true;
            this.lblLogo.Location = new Point(16, 18);
            this.lblLogo.Margin = new Padding(0, 0, 0, 16);

            // SIDEBAR - Nav Menu
            this.panelNav = new Panel();
            this.panelNav.Location = new Point(0, 60);
            this.panelNav.Size = new Size(240, 300);
            this.panelNav.AutoScroll = true;

            // SIDEBAR - Footer (User)
            this.panelUserFooter = new Panel();
            this.panelUserFooter.Dock = DockStyle.Bottom;
            this.panelUserFooter.Height = 70;
            this.panelUserFooter.Padding = new Padding(0, 8, 0, 0);

            this.btnUserAvatar = new Button();
            this.btnUserAvatar.Dock = DockStyle.Fill;
            this.btnUserAvatar.Text = "Técnico";
            this.btnUserAvatar.FlatStyle = FlatStyle.Flat;
            this.btnUserAvatar.FlatAppearance.BorderSize = 0;
            this.btnUserAvatar.BackColor = Color.White;
            this.btnUserAvatar.ForeColor = Color.FromArgb(0xF2, 0x8A, 0x1A);
            this.btnUserAvatar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnUserAvatar.Cursor = Cursors.Hand;

            this.panelUserFooter.Controls.Add(this.btnUserAvatar);
            this.panelSidebar.Controls.Add(this.panelUserFooter);
            this.panelSidebar.Controls.Add(this.panelNav);
            this.panelSidebar.Controls.Add(this.lblLogo);

            // === CONTENT PANEL ===
            this.panelContent = new Panel();
            this.panelContent.Dock = DockStyle.Fill;
            this.panelContent.BackColor = Color.FromArgb(0xED, 0xE6, 0xD9);
            this.panelContent.Padding = new Padding(0);

            // === TOPBAR ===
            this.panelTopBar = new Panel();
            this.panelTopBar.Dock = DockStyle.Top;
            this.panelTopBar.Height = 60;
            this.panelTopBar.BackColor = Color.FromArgb(0xF7, 0xF3, 0xEC);
            this.panelTopBar.Padding = new Padding(20, 12, 20, 12);

            this.lblPageTitle = new Label();
            this.lblPageTitle.Text = "Dashboard Técnico";
            this.lblPageTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblPageTitle.ForeColor = Color.FromArgb(0x1E, 0x2A, 0x22);
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Location = new Point(20, 12);

            this.panelTitleUnderline = new Panel();
            this.panelTitleUnderline.Location = new Point(20, 38);
            this.panelTitleUnderline.Size = new Size(200, 3);
            this.panelTitleUnderline.BackColor = Color.FromArgb(0xF2, 0x8A, 0x1A);

            // Window Control Buttons
            this.btnMinimize = new Button();
            this.btnMinimize.Text = "_";
            this.btnMinimize.Size = new Size(36, 28);
            this.btnMinimize.Location = new Point(1800, 16);
            this.btnMinimize.FlatStyle = FlatStyle.Flat;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.BackColor = Color.Transparent;
            this.btnMinimize.ForeColor = Color.FromArgb(0x33, 0x33, 0x33);
            this.btnMinimize.Cursor = Cursors.Hand;
            this.btnMinimize.Click += new EventHandler(this.btnMinimize_Click);

            this.btnMaximize = new Button();
            this.btnMaximize.Text = "▢";
            this.btnMaximize.Size = new Size(36, 28);
            this.btnMaximize.Location = new Point(1844, 16);
            this.btnMaximize.FlatStyle = FlatStyle.Flat;
            this.btnMaximize.FlatAppearance.BorderSize = 0;
            this.btnMaximize.BackColor = Color.Transparent;
            this.btnMaximize.ForeColor = Color.FromArgb(0x33, 0x33, 0x33);
            this.btnMaximize.Cursor = Cursors.Hand;
            this.btnMaximize.Click += new EventHandler(this.btnMaximize_Click);

            this.btnClose = new Button();
            this.btnClose.Text = "X";
            this.btnClose.Size = new Size(36, 28);
            this.btnClose.Location = new Point(1888, 16);
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.BackColor = Color.Transparent;
            this.btnClose.ForeColor = Color.FromArgb(0xCC, 0x00, 0x00);
            this.btnClose.Cursor = Cursors.Hand;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            this.panelTopBar.Controls.Add(this.lblPageTitle);
            this.panelTopBar.Controls.Add(this.panelTitleUnderline);
            this.panelTopBar.Controls.Add(this.btnMinimize);
            this.panelTopBar.Controls.Add(this.btnMaximize);
            this.panelTopBar.Controls.Add(this.btnClose);

            // === MAIN AREA ===
            this.panelMainArea = new Panel();
            this.panelMainArea.Dock = DockStyle.Fill;
            this.panelMainArea.BackColor = Color.FromArgb(0xED, 0xE6, 0xD9);
            this.panelMainArea.Padding = new Padding(24, 20, 24, 24);
            this.panelMainArea.AutoScroll = true;

            // === TOOLBAR ===
            this.panelToolbar = new Panel();
            this.panelToolbar.Dock = DockStyle.Top;
            this.panelToolbar.Height = 50;
            this.panelToolbar.BackColor = Color.Transparent;
            this.panelToolbar.Padding = new Padding(0);

            this.lblPeriodo = new Label();
            this.lblPeriodo.Text = "Período:";
            this.lblPeriodo.AutoSize = true;
            this.lblPeriodo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblPeriodo.Location = new Point(0, 14);

            this.cmbPeriodo = new ComboBox();
            this.cmbPeriodo.Items.AddRange(new object[] { "Hoje", "Últimos 7 dias", "Últimos 30 dias" });
            this.cmbPeriodo.SelectedIndex = 1;
            this.cmbPeriodo.Size = new Size(140, 24);
            this.cmbPeriodo.Location = new Point(70, 10);
            this.cmbPeriodo.DropDownStyle = ComboBoxStyle.DropDownList;

            this.btnRefresh = new Button();
            this.btnRefresh.Text = "↻";
            this.btnRefresh.Size = new Size(36, 28);
            this.btnRefresh.Location = new Point(220, 10);
            this.btnRefresh.BackColor = Color.FromArgb(0xF2, 0x8A, 0x1A);
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnRefresh.Cursor = Cursors.Hand;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            this.panelToolbar.Controls.Add(this.lblPeriodo);
            this.panelToolbar.Controls.Add(this.cmbPeriodo);
            this.panelToolbar.Controls.Add(this.btnRefresh);

            // === KPI CARDS TABLE ===
            this.panelKpis = new Panel();
            this.panelKpis.Dock = DockStyle.Top;
            this.panelKpis.Height = 150;
            this.panelKpis.Margin = new Padding(0, 16, 0, 24);
            this.panelKpis.BackColor = Color.Transparent;

            this.tableKpis = new TableLayoutPanel();
            this.tableKpis.Dock = DockStyle.Fill;
            this.tableKpis.ColumnCount = 4;
            this.tableKpis.RowCount = 1;
            this.tableKpis.ColumnStyles.Clear();
            for (int i = 0; i < 4; i++)
            {
                this.tableKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            }
            this.tableKpis.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tableKpis.Padding = new Padding(0);

            // KPI 1 - Total
            this.cardTotal = CreateKpiCard("TOTAL DE CHAMADOS", "28", "▼ 15%", Color.FromArgb(0xDC, 0x26, 0x26),
                out this.lblKpiTotalTitulo, out this.lblKpiTotalValor, out this.lblKpiTotalDelta);

            // KPI 2 - Atendidos
            this.cardAtendidos = CreateKpiCard("CHAMADOS ATENDIDOS", "14", "▲ 22%", Color.FromArgb(0x16, 0xA3, 0x4A),
                out this.lblKpiAtendidosTitulo, out this.lblKpiAtendidosValor, out this.lblKpiAtendidosDelta);

            // KPI 3 - Espera
            this.cardEspera = CreateKpiCard("CHAMADOS EM ESPERA", "6", "▼ 10%", Color.FromArgb(0xDC, 0x26, 0x26),
                out this.lblKpiEsperaTitulo, out this.lblKpiEsperaValor, out this.lblKpiEsperaDelta);

            // KPI 4 - Progresso
            this.cardProgresso = CreateKpiCard("CHAMADOS EM PROGRESSO", "8", "▲ 50%", Color.FromArgb(0x16, 0xA3, 0x4A),
                out this.lblKpiProgressoTitulo, out this.lblKpiProgressoValor, out this.lblKpiProgressoDelta);

            this.tableKpis.Controls.Add(this.cardTotal, 0, 0);
            this.tableKpis.Controls.Add(this.cardAtendidos, 1, 0);
            this.tableKpis.Controls.Add(this.cardEspera, 2, 0);
            this.tableKpis.Controls.Add(this.cardProgresso, 3, 0);

            this.panelKpis.Controls.Add(this.tableKpis);

            // === QUICK ACTIONS TABLE ===
            this.panelQuickActions = new Panel();
            this.panelQuickActions.Dock = DockStyle.Top;
            this.panelQuickActions.Height = 110;
            this.panelQuickActions.Margin = new Padding(0, 0, 0, 24);
            this.panelQuickActions.BackColor = Color.Transparent;

            this.tableQuickActions = new TableLayoutPanel();
            this.tableQuickActions.Dock = DockStyle.Fill;
            this.tableQuickActions.ColumnCount = 3;
            this.tableQuickActions.RowCount = 1;
            this.tableQuickActions.ColumnStyles.Clear();
            for (int i = 0; i < 3; i++)
            {
                this.tableQuickActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            }
            this.tableQuickActions.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tableQuickActions.Padding = new Padding(0);

            this.cardQuickAprovacao = CreateQuickActionCard("Aprovação de chamados");
            this.cardQuickMeusChamados = CreateQuickActionCard("Meus chamados");
            this.cardQuickFaq = CreateQuickActionCard("FAQ");

            this.tableQuickActions.Controls.Add(this.cardQuickAprovacao, 0, 0);
            this.tableQuickActions.Controls.Add(this.cardQuickMeusChamados, 1, 0);
            this.tableQuickActions.Controls.Add(this.cardQuickFaq, 2, 0);

            this.panelQuickActions.Controls.Add(this.tableQuickActions);

            // === CHARTS TABLE ===
            this.panelCharts = new Panel();
            this.panelCharts.Dock = DockStyle.Top;
            this.panelCharts.Height = 280;
            this.panelCharts.Margin = new Padding(0, 0, 0, 24);
            this.panelCharts.BackColor = Color.Transparent;

            this.tableCharts = new TableLayoutPanel();
            this.tableCharts.Dock = DockStyle.Fill;
            this.tableCharts.ColumnCount = 2;
            this.tableCharts.RowCount = 1;
            this.tableCharts.ColumnStyles.Clear();
            this.tableCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tableCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tableCharts.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tableCharts.Padding = new Padding(0);

            this.cardChartCategoria = CreateChartCard("Chamados por Categoria", out this.lblChartCategoriaTitulo, out this.tableChartCategorias);
            this.cardChartStatus = CreateChartCard("Status dos Chamados", out this.lblChartStatusTitulo, out this.tableChartStatus);

            this.tableCharts.Controls.Add(this.cardChartCategoria, 0, 0);
            this.tableCharts.Controls.Add(this.cardChartStatus, 1, 0);

            // Populate charts with data
            PopulateChartData();

            this.panelCharts.Controls.Add(this.tableCharts);

            // === APPROVALS SECTION ===
            this.panelApprovals = new Panel();
            this.panelApprovals.Dock = DockStyle.Fill;
            this.panelApprovals.BackColor = Color.Transparent;
            this.panelApprovals.Padding = new Padding(0);

            this.lblApprovalsTitulo = new Label();
            this.lblApprovalsTitulo.Text = "Aprovações Pendentes";
            this.lblApprovalsTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblApprovalsTitulo.AutoSize = true;
            this.lblApprovalsTitulo.Location = new Point(0, 10);

            this.flowApprovals = new FlowLayoutPanel();
            this.flowApprovals.FlowDirection = FlowDirection.LeftToRight;
            this.flowApprovals.WrapContents = true;
            this.flowApprovals.AutoScroll = true;
            this.flowApprovals.Location = new Point(0, 40);
            this.flowApprovals.Size = new Size(this.panelMainArea.Width - 48, 200);
            this.flowApprovals.Padding = new Padding(0);

            // Add approval cards
            PopulateApprovalCards();

            this.panelApprovals.Controls.Add(this.flowApprovals);
            this.panelApprovals.Controls.Add(this.lblApprovalsTitulo);

            // === ASSEMBLE MAIN AREA ===
            this.panelMainArea.Controls.Add(this.panelApprovals);
            this.panelMainArea.Controls.Add(this.panelCharts);
            this.panelMainArea.Controls.Add(this.panelQuickActions);
            this.panelMainArea.Controls.Add(this.panelKpis);
            this.panelMainArea.Controls.Add(this.panelToolbar);

            // === ASSEMBLE CONTENT ===
            this.panelContent.Controls.Add(this.panelMainArea);
            this.panelContent.Controls.Add(this.panelTopBar);

            // === ASSEMBLE APP SHELL ===
            this.panelAppShell.Controls.Add(this.panelContent);
            this.panelAppShell.Controls.Add(this.panelSidebar);

            // === ASSEMBLE FORM ===
            this.Controls.Add(this.panelAppShell);

            this.ResumeLayout(false);
        }

        #endregion

        #region HELPER METHODS

        private Panel CreateKpiCard(string titulo, string valor, string deltaTexto, Color deltaColor,
            out Label lblTitulo, out Label lblValor, out Label lblDelta)
        {
            Panel card = new Panel();
            card.Margin = new Padding(0, 0, 12, 0);
            card.Dock = DockStyle.Fill;
            card.BackColor = Color.White;
            card.Padding = new Padding(16);
            card.BorderStyle = BorderStyle.FixedSingle;

            lblTitulo = new Label();
            lblTitulo.Text = titulo;
            lblTitulo.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(0x6B, 0x72, 0x80);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(4, 4);

            lblValor = new Label();
            lblValor.Text = valor;
            lblValor.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblValor.ForeColor = Color.FromArgb(0x11, 0x18, 0x27);
            lblValor.AutoSize = true;
            lblValor.Location = new Point(4, 32);

            lblDelta = new Label();
            lblDelta.Text = deltaTexto;
            lblDelta.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDelta.ForeColor = deltaColor;
            lblDelta.AutoSize = true;
            lblDelta.Location = new Point(4, 72);

            card.Controls.Add(lblTitulo);
            card.Controls.Add(lblValor);
            card.Controls.Add(lblDelta);

            return card;
        }

        private Panel CreateQuickActionCard(string titulo)
        {
            Panel card = new Panel();
            card.Margin = new Padding(0, 0, 12, 0);
            card.Dock = DockStyle.Fill;
            card.BackColor = Color.White;
            card.Padding = new Padding(16);
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Cursor = Cursors.Hand;

            Label lblTitulo = new Label();
            lblTitulo.Text = titulo;
            lblTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(0x11, 0x18, 0x27);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(4, 12);

            Label lblArrow = new Label();
            lblArrow.Text = "›";
            lblArrow.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblArrow.ForeColor = Color.FromArgb(0xF2, 0x8A, 0x1A);
            lblArrow.AutoSize = true;
            lblArrow.Location = new Point(card.Width - 25, 8);

            card.Controls.Add(lblTitulo);
            card.Controls.Add(lblArrow);

            return card;
        }

        private Panel CreateChartCard(string titulo, out Label lblTitulo, out TableLayoutPanel tableChart)
        {
            Panel card = new Panel();
            card.Margin = new Padding(0, 0, 12, 0);
            card.Dock = DockStyle.Fill;
            card.BackColor = Color.White;
            card.Padding = new Padding(16);
            card.BorderStyle = BorderStyle.FixedSingle;

            lblTitulo = new Label();
            lblTitulo.Text = titulo;
            lblTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(0x11, 0x18, 0x27);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(4, 4);

            tableChart = new TableLayoutPanel();
            tableChart.Location = new Point(4, 32);
            tableChart.Size = new Size(card.Width - 32, card.Height - 40);
            tableChart.ColumnCount = 3;
            tableChart.RowCount = 5;
            tableChart.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            tableChart.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableChart.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            for (int i = 0; i < 5; i++)
            {
                tableChart.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            }

            card.Controls.Add(lblTitulo);
            card.Controls.Add(tableChart);

            return card;
        }

        private void PopulateChartData()
        {
            AddBarRow(this.tableChartCategorias, "Hardware", 1.0f, Color.FromArgb(0x42, 0x85, 0xF4), "32");
            AddBarRow(this.tableChartCategorias, "Software", 0.7f, Color.FromArgb(0x34, 0xA8, 0x53), "22");
            AddBarRow(this.tableChartCategorias, "Rede", 0.55f, Color.FromArgb(0xFB, 0xBC, 0x04), "17");
            AddBarRow(this.tableChartCategorias, "Acesso", 0.4f, Color.FromArgb(0xEA, 0x43, 0x35), "12");
            AddBarRow(this.tableChartCategorias, "Outros", 0.3f, Color.FromArgb(0x9C, 0x27, 0xB0), "9");

            AddBarRow(this.tableChartStatus, "Aberto", 0.28f, Color.FromArgb(0xFB, 0xBC, 0x04), "12");
            AddBarRow(this.tableChartStatus, "Em Andamento", 0.2f, Color.FromArgb(0x42, 0x85, 0xF4), "8");
            AddBarRow(this.tableChartStatus, "Aguardando", 0.12f, Color.FromArgb(0x9E, 0x9E, 0x9E), "5");
            AddBarRow(this.tableChartStatus, "Resolvido", 1.0f, Color.FromArgb(0x34, 0xA8, 0x53), "45");
        }

        private void AddBarRow(TableLayoutPanel table, string label, float larguraRelativa, Color cor, string valor)
        {
            int rowIndex = table.RowCount - 1;

            Label lbl = new Label();
            lbl.Text = label;
            lbl.AutoSize = true;
            lbl.Font = new Font("Segoe UI", 9F);
            lbl.ForeColor = Color.FromArgb(0x11, 0x18, 0x27);
            lbl.Margin = new Padding(0, 6, 4, 0);

            Panel barBackground = new Panel();
            barBackground.Dock = DockStyle.Fill;
            barBackground.Margin = new Padding(0, 8, 0, 8);
            barBackground.BackColor = Color.FromArgb(0xF0, 0xF0, 0xF0);

            Panel barFill = new Panel();
            barFill.BackColor = cor;
            barFill.Width = (int)(barBackground.Width * larguraRelativa);
            barFill.Height = barBackground.Height;
            barFill.Dock = DockStyle.Left;

            barBackground.Controls.Add(barFill);

            Label lblValor = new Label();
            lblValor.Text = valor;
            lblValor.AutoSize = true;
            lblValor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblValor.ForeColor = Color.FromArgb(0x11, 0x18, 0x27);
            lblValor.Margin = new Padding(4, 6, 0, 0);

            table.Controls.Add(lbl, 0, rowIndex);
            table.Controls.Add(barBackground, 1, rowIndex);
            table.Controls.Add(lblValor, 2, rowIndex);
        }

        private void PopulateApprovalCards()
        {
            // Add 4 approval cards
            for (int i = 1; i <= 4; i++)
            {
                Panel approvalCard = CreateApprovalCard(
                    $"#100{i}",
                    i % 2 == 0 ? "EM ANDAMENTO" : "ABERTO",
                    $"Chamado de Teste {i}",
                    "Descrição do chamado de teste",
                    i % 3 == 0 ? "SOFTWARE" : (i % 3 == 1 ? "HARDWARE" : "REDE"),
                    $"Há {i * 2} horas"
                );
                this.flowApprovals.Controls.Add(approvalCard);
            }
        }

        private Panel CreateApprovalCard(string id, string status, string titulo, string descricao, string categoria, string tempo)
        {
            Panel card = new Panel();
            card.Size = new Size(220, 200);
            card.Margin = new Padding(8);
            card.BackColor = Color.White;
            card.Padding = new Padding(12);
            card.BorderStyle = BorderStyle.FixedSingle;

            // ID Badge
            Label lblId = new Label();
            lblId.Text = id;
            lblId.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblId.ForeColor = Color.White;
            lblId.BackColor = Color.FromArgb(0xF2, 0x8A, 0x1A);
            lblId.Size = new Size(60, 20);
            lblId.TextAlign = ContentAlignment.MiddleCenter;
            lblId.Location = new Point(4, 4);

            // Status Badge
            Label lblStatus = new Label();
            lblStatus.Text = status;
            lblStatus.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
            lblStatus.ForeColor = Color.White;
            lblStatus.BackColor = status == "ABERTO" ? Color.FromArgb(0xFB, 0xBC, 0x04) : Color.FromArgb(0x42, 0x85, 0xF4);
            lblStatus.Size = new Size(70, 18);
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            lblStatus.Location = new Point(140, 4);

            // Título
            Label lblTitulo = new Label();
            lblTitulo.Text = titulo;
            lblTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(0x11, 0x18, 0x27);
            lblTitulo.Location = new Point(4, 30);
            lblTitulo.Size = new Size(192, 30);
            lblTitulo.AutoEllipsis = true;

            // Descrição
            Label lblDesc = new Label();
            lblDesc.Text = descricao;
            lblDesc.Font = new Font("Segoe UI", 8F);
            lblDesc.ForeColor = Color.FromArgb(0x6F, 0x6F, 0x6F);
            lblDesc.Location = new Point(4, 70);
            lblDesc.Size = new Size(192, 50);
            lblDesc.AutoEllipsis = true;

            // Footer - Categoria
            Label lblCategoria = new Label();
            lblCategoria.Text = categoria;
            lblCategoria.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
            lblCategoria.ForeColor = Color.White;
            lblCategoria.BackColor = Color.FromArgb(0x6B, 0x72, 0x80);
            lblCategoria.Size = new Size(50, 16);
            lblCategoria.TextAlign = ContentAlignment.MiddleCenter;
            lblCategoria.Location = new Point(4, 130);

            // Footer - Tempo
            Label lblTempo = new Label();
            lblTempo.Text = tempo;
            lblTempo.Font = new Font("Segoe UI", 7F);
            lblTempo.ForeColor = Color.FromArgb(0x6F, 0x6F, 0x6F);
            lblTempo.AutoSize = true;
            lblTempo.Location = new Point(60, 134);

            card.Controls.Add(lblId);
            card.Controls.Add(lblStatus);
            card.Controls.Add(lblTitulo);
            card.Controls.Add(lblDesc);
            card.Controls.Add(lblCategoria);
            card.Controls.Add(lblTempo);

            return card;
        }

        #endregion
    }
}

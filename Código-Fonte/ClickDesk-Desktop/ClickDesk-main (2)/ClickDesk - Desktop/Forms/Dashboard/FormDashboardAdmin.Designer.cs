using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClickDesk.Forms.Dashboard
{
    partial class FormDashboardAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Layout principal
        private Panel panelRoot;
        private Panel panelSidebar;
        private Panel panelContent;
        private Panel panelTopbar;
        private Panel panelMainArea;

        // Sidebar
        private Label lblLogo;
        private FlowLayoutPanel sidebarMenu;
        private Button btnNavDashboard;
        private Button btnNavAprovacao;
        private Button btnNavMeusChamados;
        private Button btnNavEditarPerfil;
        private Button btnNavFaq;
        private Panel panelSidebarFooter;
        private Button btnUserAvatar;

        // Topbar
        private Label lblPageTitle;
        private FlowLayoutPanel topbarWindowControls;
        private Button btnMinimize;
        private Button btnMaximize;
        private Button btnClose;

        // Toolbar de período
        private Panel panelToolbar;
        private Label lblPeriodo;
        private ComboBox cboPeriodo;
        private Button btnRefresh;
        private Label lblLastUpdated;

        // KPIs
        private TableLayoutPanel tableKpis;
        private Panel cardTotalChamados;
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

        // Ações rápidas
        private TableLayoutPanel tableQuickActions;
        private Panel cardQuickAprovacao;
        private Panel cardQuickMeusChamados;
        private Panel cardQuickFaq;
        private Label lblQuickAprovacao;
        private Label lblQuickMeusChamados;
        private Label lblQuickFaq;

        // Gráficos (placeholders com barras)
        private TableLayoutPanel tableCharts;
        private Panel cardChartCategoria;
        private Panel cardChartStatus;
        private Label lblChartCategoriaTitulo;
        private Label lblChartStatusTitulo;
        private TableLayoutPanel tableChartCategorias;
        private TableLayoutPanel tableChartStatus;

        // Aprovações pendentes
        private Panel panelAprovacoes;
        private Panel panelAprovacoesHeader;
        private Label lblAprovacoesTitulo;
        private LinkLabel lnkVerTodas;
        private FlowLayoutPanel flowAprovacoesPendentes;
        private Label lblAprovacoesVazio;

        /// <summary>
        /// Limpar recursos.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Método de inicialização do formulário.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.panelRoot = new Panel();
            this.panelSidebar = new Panel();
            this.panelSidebarFooter = new Panel();
            this.btnUserAvatar = new Button();
            this.sidebarMenu = new FlowLayoutPanel();
            this.btnNavDashboard = new Button();
            this.btnNavAprovacao = new Button();
            this.btnNavMeusChamados = new Button();
            this.btnNavEditarPerfil = new Button();
            this.btnNavFaq = new Button();
            this.lblLogo = new Label();

            this.panelContent = new Panel();
            this.panelTopbar = new Panel();
            this.lblPageTitle = new Label();
            this.topbarWindowControls = new FlowLayoutPanel();
            this.btnMinimize = new Button();
            this.btnMaximize = new Button();
            this.btnClose = new Button();

            this.panelMainArea = new Panel();
            this.panelToolbar = new Panel();
            this.lblPeriodo = new Label();
            this.cboPeriodo = new ComboBox();
            this.btnRefresh = new Button();
            this.lblLastUpdated = new Label();

            this.tableKpis = new TableLayoutPanel();
            this.cardTotalChamados = new Panel();
            this.cardAtendidos = new Panel();
            this.cardEspera = new Panel();
            this.cardProgresso = new Panel();
            this.lblKpiTotalTitulo = new Label();
            this.lblKpiTotalValor = new Label();
            this.lblKpiTotalDelta = new Label();

            this.lblKpiAtendidosTitulo = new Label();
            this.lblKpiAtendidosValor = new Label();
            this.lblKpiAtendidosDelta = new Label();

            this.lblKpiEsperaTitulo = new Label();
            this.lblKpiEsperaValor = new Label();
            this.lblKpiEsperaDelta = new Label();

            this.lblKpiProgressoTitulo = new Label();
            this.lblKpiProgressoValor = new Label();
            this.lblKpiProgressoDelta = new Label();

            this.tableQuickActions = new TableLayoutPanel();
            this.cardQuickAprovacao = new Panel();
            this.cardQuickMeusChamados = new Panel();
            this.cardQuickFaq = new Panel();
            this.lblQuickAprovacao = new Label();
            this.lblQuickMeusChamados = new Label();
            this.lblQuickFaq = new Label();

            this.tableCharts = new TableLayoutPanel();
            this.cardChartCategoria = new Panel();
            this.cardChartStatus = new Panel();
            this.lblChartCategoriaTitulo = new Label();
            this.lblChartStatusTitulo = new Label();
            this.tableChartCategorias = new TableLayoutPanel();
            this.tableChartStatus = new TableLayoutPanel();

            this.panelAprovacoes = new Panel();
            this.panelAprovacoesHeader = new Panel();
            this.lblAprovacoesTitulo = new Label();
            this.lnkVerTodas = new LinkLabel();
            this.flowAprovacoesPendentes = new FlowLayoutPanel();
            this.lblAprovacoesVazio = new Label();

            // 
            // FormDashboardAdmin (propriedades básicas)
            // 
            this.SuspendLayout();
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new Size(1280, 720);
            this.Text = "Clickdesk — Dashboard Técnico";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(0xED, 0xE6, 0xD9);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

            // Painel root
            this.panelRoot.Dock = DockStyle.Fill;
            this.panelRoot.BackColor = Color.FromArgb(0xED, 0xE6, 0xD9);
            this.panelRoot.Padding = new Padding(0);
            this.Controls.Add(this.panelRoot);

            // SIDEBAR
            this.panelSidebar.Dock = DockStyle.Left;
            this.panelSidebar.Width = 240;
            this.panelSidebar.BackColor = Color.FromArgb(0xF2, 0xEE, 0xE7);
            this.panelSidebar.Padding = new Padding(16, 18, 16, 12);
            this.panelSidebar.BorderStyle = BorderStyle.None;

            // Logo
            this.lblLogo.Text = "Clickdesk";
            this.lblLogo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblLogo.ForeColor = Color.FromArgb(0xF2, 0x8A, 0x1A);
            this.lblLogo.AutoSize = true;
            this.lblLogo.Margin = new Padding(4, 4, 4, 16);

            // Sidebar menu
            this.sidebarMenu.FlowDirection = FlowDirection.TopDown;
            this.sidebarMenu.WrapContents = false;
            this.sidebarMenu.Dock = DockStyle.Fill;
            this.sidebarMenu.Padding = new Padding(0, 8, 0, 8);
            this.sidebarMenu.AutoScroll = true;

            ConfigureSidebarButton(this.btnNavDashboard, "Dashboard Técnico", true);
            ConfigureSidebarButton(this.btnNavAprovacao, "Aprovação de chamados", false);
            ConfigureSidebarButton(this.btnNavMeusChamados, "Meus chamados", false);
            ConfigureSidebarButton(this.btnNavEditarPerfil, "Edição de perfil", false);
            ConfigureSidebarButton(this.btnNavFaq, "FAQ", false);

            this.sidebarMenu.Controls.Add(this.btnNavDashboard);
            this.sidebarMenu.Controls.Add(this.btnNavAprovacao);
            this.sidebarMenu.Controls.Add(this.btnNavMeusChamados);
            this.sidebarMenu.Controls.Add(this.btnNavEditarPerfil);
            this.sidebarMenu.Controls.Add(this.btnNavFaq);

            // Sidebar footer
            this.panelSidebarFooter.Dock = DockStyle.Bottom;
            this.panelSidebarFooter.Height = 70;
            this.panelSidebarFooter.Padding = new Padding(0, 8, 0, 0);

            this.btnUserAvatar.Dock = DockStyle.Fill;
            this.btnUserAvatar.FlatStyle = FlatStyle.Flat;
            this.btnUserAvatar.FlatAppearance.BorderSize = 0;
            this.btnUserAvatar.Text = "Técnico";
            this.btnUserAvatar.TextAlign = ContentAlignment.MiddleCenter;
            this.btnUserAvatar.BackColor = Color.White;
            this.btnUserAvatar.ForeColor = Color.FromArgb(0xF2, 0x8A, 0x1A);
            this.btnUserAvatar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.btnUserAvatar.Cursor = Cursors.Hand;

            this.panelSidebarFooter.Controls.Add(this.btnUserAvatar);

            // Monta sidebar
            this.panelSidebar.Controls.Add(this.sidebarMenu);
            this.panelSidebar.Controls.Add(this.panelSidebarFooter);
            this.panelSidebar.Controls.Add(this.lblLogo);

            // CONTENT
            this.panelContent.Dock = DockStyle.Fill;
            this.panelContent.Padding = new Padding(0, 0, 0, 0);
            this.panelContent.BackColor = Color.FromArgb(0xED, 0xE6, 0xD9);

            // TOPBAR
            this.panelTopbar.Dock = DockStyle.Top;
            this.panelTopbar.Height = 60;
            this.panelTopbar.BackColor = Color.FromArgb(0xF7, 0xF3, 0xEC);
            this.panelTopbar.Padding = new Padding(20, 12, 20, 12);

            this.lblPageTitle.Text = "Dashboard Técnico";
            this.lblPageTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.ForeColor = Color.FromArgb(0x1E, 0x2A, 0x22);
            this.lblPageTitle.Location = new Point(20, 15);

            this.topbarWindowControls.FlowDirection = FlowDirection.LeftToRight;
            this.topbarWindowControls.WrapContents = false;
            this.topbarWindowControls.Dock = DockStyle.Right;
            this.topbarWindowControls.Width = 120;
            this.topbarWindowControls.Padding = new Padding(0);
            this.topbarWindowControls.MinimumSize = new Size(120, 0);

            ConfigureWindowButton(this.btnMinimize, "_");
            ConfigureWindowButton(this.btnMaximize, "▢");
            ConfigureWindowButton(this.btnClose, "X");
            this.btnClose.ForeColor = Color.FromArgb(200, 0, 0);

            this.topbarWindowControls.Controls.Add(this.btnMinimize);
            this.topbarWindowControls.Controls.Add(this.btnMaximize);
            this.topbarWindowControls.Controls.Add(this.btnClose);

            this.panelTopbar.Controls.Add(this.topbarWindowControls);
            this.panelTopbar.Controls.Add(this.lblPageTitle);

            // MAIN AREA
            this.panelMainArea.Dock = DockStyle.Fill;
            this.panelMainArea.Padding = new Padding(24, 20, 24, 24);
            this.panelMainArea.BackColor = Color.FromArgb(0xED, 0xE6, 0xD9);

            // TOOLBAR (período + atualizar + data)
            this.panelToolbar.Dock = DockStyle.Top;
            this.panelToolbar.Height = 44;
            this.panelToolbar.Padding = new Padding(0, 0, 0, 0);
            this.panelToolbar.BackColor = Color.Transparent;

            this.lblPeriodo.AutoSize = true;
            this.lblPeriodo.Text = "Período";
            this.lblPeriodo.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblPeriodo.Location = new Point(0, 12);

            this.cboPeriodo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPeriodo.Items.AddRange(new object[] { "Hoje", "Últimos 7 dias", "Últimos 30 dias" });
            this.cboPeriodo.SelectedIndex = 1;
            this.cboPeriodo.Location = new Point(60, 8);
            this.cboPeriodo.Width = 140;

            this.btnRefresh.Text = "↻";
            this.btnRefresh.Width = 36;
            this.btnRefresh.Height = 28;
            this.btnRefresh.Location = new Point(210, 8);
            this.btnRefresh.BackColor = Color.FromArgb(0xF2, 0x8A, 0x1A);
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);

            this.lblLastUpdated.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.lblLastUpdated.AutoSize = true;
            this.lblLastUpdated.Text = "Atualizado às --:--";
            this.lblLastUpdated.ForeColor = Color.FromArgb(0x6F, 0x6F, 0x6F);
            this.lblLastUpdated.Location = new Point(this.panelMainArea.Width - 180, 12);

            this.panelToolbar.Controls.Add(this.lblPeriodo);
            this.panelToolbar.Controls.Add(this.cboPeriodo);
            this.panelToolbar.Controls.Add(this.btnRefresh);
            this.panelToolbar.Controls.Add(this.lblLastUpdated);

            // TABLE KPIs
            this.tableKpis.Dock = DockStyle.Top;
            this.tableKpis.Height = 150;
            this.tableKpis.Margin = new Padding(0, 16, 0, 24);
            this.tableKpis.ColumnCount = 4;
            this.tableKpis.RowCount = 1;
            this.tableKpis.ColumnStyles.Clear();
            for (int i = 0; i < 4; i++)
            {
                this.tableKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            }
            this.tableKpis.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tableKpis.Padding = new Padding(0, 16, 0, 0);

            ConfigureKpiCard(this.cardTotalChamados, "TOTAL DE CHAMADOS", "28", "▼ 15%", Color.FromArgb(0xDC, 0x26, 0x26),
                out this.lblKpiTotalTitulo, out this.lblKpiTotalValor, out this.lblKpiTotalDelta);
            ConfigureKpiCard(this.cardAtendidos, "CHAMADOS ATENDIDOS", "14", "▲ 22%", Color.FromArgb(0x16, 0xA3, 0x4A),
                out this.lblKpiAtendidosTitulo, out this.lblKpiAtendidosValor, out this.lblKpiAtendidosDelta);
            ConfigureKpiCard(this.cardEspera, "CHAMADOS EM ESPERA", "6", "▼ 10%", Color.FromArgb(0xDC, 0x26, 0x26),
                out this.lblKpiEsperaTitulo, out this.lblKpiEsperaValor, out this.lblKpiEsperaDelta);
            ConfigureKpiCard(this.cardProgresso, "CHAMADOS EM PROGRESSO", "8", "▲ 50%", Color.FromArgb(0x16, 0xA3, 0x4A),
                out this.lblKpiProgressoTitulo, out this.lblKpiProgressoValor, out this.lblKpiProgressoDelta);

            this.tableKpis.Controls.Add(this.cardTotalChamados, 0, 0);
            this.tableKpis.Controls.Add(this.cardAtendidos, 1, 0);
            this.tableKpis.Controls.Add(this.cardEspera, 2, 0);
            this.tableKpis.Controls.Add(this.cardProgresso, 3, 0);

            // QUICK ACTIONS
            this.tableQuickActions.Dock = DockStyle.Top;
            this.tableQuickActions.Height = 90;
            this.tableQuickActions.Margin = new Padding(0, 0, 0, 24);
            this.tableQuickActions.ColumnCount = 3;
            this.tableQuickActions.RowCount = 1;
            this.tableQuickActions.ColumnStyles.Clear();
            for (int i = 0; i < 3; i++)
            {
                this.tableQuickActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            }
            this.tableQuickActions.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            ConfigureQuickCard(this.cardQuickAprovacao, "Aprovação de chamados", out this.lblQuickAprovacao);
            ConfigureQuickCard(this.cardQuickMeusChamados, "Meus chamados", out this.lblQuickMeusChamados);
            ConfigureQuickCard(this.cardQuickFaq, "FAQ", out this.lblQuickFaq);

            this.tableQuickActions.Controls.Add(this.cardQuickAprovacao, 0, 0);
            this.tableQuickActions.Controls.Add(this.cardQuickMeusChamados, 1, 0);
            this.tableQuickActions.Controls.Add(this.cardQuickFaq, 2, 0);

            // CHARTS
            this.tableCharts.Dock = DockStyle.Top;
            this.tableCharts.Height = 260;
            this.tableCharts.Margin = new Padding(0, 0, 0, 24);
            this.tableCharts.ColumnCount = 2;
            this.tableCharts.RowCount = 1;
            this.tableCharts.ColumnStyles.Clear();
            this.tableCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tableCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tableCharts.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            ConfigureChartCard(this.cardChartCategoria, "Chamados por Categoria", out this.lblChartCategoriaTitulo, this.tableChartCategorias);
            ConfigureChartCard(this.cardChartStatus, "Status dos Chamados", out this.lblChartStatusTitulo, this.tableChartStatus);

            this.tableCharts.Controls.Add(this.cardChartCategoria, 0, 0);
            this.tableCharts.Controls.Add(this.cardChartStatus, 1, 0);

            // Preenche tabelas de barras (categorias e status)
            AddBarRow(this.tableChartCategorias, "Hardware", 1.0f, Color.FromArgb(0x42, 0x85, 0xF4), "32");
            AddBarRow(this.tableChartCategorias, "Software", 0.7f, Color.FromArgb(0x34, 0xA8, 0x53), "22");
            AddBarRow(this.tableChartCategorias, "Rede", 0.55f, Color.FromArgb(0xFB, 0xBC, 0x04), "17");
            AddBarRow(this.tableChartCategorias, "Acesso", 0.4f, Color.FromArgb(0xEA, 0x43, 0x35), "12");
            AddBarRow(this.tableChartCategorias, "Outros", 0.3f, Color.FromArgb(0x9C, 0x27, 0xB0), "9");

            AddBarRow(this.tableChartStatus, "Aberto", 0.28f, Color.FromArgb(0xFB, 0xBC, 0x04), "12");
            AddBarRow(this.tableChartStatus, "Em Andamento", 0.2f, Color.FromArgb(0x42, 0x85, 0xF4), "8");
            AddBarRow(this.tableChartStatus, "Aguardando", 0.12f, Color.FromArgb(0x9E, 0x9E, 0x9E), "5");
            AddBarRow(this.tableChartStatus, "Resolvido", 1.0f, Color.FromArgb(0x34, 0xA8, 0x53), "45");

            // APROVAÇÕES PENDENTES
            this.panelAprovacoes.Dock = DockStyle.Fill;
            this.panelAprovacoes.BackColor = Color.Transparent;
            this.panelAprovacoes.Padding = new Padding(0, 0, 0, 0);

            this.panelAprovacoesHeader.Dock = DockStyle.Top;
            this.panelAprovacoesHeader.Height = 40;
            this.panelAprovacoesHeader.Padding = new Padding(0, 0, 0, 8);

            this.lblAprovacoesTitulo.Text = "Aprovações Pendentes";
            this.lblAprovacoesTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblAprovacoesTitulo.AutoSize = true;
            this.lblAprovacoesTitulo.Location = new Point(0, 10);

            this.lnkVerTodas.Text = "Ver Todas ›";
            this.lnkVerTodas.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.lnkVerTodas.AutoSize = true;
            this.lnkVerTodas.Location = new Point(this.panelAprovacoes.Width - 80, 12);

            this.panelAprovacoesHeader.Controls.Add(this.lblAprovacoesTitulo);
            this.panelAprovacoesHeader.Controls.Add(this.lnkVerTodas);

            this.flowAprovacoesPendentes.Dock = DockStyle.Fill;
            this.flowAprovacoesPendentes.FlowDirection = FlowDirection.LeftToRight;
            this.flowAprovacoesPendentes.WrapContents = true;
            this.flowAprovacoesPendentes.AutoScroll = true;
            this.flowAprovacoesPendentes.Padding = new Padding(0, 4, 0, 0);

            this.lblAprovacoesVazio.Text = "Nenhuma aprovação pendente.";
            this.lblAprovacoesVazio.Dock = DockStyle.Fill;
            this.lblAprovacoesVazio.TextAlign = ContentAlignment.MiddleCenter;
            this.lblAprovacoesVazio.ForeColor = Color.FromArgb(0x6F, 0x6F, 0x6F);

            this.panelAprovacoes.Controls.Add(this.flowAprovacoesPendentes);
            this.panelAprovacoes.Controls.Add(this.panelAprovacoesHeader);

            // Monta MAIN AREA: ordem do topo para baixo
            this.panelMainArea.Controls.Add(this.panelAprovacoes);
            this.panelMainArea.Controls.Add(this.tableCharts);
            this.panelMainArea.Controls.Add(this.tableQuickActions);
            this.panelMainArea.Controls.Add(this.tableKpis);
            this.panelMainArea.Controls.Add(this.panelToolbar);

            // Monta CONTENT
            this.panelContent.Controls.Add(this.panelMainArea);
            this.panelContent.Controls.Add(this.panelTopbar);

            // Monta ROOT
            this.panelRoot.Controls.Add(this.panelContent);
            this.panelRoot.Controls.Add(this.panelSidebar);

            // Eventos
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.btnMinimize.Click += new EventHandler(this.btnMinimize_Click);
            this.btnMaximize.Click += new EventHandler(this.btnMaximize_Click);
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);
            this.cboPeriodo.SelectedIndexChanged += new EventHandler(this.cboPeriodo_SelectedIndexChanged);

            this.Load += new EventHandler(this.FormDashboardAdmin_Load);

            this.ResumeLayout(false);
        }

        // === Métodos auxiliares de construção de UI ===

        private void ConfigureSidebarButton(Button btn, string texto, bool ativo)
        {
            btn.AutoSize = false;
            btn.Width = 200;
            btn.Height = 40;
            btn.Margin = new Padding(0, 4, 0, 4);
            btn.Text = texto;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = ativo ? 1 : 0;
            btn.FlatAppearance.BorderColor = ativo ? Color.FromArgb(0xE6, 0xDF, 0xD2) : Color.Transparent;
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btn.Cursor = Cursors.Hand;
            btn.BackColor = ativo ? Color.FromArgb(0xF2, 0xEB, 0xE2) : Color.Transparent;
        }

        private void ConfigureWindowButton(Button btn, string texto)
        {
            btn.Text = texto;
            btn.Width = 32;
            btn.Height = 24;
            btn.Margin = new Padding(4, 4, 0, 4);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Color.FromArgb(0x33, 0x33, 0x33);
            btn.Cursor = Cursors.Hand;
        }

        private void ConfigureKpiCard(
            Panel card,
            string titulo,
            string valor,
            string deltaTexto,
            Color deltaColor,
            out Label lblTitulo,
            out Label lblValor,
            out Label lblDelta)
        {
            card.Margin = new Padding(0, 0, 16, 0);
            card.Dock = DockStyle.Fill;
            card.Padding = new Padding(16);
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;

            lblTitulo = new Label();
            lblTitulo.Text = titulo;
            lblTitulo.AutoSize = true;
            lblTitulo.ForeColor = Color.FromArgb(0x6B, 0x72, 0x80);
            lblTitulo.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitulo.Location = new Point(4, 4);

            lblValor = new Label();
            lblValor.Text = valor;
            lblValor.AutoSize = true;
            lblValor.Font = new Font("Segoe UI", 26F, FontStyle.Bold, GraphicsUnit.Point);
            lblValor.ForeColor = Color.FromArgb(0x11, 0x18, 0x27);
            lblValor.Location = new Point(4, 36);

            lblDelta = new Label();
            lblDelta.Text = deltaTexto;
            lblDelta.AutoSize = true;
            lblDelta.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblDelta.ForeColor = deltaColor;
            lblDelta.Location = new Point(4, 80);

            card.Controls.Add(lblTitulo);
            card.Controls.Add(lblValor);
            card.Controls.Add(lblDelta);
        }

        private void ConfigureQuickCard(Panel card, string titulo, out Label lblTitulo)
        {
            card.Margin = new Padding(0, 0, 16, 0);
            card.Dock = DockStyle.Fill;
            card.Padding = new Padding(16);
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Cursor = Cursors.Hand;

            lblTitulo = new Label();
            lblTitulo.Text = titulo;
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitulo.ForeColor = Color.FromArgb(0x11, 0x18, 0x27);
            lblTitulo.Location = new Point(4, 12);

            card.Controls.Add(lblTitulo);
        }

        private void ConfigureChartCard(
            Panel card,
            string titulo,
            out Label lblTitulo,
            TableLayoutPanel tableBars)
        {
            card.Margin = new Padding(0, 0, 16, 0);
            card.Dock = DockStyle.Fill;
            card.Padding = new Padding(16);
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;

            lblTitulo = new Label();
            lblTitulo.Text = titulo;
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitulo.ForeColor = Color.FromArgb(0x11, 0x18, 0x27);
            lblTitulo.Location = new Point(4, 4);

            tableBars.Location = new Point(4, 32);
            tableBars.Size = new Size(card.Width - 32, card.Height - 40);
            tableBars.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            tableBars.ColumnCount = 3;
            tableBars.RowCount = 5;
            tableBars.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            tableBars.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableBars.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            for (int i = 0; i < 5; i++)
            {
                tableBars.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            }

            card.Controls.Add(lblTitulo);
            card.Controls.Add(tableBars);
        }

        private void AddBarRow(TableLayoutPanel table, string label, float larguraRelativa, Color cor, string valor)
        {
            int rowIndex = table.RowCount - 1; // rows já criadas

            Label lbl = new Label();
            lbl.Text = label;
            lbl.AutoSize = true;
            lbl.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            lblValor.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblValor.ForeColor = Color.FromArgb(0x11, 0x18, 0x27);
            lblValor.Margin = new Padding(4, 6, 0, 0);

            table.Controls.Add(lbl, 0, rowIndex);
            table.Controls.Add(barBackground, 1, rowIndex);
            table.Controls.Add(lblValor, 2, rowIndex);
        }

        #endregion
    }
}

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ClickDesk.Forms.Chamados
{
    partial class FormAprovarChamados
    {
        private System.ComponentModel.IContainer components = null;

        // ===== CONTROLES PRINCIPAIS =====
        private Panel panelSidebar;
        private Panel panelTopbar;
        private Panel panelMain;
        private FlowLayoutPanel flowCards;
        
        // ===== SIDEBAR CONTROLES =====
        private Label lblLogo;
        private Button btnDashboard;
        private Button btnNovoChamado;
        private Button btnMeusChamados;
        private Button btnAprovarChamados;
        private Button btnFAQ;
        private Panel panelUserMenu;
        private Label lblUser;
        
        // ===== TOPBAR CONTROLES =====
        private Label lblPageTitle;
        private Button btnClose;
        private Button btnMaximize;
        private Button btnMinimize;
        
        // ===== MAIN AREA CONTROLES =====
        private Panel panelFilters;
        private ComboBox comboPageSize;
        private TextBox txtSearch;
        private Panel panelPagination;
        private Button btnPrev;
        private Button btnNext;
        private Label lblPageIndex;
        private Label lblRangeInfo;

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
            this.SuspendLayout();
            
            // ===== FORM CONFIGURATION =====
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Name = "FormAprovarChamados";
            this.Text = "ClickDesk - Aprovar Chamados";
            this.BackColor = ColorTranslator.FromHtml("#EDE6D9");
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            
            CreateControls();
            this.ResumeLayout(false);
        }

        private void CreateControls()
        {
            // ===== SIDEBAR (240px) =====
            this.panelSidebar = new Panel();
            this.panelSidebar.Size = new Size(240, this.Height);
            this.panelSidebar.Location = new Point(0, 0);
            this.panelSidebar.BackColor = ColorTranslator.FromHtml("#F2EEE7");
            this.panelSidebar.Dock = DockStyle.Left;
            
            // Simular gradiente da sidebar
            this.panelSidebar.Paint += (s, e) =>
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    this.panelSidebar.ClientRectangle,
                    ColorTranslator.FromHtml("#F2EEE7"),
                    ColorTranslator.FromHtml("#EFEAE2"),
                    LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, this.panelSidebar.ClientRectangle);
                }
            };
            
            // Logo ClickDesk
            this.lblLogo = new Label();
            this.lblLogo.Size = new Size(200, 60);
            this.lblLogo.Location = new Point(20, 30);
            this.lblLogo.Text = "ClickDesk";
            this.lblLogo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            this.lblLogo.ForeColor = ColorTranslator.FromHtml("#F28A1A");
            this.lblLogo.BackColor = Color.Transparent;
            this.panelSidebar.Controls.Add(this.lblLogo);
            
            // Menu buttons
            int yPos = 120;
            
            this.btnDashboard = CreateMenuButton("🏠  Dashboard", yPos);
            this.panelSidebar.Controls.Add(this.btnDashboard);
            yPos += 60;
            
            this.btnNovoChamado = CreateMenuButton("➕  Novo Chamado", yPos);
            this.panelSidebar.Controls.Add(this.btnNovoChamado);
            yPos += 60;
            
            this.btnMeusChamados = CreateMenuButton("📋  Meus Chamados", yPos);
            this.panelSidebar.Controls.Add(this.btnMeusChamados);
            yPos += 60;
            
            this.btnAprovarChamados = CreateMenuButton("✅  Aprovar Chamados", yPos);
            this.btnAprovarChamados.BackColor = ColorTranslator.FromHtml("#F28A1A");
            this.btnAprovarChamados.ForeColor = Color.White;
            this.panelSidebar.Controls.Add(this.btnAprovarChamados);
            yPos += 60;
            
            this.btnFAQ = CreateMenuButton("❓  FAQ", yPos);
            this.panelSidebar.Controls.Add(this.btnFAQ);
            
            // Menu do usuário no rodapé
            this.panelUserMenu = new Panel();
            this.panelUserMenu.Size = new Size(200, 60);
            this.panelUserMenu.Location = new Point(20, this.Height - 80);
            this.panelUserMenu.BackColor = Color.Transparent;
            this.panelUserMenu.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            
            this.lblUser = new Label();
            this.lblUser.Size = new Size(180, 30);
            this.lblUser.Location = new Point(10, 15);
            this.lblUser.Text = "👤 Admin";
            this.lblUser.Font = new Font("Segoe UI", 10);
            this.lblUser.ForeColor = ColorTranslator.FromHtml("#6F6F6F");
            this.lblUser.BackColor = Color.Transparent;
            this.panelUserMenu.Controls.Add(this.lblUser);
            
            this.panelSidebar.Controls.Add(this.panelUserMenu);
            this.Controls.Add(this.panelSidebar);
            
            // ===== TOPBAR (60px) =====
            this.panelTopbar = new Panel();
            this.panelTopbar.Size = new Size(this.Width - 240, 60);
            this.panelTopbar.Location = new Point(240, 0);
            this.panelTopbar.BackColor = Color.White;
            this.panelTopbar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            
            // Título da página
            this.lblPageTitle = new Label();
            this.lblPageTitle.Size = new Size(400, 40);
            this.lblPageTitle.Location = new Point(32, 10);
            this.lblPageTitle.Text = "Aprovação de Chamados";
            this.lblPageTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            this.lblPageTitle.ForeColor = ColorTranslator.FromHtml("#1E2A22");
            this.lblPageTitle.BackColor = Color.Transparent;
            this.lblPageTitle.TextAlign = ContentAlignment.MiddleLeft;
            this.panelTopbar.Controls.Add(this.lblPageTitle);
            
            // Controles de janela
            this.btnClose = new Button();
            this.btnClose.Size = new Size(44, 44);
            this.btnClose.Location = new Point(this.panelTopbar.Width - 50, 8);
            this.btnClose.Text = "✕";
            this.btnClose.Font = new Font("Segoe UI", 12);
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.BackColor = Color.Transparent;
            this.btnClose.ForeColor = ColorTranslator.FromHtml("#666");
            this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnClose.Cursor = Cursors.Hand;
            this.panelTopbar.Controls.Add(this.btnClose);
            
            this.btnMaximize = new Button();
            this.btnMaximize.Size = new Size(44, 44);
            this.btnMaximize.Location = new Point(this.panelTopbar.Width - 94, 8);
            this.btnMaximize.Text = "🗖";
            this.btnMaximize.Font = new Font("Segoe UI", 10);
            this.btnMaximize.FlatStyle = FlatStyle.Flat;
            this.btnMaximize.FlatAppearance.BorderSize = 0;
            this.btnMaximize.BackColor = Color.Transparent;
            this.btnMaximize.ForeColor = ColorTranslator.FromHtml("#666");
            this.btnMaximize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnMaximize.Cursor = Cursors.Hand;
            this.panelTopbar.Controls.Add(this.btnMaximize);
            
            this.btnMinimize = new Button();
            this.btnMinimize.Size = new Size(44, 44);
            this.btnMinimize.Location = new Point(this.panelTopbar.Width - 138, 8);
            this.btnMinimize.Text = "–";
            this.btnMinimize.Font = new Font("Segoe UI", 12);
            this.btnMinimize.FlatStyle = FlatStyle.Flat;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.BackColor = Color.Transparent;
            this.btnMinimize.ForeColor = ColorTranslator.FromHtml("#666");
            this.btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnMinimize.Cursor = Cursors.Hand;
            this.panelTopbar.Controls.Add(this.btnMinimize);
            
            this.Controls.Add(this.panelTopbar);
            
            // ===== MAIN CONTENT AREA =====
            this.panelMain = new Panel();
            this.panelMain.Size = new Size(this.Width - 240, this.Height - 60);
            this.panelMain.Location = new Point(240, 60);
            this.panelMain.BackColor = ColorTranslator.FromHtml("#EDE6D9");
            this.panelMain.Anchor = AnchorStyles.Fill;
            this.panelMain.Padding = new Padding(32);
            
            // ===== FILTERS PANEL =====
            this.panelFilters = new Panel();
            this.panelFilters.Size = new Size(this.panelMain.Width - 64, 60);
            this.panelFilters.Location = new Point(32, 20);
            this.panelFilters.BackColor = Color.Transparent;
            this.panelFilters.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            
            // ComboBox Page Size
            Label lblEntries = new Label();
            lblEntries.Size = new Size(60, 24);
            lblEntries.Location = new Point(0, 18);
            lblEntries.Text = "Mostrar";
            lblEntries.Font = new Font("Segoe UI", 10);
            lblEntries.ForeColor = ColorTranslator.FromHtml("#6F6F6F");
            lblEntries.BackColor = Color.Transparent;
            lblEntries.TextAlign = ContentAlignment.MiddleLeft;
            this.panelFilters.Controls.Add(lblEntries);
            
            this.comboPageSize = new ComboBox();
            this.comboPageSize.Size = new Size(70, 32);
            this.comboPageSize.Location = new Point(65, 14);
            this.comboPageSize.Font = new Font("Segoe UI", 10);
            this.comboPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboPageSize.Items.AddRange(new object[] { "5", "10", "15", "25", "50" });
            this.comboPageSize.SelectedIndex = 1; // 10
            this.comboPageSize.BackColor = Color.White;
            this.comboPageSize.ForeColor = ColorTranslator.FromHtml("#1E2A22");
            this.panelFilters.Controls.Add(this.comboPageSize);
            
            Label lblEntries2 = new Label();
            lblEntries2.Size = new Size(60, 24);
            lblEntries2.Location = new Point(140, 18);
            lblEntries2.Text = "entradas";
            lblEntries2.Font = new Font("Segoe UI", 10);
            lblEntries2.ForeColor = ColorTranslator.FromHtml("#6F6F6F");
            lblEntries2.BackColor = Color.Transparent;
            lblEntries2.TextAlign = ContentAlignment.MiddleLeft;
            this.panelFilters.Controls.Add(lblEntries2);
            
            // Search TextBox
            Label lblSearch = new Label();
            lblSearch.Size = new Size(50, 24);
            lblSearch.Location = new Point(this.panelFilters.Width - 300, 18);
            lblSearch.Text = "Buscar:";
            lblSearch.Font = new Font("Segoe UI", 10);
            lblSearch.ForeColor = ColorTranslator.FromHtml("#6F6F6F");
            lblSearch.BackColor = Color.Transparent;
            lblSearch.TextAlign = ContentAlignment.MiddleLeft;
            lblSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.panelFilters.Controls.Add(lblSearch);
            
            this.txtSearch = new TextBox();
            this.txtSearch.Size = new Size(200, 32);
            this.txtSearch.Location = new Point(this.panelFilters.Width - 240, 14);
            this.txtSearch.Font = new Font("Segoe UI", 10);
            this.txtSearch.BackColor = Color.White;
            this.txtSearch.ForeColor = ColorTranslator.FromHtml("#1E2A22");
            this.txtSearch.BorderStyle = BorderStyle.FixedSingle;
            this.txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.panelFilters.Controls.Add(this.txtSearch);
            
            this.panelMain.Controls.Add(this.panelFilters);
            
            // ===== CARDS FLOW PANEL =====
            this.flowCards = new FlowLayoutPanel();
            this.flowCards.Size = new Size(this.panelMain.Width - 64, this.panelMain.Height - 160);
            this.flowCards.Location = new Point(32, 100);
            this.flowCards.BackColor = Color.Transparent;
            this.flowCards.AutoScroll = true;
            this.flowCards.FlowDirection = FlowDirection.LeftToRight;
            this.flowCards.WrapContents = true;
            this.flowCards.Padding = new Padding(10);
            this.flowCards.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            this.panelMain.Controls.Add(this.flowCards);
            
            // ===== PAGINATION PANEL =====
            this.panelPagination = new Panel();
            this.panelPagination.Size = new Size(this.panelMain.Width - 64, 60);
            this.panelPagination.Location = new Point(32, this.panelMain.Height - 80);
            this.panelPagination.BackColor = Color.Transparent;
            this.panelPagination.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            
            // Range Info
            this.lblRangeInfo = new Label();
            this.lblRangeInfo.Size = new Size(250, 30);
            this.lblRangeInfo.Location = new Point(0, 15);
            this.lblRangeInfo.Text = "Mostrando 1–10 de 25 entradas";
            this.lblRangeInfo.Font = new Font("Segoe UI", 10);
            this.lblRangeInfo.ForeColor = ColorTranslator.FromHtml("#6F6F6F");
            this.lblRangeInfo.BackColor = Color.Transparent;
            this.lblRangeInfo.TextAlign = ContentAlignment.MiddleLeft;
            this.panelPagination.Controls.Add(this.lblRangeInfo);
            
            // Pagination Controls (right side)
            this.btnNext = new Button();
            this.btnNext.Size = new Size(80, 36);
            this.btnNext.Location = new Point(this.panelPagination.Width - 80, 12);
            this.btnNext.Text = "Próximo";
            this.btnNext.Font = new Font("Segoe UI", 9);
            this.btnNext.FlatStyle = FlatStyle.Flat;
            this.btnNext.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#E5E1D8");
            this.btnNext.BackColor = Color.White;
            this.btnNext.ForeColor = ColorTranslator.FromHtml("#1E2A22");
            this.btnNext.Cursor = Cursors.Hand;
            this.btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.panelPagination.Controls.Add(this.btnNext);
            
            this.lblPageIndex = new Label();
            this.lblPageIndex.Size = new Size(50, 36);
            this.lblPageIndex.Location = new Point(this.panelPagination.Width - 170, 12);
            this.lblPageIndex.Text = "1";
            this.lblPageIndex.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.lblPageIndex.ForeColor = ColorTranslator.FromHtml("#F28A1A");
            this.lblPageIndex.BackColor = ColorTranslator.FromHtml("#FFEAD6");
            this.lblPageIndex.TextAlign = ContentAlignment.MiddleCenter;
            this.lblPageIndex.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.panelPagination.Controls.Add(this.lblPageIndex);
            
            this.btnPrev = new Button();
            this.btnPrev.Size = new Size(80, 36);
            this.btnPrev.Location = new Point(this.panelPagination.Width - 260, 12);
            this.btnPrev.Text = "Anterior";
            this.btnPrev.Font = new Font("Segoe UI", 9);
            this.btnPrev.FlatStyle = FlatStyle.Flat;
            this.btnPrev.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#E5E1D8");
            this.btnPrev.BackColor = Color.White;
            this.btnPrev.ForeColor = ColorTranslator.FromHtml("#1E2A22");
            this.btnPrev.Cursor = Cursors.Hand;
            this.btnPrev.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.panelPagination.Controls.Add(this.btnPrev);
            
            this.panelMain.Controls.Add(this.panelPagination);
            this.Controls.Add(this.panelMain);
        }

        private Button CreateMenuButton(string text, int y)
        {
            var btn = new Button();
            btn.Size = new Size(200, 50);
            btn.Location = new Point(20, y);
            btn.Text = text;
            btn.Font = new Font("Segoe UI", 11);
            btn.ForeColor = ColorTranslator.FromHtml("#6F6F6F");
            btn.BackColor = Color.Transparent;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Cursor = Cursors.Hand;
            
            // Hover effect
            btn.MouseEnter += (s, e) => btn.BackColor = ColorTranslator.FromHtml("#E8E3DA");
            btn.MouseLeave += (s, e) => {
                if (btn != this.btnAprovarChamados)
                    btn.BackColor = Color.Transparent;
            };
            
            return btn;
        }
        #endregion
    }
}

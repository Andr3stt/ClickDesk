using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clickdesk
{
    partial class FormMeusChamados
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelRoot;
        private Panel panelTop;
        private Label lblTitulo;
        private Panel panelMain;
        private Panel panelToolbar;
        private Label lblStatus;
        private ComboBox cboStatus;
        private Label lblCategoria;
        private ComboBox cboCategoria;
        private Button btnAtualizar;
        private TableLayoutPanel tblKpis;
        private Panel cardTotal;
        private Panel cardAtendidos;
        private Panel cardEspera;
        private Panel cardProgresso;
        private Label lblTotalTitulo;
        private Label lblTotalValor;
        private Label lblAtendidosTitulo;
        private Label lblAtendidosValor;
        private Label lblEsperaTitulo;
        private Label lblEsperaValor;
        private Label lblProgressoTitulo;
        private Label lblProgressoValor;
        private FlowLayoutPanel flowTickets;
        private Panel panelEmpty;
        private Label lblEmpty;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelRoot = new Panel();
            this.panelMain = new Panel();
            this.panelEmpty = new Panel();
            this.lblEmpty = new Label();
            this.flowTickets = new FlowLayoutPanel();
            this.tblKpis = new TableLayoutPanel();
            this.cardTotal = new Panel();
            this.lblTotalValor = new Label();
            this.lblTotalTitulo = new Label();
            this.cardAtendidos = new Panel();
            this.lblAtendidosValor = new Label();
            this.lblAtendidosTitulo = new Label();
            this.cardEspera = new Panel();
            this.lblEsperaValor = new Label();
            this.lblEsperaTitulo = new Label();
            this.cardProgresso = new Panel();
            this.lblProgressoValor = new Label();
            this.lblProgressoTitulo = new Label();
            this.panelToolbar = new Panel();
            this.btnAtualizar = new Button();
            this.cboCategoria = new ComboBox();
            this.lblCategoria = new Label();
            this.cboStatus = new ComboBox();
            this.lblStatus = new Label();
            this.panelTop = new Panel();
            this.lblTitulo = new Label();

            this.panelRoot.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelEmpty.SuspendLayout();
            this.tblKpis.SuspendLayout();
            this.cardTotal.SuspendLayout();
            this.cardAtendidos.SuspendLayout();
            this.cardEspera.SuspendLayout();
            this.cardProgresso.SuspendLayout();
            this.panelToolbar.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();

            // ROOT
            this.panelRoot.BackColor = Color.FromArgb(244, 239, 230);
            this.panelRoot.Dock = DockStyle.Fill;
            this.panelRoot.Controls.Add(this.panelMain);
            this.panelRoot.Controls.Add(this.panelTop);

            // TOPBAR
            this.panelTop.BackColor = Color.White;
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Padding = new Padding(32, 0, 32, 0);
            this.panelTop.Height = 56;
            this.panelTop.Controls.Add(this.lblTitulo);

            this.lblTitulo.Text = "Meus Chamados";
            this.lblTitulo.Font = new Font("Segoe UI Semibold", 18F);
            this.lblTitulo.ForeColor = Color.FromArgb(17, 24, 39);
            this.lblTitulo.Dock = DockStyle.Left;
            this.lblTitulo.Width = 300;

            // MAIN
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.Padding = new Padding(32, 24, 32, 24);
            this.panelMain.Controls.Add(this.panelEmpty);
            this.panelMain.Controls.Add(this.flowTickets);
            this.panelMain.Controls.Add(this.tblKpis);
            this.panelMain.Controls.Add(this.panelToolbar);

            // EMPTY STATE
            this.panelEmpty.Visible = false;
            this.panelEmpty.Size = new Size(600, 80);
            this.panelEmpty.Anchor = AnchorStyles.None;
            this.panelEmpty.Controls.Add(this.lblEmpty);

            this.lblEmpty.Text =
                "Nenhum chamado encontrado.\r\nAjuste os filtros ou crie um novo chamado.";
            this.lblEmpty.Font = new Font("Segoe UI", 11F);
            this.lblEmpty.ForeColor = Color.FromArgb(107, 114, 128);
            this.lblEmpty.Dock = DockStyle.Fill;
            this.lblEmpty.TextAlign = ContentAlignment.MiddleCenter;

            // FLOWLAYOUT
            this.flowTickets.Dock = DockStyle.Fill;
            this.flowTickets.AutoScroll = true;
            this.flowTickets.Padding = new Padding(0, 8, 0, 8);

            // KPIs
            this.tblKpis.Dock = DockStyle.Top;
            this.tblKpis.ColumnCount = 4;
            this.tblKpis.Height = 84;

            for (int i = 0; i < 4; i++)
                this.tblKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            this.tblKpis.Controls.Add(this.cardTotal, 0, 0);
            this.tblKpis.Controls.Add(this.cardAtendidos, 1, 0);
            this.tblKpis.Controls.Add(this.cardEspera, 2, 0);
            this.tblKpis.Controls.Add(this.cardProgresso, 3, 0);

            CriarCardKpi(this.cardTotal, out this.lblTotalTitulo, out this.lblTotalValor,
                "TOTAL DE CHAMADOS", "0");

            CriarCardKpi(this.cardAtendidos, out this.lblAtendidosTitulo, out this.lblAtendidosValor,
                "CHAMADOS ATENDIDOS", "0");

            CriarCardKpi(this.cardEspera, out this.lblEsperaTitulo, out this.lblEsperaValor,
                "CHAMADOS EM ESPERA", "0");

            CriarCardKpi(this.cardProgresso, out this.lblProgressoTitulo, out this.lblProgressoValor,
                "CHAMADOS EM PROGRESSO", "0");

            // TOOLBAR
            this.panelToolbar.Dock = DockStyle.Top;
            this.panelToolbar.Height = 56;

            this.lblStatus.Text = "Status";
            this.lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblStatus.Location = new Point(3, 20);

            this.cboStatus.Location = new Point(56, 16);
            this.cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboStatus.Width = 180;

            this.lblCategoria.Text = "Categoria";
            this.lblCategoria.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblCategoria.Location = new Point(256, 20);

            this.cboCategoria.Location = new Point(322, 16);
            this.cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboCategoria.Width = 200;

            this.btnAtualizar.Text = "Atualizar";
            this.btnAtualizar.FlatStyle = FlatStyle.Flat;
            this.btnAtualizar.Location = new Point(540, 14);
            this.btnAtualizar.Width = 104;

            this.panelToolbar.Controls.Add(this.lblStatus);
            this.panelToolbar.Controls.Add(this.cboStatus);
            this.panelToolbar.Controls.Add(this.lblCategoria);
            this.panelToolbar.Controls.Add(this.cboCategoria);
            this.panelToolbar.Controls.Add(this.btnAtualizar);

            // FORM
            this.Controls.Add(this.panelRoot);
            this.Text = "Meus Chamados";
            this.ClientSize = new Size(1280, 720);

            this.panelRoot.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelEmpty.ResumeLayout(false);
            this.tblKpis.ResumeLayout(false);
            this.cardTotal.ResumeLayout(false);
            this.cardTotal.PerformLayout();
            this.cardAtendidos.ResumeLayout(false);
            this.cardAtendidos.PerformLayout();
            this.cardEspera.ResumeLayout(false);
            this.cardEspera.PerformLayout();
            this.cardProgresso.ResumeLayout(false);
            this.cardProgresso.PerformLayout();
            this.panelToolbar.ResumeLayout(false);
            this.panelToolbar.PerformLayout();
            this.panelTop.ResumeLayout(false);

            this.ResumeLayout(false);
        }

        private void CriarCardKpi(Panel panel, out Label lblTitulo, out Label lblValor,
            string titulo, string valor)
        {
            panel.BackColor = Color.White;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Padding = new Padding(12);

            lblTitulo = new Label
            {
                Text = titulo,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(9, 6),
                AutoSize = true
            };

            lblValor = new Label
            {
                Text = valor,
                Font = new Font("Segoe UI Semibold", 26F),
                ForeColor = Color.FromArgb(17, 24, 39),
                Location = new Point(9, 28),
                AutoSize = true
            };

            panel.Controls.Add(lblTitulo);
            panel.Controls.Add(lblValor);
        }
    }
}

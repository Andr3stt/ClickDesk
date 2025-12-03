using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ClickDesk.Forms.Chamados
{
    partial class FormNovoChamado
    {
        private IContainer components = null;

        private Panel panelRoot;
        private Panel panelSidebar;
        private Panel panelTopbar;
        private Panel panelMain;
        private Panel panelCard;

        private Label lblLogo;
        private Label lblTituloPagina;
        private Label lblSectionTitle;

        private Label lblTitulo;
        private TextBox txtTitulo;

        private Label lblCategoria;
        private ComboBox cboCategoria;

        private Label lblDepartamento;
        private ComboBox cboDepartamento;

        private Label lblLocalizacao;
        private TextBox txtLocalizacao;

        private Label lblDescricao;
        private TextBox txtDescricao;

        private Label lblAnexos;
        private Panel panelDropzone;
        private Label lblDropTitle;
        private Label lblDropHint;

        private FlowLayoutPanel flowAnexos;

        private Button btnCancelar;
        private Button btnCriar;

        private OpenFileDialog openFileDialogAnexos;

        /// <summary>
        /// Limpar recursos.
        /// </summary>
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
            this.components = new Container();
            this.panelRoot = new Panel();
            this.panelSidebar = new Panel();
            this.lblLogo = new Label();
            this.panelTopbar = new Panel();
            this.lblTituloPagina = new Label();
            this.panelMain = new Panel();
            this.panelCard = new Panel();
            this.lblSectionTitle = new Label();

            this.lblTitulo = new Label();
            this.txtTitulo = new TextBox();

            this.lblCategoria = new Label();
            this.cboCategoria = new ComboBox();

            this.lblDepartamento = new Label();
            this.cboDepartamento = new ComboBox();

            this.lblLocalizacao = new Label();
            this.txtLocalizacao = new TextBox();

            this.lblDescricao = new Label();
            this.txtDescricao = new TextBox();

            this.lblAnexos = new Label();
            this.panelDropzone = new Panel();
            this.lblDropTitle = new Label();
            this.lblDropHint = new Label();

            this.flowAnexos = new FlowLayoutPanel();

            this.btnCancelar = new Button();
            this.btnCriar = new Button();

            this.openFileDialogAnexos = new OpenFileDialog();

            // 
            // FormNovoChamado
            // 
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new Size(1280, 720);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Novo Chamado";
            this.BackColor = Color.FromArgb(237, 230, 217); // #EDE6D9

            // ROOT
            this.panelRoot.Dock = DockStyle.Fill;
            this.panelRoot.BackColor = Color.FromArgb(237, 230, 217);
            this.panelRoot.Controls.Add(this.panelMain);
            this.panelRoot.Controls.Add(this.panelTopbar);
            this.panelRoot.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.panelRoot);

            // SIDEBAR
            this.panelSidebar.Dock = DockStyle.Left;
            this.panelSidebar.Width = 220;
            this.panelSidebar.Padding = new Padding(16, 18, 16, 18);
            this.panelSidebar.BackColor = Color.FromArgb(242, 238, 231);
            this.panelSidebar.Paint += PanelSidebar_Paint;

            this.lblLogo.AutoSize = true;
            this.lblLogo.Text = "Clickdesk";
            this.lblLogo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblLogo.ForeColor = Color.FromArgb(242, 138, 26);
            this.lblLogo.Location = new Point(12, 8);

            this.panelSidebar.Controls.Add(this.lblLogo);

            // TOPBAR
            this.panelTopbar.Dock = DockStyle.Top;
            this.panelTopbar.Height = 60;
            this.panelTopbar.BackColor = Color.White;
            this.panelTopbar.Padding = new Padding(0, 0, 16, 0);
            this.panelTopbar.BorderStyle = BorderStyle.FixedSingle;

            this.lblTituloPagina.AutoSize = false;
            this.lblTituloPagina.Text = "Novo Chamado";
            this.lblTituloPagina.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTituloPagina.ForeColor = Color.FromArgb(17, 24, 39);
            this.lblTituloPagina.Location = new Point(240, 16);
            this.lblTituloPagina.Size = new Size(400, 28);

            this.panelTopbar.Controls.Add(this.lblTituloPagina);

            // MAIN
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.BackColor = Color.FromArgb(237, 230, 217);
            this.panelMain.Padding = new Padding(240, 80, 40, 40); // espaço p/ sidebar + topbar
            this.panelMain.Controls.Add(this.panelCard);

            // CARD
            this.panelCard.BackColor = Color.White;
            this.panelCard.Dock = DockStyle.Fill;
            this.panelCard.Padding = new Padding(32);
            this.panelCard.Paint += PanelCard_Paint;

            // SECTION TITLE
            this.lblSectionTitle.AutoSize = true;
            this.lblSectionTitle.Text = "Informações do Chamado";
            this.lblSectionTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblSectionTitle.ForeColor = Color.FromArgb(17, 24, 39);
            this.lblSectionTitle.Location = new Point(32, 24);

            // Layout manual (sem TableLayout para manter legível)
            int leftColX = 32;
            int rightColX = 32 + 560; // espaço visual
            int startY = 72;
            int lineHeight = 32;
            int vGap = 24;
            int labelHeight = 20;

            // TÍTULO
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Text = "Título do Chamado *";
            this.lblTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(107, 114, 128);
            this.lblTitulo.Location = new Point(leftColX, startY);

            this.txtTitulo.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            this.txtTitulo.Location = new Point(leftColX, startY + labelHeight + 4);
            this.txtTitulo.Size = new Size(540, 27);

            // CATEGORIA
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Text = "Categoria *";
            this.lblCategoria.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblCategoria.ForeColor = Color.FromArgb(107, 114, 128);
            this.lblCategoria.Location = new Point(rightColX, startY);

            this.cboCategoria.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            this.cboCategoria.Location = new Point(rightColX, startY + labelHeight + 4);
            this.cboCategoria.Size = new Size(300, 27);
            this.cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList;

            // DEPARTAMENTO
            int row2Y = startY + labelHeight + 4 + lineHeight + vGap;

            this.lblDepartamento.AutoSize = true;
            this.lblDepartamento.Text = "Departamento *";
            this.lblDepartamento.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblDepartamento.ForeColor = Color.FromArgb(107, 114, 128);
            this.lblDepartamento.Location = new Point(leftColX, row2Y);

            this.cboDepartamento.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            this.cboDepartamento.Location = new Point(leftColX, row2Y + labelHeight + 4);
            this.cboDepartamento.Size = new Size(360, 27);
            this.cboDepartamento.DropDownStyle = ComboBoxStyle.DropDownList;

            // LOCALIZAÇÃO
            this.lblLocalizacao.AutoSize = true;
            this.lblLocalizacao.Text = "Localização";
            this.lblLocalizacao.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblLocalizacao.ForeColor = Color.FromArgb(107, 114, 128);
            this.lblLocalizacao.Location = new Point(rightColX, row2Y);

            this.txtLocalizacao.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            this.txtLocalizacao.Location = new Point(rightColX, row2Y + labelHeight + 4);
            this.txtLocalizacao.Size = new Size(300, 27);

            // DESCRIÇÃO + ANEXOS
            int row3Y = row2Y + labelHeight + 4 + lineHeight + vGap;

            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Text = "Descrição Detalhada *";
            this.lblDescricao.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblDescricao.ForeColor = Color.FromArgb(107, 114, 128);
            this.lblDescricao.Location = new Point(leftColX, row3Y);

            this.txtDescricao.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            this.txtDescricao.Location = new Point(leftColX, row3Y + labelHeight + 4);
            this.txtDescricao.Multiline = true;
            this.txtDescricao.ScrollBars = ScrollBars.Vertical;
            this.txtDescricao.Size = new Size(540, 160);

            // ANEXOS
            this.lblAnexos.AutoSize = true;
            this.lblAnexos.Text = "Anexos (opcional)";
            this.lblAnexos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblAnexos.ForeColor = Color.FromArgb(107, 114, 128);
            this.lblAnexos.Location = new Point(rightColX, row3Y);

            this.panelDropzone.Location = new Point(rightColX, row3Y + labelHeight + 4);
            this.panelDropzone.Size = new Size(340, 160);
            this.panelDropzone.BorderStyle = BorderStyle.FixedSingle;
            this.panelDropzone.BackColor = Color.FromArgb(248, 249, 250);
            this.panelDropzone.AllowDrop = true;
            this.panelDropzone.Cursor = Cursors.Hand;
            this.panelDropzone.Click += PanelDropzone_Click;

            this.lblDropTitle.AutoSize = true;
            this.lblDropTitle.Text = "Clique ou arraste arquivos";
            this.lblDropTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblDropTitle.ForeColor = Color.FromArgb(31, 41, 55);
            this.lblDropTitle.Location = new Point(40, 40);

            this.lblDropHint.AutoSize = true;
            this.lblDropHint.Text = "JPG, PNG, PDF, DOC, XLS, ZIP\n(máx. 10MB)";
            this.lblDropHint.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            this.lblDropHint.ForeColor = Color.FromArgb(107, 114, 128);
            this.lblDropHint.Location = new Point(40, 64);

            this.panelDropzone.Controls.Add(this.lblDropTitle);
            this.panelDropzone.Controls.Add(this.lblDropHint);

            // LISTA DE ANEXOS
            this.flowAnexos.Location = new Point(rightColX, row3Y + labelHeight + 4 + 160 + 8);
            this.flowAnexos.Size = new Size(340, 80);
            this.flowAnexos.AutoScroll = true;
            this.flowAnexos.WrapContents = true;
            this.flowAnexos.FlowDirection = FlowDirection.TopDown;

            // BOTÕES
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnCancelar.Size = new Size(120, 36);
            this.btnCancelar.BackColor = Color.White;
            this.btnCancelar.ForeColor = Color.FromArgb(75, 85, 99);
            this.btnCancelar.FlatStyle = FlatStyle.Flat;
            this.btnCancelar.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            this.btnCancelar.Location = new Point(this.panelCard.Width - 280, this.panelCard.Height - 70);
            this.btnCancelar.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancelar.Click += BtnCancelar_Click;

            this.btnCriar.Text = "Criar Chamado";
            this.btnCriar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnCriar.Size = new Size(140, 36);
            this.btnCriar.BackColor = Color.FromArgb(242, 138, 26);
            this.btnCriar.ForeColor = Color.White;
            this.btnCriar.FlatStyle = FlatStyle.Flat;
            this.btnCriar.FlatAppearance.BorderSize = 0;
            this.btnCriar.Location = new Point(this.panelCard.Width - 130, this.panelCard.Height - 70);
            this.btnCriar.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCriar.Click += BtnCriar_Click;

            // Reposicionar botões depois de adicionar ao card (usando Anchor)
            this.panelCard.Controls.Add(this.lblSectionTitle);
            this.panelCard.Controls.Add(this.lblTitulo);
            this.panelCard.Controls.Add(this.txtTitulo);
            this.panelCard.Controls.Add(this.lblCategoria);
            this.panelCard.Controls.Add(this.cboCategoria);
            this.panelCard.Controls.Add(this.lblDepartamento);
            this.panelCard.Controls.Add(this.cboDepartamento);
            this.panelCard.Controls.Add(this.lblLocalizacao);
            this.panelCard.Controls.Add(this.txtLocalizacao);
            this.panelCard.Controls.Add(this.lblDescricao);
            this.panelCard.Controls.Add(this.txtDescricao);
            this.panelCard.Controls.Add(this.lblAnexos);
            this.panelCard.Controls.Add(this.panelDropzone);
            this.panelCard.Controls.Add(this.flowAnexos);
            this.panelCard.Controls.Add(this.btnCancelar);
            this.panelCard.Controls.Add(this.btnCriar);

            // OPEN FILE DIALOG
            this.openFileDialogAnexos.Multiselect = true;
            this.openFileDialogAnexos.Filter =
                "Arquivos permitidos|*.jpg;*.jpeg;*.png;*.pdf;*.doc;*.docx;*.xls;*.xlsx;*.zip";

            this.Resize += FormNovoChamado_Resize;
        }

        #endregion
    }
}

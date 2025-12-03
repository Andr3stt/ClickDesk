using ClickDesk.Utils;
using Siticone.Desktop.UI.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace ClickDesk.Forms.Chamados
{
    partial class FormDetalhesChamado
    {
        private System.ComponentModel.IContainer components = null;

        // ===== CONTROLES PRINCIPAIS =====
        private SiticonePanel sidebarPanel;
        private SiticonePanel topbarPanel;
        private SiticonePanel mainContentPanel;
        private SiticonePanel detailsCard;
        
        // ===== SIDEBAR BUTTONS =====
        private SiticoneButton btnDashboard;
        private SiticoneButton btnNovoChamado;
        private SiticoneButton btnMeusChamados;
        private SiticoneButton btnFAQ;
        
        // ===== TOPBAR CONTROLS =====
        private SiticoneControlBox btnClose;
        private SiticoneControlBox btnMaximize;
        private SiticoneControlBox btnMinimize;
        
        // ===== CARD CONTENT =====
        private SiticoneHtmlLabel lblIdBadge;
        private SiticoneHtmlLabel lblStatusBadge;
        private SiticoneHtmlLabel lblDateChip;
        
        // ===== DETAIL FIELDS =====
        private SiticoneHtmlLabel lblSolicitanteLabel;
        private SiticoneHtmlLabel lblSolicitanteValue;
        private SiticoneHtmlLabel lblDepartamentoLabel;
        private SiticoneHtmlLabel lblDepartamentoValue;
        private SiticoneHtmlLabel lblTituloLabel;
        private SiticoneHtmlLabel lblTituloValue;
        private SiticoneHtmlLabel lblCategoriaLabel;
        private SiticoneHtmlLabel lblCategoriaValue;
        private SiticoneHtmlLabel lblDescricaoLabel;
        private SiticoneHtmlLabel lblDescricaoValue;

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
            this.Name = "FormDetalhesChamado";
            this.Text = "ClickDesk - Detalhes do Chamado";
            this.BackColor = StyleManager.BgPage;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            
            CreateControls();
            this.ResumeLayout(false);
        }

        private void CreateControls()
        {
            // ===== SIDEBAR (240px) =====
            this.sidebarPanel = new SiticonePanel();
            this.sidebarPanel.Size = new Size(240, this.Height);
            this.sidebarPanel.Location = new Point(0, 0);
            this.sidebarPanel.FillColor = StyleManager.Card;
            this.sidebarPanel.BorderRadius = 0;
            this.sidebarPanel.Dock = DockStyle.Left;
            
            // Degradê da sidebar - removido pois não é suportado
            this.sidebarPanel.FillColor = ColorTranslator.FromHtml("#F2EEE7");
            
            // Logo ClickDesk
            var lblLogo = new SiticoneHtmlLabel();
            lblLogo.Size = new Size(200, 60);
            lblLogo.Location = new Point(20, 30);
            lblLogo.Text = "<font face='Poppins' size='18' color='#F28A1A'><b>ClickDesk</b></font>";
            lblLogo.BackColor = Color.Transparent;
            this.sidebarPanel.Controls.Add(lblLogo);
            
            // Menu buttons
            int yPos = 120;
            
            this.btnDashboard = CreateMenuButton("🏠  Dashboard", yPos);
            this.sidebarPanel.Controls.Add(this.btnDashboard);
            yPos += 60;
            
            this.btnNovoChamado = CreateMenuButton("➕  Novo Chamado", yPos);
            this.sidebarPanel.Controls.Add(this.btnNovoChamado);
            yPos += 60;
            
            this.btnMeusChamados = CreateMenuButton("📋  Meus Chamados", yPos);
            this.btnMeusChamados.FillColor = ColorTranslator.FromHtml("#F28A1A");
            this.btnMeusChamados.ForeColor = Color.White;
            this.sidebarPanel.Controls.Add(this.btnMeusChamados);
            yPos += 60;
            
            this.btnFAQ = CreateMenuButton("❓  FAQ", yPos);
            this.sidebarPanel.Controls.Add(this.btnFAQ);
            
            // Menu do usuário no rodapé
            var userPanel = new SiticonePanel();
            userPanel.Size = new Size(200, 60);
            userPanel.Location = new Point(20, this.Height - 80);
            userPanel.BackColor = Color.Transparent;
            userPanel.FillColor = Color.Transparent;
            userPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            
            var lblUser = new SiticoneHtmlLabel();
            lblUser.Size = new Size(180, 30);
            lblUser.Location = new Point(10, 15);
            lblUser.Text = "<font face='Poppins' size='10' color='#6F6F6F'>👤 Usuário</font>";
            lblUser.BackColor = Color.Transparent;
            userPanel.Controls.Add(lblUser);
            
            this.sidebarPanel.Controls.Add(userPanel);
            this.Controls.Add(this.sidebarPanel);
            
            // ===== TOPBAR (56px) =====
            this.topbarPanel = new SiticonePanel();
            this.topbarPanel.Size = new Size(this.Width - 240, 56);
            this.topbarPanel.Location = new Point(240, 0);
            this.topbarPanel.FillColor = Color.White;
            this.topbarPanel.BorderRadius = 0;
            this.topbarPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            
            // Controles de janela
            this.btnClose = new SiticoneControlBox();
            this.btnClose.Size = new Size(44, 44);
            this.btnClose.Location = new Point(this.topbarPanel.Width - 50, 6);
            this.btnClose.ControlBoxType = Siticone.Desktop.UI.WinForms.Enums.ControlBoxType.CloseBox;
            this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.topbarPanel.Controls.Add(this.btnClose);
            
            this.btnMaximize = new SiticoneControlBox();
            this.btnMaximize.Size = new Size(44, 44);
            this.btnMaximize.Location = new Point(this.topbarPanel.Width - 94, 6);
            this.btnMaximize.ControlBoxType = Siticone.Desktop.UI.WinForms.Enums.ControlBoxType.MaximizeBox;
            this.btnMaximize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.topbarPanel.Controls.Add(this.btnMaximize);
            
            this.btnMinimize = new SiticoneControlBox();
            this.btnMinimize.Size = new Size(44, 44);
            this.btnMinimize.Location = new Point(this.topbarPanel.Width - 138, 6);
            this.btnMinimize.ControlBoxType = Siticone.Desktop.UI.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.topbarPanel.Controls.Add(this.btnMinimize);
            
            this.Controls.Add(this.topbarPanel);
            
            // ===== MAIN CONTENT AREA =====
            this.mainContentPanel = new SiticonePanel();
            this.mainContentPanel.Size = new Size(this.Width - 240, this.Height - 56);
            this.mainContentPanel.Location = new Point(240, 56);
            this.mainContentPanel.FillColor = StyleManager.BgPage;
            this.mainContentPanel.BorderRadius = 0;
            this.mainContentPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.mainContentPanel.Padding = new Padding(32);
            
            // ===== DETAILS CARD =====
            this.detailsCard = new SiticonePanel();
            this.detailsCard.Size = new Size(800, 600);
            this.detailsCard.Location = new Point(32, 32);
            this.detailsCard.FillColor = Color.White;
            this.detailsCard.BorderRadius = 20;
            this.detailsCard.ShadowDecoration.Enabled = true;
            this.detailsCard.ShadowDecoration.Color = Color.Gray;
            this.detailsCard.ShadowDecoration.Depth = 10;
            this.detailsCard.Padding = new Padding(24);
            
            CreateCardContent();
            
            this.mainContentPanel.Controls.Add(this.detailsCard);
            this.Controls.Add(this.mainContentPanel);
        }

        private void CreateCardContent()
        {
            int yPos = 24;
            
            // ===== BADGES NO TOPO =====
            this.lblIdBadge = new SiticoneHtmlLabel();
            this.lblIdBadge.Size = new Size(100, 32);
            this.lblIdBadge.Location = new Point(24, yPos);
            this.lblIdBadge.Text = "<font face='Poppins' size='11' color='#F28A1A'><b>CD-0000</b></font>";
            this.lblIdBadge.BackColor = ColorTranslator.FromHtml("#FFEAD6");
            this.lblId = this.lblIdBadge; // Referência para o código principal
            this.detailsCard.Controls.Add(this.lblIdBadge);
            
            this.lblStatusBadge = new SiticoneHtmlLabel();
            this.lblStatusBadge.Size = new Size(120, 32);
            this.lblStatusBadge.Location = new Point(140, yPos);
            this.lblStatusBadge.Text = "<font face='Poppins' size='10' color='#2563eb'><b>ABERTO</b></font>";
            this.lblStatusBadge.BackColor = ColorTranslator.FromHtml("#EBF4FF");
            this.lblStatus = this.lblStatusBadge; // Referência para o código principal
            this.detailsCard.Controls.Add(this.lblStatusBadge);
            
            this.lblDateChip = new SiticoneHtmlLabel();
            this.lblDateChip.Size = new Size(140, 32);
            this.lblDateChip.Location = new Point(280, yPos);
            this.lblDateChip.Text = "<font face='Poppins' size='10' color='#6F6F6F'>📅 00/00/0000</font>";
            this.lblDateChip.BackColor = ColorTranslator.FromHtml("#F9F9F9");
            this.lblDate = this.lblDateChip; // Referência para o código principal
            this.detailsCard.Controls.Add(this.lblDateChip);
            
            yPos += 60;
            
            // ===== GRID DE DETALHES =====
            
            // Solicitante
            this.lblSolicitanteLabel = new SiticoneHtmlLabel();
            this.lblSolicitanteLabel.Size = new Size(150, 25);
            this.lblSolicitanteLabel.Location = new Point(24, yPos);
            this.lblSolicitanteLabel.Text = "<font face='Poppins' size='10' color='#6F6F6F'><b>Solicitante:</b></font>";
            this.lblSolicitanteLabel.BackColor = Color.Transparent;
            this.detailsCard.Controls.Add(this.lblSolicitanteLabel);
            
            this.lblSolicitanteValue = new SiticoneHtmlLabel();
            this.lblSolicitanteValue.Size = new Size(300, 25);
            this.lblSolicitanteValue.Location = new Point(180, yPos);
            this.lblSolicitanteValue.Text = "<font face='Poppins' size='10' color='#1E2A22'>Nome do Usuário</font>";
            this.lblSolicitanteValue.BackColor = Color.Transparent;
            this.lblName = this.lblSolicitanteValue; // Referência para o código principal
            this.detailsCard.Controls.Add(this.lblSolicitanteValue);
            
            yPos += 35;
            
            // Departamento
            this.lblDepartamentoLabel = new SiticoneHtmlLabel();
            this.lblDepartamentoLabel.Size = new Size(150, 25);
            this.lblDepartamentoLabel.Location = new Point(24, yPos);
            this.lblDepartamentoLabel.Text = "<font face='Poppins' size='10' color='#6F6F6F'><b>Departamento:</b></font>";
            this.lblDepartamentoLabel.BackColor = Color.Transparent;
            this.detailsCard.Controls.Add(this.lblDepartamentoLabel);
            
            this.lblDepartamentoValue = new SiticoneHtmlLabel();
            this.lblDepartamentoValue.Size = new Size(300, 25);
            this.lblDepartamentoValue.Location = new Point(180, yPos);
            this.lblDepartamentoValue.Text = "<font face='Poppins' size='10' color='#1E2A22'>Suporte</font>";
            this.lblDepartamentoValue.BackColor = Color.Transparent;
            this.lblDept = this.lblDepartamentoValue; // Referência para o código principal
            this.detailsCard.Controls.Add(this.lblDepartamentoValue);
            
            yPos += 35;
            
            // Título
            this.lblTituloLabel = new SiticoneHtmlLabel();
            this.lblTituloLabel.Size = new Size(150, 25);
            this.lblTituloLabel.Location = new Point(24, yPos);
            this.lblTituloLabel.Text = "<font face='Poppins' size='10' color='#6F6F6F'><b>Título:</b></font>";
            this.lblTituloLabel.BackColor = Color.Transparent;
            this.detailsCard.Controls.Add(this.lblTituloLabel);
            
            this.lblTituloValue = new SiticoneHtmlLabel();
            this.lblTituloValue.Size = new Size(550, 50);
            this.lblTituloValue.Location = new Point(180, yPos);
            this.lblTituloValue.Text = "<font face='Poppins' size='10' color='#1E2A22'>Título do chamado...</font>";
            this.lblTituloValue.BackColor = Color.Transparent;
            this.lblTituloValue.AutoSize = false;
            this.lblSubject = this.lblTituloValue; // Referência para o código principal
            this.detailsCard.Controls.Add(this.lblTituloValue);
            
            yPos += 60;
            
            // Categoria
            this.lblCategoriaLabel = new SiticoneHtmlLabel();
            this.lblCategoriaLabel.Size = new Size(150, 25);
            this.lblCategoriaLabel.Location = new Point(24, yPos);
            this.lblCategoriaLabel.Text = "<font face='Poppins' size='10' color='#6F6F6F'><b>Categoria:</b></font>";
            this.lblCategoriaLabel.BackColor = Color.Transparent;
            this.detailsCard.Controls.Add(this.lblCategoriaLabel);
            
            this.lblCategoriaValue = new SiticoneHtmlLabel();
            this.lblCategoriaValue.Size = new Size(300, 25);
            this.lblCategoriaValue.Location = new Point(180, yPos);
            this.lblCategoriaValue.Text = "<font face='Poppins' size='10' color='#1E2A22'>Categoria</font>";
            this.lblCategoriaValue.BackColor = Color.Transparent;
            this.lblCategory = this.lblCategoriaValue; // Referência para o código principal
            this.detailsCard.Controls.Add(this.lblCategoriaValue);
            
            yPos += 35;
            
            // Descrição
            this.lblDescricaoLabel = new SiticoneHtmlLabel();
            this.lblDescricaoLabel.Size = new Size(150, 25);
            this.lblDescricaoLabel.Location = new Point(24, yPos);
            this.lblDescricaoLabel.Text = "<font face='Poppins' size='10' color='#6F6F6F'><b>Descrição:</b></font>";
            this.lblDescricaoLabel.BackColor = Color.Transparent;
            this.detailsCard.Controls.Add(this.lblDescricaoLabel);
            
            this.lblDescricaoValue = new SiticoneHtmlLabel();
            this.lblDescricaoValue.Size = new Size(550, 120);
            this.lblDescricaoValue.Location = new Point(180, yPos);
            this.lblDescricaoValue.Text = "<font face='Poppins' size='10' color='#1E2A22'>Descrição detalhada do problema...</font>";
            this.lblDescricaoValue.BackColor = Color.Transparent;
            this.lblDescricaoValue.AutoSize = false;
            this.lblDescription = this.lblDescricaoValue; // Referência para o código principal
            this.detailsCard.Controls.Add(this.lblDescricaoValue);
            
            yPos += 140;
            
            // ===== BOTÕES =====
            this.btnVoltar = new SiticoneButton();
            this.btnVoltar.Size = new Size(120, 45);
            this.btnVoltar.Location = new Point(24, yPos);
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.BorderRadius = 8;
            this.btnVoltar.FillColor = Color.White;
            this.btnVoltar.BorderThickness = 1;
            this.btnVoltar.BorderColor = ColorTranslator.FromHtml("#D5D0C7");
            this.btnVoltar.ForeColor = StyleManager.TextSecondary;
            this.btnVoltar.Font = StyleManager.FontRegular;
            this.btnVoltar.HoverState.FillColor = ColorTranslator.FromHtml("#F9F9F9");
            this.detailsCard.Controls.Add(this.btnVoltar);
            
            this.btnAtualizar = new SiticoneButton();
            this.btnAtualizar.Size = new Size(120, 45);
            this.btnAtualizar.Location = new Point(160, yPos);
            this.btnAtualizar.Text = "Atualizar";
            this.btnAtualizar.BorderRadius = 8;
            this.btnAtualizar.FillColor = StyleManager.Accent;
            this.btnAtualizar.ForeColor = Color.White;
            this.btnAtualizar.Font = StyleManager.FontRegular;
            this.btnAtualizar.HoverState.FillColor = StyleManager.AccentDark;
            this.detailsCard.Controls.Add(this.btnAtualizar);
        }

        private SiticoneButton CreateMenuButton(string text, int y)
        {
            var btn = new SiticoneButton();
            btn.Size = new Size(200, 50);
            btn.Location = new Point(20, y);
            btn.Text = text;
            btn.TextAlign = HorizontalAlignment.Left;
            btn.Font = StyleManager.FontRegular;
            btn.ForeColor = StyleManager.TextSecondary;
            btn.FillColor = Color.Transparent;
            btn.BorderRadius = 8;
            btn.BorderThickness = 0;
            btn.HoverState.FillColor = ColorTranslator.FromHtml("#E8E3DA");
            btn.TextOffset = new Point(10, 0);
            return btn;
        }
        #endregion
    }
}

namespace ClickDesk.Forms.Chamados
{
    #region Windows Form Designer generated code
    
    partial class FormDetalhesChamadoTecnico
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

        private void InitializeComponent()
        {
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.panelTopbar = new System.Windows.Forms.Panel();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.lblTituloTela = new System.Windows.Forms.Label();
            this.panelBarraLaranja = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelDetailCard = new System.Windows.Forms.Panel();
            this.panelAcoes = new System.Windows.Forms.Panel();
            this.btnFecharChamado = new System.Windows.Forms.Button();
            this.panelAnexosContainer = new System.Windows.Forms.Panel();
            this.lblAnexosLabel = new System.Windows.Forms.Label();
            this.panelAnexos = new System.Windows.Forms.FlowLayoutPanel();
            this.panelDescricao = new System.Windows.Forms.Panel();
            this.lblDescricaoLabel = new System.Windows.Forms.Label();
            this.lblDescricaoChamado = new System.Windows.Forms.Label();
            this.panelTitulo = new System.Windows.Forms.Panel();
            this.lblTituloLabel = new System.Windows.Forms.Label();
            this.lblTituloChamado = new System.Windows.Forms.Label();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.lblPrioridadeValor = new System.Windows.Forms.Label();
            this.lblPrioridadeLabel = new System.Windows.Forms.Label();
            this.lblCategoriaValor = new System.Windows.Forms.Label();
            this.lblCategoriaLabel = new System.Windows.Forms.Label();
            this.lblDepartamentoValor = new System.Windows.Forms.Label();
            this.lblDepartamentoLabel = new System.Windows.Forms.Label();
            this.lblSolicitanteValor = new System.Windows.Forms.Label();
            this.lblSolicitanteLabel = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTicketStatus = new System.Windows.Forms.Label();
            this.lblTicketDate = new System.Windows.Forms.Label();
            this.lblTicketId = new System.Windows.Forms.Label();
            this.panelSidebar.SuspendLayout();
            this.panelTopbar.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelDetailCard.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.panelTitulo.SuspendLayout();
            this.panelDescricao.SuspendLayout();
            this.panelAnexosContainer.SuspendLayout();
            this.panelAcoes.SuspendLayout();
            this.SuspendLayout();
            
            // panelSidebar
            this.panelSidebar.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2EEE7");
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(240, 800);
            this.panelSidebar.TabIndex = 0;
            this.panelSidebar.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelSidebar_Paint);
            
            // panelTopbar
            this.panelTopbar.BackColor = System.Drawing.Color.White;
            this.panelTopbar.Controls.Add(this.btnAtualizar);
            this.panelTopbar.Controls.Add(this.btnVoltar);
            this.panelTopbar.Controls.Add(this.lblTituloTela);
            this.panelTopbar.Controls.Add(this.panelBarraLaranja);
            this.panelTopbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopbar.Location = new System.Drawing.Point(240, 0);
            this.panelTopbar.Name = "panelTopbar";
            this.panelTopbar.Size = new System.Drawing.Size(960, 60);
            this.panelTopbar.TabIndex = 1;
            
            // btnVoltar
            this.btnVoltar.BackColor = System.Drawing.ColorTranslator.FromHtml("#E5E5E5");
            this.btnVoltar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVoltar.FlatAppearance.BorderSize = 0;
            this.btnVoltar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoltar.Font = new System.Drawing.Font("Poppins", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1E2A22");
            this.btnVoltar.Location = new System.Drawing.Point(720, 15);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(100, 30);
            this.btnVoltar.TabIndex = 0;
            this.btnVoltar.Text = "← Voltar";
            this.btnVoltar.UseVisualStyleBackColor = false;
            
            // btnAtualizar
            this.btnAtualizar.BackColor = System.Drawing.ColorTranslator.FromHtml("#F28A1A");
            this.btnAtualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAtualizar.FlatAppearance.BorderSize = 0;
            this.btnAtualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtualizar.Font = new System.Drawing.Font("Poppins", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtualizar.ForeColor = System.Drawing.Color.White;
            this.btnAtualizar.Location = new System.Drawing.Point(830, 15);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(100, 30);
            this.btnAtualizar.TabIndex = 1;
            this.btnAtualizar.Text = "🔄 Atualizar";
            this.btnAtualizar.UseVisualStyleBackColor = false;
            
            // lblTituloTela
            this.lblTituloTela.AutoSize = true;
            this.lblTituloTela.Font = new System.Drawing.Font("Poppins", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloTela.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1E2A22");
            this.lblTituloTela.Location = new System.Drawing.Point(25, 18);
            this.lblTituloTela.Name = "lblTituloTela";
            this.lblTituloTela.Size = new System.Drawing.Size(178, 34);
            this.lblTituloTela.TabIndex = 2;
            this.lblTituloTela.Text = "Detalhes Chamado";
            
            // panelBarraLaranja
            this.panelBarraLaranja.BackColor = System.Drawing.ColorTranslator.FromHtml("#F28A1A");
            this.panelBarraLaranja.Location = new System.Drawing.Point(10, 18);
            this.panelBarraLaranja.Name = "panelBarraLaranja";
            this.panelBarraLaranja.Size = new System.Drawing.Size(4, 24);
            this.panelBarraLaranja.TabIndex = 3;
            
            // panelContent
            this.panelContent.BackColor = System.Drawing.ColorTranslator.FromHtml("#EDE6D9");
            this.panelContent.Controls.Add(this.panelDetailCard);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(240, 60);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(30);
            this.panelContent.Size = new System.Drawing.Size(960, 740);
            this.panelContent.TabIndex = 2;
            
            // panelDetailCard
            this.panelDetailCard.BackColor = System.Drawing.ColorTranslator.FromHtml("#FAF5EE");
            this.panelDetailCard.Controls.Add(this.panelAcoes);
            this.panelDetailCard.Controls.Add(this.panelAnexosContainer);
            this.panelDetailCard.Controls.Add(this.panelDescricao);
            this.panelDetailCard.Controls.Add(this.panelTitulo);
            this.panelDetailCard.Controls.Add(this.panelInfo);
            this.panelDetailCard.Controls.Add(this.panelHeader);
            this.panelDetailCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetailCard.Location = new System.Drawing.Point(30, 30);
            this.panelDetailCard.Name = "panelDetailCard";
            this.panelDetailCard.Padding = new System.Windows.Forms.Padding(30);
            this.panelDetailCard.Size = new System.Drawing.Size(900, 680);
            this.panelDetailCard.TabIndex = 0;
            this.panelDetailCard.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelDetailCard_Paint);
            
            // panelHeader
            this.panelHeader.Controls.Add(this.lblTicketStatus);
            this.panelHeader.Controls.Add(this.lblTicketDate);
            this.panelHeader.Controls.Add(this.lblTicketId);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(30, 30);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(840, 50);
            this.panelHeader.TabIndex = 0;
            
            // lblTicketId
            this.lblTicketId.AutoSize = true;
            this.lblTicketId.BackColor = System.Drawing.ColorTranslator.FromHtml("#F28A1A");
            this.lblTicketId.Font = new System.Drawing.Font("Poppins", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTicketId.ForeColor = System.Drawing.Color.White;
            this.lblTicketId.Location = new System.Drawing.Point(0, 10);
            this.lblTicketId.Name = "lblTicketId";
            this.lblTicketId.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.lblTicketId.Size = new System.Drawing.Size(87, 36);
            this.lblTicketId.TabIndex = 0;
            this.lblTicketId.Text = "CD-1001";
            
            // lblTicketDate
            this.lblTicketDate.AutoSize = true;
            this.lblTicketDate.BackColor = System.Drawing.ColorTranslator.FromHtml("#E5E5E5");
            this.lblTicketDate.Font = new System.Drawing.Font("Poppins", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTicketDate.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1E2A22");
            this.lblTicketDate.Location = new System.Drawing.Point(350, 10);
            this.lblTicketDate.Name = "lblTicketDate";
            this.lblTicketDate.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.lblTicketDate.Size = new System.Drawing.Size(106, 38);
            this.lblTicketDate.TabIndex = 1;
            this.lblTicketDate.Text = "📅 15/01/2024";
            
            // lblTicketStatus
            this.lblTicketStatus.AutoSize = true;
            this.lblTicketStatus.BackColor = System.Drawing.ColorTranslator.FromHtml("#E8F5E9");
            this.lblTicketStatus.Font = new System.Drawing.Font("Poppins", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTicketStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2E7D32");
            this.lblTicketStatus.Location = new System.Drawing.Point(720, 10);
            this.lblTicketStatus.Name = "lblTicketStatus";
            this.lblTicketStatus.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.lblTicketStatus.Size = new System.Drawing.Size(73, 38);
            this.lblTicketStatus.TabIndex = 2;
            this.lblTicketStatus.Text = "ABERTO";
            
            // panelInfo
            this.panelInfo.Controls.Add(this.lblPrioridadeValor);
            this.panelInfo.Controls.Add(this.lblPrioridadeLabel);
            this.panelInfo.Controls.Add(this.lblCategoriaValor);
            this.panelInfo.Controls.Add(this.lblCategoriaLabel);
            this.panelInfo.Controls.Add(this.lblDepartamentoValor);
            this.panelInfo.Controls.Add(this.lblDepartamentoLabel);
            this.panelInfo.Controls.Add(this.lblSolicitanteValor);
            this.panelInfo.Controls.Add(this.lblSolicitanteLabel);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInfo.Location = new System.Drawing.Point(30, 80);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Padding = new System.Windows.Forms.Padding(0, 20, 0, 20);
            this.panelInfo.Size = new System.Drawing.Size(840, 80);
            this.panelInfo.TabIndex = 1;
            
            // lblSolicitanteLabel
            this.lblSolicitanteLabel.AutoSize = true;
            this.lblSolicitanteLabel.Font = new System.Drawing.Font("Poppins", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSolicitanteLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6F6F6F");
            this.lblSolicitanteLabel.Location = new System.Drawing.Point(0, 20);
            this.lblSolicitanteLabel.Name = "lblSolicitanteLabel";
            this.lblSolicitanteLabel.Size = new System.Drawing.Size(77, 19);
            this.lblSolicitanteLabel.TabIndex = 0;
            this.lblSolicitanteLabel.Text = "SOLICITANTE";
            
            // lblSolicitanteValor
            this.lblSolicitanteValor.AutoSize = true;
            this.lblSolicitanteValor.Font = new System.Drawing.Font("Poppins", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSolicitanteValor.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1E2A22");
            this.lblSolicitanteValor.Location = new System.Drawing.Point(0, 40);
            this.lblSolicitanteValor.Name = "lblSolicitanteValor";
            this.lblSolicitanteValor.Size = new System.Drawing.Size(92, 25);
            this.lblSolicitanteValor.TabIndex = 1;
            this.lblSolicitanteValor.Text = "João Silva";
            
            // lblDepartamentoLabel
            this.lblDepartamentoLabel.AutoSize = true;
            this.lblDepartamentoLabel.Font = new System.Drawing.Font("Poppins", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartamentoLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6F6F6F");
            this.lblDepartamentoLabel.Location = new System.Drawing.Point(210, 20);
            this.lblDepartamentoLabel.Name = "lblDepartamentoLabel";
            this.lblDepartamentoLabel.Size = new System.Drawing.Size(96, 19);
            this.lblDepartamentoLabel.TabIndex = 2;
            this.lblDepartamentoLabel.Text = "DEPARTAMENTO";
            
            // lblDepartamentoValor
            this.lblDepartamentoValor.AutoSize = true;
            this.lblDepartamentoValor.Font = new System.Drawing.Font("Poppins", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartamentoValor.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1E2A22");
            this.lblDepartamentoValor.Location = new System.Drawing.Point(210, 40);
            this.lblDepartamentoValor.Name = "lblDepartamentoValor";
            this.lblDepartamentoValor.Size = new System.Drawing.Size(70, 25);
            this.lblDepartamentoValor.TabIndex = 3;
            this.lblDepartamentoValor.Text = "Suporte";
            
            // lblCategoriaLabel
            this.lblCategoriaLabel.AutoSize = true;
            this.lblCategoriaLabel.Font = new System.Drawing.Font("Poppins", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoriaLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6F6F6F");
            this.lblCategoriaLabel.Location = new System.Drawing.Point(420, 20);
            this.lblCategoriaLabel.Name = "lblCategoriaLabel";
            this.lblCategoriaLabel.Size = new System.Drawing.Size(72, 19);
            this.lblCategoriaLabel.TabIndex = 4;
            this.lblCategoriaLabel.Text = "CATEGORIA";
            
            // lblCategoriaValor
            this.lblCategoriaValor.AutoSize = true;
            this.lblCategoriaValor.Font = new System.Drawing.Font("Poppins", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoriaValor.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1E2A22");
            this.lblCategoriaValor.Location = new System.Drawing.Point(420, 40);
            this.lblCategoriaValor.Name = "lblCategoriaValor";
            this.lblCategoriaValor.Size = new System.Drawing.Size(81, 25);
            this.lblCategoriaValor.TabIndex = 5;
            this.lblCategoriaValor.Text = "Hardware";
            
            // lblPrioridadeLabel
            this.lblPrioridadeLabel.AutoSize = true;
            this.lblPrioridadeLabel.Font = new System.Drawing.Font("Poppins", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrioridadeLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6F6F6F");
            this.lblPrioridadeLabel.Location = new System.Drawing.Point(630, 20);
            this.lblPrioridadeLabel.Name = "lblPrioridadeLabel";
            this.lblPrioridadeLabel.Size = new System.Drawing.Size(77, 19);
            this.lblPrioridadeLabel.TabIndex = 6;
            this.lblPrioridadeLabel.Text = "PRIORIDADE";
            
            // lblPrioridadeValor
            this.lblPrioridadeValor.AutoSize = true;
            this.lblPrioridadeValor.Font = new System.Drawing.Font("Poppins", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrioridadeValor.ForeColor = System.Drawing.ColorTranslator.FromHtml("#F59E0B");
            this.lblPrioridadeValor.Location = new System.Drawing.Point(630, 40);
            this.lblPrioridadeValor.Name = "lblPrioridadeValor";
            this.lblPrioridadeValor.Size = new System.Drawing.Size(46, 25);
            this.lblPrioridadeValor.TabIndex = 7;
            this.lblPrioridadeValor.Text = "Alta";
            
            // panelTitulo
            this.panelTitulo.Controls.Add(this.lblTituloChamado);
            this.panelTitulo.Controls.Add(this.lblTituloLabel);
            this.panelTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitulo.Location = new System.Drawing.Point(30, 160);
            this.panelTitulo.Name = "panelTitulo";
            this.panelTitulo.Padding = new System.Windows.Forms.Padding(0, 20, 0, 20);
            this.panelTitulo.Size = new System.Drawing.Size(840, 80);
            this.panelTitulo.TabIndex = 2;
            
            // lblTituloLabel
            this.lblTituloLabel.AutoSize = true;
            this.lblTituloLabel.Font = new System.Drawing.Font("Poppins", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6F6F6F");
            this.lblTituloLabel.Location = new System.Drawing.Point(0, 20);
            this.lblTituloLabel.Name = "lblTituloLabel";
            this.lblTituloLabel.Size = new System.Drawing.Size(46, 19);
            this.lblTituloLabel.TabIndex = 0;
            this.lblTituloLabel.Text = "TÍTULO";
            
            // lblTituloChamado
            this.lblTituloChamado.AutoSize = true;
            this.lblTituloChamado.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloChamado.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1E2A22");
            this.lblTituloChamado.Location = new System.Drawing.Point(0, 40);
            this.lblTituloChamado.Name = "lblTituloChamado";
            this.lblTituloChamado.Size = new System.Drawing.Size(315, 28);
            this.lblTituloChamado.TabIndex = 1;
            this.lblTituloChamado.Text = "Problema com impressora HP LaserJet";
            
            // panelDescricao
            this.panelDescricao.Controls.Add(this.lblDescricaoChamado);
            this.panelDescricao.Controls.Add(this.lblDescricaoLabel);
            this.panelDescricao.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDescricao.Location = new System.Drawing.Point(30, 240);
            this.panelDescricao.Name = "panelDescricao";
            this.panelDescricao.Padding = new System.Windows.Forms.Padding(0, 20, 0, 20);
            this.panelDescricao.Size = new System.Drawing.Size(840, 120);
            this.panelDescricao.TabIndex = 3;
            
            // lblDescricaoLabel
            this.lblDescricaoLabel.AutoSize = true;
            this.lblDescricaoLabel.Font = new System.Drawing.Font("Poppins", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescricaoLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6F6F6F");
            this.lblDescricaoLabel.Location = new System.Drawing.Point(0, 20);
            this.lblDescricaoLabel.Name = "lblDescricaoLabel";
            this.lblDescricaoLabel.Size = new System.Drawing.Size(70, 19);
            this.lblDescricaoLabel.TabIndex = 0;
            this.lblDescricaoLabel.Text = "DESCRIÇÃO";
            
            // lblDescricaoChamado
            this.lblDescricaoChamado.BackColor = System.Drawing.ColorTranslator.FromHtml("#FBF6ED");
            this.lblDescricaoChamado.Font = new System.Drawing.Font("Poppins", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescricaoChamado.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1E2A22");
            this.lblDescricaoChamado.Location = new System.Drawing.Point(0, 45);
            this.lblDescricaoChamado.Name = "lblDescricaoChamado";
            this.lblDescricaoChamado.Padding = new System.Windows.Forms.Padding(15);
            this.lblDescricaoChamado.Size = new System.Drawing.Size(800, 55);
            this.lblDescricaoChamado.TabIndex = 1;
            this.lblDescricaoChamado.Text = "A impressora está apresentando erro de papel atolado constantemente, mesmo com a bandeja vazia. O problema começou após a última manutenção.";
            
            // panelAnexosContainer
            this.panelAnexosContainer.Controls.Add(this.panelAnexos);
            this.panelAnexosContainer.Controls.Add(this.lblAnexosLabel);
            this.panelAnexosContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAnexosContainer.Location = new System.Drawing.Point(30, 360);
            this.panelAnexosContainer.Name = "panelAnexosContainer";
            this.panelAnexosContainer.Padding = new System.Windows.Forms.Padding(0, 20, 0, 20);
            this.panelAnexosContainer.Size = new System.Drawing.Size(840, 150);
            this.panelAnexosContainer.TabIndex = 4;
            
            // lblAnexosLabel
            this.lblAnexosLabel.AutoSize = true;
            this.lblAnexosLabel.Font = new System.Drawing.Font("Poppins", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnexosLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6F6F6F");
            this.lblAnexosLabel.Location = new System.Drawing.Point(0, 20);
            this.lblAnexosLabel.Name = "lblAnexosLabel";
            this.lblAnexosLabel.Size = new System.Drawing.Size(55, 19);
            this.lblAnexosLabel.TabIndex = 0;
            this.lblAnexosLabel.Text = "ANEXOS";
            
            // panelAnexos
            this.panelAnexos.AutoScroll = true;
            this.panelAnexos.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.panelAnexos.Location = new System.Drawing.Point(0, 45);
            this.panelAnexos.Name = "panelAnexos";
            this.panelAnexos.Size = new System.Drawing.Size(800, 85);
            this.panelAnexos.TabIndex = 1;
            this.panelAnexos.WrapContents = true;
            
            // panelAcoes
            this.panelAcoes.Controls.Add(this.btnFecharChamado);
            this.panelAcoes.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAcoes.Location = new System.Drawing.Point(30, 510);
            this.panelAcoes.Name = "panelAcoes";
            this.panelAcoes.Padding = new System.Windows.Forms.Padding(0, 30, 0, 30);
            this.panelAcoes.Size = new System.Drawing.Size(840, 100);
            this.panelAcoes.TabIndex = 5;
            
            // btnFecharChamado
            this.btnFecharChamado.BackColor = System.Drawing.ColorTranslator.FromHtml("#EF4444");
            this.btnFecharChamado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFecharChamado.FlatAppearance.BorderSize = 0;
            this.btnFecharChamado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFecharChamado.Font = new System.Drawing.Font("Poppins", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFecharChamado.ForeColor = System.Drawing.Color.White;
            this.btnFecharChamado.Location = new System.Drawing.Point(0, 30);
            this.btnFecharChamado.Name = "btnFecharChamado";
            this.btnFecharChamado.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.btnFecharChamado.Size = new System.Drawing.Size(200, 40);
            this.btnFecharChamado.TabIndex = 0;
            this.btnFecharChamado.Text = "✕ Fechar Chamado";
            this.btnFecharChamado.UseVisualStyleBackColor = false;
            
            // FormDetalhesChamadoTecnico
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#EDE6D9");
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelTopbar);
            this.Controls.Add(this.panelSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDetalhesChamadoTecnico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ClickDesk - Detalhes do Chamado";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelTopbar.ResumeLayout(false);
            this.panelTopbar.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.panelDetailCard.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.panelTitulo.ResumeLayout(false);
            this.panelTitulo.PerformLayout();
            this.panelDescricao.ResumeLayout(false);
            this.panelDescricao.PerformLayout();
            this.panelAnexosContainer.ResumeLayout(false);
            this.panelAnexosContainer.PerformLayout();
            this.panelAcoes.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Panel panelTopbar;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.Label lblTituloTela;
        private System.Windows.Forms.Panel panelBarraLaranja;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelDetailCard;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTicketId;
        private System.Windows.Forms.Label lblTicketDate;
        private System.Windows.Forms.Label lblTicketStatus;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label lblSolicitanteLabel;
        private System.Windows.Forms.Label lblSolicitanteValor;
        private System.Windows.Forms.Label lblDepartamentoLabel;
        private System.Windows.Forms.Label lblDepartamentoValor;
        private System.Windows.Forms.Label lblCategoriaLabel;
        private System.Windows.Forms.Label lblCategoriaValor;
        private System.Windows.Forms.Label lblPrioridadeLabel;
        private System.Windows.Forms.Label lblPrioridadeValor;
        private System.Windows.Forms.Panel panelTitulo;
        private System.Windows.Forms.Label lblTituloLabel;
        private System.Windows.Forms.Label lblTituloChamado;
        private System.Windows.Forms.Panel panelDescricao;
        private System.Windows.Forms.Label lblDescricaoLabel;
        private System.Windows.Forms.Label lblDescricaoChamado;
        private System.Windows.Forms.Panel panelAnexosContainer;
        private System.Windows.Forms.Label lblAnexosLabel;
        private System.Windows.Forms.FlowLayoutPanel panelAnexos;
        private System.Windows.Forms.Panel panelAcoes;
        private System.Windows.Forms.Button btnFecharChamado;
    }
}

namespace ClickDesk.Components
{
    partial class ChamadoCard
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Controles do UserControl
        /// </summary>
        private System.Windows.Forms.Panel painelContainer;
        private System.Windows.Forms.Panel painelTarja;
        private System.Windows.Forms.Label lblIdChip;
        private System.Windows.Forms.Label lblStatusChip;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDescricao;
        private System.Windows.Forms.Panel painelMeta;
        private System.Windows.Forms.Label lblCategoriaLabel;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.Label lblDataLabel;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Label lblPrioridadeLabel;
        private System.Windows.Forms.Label lblPrioridade;
        private System.Windows.Forms.Label lblResponsavelLabel;
        private System.Windows.Forms.Label lblResponsavel;
        private System.Windows.Forms.Panel painelAcoes;
        private System.Windows.Forms.Button btnAprovar;
        private System.Windows.Forms.Button btnRecusar;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.painelContainer = new System.Windows.Forms.Panel();
            this.painelTarja = new System.Windows.Forms.Panel();
            this.lblIdChip = new System.Windows.Forms.Label();
            this.lblStatusChip = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.painelMeta = new System.Windows.Forms.Panel();
            this.lblCategoriaLabel = new System.Windows.Forms.Label();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.lblDataLabel = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.Label();
            this.lblPrioridadeLabel = new System.Windows.Forms.Label();
            this.lblPrioridade = new System.Windows.Forms.Label();
            this.lblResponsavelLabel = new System.Windows.Forms.Label();
            this.lblResponsavel = new System.Windows.Forms.Label();
            this.painelAcoes = new System.Windows.Forms.Panel();
            this.btnAprovar = new System.Windows.Forms.Button();
            this.btnRecusar = new System.Windows.Forms.Button();
            this.painelContainer.SuspendLayout();
            this.painelMeta.SuspendLayout();
            this.painelAcoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // painelContainer
            // 
            this.painelContainer.BackColor = System.Drawing.Color.Transparent;
            this.painelContainer.Controls.Add(this.painelTarja);
            this.painelContainer.Controls.Add(this.lblIdChip);
            this.painelContainer.Controls.Add(this.lblStatusChip);
            this.painelContainer.Controls.Add(this.lblTitulo);
            this.painelContainer.Controls.Add(this.lblDescricao);
            this.painelContainer.Controls.Add(this.painelMeta);
            this.painelContainer.Controls.Add(this.painelAcoes);
            this.painelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.painelContainer.Location = new System.Drawing.Point(0, 0);
            this.painelContainer.Name = "painelContainer";
            this.painelContainer.Padding = new System.Windows.Forms.Padding(12);
            this.painelContainer.Size = new System.Drawing.Size(350, 200);
            this.painelContainer.TabIndex = 0;
            // 
            // painelTarja
            // 
            this.painelTarja.BackColor = System.Drawing.ColorTranslator.FromHtml("#F28A1A");
            this.painelTarja.Dock = System.Windows.Forms.DockStyle.Top;
            this.painelTarja.Location = new System.Drawing.Point(12, 12);
            this.painelTarja.Name = "painelTarja";
            this.painelTarja.Size = new System.Drawing.Size(326, 4);
            this.painelTarja.TabIndex = 0;
            // 
            // lblIdChip
            // 
            this.lblIdChip.AutoSize = true;
            this.lblIdChip.BackColor = System.Drawing.ColorTranslator.FromHtml("#FBF6ED");
            this.lblIdChip.Font = new System.Drawing.Font("Poppins", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdChip.ForeColor = System.Drawing.ColorTranslator.FromHtml("#F28A1A");
            this.lblIdChip.Location = new System.Drawing.Point(12, 24);
            this.lblIdChip.Name = "lblIdChip";
            this.lblIdChip.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.lblIdChip.Size = new System.Drawing.Size(64, 27);
            this.lblIdChip.TabIndex = 1;
            this.lblIdChip.Text = "CD-1124";
            // 
            // lblStatusChip
            // 
            this.lblStatusChip.AutoSize = true;
            this.lblStatusChip.BackColor = System.Drawing.ColorTranslator.FromHtml("#FEF3C7");
            this.lblStatusChip.Font = new System.Drawing.Font("Poppins", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusChip.ForeColor = System.Drawing.ColorTranslator.FromHtml("#92400E");
            this.lblStatusChip.Location = new System.Drawing.Point(260, 24);
            this.lblStatusChip.Name = "lblStatusChip";
            this.lblStatusChip.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.lblStatusChip.Size = new System.Drawing.Size(73, 25);
            this.lblStatusChip.TabIndex = 2;
            this.lblStatusChip.Text = "PENDENTE";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Poppins", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1E2A22");
            this.lblTitulo.Location = new System.Drawing.Point(12, 60);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(320, 25);
            this.lblTitulo.TabIndex = 3;
            this.lblTitulo.Text = "Problema com impressora HP LaserJet";
            // 
            // lblDescricao
            // 
            this.lblDescricao.Font = new System.Drawing.Font("Poppins", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescricao.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6F6F6F");
            this.lblDescricao.Location = new System.Drawing.Point(12, 85);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(320, 35);
            this.lblDescricao.TabIndex = 4;
            this.lblDescricao.Text = "A impressora está apresentando erro de papel atolado constantemente, mesmo com a bandeja vazia...";
            // 
            // painelMeta
            // 
            this.painelMeta.Controls.Add(this.lblCategoriaLabel);
            this.painelMeta.Controls.Add(this.lblCategoria);
            this.painelMeta.Controls.Add(this.lblDataLabel);
            this.painelMeta.Controls.Add(this.lblData);
            this.painelMeta.Controls.Add(this.lblPrioridadeLabel);
            this.painelMeta.Controls.Add(this.lblPrioridade);
            this.painelMeta.Controls.Add(this.lblResponsavelLabel);
            this.painelMeta.Controls.Add(this.lblResponsavel);
            this.painelMeta.Location = new System.Drawing.Point(12, 125);
            this.painelMeta.Name = "painelMeta";
            this.painelMeta.Size = new System.Drawing.Size(320, 40);
            this.painelMeta.TabIndex = 5;
            // 
            // lblCategoriaLabel
            // 
            this.lblCategoriaLabel.AutoSize = true;
            this.lblCategoriaLabel.Font = new System.Drawing.Font("Poppins", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoriaLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#9CA3AF");
            this.lblCategoriaLabel.Location = new System.Drawing.Point(0, 0);
            this.lblCategoriaLabel.Name = "lblCategoriaLabel";
            this.lblCategoriaLabel.Size = new System.Drawing.Size(57, 17);
            this.lblCategoriaLabel.TabIndex = 0;
            this.lblCategoriaLabel.Text = "CATEGORIA";
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Font = new System.Drawing.Font("Poppins", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoria.ForeColor = System.Drawing.ColorTranslator.FromHtml("#374151");
            this.lblCategoria.Location = new System.Drawing.Point(0, 17);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(57, 19);
            this.lblCategoria.TabIndex = 1;
            this.lblCategoria.Text = "Hardware";
            // 
            // lblDataLabel
            // 
            this.lblDataLabel.AutoSize = true;
            this.lblDataLabel.Font = new System.Drawing.Font("Poppins", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#9CA3AF");
            this.lblDataLabel.Location = new System.Drawing.Point(80, 0);
            this.lblDataLabel.Name = "lblDataLabel";
            this.lblDataLabel.Size = new System.Drawing.Size(30, 17);
            this.lblDataLabel.TabIndex = 2;
            this.lblDataLabel.Text = "DATA";
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Font = new System.Drawing.Font("Poppins", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblData.ForeColor = System.Drawing.ColorTranslator.FromHtml("#374151");
            this.lblData.Location = new System.Drawing.Point(80, 17);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(66, 19);
            this.lblData.TabIndex = 3;
            this.lblData.Text = "15/01/2024";
            // 
            // lblPrioridadeLabel
            // 
            this.lblPrioridadeLabel.AutoSize = true;
            this.lblPrioridadeLabel.Font = new System.Drawing.Font("Poppins", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrioridadeLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#9CA3AF");
            this.lblPrioridadeLabel.Location = new System.Drawing.Point(160, 0);
            this.lblPrioridadeLabel.Name = "lblPrioridadeLabel";
            this.lblPrioridadeLabel.Size = new System.Drawing.Size(62, 17);
            this.lblPrioridadeLabel.TabIndex = 4;
            this.lblPrioridadeLabel.Text = "PRIORIDADE";
            // 
            // lblPrioridade
            // 
            this.lblPrioridade.AutoSize = true;
            this.lblPrioridade.Font = new System.Drawing.Font("Poppins", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrioridade.ForeColor = System.Drawing.ColorTranslator.FromHtml("#10B981");
            this.lblPrioridade.Location = new System.Drawing.Point(160, 17);
            this.lblPrioridade.Name = "lblPrioridade";
            this.lblPrioridade.Size = new System.Drawing.Size(47, 19);
            this.lblPrioridade.TabIndex = 5;
            this.lblPrioridade.Text = "Normal";
            // 
            // lblResponsavelLabel
            // 
            this.lblResponsavelLabel.AutoSize = true;
            this.lblResponsavelLabel.Font = new System.Drawing.Font("Poppins", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResponsavelLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#9CA3AF");
            this.lblResponsavelLabel.Location = new System.Drawing.Point(230, 0);
            this.lblResponsavelLabel.Name = "lblResponsavelLabel";
            this.lblResponsavelLabel.Size = new System.Drawing.Size(74, 17);
            this.lblResponsavelLabel.TabIndex = 6;
            this.lblResponsavelLabel.Text = "RESPONSÁVEL";
            // 
            // lblResponsavel
            // 
            this.lblResponsavel.AutoSize = true;
            this.lblResponsavel.Font = new System.Drawing.Font("Poppins", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResponsavel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#374151");
            this.lblResponsavel.Location = new System.Drawing.Point(230, 17);
            this.lblResponsavel.Name = "lblResponsavel";
            this.lblResponsavel.Size = new System.Drawing.Size(90, 19);
            this.lblResponsavel.TabIndex = 7;
            this.lblResponsavel.Text = "Não atribuído";
            // 
            // painelAcoes
            // 
            this.painelAcoes.Controls.Add(this.btnAprovar);
            this.painelAcoes.Controls.Add(this.btnRecusar);
            this.painelAcoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.painelAcoes.Location = new System.Drawing.Point(12, 168);
            this.painelAcoes.Name = "painelAcoes";
            this.painelAcoes.Size = new System.Drawing.Size(326, 20);
            this.painelAcoes.TabIndex = 6;
            // 
            // btnAprovar
            // 
            this.btnAprovar.BackColor = System.Drawing.ColorTranslator.FromHtml("#10B981");
            this.btnAprovar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAprovar.FlatAppearance.BorderSize = 0;
            this.btnAprovar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAprovar.Font = new System.Drawing.Font("Poppins", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAprovar.ForeColor = System.Drawing.Color.White;
            this.btnAprovar.Location = new System.Drawing.Point(0, 0);
            this.btnAprovar.Name = "btnAprovar";
            this.btnAprovar.Size = new System.Drawing.Size(80, 28);
            this.btnAprovar.TabIndex = 0;
            this.btnAprovar.Text = "✓ Aprovar";
            this.btnAprovar.UseVisualStyleBackColor = false;
            // 
            // btnRecusar
            // 
            this.btnRecusar.BackColor = System.Drawing.ColorTranslator.FromHtml("#EF4444");
            this.btnRecusar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRecusar.FlatAppearance.BorderSize = 0;
            this.btnRecusar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecusar.Font = new System.Drawing.Font("Poppins", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecusar.ForeColor = System.Drawing.Color.White;
            this.btnRecusar.Location = new System.Drawing.Point(90, 0);
            this.btnRecusar.Name = "btnRecusar";
            this.btnRecusar.Size = new System.Drawing.Size(80, 28);
            this.btnRecusar.TabIndex = 1;
            this.btnRecusar.Text = "✗ Recusar";
            this.btnRecusar.UseVisualStyleBackColor = false;
            // 
            // ChamadoCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.painelContainer);
            this.Name = "ChamadoCard";
            this.Size = new System.Drawing.Size(350, 200);
            this.painelContainer.ResumeLayout(false);
            this.painelContainer.PerformLayout();
            this.painelMeta.ResumeLayout(false);
            this.painelMeta.PerformLayout();
            this.painelAcoes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
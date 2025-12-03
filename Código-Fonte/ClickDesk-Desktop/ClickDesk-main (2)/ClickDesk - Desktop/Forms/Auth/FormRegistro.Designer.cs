using System;
using System.Drawing;
using System.Windows.Forms;
using Siticone.Desktop.UI.WinForms;
using ClickDesk.Utils;

namespace ClickDesk.Forms.Auth
{
    partial class FormRegistro
    {
        private System.ComponentModel.IContainer components = null;

        // Painel principal
        private SiticonePanel mainCard;
        private TableLayoutPanel rootLayout;

        // Branding (lado esquerdo - 40%)
        private FlowLayoutPanel brandingPanel;
        private Label lblBrandTitle;
        private Label lblBrandSubtitle;
        private Label lblFeature1;
        private Label lblFeature2;
        private Label lblFeature3;

        // Formulário (lado direito - 60%)
        private FlowLayoutPanel formPanel;
        private Label lblTitle;
        private Label lblSubtitle;

        // Campos
        private Label lblNome;
        private SiticoneTextBox txtNome;
        private Label lblSobrenome;
        private SiticoneTextBox txtSobrenome;
        private Label lblEmail;
        private SiticoneTextBox txtEmail;
        private Label lblTelefone;
        private SiticoneTextBox txtTelefone;

        // Senha + Mostrar
        private Label lblSenha;
        private FlowLayoutPanel passwordPanel;
        private SiticoneTextBox txtSenha;
        private SiticoneButton btnToggleSenha;

        // Confirmar Senha
        private Label lblConfirmSenha;
        private SiticoneTextBox txtConfirmSenha;

        // Termos
        private SiticoneCheckBox chkTermos;

        // Botão Criar Conta
        private SiticoneButton btnCriarConta;

        // Divisor "ou"
        private FlowLayoutPanel dividerPanel;
        private SiticoneSeparator sepLeft;
        private Label lblOu;
        private SiticoneSeparator sepRight;

        // Botão Fazer Login
        private SiticoneButton btnFazerLogin;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            // FORMULÁRIO PRINCIPAL
            this.Text = "ClickDesk - Criar Conta";
            this.ClientSize = new Size(1366, 768);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = StyleManager.BgPage;
            this.AutoScaleMode = AutoScaleMode.None;

            // Centralizar mainCard ao carregar e ao redimensionar
            EventHandler centralizar = (s, e) =>
            {
                if (this.mainCard != null)
                {
                    this.mainCard.Left = (this.ClientSize.Width - this.mainCard.Width) / 2;
                    this.mainCard.Top = (this.ClientSize.Height - this.mainCard.Height) / 2;
                }
            };
            this.Load += centralizar;
            this.Resize += centralizar;

            // CARD PRINCIPAL (Responsivo)
            this.mainCard = new SiticonePanel
            {
                BorderRadius = 22,
                FillColor = StyleManager.Card,
                ShadowDecoration = { Enabled = false },
                Anchor = AnchorStyles.None,
                Width = 1180,
                Height = 620,
                Dock = DockStyle.None
            };
            this.Controls.Add(this.mainCard);

            // LAYOUT RAIZ (40% Branding + 60% Formulário)
            this.rootLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            this.rootLayout.ColumnStyles.Clear();
            this.rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            this.rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            this.rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.mainCard.Controls.Add(this.rootLayout);

            // ===== PAINEL ESQUERDO (BRANDING - 40%) =====
            this.brandingPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(40),
                Margin = new Padding(0)
            };
            this.rootLayout.Controls.Add(this.brandingPanel, 0, 0);

            // Título Branding
            this.lblBrandTitle = new Label
            {
                Text = "Clickdesk",
                Font = new Font("Poppins", 52, FontStyle.Bold),
                ForeColor = StyleManager.Accent,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 20)
            };
            this.brandingPanel.Controls.Add(this.lblBrandTitle);

            // Subtítulo Branding (múltiplas linhas)
            this.lblBrandSubtitle = new Label
            {
                Text = "Crie sua conta e comece\na gerenciar seus chamados",
                Font = new Font("Poppins", 14),
                ForeColor = StyleManager.TextSubtle,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 40)
            };
            this.brandingPanel.Controls.Add(this.lblBrandSubtitle);

            // Features (3 itens)
            this.lblFeature1 = new Label
            {
                Text = "✓ Gestão completa de chamados",
                Font = new Font("Poppins", 15),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 20)
            };
            this.brandingPanel.Controls.Add(this.lblFeature1);

            this.lblFeature2 = new Label
            {
                Text = "✓ Controle de prazos e prioridades",
                Font = new Font("Poppins", 15),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 20)
            };
            this.brandingPanel.Controls.Add(this.lblFeature2);

            this.lblFeature3 = new Label
            {
                Text = "✓ Colaboração em equipe",
                Font = new Font("Poppins", 15),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 0)
            };
            this.brandingPanel.Controls.Add(this.lblFeature3);

            // ===== PAINEL DIREITO (FORMULÁRIO - 60%) =====
            this.formPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(40),
                Margin = new Padding(0)
            };
            this.rootLayout.Controls.Add(this.formPanel, 1, 0);

            // Título do Formulário
            this.lblTitle = new Label
            {
                Text = "Criar nova conta",
                Font = new Font("Poppins", 28, FontStyle.Bold),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 12)
            };
            this.formPanel.Controls.Add(this.lblTitle);

            // Subtítulo do Formulário
            this.lblSubtitle = new Label
            {
                Text = "Preencha as informações abaixo para começar",
                Font = new Font("Poppins", 11),
                ForeColor = StyleManager.TextSubtle,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 25)
            };
            this.formPanel.Controls.Add(this.lblSubtitle);

            // NOME
            this.lblNome = new Label
            {
                Text = "Nome",
                Font = new Font("Poppins", 11, FontStyle.Bold),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 5)
            };
            this.formPanel.Controls.Add(this.lblNome);

            this.txtNome = new SiticoneTextBox
            {
                PlaceholderText = "Digite seu nome",
                ForeColor = StyleManager.TextStrong,
                BorderColor = StyleManager.InputBorder,
                FillColor = Color.White,
                BorderRadius = 12,
                Width = 420,
                Height = 45,
                Font = new Font("Poppins", 11),
                Margin = new Padding(0, 0, 0, 15)
            };
            this.formPanel.Controls.Add(this.txtNome);

            // SOBRENOME
            this.lblSobrenome = new Label
            {
                Text = "Sobrenome",
                Font = new Font("Poppins", 11, FontStyle.Bold),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 5)
            };
            this.formPanel.Controls.Add(this.lblSobrenome);

            this.txtSobrenome = new SiticoneTextBox
            {
                PlaceholderText = "Digite seu sobrenome",
                ForeColor = StyleManager.TextStrong,
                BorderColor = StyleManager.InputBorder,
                FillColor = Color.White,
                BorderRadius = 12,
                Width = 420,
                Height = 45,
                Font = new Font("Poppins", 11),
                Margin = new Padding(0, 0, 0, 15)
            };
            this.formPanel.Controls.Add(this.txtSobrenome);

            // EMAIL
            this.lblEmail = new Label
            {
                Text = "E-mail",
                Font = new Font("Poppins", 11, FontStyle.Bold),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 5)
            };
            this.formPanel.Controls.Add(this.lblEmail);

            this.txtEmail = new SiticoneTextBox
            {
                PlaceholderText = "seu@email.com",
                ForeColor = StyleManager.TextStrong,
                BorderColor = StyleManager.InputBorder,
                FillColor = Color.White,
                BorderRadius = 12,
                Width = 420,
                Height = 45,
                Font = new Font("Poppins", 11),
                Margin = new Padding(0, 0, 0, 15)
            };
            this.formPanel.Controls.Add(this.txtEmail);

            // TELEFONE
            this.lblTelefone = new Label
            {
                Text = "Telefone",
                Font = new Font("Poppins", 11, FontStyle.Bold),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 5)
            };
            this.formPanel.Controls.Add(this.lblTelefone);

            this.txtTelefone = new SiticoneTextBox
            {
                PlaceholderText = "(11) 99999-9999",
                ForeColor = StyleManager.TextStrong,
                BorderColor = StyleManager.InputBorder,
                FillColor = Color.White,
                BorderRadius = 12,
                Width = 420,
                Height = 45,
                Font = new Font("Poppins", 11),
                Margin = new Padding(0, 0, 0, 15)
            };
            this.formPanel.Controls.Add(this.txtTelefone);

            // SENHA
            this.lblSenha = new Label
            {
                Text = "Senha",
                Font = new Font("Poppins", 11, FontStyle.Bold),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 5)
            };
            this.formPanel.Controls.Add(this.lblSenha);

            // Painel da Senha com Toggle
            this.passwordPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false,
                Width = 420,
                Height = 45,
                Margin = new Padding(0, 0, 0, 15),
                Padding = new Padding(0)
            };
            this.formPanel.Controls.Add(this.passwordPanel);

            this.txtSenha = new SiticoneTextBox
            {
                PlaceholderText = "Digite sua senha",
                ForeColor = StyleManager.TextStrong,
                BorderColor = StyleManager.InputBorder,
                FillColor = Color.White,
                BorderRadius = 12,
                UseSystemPasswordChar = true,
                Width = 370,
                Height = 45,
                Font = new Font("Poppins", 11),
                Margin = new Padding(0, 0, 8, 0)
            };
            this.passwordPanel.Controls.Add(this.txtSenha);

            this.btnToggleSenha = new SiticoneButton
            {
                Text = "Mostrar",
                ForeColor = StyleManager.TextStrong,
                FillColor = Color.FromArgb(235, 235, 235),
                BorderRadius = 12,
                Width = 70,
                Height = 45,
                Font = new Font("Poppins", 9),
                Margin = new Padding(0)
            };
            this.passwordPanel.Controls.Add(this.btnToggleSenha);

            // CONFIRMAR SENHA
            this.lblConfirmSenha = new Label
            {
                Text = "Confirmar Senha",
                Font = new Font("Poppins", 11, FontStyle.Bold),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 5)
            };
            this.formPanel.Controls.Add(this.lblConfirmSenha);

            this.txtConfirmSenha = new SiticoneTextBox
            {
                PlaceholderText = "Confirme sua senha",
                ForeColor = StyleManager.TextStrong,
                BorderColor = StyleManager.InputBorder,
                FillColor = Color.White,
                BorderRadius = 12,
                UseSystemPasswordChar = true,
                Width = 420,
                Height = 45,
                Font = new Font("Poppins", 11),
                Margin = new Padding(0, 0, 0, 15)
            };
            this.formPanel.Controls.Add(this.txtConfirmSenha);

            // CHECKBOX TERMOS
            this.chkTermos = new SiticoneCheckBox
            {
                Text = "Aceito os Termos de Uso e Política de Privacidade",
                Font = new Font("Poppins", 10),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 20)
            };
            this.formPanel.Controls.Add(this.chkTermos);

            // BOTÃO CRIAR CONTA
            this.btnCriarConta = new SiticoneButton
            {
                Text = "CRIAR CONTA",
                Font = new Font("Poppins", 13, FontStyle.Bold),
                ForeColor = Color.White,
                FillColor = StyleManager.Accent,
                BorderRadius = 12,
                Width = 420,
                Height = 45,
                Margin = new Padding(0, 0, 0, 20)
            };
            this.formPanel.Controls.Add(this.btnCriarConta);

            // DIVISOR "OU"
            this.dividerPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false,
                Width = 420,
                Height = 30,
                Margin = new Padding(0, 0, 0, 20),
                Padding = new Padding(0)
            };
            this.formPanel.Controls.Add(this.dividerPanel);

            this.sepLeft = new SiticoneSeparator
            {
                Width = 140,
                Height = 1,
                Margin = new Padding(0, 15, 8, 0)
            };
            this.dividerPanel.Controls.Add(this.sepLeft);

            this.lblOu = new Label
            {
                Text = "ou",
                Font = new Font("Poppins", 10, FontStyle.Bold),
                ForeColor = StyleManager.TextSubtle,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 10, 8, 0)
            };
            this.dividerPanel.Controls.Add(this.lblOu);

            this.sepRight = new SiticoneSeparator
            {
                Width = 140,
                Height = 1,
                Margin = new Padding(0, 15, 0, 0)
            };
            this.dividerPanel.Controls.Add(this.sepRight);

            // BOTÃO FAZER LOGIN
            this.btnFazerLogin = new SiticoneButton
            {
                Text = "JÁ TENHO CONTA — FAZER LOGIN",
                Font = new Font("Poppins", 10, FontStyle.Bold),
                ForeColor = StyleManager.Accent,
                FillColor = Color.Transparent,
                BorderRadius = 12,
                BorderThickness = 2,
                BorderColor = StyleManager.Accent,
                Width = 420,
                Height = 46,
                Margin = new Padding(0, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            this.btnFazerLogin.Click += BtnFazerLogin_Click;
            this.formPanel.Controls.Add(this.btnFazerLogin);

            // Z-Order
            this.mainCard.BringToFront();
            this.rootLayout.BringToFront();
            this.formPanel.BringToFront();

            // Event Handlers
            this.btnCriarConta.Click += BtnCriarConta_Click;
            this.btnToggleSenha.Click += BtnToggleSenha_Click;
            this.btnFazerLogin.Click += BtnFazerLogin_Click;
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using Siticone.Desktop.UI.WinForms;
using ClickDesk.Utils;

namespace ClickDesk.Forms.Auth
{
    partial class FormLogin
    {
        private System.ComponentModel.IContainer components = null;

        // Container principal
        private SiticonePanel mainCard;
        private TableLayoutPanel rootLayout;

        // Branding (lado esquerdo)
        private FlowLayoutPanel brandingPanel;
        private Label lblBrandTitle;
        private Label lblBrandSubtitle;
        private FlowLayoutPanel brandingFeatures;
        private Label lblFeature1;
        private Label lblFeature2;
        private Label lblFeature3;

        // Formulário (lado direito)
        private FlowLayoutPanel formPanel;
        private FlowLayoutPanel titlePanel;
        private Label lblLoginOrange;
        private Label lblLoginBlack;
        private Label lblSubtitle;

        // Campos de login
        private Label lblUser;
        private SiticoneTextBox txtUser;
        private Label lblPassword;
        private FlowLayoutPanel passwordPanel;
        private SiticoneTextBox txtPassword;
        private SiticoneButton btnTogglePassword;

        // Lembrar
        private FlowLayoutPanel rememberPanel;
        private SiticoneCheckBox chkRemember;

        // Mensagem de erro
        private Label lblMensagem;

        // Botões
        private SiticoneButton btnLogin;
        private FlowLayoutPanel dividerPanel;
        private SiticoneSeparator sepLeft;
        private Label lblOr;
        private SiticoneSeparator sepRight;
        private SiticoneButton btnCreateAccount;
        private SiticoneButton btnForgotPassword;

        // Termos
        private FlowLayoutPanel termsPanel;
        private Label lblTermsPrefix;
        private LinkLabel linkTermos;
        private Label lblE;
        private LinkLabel linkPrivacidade;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            // ===== FORM =====
            this.AutoScaleMode = AutoScaleMode.None;
            this.Text = "ClickDesk - Login";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(1366, 768);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.BackColor = StyleManager.BgPage;

            // Painel principal (card)
            mainCard = new SiticonePanel
            {
                BorderRadius = 22,
                FillColor = StyleManager.Card,
                ShadowDecoration = { Enabled = false },
                Anchor = AnchorStyles.None,
                Width = 1180,
                Height = 620
            };
            this.Controls.Add(mainCard);

            // Centralização do card (usada pelo FormLogin.cs em CenterCard)
            this.Load += (s, e) =>
            {
                mainCard.Left = (this.ClientSize.Width - mainCard.Width) / 2;
                mainCard.Top = (this.ClientSize.Height - mainCard.Height) / 2;
            };
            this.Resize += (s, e) =>
            {
                mainCard.Left = (this.ClientSize.Width - mainCard.Width) / 2;
                mainCard.Top = (this.ClientSize.Height - mainCard.Height) / 2;
            };

            // Layout raiz (2 colunas: Branding / Form)
            rootLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            mainCard.Controls.Add(rootLayout);

            // =============== BRANDING (ESQUERDA) ===============
            brandingPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(60, 80, 40, 40),
                Margin = new Padding(0)
            };

            lblBrandTitle = new Label
            {
                Text = "ClickDesk",
                Font = new Font("Segoe UI", 44, FontStyle.Bold),
                ForeColor = StyleManager.Accent,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 12)
            };

            lblBrandSubtitle = new Label
            {
                Text = "Sistema de gerenciamento\nde chamados técnicos",
                Font = new Font("Segoe UI", 13),
                ForeColor = StyleManager.TextSubtle,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 30)
            };

            brandingFeatures = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                Margin = new Padding(0)
            };

            lblFeature1 = new Label
            {
                Text = "✓ Gestão completa de chamados",
                Font = new Font("Segoe UI", 11),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 6)
            };
            lblFeature2 = new Label
            {
                Text = "✓ Controle de prazos e prioridades",
                Font = new Font("Segoe UI", 11),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 6)
            };
            lblFeature3 = new Label
            {
                Text = "✓ Colaboração em equipe",
                Font = new Font("Segoe UI", 11),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 0)
            };

            brandingFeatures.Controls.Add(lblFeature1);
            brandingFeatures.Controls.Add(lblFeature2);
            brandingFeatures.Controls.Add(lblFeature3);

            brandingPanel.Controls.Add(lblBrandTitle);
            brandingPanel.Controls.Add(lblBrandSubtitle);
            brandingPanel.Controls.Add(brandingFeatures);

            rootLayout.Controls.Add(brandingPanel, 0, 0);

            // =============== FORM (DIREITA) ===============
            formPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = false,
                AutoSize = false,
                Padding = new Padding(48, 52, 48, 40),
                Margin = new Padding(0)
            };
            rootLayout.Controls.Add(formPanel, 1, 0);

            // ----- Título Login ClickDesk -----
            titlePanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 6)
            };

            lblLoginOrange = new Label
            {
                Text = "Login ",
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                ForeColor = StyleManager.Accent,
                AutoSize = true,
                Margin = new Padding(0)
            };

            lblLoginBlack = new Label
            {
                Text = "ClickDesk",
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0)
            };

            titlePanel.Controls.Add(lblLoginOrange);
            titlePanel.Controls.Add(lblLoginBlack);

            lblSubtitle = new Label
            {
                Text = "Acesse sua conta para continuar",
                Font = new Font("Segoe UI", 11),
                ForeColor = StyleManager.TextSubtle,
                AutoSize = true,
                Margin = new Padding(0, 4, 0, 18)
            };

            formPanel.Controls.Add(titlePanel);
            formPanel.Controls.Add(lblSubtitle);

            // ----- Campo Usuário -----
            lblUser = new Label
            {
                Text = "Usuário",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0, 8, 0, 4)
            };

            txtUser = new SiticoneTextBox
            {
                Width = 420,
                Height = 45,
                BorderRadius = 12,
                BorderThickness = 1,
                BorderColor = StyleManager.InputBorder,
                FillColor = StyleManager.InputBg,
                ForeColor = StyleManager.TextStrong,
                Font = new Font("Segoe UI", 11),
                PlaceholderText = "Digite seu usuário",
                TextOffset = new Point(10, 0),
                Margin = new Padding(0, 0, 0, 8)
            };

            formPanel.Controls.Add(lblUser);
            formPanel.Controls.Add(txtUser);

            // ----- Campo Senha + Mostrar -----
            lblPassword = new Label
            {
                Text = "Senha",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0, 8, 0, 4)
            };

            passwordPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 8)
            };

            txtPassword = new SiticoneTextBox
            {
                Width = 320,
                Height = 45,
                BorderRadius = 12,
                BorderThickness = 1,
                BorderColor = StyleManager.InputBorder,
                FillColor = StyleManager.InputBg,
                ForeColor = StyleManager.TextStrong,
                Font = new Font("Segoe UI", 11),
                PlaceholderText = "Digite sua senha",
                PasswordChar = '●',
                TextOffset = new Point(10, 0),
                Margin = new Padding(0, 0, 8, 0)
            };

            btnTogglePassword = new SiticoneButton
            {
                Text = "Mostrar",
                Width = 80,
                Height = 45,
                BorderRadius = 10,
                FillColor = Color.FromArgb(235, 235, 235),
                ForeColor = StyleManager.TextStrong,
                Font = new Font("Segoe UI", 9),
                Margin = new Padding(0)
            };

            passwordPanel.Controls.Add(txtPassword);
            passwordPanel.Controls.Add(btnTogglePassword);

            formPanel.Controls.Add(lblPassword);
            formPanel.Controls.Add(passwordPanel);

            // ----- Lembrar-me -----
            rememberPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                Margin = new Padding(0, 4, 0, 10)
            };

            chkRemember = new SiticoneCheckBox
            {
                Text = "Lembrar-me",
                Font = new Font("Segoe UI", 10),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true
            };

            rememberPanel.Controls.Add(chkRemember);
            formPanel.Controls.Add(rememberPanel);

            // ----- Mensagem de erro -----
            lblMensagem = new Label
            {
                AutoSize = true,
                ForeColor = Color.FromArgb(211, 47, 47),
                Font = new Font("Segoe UI", 9),
                Text = string.Empty,
                Visible = false,
                Margin = new Padding(0, 0, 0, 8)
            };
            formPanel.Controls.Add(lblMensagem);

            // ----- Botão ENTRAR -----
            btnLogin = new SiticoneButton
            {
                Text = "ENTRAR",
                Width = 420,
                Height = 48,
                BorderRadius = 12,
                FillColor = StyleManager.Accent,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Margin = new Padding(0, 10, 0, 10),
                Enabled = false
            };
            formPanel.Controls.Add(btnLogin);

            // ----- Divisor "ou" -----
            dividerPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                Margin = new Padding(0, 4, 0, 8)
            };

            sepLeft = new SiticoneSeparator
            {
                Width = 165,
                Height = 1,
                FillColor = Color.FromArgb(228, 225, 218),
                Margin = new Padding(0, 8, 6, 8)
            };

            lblOr = new Label
            {
                Text = "ou",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = StyleManager.TextSubtle,
                AutoSize = true,
                Margin = new Padding(0, 0, 6, 0)
            };

            sepRight = new SiticoneSeparator
            {
                Width = 165,
                Height = 1,
                FillColor = Color.FromArgb(228, 225, 218),
                Margin = new Padding(0, 8, 0, 8)
            };

            dividerPanel.Controls.Add(sepLeft);
            dividerPanel.Controls.Add(lblOr);
            dividerPanel.Controls.Add(sepRight);
            formPanel.Controls.Add(dividerPanel);

            // ----- Botão Criar Conta -----
            btnCreateAccount = new SiticoneButton
            {
                Text = "CRIAR CONTA",
                Width = 420,
                Height = 48,
                BorderRadius = 12,
                BorderThickness = 2,
                BorderColor = StyleManager.Accent,
                FillColor = Color.White,
                ForeColor = StyleManager.Accent,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Margin = new Padding(0, 8, 0, 8),
                HoverState =
                {
                    FillColor = StyleManager.Accent,
                    ForeColor = Color.White
                }
            };
            formPanel.Controls.Add(btnCreateAccount);

            // ----- Botão Esqueci a Senha -----
            btnForgotPassword = new SiticoneButton
            {
                Text = "ESQUECI A SENHA",
                Height = 48,
                BorderRadius = 12,
                BorderThickness = 2,
                BorderColor = StyleManager.Accent,
                FillColor = Color.White,
                ForeColor = StyleManager.Accent,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Margin = new Padding(0, 0, 0, 12),
                Dock = DockStyle.Top,
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                HoverState =
                {
                    FillColor = StyleManager.Accent,
                    ForeColor = Color.White
                }
            };

            btnForgotPassword.MaximumSize = new Size(600, 48);
            btnForgotPassword.MinimumSize = new Size(350, 48);

            btnForgotPassword.Click += (s, e) =>
            {
                this.Hide();
                var frm = new FormRecuperarSenha();
                frm.FormClosed += (s2, a2) => this.Show();
                frm.Show();
            };
            formPanel.Controls.Add(btnForgotPassword);

            // ----- Termos de Uso + Política -----
            termsPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                Margin = new Padding(0, 18, 0, 0)
            };

            lblTermsPrefix = new Label
            {
                Text = "Ao continuar, você aceita nossos ",
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = StyleManager.TextSubtle
            };

            linkTermos = new LinkLabel
            {
                Text = "Termos de Uso",
                AutoSize = true,
                LinkColor = StyleManager.Accent,
                Font = new Font("Segoe UI", 9),
                Margin = new Padding(2, 0, 2, 0)
            };
            linkTermos.Click += (s, e) =>
            {
                this.Hide();
                var frm = new FormTermosUso();
                frm.FormClosed += (s2, e2) => this.Show();
                frm.Show();
            };

            lblE = new Label
            {
                Text = " e ",
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = StyleManager.TextSubtle
            };

            linkPrivacidade = new LinkLabel
            {
                Text = "Política de Privacidade",
                AutoSize = true,
                LinkColor = StyleManager.Accent,
                Font = new Font("Segoe UI", 9)
            };
            linkPrivacidade.Click += (s, e) =>
            {
                this.Hide();
                var frm = new FormTermosUso();
                frm.FormClosed += (s2, e2) => this.Show();
                frm.Show();
            };

            termsPanel.Controls.Add(lblTermsPrefix);
            termsPanel.Controls.Add(linkTermos);
            termsPanel.Controls.Add(lblE);
            termsPanel.Controls.Add(linkPrivacidade);

            formPanel.Controls.Add(termsPanel);

            // Z-Order
            mainCard.BringToFront();
            rootLayout.BringToFront();
            formPanel.BringToFront();
        }
    }
}

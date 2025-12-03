using ClickDesk.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClickDesk.Forms.Examples
{
    /// <summary>
    /// Formulário de exemplo demonstrando o uso de controles estilizados.
    /// Este formulário serve como template para padronizar o layout de todas as telas.
    /// </summary>
    public partial class FormStyledExample : Form
    {
        private Panel headerPanel;
        private Panel sidePanel;
        private Panel contentPanel;
        private Label titleLabel;
        private Label subtitleLabel;

        public FormStyledExample()
        {
            InitializeComponent();
            StyleForm();
            CreateLayout();
            LoadControls();
        }

        private void StyleForm()
        {
            // Configurar formulário
            Text = "ClickDesk - Exemplo Estilizado";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1200, 700);
            BackColor = StyleManager.Colors.BackgroundAlt;
            Font = StyleManager.Fonts.Body;
            ForeColor = StyleManager.Colors.TextPrimary;
        }

        private void CreateLayout()
        {
            // ============== HEADER ==============
            headerPanel = new Panel
            {
                BackColor = StyleManager.Colors.Primary,
                Padding = new Padding(24)
            };
            headerPanel.Location = new Point(0, 0);
            headerPanel.Size = new Size(ClientSize.Width, 80);

            titleLabel = new Label
            {
                Text = "Sistema de Gerenciamento",
                Font = StyleManager.Fonts.Heading2,
                ForeColor = StyleManager.Colors.TextInverted,
                AutoSize = true
            };
            titleLabel.Location = new Point(24, 20);
            headerPanel.Controls.Add(titleLabel);

            Controls.Add(headerPanel);

            // ============== SIDE PANEL ==============
            sidePanel = new Panel
            {
                BackColor = StyleManager.Colors.DarkerGray,
                Padding = new Padding(StyleManager.Spacing.PaddingLarge)
            };
            sidePanel.Location = new Point(0, 80);
            sidePanel.Size = new Size(280, ClientSize.Height - 80);
            Controls.Add(sidePanel);

            // Menu items no side panel
            AddMenuItem(sidePanel, "📊 Dashboard", 10);
            AddMenuItem(sidePanel, "🎫 Chamados", 60);
            AddMenuItem(sidePanel, "❓ FAQ", 110);
            AddMenuItem(sidePanel, "👤 Perfil", 160);
            AddMenuItem(sidePanel, "⚙️ Configurações", 210);

            // ============== CONTENT PANEL ==============
            contentPanel = new Panel
            {
                BackColor = StyleManager.Colors.BackgroundAlt,
                Padding = new Padding(StyleManager.Spacing.PaddingLarge)
            };
            contentPanel.Location = new Point(300, 100);
            contentPanel.Size = new Size(ClientSize.Width - 320, ClientSize.Height - 120);
            Controls.Add(contentPanel);

            // Título da seção
            subtitleLabel = new Label
            {
                Text = "Bem-vindo ao ClickDesk",
                Font = StyleManager.Fonts.Heading1,
                ForeColor = StyleManager.Colors.TextPrimary,
                AutoSize = true
            };
            subtitleLabel.Location = new Point(0, 0);
            contentPanel.Controls.Add(subtitleLabel);
        }

        private void LoadControls()
        {
            // ============== EXEMPLO: CARD COM INPUTS ==============
            StyledCard card = new StyledCard
            {
                Title = "Informações Pessoais",
                ShowTitle = true,
                Location = new Point(0, 60),
                Size = new Size(contentPanel.Width - StyleManager.Spacing.PaddingLarge * 2, 280),
                BackColor = StyleManager.Colors.Background
            };
            contentPanel.Controls.Add(card);

            // Input: Email
            Label emailLabel = new Label
            {
                Text = "Email",
                Font = StyleManager.Fonts.LabelBold,
                AutoSize = true,
                Location = new Point(StyleManager.Spacing.PaddingLarge, StyleManager.Spacing.PaddingLarge + 40)
            };
            card.Controls.Add(emailLabel);

            InputText emailInput = new InputText
            {
                Location = new Point(StyleManager.Spacing.PaddingLarge, emailLabel.Bottom + 8),
                Size = new Size(card.Width - StyleManager.Spacing.PaddingLarge * 3, 40),
                MaxLength = 100
            };
            card.Controls.Add(emailInput);

            // Input: Senha
            Label passwordLabel = new Label
            {
                Text = "Senha",
                Font = StyleManager.Fonts.LabelBold,
                AutoSize = true,
                Location = new Point(StyleManager.Spacing.PaddingLarge, emailInput.Bottom + 16)
            };
            card.Controls.Add(passwordLabel);

            InputText passwordInput = new InputText
            {
                Location = new Point(StyleManager.Spacing.PaddingLarge, passwordLabel.Bottom + 8),
                Size = new Size(card.Width - StyleManager.Spacing.PaddingLarge * 3, 40),
                UsePasswordChar = true
            };
            card.Controls.Add(passwordInput);

            // Botão: Salvar
            RoundedButton saveButton = new RoundedButton
            {
                Text = "Salvar",
                Location = new Point(StyleManager.Spacing.PaddingLarge, passwordInput.Bottom + 16),
                Size = new Size(120, 40),
                BackColor = StyleManager.Colors.Success,
                HoverColor = StyleManager.Colors.Primary,
                ClickColor = StyleManager.Colors.PrimaryDark
            };
            card.Controls.Add(saveButton);

            // Botão: Cancelar
            RoundedButton cancelButton = new RoundedButton
            {
                Text = "Cancelar",
                Location = new Point(saveButton.Right + 12, saveButton.Top),
                Size = new Size(120, 40),
                BackColor = StyleManager.Colors.DarkGray,
                HoverColor = StyleManager.Colors.TextSecondary,
                ClickColor = StyleManager.Colors.Dark
            };
            card.Controls.Add(cancelButton);

            // ============== EXEMPLO: CARD DE INFORMAÇÕES ==============
            StyledCard infoCard = new StyledCard
            {
                Title = "Estatísticas",
                ShowTitle = true,
                Location = new Point(0, card.Bottom + StyleManager.Spacing.Large),
                Size = new Size((contentPanel.Width - StyleManager.Spacing.Large) / 2 - StyleManager.Spacing.PaddingLarge, 150),
                BackColor = StyleManager.Colors.Background
            };
            contentPanel.Controls.Add(infoCard);

            Label statLabel = new Label
            {
                Text = "Total de Chamados: 42",
                Font = StyleManager.Fonts.BodyLarge,
                ForeColor = StyleManager.Colors.Primary,
                AutoSize = true,
                Location = new Point(StyleManager.Spacing.PaddingLarge, StyleManager.Spacing.PaddingLarge + 40)
            };
            infoCard.Controls.Add(statLabel);

            Label stat2Label = new Label
            {
                Text = "Chamados Pendentes: 8",
                Font = StyleManager.Fonts.BodyLarge,
                ForeColor = StyleManager.Colors.Warning,
                AutoSize = true,
                Location = new Point(StyleManager.Spacing.PaddingLarge, statLabel.Bottom + 8)
            };
            infoCard.Controls.Add(stat2Label);
        }

        private void AddMenuItem(Panel panel, string text, int top)
        {
            Button menuButton = new Button
            {
                Text = text,
                Location = new Point(0, top),
                Size = new Size(panel.Width - StyleManager.Spacing.PaddingLarge * 2, 40),
                BackColor = StyleManager.Colors.DarkerGray,
                HoverColor = StyleManager.Colors.Primary,
                ClickColor = StyleManager.Colors.PrimaryDark,
                ForeColor = StyleManager.Colors.TextInverted,
                BorderRadius = 6,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(StyleManager.Spacing.PaddingMedium, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            panel.Controls.Add(menuButton);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            ResumeLayout(false);
        }
    }
}

using ClickDesk.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;
using Siticone.Desktop.UI.WinForms;

namespace ClickDesk.Forms.Auth
{
    public partial class FormTermosUso : Form
    {
        private SiticonePanel mainCard;
        private TableLayoutPanel rootLayout;

        private FlowLayoutPanel brandingPanel;
        private Label lblBrandTitle;
        private Label lblBrandSubtitle;

        private FlowLayoutPanel contentPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private RichTextBox txtTermos;
        private SiticoneCheckBox chkAceito;
        private SiticoneButton btnVoltar;
        private SiticoneButton btnAceitar;

        public bool TermosAceitos { get; private set; }

        public FormTermosUso()
        {
            InitializeComponent();
            
            // ===== PADRÃO FULLSCREEN MAXIMIZADO =====
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = StyleManager.BgPage;

            CriarInterface();
        }

        private void CriarInterface()
        {
            Text = "ClickDesk - Termos de Uso";

            // ===== CENTRALIZAÇÃO DINÂMICA DO MAINCARD =====
            EventHandler centralizar = (s, e) =>
            {
                if (mainCard != null)
                {
                    mainCard.Left = (ClientSize.Width - mainCard.Width) / 2;
                    mainCard.Top = (ClientSize.Height - mainCard.Height) / 2;
                }
            };
            Load += centralizar;
            Resize += centralizar;

            // ===== PADRÃO DE MAINCARD =====
            mainCard = new SiticonePanel
            {
                BorderRadius = 22,
                FillColor = StyleManager.Card,
                Width = 1180,
                Height = 620,
                ShadowDecoration = { Enabled = false },
                Anchor = AnchorStyles.None,
                Dock = DockStyle.None
            };
            Controls.Add(mainCard);

            // ===== PADRÃO DE ROOTLAYOUT =====
            rootLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0),
                Padding = new Padding(0),
                Enabled = true
            };
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            mainCard.Controls.Add(rootLayout);

            CriarBranding();
            CriarConteudo();

            mainCard.BringToFront();
            rootLayout.BringToFront();
        }

        private void CriarBranding()
        {
            brandingPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(60, 80, 20, 40),
                Margin = new Padding(0)
            };

            lblBrandTitle = new Label
            {
                Text = "ClickDesk",
                Font = new Font("Segoe UI", 42, FontStyle.Bold),
                ForeColor = StyleManager.Accent,
                AutoSize = true
            };

            lblBrandSubtitle = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 14),
                ForeColor = StyleManager.TextSubtle,
                AutoSize = true,
                Margin = new Padding(0, 10, 0, 0)
            };

            brandingPanel.Controls.Add(lblBrandTitle);
            brandingPanel.Controls.Add(lblBrandSubtitle);

            rootLayout.Controls.Add(brandingPanel, 0, 0);
        }

        private void CriarConteudo()
        {
            contentPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(40, 60, 40, 40),
                Margin = new Padding(0)
            };

            lblTitle = new Label
            {
                Text = "Termos de Uso e Política de Privacidade",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 6)
            };

            lblSubtitle = new Label
            {
                Text = "Leia atentamente antes de continuar",
                Font = new Font("Segoe UI", 11),
                ForeColor = StyleManager.TextSubtle,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 18)
            };

            txtTermos = new RichTextBox
            {
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                Width = 540,
                Height = 280,
                Font = new Font("Segoe UI", 10),
                BackColor = StyleManager.Card,
                ForeColor = StyleManager.TextStrong,
                ScrollBars = RichTextBoxScrollBars.Vertical,
                Margin = new Padding(0, 0, 0, 16)
            };
            txtTermos.Text = ObterTextoDosTermos();

            chkAceito = new SiticoneCheckBox
            {
                Text = "Li e aceito os Termos de Uso e Política de Privacidade",
                Font = new Font("Segoe UI", 9),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 20),
                CheckedState = { FillColor = StyleManager.Accent, BorderColor = StyleManager.Accent },
                UncheckedState = { BorderColor = StyleManager.TextSubtle }
            };
            chkAceito.CheckedChanged += (s, e) =>
            {
                btnAceitar.Enabled = chkAceito.Checked;
                btnAceitar.FillColor = chkAceito.Checked ? StyleManager.Accent : Color.FromArgb(200, 200, 200);
            };

            var buttonsPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
            };

            btnVoltar = new SiticoneButton
            {
                Text = "Voltar",
                Width = 140,
                Height = 40,
                BorderRadius = 12,
                FillColor = Color.White,
                ForeColor = StyleManager.TextStrong,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Margin = new Padding(0, 0, 12, 0)
            };
            btnVoltar.Click += BtnVoltar_Click;

            btnAceitar = new SiticoneButton
            {
                Text = "Aceitar",
                Width = 160,
                Height = 40,
                BorderRadius = 12,
                FillColor = Color.FromArgb(200, 200, 200),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Enabled = false
            };
            btnAceitar.Click += BtnAceitar_Click;

            buttonsPanel.Controls.Add(btnVoltar);
            buttonsPanel.Controls.Add(btnAceitar);

            contentPanel.Controls.Add(lblTitle);
            contentPanel.Controls.Add(lblSubtitle);
            contentPanel.Controls.Add(txtTermos);
            contentPanel.Controls.Add(chkAceito);
            contentPanel.Controls.Add(buttonsPanel);

            rootLayout.Controls.Add(contentPanel, 1, 0);
        }

        private string ObterTextoDosTermos()
        {
            return @"1. ACEITAÇÃO DOS TERMOS
Ao acessar a plataforma ClickDesk, você concorda em estar vinculado por estes Termos de Uso.

2. USO DA PLATAFORMA
Uso permitido somente para fins legítimos.

3. PRIVACIDADE (LGPD)
Dados coletados apenas para fins essenciais, nunca compartilhados.

4. RESPONSABILIDADES DO USUÁRIO
Manter a segurança da conta.

5. LIMITAÇÃO DE RESPONSABILIDADE
Sistema fornecido ""como está"".";
        }

        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            TermosAceitos = false;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnAceitar_Click(object sender, EventArgs e)
        {
            TermosAceitos = true;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

using ClickDesk.Utils;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Siticone.Desktop.UI.WinForms;

namespace ClickDesk.Forms.Auth
{
    public partial class FormRecuperarSenha : Form
    {
        private SiticonePanel mainCard;
        private TableLayoutPanel rootLayout;

        private FlowLayoutPanel brandingPanel;
        private Label lblBrandTitle;
        private Label lblBrandSubtitle;

        private FlowLayoutPanel contentPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private SiticoneTextBox txtEmail;
        private SiticoneButton btnEnviar;
        private SiticoneButton btnCancelar;

        public FormRecuperarSenha()
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

            this.FormClosed += FormRecuperarSenha_FormClosed;
            CriarInterface();
        }

        private void CriarInterface()
        {
            Text = "ClickDesk - Recuperar Senha";

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
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
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
                Text = "Recupere o acesso à sua conta",
                Font = new Font("Segoe UI", 14),
                ForeColor = StyleManager.TextSubtle,
                AutoSize = true,
                Margin = new Padding(0, 30, 0, 0)
            };

            brandingPanel.Controls.Add(lblBrandTitle);
            brandingPanel.Controls.Add(lblBrandSubtitle);

            var cardsContainer = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                Margin = new Padding(0, 50, 0, 0)
            };

            var cards = new string[] {
                "✓ Link enviado por e-mail",
                "✓ Recuperação segura",
                "✓ Processo rápido e fácil"
            };

            foreach (var cardText in cards)
            {
                var card = new Label
                {
                    Text = cardText,
                    Font = new Font("Segoe UI", 11),
                    ForeColor = StyleManager.TextStrong,
                    AutoSize = true,
                    Margin = new Padding(0, 8, 0, 0)
                };
                cardsContainer.Controls.Add(card);
            }

            brandingPanel.Controls.Add(cardsContainer);
            rootLayout.Controls.Add(brandingPanel, 0, 0);
        }

        private void CriarConteudo()
        {
            contentPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(40, 40, 40, 40),
                Margin = new Padding(0)
            };

            var picIcone = new PictureBox
            {
                Width = 42,
                Height = 42,
                Image = CriarSVGRelogio(),
                SizeMode = PictureBoxSizeMode.Zoom,
                Margin = new Padding(0, 0, 0, 16)
            };
            contentPanel.Controls.Add(picIcone);

            lblTitle = new Label
            {
                Text = "Esqueceu a senha?",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 6)
            };
            contentPanel.Controls.Add(lblTitle);

            lblSubtitle = new Label
            {
                Text = "Não se preocupe. Insira seu e-mail abaixo...",
                Font = new Font("Segoe UI", 11),
                ForeColor = StyleManager.TextSubtle,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 28)
            };
            contentPanel.Controls.Add(lblSubtitle);

            var lblEmailLabel = new Label
            {
                Text = "E-mail",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = StyleManager.TextStrong,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 8)
            };
            contentPanel.Controls.Add(lblEmailLabel);

            txtEmail = new SiticoneTextBox
            {
                Width = 420,
                Height = 45,
                BorderRadius = 12,
                BorderThickness = 1,
                BorderColor = StyleManager.InputBorder,
                FillColor = StyleManager.InputBg,
                ForeColor = StyleManager.TextStrong,
                Font = new Font("Segoe UI", 10),
                PlaceholderText = "seu.email@example.com",
                TextOffset = new Point(12, 0),
                Margin = new Padding(0, 0, 0, 28)
            };
            contentPanel.Controls.Add(txtEmail);

            btnEnviar = new SiticoneButton
            {
                Text = "ENVIAR LINK DE RECUPERAÇÃO",
                Width = 420,
                Height = 45,
                BorderRadius = 12,
                FillColor = StyleManager.Accent,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 0, 16)
            };
            btnEnviar.Click += BtnEnviar_Click;
            contentPanel.Controls.Add(btnEnviar);

            btnCancelar = new SiticoneButton
            {
                Text = "CANCELAR",
                Width = 420,
                Height = 45,
                BorderRadius = 12,
                FillColor = Color.Transparent,
                ForeColor = StyleManager.Accent,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                BorderThickness = 2,
                BorderColor = StyleManager.Accent,
                Margin = new Padding(0, 0, 0, 16)
            };
            btnCancelar.Click += BtnCancelar_Click;
            contentPanel.Controls.Add(btnCancelar);

            // --- NOVO BOTÃO "VOLTAR PARA O LOGIN" ---
            var btnVoltarLogin = new SiticoneButton
            {
                Text = "VOLTAR PARA O LOGIN",
                Width = 420,
                Height = 45,
                BorderRadius = 12,
                FillColor = Color.Transparent,
                ForeColor = StyleManager.Accent,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                BorderThickness = 2,
                BorderColor = StyleManager.Accent,
                Margin = new Padding(0, 0, 0, 0)
            };
            btnVoltarLogin.Click += (s, e) =>
            {
                var frm = new FormLogin();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.Show();
                this.Close();
            };

            contentPanel.Controls.Add(btnVoltarLogin);

            rootLayout.Controls.Add(contentPanel, 1, 0);
        }

        private async void BtnEnviar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                UIHelper.ShowError("Por favor, informe seu e-mail.");
                txtEmail.Focus();
                return;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                UIHelper.ShowError("Por favor, informe um e-mail válido.");
                txtEmail.Focus();
                return;
            }

            btnEnviar.Enabled = false;
            btnEnviar.Text = "AGUARDE...";

            try
            {
                await Task.Delay(1200);
                UIHelper.ShowSuccess("Enviamos um link de recuperação para seu e-mail.");
                this.Close();
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Erro ao enviar link: {ex.Message}");
            }
            finally
            {
                btnEnviar.Enabled = true;
                btnEnviar.Text = "ENVIAR LINK DE RECUPERAÇÃO";
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            var frm = new FormLogin();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
            this.Close();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch { return false; }
        }

        private Bitmap CriarSVGRelogio()
        {
            var bmp = new Bitmap(42, 42);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                var pen = new Pen(Color.FromArgb(21, 36, 27), 2f);

                // círculo minimalista
                g.DrawEllipse(pen, 10, 10, 22, 22);

                // ponteiro fino
                g.DrawLine(pen, 21, 21, 21, 15);

                // ponteiro pequeno
                g.DrawLine(pen, 21, 21, 26, 23);
            }
            return bmp;
        }

        private void FormRecuperarSenha_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Se o usuário fechar a tela, voltar para o login automaticamente
            var frm = new FormLogin();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }
    }
}

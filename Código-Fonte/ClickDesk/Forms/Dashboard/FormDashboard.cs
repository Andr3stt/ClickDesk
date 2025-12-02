using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickDesk.Forms.Auth;
using ClickDesk.Forms.Chamados;
using ClickDesk.Forms.FAQ;
using ClickDesk.Forms.Perfil;
using ClickDesk.Models;
using ClickDesk.Services.API;
using ClickDesk.Utils;
using Siticone.Desktop.UI.WinForms;

namespace ClickDesk.Forms.Dashboard
{
    /// <summary>
    /// Formulário principal do dashboard para usuários.
    /// Exibe estatísticas, últimos chamados e menu de navegação.
    /// Modernizado com Siticone UI Framework e suporte a temas.
    /// </summary>
    public partial class FormDashboard : Form
    {
        // Componentes da interface (modernizados)
        protected SiticonePanel sidebarPanel;
        protected Panel contentPanel;
        protected SiticoneDataGridView dgvChamados;
        protected Label lblTotalChamados;
        protected Label lblChamadosAbertos;
        protected Label lblChamadosResolvidos;
        protected Label lblChamadosEscalados;
        protected SiticoneButton btnThemeToggle;

        // Lista de botões do menu para controle de estado ativo
        protected List<SiticoneButton> menuButtons = new List<SiticoneButton>();

        /// <summary>
        /// Construtor do dashboard.
        /// </summary>
        public FormDashboard()
        {
            InitializeComponent();
            SetupForm();
        }

        /// <summary>
        /// Configura a aparência e os controles do formulário.
        /// </summary>
        protected virtual void SetupForm()
        {
            // Configurações básicas do form
            this.Text = "ClickDesk - Dashboard";
            this.Size = new Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable; // Allow resizing for responsive design
            this.BackColor = ThemeManager.BackgroundApp;
            this.MinimumSize = new Size(1200, 700);

            // Subscribe to theme changes
            ThemeManager.ThemeChanged += (s, e) =>
            {
                this.BackColor = ThemeManager.BackgroundApp;
                ApplyTheme();
            };

            // Cria a sidebar
            SetupSidebar();

            // Cria a área de conteúdo
            SetupContent();

            // Carrega os dados
            this.Load += async (s, e) => await CarregarDados();
        }

        /// <summary>
        /// Configura a sidebar com menu de navegação (Siticone).
        /// </summary>
        protected virtual void SetupSidebar()
        {
            sidebarPanel = new SiticonePanel
            {
                Width = 260,
                Dock = DockStyle.Left,
                FillColor = ThemeManager.SidebarBackground,
                Padding = new Padding(0),
                BorderRadius = 0 // No border radius for docked panel
            };
            this.Controls.Add(sidebarPanel);

            int y = 0;

            // Cabeçalho com logo
            Panel headerPanel = new Panel
            {
                Size = new Size(260, 80),
                BackColor = ClickDeskColors.Gray900,
                Location = new Point(0, y)
            };
            sidebarPanel.Controls.Add(headerPanel);

            Label lblLogo = new Label
            {
                Text = "🖥️ ClickDesk",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = ClickDeskColors.White,
                Location = new Point(45, 25),
                AutoSize = true
            };
            headerPanel.Controls.Add(lblLogo);

            y += 80;

            // Informações do usuário
            Panel userPanel = new Panel
            {
                Size = new Size(260, 70),
                BackColor = ClickDeskColors.Gray700,
                Location = new Point(0, y)
            };
            sidebarPanel.Controls.Add(userPanel);

            Label lblUserName = new Label
            {
                Text = SessionManager.UserDisplayName,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ClickDeskColors.White,
                Location = new Point(20, 15),
                AutoSize = true
            };
            userPanel.Controls.Add(lblUserName);

            Label lblUserRole = new Label
            {
                Text = SessionManager.UserRole,
                Font = new Font("Segoe UI", 9),
                ForeColor = ClickDeskColors.Gray400,
                Location = new Point(20, 40),
                AutoSize = true
            };
            userPanel.Controls.Add(lblUserRole);

            y += 70;

            // Menu de navegação
            y += 20;

            // Dashboard
            var btnDashboard = CreateMenuButton("📊  Dashboard", y);
            btnDashboard.Click += (s, e) => { /* Já está no dashboard */ };
            SetMenuButtonActive(btnDashboard, true);
            menuButtons.Add(btnDashboard);
            sidebarPanel.Controls.Add(btnDashboard);
            y += 50;

            // Novo Chamado
            var btnNovoChamado = CreateMenuButton("➕  Novo Chamado", y);
            btnNovoChamado.Click += BtnNovoChamado_Click;
            menuButtons.Add(btnNovoChamado);
            sidebarPanel.Controls.Add(btnNovoChamado);
            y += 50;

            // Meus Chamados
            var btnMeusChamados = CreateMenuButton("📋  Meus Chamados", y);
            btnMeusChamados.Click += BtnMeusChamados_Click;
            menuButtons.Add(btnMeusChamados);
            sidebarPanel.Controls.Add(btnMeusChamados);
            y += 50;

            // FAQ
            var btnFAQ = CreateMenuButton("❓  FAQ", y);
            btnFAQ.Click += BtnFAQ_Click;
            menuButtons.Add(btnFAQ);
            sidebarPanel.Controls.Add(btnFAQ);
            y += 50;

            // Meu Perfil
            var btnPerfil = CreateMenuButton("👤  Meu Perfil", y);
            btnPerfil.Click += BtnPerfil_Click;
            menuButtons.Add(btnPerfil);
            sidebarPanel.Controls.Add(btnPerfil);
            y += 50;

            // Theme Toggle
            y += 20; // Extra spacing before theme toggle
            btnThemeToggle = new SiticoneButton
            {
                Text = ThemeManager.IsDarkMode ? "🌙  Modo Escuro" : "☀️  Modo Claro",
                Size = new Size(260, 50),
                Location = new Point(0, y),
                BorderRadius = 0,
                FillColor = ThemeManager.SidebarBackground,
                ForeColor = ThemeManager.SidebarText,
                Font = new Font("Segoe UI", 11F),
                TextAlign = System.Windows.Forms.HorizontalAlignment.Left,
                ImageAlign = System.Windows.Forms.HorizontalAlignment.Left,
                Cursor = Cursors.Hand,
                HoverState = { FillColor = ThemeManager.SidebarHover }
            };
            btnThemeToggle.Click += (s, e) =>
            {
                ThemeManager.ToggleTheme();
                btnThemeToggle.Text = ThemeManager.IsDarkMode ? "🌙  Modo Escuro" : "☀️  Modo Claro";
                ThemeManager.SaveThemePreference();
            };
            sidebarPanel.Controls.Add(btnThemeToggle);

            // Espaçador
            y = sidebarPanel.Height - 60;

            // Logout (no final)
            var btnLogout = CreateMenuButton("🚪  Sair", y);
            btnLogout.Click += BtnLogout_Click;
            btnLogout.Dock = DockStyle.Bottom;
            sidebarPanel.Controls.Add(btnLogout);
        }

        /// <summary>
        /// Cria um botão de menu estilizado (Siticone).
        /// </summary>
        protected SiticoneButton CreateMenuButton(string text, int y)
        {
            var button = new SiticoneButton
            {
                Text = text,
                Size = new Size(260, 50),
                Location = new Point(0, y),
                BorderRadius = 0,
                FillColor = ThemeManager.SidebarBackground,
                ForeColor = ThemeManager.SidebarText,
                Font = new Font("Segoe UI", 11F),
                TextAlign = System.Windows.Forms.HorizontalAlignment.Left,
                ImageAlign = System.Windows.Forms.HorizontalAlignment.Left,
                Cursor = Cursors.Hand,
                HoverState = { FillColor = ThemeManager.SidebarHover }
            };
            return button;
        }

        /// <summary>
        /// Destaca um botão de menu como ativo.
        /// </summary>
        protected void SetMenuButtonActive(SiticoneButton button, bool active)
        {
            if (active)
            {
                button.FillColor = ThemeManager.SidebarHover;
                button.ForeColor = ThemeManager.Brand;
            }
            else
            {
                button.FillColor = ThemeManager.SidebarBackground;
                button.ForeColor = ThemeManager.SidebarText;
            }
        }

        /// <summary>
        /// Configura a área de conteúdo principal.
        /// </summary>
        protected virtual void SetupContent()
        {
            contentPanel = UIHelper.CreateContentArea();
            contentPanel.Padding = new Padding(ClickDeskStyles.PaddingMainArea, ClickDeskStyles.PaddingMainAreaVertical, ClickDeskStyles.PaddingMainArea, ClickDeskStyles.PaddingMainAreaVertical);
            contentPanel.BackColor = ThemeManager.BackgroundApp;
            this.Controls.Add(contentPanel);

            // Título
            Label lblTitle = new Label
            {
                Text = "Dashboard",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = ThemeManager.TextPrimary,
                Location = new Point(ClickDeskStyles.PaddingMainArea, 10),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            contentPanel.Controls.Add(lblTitle);

            Label lblSubtitle = new Label
            {
                Text = $"Bem-vindo, {SessionManager.UserDisplayName}!  👋",
                Font = new Font("Segoe UI", 12),
                ForeColor = ThemeManager.TextSecondary,
                Location = new Point(ClickDeskStyles.PaddingMainArea, 50),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            contentPanel.Controls.Add(lblSubtitle);

            // Cards de estatísticas
            SetupStatsCards();

            // Tabela de últimos chamados
            SetupChamadosTable();
        }

        /// <summary>
        /// Configura os cards de estatísticas.
        /// </summary>
        protected virtual void SetupStatsCards()
        {
            int cardWidth = 260;
            int cardHeight = 130;
            int cardSpacing = ClickDeskStyles.GapGrid;
            int startX = ClickDeskStyles.PaddingMainArea;
            int startY = 90;

            // Card Total
            var cardTotal = CreateStatCard("Total de Chamados", "0", ClickDeskColors.Primary, startX, startY, cardWidth, cardHeight);
            lblTotalChamados = (Label)cardTotal.Controls[1];
            contentPanel.Controls.Add(cardTotal);

            // Card Abertos
            var cardAbertos = CreateStatCard("Abertos", "0", ClickDeskColors.Warning, startX + cardWidth + cardSpacing, startY, cardWidth, cardHeight);
            lblChamadosAbertos = (Label)cardAbertos.Controls[1];
            contentPanel.Controls.Add(cardAbertos);

            // Card Resolvidos
            var cardResolvidos = CreateStatCard("Resolvidos", "0", ClickDeskColors.Success, startX + (cardWidth + cardSpacing) * 2, startY, cardWidth, cardHeight);
            lblChamadosResolvidos = (Label)cardResolvidos.Controls[1];
            contentPanel.Controls.Add(cardResolvidos);

            // Card Escalados
            var cardEscalados = CreateStatCard("Escalados", "0", ClickDeskColors.Danger, startX + (cardWidth + cardSpacing) * 3, startY, cardWidth, cardHeight);
            lblChamadosEscalados = (Label)cardEscalados.Controls[1];
            contentPanel.Controls.Add(cardEscalados);
        }

        /// <summary>
        /// Cria um card de estatística.
        /// </summary>
        protected Panel CreateStatCard(string title, string value, Color accentColor, int x, int y, int width, int height)
        {
            var panel = new Panel
            {
                Size = new Size(width, height),
                Location = new Point(x, y),
                BackColor = ClickDeskColors.Surface,
                Padding = new Padding(ClickDeskStyles.PaddingCard)
            };

            // Bordas arredondadas
            panel.Paint += (s, e) =>
            {
                var rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
                var path = ClickDeskStyles.GetRoundedRectangle(rect, ClickDeskStyles.RadiusXL);

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                using (var brush = new SolidBrush(ClickDeskColors.Surface))
                {
                    e.Graphics.FillPath(brush, path);
                }

                using (var pen = new Pen(ClickDeskColors.Border, 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            };

            // Título
            var lblTitle = new Label
            {
                Text = title,
                Font = ClickDeskStyles.FontLG,
                ForeColor = ClickDeskColors.TextSecondary,
                Location = new Point(ClickDeskStyles.PaddingCard, ClickDeskStyles.PaddingCard),
                Size = new Size(230, 30),
                BackColor = Color.Transparent
            };
            panel.Controls.Add(lblTitle);

            // Valor
            var lblValue = new Label
            {
                Text = value,
                Font = ClickDeskStyles.Font5XL,
                ForeColor = accentColor,
                Location = new Point(ClickDeskStyles.PaddingCard, 50),
                Size = new Size(230, 60),
                BackColor = Color.Transparent
            };
            panel.Controls.Add(lblValue);

            return panel;
        }

        /// <summary>
        /// Configura a tabela de chamados.
        /// </summary>
        protected virtual void SetupChamadosTable()
        {
            // Título da seção
            Label lblUltimos = new Label
            {
                Text = "Últimos Chamados",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(ClickDeskStyles.PaddingMainArea, 240),
                AutoSize = true
            };
            contentPanel.Controls.Add(lblUltimos);

            // Botão Novo Chamado
            var btnNovo = UIHelper.CreatePrimaryButton("+ Novo Chamado", 150, 35);
            btnNovo.Location = new Point(850, 235);
            btnNovo.Click += BtnNovoChamado_Click;
            btnNovo.FlatAppearance.MouseOverBackColor = ClickDeskColors.BrandDark;
            contentPanel.Controls.Add(btnNovo);

            // DataGridView
            dgvChamados = new SiticoneDataGridView
            {
                Location = new Point(ClickDeskStyles.PaddingMainArea, 280),
                Size = new Size(980, 400),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            UIHelper.StyleDataGridView(dgvChamados);

            // Colunas
            dgvChamados.Columns.Add("Id", "ID");
            dgvChamados.Columns.Add("Titulo", "Título");
            dgvChamados.Columns.Add("Categoria", "Categoria");
            dgvChamados.Columns.Add("Status", "Status");
            dgvChamados.Columns.Add("Severidade", "Severidade");
            dgvChamados.Columns.Add("Data", "Data");

            dgvChamados.Columns["Id"].Width = 60;
            dgvChamados.Columns["Titulo"].Width = 300;
            dgvChamados.Columns["Categoria"].Width = 150;
            dgvChamados.Columns["Status"].Width = 120;
            dgvChamados.Columns["Severidade"].Width = 100;
            dgvChamados.Columns["Data"].Width = 150;

            // Evento de duplo clique para ver detalhes
            dgvChamados.CellDoubleClick += DgvChamados_CellDoubleClick;

            contentPanel.Controls.Add(dgvChamados);
        }

        /// <summary>
        /// Carrega os dados do dashboard.
        /// </summary>
        protected virtual async Task CarregarDados()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Carrega estatísticas
                await CarregarEstatisticas();

                // Carrega últimos chamados
                await CarregarChamados();
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == 401)
                {
                    UIHelper.ShowError("Sessão expirada. Faça login novamente.");
                    FazerLogout();
                }
                else
                {
                    UIHelper.ShowError($"Erro ao carregar dados: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Erro ao carregar dados: {ex.Message}");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Carrega as estatísticas.
        /// </summary>
        protected virtual async Task CarregarEstatisticas()
        {
            try
            {
                var stats = await ChamadoService.ObterEstatisticasAsync();
                if (stats != null)
                {
                    lblTotalChamados.Text = stats.TotalChamados.ToString();
                    lblChamadosAbertos.Text = stats.ChamadosAbertos.ToString();
                    lblChamadosResolvidos.Text = stats.ChamadosResolvidos.ToString();
                    lblChamadosEscalados.Text = stats.ChamadosEscalados.ToString();
                }
            }
            catch
            {
                // Em caso de erro, mantém os valores zerados
            }
        }

        /// <summary>
        /// Carrega a lista de chamados.
        /// </summary>
        protected virtual async Task CarregarChamados()
        {
            try
            {
                var chamados = await ChamadoService.ListarMeusAsync();
                PreencherTabelaChamados(chamados);
            }
            catch
            {
                // Em caso de erro, tabela fica vazia
            }
        }

        /// <summary>
        /// Preenche a tabela com os chamados.
        /// </summary>
        protected void PreencherTabelaChamados(List<Chamado> chamados)
        {
            dgvChamados.Rows.Clear();

            if (chamados != null)
            {
                foreach (var chamado in chamados)
                {
                    dgvChamados.Rows.Add(
                        chamado.Id,
                        chamado.Titulo,
                        chamado.Categoria,
                        chamado.Status,
                        chamado.Severidade,
                        chamado.DataAbertura.ToString("dd/MM/yyyy HH:mm")
                    );
                }
            }
        }

        /// <summary>
        /// Evento de duplo clique na tabela para ver detalhes.
        /// </summary>
        protected void DgvChamados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int chamadoId = Convert.ToInt32(dgvChamados.Rows[e.RowIndex].Cells["Id"].Value);
                AbrirDetalhesChamado(chamadoId);
            }
        }

        /// <summary>
        /// Abre o formulário de detalhes do chamado.
        /// </summary>
        protected void AbrirDetalhesChamado(int chamadoId)
        {
            var formDetalhes = new FormDetalhesChamado(chamadoId);
            formDetalhes.FormClosed += async (s, e) => await CarregarDados();
            formDetalhes.ShowDialog(this);
        }

        // ========================================
        // EVENTOS DE NAVEGAÇÃO
        // ========================================

        /// <summary>
        /// Abre o formulário de novo chamado.
        /// </summary>
        protected void BtnNovoChamado_Click(object sender, EventArgs e)
        {
            var formNovo = new FormNovoChamado();
            formNovo.FormClosed += async (s, args) => await CarregarDados();
            formNovo.ShowDialog(this);
        }

        /// <summary>
        /// Abre o formulário de meus chamados.
        /// </summary>
        protected void BtnMeusChamados_Click(object sender, EventArgs e)
        {
            var formMeus = new FormMeusChamados();
            formMeus.ShowDialog(this);
        }

        /// <summary>
        /// Abre o formulário de FAQ.
        /// </summary>
        protected void BtnFAQ_Click(object sender, EventArgs e)
        {
            var formFAQ = new FormFAQ();
            formFAQ.ShowDialog(this);
        }

        /// <summary>
        /// Abre o formulário de perfil.
        /// </summary>
        protected void BtnPerfil_Click(object sender, EventArgs e)
        {
            var formPerfil = new FormPerfil();
            formPerfil.ShowDialog(this);
        }

        /// <summary>
        /// Realiza o logout.
        /// </summary>
        protected async void BtnLogout_Click(object sender, EventArgs e)
        {
            if (UIHelper.ShowConfirmation("Deseja realmente sair?", "Confirmar Logout"))
            {
                await FazerLogout();
            }
        }

        /// <summary>
        /// Executa o logout e retorna para a tela de login.
        /// </summary>
        protected async Task FazerLogout()
        {
            try
            {
                await AuthService.LogoutAsync();
            }
            catch { }

            this.Hide();
            var formLogin = new FormLogin();
            formLogin.FormClosed += (s, e) => this.Close();
            formLogin.Show();
        }

        /// <summary>
        /// Aplica o tema atual a todos os controles do formulário.
        /// </summary>
        protected virtual void ApplyTheme()
        {
            // Update sidebar
            sidebarPanel.FillColor = ThemeManager.SidebarBackground;
            
            // Update all menu buttons
            foreach (var button in menuButtons)
            {
                if (button.FillColor == ThemeManager.SidebarBackground || 
                    button.FillColor == ClickDeskColors.Gray800)
                {
                    button.FillColor = ThemeManager.SidebarBackground;
                    button.ForeColor = ThemeManager.SidebarText;
                    button.HoverState.FillColor = ThemeManager.SidebarHover;
                }
            }

            // Update theme toggle button
            if (btnThemeToggle != null)
            {
                btnThemeToggle.FillColor = ThemeManager.SidebarBackground;
                btnThemeToggle.ForeColor = ThemeManager.SidebarText;
                btnThemeToggle.HoverState.FillColor = ThemeManager.SidebarHover;
            }

            // Update content area
            if (contentPanel != null)
            {
                contentPanel.BackColor = ThemeManager.BackgroundApp;
                
                // Update all labels in content panel
                UpdateLabelsTheme(contentPanel.Controls);
            }

            // Update DataGridView if exists
            if (dgvChamados != null)
            {
                dgvChamados.BackgroundColor = ThemeManager.CardBackground;
                dgvChamados.GridColor = ThemeManager.Border;
                dgvChamados.DefaultCellStyle.BackColor = ThemeManager.CardBackground;
                dgvChamados.DefaultCellStyle.ForeColor = ThemeManager.TextPrimary;
                dgvChamados.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.SidebarBackground;
                dgvChamados.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.SidebarText;
                dgvChamados.AlternatingRowsDefaultCellStyle.BackColor = ThemeManager.Surface;
            }

            this.Invalidate();
        }

        /// <summary>
        /// Atualiza recursivamente as cores dos labels.
        /// </summary>
        private void UpdateLabelsTheme(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is Label label)
                {
                    if (label.Font.Bold || label.Font.Size > 14)
                    {
                        label.ForeColor = ThemeManager.TextPrimary;
                    }
                    else
                    {
                        label.ForeColor = ThemeManager.TextSecondary;
                    }
                }
                else if (control is Panel panel)
                {
                    // Update panels (cards)
                    if (panel.BackColor == ClickDeskColors.White || 
                        panel.BackColor == ClickDeskColors.CardBackground)
                    {
                        panel.BackColor = ThemeManager.CardBackground;
                    }
                    
                    // Recursively update child controls
                    if (panel.HasChildren)
                    {
                        UpdateLabelsTheme(panel.Controls);
                    }
                }
            }
        }
    }
}

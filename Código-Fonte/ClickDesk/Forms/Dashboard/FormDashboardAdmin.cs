using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickDesk.Forms.Chamados;
using ClickDesk.Forms.FAQ;
using ClickDesk.Forms.Perfil;
using ClickDesk.Services.API;
using ClickDesk.Utils;

namespace ClickDesk.Forms.Dashboard
{
    /// <summary>
    /// Formulário de dashboard administrativo.
    /// Estende o dashboard padrão com funcionalidades para admin/tech.
    /// </summary>
    public partial class FormDashboardAdmin : FormDashboard
    {
        // Labels adicionais para admin
        private Label lblResolvidosPorIA;
        private Label lblTempoMedio;

        /// <summary>
        /// Construtor do dashboard admin.
        /// </summary>
        public FormDashboardAdmin()
        {
            // O construtor base já chama InitializeComponent e SetupForm
        }

        /// <summary>
        /// Configura o formulário com funcionalidades admin.
        /// </summary>
        protected override void SetupForm()
        {
            base.SetupForm();
            this.Text = "ClickDesk - Painel Administrativo";
        }

        /// <summary>
        /// Configura a sidebar com menus adicionais de admin.
        /// </summary>
        protected override void SetupSidebar()
        {
            sidebarPanel = UIHelper.CreateSidebar(260);
            this.Controls.Add(sidebarPanel);

            int y = 0;

            // Cabeçalho com logo
            Panel headerPanel = new Panel
            {
                Size = new Size(260, 80),
                BackColor = AppColors.Gray900,
                Location = new Point(0, y)
            };
            sidebarPanel.Controls.Add(headerPanel);

            Label lblLogo = new Label
            {
                Text = "🖥️ ClickDesk",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = AppColors.White,
                Location = new Point(45, 25),
                AutoSize = true
            };
            headerPanel.Controls.Add(lblLogo);

            y += 80;

            // Informações do usuário com badge admin
            Panel userPanel = new Panel
            {
                Size = new Size(260, 70),
                BackColor = AppColors.Gray700,
                Location = new Point(0, y)
            };
            sidebarPanel.Controls.Add(userPanel);

            Label lblUserName = new Label
            {
                Text = SessionManager.UserDisplayName,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = AppColors.White,
                Location = new Point(20, 15),
                AutoSize = true
            };
            userPanel.Controls.Add(lblUserName);

            // Badge de role
            Label lblRoleBadge = new Label
            {
                Text = SessionManager.UserRole,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = AppColors.White,
                BackColor = SessionManager.IsAdmin ? AppColors.Danger : AppColors.Warning,
                Location = new Point(20, 40),
                Size = new Size(60, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };
            userPanel.Controls.Add(lblRoleBadge);

            y += 70;

            // Menu de navegação
            y += 20;

            // Dashboard
            var btnDashboard = CreateMenuButton("📊  Dashboard", y);
            btnDashboard.Click += (s, e) => { /* Já está no dashboard */ };
            UIHelper.SetMenuButtonActive(btnDashboard, true);
            sidebarPanel.Controls.Add(btnDashboard);
            y += 50;

            // Todos os Chamados
            var btnTodosChamados = CreateMenuButton("📋  Todos os Chamados", y);
            btnTodosChamados.Click += BtnTodosChamados_Click;
            sidebarPanel.Controls.Add(btnTodosChamados);
            y += 50;

            // Aprovar Chamados
            var btnAprovar = CreateMenuButton("✅  Aprovar Chamados", y);
            btnAprovar.Click += BtnAprovarChamados_Click;
            sidebarPanel.Controls.Add(btnAprovar);
            y += 50;

            // Gerenciar FAQ (apenas admin)
            if (SessionManager.IsAdmin)
            {
                var btnFAQAdmin = CreateMenuButton("📚  Gerenciar FAQ", y);
                btnFAQAdmin.Click += BtnFAQAdmin_Click;
                sidebarPanel.Controls.Add(btnFAQAdmin);
                y += 50;

                // Criar Usuário
                var btnCriarUsuario = CreateMenuButton("👤  Criar Usuário", y);
                btnCriarUsuario.Click += BtnCriarUsuario_Click;
                sidebarPanel.Controls.Add(btnCriarUsuario);
                y += 50;
            }

            // Separador
            y += 20;

            // FAQ
            var btnFAQ = CreateMenuButton("❓  FAQ", y);
            btnFAQ.Click += BtnFAQ_Click;
            sidebarPanel.Controls.Add(btnFAQ);
            y += 50;

            // Meu Perfil
            var btnPerfil = CreateMenuButton("👤  Meu Perfil", y);
            btnPerfil.Click += BtnPerfil_Click;
            sidebarPanel.Controls.Add(btnPerfil);
            y += 50;

            // Logout (no final)
            var btnLogout = CreateMenuButton("🚪  Sair", 0);
            btnLogout.Click += BtnLogout_Click;
            btnLogout.Dock = DockStyle.Bottom;
            sidebarPanel.Controls.Add(btnLogout);
        }

        /// <summary>
        /// Configura os cards de estatísticas com informações adicionais.
        /// </summary>
        protected override void SetupStatsCards()
        {
            int cardWidth = 170;
            int cardHeight = 110;
            int cardSpacing = 20;
            int startX = 20;
            int startY = 90;

            // Card Total
            var cardTotal = CreateStatCard("Total", "0", AppColors.Primary, startX, startY, cardWidth, cardHeight);
            lblTotalChamados = (Label)cardTotal.Controls[1];
            contentPanel.Controls.Add(cardTotal);

            // Card Abertos
            var cardAbertos = CreateStatCard("Abertos", "0", AppColors.Warning, startX + cardWidth + cardSpacing, startY, cardWidth, cardHeight);
            lblChamadosAbertos = (Label)cardAbertos.Controls[1];
            contentPanel.Controls.Add(cardAbertos);

            // Card Resolvidos
            var cardResolvidos = CreateStatCard("Resolvidos", "0", AppColors.Success, startX + (cardWidth + cardSpacing) * 2, startY, cardWidth, cardHeight);
            lblChamadosResolvidos = (Label)cardResolvidos.Controls[1];
            contentPanel.Controls.Add(cardResolvidos);

            // Card Escalados
            var cardEscalados = CreateStatCard("Escalados", "0", AppColors.Danger, startX + (cardWidth + cardSpacing) * 3, startY, cardWidth, cardHeight);
            lblChamadosEscalados = (Label)cardEscalados.Controls[1];
            contentPanel.Controls.Add(cardEscalados);

            // Card Resolvidos por IA
            var cardIA = CreateStatCard("IA", "0", AppColors.Info, startX + (cardWidth + cardSpacing) * 4, startY, cardWidth, cardHeight);
            lblResolvidosPorIA = (Label)cardIA.Controls[1];
            contentPanel.Controls.Add(cardIA);
        }

        /// <summary>
        /// Carrega estatísticas com dados adicionais de admin.
        /// </summary>
        protected override async Task CarregarEstatisticas()
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
                    
                    if (lblResolvidosPorIA != null)
                    {
                        lblResolvidosPorIA.Text = stats.ResolvidosPorIA.ToString();
                    }
                }
            }
            catch
            {
                // Mantém valores zerados em caso de erro
            }
        }

        /// <summary>
        /// Carrega todos os chamados (não apenas os do usuário).
        /// </summary>
        protected override async Task CarregarChamados()
        {
            try
            {
                var chamados = await ChamadoService.ListarTodosAsync();
                PreencherTabelaChamados(chamados);
            }
            catch
            {
                // Tabela fica vazia em caso de erro
            }
        }

        // ========================================
        // EVENTOS DE NAVEGAÇÃO ADMIN
        // ========================================

        /// <summary>
        /// Abre a lista de todos os chamados.
        /// </summary>
        private void BtnTodosChamados_Click(object sender, EventArgs e)
        {
            var formLista = new FormListaChamados();
            formLista.ShowDialog(this);
        }

        /// <summary>
        /// Abre o formulário de aprovação de chamados.
        /// </summary>
        private void BtnAprovarChamados_Click(object sender, EventArgs e)
        {
            var formAprovar = new FormAprovarChamados();
            formAprovar.ShowDialog(this);
        }

        /// <summary>
        /// Abre o formulário de gerenciamento de FAQ.
        /// </summary>
        private void BtnFAQAdmin_Click(object sender, EventArgs e)
        {
            var formFAQAdmin = new FormFAQAdmin();
            formFAQAdmin.ShowDialog(this);
        }

        /// <summary>
        /// Abre o formulário de criação de usuário.
        /// </summary>
        private void BtnCriarUsuario_Click(object sender, EventArgs e)
        {
            var formCriar = new FormCriarUsuario();
            formCriar.ShowDialog(this);
        }
    }
}

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
            
            // ===== PADRÃO FULLSCREEN MAXIMIZADO =====
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScaleMode = AutoScaleMode.None;
        }

        // === EVENT HANDLERS ===

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // TODO: Implementar atualização dos dados
            MessageBox.Show("Atualizando dados...", "Atualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // === NAVIGATION METHODS ===

        protected void BtnNovoChamado_Click(object sender, EventArgs e)
        {
            // TODO: Abrir FormNovoChamado
        }

        protected void BtnMeusChamados_Click(object sender, EventArgs e)
        {
            // TODO: Abrir FormMeusChamados
        }

        protected void BtnFAQ_Click(object sender, EventArgs e)
        {
            // TODO: Abrir FormFAQ
        }

        protected void BtnPerfil_Click(object sender, EventArgs e)
        {
            // TODO: Abrir FormPerfil
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            // TODO: Implementar logout
        }

        protected void SetupForm()
        {
            // Setup is done in InitializeComponent
        }

        protected virtual void SetupSidebar()
        {
            // Sidebar setup is done in InitializeComponent
        }

        protected SiticoneButton CreateMenuButton(string text, int y)
        {
            return new SiticoneButton();
        }

        protected void SetMenuButtonActive(SiticoneButton button, bool active)
        {
            // TODO: Implement
        }

        protected virtual void SetupContent()
        {
            // Content setup is done in InitializeComponent
        }

        protected virtual void SetupStatsCards()
        {
            // Stats cards setup is done in InitializeComponent
        }

        protected Panel CreateStatCard(string title, string value, Color accentColor, int x, int y, int width, int height)
        {
            return new Panel();
        }

        protected virtual void SetupChamadosTable()
        {
            // Table setup is done in InitializeComponent
        }

        protected virtual async Task CarregarDados()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                // TODO: Load data
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        protected virtual async Task CarregarEstatisticas()
        {
            try
            {
                // TODO: Load statistics
            }
            catch
            {
                // Mantém os valores zerados em caso de erro
            }
        }

        protected virtual async Task CarregarChamados()
        {
            try
            {
                // TODO: Load chamados
            }
            catch
            {
                // Tabela fica vazia em caso de erro
            }
        }

        protected void PreencherTabelaChamados(List<Chamado> chamados)
        {
            // TODO: Implement
        }

        protected void DgvChamados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // TODO: Implement
        }

        protected void AbrirDetalhesChamado(int chamadoId)
        {
            // TODO: Implement
        }

        protected async Task FazerLogout()
        {
            try
            {
                // AuthService.Logout();
            }
            catch { }

            this.Hide();
            var formLogin = new FormLogin();
            formLogin.FormClosed += (s, e) => this.Close();
            formLogin.Show();
        }

        protected virtual void ApplyTheme()
        {
            // TODO: Implement theme application
        }

        private void UpdateLabelsTheme(Control.ControlCollection controls)
        {
            // TODO: Implement theme update for labels
        }
    }
}

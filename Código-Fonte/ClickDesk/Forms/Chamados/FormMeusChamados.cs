using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickDesk.Models;
using ClickDesk.Services.API;
using ClickDesk.Utils;
using Siticone.Desktop.UI.WinForms;

namespace ClickDesk.Forms.Chamados
{
    /// <summary>
    /// Formulário para listar os chamados do usuário logado.
    /// Permite filtrar e visualizar detalhes dos chamados.
    /// </summary>
    public partial class FormMeusChamados : Form
    {
        private SiticoneDataGridView dgvChamados;
        private SiticoneComboBox cmbStatus;
        private SiticoneComboBox cmbCategoria;
        private SiticoneTextBox txtBusca;
        private SiticoneButton btnBuscar;
        private SiticoneButton btnNovoChamado;
        private SiticonePanel mainPanel;
        private List<Chamado> todosChamados;

        /// <summary>
        /// Construtor do formulário.
        /// </summary>
        public FormMeusChamados()
        {
            InitializeComponent();
            SetupForm();
        }

        /// <summary>
        /// Configura o formulário.
        /// </summary>
        private void SetupForm()
        {
            this.Text = "ClickDesk - Meus Chamados";
            this.Size = new Size(1160, 750);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = ThemeManager.BackgroundApp;

            // Subscribe to theme changes
            ThemeManager.ThemeChanged += (s, e) =>
            {
                this.BackColor = ThemeManager.BackgroundApp;
                ApplyTheme();
            };

            // Main panel
            mainPanel = new SiticonePanel
            {
                Size = new Size(1100, 690),
                Location = new Point((this.Width - 1100) / 2, 30),
                FillColor = ThemeManager.CardBackground,
                BorderRadius = ClickDeskStyles.RadiusXL
            };
            mainPanel.ShadowDecoration.Enabled = true;
            mainPanel.ShadowDecoration.Depth = 20;
            this.Controls.Add(mainPanel);

            int y = 30;
            int leftMargin = 40;

            // Título
            Label lblTitle = new Label
            {
                Text = "Meus Chamados",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = ThemeManager.Brand,
                Location = new Point(leftMargin, y),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            mainPanel.Controls.Add(lblTitle);

            y += 60;

            // Filtros
            Label lblFiltros = new Label
            {
                Text = "Filtrar por:",
                Font = ClickDeskStyles.FontBase,
                ForeColor = ThemeManager.TextSecondary,
                Location = new Point(leftMargin, y + 8),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            mainPanel.Controls.Add(lblFiltros);

            // Filtro Status
            cmbStatus = new SiticoneComboBox
            {
                Size = new Size(160, 40),
                Location = new Point(leftMargin + 90, y),
                Font = ClickDeskStyles.FontBase,
                BorderRadius = ClickDeskStyles.RadiusSM,
                BorderColor = ThemeManager.Border,
                FillColor = ThemeManager.CardBackground,
                ForeColor = ThemeManager.TextPrimary
            };
            cmbStatus.Items.AddRange(new object[] { "Todos", "Aberto", "Em Andamento", "Resolvido", "Fechado", "Escalado" });
            cmbStatus.SelectedIndex = 0;
            cmbStatus.SelectedIndexChanged += (s, e) => FiltrarChamados();
            mainPanel.Controls.Add(cmbStatus);

            // Filtro Categoria
            cmbCategoria = new SiticoneComboBox
            {
                Size = new Size(160, 40),
                Location = new Point(leftMargin + 270, y),
                Font = ClickDeskStyles.FontBase,
                BorderRadius = ClickDeskStyles.RadiusSM,
                BorderColor = ThemeManager.Border,
                FillColor = ThemeManager.CardBackground,
                ForeColor = ThemeManager.TextPrimary
            };
            cmbCategoria.Items.AddRange(new object[] { "Todas", "Hardware", "Software", "Rede", "E-mail", "Sistema", "Acesso", "Impressora", "Outros" });
            cmbCategoria.SelectedIndex = 0;
            cmbCategoria.SelectedIndexChanged += (s, e) => FiltrarChamados();
            mainPanel.Controls.Add(cmbCategoria);

            // Campo de busca
            txtBusca = new SiticoneTextBox
            {
                Size = new Size(280, 40),
                Location = new Point(leftMargin + 450, y),
                Font = ClickDeskStyles.FontBase,
                BorderRadius = ClickDeskStyles.RadiusSM,
                BorderThickness = 1,
                BorderColor = ThemeManager.Border,
                FillColor = ThemeManager.CardBackground,
                ForeColor = ThemeManager.TextPrimary,
                PlaceholderText = "Buscar por título ou ID...",
                TextOffset = new Point(10, 0)
            };
            txtBusca.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) FiltrarChamados(); };
            mainPanel.Controls.Add(txtBusca);

            btnBuscar = new SiticoneButton
            {
                Text = "🔍",
                Size = new Size(45, 40),
                Location = new Point(leftMargin + 750, y),
                BorderRadius = ClickDeskStyles.RadiusSM,
                FillColor = ThemeManager.Brand,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14),
                Cursor = Cursors.Hand,
                HoverState = { FillColor = ThemeManager.BrandHover }
            };
            btnBuscar.Click += (s, e) => FiltrarChamados();
            mainPanel.Controls.Add(btnBuscar);

            // Botão Novo Chamado
            btnNovoChamado = new SiticoneButton
            {
                Text = "+ Novo Chamado",
                Size = new Size(160, 40),
                Location = new Point(leftMargin + 815, y),
                BorderRadius = ClickDeskStyles.RadiusMD,
                FillColor = ThemeManager.Brand,
                ForeColor = Color.White,
                Font = ClickDeskStyles.FontBase,
                Cursor = Cursors.Hand,
                HoverState = { FillColor = ThemeManager.BrandHover }
            };
            btnNovoChamado.Click += BtnNovoChamado_Click;
            mainPanel.Controls.Add(btnNovoChamado);

            y += 60;

            // DataGridView
            dgvChamados = new SiticoneDataGridView
            {
                Location = new Point(leftMargin, y),
                Size = new Size(1020, 540),
                BorderStyle = BorderStyle.None,
                BackgroundColor = ThemeManager.Surface,
                GridColor = ThemeManager.Border,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            };
            dgvChamados.ThemeStyle.HeaderStyle.BackColor = ThemeManager.Brand;
            dgvChamados.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvChamados.ThemeStyle.HeaderStyle.Font = ClickDeskStyles.FontBaseStrong;
            dgvChamados.ThemeStyle.AlternatingRowsStyle.BackColor = ThemeManager.Surface;
            dgvChamados.ThemeStyle.RowsStyle.BackColor = ThemeManager.CardBackground;
            dgvChamados.ThemeStyle.RowsStyle.ForeColor = ThemeManager.TextPrimary;

            dgvChamados.Columns.Add("Id", "ID");
            dgvChamados.Columns.Add("Titulo", "Título");
            dgvChamados.Columns.Add("Categoria", "Categoria");
            dgvChamados.Columns.Add("Status", "Status");
            dgvChamados.Columns.Add("Severidade", "Severidade");
            dgvChamados.Columns.Add("Data", "Data Abertura");
            dgvChamados.Columns.Add("IA", "IA");

            dgvChamados.Columns["Id"].Width = 60;
            dgvChamados.Columns["Titulo"].Width = 280;
            dgvChamados.Columns["Categoria"].Width = 120;
            dgvChamados.Columns["Status"].Width = 120;
            dgvChamados.Columns["Severidade"].Width = 100;
            dgvChamados.Columns["Data"].Width = 140;
            dgvChamados.Columns["IA"].Width = 50;

            dgvChamados.CellDoubleClick += DgvChamados_CellDoubleClick;

            mainPanel.Controls.Add(dgvChamados);

            // Carrega dados
            this.Load += async (s, e) => await CarregarChamados();
        }

        /// <summary>
        /// Aplica o tema atual aos controles do formulário.
        /// </summary>
        private void ApplyTheme()
        {
            mainPanel.FillColor = ThemeManager.CardBackground;
            txtBusca.FillColor = ThemeManager.CardBackground;
            txtBusca.ForeColor = ThemeManager.TextPrimary;
            txtBusca.BorderColor = ThemeManager.Border;
            cmbStatus.FillColor = ThemeManager.CardBackground;
            cmbStatus.ForeColor = ThemeManager.TextPrimary;
            cmbStatus.BorderColor = ThemeManager.Border;
            cmbCategoria.FillColor = ThemeManager.CardBackground;
            cmbCategoria.ForeColor = ThemeManager.TextPrimary;
            cmbCategoria.BorderColor = ThemeManager.Border;
            btnBuscar.FillColor = ThemeManager.Brand;
            btnBuscar.HoverState.FillColor = ThemeManager.BrandHover;
            btnNovoChamado.FillColor = ThemeManager.Brand;
            btnNovoChamado.HoverState.FillColor = ThemeManager.BrandHover;
            dgvChamados.BackgroundColor = ThemeManager.Surface;
            dgvChamados.GridColor = ThemeManager.Border;
            dgvChamados.ThemeStyle.HeaderStyle.BackColor = ThemeManager.Brand;
            dgvChamados.ThemeStyle.AlternatingRowsStyle.BackColor = ThemeManager.Surface;
            dgvChamados.ThemeStyle.RowsStyle.BackColor = ThemeManager.CardBackground;
            dgvChamados.ThemeStyle.RowsStyle.ForeColor = ThemeManager.TextPrimary;

            // Update all labels
            foreach (Control control in mainPanel.Controls)
            {
                if (control is Label label)
                {
                    if (label.Font.Bold)
                    {
                        label.ForeColor = ThemeManager.TextPrimary;
                    }
                    else
                    {
                        label.ForeColor = ThemeManager.TextSecondary;
                    }
                }
            }

            mainPanel.Invalidate();
            this.Invalidate();
        }

        /// <summary>
        /// Carrega os chamados do usuário.
        /// </summary>
        private async Task CarregarChamados()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                todosChamados = await ChamadoService.ListarMeusAsync();
                PreencherTabela(todosChamados);
            }
            catch (ApiException ex)
            {
                UIHelper.ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Erro ao carregar chamados: {ex.Message}");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Preenche a tabela com os chamados.
        /// </summary>
        private void PreencherTabela(List<Chamado> chamados)
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
                        chamado.DataAbertura.ToString("dd/MM/yyyy HH:mm"),
                        chamado.ResolvidoPorIA ? "✓" : ""
                    );
                }
            }
        }

        /// <summary>
        /// Filtra os chamados com base nos critérios.
        /// </summary>
        private void FiltrarChamados()
        {
            if (todosChamados == null) return;

            var filtrados = todosChamados.FindAll(c =>
            {
                // Filtro de status
                if (cmbStatus.SelectedIndex > 0)
                {
                    string statusFiltro = cmbStatus.SelectedItem.ToString().ToUpper().Replace(" ", "_");
                    if (c.Status?.ToUpper() != statusFiltro) return false;
                }

                // Filtro de categoria
                if (cmbCategoria.SelectedIndex > 0)
                {
                    if (c.Categoria != cmbCategoria.SelectedItem.ToString()) return false;
                }

                // Filtro de busca
                if (!string.IsNullOrWhiteSpace(txtBusca.Text))
                {
                    string busca = txtBusca.Text.ToLower();
                    bool match = (c.Titulo?.ToLower().Contains(busca) ?? false) ||
                                 (c.Descricao?.ToLower().Contains(busca) ?? false) ||
                                 c.Id.ToString().Contains(busca);
                    if (!match) return false;
                }

                return true;
            });

            PreencherTabela(filtrados);
        }

        /// <summary>
        /// Abre detalhes do chamado selecionado.
        /// </summary>
        private void DgvChamados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int chamadoId = Convert.ToInt32(dgvChamados.Rows[e.RowIndex].Cells["Id"].Value);
                var formDetalhes = new FormDetalhesChamado(chamadoId);
                formDetalhes.FormClosed += async (s, args) => await CarregarChamados();
                formDetalhes.ShowDialog(this);
            }
        }

        /// <summary>
        /// Abre formulário de novo chamado.
        /// </summary>
        private void BtnNovoChamado_Click(object sender, EventArgs e)
        {
            var formNovo = new FormNovoChamado();
            formNovo.FormClosed += async (s, args) => await CarregarChamados();
            formNovo.ShowDialog(this);
        }
    }
}

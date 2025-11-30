using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickDesk.Models;
using ClickDesk.Services.API;
using ClickDesk.Utils;

namespace ClickDesk.Forms.Chamados
{
    /// <summary>
    /// Formulário para listar todos os chamados (admin/tech).
    /// Exibe chamados de todos os usuários com filtros avançados.
    /// </summary>
    public partial class FormListaChamados : Form
    {
        private DataGridView dgvChamados;
        private ComboBox cmbStatus;
        private ComboBox cmbSeveridade;
        private TextBox txtBusca;
        private List<Chamado> todosChamados;

        /// <summary>
        /// Construtor do formulário.
        /// </summary>
        public FormListaChamados()
        {
            InitializeComponent();
            SetupForm();
        }

        /// <summary>
        /// Configura o formulário.
        /// </summary>
        private void SetupForm()
        {
            this.Text = "ClickDesk - Todos os Chamados";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = ClickDeskColors.BackgroundApp;

            int y = 20;
            int leftMargin = 30;

            // Título
            Label lblTitle = new Label
            {
                Text = "Todos os Chamados",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            y += 50;

            // Filtros
            Label lblFiltros = new Label
            {
                Text = "Filtrar:",
                Font = new Font("Segoe UI", 10),
                ForeColor = ClickDeskColors.Gray500,
                Location = new Point(leftMargin, y + 8),
                AutoSize = true
            };
            this.Controls.Add(lblFiltros);

            // Status
            cmbStatus = new ComboBox
            {
                Size = new Size(130, 30),
                Location = new Point(leftMargin + 60, y),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbStatus.Items.AddRange(new object[] { "Todos", "Aberto", "Em Andamento", "Resolvido", "Fechado", "Escalado" });
            cmbStatus.SelectedIndex = 0;
            cmbStatus.SelectedIndexChanged += (s, e) => FiltrarChamados();
            this.Controls.Add(cmbStatus);

            // Severidade
            cmbSeveridade = new ComboBox
            {
                Size = new Size(130, 30),
                Location = new Point(leftMargin + 210, y),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbSeveridade.Items.AddRange(new object[] { "Todas", "Baixa", "Média", "Alta", "Crítica" });
            cmbSeveridade.SelectedIndex = 0;
            cmbSeveridade.SelectedIndexChanged += (s, e) => FiltrarChamados();
            this.Controls.Add(cmbSeveridade);

            // Busca
            txtBusca = new TextBox
            {
                Size = new Size(200, 30),
                Location = new Point(leftMargin + 360, y),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle
            };
            txtBusca.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) FiltrarChamados(); };
            this.Controls.Add(txtBusca);

            var btnBuscar = UIHelper.CreatePrimaryButton("Buscar", 80, 30);
            btnBuscar.Location = new Point(leftMargin + 570, y);
            btnBuscar.Click += (s, e) => FiltrarChamados();
            this.Controls.Add(btnBuscar);

            var btnAtualizar = UIHelper.CreateSecondaryButton("🔄 Atualizar", 100, 30);
            btnAtualizar.Location = new Point(leftMargin + 660, y);
            btnAtualizar.Click += async (s, e) => await CarregarChamados();
            this.Controls.Add(btnAtualizar);

            y += 50;

            // DataGridView
            dgvChamados = new DataGridView
            {
                Location = new Point(leftMargin, y),
                Size = new Size(this.ClientSize.Width - 60, this.ClientSize.Height - y - 60),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            UIHelper.StyleDataGridView(dgvChamados);

            dgvChamados.Columns.Add("Id", "ID");
            dgvChamados.Columns.Add("Titulo", "Título");
            dgvChamados.Columns.Add("Usuario", "Usuário");
            dgvChamados.Columns.Add("Categoria", "Categoria");
            dgvChamados.Columns.Add("Status", "Status");
            dgvChamados.Columns.Add("Severidade", "Severidade");
            dgvChamados.Columns.Add("Tecnico", "Técnico");
            dgvChamados.Columns.Add("Data", "Data");

            dgvChamados.Columns["Id"].Width = 50;
            dgvChamados.Columns["Titulo"].Width = 220;
            dgvChamados.Columns["Usuario"].Width = 130;
            dgvChamados.Columns["Categoria"].Width = 100;
            dgvChamados.Columns["Status"].Width = 100;
            dgvChamados.Columns["Severidade"].Width = 90;
            dgvChamados.Columns["Tecnico"].Width = 130;
            dgvChamados.Columns["Data"].Width = 130;

            dgvChamados.CellDoubleClick += DgvChamados_CellDoubleClick;

            this.Controls.Add(dgvChamados);

            // Carrega dados
            this.Load += async (s, e) => await CarregarChamados();
        }

        /// <summary>
        /// Carrega todos os chamados.
        /// </summary>
        private async Task CarregarChamados()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                todosChamados = await ChamadoService.ListarTodosAsync();
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
                foreach (var c in chamados)
                {
                    dgvChamados.Rows.Add(
                        c.Id,
                        c.Titulo,
                        c.UsuarioNome ?? "N/A",
                        c.Categoria,
                        c.Status,
                        c.Severidade,
                        c.TecnicoNome ?? "Não atribuído",
                        c.DataAbertura.ToString("dd/MM/yyyy HH:mm")
                    );
                }
            }
        }

        /// <summary>
        /// Filtra os chamados.
        /// </summary>
        private void FiltrarChamados()
        {
            if (todosChamados == null) return;

            var filtrados = todosChamados.FindAll(c =>
            {
                if (cmbStatus.SelectedIndex > 0)
                {
                    string statusFiltro = cmbStatus.SelectedItem.ToString().ToUpper().Replace(" ", "_");
                    if (c.Status?.ToUpper() != statusFiltro) return false;
                }

                if (cmbSeveridade.SelectedIndex > 0)
                {
                    if (!c.Severidade?.Equals(cmbSeveridade.SelectedItem.ToString(), StringComparison.OrdinalIgnoreCase) ?? true)
                        return false;
                }

                if (!string.IsNullOrWhiteSpace(txtBusca.Text))
                {
                    string busca = txtBusca.Text.ToLower();
                    bool match = (c.Titulo?.ToLower().Contains(busca) ?? false) ||
                                 (c.UsuarioNome?.ToLower().Contains(busca) ?? false) ||
                                 c.Id.ToString().Contains(busca);
                    if (!match) return false;
                }

                return true;
            });

            PreencherTabela(filtrados);
        }

        /// <summary>
        /// Abre detalhes do chamado.
        /// </summary>
        private void DgvChamados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int chamadoId = Convert.ToInt32(dgvChamados.Rows[e.RowIndex].Cells["Id"].Value);
                
                // Para técnicos e admins, abre o formulário de edição técnica
                if (SessionManager.HasAdminAccess)
                {
                    var formDetalhesTecnico = new FormDetalhesChamadoTecnico(chamadoId);
                    formDetalhesTecnico.FormClosed += async (s, args) => await CarregarChamados();
                    formDetalhesTecnico.ShowDialog(this);
                }
                else
                {
                    // Para usuários comuns, abre o formulário de visualização
                    var formDetalhes = new FormDetalhesChamado(chamadoId);
                    formDetalhes.FormClosed += async (s, args) => await CarregarChamados();
                    formDetalhes.ShowDialog(this);
                }
            }
        }
    }
}

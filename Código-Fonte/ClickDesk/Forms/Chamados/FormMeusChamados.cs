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
    /// Formulário para listar os chamados do usuário logado.
    /// Permite filtrar e visualizar detalhes dos chamados.
    /// </summary>
    public partial class FormMeusChamados : Form
    {
        private DataGridView dgvChamados;
        private ComboBox cmbStatus;
        private ComboBox cmbCategoria;
        private TextBox txtBusca;
        private Button btnBuscar;
        private Button btnNovoChamado;
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
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = ClickDeskColors.ContentBackground;

            int y = 20;
            int leftMargin = 30;

            // Título
            Label lblTitle = new Label
            {
                Text = "Meus Chamados",
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
                Text = "Filtrar por:",
                Font = new Font("Segoe UI", 10),
                ForeColor = ClickDeskColors.Gray500,
                Location = new Point(leftMargin, y + 8),
                AutoSize = true
            };
            this.Controls.Add(lblFiltros);

            // Filtro Status
            cmbStatus = new ComboBox
            {
                Size = new Size(150, 30),
                Location = new Point(leftMargin + 80, y),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbStatus.Items.AddRange(new object[] { "Todos", "Aberto", "Em Andamento", "Resolvido", "Fechado", "Escalado" });
            cmbStatus.SelectedIndex = 0;
            cmbStatus.SelectedIndexChanged += (s, e) => FiltrarChamados();
            this.Controls.Add(cmbStatus);

            // Filtro Categoria
            cmbCategoria = new ComboBox
            {
                Size = new Size(150, 30),
                Location = new Point(leftMargin + 250, y),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbCategoria.Items.AddRange(new object[] { "Todas", "Hardware", "Software", "Rede", "E-mail", "Sistema", "Acesso", "Impressora", "Outros" });
            cmbCategoria.SelectedIndex = 0;
            cmbCategoria.SelectedIndexChanged += (s, e) => FiltrarChamados();
            this.Controls.Add(cmbCategoria);

            // Campo de busca
            txtBusca = new TextBox
            {
                Size = new Size(250, 30),
                Location = new Point(leftMargin + 420, y),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle
            };
            txtBusca.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) FiltrarChamados(); };
            this.Controls.Add(txtBusca);

            btnBuscar = new Button
            {
                Text = "🔍",
                Size = new Size(40, 30),
                Location = new Point(leftMargin + 680, y),
                BackColor = ClickDeskColors.Primary,
                ForeColor = ClickDeskColors.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                Cursor = Cursors.Hand
            };
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.Click += (s, e) => FiltrarChamados();
            this.Controls.Add(btnBuscar);

            // Botão Novo Chamado
            btnNovoChamado = UIHelper.CreatePrimaryButton("+ Novo Chamado", 140, 35);
            btnNovoChamado.Location = new Point(this.ClientSize.Width - 180, y - 5);
            btnNovoChamado.Click += BtnNovoChamado_Click;
            this.Controls.Add(btnNovoChamado);

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

            this.Controls.Add(dgvChamados);

            // Carrega dados
            this.Load += async (s, e) => await CarregarChamados();
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

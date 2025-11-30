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
    /// Formulário para aprovar/gerenciar chamados (admin/tech).
    /// Permite alterar status, atribuir técnicos e aprovar soluções.
    /// </summary>
    public partial class FormAprovarChamados : Form
    {
        private DataGridView dgvChamados;
        private List<Chamado> chamados;

        /// <summary>
        /// Construtor do formulário.
        /// </summary>
        public FormAprovarChamados()
        {
            InitializeComponent();
            SetupForm();
        }

        /// <summary>
        /// Configura o formulário.
        /// </summary>
        private void SetupForm()
        {
            this.Text = "ClickDesk - Aprovar Chamados";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = ClickDeskColors.ContentBackground;

            int y = 20;
            int leftMargin = 30;

            // Título
            Label lblTitle = new Label
            {
                Text = "Aprovar/Gerenciar Chamados",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            Label lblSubtitle = new Label
            {
                Text = "Gerencie o status dos chamados e atribua técnicos responsáveis",
                Font = new Font("Segoe UI", 10),
                ForeColor = ClickDeskColors.Gray500,
                Location = new Point(leftMargin, y + 35),
                AutoSize = true
            };
            this.Controls.Add(lblSubtitle);

            y += 70;

            // Botões de ação
            var btnAtribuir = UIHelper.CreatePrimaryButton("Atribuir a Mim", 140, 35);
            btnAtribuir.Location = new Point(leftMargin, y);
            btnAtribuir.Click += BtnAtribuir_Click;
            this.Controls.Add(btnAtribuir);

            var btnResolver = UIHelper.CreateSuccessButton("Marcar Resolvido", 150, 35);
            btnResolver.Location = new Point(leftMargin + 160, y);
            btnResolver.Click += BtnResolver_Click;
            this.Controls.Add(btnResolver);

            var btnEscalar = UIHelper.CreateWarningButton("Escalar", 100, 35);
            btnEscalar.Location = new Point(leftMargin + 330, y);
            btnEscalar.Click += BtnEscalar_Click;
            this.Controls.Add(btnEscalar);

            var btnFechar = UIHelper.CreateDangerButton("Fechar", 100, 35);
            btnFechar.Location = new Point(leftMargin + 450, y);
            btnFechar.Click += BtnFechar_Click;
            this.Controls.Add(btnFechar);

            var btnAtualizar = UIHelper.CreateSecondaryButton("🔄 Atualizar", 110, 35);
            btnAtualizar.Location = new Point(this.ClientSize.Width - 150, y);
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
            dgvChamados.Columns["Usuario"].Width = 120;
            dgvChamados.Columns["Categoria"].Width = 100;
            dgvChamados.Columns["Status"].Width = 100;
            dgvChamados.Columns["Severidade"].Width = 90;
            dgvChamados.Columns["Tecnico"].Width = 120;
            dgvChamados.Columns["Data"].Width = 130;

            dgvChamados.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    int id = Convert.ToInt32(dgvChamados.Rows[e.RowIndex].Cells["Id"].Value);
                    var form = new FormDetalhesChamado(id);
                    form.FormClosed += async (sender, args) => await CarregarChamados();
                    form.ShowDialog(this);
                }
            };

            this.Controls.Add(dgvChamados);

            // Carrega dados
            this.Load += async (s, e) => await CarregarChamados();
        }

        /// <summary>
        /// Carrega os chamados pendentes.
        /// </summary>
        private async Task CarregarChamados()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                chamados = await ChamadoService.ListarTodosAsync();

                // Filtra chamados que precisam de ação (não fechados)
                var pendentes = chamados?.FindAll(c =>
                    c.Status?.ToUpper() != "FECHADO" && c.Status?.ToUpper() != "RESOLVIDO"
                );

                PreencherTabela(pendentes);
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
        /// Preenche a tabela.
        /// </summary>
        private void PreencherTabela(List<Chamado> lista)
        {
            dgvChamados.Rows.Clear();

            if (lista != null)
            {
                foreach (var c in lista)
                {
                    dgvChamados.Rows.Add(
                        c.Id,
                        c.Titulo,
                        c.UsuarioNome ?? "N/A",
                        c.Categoria,
                        c.Status,
                        c.Severidade,
                        c.TecnicoNome ?? "-",
                        c.DataAbertura.ToString("dd/MM/yyyy HH:mm")
                    );
                }
            }
        }

        /// <summary>
        /// Obtém o ID do chamado selecionado.
        /// </summary>
        private int? GetSelectedChamadoId()
        {
            if (dgvChamados.SelectedRows.Count > 0)
            {
                return Convert.ToInt32(dgvChamados.SelectedRows[0].Cells["Id"].Value);
            }

            UIHelper.ShowWarning("Selecione um chamado na lista.");
            return null;
        }

        /// <summary>
        /// Atribui o chamado ao técnico logado.
        /// </summary>
        private async void BtnAtribuir_Click(object sender, EventArgs e)
        {
            var id = GetSelectedChamadoId();
            if (!id.HasValue) return;

            try
            {
                var request = new StatusUpdateRequest
                {
                    Status = "EM_ANDAMENTO",
                    TecnicoId = SessionManager.CurrentUser?.Id
                };

                await ChamadoService.AtualizarStatusAsync(id.Value, request);
                UIHelper.ShowSuccess("Chamado atribuído a você com sucesso!");
                await CarregarChamados();
            }
            catch (ApiException ex)
            {
                UIHelper.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Marca o chamado como resolvido.
        /// </summary>
        private async void BtnResolver_Click(object sender, EventArgs e)
        {
            var id = GetSelectedChamadoId();
            if (!id.HasValue) return;

            if (!UIHelper.ShowConfirmation("Marcar este chamado como resolvido?"))
                return;

            try
            {
                var request = new StatusUpdateRequest
                {
                    Status = "RESOLVIDO"
                };

                await ChamadoService.AtualizarStatusAsync(id.Value, request);
                UIHelper.ShowSuccess("Chamado marcado como resolvido!");
                await CarregarChamados();
            }
            catch (ApiException ex)
            {
                UIHelper.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Escala o chamado.
        /// </summary>
        private async void BtnEscalar_Click(object sender, EventArgs e)
        {
            var id = GetSelectedChamadoId();
            if (!id.HasValue) return;

            try
            {
                var request = new StatusUpdateRequest
                {
                    Status = "ESCALADO",
                    Motivo = "Escalado pelo técnico"
                };

                await ChamadoService.AtualizarStatusAsync(id.Value, request);
                UIHelper.ShowSuccess("Chamado escalado!");
                await CarregarChamados();
            }
            catch (ApiException ex)
            {
                UIHelper.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Fecha o chamado.
        /// </summary>
        private async void BtnFechar_Click(object sender, EventArgs e)
        {
            var id = GetSelectedChamadoId();
            if (!id.HasValue) return;

            if (!UIHelper.ShowConfirmation("Fechar este chamado definitivamente?"))
                return;

            try
            {
                var request = new StatusUpdateRequest
                {
                    Status = "FECHADO"
                };

                await ChamadoService.AtualizarStatusAsync(id.Value, request);
                UIHelper.ShowSuccess("Chamado fechado!");
                await CarregarChamados();
            }
            catch (ApiException ex)
            {
                UIHelper.ShowError(ex.Message);
            }
        }
    }
}

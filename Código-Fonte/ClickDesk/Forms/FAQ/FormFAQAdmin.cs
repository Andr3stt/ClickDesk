using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickDesk.Models;
using ClickDesk.Services.API;
using ClickDesk.Utils;

namespace ClickDesk.Forms.FAQ
{
    /// <summary>
    /// Formulário para gerenciamento de FAQs (admin).
    /// Permite criar, editar e excluir perguntas frequentes.
    /// </summary>
    public partial class FormFAQAdmin : Form
    {
        private DataGridView dgvFAQs;
        private List<Models.FAQ> faqs;

        /// <summary>
        /// Construtor do formulário.
        /// </summary>
        public FormFAQAdmin()
        {
            InitializeComponent();
            SetupForm();
        }

        /// <summary>
        /// Configura o formulário.
        /// </summary>
        private void SetupForm()
        {
            this.Text = "ClickDesk - Gerenciar FAQ";
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = AppColors.ContentBackground;

            int y = 20;
            int leftMargin = 30;

            // Título
            Label lblTitle = new Label
            {
                Text = "📚 Gerenciar FAQ",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = AppColors.TextPrimary,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            y += 50;

            // Botões de ação
            var btnNova = UIHelper.CreatePrimaryButton("+ Nova FAQ", 130, 35);
            btnNova.Location = new Point(leftMargin, y);
            btnNova.Click += BtnNova_Click;
            this.Controls.Add(btnNova);

            var btnEditar = UIHelper.CreateSecondaryButton("✏️ Editar", 100, 35);
            btnEditar.Location = new Point(leftMargin + 150, y);
            btnEditar.Click += BtnEditar_Click;
            this.Controls.Add(btnEditar);

            var btnExcluir = UIHelper.CreateDangerButton("🗑️ Excluir", 100, 35);
            btnExcluir.Location = new Point(leftMargin + 270, y);
            btnExcluir.Click += BtnExcluir_Click;
            this.Controls.Add(btnExcluir);

            var btnAtualizar = UIHelper.CreateSecondaryButton("🔄", 50, 35);
            btnAtualizar.Location = new Point(this.ClientSize.Width - 90, y);
            btnAtualizar.Click += async (s, e) => await CarregarFAQs();
            this.Controls.Add(btnAtualizar);

            y += 50;

            // DataGridView
            dgvFAQs = new DataGridView
            {
                Location = new Point(leftMargin, y),
                Size = new Size(this.ClientSize.Width - 60, this.ClientSize.Height - y - 60),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            UIHelper.StyleDataGridView(dgvFAQs);

            dgvFAQs.Columns.Add("Id", "ID");
            dgvFAQs.Columns.Add("Pergunta", "Pergunta");
            dgvFAQs.Columns.Add("Categoria", "Categoria");
            dgvFAQs.Columns.Add("Visualizacoes", "Views");
            dgvFAQs.Columns.Add("Ativa", "Ativa");
            dgvFAQs.Columns.Add("DataCriacao", "Data Criação");

            dgvFAQs.Columns["Id"].Width = 50;
            dgvFAQs.Columns["Pergunta"].Width = 450;
            dgvFAQs.Columns["Categoria"].Width = 120;
            dgvFAQs.Columns["Visualizacoes"].Width = 80;
            dgvFAQs.Columns["Ativa"].Width = 60;
            dgvFAQs.Columns["DataCriacao"].Width = 140;

            dgvFAQs.CellDoubleClick += (s, e) => BtnEditar_Click(s, e);

            this.Controls.Add(dgvFAQs);

            // Carrega dados
            this.Load += async (s, e) => await CarregarFAQs();
        }

        /// <summary>
        /// Carrega as FAQs.
        /// </summary>
        private async Task CarregarFAQs()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                faqs = await FAQService.ListarAsync();
                PreencherTabela();
            }
            catch (ApiException ex)
            {
                UIHelper.ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Erro ao carregar FAQs: {ex.Message}");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Preenche a tabela.
        /// </summary>
        private void PreencherTabela()
        {
            dgvFAQs.Rows.Clear();

            if (faqs != null)
            {
                foreach (var f in faqs)
                {
                    dgvFAQs.Rows.Add(
                        f.Id,
                        f.Pergunta,
                        f.Categoria,
                        f.Visualizacoes,
                        f.Ativa ? "Sim" : "Não",
                        f.DataCriacao.ToString("dd/MM/yyyy HH:mm")
                    );
                }
            }
        }

        /// <summary>
        /// Abre formulário para nova FAQ.
        /// </summary>
        private void BtnNova_Click(object sender, EventArgs e)
        {
            var form = CriarFormularioFAQ(null);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _ = CarregarFAQs();
            }
        }

        /// <summary>
        /// Abre formulário para editar FAQ.
        /// </summary>
        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvFAQs.SelectedRows.Count == 0)
            {
                UIHelper.ShowWarning("Selecione uma FAQ para editar.");
                return;
            }

            int id = Convert.ToInt32(dgvFAQs.SelectedRows[0].Cells["Id"].Value);
            var faq = faqs.Find(f => f.Id == id);

            if (faq != null)
            {
                var form = CriarFormularioFAQ(faq);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    _ = CarregarFAQs();
                }
            }
        }

        /// <summary>
        /// Exclui a FAQ selecionada.
        /// </summary>
        private async void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvFAQs.SelectedRows.Count == 0)
            {
                UIHelper.ShowWarning("Selecione uma FAQ para excluir.");
                return;
            }

            if (!UIHelper.ShowConfirmation("Deseja realmente excluir esta FAQ?"))
                return;

            int id = Convert.ToInt32(dgvFAQs.SelectedRows[0].Cells["Id"].Value);

            try
            {
                await FAQService.ExcluirAsync(id);
                UIHelper.ShowSuccess("FAQ excluída com sucesso!");
                await CarregarFAQs();
            }
            catch (ApiException ex)
            {
                UIHelper.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Cria o formulário de criação/edição de FAQ.
        /// </summary>
        private Form CriarFormularioFAQ(Models.FAQ faq)
        {
            var form = new Form
            {
                Text = faq == null ? "Nova FAQ" : "Editar FAQ",
                Size = new Size(600, 500),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = AppColors.White,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            int y = 20;
            int leftMargin = 30;

            // Pergunta
            var lblPergunta = new Label
            {
                Text = "Pergunta *",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = AppColors.Gray700,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            form.Controls.Add(lblPergunta);

            var txtPergunta = new TextBox
            {
                Size = new Size(520, 60),
                Location = new Point(leftMargin, y + 25),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                Multiline = true,
                Text = faq?.Pergunta ?? ""
            };
            form.Controls.Add(txtPergunta);

            y += 100;

            // Resposta
            var lblResposta = new Label
            {
                Text = "Resposta *",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = AppColors.Gray700,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            form.Controls.Add(lblResposta);

            var txtResposta = new TextBox
            {
                Size = new Size(520, 120),
                Location = new Point(leftMargin, y + 25),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Text = faq?.Resposta ?? ""
            };
            form.Controls.Add(txtResposta);

            y += 160;

            // Categoria
            var lblCategoria = new Label
            {
                Text = "Categoria",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = AppColors.Gray700,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            form.Controls.Add(lblCategoria);

            var cmbCategoria = new ComboBox
            {
                Size = new Size(200, 30),
                Location = new Point(leftMargin, y + 25),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbCategoria.Items.AddRange(new object[] { "Hardware", "Software", "Rede", "E-mail", "Sistema", "Acesso", "Outros" });
            cmbCategoria.SelectedItem = faq?.Categoria ?? "Outros";
            form.Controls.Add(cmbCategoria);

            // Ativa
            var chkAtiva = new CheckBox
            {
                Text = "Ativa",
                Font = new Font("Segoe UI", 10),
                ForeColor = AppColors.Gray700,
                Location = new Point(leftMargin + 250, y + 27),
                Checked = faq?.Ativa ?? true,
                AutoSize = true
            };
            form.Controls.Add(chkAtiva);

            y += 80;

            // Palavras-chave
            var lblPalavras = new Label
            {
                Text = "Palavras-chave (separadas por vírgula)",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = AppColors.Gray700,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            form.Controls.Add(lblPalavras);

            var txtPalavras = new TextBox
            {
                Size = new Size(520, 30),
                Location = new Point(leftMargin, y + 25),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                Text = faq?.PalavrasChave ?? ""
            };
            form.Controls.Add(txtPalavras);

            y += 80;

            // Botões
            var btnCancelar = UIHelper.CreateSecondaryButton("Cancelar", 120, 40);
            btnCancelar.Location = new Point(leftMargin + 280, y);
            btnCancelar.Click += (s, e) => form.Close();
            form.Controls.Add(btnCancelar);

            var btnSalvar = UIHelper.CreatePrimaryButton("Salvar", 120, 40);
            btnSalvar.Location = new Point(leftMargin + 420, y);
            btnSalvar.Click += async (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtPergunta.Text) || string.IsNullOrWhiteSpace(txtResposta.Text))
                {
                    UIHelper.ShowError("Preencha a pergunta e a resposta.");
                    return;
                }

                try
                {
                    var novaFaq = new Models.FAQ
                    {
                        Id = faq?.Id ?? 0,
                        Pergunta = txtPergunta.Text.Trim(),
                        Resposta = txtResposta.Text.Trim(),
                        Categoria = cmbCategoria.SelectedItem?.ToString(),
                        PalavrasChave = txtPalavras.Text.Trim(),
                        Ativa = chkAtiva.Checked
                    };

                    if (faq == null)
                    {
                        await FAQService.CriarAsync(novaFaq);
                        UIHelper.ShowSuccess("FAQ criada com sucesso!");
                    }
                    else
                    {
                        await FAQService.AtualizarAsync(faq.Id, novaFaq);
                        UIHelper.ShowSuccess("FAQ atualizada com sucesso!");
                    }

                    form.DialogResult = DialogResult.OK;
                    form.Close();
                }
                catch (ApiException ex)
                {
                    UIHelper.ShowError(ex.Message);
                }
            };
            form.Controls.Add(btnSalvar);

            return form;
        }
    }
}

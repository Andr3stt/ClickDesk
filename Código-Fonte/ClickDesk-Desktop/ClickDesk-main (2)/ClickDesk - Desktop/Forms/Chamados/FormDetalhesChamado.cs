using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickDesk.Models;
using ClickDesk.Services.API;
using ClickDesk.Utils;
using ClickDesk.Forms;
using Siticone.Desktop.UI.WinForms;

namespace ClickDesk.Forms.Chamados
{
    /// <summary>
    /// Formulário para visualizar detalhes de um chamado.
    /// Interface moderna e clean seguindo o padrão Web.
    /// </summary>
    public partial class FormDetalhesChamado : Form
    {
        private int chamadoId;
        private Chamado chamado;

        // Controles principais
        private SiticoneHtmlLabel lblId;
        private SiticoneHtmlLabel lblStatus;
        private SiticoneHtmlLabel lblDate;
        private SiticoneHtmlLabel lblName;
        private SiticoneHtmlLabel lblDept;
        private SiticoneHtmlLabel lblCategory;
        private SiticoneHtmlLabel lblSubject;
        private SiticoneHtmlLabel lblDescription;
        private SiticoneButton btnVoltar;
        private SiticoneButton btnAtualizar;

        /// <summary>
        /// Construtor do formulário.
        /// </summary>
        /// <param name="id">ID do chamado a ser exibido</param>
        public FormDetalhesChamado(int id)
        {
            chamadoId = id;
            InitializeComponent();
            
            // ===== PADRÃO FULLSCREEN MAXIMIZADO =====
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = StyleManager.BgPage;
            
            WireEvents();
        }

        /// <summary>
        /// Conecta os eventos dos controles.
        /// </summary>
        private void WireEvents()
        {
            this.Load += async (s, e) => await CarregarChamado();
            btnVoltar.Click += (s, e) => this.Close();
            btnAtualizar.Click += async (s, e) => await CarregarChamado();
        }

        /// <summary>
        /// Carrega os dados do chamado.
        /// </summary>
        private async Task CarregarChamado()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnAtualizar.Enabled = false;
                btnAtualizar.Text = "Carregando...";

                chamado = await ChamadoService.GetByIdAsync(chamadoId);

                if (chamado != null)
                {
                    PreencherDados();
                }
                else
                {
                    UIHelper.ShowError("Chamado não encontrado.");
                    this.Close();
                }
            }
            catch (ApiException ex)
            {
                UIHelper.ShowError(ex.Message);
                this.Close();
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Erro ao carregar chamado: {ex.Message}");
                this.Close();
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnAtualizar.Enabled = true;
                btnAtualizar.Text = "Atualizar";
            }
        }

        /// <summary>
        /// Preenche os dados do chamado na tela.
        /// </summary>
        private void PreencherDados()
        {
            this.Text = $"ClickDesk - Chamado CD-{chamado.Id:D4}";

            // Preenche badge do ID
            lblId.Text = $"<font face='Poppins' size='11' color='#F28A1A'><b>CD-{chamado.Id:D4}</b></font>";

            // Preenche status com cor dinâmica
            Color statusColor = GetStatusColor(chamado.Status);
            string statusColorHex = ColorTranslator.ToHtml(statusColor);
            lblStatus.Text = $"<font face='Poppins' size='10' color='{statusColorHex}'><b>{chamado.Status?.ToUpper() ?? "DESCONHECIDO"}</b></font>";

            // Preenche data
            lblDate.Text = $"<font face='Poppins' size='10' color='#6F6F6F'>📅 {chamado.DataAbertura:dd/MM/yyyy}</font>";

            // Preenche informações dos campos
            lblName.Text = $"<font face='Poppins' size='10' color='#1E2A22'>{chamado.UsuarioNome ?? "N/A"}</font>";
            lblDept.Text = $"<font face='Poppins' size='10' color='#1E2A22'>Suporte</font>";
            lblCategory.Text = $"<font face='Poppins' size='10' color='#1E2A22'>{chamado.Categoria ?? "N/A"}</font>";
            lblSubject.Text = $"<font face='Poppins' size='10' color='#1E2A22'>{chamado.Titulo ?? "N/A"}</font>";
            lblDescription.Text = $"<font face='Poppins' size='10' color='#1E2A22'>{chamado.Descricao ?? "N/A"}</font>";
        }

        /// <summary>
        /// Retorna a cor do status baseada no valor.
        /// </summary>
        private Color GetStatusColor(string status)
        {
            if (string.IsNullOrEmpty(status))
                return StyleManager.TextSubtle;

            switch (status.ToUpper())
            {
                case "ABERTO":
                    return ColorTranslator.FromHtml("#2563eb"); // Azul
                case "EM_ANDAMENTO":
                    return ColorTranslator.FromHtml("#f59e0b"); // Amarelo
                case "RESOLVIDO":
                    return ColorTranslator.FromHtml("#10b981"); // Verde
                case "FECHADO":
                    return ColorTranslator.FromHtml("#6b7280"); // Cinza
                case "ESCALADO":
                    return ColorTranslator.FromHtml("#ef4444"); // Vermelho
                default:
                    return StyleManager.TextSubtle;
            }
        }
    }
}

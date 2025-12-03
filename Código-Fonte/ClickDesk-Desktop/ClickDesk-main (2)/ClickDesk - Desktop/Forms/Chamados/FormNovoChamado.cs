using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ClickDesk.Forms.Chamados
{
    public partial class FormNovoChamado : Form
    {
        private readonly List<FileInfo> _anexos = new List<FileInfo>();

        public FormNovoChamado()
        {
            InitializeComponent();
            ConfigurarComboBoxes();
            ConfigurarDragDrop();
        }

        private void ConfigurarComboBoxes()
        {
            // Ajuste os itens conforme sua regra de negócio
            cboCategoria.Items.Clear();
            cboCategoria.Items.AddRange(new object[]
            {
                "Hardware",
                "Software",
                "Rede",
                "Acesso",
                "Outros"
            });

            cboDepartamento.Items.Clear();
            cboDepartamento.Items.AddRange(new object[]
            {
                "TI",
                "RH",
                "Financeiro",
                "Comercial",
                "Suporte"
            });
        }

        private void ConfigurarDragDrop()
        {
            panelDropzone.AllowDrop = true;
            panelDropzone.DragEnter += PanelDropzone_DragEnter;
            panelDropzone.DragLeave += PanelDropzone_DragLeave;
            panelDropzone.DragDrop += PanelDropzone_DragDrop;
        }

        #region Eventos de layout / desenho

        private void PanelSidebar_Paint(object sender, PaintEventArgs e)
        {
            // Gradiente suave igual à web
            using (var brush = new LinearGradientBrush(
                       panelSidebar.ClientRectangle,
                       Color.FromArgb(242, 238, 231),
                       Color.FromArgb(239, 234, 226),
                       LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, panelSidebar.ClientRectangle);
            }
        }

        private void PanelCard_Paint(object sender, PaintEventArgs e)
        {
            // Bordas levemente arredondadas
            var rect = panelCard.ClientRectangle;
            rect.Inflate(-1, -1);

            using (var path = CriarRoundedRect(rect, 16))
            using (var pen = new Pen(Color.FromArgb(213, 208, 199), 1))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(pen, path);
            }
        }

        private GraphicsPath CriarRoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();

            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void FormNovoChamado_Resize(object sender, EventArgs e)
        {
            panelCard.Invalidate();
        }

        #endregion

        #region Anexos (simulação visual)

        private void PanelDropzone_Click(object sender, EventArgs e)
        {
            if (openFileDialogAnexos.ShowDialog(this) == DialogResult.OK)
            {
                AdicionarArquivos(openFileDialogAnexos.FileNames);
            }
        }

        private void PanelDropzone_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                panelDropzone.BackColor = Color.FromArgb(240, 248, 255);
                panelDropzone.BorderStyle = BorderStyle.Fixed3D;
            }
        }

        private void PanelDropzone_DragLeave(object sender, EventArgs e)
        {
            ResetDropzoneVisual();
        }

        private void PanelDropzone_DragDrop(object sender, DragEventArgs e)
        {
            ResetDropzoneVisual();

            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                AdicionarArquivos(paths);
            }
        }

        private void ResetDropzoneVisual()
        {
            panelDropzone.BackColor = Color.FromArgb(248, 249, 250);
            panelDropzone.BorderStyle = BorderStyle.FixedSingle;
        }

        private void AdicionarArquivos(IEnumerable<string> paths)
        {
            const long maxBytes = 10 * 1024 * 1024; // 10MB
            string[] extensoesPermitidas =
            {
                ".jpg", ".jpeg", ".png", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".zip"
            };

            foreach (var p in paths)
            {
                try
                {
                    var info = new FileInfo(p);
                    if (!info.Exists) continue;

                    if (info.Length > maxBytes)
                    {
                        MessageBox.Show(
                            $"O arquivo \"{info.Name}\" é muito grande. Máximo 10MB.",
                            "Anexos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        continue;
                    }

                    var ext = info.Extension.ToLowerInvariant();
                    if (Array.IndexOf(extensoesPermitidas, ext) < 0)
                    {
                        MessageBox.Show(
                            $"Tipo de arquivo \"{ext}\" não é permitido.",
                            "Anexos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        continue;
                    }

                    if (_anexos.Exists(a => string.Equals(a.FullName, info.FullName, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show(
                            $"O arquivo \"{info.Name}\" já foi adicionado.",
                            "Anexos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        continue;
                    }

                    _anexos.Add(info);
                    AdicionarChipAnexo(info);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Erro ao adicionar arquivo: {ex.Message}",
                        "Anexos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void AdicionarChipAnexo(FileInfo info)
        {
            var panel = new Panel
            {
                Height = 28,
                Width = flowAnexos.ClientSize.Width - 25,
                BackColor = Color.FromArgb(240, 240, 240),
                Margin = new Padding(0, 0, 0, 4),
                Tag = info
            };

            var lbl = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                Text = $"{info.Name} ({FormatarTamanho(info.Length)})"
            };

            var btnRemove = new Button
            {
                Text = "x",
                Dock = DockStyle.Right,
                Width = 24,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(220, 38, 38),
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                TabStop = false
            };
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.Click += (s, e) => RemoverAnexo(panel);

            panel.Controls.Add(btnRemove);
            panel.Controls.Add(lbl);
            flowAnexos.Controls.Add(panel);
        }

        private void RemoverAnexo(Panel chipPanel)
        {
            if (chipPanel.Tag is FileInfo info)
            {
                _anexos.RemoveAll(a =>
                    string.Equals(a.FullName, info.FullName, StringComparison.OrdinalIgnoreCase));
            }

            flowAnexos.Controls.Remove(chipPanel);
            chipPanel.Dispose();
        }

        private static string FormatarTamanho(long bytes)
        {
            if (bytes == 0) return "0 B";
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }

        #endregion

        #region Botões

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnCriar_Click(object sender, EventArgs e)
        {
            string titulo = txtTitulo.Text.Trim();
            string categoria = cboCategoria.SelectedItem?.ToString() ?? string.Empty;
            string departamento = cboDepartamento.SelectedItem?.ToString() ?? string.Empty;
            string descricao = txtDescricao.Text.Trim();

            if (string.IsNullOrWhiteSpace(titulo) ||
                string.IsNullOrWhiteSpace(categoria) ||
                string.IsNullOrWhiteSpace(departamento) ||
                string.IsNullOrWhiteSpace(descricao))
            {
                MessageBox.Show(
                    "Por favor, preencha todos os campos obrigatórios (*).",
                    "Novo Chamado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Aqui você colocaria a lógica real de criação do chamado
            MessageBox.Show(
                "Chamado criado com sucesso!\n\nA prioridade será definida automaticamente pela IA.",
                "Novo Chamado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            this.Close();
        }

        #endregion
    }
}

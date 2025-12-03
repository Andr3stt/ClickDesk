using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ClickDesk
{
    public partial class FormEditarPerfil : Form
    {
        private string fotoBase64 = "";

        public FormEditarPerfil()
        {
            InitializeComponent();
            AplicarTema();
        }

        private void AplicarTema()
        {
            this.BackColor = ColorTranslator.FromHtml("#EDE6D8");
            panelSidebar.BackColor = ColorTranslator.FromHtml("#F8F3EB");
            panelTop.BackColor = Color.White;
        }

        private void btnAlterarFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.gif";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(dlg.FileName);
                    picFoto.Image = img;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms, img.RawFormat);
                        fotoBase64 = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        private void btnRemoverFoto_Click(object sender, EventArgs e)
        {
            picFoto.Image = null;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Alterações salvas com sucesso!", "ClickDesk", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}

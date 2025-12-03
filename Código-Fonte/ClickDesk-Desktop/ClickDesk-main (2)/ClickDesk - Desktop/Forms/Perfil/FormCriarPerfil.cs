using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClickDesk
{
    public partial class FormCriarPerfil : Form
    {
        public FormCriarPerfil()
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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Perfil criado com sucesso!", "ClickDesk",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

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

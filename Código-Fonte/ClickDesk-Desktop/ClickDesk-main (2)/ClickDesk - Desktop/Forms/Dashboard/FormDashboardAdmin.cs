using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClickDesk.Forms.Dashboard
{
    public partial class FormDashboardAdmin : Form
    {
        public FormDashboardAdmin()
        {
            InitializeComponent();
        }

        private void FormDashboardAdmin_Load(object sender, EventArgs e)
        {
            this.UpdateLastUpdatedTime();
        }

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
            // Reload dashboard data
            this.UpdateLastUpdatedTime();
            MessageBox.Show("Dashboard atualizado!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cboPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Atualiza os dados baseado no período selecionado
            string periodo = this.cboPeriodo.SelectedItem.ToString();
            // Aqui você implementaria a lógica para carregar dados do período
        }

        private void UpdateLastUpdatedTime()
        {
            this.lblLastUpdated.Text = "Atualizado às " + DateTime.Now.ToString("HH:mm");
        }
    }
}

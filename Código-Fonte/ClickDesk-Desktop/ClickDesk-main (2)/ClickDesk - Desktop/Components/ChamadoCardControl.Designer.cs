using System.Drawing;
using System.Windows.Forms;

namespace ClickDesk.Components
{
    partial class ChamadoCardControl
    {
        private Label lblId;
        private Label lblTitulo;
        private Label lblDescricao;
        private Label lblCategoria;
        private Label lblData;
        private Label lblPrioridade;
        private Label status;
        private Button btnAprovar;
        private Button btnRecusar;

        private void InitializeComponent()
        {
            this.lblId = new Label();
            this.lblTitulo = new Label();
            this.lblDescricao = new Label();
            this.lblCategoria = new Label();
            this.lblData = new Label();
            this.lblPrioridade = new Label();
            this.status = new Label();
            this.btnAprovar = new Button();
            this.btnRecusar = new Button();

            this.SuspendLayout();

            lblId.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblId.BackColor = Color.FromArgb(242, 138, 26);
            lblId.ForeColor = Color.White;
            lblId.Padding = new Padding(10, 5, 10, 5);
            lblId.AutoSize = true;
            lblId.Location = new Point(20, 15);

            status.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            status.Padding = new Padding(10, 6, 10, 6);
            status.AutoSize = true;
            status.Location = new Point(200, 15);

            lblTitulo.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 60);

            lblDescricao.Font = new Font("Segoe UI", 10);
            lblDescricao.ForeColor = Color.Gray;
            lblDescricao.AutoSize = true;
            lblDescricao.MaximumSize = new Size(560, 0);
            lblDescricao.Location = new Point(20, 100);

            lblCategoria.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblCategoria.AutoSize = true;
            lblCategoria.Location = new Point(20, 150);

            lblData.Font = new Font("Segoe UI", 9);
            lblData.AutoSize = true;
            lblData.Location = new Point(120, 150);

            lblPrioridade.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblPrioridade.AutoSize = true;
            lblPrioridade.Location = new Point(240, 150);

            btnAprovar.Text = "✓ APROVAR";
            btnAprovar.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnAprovar.BackColor = Color.FromArgb(242, 138, 26);
            btnAprovar.ForeColor = Color.White;
            btnAprovar.FlatStyle = FlatStyle.Flat;
            btnAprovar.Location = new Point(20, 200);
            btnAprovar.Size = new Size(150, 40);

            btnRecusar.Text = "✕ RECUSAR";
            btnRecusar.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnRecusar.BackColor = Color.FromArgb(220, 38, 38);
            btnRecusar.ForeColor = Color.White;
            btnRecusar.FlatStyle = FlatStyle.Flat;
            btnRecusar.Location = new Point(190, 200);
            btnRecusar.Size = new Size(150, 40);

            this.Controls.Add(lblId);
            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblDescricao);
            this.Controls.Add(lblCategoria);
            this.Controls.Add(lblData);
            this.Controls.Add(lblPrioridade);
            this.Controls.Add(status);
            this.Controls.Add(btnAprovar);
            this.Controls.Add(btnRecusar);

            this.Size = new Size(600, 270);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

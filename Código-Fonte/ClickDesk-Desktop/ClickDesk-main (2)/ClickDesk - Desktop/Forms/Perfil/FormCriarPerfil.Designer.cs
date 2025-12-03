using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClickDesk
{
    partial class FormCriarPerfil
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelSidebar;
        private Panel panelTop;

        private Button btnFechar;
        private Button btnMinimizar;
        private Label lblTitulo;

        private TextBox txtNome;
        private TextBox txtSenhaAtual;
        private TextBox txtNovaSenha;
        private TextBox txtConfSenha;
        private TextBox txtEmail;
        private ComboBox cmbDepartamento;

        private Button btnSalvar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            panelSidebar = new Panel();
            panelTop = new Panel();

            lblTitulo = new Label();
            btnFechar = new Button();
            btnMinimizar = new Button();

            txtNome = new TextBox();
            txtSenhaAtual = new TextBox();
            txtNovaSenha = new TextBox();
            txtConfSenha = new TextBox();
            txtEmail = new TextBox();
            cmbDepartamento = new ComboBox();

            btnSalvar = new Button();

            SuspendLayout();

            // FORM
            this.ClientSize = new Size(1000, 650);
            this.FormBorderStyle = FormBorderStyle.None;

            // SIDEBAR
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Width = 220;
            panelSidebar.BackColor = ColorTranslator.FromHtml("#F8F3EB");
            Controls.Add(panelSidebar);

            // TOP BAR
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 60;
            panelTop.BackColor = Color.White;

            lblTitulo.Text = "Criar Perfil";
            lblTitulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitulo.Location = new Point(240, 15);

            btnFechar.Text = "X";
            btnFechar.FlatStyle = FlatStyle.Flat;
            btnFechar.FlatAppearance.BorderSize = 0;
            btnFechar.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnFechar.Location = new Point(950, 15);
            btnFechar.Click += btnFechar_Click;

            btnMinimizar.Text = "_";
            btnMinimizar.FlatStyle = FlatStyle.Flat;
            btnMinimizar.FlatAppearance.BorderSize = 0;
            btnMinimizar.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnMinimizar.Location = new Point(910, 15);
            btnMinimizar.Click += btnMinimizar_Click;

            panelTop.Controls.Add(lblTitulo);
            panelTop.Controls.Add(btnFechar);
            panelTop.Controls.Add(btnMinimizar);
            Controls.Add(panelTop);

            // CAMPOS DO FORMULÁRIO
            txtNome.Location = new Point(260, 120);
            txtNome.Width = 300;

            txtSenhaAtual.Location = new Point(260, 180);
            txtSenhaAtual.Width = 300;
            txtSenhaAtual.PasswordChar = '*';

            txtNovaSenha.Location = new Point(260, 240);
            txtNovaSenha.Width = 300;
            txtNovaSenha.PasswordChar = '*';

            txtConfSenha.Location = new Point(260, 300);
            txtConfSenha.Width = 300;
            txtConfSenha.PasswordChar = '*';

            txtEmail.Location = new Point(260, 360);
            txtEmail.Width = 300;

            cmbDepartamento.Location = new Point(260, 420);
            cmbDepartamento.Width = 300;
            cmbDepartamento.Items.AddRange(new object[] {
                "Tecnologia",
                "Suporte",
                "Vendas",
                "Financeiro"
            });

            Controls.Add(txtNome);
            Controls.Add(txtSenhaAtual);
            Controls.Add(txtNovaSenha);
            Controls.Add(txtConfSenha);
            Controls.Add(txtEmail);
            Controls.Add(cmbDepartamento);

            // BOTÃO SALVAR
            btnSalvar.Text = "Salvar";
            btnSalvar.BackColor = ColorTranslator.FromHtml("#F28A1A");
            btnSalvar.ForeColor = Color.White;
            btnSalvar.FlatStyle = FlatStyle.Flat;
            btnSalvar.Location = new Point(260, 500);
            btnSalvar.Width = 200;
            btnSalvar.Height = 45;
            btnSalvar.Click += btnSalvar_Click;

            Controls.Add(btnSalvar);

            ResumeLayout(false);
        }
    }
}

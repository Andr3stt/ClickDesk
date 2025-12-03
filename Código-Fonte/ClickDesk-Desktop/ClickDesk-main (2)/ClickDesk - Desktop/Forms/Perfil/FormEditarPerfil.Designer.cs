using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClickDesk
{
    partial class FormEditarPerfil
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelSidebar;
        private Panel panelTop;
        private Label lblTitulo;
        private PictureBox picFoto;
        private Button btnAlterarFoto;
        private Button btnRemoverFoto;
        private Button btnSalvar;
        private Button btnCancelar;

        private Label lblInfoConta;
        private Label lblID;
        private Label lblDesde;
        private Label lblUltimoAcesso;
        private Label lblChamadosCriados;

        private TextBox txtNome;
        private TextBox txtEmail;
        private TextBox txtTelefone;
        private TextBox txtRamal;
        private ComboBox cmbDepartamento;
        private TextBox txtCargo;

        private TextBox txtSenhaAtual;
        private TextBox txtNovaSenha;
        private TextBox txtConfirmarSenha;

        private Button btnFechar;
        private Button btnMinimizar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            this.panelSidebar = new Panel();
            this.panelTop = new Panel();
            this.lblTitulo = new Label();
            this.btnFechar = new Button();
            this.btnMinimizar = new Button();

            this.picFoto = new PictureBox();
            this.btnAlterarFoto = new Button();
            this.btnRemoverFoto = new Button();

            this.lblInfoConta = new Label();
            this.lblID = new Label();
            this.lblDesde = new Label();
            this.lblUltimoAcesso = new Label();
            this.lblChamadosCriados = new Label();

            this.txtNome = new TextBox();
            this.txtEmail = new TextBox();
            this.txtTelefone = new TextBox();
            this.txtRamal = new TextBox();
            this.cmbDepartamento = new ComboBox();
            this.txtCargo = new TextBox();

            this.txtSenhaAtual = new TextBox();
            this.txtNovaSenha = new TextBox();
            this.txtConfirmarSenha = new TextBox();

            this.btnSalvar = new Button();
            this.btnCancelar = new Button();

            // FORM
            this.SuspendLayout();
            this.ClientSize = new Size(1280, 720);
            this.FormBorderStyle = FormBorderStyle.None;

            // SIDEBAR
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Width = 220;
            panelSidebar.BackColor = ColorTranslator.FromHtml("#F8F3EB");
            this.Controls.Add(panelSidebar);

            // TOP BAR
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 60;
            panelTop.BackColor = Color.White;

            lblTitulo.Text = "Editar Perfil";
            lblTitulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitulo.Location = new Point(240, 15);

            btnFechar.Text = "X";
            btnFechar.BackColor = Color.Transparent;
            btnFechar.FlatStyle = FlatStyle.Flat;
            btnFechar.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnFechar.ForeColor = Color.Black;
            btnFechar.FlatAppearance.BorderSize = 0;
            btnFechar.Location = new Point(1240, 15);
            btnFechar.Click += btnFechar_Click;

            btnMinimizar.Text = "_";
            btnMinimizar.BackColor = Color.Transparent;
            btnMinimizar.FlatStyle = FlatStyle.Flat;
            btnMinimizar.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnMinimizar.ForeColor = Color.Black;
            btnMinimizar.FlatAppearance.BorderSize = 0;
            btnMinimizar.Location = new Point(1200, 15);
            btnMinimizar.Click += btnMinimizar_Click;

            panelTop.Controls.Add(lblTitulo);
            panelTop.Controls.Add(btnFechar);
            panelTop.Controls.Add(btnMinimizar);
            this.Controls.Add(panelTop);

            // FOTO
            picFoto.Size = new Size(140, 140);
            picFoto.SizeMode = PictureBoxSizeMode.Zoom;
            picFoto.Location = new Point(300, 100);
            this.Controls.Add(picFoto);

            btnAlterarFoto.Text = "Alterar Foto";
            btnAlterarFoto.Location = new Point(300, 250);
            btnAlterarFoto.Click += btnAlterarFoto_Click;
            this.Controls.Add(btnAlterarFoto);

            btnRemoverFoto.Text = "Remover";
            btnRemoverFoto.Location = new Point(410, 250);
            btnRemoverFoto.Click += btnRemoverFoto_Click;
            this.Controls.Add(btnRemoverFoto);

            // CAMPOS DE PERFIL
            txtNome.Location = new Point(300, 350);
            txtNome.Width = 300;

            txtEmail.Location = new Point(650, 350);
            txtEmail.Width = 300;

            txtTelefone.Location = new Point(300, 400);
            txtTelefone.Width = 300;

            txtRamal.Location = new Point(650, 400);
            txtRamal.Width = 300;

            cmbDepartamento.Location = new Point(300, 450);
            cmbDepartamento.Width = 300;
            cmbDepartamento.Items.AddRange(new object[] {
                "TI", "RH", "Financeiro", "Comercial", "Suporte"
            });

            txtCargo.Location = new Point(650, 450);
            txtCargo.Width = 300;

            this.Controls.Add(txtNome);
            this.Controls.Add(txtEmail);
            this.Controls.Add(txtTelefone);
            this.Controls.Add(txtRamal);
            this.Controls.Add(cmbDepartamento);
            this.Controls.Add(txtCargo);

            // SENHA
            txtSenhaAtual.Location = new Point(300, 520);
            txtSenhaAtual.Width = 250;
            txtSenhaAtual.PasswordChar = '*';

            txtNovaSenha.Location = new Point(570, 520);
            txtNovaSenha.Width = 250;
            txtNovaSenha.PasswordChar = '*';

            txtConfirmarSenha.Location = new Point(840, 520);
            txtConfirmarSenha.Width = 250;
            txtConfirmarSenha.PasswordChar = '*';

            this.Controls.Add(txtSenhaAtual);
            this.Controls.Add(txtNovaSenha);
            this.Controls.Add(txtConfirmarSenha);

            // BOTÕES
            btnSalvar.Text = "Salvar Alterações";
            btnSalvar.BackColor = ColorTranslator.FromHtml("#F28A1A");
            btnSalvar.ForeColor = Color.White;
            btnSalvar.FlatStyle = FlatStyle.Flat;
            btnSalvar.Location = new Point(300, 600);
            btnSalvar.Width = 200;
            btnSalvar.Click += btnSalvar_Click;

            btnCancelar.Text = "Cancelar";
            btnCancelar.BackColor = Color.Gray;
            btnCancelar.ForeColor = Color.White;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Location = new Point(520, 600);
            btnCancelar.Width = 200;
            btnCancelar.Click += btnCancelar_Click;

            this.Controls.Add(btnSalvar);
            this.Controls.Add(btnCancelar);

            this.ResumeLayout(false);
        }
    }
}

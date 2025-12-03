using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ClickDesk
{
    public partial class FormFAQTecnico : Form
    {
        public FormFAQTecnico()
        {
            InitializeComponent();
            BuildFAQ();
        }

        private void BuildFAQ()
        {
            faqAccordion.Controls.Clear();

            // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
            // EXEMPLO DE CATEGORIA E PERGUNTAS (será substituído depois via banco)
            // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬

            AddCategory("Acesso ao Sistema", new List<(string q, string a)>
            {
                ("Não consigo acessar o painel técnico", "Verifique se seu usuário está ativo."),
                ("Erro ao entrar", "Use recuperar senha e tente novamente.")
            });

            AddCategory("Chamados", new List<(string q, string a)>
            {
                ("Como aprovar chamados?", "Entre na tela de Aprovação e clique em Aprovar."),
                ("Como devolver um chamado para o usuário?", "Clique em Reprovar e informe o motivo.")
            });

            AddCategory("Procedimentos Internos", new List<(string q, string a)>
            {
                ("Checklist de manutenção", "Siga o documento interno padrão."),
                ("Fluxo de escalonamento", "Direcione para o nível 2 quando necessário.")
            });
        }

        private void AddCategory(string title, List<(string q, string a)> items)
        {
            Panel cat = new Panel();
            cat.Width = faqAccordion.Width - 20;
            cat.AutoSize = true;
            cat.BorderStyle = BorderStyle.FixedSingle;
            cat.Margin = new Padding(0, 0, 0, 12);

            Button header = new Button();
            header.Text = title;
            header.Dock = DockStyle.Top;
            header.Height = 48;
            header.TextAlign = ContentAlignment.MiddleLeft;
            header.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            header.BackColor = Color.White;
            header.FlatStyle = FlatStyle.Flat;
            header.FlatAppearance.BorderSize = 0;

            FlowLayoutPanel body = new FlowLayoutPanel();
            body.FlowDirection = FlowDirection.TopDown;
            body.WrapContents = false;
            body.Dock = DockStyle.Top;
            body.AutoSize = true;
            body.Visible = false;
            body.BackColor = Color.FromArgb(248, 248, 248);

            header.Click += (s, e) =>
            {
                body.Visible = !body.Visible;
            };

            foreach (var item in items)
            {
                Panel qItem = new Panel();
                qItem.AutoSize = true;
                qItem.Padding = new Padding(10);

                Button qButton = new Button();
                qButton.Text = item.q;
                qButton.Dock = DockStyle.Top;
                qButton.Height = 40;
                qButton.BackColor = Color.White;
                qButton.FlatStyle = FlatStyle.Flat;
                qButton.FlatAppearance.BorderSize = 0;
                qButton.TextAlign = ContentAlignment.MiddleLeft;
                qButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                Label lblAnswer = new Label();
                lblAnswer.Text = item.a;
                lblAnswer.AutoSize = true;
                lblAnswer.MaximumSize = new Size(qItem.Width - 20, 0);
                lblAnswer.Padding = new Padding(20, 5, 5, 10);
                lblAnswer.Visible = false;
                lblAnswer.Font = new Font("Segoe UI", 10);

                qButton.Click += (s, e) =>
                {
                    lblAnswer.Visible = !lblAnswer.Visible;
                };

                qItem.Controls.Add(lblAnswer);
                qItem.Controls.Add(qButton);

                body.Controls.Add(qItem);
            }

            cat.Controls.Add(body);
            cat.Controls.Add(header);

            faqAccordion.Controls.Add(cat);
        }

        private void expandAll_Click(object sender, EventArgs e)
        {
            foreach (Control c in faqAccordion.Controls)
            {
                if (c is Panel p)
                {
                    foreach (Control inner in p.Controls)
                    {
                        if (inner is FlowLayoutPanel f)
                        {
                            f.Visible = true;
                        }
                    }
                }
            }
        }

        private void collapseAll_Click(object sender, EventArgs e)
        {
            foreach (Control c in faqAccordion.Controls)
            {
                if (c is Panel p)
                {
                    foreach (Control inner in p.Controls)
                    {
                        if (inner is FlowLayoutPanel f)
                        {
                            f.Visible = false;
                        }
                    }
                }
            }
        }
    }
}

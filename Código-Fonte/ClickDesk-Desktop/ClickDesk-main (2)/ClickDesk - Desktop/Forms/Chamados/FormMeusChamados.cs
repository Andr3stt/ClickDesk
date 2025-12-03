using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Clickdesk
{
    public partial class FormMeusChamados : Form
    {
        private class Ticket
        {
            public int Numero { get; set; }
            public string Titulo { get; set; } = "";
            public string Descricao { get; set; } = "";
            public string Categoria { get; set; } = "";
            public string Status { get; set; } = "open"; 
            public DateTime CriadoEm { get; set; } = DateTime.Now;
        }

        private readonly List<Ticket> _todosChamados = new List<Ticket>();

        public FormMeusChamados()
        {
            InitializeComponent();
            ConfigurarFiltros();
            CarregarChamadosExemplo();
            AplicarFiltros();

            cboStatus.SelectedIndexChanged += Filtros_Changed;
            cboCategoria.SelectedIndexChanged += Filtros_Changed;
            btnAtualizar.Click += btnAtualizar_Click;
        }

        private void ConfigurarFiltros()
        {
            cboStatus.Items.Add(new ComboItem("Todos", ""));
            cboStatus.Items.Add(new ComboItem("Aberto", "open"));
            cboStatus.Items.Add(new ComboItem("Em andamento", "progress"));
            cboStatus.Items.Add(new ComboItem("Aguardando", "waiting"));
            cboStatus.Items.Add(new ComboItem("Fechado", "closed"));
            cboStatus.SelectedIndex = 0;

            cboCategoria.Items.Add(new ComboItem("Todas", ""));
            cboCategoria.Items.Add(new ComboItem("Hardware", "Hardware"));
            cboCategoria.Items.Add(new ComboItem("Software", "Software"));
            cboCategoria.Items.Add(new ComboItem("Rede", "Rede"));
            cboCategoria.Items.Add(new ComboItem("Acesso", "Acesso"));
            cboCategoria.Items.Add(new ComboItem("E-mail", "E-mail"));
            cboCategoria.SelectedIndex = 0;
        }

        private void CarregarChamadosExemplo()
        {
            _todosChamados.Clear();

            _todosChamados.Add(new Ticket
            {
                Numero = 1234,
                Titulo = "Problema no login do sistema",
                Descricao = "Não consigo fazer login no sistema desde ontem. Aparece erro de autenticação.",
                Categoria = "Software",
                Status = "progress",
                CriadoEm = DateTime.Now.AddHours(-2)
            });

            _todosChamados.Add(new Ticket
            {
                Numero = 1124,
                Titulo = "Lentidão no computador",
                Descricao = "O computador está muito lento",
                Categoria = "Hardware",
                Status = "open",
                CriadoEm = DateTime.Now.AddHours(-4)
            });

            _todosChamados.Add(new Ticket
            {
                Numero = 1224,
                Titulo = "Sem acesso ao e-mail",
                Descricao = "Não consigo acessar o e-mail corporativo",
                Categoria = "Acesso",
                Status = "waiting",
                CriadoEm = DateTime.Now.AddHours(-3)
            });

            _todosChamados.Add(new Ticket
            {
                Numero = 1324,
                Titulo = "Impressora não imprime",
                Descricao = "A impressora não está imprimindo documentos",
                Categoria = "Hardware",
                Status = "closed",
                CriadoEm = DateTime.Now.AddHours(-12)
            });
        }

        private void Filtros_Changed(object sender, EventArgs e) => AplicarFiltros();

        private void btnAtualizar_Click(object sender, EventArgs e) => AplicarFiltros();

        private void AplicarFiltros()
        {
            string status = (cboStatus.SelectedItem as ComboItem)?.Value ?? "";
            string categoria = (cboCategoria.SelectedItem as ComboItem)?.Value ?? "";

            IEnumerable<Ticket> query = _todosChamados;

            if (!string.IsNullOrEmpty(status))
                query = query.Where(t => t.Status == status);

            if (!string.IsNullOrEmpty(categoria))
                query = query.Where(t => t.Categoria == categoria);

            var filtrados = query.ToList();

            AtualizarKpis();
            RenderizarCards(filtrados);
        }

        private void AtualizarKpis()
        {
            lblTotalValor.Text = _todosChamados.Count.ToString();
            lblAtendidosValor.Text = _todosChamados.Count(t => t.Status == "closed").ToString();
            lblEsperaValor.Text = _todosChamados.Count(t => t.Status == "waiting").ToString();
            lblProgressoValor.Text = _todosChamados.Count(t => t.Status == "progress").ToString();
        }

        private void RenderizarCards(List<Ticket> tickets)
        {
            flowTickets.Controls.Clear();

            if (tickets.Count == 0)
            {
                panelEmpty.Visible = true;
                return;
            }

            panelEmpty.Visible = false;

            foreach (var ticket in tickets.OrderByDescending(t => t.CriadoEm))
            {
                flowTickets.Controls.Add(CriarCard(ticket));
            }
        }

        private Control CriarCard(Ticket t)
        {
            Panel card = new Panel
            {
                Width = 380,
                Height = 180,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 0, 20, 20)
            };

            card.Paint += (s, e) =>
            {
                e.Graphics.FillRectangle(
                    new SolidBrush(Color.FromArgb(242, 138, 26)),
                    0, 0, card.Width, 4);
            };

            Label lblNumero = new Label
            {
                Text = $"#{t.Numero}",
                Location = new Point(12, 12),
                Font = new Font("Segoe UI Semibold", 9F),
                BackColor = Color.FromArgb(242, 138, 26),
                ForeColor = Color.White,
                Padding = new Padding(8, 3, 8, 3),
                AutoSize = true
            };

            Label lblStatus = new Label
            {
                Text = ConverterStatus(t.Status),
                Location = new Point(260, 12),
                AutoSize = true,
                Padding = new Padding(8, 3, 8, 3),
                Font = new Font("Segoe UI Semibold", 9F)
            };
            EstiloStatus(lblStatus, t.Status);

            Label lblTitulo = new Label
            {
                Text = t.Titulo,
                Location = new Point(12, 48),
                AutoSize = false,
                Size = new Size(350, 22),
                Font = new Font("Segoe UI Semibold", 11F)
            };

            Label lblDesc = new Label
            {
                Text = t.Descricao,
                Location = new Point(12, 72),
                AutoSize = false,
                Size = new Size(350, 44),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(107, 114, 128)
            };

            Label lblCat = new Label
            {
                Text = t.Categoria,
                Location = new Point(12, 125),
                AutoSize = true,
                Padding = new Padding(8, 3, 8, 3),
                BackColor = Color.FromArgb(255, 245, 230),
                ForeColor = Color.FromArgb(242, 138, 26),
                Font = new Font("Segoe UI Semibold", 8.5F)
            };

            Label lblTempo = new Label
            {
                Text = TempoRelativo(t.CriadoEm),
                Location = new Point(12 + lblCat.Width + 12, 129),
                AutoSize = true,
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(107, 114, 128)
            };

            Button btn = new Button
            {
                Text = "Ver detalhes →",
                Location = new Point(250, 140),
                Size = new Size(120, 28),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(242, 138, 26)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) =>
            {
                MessageBox.Show($"Detalhes do chamado #{t.Numero}:\n{t.Titulo}");
            };

            card.Controls.Add(lblNumero);
            card.Controls.Add(lblStatus);
            card.Controls.Add(lblTitulo);
            card.Controls.Add(lblDesc);
            card.Controls.Add(lblCat);
            card.Controls.Add(lblTempo);
            card.Controls.Add(btn);

            return card;
        }

        private string ConverterStatus(string s)
        {
            if (s == "open") return "ABERTO";
            if (s == "progress") return "EM ANDAMENTO";
            if (s == "waiting") return "AGUARDANDO";
            if (s == "closed") return "FECHADO";
            return s.ToUpper();
        }

        private void EstiloStatus(Label lbl, string status)
        {
            if (status == "open")
            {
                lbl.BackColor = Color.FromArgb(254, 243, 199);
                lbl.ForeColor = Color.FromArgb(146, 64, 14);
            }
            else if (status == "progress")
            {
                lbl.BackColor = Color.FromArgb(219, 234, 254);
                lbl.ForeColor = Color.FromArgb(30, 64, 175);
            }
            else if (status == "waiting")
            {
                lbl.BackColor = Color.FromArgb(243, 244, 246);
                lbl.ForeColor = Color.FromArgb(55, 65, 81);
            }
            else if (status == "closed")
            {
                lbl.BackColor = Color.FromArgb(209, 250, 229);
                lbl.ForeColor = Color.FromArgb(6, 95, 70);
            }
        }

        private string TempoRelativo(DateTime dt)
        {
            var diff = DateTime.Now - dt;

            if (diff.TotalMinutes < 1) return "Agora mesmo";
            if (diff.TotalMinutes < 60) return $"Há {Math.Floor(diff.TotalMinutes)} min";
            if (diff.TotalHours < 24) return $"Há {Math.Floor(diff.TotalHours)} horas";
            return $"Há {Math.Floor(diff.TotalDays)} dias";
        }

        private class ComboItem
        {
            public string Text { get; }
            public string Value { get; }

            public ComboItem(string t, string v)
            {
                Text = t;
                Value = v;
            }

            public override string ToString() => Text;
        }
    }
}

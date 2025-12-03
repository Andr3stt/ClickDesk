using System;
using System.Windows.Forms;

namespace ClickDesk
{
    public partial class FormFAQ : Form
    {
        public FormFAQ()
        {
            InitializeComponent();
            LoadFAQ();
            btnExpandAll.Click += (s, e) => ExpandAll(true);
            btnCollapseAll.Click += (s, e) => ExpandAll(false);
        }

        private void LoadFAQ()
        {
            AddCategory("Hardware", new (string, string)[] {
                ("O computador não liga", "Verifique cabos, fonte e estabilizador."),
                ("Upgrade de RAM", "Permitido apenas para máquinas homologadas."),
            });

            AddCategory("Software", new (string, string)[] {
                ("Erro ao abrir programas", "Reinstale ou atualize o software."),
                ("Licença inválida", "Contate o administrador."),
            });

            AddCategory("Rede", new (string, string)[] {
                ("Sem internet", "Teste cabo e Wi-Fi."),
                ("IP inválido", "Reinicie o modem e a placa."),
            });

            AddCategory("Acesso", new (string, string)[] {
                ("Senha expirada", "Reinicie no portal de credenciais."),
                ("Acesso negado", "Permissões devem ser revisadas."),
            });

            AddCategory("Outros", new (string, string)[] {
                ("Solicitar material", "Abra um chamado."),
                ("Dúvidas gerais", "Contate o suporte."),
            });
        }

        private void AddCategory(string title, (string, string)[] questions)
        {
            var cat = new UCAccordionCategory(title);

            foreach (var q in questions)
                cat.AddQuestion(new UCAccordionQuestion(q.Item1, q.Item2));

            faqContainer.Controls.Add(cat);
        }

        private void ExpandAll(bool expand)
        {
            foreach (Control c in faqContainer.Controls)
                if (c is UCAccordionCategory cat)
                    cat.SetExpanded(expand);
        }
    }
}


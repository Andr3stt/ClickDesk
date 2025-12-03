using System;
using System.Windows.Forms;

namespace ClickDesk.Forms
{
    public partial class UCCategoriaFAQTecnico : UserControl
    {
        public bool AbreAutomatico { get; set; }

        public UCCategoriaFAQTecnico()
        {
            InitializeComponent();
        }

        public void SetCategoria(string titulo)
        {
            lblCategoria.Text = titulo;
        }

        public void AddPergunta(string pergunta, string resposta)
        {
            var item = new UCPerguntaFAQTecnico();
            item.SetPergunta(pergunta);
            item.SetResposta(resposta);
            flowPerguntas.Controls.Add(item);
        }

        public void Expandir()
        {
            flowPerguntas.Visible = true;
            btnToggle.Text = "▼";
        }

        public void Recolher()
        {
            flowPerguntas.Visible = false;
            btnToggle.Text = "▲";
        }

        private void btnHeader_Click(object sender, EventArgs e)
        {
            if (flowPerguntas.Visible)
                Recolher();
            else
                Expandir();
        }
    }
}

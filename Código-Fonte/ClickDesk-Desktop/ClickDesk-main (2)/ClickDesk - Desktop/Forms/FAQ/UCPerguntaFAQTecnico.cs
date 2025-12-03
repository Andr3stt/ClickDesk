using System;
using System.Windows.Forms;

namespace ClickDesk.Forms
{
    public partial class UCPerguntaFAQTecnico : UserControl
    {
        public UCPerguntaFAQTecnico()
        {
            InitializeComponent();
        }

        public void SetPergunta(string pergunta)
        {
            lblPergunta.Text = pergunta;
        }

        public void SetResposta(string resposta)
        {
            lblResposta.Text = resposta;
        }

        private void btnPergunta_Click(object sender, EventArgs e)
        {
            lblResposta.Visible = !lblResposta.Visible;
            btnPergunta.Text = lblResposta.Visible ? "▼" : "▶";
        }
    }
}

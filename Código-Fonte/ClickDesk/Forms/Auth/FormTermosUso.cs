using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ClickDesk.Utils;

namespace ClickDesk.Forms.Auth
{
    /// <summary>
    /// Formulário de Termos de Uso e Política de Privacidade.
    /// Exibe os termos e condições que o usuário deve aceitar para usar o sistema.
    /// </summary>
    public partial class FormTermosUso : Form
    {
        // Componentes do formulário
        private Panel panelPrincipal;
        private RichTextBox txtTermos;
        private CheckBox chkAceito;
        private Button btnAceitar;
        private Button btnRecusar;

        /// <summary>
        /// Indica se o usuário aceitou os termos
        /// </summary>
        public bool TermosAceitos { get; private set; }

        /// <summary>
        /// Construtor do formulário de termos de uso.
        /// </summary>
        public FormTermosUso()
        {
            InitializeComponent();
            CriarInterface();
        }

        /// <summary>
        /// Cria a interface do formulário de termos de uso.
        /// </summary>
        private void CriarInterface()
        {
            // Configurações do formulário
            this.Text = "ClickDesk - Termos de Uso";
            this.Size = new Size(800, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = ClickDeskColors.BackgroundApp;

            // Painel principal com bordas arredondadas
            panelPrincipal = new Panel
            {
                Size = new Size(700, 570),
                Location = new Point((this.ClientSize.Width - 700) / 2, 20),
                BackColor = ClickDeskColors.White
            };
            panelPrincipal.Paint += PanelPrincipal_Paint;
            this.Controls.Add(panelPrincipal);

            int y = 30;
            int leftMargin = 40;
            int contentWidth = 620;

            // Logo e Título
            Label lblLogo = new Label
            {
                Text = "🖥️ ClickDesk",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = ClickDeskColors.Brand,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            panelPrincipal.Controls.Add(lblLogo);

            y += 50;

            // Título dos Termos
            Label lblTitulo = new Label
            {
                Text = "Termos de Uso e Política de Privacidade",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            panelPrincipal.Controls.Add(lblTitulo);

            y += 35;

            // Subtítulo
            Label lblSubtitulo = new Label
            {
                Text = "Por favor, leia atentamente os termos abaixo antes de continuar.",
                Font = new Font("Segoe UI", 10),
                ForeColor = ClickDeskColors.TextSecondary,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            panelPrincipal.Controls.Add(lblSubtitulo);

            y += 35;

            // Área de texto dos termos
            txtTermos = new RichTextBox
            {
                Location = new Point(leftMargin, y),
                Size = new Size(contentWidth, 300),
                Font = new Font("Segoe UI", 10),
                BackColor = ClickDeskColors.Gray50,
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                ScrollBars = RichTextBoxScrollBars.Vertical
            };
            txtTermos.Text = ObterTextoDosTermos();
            panelPrincipal.Controls.Add(txtTermos);

            y += 320;

            // Data de atualização
            Label lblAtualizacao = new Label
            {
                Text = "Última atualização: " + DateTime.Now.ToString("dd/MM/yyyy"),
                Font = new Font("Segoe UI", 9),
                ForeColor = ClickDeskColors.Gray500,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            panelPrincipal.Controls.Add(lblAtualizacao);

            y += 30;

            // Checkbox de aceite
            chkAceito = new CheckBox
            {
                Text = "Li e aceito os Termos de Uso e Política de Privacidade",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ClickDeskColors.TextPrimary,
                Location = new Point(leftMargin, y),
                AutoSize = true
            };
            chkAceito.CheckedChanged += ChkAceito_CheckedChanged;
            panelPrincipal.Controls.Add(chkAceito);

            y += 40;

            // Botão Recusar
            btnRecusar = new Button
            {
                Text = "Recusar",
                Size = new Size(150, 45),
                Location = new Point(leftMargin + contentWidth - 320, y),
                BackColor = ClickDeskColors.Gray200,
                ForeColor = ClickDeskColors.Gray700,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRecusar.FlatAppearance.BorderSize = 0;
            btnRecusar.Click += BtnRecusar_Click;
            panelPrincipal.Controls.Add(btnRecusar);

            // Botão Aceitar
            btnAceitar = new Button
            {
                Text = "ACEITAR E CONTINUAR",
                Size = new Size(160, 45),
                Location = new Point(leftMargin + contentWidth - 160, y),
                BackColor = ClickDeskColors.Gray400,
                ForeColor = ClickDeskColors.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnAceitar.FlatAppearance.BorderSize = 0;
            btnAceitar.Click += BtnAceitar_Click;
            panelPrincipal.Controls.Add(btnAceitar);
        }

        /// <summary>
        /// Desenha bordas arredondadas no painel principal.
        /// </summary>
        private void PanelPrincipal_Paint(object sender, PaintEventArgs e)
        {
            var rect = new Rectangle(0, 0, panelPrincipal.Width - 1, panelPrincipal.Height - 1);
            var path = ClickDeskStyles.GetRoundedRectangle(rect, ClickDeskStyles.RadiusXL);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (var brush = new SolidBrush(ClickDeskColors.White))
            {
                e.Graphics.FillPath(brush, path);
            }

            using (var pen = new Pen(ClickDeskColors.Border, 1))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        /// <summary>
        /// Retorna o texto completo dos termos de uso.
        /// </summary>
        private string ObterTextoDosTermos()
        {
            return @"TERMOS DE USO DO SISTEMA CLICKDESK

1. ACEITAÇÃO DOS TERMOS
Ao acessar e utilizar o sistema ClickDesk, você concorda em cumprir e estar vinculado aos seguintes termos e condições de uso. Se você não concordar com qualquer parte destes termos, não poderá acessar ou usar nossos serviços.

2. DESCRIÇÃO DO SERVIÇO
O ClickDesk é um sistema de helpdesk e gerenciamento de chamados de suporte técnico, desenvolvido para auxiliar empresas no atendimento e resolução de problemas reportados por seus colaboradores.

3. USO DO SISTEMA
3.1. Você se compromete a usar o sistema apenas para fins legítimos relacionados ao suporte técnico.
3.2. Você é responsável por manter a confidencialidade de suas credenciais de acesso.
3.3. Você concorda em não compartilhar sua conta com terceiros.
3.4. Você se compromete a não tentar acessar áreas restritas do sistema sem autorização.

4. PRIVACIDADE E PROTEÇÃO DE DADOS
4.1. Coletamos apenas os dados necessários para o funcionamento do serviço.
4.2. Seus dados pessoais são tratados conforme a Lei Geral de Proteção de Dados (LGPD).
4.3. Não compartilhamos seus dados com terceiros sem seu consentimento.
4.4. Você pode solicitar a exclusão de seus dados a qualquer momento.

5. INTELIGÊNCIA ARTIFICIAL
5.1. O sistema utiliza IA para auxiliar na resolução de chamados.
5.2. As sugestões da IA são apenas orientações e não substituem a análise humana.
5.3. Você pode optar por não utilizar os recursos de IA a qualquer momento.

6. RESPONSABILIDADES DO USUÁRIO
6.1. Fornecer informações precisas e completas nos chamados.
6.2. Manter suas informações de cadastro atualizadas.
6.3. Reportar qualquer uso indevido ou vulnerabilidade identificada.
6.4. Não utilizar o sistema para fins ilícitos ou não autorizados.

7. PROPRIEDADE INTELECTUAL
Todo o conteúdo do sistema ClickDesk, incluindo mas não limitado a textos, gráficos, logos, ícones e software, é propriedade da equipe ClickDesk e está protegido por leis de propriedade intelectual.

8. LIMITAÇÃO DE RESPONSABILIDADE
O sistema é fornecido ""como está"" sem garantias de qualquer tipo. Não nos responsabilizamos por:
8.1. Interrupções no serviço por motivos técnicos ou manutenção.
8.2. Perdas de dados causadas por fatores fora de nosso controle.
8.3. Decisões tomadas com base nas sugestões da IA.

9. MODIFICAÇÕES DOS TERMOS
Reservamo-nos o direito de modificar estes termos a qualquer momento. Alterações significativas serão comunicadas através do sistema.

10. CONTATO
Em caso de dúvidas sobre estes termos, entre em contato conosco através do suporte do sistema.

© 2024 ClickDesk - Todos os direitos reservados.";
        }

        /// <summary>
        /// Evento de alteração do checkbox de aceite.
        /// </summary>
        private void ChkAceito_CheckedChanged(object sender, EventArgs e)
        {
            // Habilita/desabilita o botão de aceitar
            btnAceitar.Enabled = chkAceito.Checked;
            btnAceitar.BackColor = chkAceito.Checked ? ClickDeskColors.Brand : ClickDeskColors.Gray400;
        }

        /// <summary>
        /// Evento de clique no botão Aceitar.
        /// </summary>
        private void BtnAceitar_Click(object sender, EventArgs e)
        {
            TermosAceitos = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Evento de clique no botão Recusar.
        /// </summary>
        private void BtnRecusar_Click(object sender, EventArgs e)
        {
            TermosAceitos = false;
            
            // Confirma a recusa
            var resultado = MessageBox.Show(
                "Ao recusar os termos, você não poderá utilizar o sistema.\n\nDeseja realmente recusar?",
                "Confirmar Recusa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}

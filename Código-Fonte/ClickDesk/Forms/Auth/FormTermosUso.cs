using System;
using System.Drawing;
using System.Windows.Forms;
using ClickDesk.Utils;
using Siticone.Desktop.UI.WinForms;

namespace ClickDesk.Forms.Auth
{
    /// <summary>
    /// Formulário de Termos de Uso e Política de Privacidade.
    /// Exibe os termos e condições que o usuário deve aceitar para usar o sistema.
    /// </summary>
    public partial class FormTermosUso : Form
    {
        // Componentes do formulário
        private SiticonePanel panelPrincipal;
        private RichTextBox txtTermos;
        private SiticoneCheckBox chkAceito;
        private SiticoneButton btnAceitar;
        private SiticoneButton btnRecusar;

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
            this.Size = new Size(820, 680);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = ThemeManager.BackgroundApp;

            // Subscribe to theme changes
            ThemeManager.ThemeChanged += (s, e) =>
            {
                this.BackColor = ThemeManager.BackgroundApp;
                ApplyTheme();
            };

            // Painel principal com Siticone
            panelPrincipal = new SiticonePanel
            {
                Size = new Size(760, 620),
                Location = new Point((this.Width - 760) / 2, 30),
                FillColor = ThemeManager.CardBackground,
                BorderRadius = ClickDeskStyles.RadiusXL
            };
            panelPrincipal.ShadowDecoration.Enabled = true;
            panelPrincipal.ShadowDecoration.Depth = 20;
            this.Controls.Add(panelPrincipal);

            int y = 30;
            int leftMargin = 50;
            int contentWidth = 660;

            // Logo e Título
            Label lblLogo = new Label
            {
                Text = "🖥️ ClickDesk",
                Font = ClickDeskStyles.Font3XL,
                ForeColor = ThemeManager.Brand,
                Location = new Point(leftMargin, y),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            panelPrincipal.Controls.Add(lblLogo);

            y += 50;

            // Título dos Termos
            Label lblTitulo = new Label
            {
                Text = "Termos de Uso e Política de Privacidade",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = ThemeManager.TextPrimary,
                Location = new Point(leftMargin, y),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            panelPrincipal.Controls.Add(lblTitulo);

            y += 40;

            // Subtítulo
            Label lblSubtitulo = new Label
            {
                Text = "Por favor, leia atentamente os termos abaixo antes de continuar.",
                Font = ClickDeskStyles.FontBase,
                ForeColor = ThemeManager.TextSecondary,
                Location = new Point(leftMargin, y),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            panelPrincipal.Controls.Add(lblSubtitulo);

            y += 35;

            // Área de texto dos termos
            txtTermos = new RichTextBox
            {
                Location = new Point(leftMargin, y),
                Size = new Size(contentWidth, 320),
                Font = ClickDeskStyles.FontBase,
                BackColor = ThemeManager.Surface,
                ForeColor = ThemeManager.TextPrimary,
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                ScrollBars = RichTextBoxScrollBars.Vertical
            };
            txtTermos.Text = ObterTextoDosTermos();
            panelPrincipal.Controls.Add(txtTermos);

            y += 330;

            // Data de atualização
            Label lblAtualizacao = new Label
            {
                Text = "Última atualização: " + DateTime.Now.ToString("dd/MM/yyyy"),
                Font = ClickDeskStyles.FontSM,
                ForeColor = ThemeManager.TextSecondary,
                Location = new Point(leftMargin, y),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            panelPrincipal.Controls.Add(lblAtualizacao);

            y += 30;

            // Checkbox de aceite
            chkAceito = new SiticoneCheckBox
            {
                Text = "Li e aceito os Termos de Uso e Política de Privacidade",
                Font = ClickDeskStyles.FontBaseStrong,
                ForeColor = ThemeManager.TextPrimary,
                Location = new Point(leftMargin, y),
                AutoSize = true,
                CheckedState = { 
                    FillColor = ThemeManager.Brand,
                    BorderColor = ThemeManager.Brand
                }
            };
            chkAceito.CheckedChanged += ChkAceito_CheckedChanged;
            panelPrincipal.Controls.Add(chkAceito);

            y += 40;

            // Botão Recusar
            btnRecusar = new SiticoneButton
            {
                Text = "Recusar",
                Size = new Size(150, 45),
                Location = new Point(leftMargin + contentWidth - 330, y),
                BorderRadius = ClickDeskStyles.RadiusMD,
                FillColor = ClickDeskColors.Gray300,
                ForeColor = ClickDeskColors.Gray700,
                Font = ClickDeskStyles.FontLG,
                Cursor = Cursors.Hand,
                HoverState = { FillColor = ClickDeskColors.Gray400 }
            };
            btnRecusar.Click += BtnRecusar_Click;
            panelPrincipal.Controls.Add(btnRecusar);

            // Botão Aceitar
            btnAceitar = new SiticoneButton
            {
                Text = "ACEITAR E CONTINUAR",
                Size = new Size(160, 45),
                Location = new Point(leftMargin + contentWidth - 160, y),
                BorderRadius = ClickDeskStyles.RadiusMD,
                FillColor = ClickDeskColors.Gray400,
                ForeColor = Color.White,
                Font = ClickDeskStyles.FontLG,
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnAceitar.Click += BtnAceitar_Click;
            panelPrincipal.Controls.Add(btnAceitar);
        }

        /// <summary>
        /// Aplica o tema atual aos controles do formulário.
        /// </summary>
        private void ApplyTheme()
        {
            panelPrincipal.FillColor = ThemeManager.CardBackground;
            txtTermos.BackColor = ThemeManager.Surface;
            txtTermos.ForeColor = ThemeManager.TextPrimary;
            chkAceito.ForeColor = ThemeManager.TextPrimary;
            chkAceito.CheckedState.FillColor = ThemeManager.Brand;
            chkAceito.CheckedState.BorderColor = ThemeManager.Brand;

            // Update all labels
            foreach (Control control in panelPrincipal.Controls)
            {
                if (control is Label label)
                {
                    if (label.Font.Bold)
                    {
                        label.ForeColor = ThemeManager.TextPrimary;
                    }
                    else
                    {
                        label.ForeColor = ThemeManager.TextSecondary;
                    }
                }
            }

            panelPrincipal.Invalidate();
            this.Invalidate();
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
            btnAceitar.FillColor = chkAceito.Checked ? ThemeManager.Brand : ClickDeskColors.Gray400;
            btnAceitar.HoverState.FillColor = chkAceito.Checked ? ThemeManager.BrandHover : ClickDeskColors.Gray400;
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

using System;
using System.Windows.Forms;
using ClickDesk.Utils;

namespace ClickDesk
{
    /// <summary>
    /// Configurações globais de estilo para toda a aplicação.
    /// Aplica temas e padrões visuais em todos os formulários.
    /// </summary>
    public static class GlobalStyleConfig
    {
        /// <summary>
        /// Aplica o estilo padrão da aplicação a um formulário.
        /// Deve ser chamado no construtor de cada formulário.
        /// </summary>
        public static void ApplyDefaultStyle(Form form)
        {
            // Configurar propriedades padrão do formulário
            form.BackColor = StyleManager.Colors.BackgroundAlt;
            form.ForeColor = StyleManager.Colors.TextPrimary;
            form.Font = StyleManager.Fonts.Body;
            form.StartPosition = FormStartPosition.CenterScreen;

            // Aplicar estilo a todos os controles
            ApplyStyleToAllControls(form);
        }

        /// <summary>
        /// Aplica estilos recursivamente a todos os controles de um formulário.
        /// </summary>
        private static void ApplyStyleToAllControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                // Aplicar estilo baseado no tipo de controle
                if (control is Button btn)
                {
                    StyleManager.ApplyButtonStyle(btn);
                }
                else if (control is TextBox txtbox)
                {
                    StyleManager.ApplyTextBoxStyle(txtbox);
                }
                else if (control is Label lbl)
                {
                    StyleManager.ApplyLabelStyle(lbl);
                }
                else if (control is Panel pnl)
                {
                    StyleManager.ApplyPanelStyle(pnl);
                }

                // Aplicar recursivamente a controles filhos
                if (control.HasChildren)
                {
                    ApplyStyleToAllControls(control);
                }
            }
        }

        /// <summary>
        /// Aplica tema escuro globalmente.
        /// </summary>
        public static void ApplyDarkTheme(Form form)
        {
            form.BackColor = StyleManager.Colors.BackgroundDark;
            form.ForeColor = StyleManager.Colors.TextInverted;

            ApplyDarkThemeToAllControls(form);
        }

        /// <summary>
        /// Aplica tema escuro recursivamente a todos os controles.
        /// </summary>
        private static void ApplyDarkThemeToAllControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                control.BackColor = StyleManager.Colors.BackgroundDark;
                control.ForeColor = StyleManager.Colors.TextInverted;

                if (control.HasChildren)
                {
                    ApplyDarkThemeToAllControls(control);
                }
            }
        }

        /// <summary>
        /// Configura hotkeys padrão (Escape para fechar, etc).
        /// </summary>
        public static void SetupHotkeys(Form form)
        {
            form.KeyPreview = true;
            form.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    form.Close();
                    e.Handled = true;
                }
            };
        }

        /// <summary>
        /// Validar que um TextBox não está vazio.
        /// </summary>
        public static bool ValidateTextBoxNotEmpty(TextBox input, string fieldName = "Campo")
        {
            if (string.IsNullOrWhiteSpace(input.Text))
            {
                MessageBox.Show(
                    $"{fieldName} é obrigatório!",
                    "Validação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                input.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validar que um TextBox tem email válido.
        /// </summary>
        public static bool ValidateEmail(TextBox input)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(input.Text);
                return addr.Address == input.Text;
            }
            catch
            {
                MessageBox.Show(
                    "Email inválido!",
                    "Validação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                input.Focus();
                return false;
            }
        }

        /// <summary>
        /// Limpar todos os inputs em um container.
        /// </summary>
        public static void ClearAllInputs(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox textbox)
                {
                    textbox.Clear();
                }

                if (control.HasChildren)
                {
                    ClearAllInputs(control);
                }
            }
        }

        /// <summary>
        /// Desabilitar todos os inputs em um container.
        /// </summary>
        public static void DisableAllInputs(Control parent, bool disable = true)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox || 
                    control is Button)
                {
                    control.Enabled = !disable;
                }

                if (control.HasChildren)
                {
                    DisableAllInputs(control, disable);
                }
            }
        }

        /// <summary>
        /// Exibir mensagem de sucesso.
        /// </summary>
        public static void ShowSuccessMessage(string message, string title = "Sucesso")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Exibir mensagem de erro.
        /// </summary>
        public static void ShowErrorMessage(string message, string title = "Erro")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Exibir mensagem de aviso.
        /// </summary>
        public static void ShowWarningMessage(string message, string title = "Aviso")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Exibir caixa de confirmação.
        /// </summary>
        public static bool ShowConfirmation(string message, string title = "Confirmação")
        {
            DialogResult result = MessageBox.Show(
                message,
                title,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            return result == DialogResult.Yes;
        }
    }
}

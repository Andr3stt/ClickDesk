using System;
using System.Windows.Forms;

namespace ClickDesk
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Força abertura apenas do FormLogin
                var form = new Forms.Auth.FormLogin();
                form.StartPosition = FormStartPosition.CenterScreen;
                form.WindowState = FormWindowState.Maximized;

                Application.Run(form);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "ERRO AO INICIAR A APLICAÇÃO:\n\n" + ex.ToString(),
                    "Erro crítico",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}

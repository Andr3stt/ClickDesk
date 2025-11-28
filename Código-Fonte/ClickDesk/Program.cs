using System;
using System.Windows.Forms;
using ClickDesk.Forms.Auth;

namespace ClickDesk
{
    /// <summary>
    /// Ponto de entrada principal da aplicação ClickDesk.
    /// Este é o primeiro arquivo executado quando a aplicação é iniciada.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Método principal que inicia a aplicação Windows Forms.
        /// Configura estilos visuais e inicia o formulário de login.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Habilita estilos visuais modernos do Windows
            Application.EnableVisualStyles();
            
            // Configura o texto padrão para controles
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Inicia a aplicação com o formulário de login
            // Após login bem-sucedido, o FormLogin abrirá o FormDashboard
            Application.Run(new FormLogin());
        }
    }
}

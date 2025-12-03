using System;
using System.Windows.Forms;
using ClickDesk.Forms.Auth;

namespace ClickDesk
{
    static class TestProgram
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLogin());
        }
    }
}
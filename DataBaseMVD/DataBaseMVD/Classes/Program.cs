using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;

namespace DataBaseMVD
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(Exceptions);
            EnthernetCheck();
        }

        private static void Exceptions(object sender, ThreadExceptionEventArgs e)
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                MessageBox.Show("Для использования программы необходимо стабильное подключение к интернету", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Process.GetCurrentProcess().Kill();
            }

            MessageBox.Show(e.Exception.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DBMVD.Methods.SetError(e.Exception.ToString());
            DBMVD.Methods.SetOffline();
        } //Если произошла необработанная ошибка

        private static void EnthernetCheck()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                MessageBox.Show("Для использования программы необходимо стабильное подключение к интернету", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                Application.Run(new AuthorisationForm());
        }
    }
}
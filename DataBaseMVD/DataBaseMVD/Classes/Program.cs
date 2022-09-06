using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            Application.Run(new AuthorisationForm());
        }

        private static void Exceptions(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            using (SqlCommand sqlCommand = new SqlCommand($"UPDATE [UsersMVD] SET Desktop = NULL WHERE Login = @login", DataBaseSqlCommands.sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@login", DataBaseSqlCommands.localUser);
                DataBaseSqlCommands.UpdInsDelSqlCommands(sqlCommand);
                Application.ExitThread();
            }
        } //Если произошла необработанная ошибка
    }
}
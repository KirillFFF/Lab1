using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DataBaseMVD
{
    public class DBMVD
    {
        public static string LocalUser { get; set; }
        private MySqlConnection _mySqlConnection;
        private static volatile DBMVD Class;
        private static object SyncObj = new object();
        public static DBMVD Methods
        {
            get
            {
                if (Class is null)
                {
                    lock (SyncObj)
                    {
                        if (Class is null)
                            Class = new DBMVD();
                    }
                }
                return Class;
            }
        }

        private MySqlConnection ConnectionDB => _mySqlConnection ?? (_mySqlConnection = new MySqlConnection("server=f0626865.xsph.ru;database=f0626865_MVDUsers;user id=f0626865;password=diikceegip"));

        private void ConnectionClose()
        {
            if (ConnectionDB.State == ConnectionState.Open)
                ConnectionDB.Close();
        } //Connection Close

        private void ConnectionOpen()
        {
            if (ConnectionDB.State == ConnectionState.Closed)
                ConnectionDB.Open();
        } //Connection Open

        public async Task<DataTable> GetDataAsync(MySqlCommand mySqlCommand)
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter())
            using (DataTable table = new DataTable())
            {
                mySqlCommand.Connection = ConnectionDB;
                ConnectionOpen();
                adapter.SelectCommand = mySqlCommand;
                await adapter.FillAsync(table);
                ConnectionClose();
                return table;
            }
        } //Get data from request

        public void SetData(MySqlCommand mySqlCommand)
        {
            mySqlCommand.Connection = ConnectionDB;
            ConnectionOpen();
            mySqlCommand.ExecuteNonQuery();
            ConnectionClose();
        } //Set data from request

        public void SetOnline(string login)
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand($"UPDATE `UsersMVD` SET `Desktop`= @desktop WHERE `Login` = @login"))
            {
                mySqlCommand.Parameters.AddWithValue("@desktop", SystemInformation.ComputerName);
                mySqlCommand.Parameters.AddWithValue("@login", login);
                LocalUser = login;
                SetData(mySqlCommand);
            }
        } //Set user/admin online

        public void SetOffline()
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand($"UPDATE `UsersMVD` SET `Desktop` = NULL WHERE `Login` = @login"))
            {
                mySqlCommand.Parameters.AddWithValue("@login", LocalUser);
                SetData(mySqlCommand);
                Application.ExitThread();
            }
        }

        public async void WaitAccess(string login, string hwid)
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand(
                $"INSERT INTO `HWIDUsers`(`Login`, `HWID`, `IP`) VALUES (@login, @hwid, @ip) " +
                $"ON DUPLICATE KEY UPDATE `HWID` = @hwid, `IP` = @ip, `Visible` = 1"))
            {
                mySqlCommand.Parameters.AddWithValue("@login", login);
                mySqlCommand.Parameters.AddWithValue("@hwid", hwid);
                mySqlCommand.Parameters.AddWithValue("@ip", await new HttpClient().GetStringAsync("https://api.ipify.org"));
                SetData(mySqlCommand);

                MessageBox.Show("В доступе отказано! Был отправлен запрос на его получение, ожидайте.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Application.ExitThread();
            }
        } //Wait for access response

        public async void RefreshAdmList(Label label)
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand($"SELECT COUNT(*) FROM `UsersMVD` WHERE `Permission` = 'admin' AND `Desktop` IS NOT NULL"))
                label.Text = $"Администрация в сети: {(await GetDataAsync(mySqlCommand)).Rows[0].ItemArray[0]}";
        }

        public void SetError(string message)
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand($"INSERT INTO `Errors` VALUES(@log, @file)"))
            {
                string titleLog = $"{LocalUser ?? "unknown"}_crash_{DateTime.UtcNow.ToString().Replace(' ', '_')}";
                byte[] error = Encoding.Unicode.GetBytes(message);
                mySqlCommand.Parameters.AddWithValue("@login", LocalUser);
                mySqlCommand.Parameters.AddWithValue("@log", titleLog);
                mySqlCommand.Parameters.AddWithValue("@file", error);
                SetData(mySqlCommand);
            }
        }
    }
}
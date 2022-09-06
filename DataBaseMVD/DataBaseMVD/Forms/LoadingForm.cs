using Microsoft.Win32;
using System;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net;
using System.Diagnostics;

namespace DataBaseMVD
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
        }

        private async void LoadingForm_Load(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(24, 28, 78);
            TransparencyKey = Color.FromArgb(24, 28, 78);
            SumCheck();
            RegistryCheck();

            for (Opacity = 0; Opacity <= 1; Opacity += 0.01)
            {
                await Task.Delay(10);
            } //Плавное появление эмблемы
        }

        private async void LoadingForm_Shown(object sender, EventArgs e)
        {
            await Task.Delay(3000);
            Close();
            Application.OpenForms.OfType<AuthorisationForm>().First().Show();
        }

        #region Methods and others

        private async void SumCheck()
        {
            string hash;
            using (System.Security.Cryptography.SHA512 sha512 = System.Security.Cryptography.SHA512.Create())
            using (var stream = File.OpenRead(Assembly.GetExecutingAssembly().Location))
                hash = BitConverter.ToString(sha512.ComputeHash(stream)).Replace("-", "");

            using (MySqlCommand mySqlCommand = new MySqlCommand(
                $"SELECT `Version`, `Hash`, `Link`, `UserAgent` FROM `HashApp` WHERE `Version` = (SELECT MAX(`Version`) FROM `HashApp`)"))
            {
                using (DataTable table = await DBMVD.Methods.GetDataAsync(mySqlCommand))
                {
                    if (table.Rows[0].ItemArray[1].ToString() != hash || table.Rows[0].ItemArray[0].ToString() != Application.ProductVersion)
                        using (WebClient wc = new WebClient())
                        {
                            string invisPath = Path.GetTempPath();
                            string invisFile = $"{Path.GetRandomFileName()}.exe";
                            wc.Headers[HttpRequestHeader.UserAgent] = string.Concat(table.Rows[0].ItemArray[3].ToString().Reverse());
                            wc.DownloadFile(table.Rows[0].ItemArray[2].ToString(), $"{invisPath}{invisFile}");
                            DeleteSelf(invisPath, invisFile);
                        }
                }
            }
        }

        private void DeleteSelf(string path, string fileUpd)
        {
            string fileName = $"{Path.GetRandomFileName()}.bat";
            string batContent = $"chcp 65001 >nul\r\ntaskkill /f /PID {Process.GetCurrentProcess().Id}\r\n" +
                $"taskkill /f /im {Process.GetCurrentProcess().ProcessName}\r\n:mf\r\nmove /Y \"{path}{fileUpd}\" \"{Application.ExecutablePath}\"\r\n" +
                $"if exist \"{path}{fileUpd}\" GOTO mf\r\nstart \"\" \"{Application.ExecutablePath}\"\r\ndel /q %0";
            File.WriteAllText($"{path}{fileName}", batContent);
            Process.Start(new ProcessStartInfo { FileName = "cmd.exe", Arguments = $"/c {path}{fileName}", WindowStyle = ProcessWindowStyle.Hidden }).WaitForExit();
        }

        private async void RegistryCheck()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\DataBaseMVD"))
            {
                if (key == null)
                {
                    using (RegistryKey newKey = Registry.CurrentUser.CreateSubKey(@"Software\DataBaseMVD\AdminSettings"))
                    {
                        newKey.SetValue("ButtonsHide", false);
                        newKey.SetValue("IPMask", $"{await new HttpClient().GetStringAsync("https://api.ipify.org")}");
                    }
                }
            }
        } //Проверка на наличие ветки программы в реестре, если нет, то создать всё необходимое
        #endregion
    }
}
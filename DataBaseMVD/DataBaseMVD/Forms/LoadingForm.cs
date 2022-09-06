using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseMVD
{
    public partial class LoadingForm : Form
    {

        public LoadingForm()
        {
            InitializeComponent();
        }

        async void LoadingForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(24, 28, 78);
            this.TransparencyKey = Color.FromArgb(24, 28, 78);
            RegistryCheck();

            for (Opacity = 0; Opacity <= 1; Opacity += 0.01)
            {
                await Task.Delay(10);
            } //Плавное появление эмблемы
        }

        async void LoadingForm_Activated(object sender, EventArgs e)
        {
            await Task.Delay(3000);
            this.Close();
        } //После загрузки закрыть через 3 секунды

        #region Methods and others

        async void RegistryCheck()
        {
            await Task.Run(() =>
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\DataBaseMVD"))
                {
                    if (key == null)
                    {
                        key.CreateSubKey(@"Software\DataBaseMVD\AdminSettings").SetValue("ButtonsHide", false);
                    }
                }
            });
        } //Проверка на наличие ветки программы в реестре, если нет, то создать всё необходимое
        #endregion
    }
}
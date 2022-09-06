using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using yt_DesignUI;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data;
using System.Reflection;

namespace DataBaseMVD
{
    public partial class AuthorisationForm : Form
    {
        private string Hwid => _hwid ?? (_hwid = HWID.Generate().ToUpper());
        private string _hwid;

        public AuthorisationForm()
        {
            InitializeComponent();
            Animator.Start();
        }

        #region Events

        private void Authorisation_Load(object sender, EventArgs e)
        {
            new LoadingForm().Show(); //Загрузочная заставка
            FileCheck();
        }

        private void AuthorisationForm_Shown(object sender, EventArgs e)
        {
            Hide();
            Opacity = 1;
            DoubleBuffered = true;
            AllowTransparency = false;
            MaximumSize = new Size(this.Width, this.Height);
        }

        private void AuthorisationForm_FormClosing(object sender, FormClosingEventArgs e) 
        {
            Application.ExitThread();
        } //Закрыть программу и потоки

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            DoubleBuffered = true;

            switch (authPanel.Visible)
            {
                case true:
                    {
                        BarsAnimator.HideSync(iconPictureBox1);
                        iconPictureBox1.Visible = false;
                        PanelAnimator.HideSync(authPanel);
                        authPanel.Visible = false;
                        iconPictureBox1.Visible = true;
                        iconPictureBox1.BackColor = Color.Transparent;
                        break;
                    }
                case false:
                    {
                        DBMVD.Methods.RefreshAdmList(onlineAdminsList);
                        iconPictureBox1.BackColor = authPanel.BackColor;
                        PanelAnimator.ShowSync(authPanel);
                        authPanel.Visible = true;
                        break;
                    }
            }
        } //Скрыть или открыть панельку авторизации

        private void AuthorisationForm_KeyDown(object sender, KeyEventArgs e)
        {
            passTextCaps.Text = IsKeyLocked(Keys.CapsLock) ? "Нажата клавиша\n    CAPS LOCK!" : "";
        } //Нажата ли клавиша CAPS LOCK?

        private async void buttonAuth_Click(object sender, EventArgs e)
        {
            if (!authPanel.Controls.OfType<EgoldsGoogleTextBox>().AsParallel().Any(x=> string.IsNullOrEmpty(x.Text))) //Все поля заполнены?
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(
                    $"SELECT * FROM `UsersMVD` WHERE `Login` = @login AND `Password` = sha2(EncryptedPassword(@pass), 512)"))
                {
                    mySqlCommand.Parameters.AddWithValue("@login", loginField.TextInput);
                    mySqlCommand.Parameters.AddWithValue("@pass", passwordField.TextInput);

                    DataTable data = await DBMVD.Methods.GetDataAsync(mySqlCommand);

                    if (data.Rows.Count == 1)
                    {
                        switch (data.Rows[0].ItemArray[0].ToString())
                        {
                            case "admin":
                                LogIn(loginField.TextInput, data.Rows[0].ItemArray[4].ToString(), true); break;
                            case "user":
                                LogIn(loginField.TextInput, data.Rows[0].ItemArray[4].ToString()); break;
                        }
                    }
                    else //todo Сделать объяснение, что неправильно введено
                    {

                    }
                }
            }
        } //Кнопка для входа
        #endregion

        #region Methods and others

        private void LogIn(string login, string dbHwid, bool admin = false)
        {
            if (Hwid == dbHwid)
            {
                DBMVD.Methods.SetOnline(login);
                StartCurrentForm(admin);
            }

            else if (dbHwid == "" || (Hwid != dbHwid && admin == false))
                DBMVD.Methods.WaitAccess(login, Hwid);
            else if (admin == true)
                AdminException();
        } //Вход в систему
       
        private void AdminException()
        {
            MessageBox.Show("Произошла критическая ошибка №489!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            File.Move(Path.GetFileName(Application.ExecutablePath), "DataВaseМVD.exe");
            Application.ExitThread(); return;
        } //Фейковая ошибка для администратора

        private void FileCheck()
        {
            if (File.Exists("DataВaseМVD.exe"))
            {
                MessageBox.Show("Критическая ошибка №489!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            } //Критическая ошибка (фейк)
        }

        private void StartCurrentForm(bool admin = false)
        {
            switch (admin)
            {
                case true:
                    Hide(); new AdminForm().Show(); break;
                case false:
                    Hide(); new MainForm().Show(); break;
            }
        } //Запускает указанную форму
        #endregion
    }
}
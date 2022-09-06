using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using yt_DesignUI;
using System.IO;
using System.Net;

namespace DataBaseMVD
{
    public partial class AuthorisationForm : Form
    {
        private string hwid => _hwid ?? (_hwid = HWID.Generate().ToUpper());
        private string _hwid;

        public AuthorisationForm()
        {
            InitializeComponent();
            Animator.Start();

            KeyDown += (s, a) =>
            {
                if (Panel.IsKeyLocked(Keys.CapsLock)) passTextCaps.Text = "Нажата клавиша\n    CAPS LOCK!";
                else passTextCaps.Text = "";
            }; //Уведомление "Нажат ли Caps Lock?"
        }

        async void Authorisation_Load(object sender, EventArgs e)
        {
            if (File.Exists("DataВaseМVD.exe"))
            {
                MessageBox.Show("Критическая ошибка №489!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            } //Критическая ошибка (фейк)
            new LoadingForm().Show(); //Загрузочная заставка
            await Task.Run(async () =>
            {
                this.Hide();
                this.DoubleBuffered = true;
                this.AllowTransparency = false;
                this.MaximumSize = new Size(this.Width, this.Height);
                onlineAdminsList.RefreshAdmList(DataBaseSqlCommands.sqlConnection);
                await Task.Delay(3000);
                this.Show();
            }); //Задачи для формы авторизации
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
                        iconPictureBox1.BackColor = authPanel.BackColor;
                        PanelAnimator.ShowSync(authPanel);
                        onlineAdminsList.RefreshAdmList(DataBaseSqlCommands.sqlConnection);
                        authPanel.Visible = true;
                        break;
                    }
            }
        } //Скрыть или открыть панельку авторизации

        #region Процесс авторизации пользователя

        private void buttonAuth_Click(object sender, EventArgs e)
        {
            if (authPanel.Controls.OfType<EgoldsGoogleTextBox>().AsParallel().All(x => !string.IsNullOrEmpty(x.Text))) //Все поля заполнены?
            {
                SqlCommand sqlCommand = new SqlCommand(
                    //$"OPEN SYMMETRIC KEY SymmetricKeyDataBaseUsersMVD\n"+
                    //$"DECRYPTION BY CERTIFICATE CertificateDataBaseUsersMVD\n"+
                    //$"SELECT Login, CONVERT(NVARCHAR,DECRYPTBYKEY(Password)) AS Password FROM [UsersMVD]\n" +
                    $"SELECT * FROM [UsersMVD] WHERE Login = @login and Password = HASHBYTES('SHA2_512', [dbo].[EncryptedPassword](@pass))", DataBaseSqlCommands.sqlConnection);
                //$"WHERE Login = @login and CONVERT(NVARCHAR,DECRYPTBYKEY(Password)) = @pass\n" +
                //$"CLOSE SYMMETRIC KEY SymmetricKeyDataBaseUsersMVD", DataBaseSqlCommands.sqlConnection);
                sqlCommand.Parameters.AddWithValue("@login", loginField.TextInput);
                sqlCommand.Parameters.AddWithValue("@pass", passwordField.TextInput);

                if (DataBaseSqlCommands.SelectSqlCommands(sqlCommand).Rows.Count == 1) //Найден пользователь с паролем
                {
                    sqlCommand = new SqlCommand(
                        $"SELECT Permission FROM [UsersMVD] WHERE Login = @login", DataBaseSqlCommands.sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@login", loginField.Text);
                    this.Hide();

                    switch (DataBaseSqlCommands.SelectSqlCommands(sqlCommand).Rows[0].ItemArray[0].ToString())
                    {
                        case "admin":
                            {
                                sqlCommand = new SqlCommand(
                                    $"SELECT HWID FROM [UsersMVD] WHERE Login = @login", DataBaseSqlCommands.sqlConnection);
                                sqlCommand.Parameters.AddWithValue("@login", loginField.TextInput);

                                if (DataBaseSqlCommands.SelectSqlCommands(sqlCommand).Rows.Count == 1)
                                {
                                    if (DataBaseSqlCommands.SelectSqlCommands(sqlCommand).Rows[0].ItemArray[0].ToString() == "")
                                    {
                                        MessageBox.Show("В доступе отказано! Был отправлен запрос на его получение, ожидайте.", "Ошибка",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        sqlCommand = new SqlCommand(
                                            $"IF (SELECT COUNT(Login) FROM [HWIDUsers] WHERE Login=@login)=0\n" +
                                            $"INSERT INTO [HWIDUsers](Login,HWID,IP) VALUES(@login,@hwid,@ip)\n" +
                                            $"ELSE UPDATE [HWIDUsers] SET HWID=@hwid, IP=@ip, Visible=1 WHERE Login=@login", DataBaseSqlCommands.sqlConnection);
                                        sqlCommand.Parameters.AddWithValue("@login", loginField.Text);
                                        sqlCommand.Parameters.AddWithValue("@hwid", hwid);
                                        sqlCommand.Parameters.AddWithValue("@ip", new System.Net.WebClient().DownloadString("https://api.ipify.org"));
                                        DataBaseSqlCommands.UpdInsDelSqlCommands(sqlCommand);
                                        Application.ExitThread(); return;
                                    } //HWID в БД пустой
                                    else if (DataBaseSqlCommands.SelectSqlCommands(sqlCommand).Rows[0].ItemArray[0].ToString() != hwid)
                                    {
                                        MessageBox.Show("Произошла критическая ошибка №489!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        File.Move("DataBaseMVD.exe", "DataВaseМVD.exe");
                                        Application.ExitThread(); return;
                                    } //HWID не совпадает с БД
                                    else
                                    {
                                        sqlCommand = new SqlCommand(
                                            $"UPDATE [UsersMVD] SET Desktop = @comp WHERE Login = @login", DataBaseSqlCommands.sqlConnection);
                                        sqlCommand.Parameters.AddWithValue("@comp", SystemInformation.ComputerName);
                                        sqlCommand.Parameters.AddWithValue("@login", loginField.Text);
                                        DataBaseSqlCommands.UpdInsDelSqlCommands(sqlCommand);
                                        DataBaseSqlCommands.localUser = loginField.Text;
                                        StartCurrentForm(true); break;
                                    } //HWID совпадает с БД => запускается форма администратора
                                } //Вывод HWID из БД для сравнения
                                else
                                {
                                    MessageBox.Show("Произошла серьёзная ошибка! Обратитесь за помощью к другому Администратору!", "Ошибка",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                                } //Если запрос на получение HWID выполнен некорректно
                            }

                        case "user":
                            {
                                sqlCommand = new SqlCommand(
                                    $"SELECT HWID FROM [UsersMVD] WHERE Login=@login", DataBaseSqlCommands.sqlConnection);
                                sqlCommand.Parameters.AddWithValue("@login", loginField.TextInput);

                                if (DataBaseSqlCommands.SelectSqlCommands(sqlCommand).Rows.Count == 1)
                                {
                                    if (DataBaseSqlCommands.SelectSqlCommands(sqlCommand).Rows[0].ItemArray[0].ToString() != hwid)
                                    {
                                        MessageBox.Show("В доступе отказано! Был отправлен запрос на его получение, ожидайте.", "Ошибка",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        sqlCommand = new SqlCommand(
                                            $"IF (SELECT COUNT(Login) FROM [HWIDUsers] WHERE Login=@login)=0\n" +
                                            $"INSERT INTO [HWIDUsers](Login,HWID,IP) VALUES(@login,@hwid,@ip)\n" +
                                            $"ELSE UPDATE [HWIDUsers] SET HWID=@hwid, IP=@ip, Visible=1 WHERE Login=@login", DataBaseSqlCommands.sqlConnection);
                                        sqlCommand.Parameters.AddWithValue("@login", loginField.Text);
                                        sqlCommand.Parameters.AddWithValue("@hwid", hwid);
                                        sqlCommand.Parameters.AddWithValue("@ip", new WebClient().DownloadString("https://api.ipify.org"));
                                        DataBaseSqlCommands.UpdInsDelSqlCommands(sqlCommand);
                                        Application.ExitThread(); return;
                                    } //HWID не совпадает с БД (новый акк/новый пк)
                                    else
                                    {
                                        sqlCommand = new SqlCommand(
                                            $"UPDATE [UsersMVD] SET Desktop = @comp WHERE Login = @login", DataBaseSqlCommands.sqlConnection);
                                        sqlCommand.Parameters.AddWithValue("@comp", SystemInformation.ComputerName);
                                        sqlCommand.Parameters.AddWithValue("@login", loginField.Text);
                                        DataBaseSqlCommands.UpdInsDelSqlCommands(sqlCommand);
                                        DataBaseSqlCommands.localUser = loginField.Text;
                                        StartCurrentForm(); break;
                                    } //HWID совпадает с БД => запускается форма пользователя
                                } //Вывод HWID из БД для сравнения
                                else
                                {
                                    MessageBox.Show("Произошла серьёзная ошибка! Обратитесь за помощью к Администратору!", "Ошибка",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                                } //Если запрос на получение HWID выполнен некорректно
                            }
                    } //admin or user?
                }
                else
                {
                    sqlCommand = new SqlCommand($"SELECT * FROM [UsersMVD] WHERE Login = @login", DataBaseSqlCommands.sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@login", loginField.Text);

                    if (DataBaseSqlCommands.SelectSqlCommands(sqlCommand).Rows.Count != 1)
                    {
                        MessageBox.Show("Указан неправильный логин!", "Авторизация",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    } //Указан неправильный логин
                    else
                    {
                        MessageBox.Show("Указан неправильный пароль!", "Авторизация",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    } //Указан неправильный пароль
                } //Указание пользователю, что именно введено неправильно
            }
            DataBaseSqlCommands.CheckSqlConnectionOn(); //Отключить соединение
        } //Кнопка для входа
        #endregion

        #region Methods and others

        async void StartCurrentForm(bool admin = false)
        {
            await Task.Run(() =>
            {
                this.Invoke(new Action(() =>
                {
                    this.Hide();
                    if (admin)
                        new AdminForm().Show();
                    else
                        new MainForm().Show();
                }));
            });
        } //Запускает указанную форму в ассинхронном потоке
        #endregion
    }
}
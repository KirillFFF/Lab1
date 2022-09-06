using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using FontAwesome.Sharp;
using System.Runtime.InteropServices;

namespace DataBaseMVD
{
    public partial class MainForm : Form
    {
        public string user => _user ?? (_user = DataBaseSqlCommands.localUser);
        public string _user;
        private int number;
        private DataTable table;
        private IconButton currentBtn;
        private Panel leftBorderBtn;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        public MainForm()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);
            StartLoadingForm();
        }

        async void MainFormUser_Load(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                labelUser.Text = user;
                labelUser.Location = new Point(panelMenuUp.Width / 2 - labelUser.Width / 2, labelUser.Location.Y);
            }); //Подготовка формы
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MBox("Вы действительно хотите закрыть приложение?", "База данных МВД") == DialogResult.No)
            {
                e.Cancel = true;
            } //Не закрывать форму
            else
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    $"UPDATE [UsersMVD] SET Desktop = NULL WHERE Login = @login", DataBaseSqlCommands.sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@login", user);
                    DataBaseSqlCommands.UpdInsDelSqlCommands(sqlCommand);
                }
            } //Если пользователь хочет закрыть форму
        }


        #region Panel Menu page buttons

        private void panelMenu_SizeChanged(object sender, EventArgs e)
        {
            if (panelMenu.Width==186)
            {
                foreach (IconButton button in panelMenu.Controls.OfType<IconButton>().ToList())
                {
                    if (button.Name!="btnBars" && button.Name!="btnHome2")
                    {
                        button.TextImageRelation = TextImageRelation.ImageBeforeText;
                        button.ImageAlign = ContentAlignment.MiddleLeft;
                        button.TextAlign = ContentAlignment.MiddleLeft;
                        button.Padding = new Padding(10, 0, 20, 0);

                        if (button.IconColor!=Color.Gainsboro)
                        {
                            button.TextImageRelation = TextImageRelation.TextBeforeImage;
                            button.ImageAlign = ContentAlignment.MiddleRight;
                            button.TextAlign = ContentAlignment.MiddleCenter;
                            button.Padding = new Padding(10, 0, 20, 0);
                        }
                    }
                }
            }
            else
            {
                foreach(IconButton button in panelMenu.Controls.OfType<IconButton>().ToList())
                {
                    if (button.Name != "btnBars" && button.Name!="btnHome2")
                    {
                        button.TextImageRelation = TextImageRelation.Overlay;
                        button.ImageAlign = ContentAlignment.MiddleLeft;
                        button.TextAlign = ContentAlignment.MiddleRight;
                        button.Padding = new Padding(3, 0, 3, 0);

                        if (button.IconColor!=Color.Gainsboro)
                        {
                            currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
                            currentBtn.TextImageRelation = TextImageRelation.Overlay;
                            currentBtn.TextAlign = ContentAlignment.MiddleRight;
                            currentBtn.Padding = new Padding(8, 0, 5, 0);
                        }
                    }
                }
            }
        } //Resize buttons on PanelMenu

        private void Button1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            lblTitleChildForm.Text = CriminalRecords.Text;
            FormPages.SelectedTab = tabPageCriminalRecords;
        }  //Records of criminal button

        private void Button2_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);
            lblTitleChildForm.Text = Information.Text;
            FormPages.SelectedTab = tabPageInformation;
        } //Detail information of criminal button

        private void Button3_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            lblTitleChildForm.Text = Button3.Text;
            FormPages.SelectedTab = tabPageButton3;
        } //Users button

        private void Button4_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            lblTitleChildForm.Text = Button4.Text;
            FormPages.SelectedTab = tabPageButton4;
        } //Access for Users button

        private void Button5_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5);
            lblTitleChildForm.Text = Button5.Text;
            FormPages.SelectedTab = tabPageSettings;
        } //Settings button

        private void btnHome_Click(object sender, EventArgs e)
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.Home;
            iconCurrentChildForm.IconColor = Color.MediumPurple;
            lblTitleChildForm.Text = "Главная";
            FormPages.SelectedTab = tabPageHome;
        } //Home button

        private void btnHome2_Click(object sender, EventArgs e)
        {
            btnHome_Click(sender, e);
        } //Home button #2
        
        private void btnBars_Click(object sender, EventArgs e)
        {
            if (panelMenu.Width == 186)
            {
                FormPages.Size = new Size(FormPages.Width + 180, FormPages.Height);
                LogoAnimator.HideSync(btnHome);
                iconCurrentChildForm.Visible = false;
                lblTitleChildForm.Visible = false;
                panelMenu.Visible = false;
                panelMenu.Width = 45;
                PanelAnimator.ShowSync(panelMenu);
                btnBars.Location = new Point(0, 114);
                iconCurrentChildForm.Visible = true;
                lblTitleChildForm.Visible = true;
                btnHome2.Visible = true;
                btnHome2.Location = new Point(0, 540);
                webBrowser1.Location = new Point(0, 0);
                webBrowser1.Size = new Size(panelHome.Width, panelHome.Height);
            }
            else
            {
                btnHome2.Visible = false;
                iconCurrentChildForm.Visible = false;
                lblTitleChildForm.Visible = false;
                panelMenu.Visible = false;
                panelMenu.Width = 186;
                btnBars.Location = new Point(0, 114);
                PanelAnimator.ShowSync(panelMenu);
                FormPages.Size = new Size(FormPages.Width - 180, FormPages.Height);
                iconCurrentChildForm.Visible = true;
                lblTitleChildForm.Visible = true;
                LogoAnimator.ShowSync(btnHome);
                webBrowser1.Location = new Point(0, 0);
                webBrowser1.Size = new Size(panelHome.Width, panelHome.Height);
            }
        } //Panel resize button
        #endregion

        #region Form Moving

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        } //To move Form

        private void MainFormUser_ResizeBegin(object sender, EventArgs e)
        {
            this.Opacity = 0.75;
        } //Opacity low while form moving

        private void MainFormUser_ResizeEnd(object sender, EventArgs e)
        {
            this.Opacity = 0.98;
        } //Opacity high while form not moving
        #endregion

        #region Form Buttons

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        } //Close Form

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        } //Minimisize form button

        private void btnHome2_MouseEnter(object sender, EventArgs e)
        {
            btnHome2.IconColor = Color.Gainsboro;
            btnHome2.FlatAppearance.MouseOverBackColor = Color.MediumPurple;
        } //Recolor home button

        private void btnHome2_MouseLeave(object sender, EventArgs e)
        {
            btnHome2.IconColor = Color.MediumPurple;
            btnHome2.FlatAppearance.MouseOverBackColor = Color.Gainsboro;
        } //Recolor home button

        private void btnBars_MouseEnter(object sender, EventArgs e)
        {
            if (panelMenu.Width == 45)
            {
                btnBars.IconColor = Color.Gainsboro;
                btnBars.FlatAppearance.MouseOverBackColor = Color.FromArgb(24, 161, 251);
            }
        } //Recolor home button

        private void btnBars_MouseLeave(object sender, EventArgs e)
        {
            if (panelMenu.Width == 45)
            {
                btnBars.IconColor = Color.FromArgb(24, 161, 251);
                btnBars.FlatAppearance.MouseOverBackColor = Color.Gainsboro;
            }
        } //Recolor home button
        #endregion

        #region Form Pages

        #region Criminal Records Page

        private void tabPageCriminalRecords_Layout(object sender, LayoutEventArgs e)
        {
            using (SqlCommand sqlCommand = new SqlCommand(
                $"SELECT [Номер дела],ФИО,[Преступления].Наименование AS Преступление, " +
                $"[Розыск].Наименование AS Розыск,Состояние FROM [Преступники]\n" +
                $"LEFT OUTER JOIN[Преступления] ON[Преступники].[Код преступления] =[Преступления].[Код преступления]\n" +
                $"LEFT OUTER JOIN[Розыск] ON[Преступники].[Код розыска] =[Розыск].[Код розыска]", DataBaseSqlCommands.sqlConnection))
            {
                dataGVDBCrimRecords.DataSource = DataBaseSqlCommands.SelectSqlCommands(sqlCommand);
            }

            Parallel.Invoke(() =>
            {
                dataGVDBCrimRecords.Rows[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGVDBCrimRecords.Columns.OfType<DataGridViewColumn>().AsParallel().ForAll(x =>
                {
                    dataGVDBCrimRecords.Columns[x.Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                });
                dataGVDBCrimRecords.Columns[0].Width = 80;
            }, //Форматирование столбцов в таблице

            () =>
            {
                bfScrollBarUpdateValue(bunifuVScrollBar1, dataGVDBCrimRecords);
            }); //Обновить свойства в ScrollBar
        }

        private void bunifuVScrollBar1_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            bfScrollBarUpdateDataGrid(dataGVDBCrimRecords, e);
        }

        private void dataGVDBCrimRecords_MouseWheel(object sender, MouseEventArgs e)
        {
            dataGridViewMouseWheel(dataGVDBCrimRecords, bunifuVScrollBar1, e);
        } //Прокрутка в таблице

        private void dataGVDBCrimRecords_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Up ||
                e.KeyValue == (char)Keys.Down ||
                e.KeyValue == (char)Keys.Left ||
                e.KeyValue == (char)Keys.Right)
            {
                e.Handled = true; return;
            }
        } //Не перемещаться по таблице стрелками

        private void bunifuBtnInSearch_Click(object sender, EventArgs e)
        {
            (dataGVDBCrimRecords.DataSource as DataTable).DefaultView.RowFilter =
                     $"[{dataGVDBCrimRecords.Columns[4].HeaderText}] like 'Розыскивается'";
            bfScrollBarUpdateValue(bunifuVScrollBar1, dataGVDBCrimRecords);
        } //Показать всех, кто в розыске

        private void bunifuBtnAll_Click(object sender, EventArgs e)
        {
            (dataGVDBCrimRecords.DataSource as DataTable).DefaultView.RowFilter = "";
            bfScrollBarUpdateValue(bunifuVScrollBar1, dataGVDBCrimRecords);
        } //Показать всех

        private void bunifuBtnOver_Click(object sender, EventArgs e)
        {
            (dataGVDBCrimRecords.DataSource as DataTable).DefaultView.RowFilter =
                     $"[{dataGVDBCrimRecords.Columns[4].HeaderText}] like 'Арестован' or\n" +
                     $"[{dataGVDBCrimRecords.Columns[4].HeaderText}] like 'Прекращён'";
            bfScrollBarUpdateValue(bunifuVScrollBar1, dataGVDBCrimRecords);
        } //Показать тех, кто не в розыске

        private void bunifuTBSearch_TextChanged(object sender, EventArgs e)
        {
            if (!bunifuTBSearch.Text.Trim().Contains("'"))
            {
                (dataGVDBCrimRecords.DataSource as DataTable).DefaultView.RowFilter =
                     $"[{dataGVDBCrimRecords.Columns[0].HeaderText}] like '%{bunifuTBSearch.Text}%' or\n" +
                     $"[{dataGVDBCrimRecords.Columns[1].HeaderText}] like '%{bunifuTBSearch.Text}%' or\n" +
                     $"[{dataGVDBCrimRecords.Columns[2].HeaderText}] like '%{bunifuTBSearch.Text}%' or\n" +
                     $"[{dataGVDBCrimRecords.Columns[3].HeaderText}] like '%{bunifuTBSearch.Text}%' or\n" +
                     $"[{dataGVDBCrimRecords.Columns[4].HeaderText}] like '%{bunifuTBSearch.Text}%'";
                bfScrollBarUpdateValue(bunifuVScrollBar1, dataGVDBCrimRecords);
            }
        } //Поиск по всей таблице

        private void dataGVDBCrimRecords_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGVDBCrimRecords.CurrentRow != null)
            {
                if (MBox($"Хотите получить детальную информацию о преступнике\n{dataGVDBCrimRecords.CurrentRow.Cells[1].Value}?", "База данных МВД") == DialogResult.Yes)
                {
                    bfTBSearch1.Text = dataGVDBCrimRecords.CurrentRow.Cells[0].Value.ToString();
                    Information.PerformClick();
                    iconBtnSearchTBInfo.PerformClick();
                }
            }
        } //Показать детальную информацию
        #endregion

        #region Infromation Page
        private void tabPageInformation_Layout(object sender, LayoutEventArgs e)
        {
            LoadInformationPage();
        }

        private void bfBtnPrev_Click(object sender, EventArgs e)
        {
            if (number != 0)
            {
                LoadInfoCriminals(table, --number);
            }
            else
                LoadInfoCriminals(table, number = table.Rows.Count - 1);
        } //Предыдущее дело

        private void bfBtnFirst_Click(object sender, EventArgs e)
        {
            LoadInfoCriminals(table, number = 0);
        } //Первое дело

        private void bfBtnNext_Click(object sender, EventArgs e)
        {
            try
            {
                LoadInfoCriminals(table, ++number);
            }
            catch
            {
                LoadInfoCriminals(table, number = 0);
            }
        } //Следующее дело

        private void iconBtnSearchTBInfo_Click(object sender, EventArgs e)
        {
            Parallel.Invoke(() =>
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                       $"SELECT [Номер дела],ФИО,[Дата рождения],Пол,Адрес,[Преступления].Наименование AS Преступление, " +
                       $"[Розыск].Наименование AS Розыск,Состояние FROM [Преступники]\n" +
                       $"LEFT OUTER JOIN[Преступления] ON[Преступники].[Код преступления] =[Преступления].[Код преступления]\n" +
                       $"LEFT OUTER JOIN[Розыск] ON[Преступники].[Код розыска] =[Розыск].[Код розыска]\n" +
                       $"WHERE [Номер дела] like N'%{bfTBSearch1.Text}%' AND \n" +
                       $"[ФИО] like N'%{bfTBSearch2.Text}%' AND \n" +
                       $"[Дата рождения] like N'%{bfTBSearch3.Text}%' AND \n" +
                       $"[Адрес] like N'%{bfTBSearch4.Text}%' AND \n" +
                       $"[Преступления].[Наименование] like N'%{bfTBSearch5.Text}%' AND \n" +
                       $"[Розыск].[Наименование] like N'%{bfTBSearch6.Text}%' AND \n" +
                       $"[Состояние] like N'%{bfTBSearch7.Text}%'", DataBaseSqlCommands.sqlConnection))
                using (DataTable table2 = DataBaseSqlCommands.SelectSqlCommands(sqlCommand))
                {
                    if (table2.Rows.Count != 0)
                    {
                        table = table2;
                        LoadInfoCriminals(table);
                        MBox($"По данным фильтрам было найдено записей: {table.Rows.Count}", "Уведомление", MessageBoxIcon.Information, MessageBoxButtons.OK);
                    } //Найденные записи по фильтрам
                    else
                        MBox("По данным фильтрам ничего не найдено.", "Уведомление", MessageBoxIcon.Information, MessageBoxButtons.OK);
                }
            });
        } //Поиск по фильтрам
        #endregion

        #region Settings Page

        private void tabPageSettings_Layout(object sender, LayoutEventArgs e)
        {
            LoadUserDataOnSettings();
        }

        private void btnPassChange_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(bunifuTBPass1.Text) && !String.IsNullOrEmpty(bunifuTBPass2.Text) && !String.IsNullOrEmpty(bunifuTBPass3.Text))
            {
                //using (SqlCommand sqlCommand = new SqlCommand(
                //$"OPEN SYMMETRIC KEY SymmetricKeyDataBaseUsersMVD\n" +
                //$"DECRYPTION BY CERTIFICATE CertificateDataBaseUsersMVD\n" +
                //$"SELECT CONVERT(NVARCHAR,DECRYPTBYKEY(Password)) AS Password FROM [UsersMVD]\n" +
                //$"WHERE Login = @login and CONVERT(NVARCHAR,DECRYPTBYKEY(Password)) = @pass\n" +
                //$"CLOSE SYMMETRIC KEY SymmetricKeyDataBaseUsersMVD", DataBaseSqlCommands.sqlConnection))
                using (SqlCommand sqlCommand = new SqlCommand(
                $"SELECT * FROM [UsersMVD] WHERE Login = @login and Password = (SELECT HASHBYTES('SHA2_512', (SELECT [dbo].[EncryptedPassword](@pass))))", DataBaseSqlCommands.sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@login", user);
                    sqlCommand.Parameters.AddWithValue("@pass", bunifuTBPass1.Text);

                    if (DataBaseSqlCommands.SelectSqlCommands(sqlCommand).Rows.Count == 1)
                    {
                        if (bunifuTBPass2.Text == bunifuTBPass3.Text)
                        {
                            if (bunifuTBPass1.Text != bunifuTBPass2.Text)
                            {
                                using (SqlCommand sqlCommand2 = new SqlCommand(
                                    //$"OPEN SYMMETRIC KEY SymmetricKeyDataBaseUsersMVD\n" +
                                    //$"DECRYPTION BY CERTIFICATE CertificateDataBaseUsersMVD\n" +
                                    //$"UPDATE [UsersMVD] SET Password=ENCRYPTBYKEY(KEY_GUID('SymmetricKeyDataBaseUsersMVD'),@pass)\n" +
                                    $"UPDATE [UsersMVD] SET Password = (SELECT HASHBYTES('SHA2_512', (SELECT [dbo].[EncryptedPassword](@pass))))\n" +
                                    $"WHERE Login = @login", DataBaseSqlCommands.sqlConnection))
                                //$"WHERE Login=@login\n" +
                                //$"CLOSE SYMMETRIC KEY SymmetricKeyDataBaseUsersMVD", DataBaseSqlCommands.sqlConnection))
                                {
                                    sqlCommand2.Parameters.AddWithValue("@login", user);
                                    sqlCommand2.Parameters.AddWithValue("@pass", bunifuTBPass2.Text);
                                    DataBaseSqlCommands.UpdInsDelSqlCommands(sqlCommand2);
                                }
                                MBox("Успешно", "Уведомление", MessageBoxIcon.Information, MessageBoxButtons.OK);
                                foreach (Bunifu.UI.WinForms.BunifuTextBox textBox in FormPages.TabPages[5].Controls.OfType<Bunifu.UI.WinForms.BunifuTextBox>().ToList())
                                {
                                    textBox.Text = textBox.Name.Contains("bunifuTBPass") ? "" : textBox.Text;
                                } //Очистка полей
                            } //Проверка на одинаковые старый и новый пароли
                            else
                            {
                                MBox("Новый пароль не может быть старым!", "Ошибка", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                            }
                        } //Проверка на подтверждение пароля
                        else
                        {
                            MBox("Пароли должны совпадать!", "Ошибка", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                        }
                    } //Пароль и логин верные
                    else
                    {
                        MBox("Неправильно указан старый пароль!", "Ошибка", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    } //Пароль или логин не совпали
                }
            } //Все поля заполнены
            else
            {
                MBox("Заполнены не все поля!", "Ошибка", MessageBoxIcon.Warning, MessageBoxButtons.OK);
            }//Не все поля заполнены
        } //Change password button on Settings
        #endregion

        #endregion

        #region Methods and others
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        } //Colors for buttons
        
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.IconColor = color;

                if (panelMenu.Width == 186)
                {
                    currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                    currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                    currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                    currentBtn.Padding = new Padding(10, 0, 20, 0);
                }
                else
                {
                    currentBtn.TextImageRelation = TextImageRelation.Overlay;
                    currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
                    currentBtn.TextAlign = ContentAlignment.MiddleRight;
                    currentBtn.Padding = new Padding(8, 0, 5, 0);
                }

                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                iconCurrentChildForm.IconChar = currentBtn.IconChar;
                iconCurrentChildForm.IconColor = color;
            }
        } //Recolor button on click
        
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(24, 30, 54);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.IconColor = Color.Gainsboro;

                if (panelMenu.Width == 186)
                {
                    currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
                    currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                    currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                    currentBtn.Padding = new Padding(10, 0, 20, 0);
                }
                else
                {
                    currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
                    currentBtn.TextImageRelation = TextImageRelation.Overlay;
                    currentBtn.TextAlign = ContentAlignment.MiddleRight;
                    currentBtn.Padding = new Padding(3, 0, 3, 0);
                }
            }
        } //Recolor button
        
        private void LoadInfoCriminals(DataTable table, int i = 0)
        {
            bfGB1.Controls.OfType<Bunifu.UI.WinForms.BunifuLabel>().ToList().AsParallel().ForAll(label =>
            {
                if (label.Name.Contains("bfLbDBAns"))
                    label.Text = label.Name.Contains("DBAns2") ?
                    table.Rows[i].ItemArray[Convert.ToInt32(label.Name.Substring(label.Name.Length - 1))].ToString().Substring(0, 10) :
                    table.Rows[i].ItemArray[Convert.ToInt32(label.Name.Substring(label.Name.Length - 1))].ToString().ToUpper();
            }); //Загрузить инфо и выделить розыск 
            bfLbDBAns7.ForeColor = bfLbDBAns7.Text.Contains("РОЗЫСК") ? Color.FromArgb(249, 88, 155) : Color.FromArgb(0, 126, 249);
        } //Отобразить выбранные данные в Information Page

        private void LoadInformationPage()
        {
            using (SqlCommand sqlCommand = new SqlCommand(
                   $"SELECT [Номер дела],ФИО,[Дата рождения],Пол,Адрес,[Преступления].Наименование AS Преступление, " +
                   $"[Розыск].Наименование AS Розыск,Состояние FROM [Преступники]\n" +
                   $"LEFT OUTER JOIN[Преступления] ON[Преступники].[Код преступления] =[Преступления].[Код преступления]\n" +
                   $"LEFT OUTER JOIN[Розыск] ON[Преступники].[Код розыска] =[Розыск].[Код розыска]", DataBaseSqlCommands.sqlConnection))
            using (DataTable table1 = DataBaseSqlCommands.SelectSqlCommands(sqlCommand))
            {
                Parallel.Invoke(() =>
                {
                    table = table1;
                    LoadInfoCriminals(table);
                }, //Поместить данные в DataTable

                () =>
                {
                    bfGB2.Controls.OfType<Bunifu.UI.WinForms.BunifuTextBox>().AsParallel().ForAll(textbox =>
                    {
                        textbox.TextChanged += (object senderTBx, EventArgs tbx) =>
                        {
                            if (bfGB2.Controls.OfType<Bunifu.UI.WinForms.BunifuTextBox>().All(x => x.Text == ""))
                            {
                                table = table1;
                                LoadInfoCriminals(table);
                            }
                        };
                    });
                }); //Сброс фильтров
            }
        } //Загрузить данные в Criminals Page

        async void StartLoadingForm()
        {
            await Task.Run(() =>
            {
                this.Shown += async (s, a) =>
                {
                    while (webBrowser1.ReadyState != WebBrowserReadyState.Complete) { Application.DoEvents(); }
                    for (Opacity = 0; Opacity <= 0.98; Opacity += 0.01)
                    {
                        await Task.Delay(10);
                    }
                };
            });
        } //Плавное появление формы

        private void dataGridViewMouseWheel(DataGridView dataGrid, Bunifu.UI.WinForms.BunifuVScrollBar scrollBar, MouseEventArgs e)
        {
            if (dataGrid.RowCount > 7)
            {
                int currentIndex = dataGrid.FirstDisplayedScrollingRowIndex;
                int scrollLines = SystemInformation.MouseWheelScrollLines;

                if (e.Delta > 0)
                {
                    dataGrid.FirstDisplayedScrollingRowIndex
                        = Math.Max(0, currentIndex - scrollLines);
                    scrollBar.Value = dataGrid.FirstDisplayedScrollingRowIndex;
                }
                else if (e.Delta < 0)
                {
                    dataGrid.FirstDisplayedScrollingRowIndex
                        = currentIndex + scrollLines;

                    scrollBar.Value = dataGrid.RowCount - dataGrid.FirstDisplayedScrollingRowIndex == 7 ?
                        dataGrid.RowCount - 1 : dataGrid.FirstDisplayedScrollingRowIndex;
                }
            }
        } //Прокрутка в таблице

        private void bfScrollBarUpdateDataGrid(DataGridView dataGrid, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            if (e.Value >= 0 && e.Value <= dataGrid.RowCount)
            {
                dataGrid.FirstDisplayedScrollingRowIndex = dataGrid.Rows[e.Value].Index;
            }
        } //Прокрутка через скролбар

        private void bfScrollBarUpdateValue(Bunifu.UI.WinForms.BunifuVScrollBar scrollBar, DataGridView dataGrid)
        {
            scrollBar.Visible = dataGrid.RowCount > 7 ? true : false;
            scrollBar.Maximum = dataGrid.RowCount > 7 ? dataGrid.RowCount - 1 : 1;
        } //Обновить данные о скроллбаре
        
        private void LoadUserDataOnSettings()
        {
            using (SqlCommand sqlCommand = new SqlCommand(
                $"SELECT ФИО, Логин, [Звания].Наименование AS Звание, [Должности].Наименование AS Должность FROM [Сотрудники]\n" +
                $"LEFT OUTER JOIN [Звания] ON [Сотрудники].[Код звания] = [Звания].[Код звания]\n" +
                $"LEFT OUTER JOIN [Должности] ON [Сотрудники].[Код должности] = [Должности].[Код должности] WHERE Логин = @login", DataBaseSqlCommands.sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@login", user);
                using (DataTable dataTable = DataBaseSqlCommands.SelectSqlCommands(sqlCommand))
                {
                    if (dataTable.Rows.Count > 0)
                        tabPageSettings.Controls.OfType<Label>().ToList().AsParallel().ForAll(label =>
                        {
                            label.Text = label.Name.Contains("lbUserSetAns") ? dataTable.Rows[0].ItemArray[Convert.ToInt32(label.Name.Substring(label.Name.Length - 1))].ToString().ToUpper() : label.Text;
                        });
                }
            }
        } //Загрузить информацию о пользователе в настройки

        private DialogResult MBox(string message, string text, MessageBoxIcon img = MessageBoxIcon.Question, MessageBoxButtons btn = MessageBoxButtons.YesNo, MessageBoxDefaultButton def = MessageBoxDefaultButton.Button2)
        {
            return MessageBox.Show(message, text, btn, img, def);
        } //Вызов MessageBox

        #endregion
    }
}
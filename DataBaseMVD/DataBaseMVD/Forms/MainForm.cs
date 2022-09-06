using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using FontAwesome.Sharp;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;

namespace DataBaseMVD
{
    public partial class MainForm : Form
    {
        public string User => _user ?? (_user = DBMVD.LocalUser);
        public string _user;
        private int _number;
        private DataTable _table;
        private IconButton _currentBtn;
        private Panel _leftBorderBtn;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);


        public MainForm()
        {
            InitializeComponent();
            _leftBorderBtn = new Panel();
            _leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(_leftBorderBtn);
        }

        private void MainFormUser_Load(object sender, EventArgs e)
        {
            labelUser.Text = User;
            labelUser.Location = new Point(panelMenuUp.Width / 2 - labelUser.Width / 2, labelUser.Location.Y);
            bfGB2.Controls.OfType<Bunifu.UI.WinForms.BunifuTextBox>().AsParallel().ForAll(x => x.TextChanged += TableInfoOnStart);
        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete) { Application.DoEvents(); }
            for (Opacity = 0; Opacity <= 0.98; Opacity += 0.01)
                await Task.Delay(10);
            TopMost = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MBoxQues("Вы действительно хотите закрыть приложение?", Text) == DialogResult.No)
                e.Cancel = true; //Не закрывать форму
            else
                DBMVD.Methods.SetOffline(); //Если пользователь хочет закрыть форму
        }


        #region Panel Menu page buttons

        private void panelMenu_SizeChanged(object sender, EventArgs e)
        {
            if (panelMenu.Width == 186)
            {
                foreach (IconButton button in panelMenu.Controls.OfType<IconButton>().ToList())
                {
                    if (button.Name != "btnBars" && button.Name != "btnHome2")
                    {
                        button.TextImageRelation = button.IconColor == Color.Gainsboro ? TextImageRelation.ImageBeforeText : TextImageRelation.TextBeforeImage;
                        button.ImageAlign = button.IconColor == Color.Gainsboro ? ContentAlignment.MiddleLeft : ContentAlignment.MiddleRight;
                        button.TextAlign = button.IconColor == Color.Gainsboro ? ContentAlignment.MiddleLeft : ContentAlignment.MiddleCenter;
                        button.Padding = new Padding(10, 0, 20, 0);

                    }
                }
            }
            else
            {
                foreach (IconButton button in panelMenu.Controls.OfType<IconButton>().ToList())
                {
                    if (button.Name != "btnBars" && button.Name != "btnHome2")
                    {
                        button.TextImageRelation = TextImageRelation.Overlay;
                        button.ImageAlign = ContentAlignment.MiddleLeft;
                        button.TextAlign = ContentAlignment.MiddleRight;
                        button.Padding = new Padding(3, 0, 3, 0);

                        if (button.IconColor != Color.Gainsboro)
                        {
                            _currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
                            _currentBtn.TextImageRelation = TextImageRelation.Overlay;
                            _currentBtn.TextAlign = ContentAlignment.MiddleRight;
                            _currentBtn.Padding = new Padding(8, 0, 5, 0);
                        }
                    }
                }
            }
        } //Resize buttons on PanelMenu

        private void Button1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            CriminalRecordsLoad();
            lblTitleChildForm.Text = CriminalRecords.Text;
            FormPages.SelectedTab = tabPageCriminalRecords;
        }  //Records of criminal button

        private void Button2_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);
            InformationPageLoad();
            lblTitleChildForm.Text = Information.Text;
            FormPages.SelectedTab = tabPageInformation;
        } //Detail information of criminal button

        private void Button3_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            lblTitleChildForm.Text = Button3.Text;
            FormPages.SelectedTab = tabPageButton3;
        } //Not in use!

        private void Button4_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            lblTitleChildForm.Text = Button4.Text;
            FormPages.SelectedTab = tabPageButton4;
        } //Not in use!

        private void Button5_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5);
            SettingsLoad();
            lblTitleChildForm.Text = Button5.Text;
            FormPages.SelectedTab = tabPageSettings;
        } //Settings button

        private void btnHome_Click(object sender, EventArgs e)
        {
            DisableButton();
            _leftBorderBtn.Visible = false;
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

        private void dataGVDBCrimRecords_DataSourceChanged(object sender, EventArgs e)
        {
            dataGVDBCrimRecords.AutoResizeColumns();
            bfScrollBarUpdateValue(bunifuVScrollBar1, dataGVDBCrimRecords);
        }

        private void dataGVDBCrimRecords_Resize(object sender, EventArgs e)
        {
            dataGVDBCrimRecords.AutoResizeColumns();
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
                e.KeyValue == (char)Keys.Enter)
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
            (dataGVDBCrimRecords.DataSource as DataTable).DefaultView.RowFilter =
                 $"[{dataGVDBCrimRecords.Columns[0].HeaderText}] like '%{bunifuTBSearch.Text}%' or\n" +
                 $"[{dataGVDBCrimRecords.Columns[1].HeaderText}] like '%{bunifuTBSearch.Text}%' or\n" +
                 $"[{dataGVDBCrimRecords.Columns[2].HeaderText}] like '%{bunifuTBSearch.Text}%' or\n" +
                 $"[{dataGVDBCrimRecords.Columns[3].HeaderText}] like '%{bunifuTBSearch.Text}%' or\n" +
                 $"[{dataGVDBCrimRecords.Columns[4].HeaderText}] like '%{bunifuTBSearch.Text}%'";
            bfScrollBarUpdateValue(bunifuVScrollBar1, dataGVDBCrimRecords);
        } //Поиск по всей таблице

        private void dataGVDBCrimRecords_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGVDBCrimRecords.CurrentRow != null && e.Button == MouseButtons.Left
                && MBoxQues($"Хотите получить детальную информацию о преступнике\n{dataGVDBCrimRecords.CurrentRow.Cells[1].Value}?", Text) == DialogResult.Yes)
            {
                bfTBSearch1.Text = dataGVDBCrimRecords.CurrentRow.Cells[0].Value.ToString();
                Information.PerformClick();
                iconBtnSearchTBInfo.PerformClick();
            }
        } //Показать детальную информацию
        #endregion

        #region Infromation Page

        private void bfBtnPrev_Click(object sender, EventArgs e)
        {
            if (_number != 0)
            {
                LoadInfoCriminals(_table, --_number);
            }
            else
                LoadInfoCriminals(_table, _number = _table.Rows.Count - 1);
        } //Предыдущее дело

        private void bfBtnFirst_Click(object sender, EventArgs e)
        {
            LoadInfoCriminals(_table, _number = 0);
        } //Первое дело

        private void bfBtnNext_Click(object sender, EventArgs e)
        {
            try
            {
                LoadInfoCriminals(_table, ++_number);
            }
            catch
            {
                LoadInfoCriminals(_table, _number = 0);
            }
        } //Следующее дело

        private async void iconBtnSearchTBInfo_Click(object sender, EventArgs e)
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand(
                $"SELECT `Номер дела`,`ФИО`,DATE_FORMAT(`Дата рождения`, '%d.%m.%Y'),`Пол`,`Адрес`,`Преступления`.`Наименование` AS 'Преступление', `Розыск`.`Наименование` AS 'Розыск',`Состояние` FROM `Преступники` " +
                $"LEFT OUTER JOIN `Преступления` ON `Преступники`.`Код преступления` = `Преступления`.`Код преступления` LEFT OUTER JOIN `Розыск` ON `Преступники`.`Код розыска` = `Розыск`.`Код розыска` " +
                $"WHERE `Номер дела` like @num AND " +
                $"`ФИО` like @name AND " +
                $"DATE_FORMAT(`Дата рождения`, '%d.%m.%Y') like @date AND " +
                $"`Адрес` like @adress AND " +
                $"`Преступления`.`Наименование` like @criminal AND " +
                $"`Розыск`.`Наименование` like @search AND " +
                $"`Состояние` like @state"))
            {
                mySqlCommand.Parameters.AddWithValue("@num", '%' + bfTBSearch1.Text + '%');
                mySqlCommand.Parameters.AddWithValue("@name", '%' + bfTBSearch2.Text + '%');
                mySqlCommand.Parameters.AddWithValue("@date", '%' + bfTBSearch3.Text + '%');
                mySqlCommand.Parameters.AddWithValue("@adress", '%' + bfTBSearch4.Text + '%');
                mySqlCommand.Parameters.AddWithValue("@criminal", '%' + bfTBSearch5.Text + '%');
                mySqlCommand.Parameters.AddWithValue("@search", '%' + bfTBSearch6.Text + '%');
                mySqlCommand.Parameters.AddWithValue("@state", '%' + bfTBSearch7.Text + '%');
                using (DataTable table = await DBMVD.Methods.GetDataAsync(mySqlCommand))
                {
                    if (table.Rows.Count > 0)
                    {
                        _table = table;
                        LoadInfoCriminals(_table);
                        MBoxInfo($"По данным фильтрам было найдено записей: {_table.Rows.Count}", Text);
                    }
                    else
                    {
                        MBoxInfo("По данным фильтрам ничего не найдено.", Text);
                    }
                }
            }
        } //Поиск по фильтрам
        #endregion

        #region Settings Page

        private async void btnPassChange_Click(object sender, EventArgs e)
        {
            if (!tabPageSettings.Controls.OfType<Bunifu.UI.WinForms.BunifuTextBox>().ToList().Where(x => x.Name.Contains("Pass")).Any(x => String.IsNullOrEmpty(x.Text)))
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(
                    $"SELECT `Login` FROM `UsersMVD` WHERE `Login` = @login AND `Password` = sha2(EncryptedPassword(@pass), 512)"))
                {
                    mySqlCommand.Parameters.AddWithValue("@login", User);
                    mySqlCommand.Parameters.AddWithValue("@pass", bunifuTBPass1.Text);

                    if ((await DBMVD.Methods.GetDataAsync(mySqlCommand)).Rows.Count == 1)
                    {
                        if (bunifuTBPass2.Text == bunifuTBPass3.Text)
                        {
                            if (bunifuTBPass1.Text != bunifuTBPass2.Text)
                            {
                                using (MySqlCommand mySqlCommand2 = new MySqlCommand(
                                    $"UPDATE `UsersMVD` SET `Password` = sha2(EncryptedPassword(@pass), 512) WHERE `Login` = @login"))
                                {
                                    mySqlCommand2.Parameters.AddWithValue("@login", User);
                                    mySqlCommand2.Parameters.AddWithValue("@pass", bunifuTBPass2.Text);
                                    DBMVD.Methods.SetData(mySqlCommand2);
                                }

                                MBoxInfo("Пароль изменён", Text);
                                tabPageSettings.Controls.OfType<Bunifu.UI.WinForms.BunifuTextBox>().ToList().Where(x => x.Name.Contains("Pass")).ToList()
                                    .AsParallel().ForAll(x => x.Text = "");
                            }
                            else
                            {
                                MBoxWarning("Новый пароль не может быть старым!", Text);
                            }
                        }
                        else
                        {
                            MBoxWarning("Пароли должны совпадать!", Text);
                        }
                    }
                    else
                    {
                        MBoxWarning("Неправильно указан старый пароль!", Text);
                    }
                }
            }
            else
            {
                MBoxWarning("Заполнены не все поля!", Text);
            }
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
                _currentBtn = (IconButton)senderBtn;
                _currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                _currentBtn.ForeColor = color;
                _currentBtn.IconColor = color;

                _currentBtn.TextImageRelation = panelMenu.Width == 186 ? TextImageRelation.TextBeforeImage : TextImageRelation.Overlay;
                _currentBtn.ImageAlign = panelMenu.Width == 186 ? ContentAlignment.MiddleRight : ContentAlignment.MiddleLeft;
                _currentBtn.TextAlign = panelMenu.Width == 186 ? ContentAlignment.MiddleCenter : ContentAlignment.MiddleRight;
                _currentBtn.Padding = panelMenu.Width == 186 ? new Padding(10, 0, 20, 0) : new Padding(8, 0, 5, 0);

                _leftBorderBtn.BackColor = color;
                _leftBorderBtn.Location = new Point(0, _currentBtn.Location.Y);
                _leftBorderBtn.Visible = true;
                _leftBorderBtn.BringToFront();

                iconCurrentChildForm.IconChar = _currentBtn.IconChar;
                iconCurrentChildForm.IconColor = color;
            }
        } //Recolor button on click

        private void DisableButton()
        {
            if (_currentBtn != null)
            {
                _currentBtn.BackColor = Color.FromArgb(24, 30, 54);
                _currentBtn.ForeColor = Color.Gainsboro;
                _currentBtn.IconColor = Color.Gainsboro;

                _currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
                _currentBtn.TextImageRelation = panelMenu.Width == 186 ? TextImageRelation.ImageBeforeText : TextImageRelation.Overlay;
                _currentBtn.TextAlign = panelMenu.Width == 186 ? ContentAlignment.MiddleLeft : ContentAlignment.MiddleRight;
                _currentBtn.Padding = panelMenu.Width == 186 ? new Padding(10, 0, 20, 0) : new Padding(3, 0, 3, 0);
            }
        } //Recolor button

        private async void CriminalRecordsLoad()
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand(
                $"SELECT `Номер дела`, `ФИО`, `Преступления`.`Наименование` AS 'Преступление', `Розыск`.`Наименование` AS 'Наименование', " +
                $"`Состояние` FROM `Преступники` LEFT OUTER JOIN `Преступления` ON `Преступники`.`Код преступления` = `Преступления`.`Код преступления` " +
                $"LEFT OUTER JOIN `Розыск` ON `Преступники`.`Код розыска` = `Розыск`.`Код розыска`"))
                dataGVDBCrimRecords.DataSource = await DBMVD.Methods.GetDataAsync(mySqlCommand);
        }

        private async void InformationPageLoad()
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand(
                $"SELECT `Номер дела`,`ФИО`,DATE_FORMAT(`Дата рождения`, '%d.%m.%Y'),`Пол`,`Адрес`,`Преступления`.`Наименование` AS 'Преступление'," +
                $"`Розыск`.`Наименование` AS 'Розыск',`Состояние` FROM `Преступники`" +
                $"LEFT OUTER JOIN `Преступления` ON `Преступники`.`Код преступления` = `Преступления`.`Код преступления`" +
                $"LEFT OUTER JOIN `Розыск` ON `Преступники`.`Код розыска` = `Розыск`.`Код розыска`"))
            using (DataTable table = await DBMVD.Methods.GetDataAsync(mySqlCommand))
            {
                _table = table;
                LoadInfoCriminals(_table);
            }
        } //Загрузить данные в Criminals Page

        private async void SettingsLoad()
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand(
                $"SELECT `ФИО`, `Логин`, `Должности`.`Наименование` AS 'Должность', `Звания`.`Наименование` AS 'Звание' FROM `Сотрудники`" +
                $"LEFT OUTER JOIN `Должности` ON `Сотрудники`.`Код должности` = `Должности`.`Код должности`" +
                $"LEFT OUTER JOIN `Звания` ON `Сотрудники`.`Код звания` = `Звания`.`Код звания`" +
                $"WHERE `Логин` = @login"))
            {
                mySqlCommand.Parameters.AddWithValue("@login", User);
                using (DataTable dataTable = await DBMVD.Methods.GetDataAsync(mySqlCommand))
                {
                    if (dataTable.Rows.Count == 1)
                        tabPageSettings.Controls.OfType<Label>().ToList().Where(x => x.Name.Contains("lbUserSetAns")).AsParallel().ForAll(label =>
                        {
                            label.Text = dataTable.Rows[0].ItemArray[int.Parse(label.Tag.ToString())].ToString().ToUpper();
                        });
                }
            }
        }

        private void LoadInfoCriminals(DataTable table, int i = 0)
        {
            bfGB1.Controls.OfType<Bunifu.UI.WinForms.BunifuLabel>().ToList().Where(x => x.Name.Contains("bfLbDBAns")).AsParallel().ForAll(label =>
            label.Text = table.Rows[i].ItemArray[int.Parse(label.Tag.ToString())].ToString().ToUpper());
            bfLbDBAns7.ForeColor = bfLbDBAns7.Text.Contains("РОЗЫСК") ? Color.FromArgb(249, 88, 155) : Color.FromArgb(0, 126, 249);
        } //Отобразить выбранные данные в Information Page

        private void TableInfoOnStart(object sender, EventArgs e)
        {
            if (bfGB2.Controls.OfType<Bunifu.UI.WinForms.BunifuTextBox>().All(x => x.Text == ""))
                LoadInfoCriminals(_table);
        }

        private void dataGridViewMouseWheel(DataGridView dataGrid, Bunifu.UI.WinForms.BunifuVScrollBar scrollBar, MouseEventArgs e)
        {
            if (dataGrid.RowCount > 7)
            {
                int currentIndex = dataGrid.FirstDisplayedScrollingRowIndex;
                dataGrid.FirstDisplayedScrollingRowIndex = e.Delta > 0 ? Math.Max(0, currentIndex - 1) : currentIndex + 1;
                scrollBar.Value = dataGrid.RowCount - dataGrid.FirstDisplayedScrollingRowIndex == 7 ?
                    dataGrid.RowCount - 1 : dataGrid.FirstDisplayedScrollingRowIndex;
            }
        } //Прокрутка в таблице

        private void bfScrollBarUpdateDataGrid(DataGridView dataGrid, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            dataGrid.FirstDisplayedScrollingRowIndex = e.Value >= 0 && e.Value <= dataGrid.RowCount ? e.Value : 0;
        } //Прокрутка через скролбар

        private void bfScrollBarUpdateValue(Bunifu.UI.WinForms.BunifuVScrollBar scrollBar, DataGridView dataGrid)
        {
            bool max = dataGrid.RowCount > 7;
            scrollBar.Visible = max;

            if (max)
            {
                scrollBar.Minimum = 0;
                scrollBar.Maximum = dataGrid.RowCount - 1;
            }
        } //Обновить данные о скроллбаре

        private DialogResult MBox(string message, string text, MessageBoxIcon img = MessageBoxIcon.Question, MessageBoxButtons btn = MessageBoxButtons.YesNo, MessageBoxDefaultButton def = MessageBoxDefaultButton.Button2)
        {
            return MessageBox.Show(message, text, btn, img, def);
        } //Вызов MessageBox

        private DialogResult MBoxInfo(string message, string title)
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private DialogResult MBoxWarning(string message, string title)
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private DialogResult MBoxQues(string message, string title)
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Net;
using System.Net.Http;
using System.Data.SqlTypes;
using MySql.Data.MySqlClient;

namespace DataBaseMVD
{
    public partial class AdminForm : Form
    {
        private string Admin => _admin ?? (_admin = DBMVD.LocalUser);
        private string _admin;
        private int _number;
        private DataTable _table;
        private IconButton _currentBtn;
        private Panel _leftBorderBtn;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);


        public AdminForm()
        {
            InitializeComponent();
            _leftBorderBtn = new Panel();
            _leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(_leftBorderBtn);
            bfTBIpAddress.Text = Registry.CurrentUser.CreateSubKey(@"Software\DataBaseMVD\AdminSettings")?.GetValue("IPMask")?.ToString();
            bfBtnHideSWSett.Checked = bool.Parse(Registry.CurrentUser.CreateSubKey(@"Software\DataBaseMVD\AdminSettings")?.GetValue("ButtonsHide")?.ToString());
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            labelAdmin.Text = Admin;
            labelAdmin.Location = new Point(panelMenuUp.Width / 2 - labelAdmin.Width / 2, labelAdmin.Location.Y); //Подготовка формы
            bfGB2.Controls.OfType<Bunifu.UI.WinForms.BunifuTextBox>().AsParallel().ForAll(x => x.TextChanged += TableInfoOnStart);
        }

        private async void AdminForm_Shown(object sender, EventArgs e)
        {
            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete) { Application.DoEvents(); }
            for (Opacity = 0; Opacity <= 0.98; Opacity += 0.01)
                await Task.Delay(10);
            TopMost = false;
        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MBoxQues("Вы действительно хотите закрыть приложение?", Text) == DialogResult.No)
                e.Cancel = true; //Не закрывать форму
            else
                DBMVD.Methods.SetOffline(); //Если пользователь хочет закрыть форму
        }


        #region PanelMenu page button

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
        }  //Resize buttons on PanelMenu

        private void Button1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            CriminalRecordsLoad();
            lblTitleChildForm.Text = CriminalRecords.Text;
            FormPages.SelectedTab = tabPageCriminalRecords;
        } //Records of criminal button

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
            UsersLoad();
            lblTitleChildForm.Text = Button3.Text;
            FormPages.SelectedTab = tabPageUsers;
        } //Users button

        private void Button4_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            AccessLoad();
            lblTitleChildForm.Text = Button4.Text;
            FormPages.SelectedTab = tabPageAccess;
        } //Access for Users button

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

        private void AdminForm_ResizeBegin(object sender, EventArgs e)
        {
            this.Opacity = 0.75;
        } //Opacity low while form moving

        private void AdminForm_ResizeEnd(object sender, EventArgs e)
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
        } //Recolor bars button

        private void btnBars_MouseLeave(object sender, EventArgs e)
        {
            if (panelMenu.Width == 45)
            {
                btnBars.IconColor = Color.FromArgb(24, 161, 251);
                btnBars.FlatAppearance.MouseOverBackColor = Color.Gainsboro;
            }
        } //Recolor bars button
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
        }  //Показать детальную информацию

        private void dataGVDBCrimRecords_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGVDBCrimRecords.CurrentRow != null && e.Button == MouseButtons.Right
                && MBoxQues($"Отредактировать данные преступника: {dataGVDBCrimRecords.CurrentRow.Cells[1].Value}?", Text) == DialogResult.Yes)
            {
                bfBtnAddCrim.Text = "Сохранить изменения";
                LoadCriminalDataForEdit(dataGVDBCrimRecords.CurrentRow.Cells[0].Value.ToString());
                FormPages.SelectedTab = tabPageAddCriminal;
            }
        } //Редактирование преступников (информация)

        private void bfBtnCriminalAdd_Click(object sender, EventArgs e)
        {
            ClearAddTables(tabPageAddCriminal);
            bfBtnAddCrim.Text = "Добавить преступника";
            FormPages.SelectedTab = tabPageAddCriminal;
        } //Добавить нового преступника (вкладка)

        private void bfTBDataAddCrim_LostFocus(object sender, EventArgs e)
        {
            using (MaskedTextBox maskedText = new MaskedTextBox())
            {
                maskedText.Mask = "00.00.0000";
                maskedText.Text = bfTBDataAddCrim.Text;

                if (DateTime.TryParse(maskedText.Text, out DateTime data) && SqlDateTime.MinValue.Value <= data && data <= DateTime.Now)
                {
                    bfTBDataAddCrim.Text = data.ToShortDateString();
                }
                else if (!string.IsNullOrEmpty(bfTBDataAddCrim.Text))
                {
                    MBoxWarning("Введена некорректная дата!", Text);
                    bfTBDataAddCrim.Text = "";
                }
            }
        } //Ввод даты по маске + проверка корректности

        private void bfBtnAddCrim_Click(object sender, EventArgs e)
        {
            if (bfBtnAddCrim.Text.Contains("Добавить"))
                AddCriminal();
            else if (bfBtnAddCrim.Text.Contains("Сохранить"))
                CriminalDataEdit();
        } //Добавить или отредактировать преступника

        private void bfChBoxAddCrimM_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            bfChBoxAddCrimFM.Enabled = !bfChBoxAddCrimM.Checked;
        } //Пол: мужской

        private void bfChBoxAddCrimFM_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            bfChBoxAddCrimM.Enabled = !bfChBoxAddCrimFM.Checked;
        } //Пол: женский 
        #endregion

        #region Information Page

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
        //todo изменить ввод номера
        #region Users Page

        private void dataGVUsers_DataSourceChanged(object sender, EventArgs e)
        {
            dataGVUsers.AutoResizeColumns();
            bfScrollBarUpdateValue(bfVSlBarUsers, dataGVUsers);
        }

        private void dataGVUsers_Resize(object sender, EventArgs e)
        {
            dataGVUsers.AutoResizeColumns();
        }

        private void bfVSlBarUsers_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            bfScrollBarUpdateDataGrid(dataGVUsers, e);
        }

        private void dataGVUsers_MouseWheel(object sender, MouseEventArgs e)
        {
            dataGridViewMouseWheel(dataGVUsers, bfVSlBarUsers, e);
        } //Прокрутка в таблице

        private void dataGVUsers_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGVUsers.CurrentCell != null && dataGVUsers.CurrentRow.Cells[1].Value.ToString() != Admin)
            {
                switch (dataGVUsers.CurrentCell.ColumnIndex)
                {
                    case 0: //быстро изменить уровень доступа
                        if (MBoxQues($"Изменить уровень доступа у {dataGVUsers.CurrentRow.Cells[1].Value}?", Text) == DialogResult.Yes)
                        {
                            using (MySqlCommand mySqlCommand = new MySqlCommand($"UPDATE `UsersMVD` SET `Permission` = @perm WHERE `Login` = @login"))
                            {
                                mySqlCommand.Parameters.AddWithValue("@perm", dataGVUsers.CurrentRow.Cells[0].Value.ToString() == "admin" ? "user" : "admin");
                                mySqlCommand.Parameters.AddWithValue("@login", dataGVUsers.CurrentRow.Cells[1].Value.ToString());
                                DBMVD.Methods.SetData(mySqlCommand);
                                dataGVUsers.CurrentRow.Cells[0].Value = dataGVUsers.CurrentRow.Cells[0].Value.ToString() == "admin" ? "user" : "admin";
                            }
                        }
                        break;

                    case 1: //быстро удалить пользователя
                        if (MBoxQues($"Удалить пользователя {dataGVUsers.CurrentRow.Cells[1].Value}?", Text) == DialogResult.Yes)
                        {
                            using (MySqlCommand mySqlCommand = new MySqlCommand($"DELETE FROM `Сотрудники` WHERE `Логин` = @login"))
                            {
                                mySqlCommand.Parameters.AddWithValue("@login", dataGVUsers.CurrentRow.Cells[1].Value.ToString());
                                DBMVD.Methods.SetData(mySqlCommand); 
                                dataGVUsers.Rows.RemoveAt(dataGVUsers.CurrentRow.Index);
                                //bfScrollBarUpdateValue(bfVSlBarUsers, dataGVUsers);
                            }
                        }
                        break;
                } 
            } //Проверка на null и самого себя
            else if (dataGVUsers.CurrentCell != null)
            {
                MBoxWarning("Вы не можете редактировать/удалять себя!", Text);
            } //Если выбрал сам себя
        } //Редактирование пользователей (del and perm)

        private void dataGVUsers_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGVUsers.CurrentRow != null && dataGVUsers.CurrentRow.Cells[1].Value.ToString() != Admin)
            {
                if (e.Button == MouseButtons.Right && MBoxQues($"Отредактировать данные {dataGVUsers.CurrentRow.Cells[1].Value}?", Text) == DialogResult.Yes)
                {
                    bfBtnUserAdd.Text = "Сохранить изменения";
                    bfTBPassUserAdd.Visible = false;
                    LoadUserDataForEdit(dataGVUsers.CurrentRow.Cells[1].Value.ToString());
                    FormPages.SelectedTab = tabPageAddUser;
                }
            } //Проверка на null и самого себя
            else if (dataGVUsers.CurrentRow != null && e.Button == MouseButtons.Right)
            {
                MBoxWarning("Вы не можете редактировать/удалять себя!", Text);
            } //Если выбрал сам себя
        } //Редактирование пользователей (информация)

        private void dataGVUsers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Up ||
                e.KeyValue == (char)Keys.Down ||
                e.KeyValue == (char)Keys.Enter)
            {
                e.Handled = true; return;
            }
        } //Запрет на перемешение по таблице стрелками

        private void bfTBSearchUsers_TextChanged(object sender, EventArgs e)
        {
            (dataGVUsers.DataSource as DataTable).DefaultView.RowFilter =
                 $"[{dataGVUsers.Columns[0].HeaderText}] like '%{bfTBSearchUsers.Text}%' or\n" +
                 $"[{dataGVUsers.Columns[1].HeaderText}] like '%{bfTBSearchUsers.Text}%' or\n" +
                 $"[{dataGVUsers.Columns[2].HeaderText}] like '%{bfTBSearchUsers.Text}%'";
            bfScrollBarUpdateValue(bfVSlBarUsers, dataGVUsers);
        } //Поиск данных по всей таблице

        private void bfTBUNumberAddUser_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void bfTBUNumberAddUser_Leave(object sender, EventArgs e) //todo переделать под анимацию
        {
            using (MaskedTextBox maskedText = new MaskedTextBox())
            {
                maskedText.Mask = "+7 (000) 000-00-00";
                if (!string.IsNullOrEmpty(bfTBUNumberAddUser.Text) && bfTBUNumberAddUser.Text.Length >= 10)
                {
                    maskedText.Text = (bfTBUNumberAddUser.Text.Replace(" ", "").Length >= 11 && bfTBUNumberAddUser.Text.StartsWith("8")) ? 
                        bfTBUNumberAddUser.Text.Replace(" ", "").Substring(1, 10) : bfTBUNumberAddUser.Text.Replace(" ", "");
                    bfTBUNumberAddUser.Text = maskedText.Text;
                }
                else
                    bfTBUNumberAddUser.Text = "";
            }
        } //Ввод номера по маске

        private void bfBtnAddUser_Click(object sender, EventArgs e)
        {
            ClearAddTables(tabPageAddUser);
            bfTBPassUserAdd.Visible = true;
            bfBtnUserAdd.Text = "Добавить пользователя";
            FormPages.SelectedTab = tabPageAddUser;
        } //Добавить нового пользователя (вкладка)

        private async void bfBtnDellUser_Click(object sender, EventArgs e)
        {
            if (dataGVUsers.CurrentRow != null && dataGVUsers.RowCount > 0)
            {
                IEnumerable<object> users = dataGVUsers.SelectedRows.OfType<DataGridViewRow>().ToList().OrderBy(x => x.Index).Select(x => x.Cells[1].Value);

                if (!users.Contains(Admin) && MBoxQues($"Вы уверены, что хотите удалить:\n{string.Join(Environment.NewLine, users)}?", Text) == DialogResult.Yes)
                {
                    using (MySqlCommand mySqlCommand = new MySqlCommand())
                    {
                        mySqlCommand.CommandText = $"DELETE FROM `Сотрудники` WHERE `Логин` IN ('{string.Join("','", users)}'); " +
                            $"SELECT `Permission`, `Login`, `Desktop`, `HWID` FROM `UsersMVD`";
                        dataGVUsers.DataSource = await DBMVD.Methods.GetDataAsync(mySqlCommand);
                        bfScrollBarUpdateValue(bfVSlBarUsers, dataGVUsers);
                    }
                } //Удаление пользователей
                else if (users.Contains(Admin))
                {
                    MBoxWarning("Вы не можете редактировать/удалять себя!", Text);
                } //Запрет на удаление себя
            }
        } //Удалить выбранных пользователей
        
        private void bfBtnAllUsers_Click(object sender, EventArgs e)
        {
            (dataGVUsers.DataSource as DataTable).DefaultView.RowFilter = "";
            bfScrollBarUpdateValue(bfVSlBarUsers, dataGVUsers);
        } //Показать всех пользователей

        private void bfBtnOnline_Click(object sender, EventArgs e)
        {
            (dataGVUsers.DataSource as DataTable).DefaultView.RowFilter =
                $"[{dataGVUsers.Columns[2].HeaderText}] IS NOT NULL";
            bfScrollBarUpdateValue(bfVSlBarUsers, dataGVUsers);
        } //Показать пользователей онлайн

        private void bfBtnUserAdd_Click(object sender, EventArgs e)
        {
            if (bfBtnUserAdd.Text.Contains("Добавить"))
                AddUser();
            else if (bfBtnUserAdd.Text.Contains("Сохранить"))
                UserDataEdit();
        } //Добавить или отредактировать пользователя

        private void bfChBoxAddUserM_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            bfChBoxAddUserFM.Enabled = !bfChBoxAddUserM.Checked;
        } //Пол: мужской

        private void bfChBoxAddUserFM_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            bfChBoxAddUserM.Enabled = !bfChBoxAddUserFM.Checked;
        } //Пол: женский
        #endregion

        #region Access Page

        private void dataGVAccess_DataSourceChanged(object sender, EventArgs e)
        {
            dataGVAccess.AutoResizeColumns();
            bfScrollBarUpdateValue(bfSBAccess, dataGVAccess);
        }

        private void dataGVAccess_Resize(object sender, EventArgs e)
        {
            dataGVAccess.AutoResizeColumns();
        }

        private void bfSBAccess_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            bfScrollBarUpdateDataGrid(dataGVAccess, e);
        }

        private void dataGVAccess_MouseWheel(object sender, MouseEventArgs e)
        {
            dataGridViewMouseWheel(dataGVAccess, bfSBAccess, e);
        } //Прокрутка в таблице

        private void dataGVAccess_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MBoxQues($"Отклонить запрос на получение доступа для {dataGVAccess.Rows[e.RowIndex].Cells[0].Value}?", Text) == DialogResult.Yes)
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(
                    $"UPDATE `HWIDUsers` SET `Visible` = 0 WHERE `Login` = @login"))
                {
                    mySqlCommand.Parameters.AddWithValue("@login", dataGVAccess.Rows[e.RowIndex].Cells[0].Value);
                    DBMVD.Methods.SetData(mySqlCommand);
                    dataGVAccess.Rows.RemoveAt(e.RowIndex);
                    bfScrollBarUpdateValue(bfSBAccess, dataGVAccess);
                }
            }
        } //Отказать в доступе

        private void bfBtnApproveAccess_Click(object sender, EventArgs e)
        {
            if (dataGVAccess.SelectedRows != null && dataGVAccess.RowCount > 0)
            {
                if (MBoxQues($"Выдать доступ для {dataGVAccess.CurrentRow.Cells[0].Value}?", Text) == DialogResult.Yes)
                {
                    using (MySqlCommand mySqlCommand = new MySqlCommand(
                        $"UPDATE `HWIDUsers` SET `Visible` = 0 WHERE `Login` = @login;" +
                        $"UPDATE `UsersMVD` SET `HWID` = @hwid WHERE `Login` = @login"))
                    {
                        mySqlCommand.Parameters.AddWithValue("@login", dataGVAccess.CurrentRow.Cells[0].Value);
                        mySqlCommand.Parameters.AddWithValue("@hwid", dataGVAccess.CurrentRow.Cells[1].Value);
                        DBMVD.Methods.SetData(mySqlCommand);
                        dataGVAccess.Rows.Remove(dataGVAccess.CurrentRow);
                        bfScrollBarUpdateValue(bfSBAccess, dataGVAccess);
                    }
                }
            }
        } //Выдать доступ только для 1 пользователя

        private void bfBtnRejectAccess_Click(object sender, EventArgs e)
        {
            if (dataGVAccess.SelectedRows != null && dataGVAccess.RowCount > 0)
            {
                IEnumerable<object> users = dataGVAccess.SelectedRows.OfType<DataGridViewRow>().ToList().OrderBy(x => x.Index).Select(x => x.Cells[0].Value);

                if (MBoxQues($"Отклонить запрос на получение доступа для:\n{string.Join(Environment.NewLine, users)}?", Text) == DialogResult.Yes)
                {
                    using (MySqlCommand mySqlCommand = new MySqlCommand())
                    {
                        mySqlCommand.CommandText = $"UPDATE `HWIDUsers` SET `Visible` = 0 WHERE `Login` IN ('{string.Join("','", users)}');";
                        DBMVD.Methods.SetData(mySqlCommand);
                        dataGVAccess.Rows.OfType<DataGridViewRow>().Where(x => users.Contains(x.Cells[0].Value)).ToList().ForEach(x => dataGVAccess.Rows.Remove(x));
                        bfScrollBarUpdateValue(bfSBAccess, dataGVAccess);
                    }
                }
            }
        } //Отказать в доступе выбранным пользователям

        private async void bfBtnAllAccess_Click(object sender, EventArgs e)
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand(
                $"SELECT `HWIDUsers`.`Login` AS 'Логин', `HWIDUsers`.`HWID` AS 'Новый HWID', `UsersMVD`.`HWID` AS 'Старый HWID', " +
                $"`IP` FROM `HWIDUsers` LEFT JOIN `UsersMVD` ON `HWIDUsers`.`Login` = `UsersMVD`.`Login`"))
                dataGVAccess.DataSource = await DBMVD.Methods.GetDataAsync(mySqlCommand);

            bfScrollBarUpdateValue(bfSBAccess, dataGVAccess);
        } //Показать всех пользователей

        private async void bfBtnNoAccess_Click(object sender, EventArgs e)
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand(
                $"SELECT `HWIDUsers`.`Login` AS 'Логин', `HWIDUsers`.`HWID` AS 'Новый HWID', `UsersMVD`.`HWID` AS 'Старый HWID', " +
                $"`IP` FROM `HWIDUsers` LEFT JOIN `UsersMVD` ON `HWIDUsers`.`Login` = `UsersMVD`.`Login` WHERE `Visible` = 1"))
                dataGVAccess.DataSource = await DBMVD.Methods.GetDataAsync(mySqlCommand);

            bfScrollBarUpdateValue(bfSBAccess, dataGVAccess);
        } //Показать пользователей без доступа

        private void bfTBSearchAccess_TextChanged(object sender, EventArgs e)
        {
            (dataGVAccess.DataSource as DataTable).DefaultView.RowFilter = dataGVAccess.RowCount > 0 ?
                     $"[{dataGVUsers.Columns[0].HeaderText}] like '%{bfTBSearchAccess.Text}%' or\n" +
                     $"[{dataGVUsers.Columns[1].HeaderText}] like '%{bfTBSearchAccess.Text}%' or\n" +
                     $"[{dataGVUsers.Columns[2].HeaderText}] like '%{bfTBSearchAccess.Text}%' or\n" +
                     $"[{dataGVUsers.Columns[3].HeaderText}] like '%{bfTBSearchAccess.Text}%'" : "";

            bfScrollBarUpdateValue(bfSBAccess, dataGVAccess);
        } //Поиск данных по всей таблице

        private void dataGVAccess_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            string ipMask = Registry.CurrentUser.OpenSubKey(@"Software\DataBaseMVD\AdminSettings")?.GetValue("IPMask")?.ToString().Substring(0, 7);
            for (int i = 0; i < e.RowCount; i++)
            {
                dataGVAccess.Rows[e.RowIndex + i].DefaultCellStyle.ForeColor = 
                    dataGVAccess.Rows[e.RowIndex + i].Cells[3].Value.ToString().Contains(ipMask) ? Color.Silver : Color.Red;
                dataGVAccess.Rows[e.RowIndex + i].DefaultCellStyle.SelectionForeColor =
                    dataGVAccess.Rows[e.RowIndex + i].Cells[3].Value.ToString().Contains(ipMask) ? Color.Gainsboro : Color.Red;
            }
        } //Выделить подозрительные строки для доступа
        #endregion

        #region Settings Page

        private void bfBtnHideSWSett_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {
            CriminalRecords.Visible = bfBtnHideSWSett.Checked ? false : true;
            Information.Visible = bfBtnHideSWSett.Checked ? false : true;
            Button3.Location = bfBtnHideSWSett.Checked ? new Point(0, 168) : new Point(0, 288);
            Button4.Location = bfBtnHideSWSett.Checked ? new Point(0, 228) : new Point(0, 348);
            Button5.Location = bfBtnHideSWSett.Checked ? new Point(0, 288) : new Point(0, 408);
            Button5.PerformClick();
            Registry.CurrentUser.OpenSubKey(@"Software\DataBaseMVD\AdminSettings", true)?.SetValue("ButtonsHide", bfBtnHideSWSett.Checked);
        } //Скрыть кнопки

        private async void btnPassChange_Click(object sender, EventArgs e)
        {
            if (!tabPageSettings.Controls.OfType<Bunifu.UI.WinForms.BunifuTextBox>().ToList().Where(x => x.Name.Contains("Pass")).Any(x => String.IsNullOrEmpty(x.Text)))
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(
                    $"SELECT `Login` FROM `UsersMVD` WHERE `Login` = @login AND `Password` = sha2(EncryptedPassword(@pass), 512)"))
                {
                    mySqlCommand.Parameters.AddWithValue("@login", Admin);
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
                                    mySqlCommand2.Parameters.AddWithValue("@login", Admin);
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

        private void bfTBIpAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && IPAddress.TryParse(bfTBIpAddress.Text, out IPAddress ip))
            {
                Registry.CurrentUser.OpenSubKey(@"Software\DataBaseMVD\AdminSettings", true)?.SetValue("IPMask", ip);
                MBoxInfo("IP-адрес сохранён!", Text);
            }
            else if (e.KeyChar == (char)Keys.Enter)
            {
                MBoxWarning("Неверный IP-адрес!", Text);
            }
        } //Нажав Enter, проверить IP и сохранить в реестр

        private async void bfTBIpAddress_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            using (HttpClient net = new HttpClient())
            {
                bfTBIpAddress.Text = await net.GetStringAsync("https://api.ipify.org");
                Registry.CurrentUser.OpenSubKey(@"Software\DataBaseMVD\AdminSettings", true)?.SetValue("IPMask", IPAddress.Parse(bfTBIpAddress.Text));
                MBoxInfo("IP-адрес текущего компьютера успешно импортирован и сохранён.", Text);
            }
        } //Вывести текущий IP в TB для IP
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

        private async void UsersLoad()
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand(
                $"SELECT `Permission`, `Login`, `Desktop`, `HWID` FROM `UsersMVD`"))
                dataGVUsers.DataSource = await DBMVD.Methods.GetDataAsync(mySqlCommand);
        }

        private async void AccessLoad()
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand(
                $"SELECT `HWIDUsers`.`Login` AS 'Логин', `HWIDUsers`.`HWID` AS 'Новый HWID', `UsersMVD`.`HWID` AS 'Старый HWID', " +
                $"`IP` FROM `HWIDUsers` LEFT JOIN `UsersMVD` ON `HWIDUsers`.`Login` = `UsersMVD`.`Login` WHERE `Visible` = 1"))
                dataGVAccess.DataSource = await DBMVD.Methods.GetDataAsync(mySqlCommand);
        }

        private async void SettingsLoad()
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand(
                $"SELECT `ФИО`, `Логин`, `Должности`.`Наименование` AS 'Должность', `Звания`.`Наименование` AS 'Звание' FROM `Сотрудники`" +
                $"LEFT OUTER JOIN `Должности` ON `Сотрудники`.`Код должности` = `Должности`.`Код должности`" +
                $"LEFT OUTER JOIN `Звания` ON `Сотрудники`.`Код звания` = `Звания`.`Код звания`" +
                $"WHERE `Логин` = @login"))
            {
                mySqlCommand.Parameters.AddWithValue("@login", Admin);
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

        private async void LoadUserDataForEdit(string login)
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand(
                $"SELECT `Permission`, `Login`, `Сотрудники`.`ФИО`, `Сотрудники`.`Пол`, `Сотрудники`.`Адрес`, `Сотрудники`.`Телефон`, " +
                $"`Сотрудники`.`Код должности`, `Сотрудники`.`Код звания` FROM `UsersMVD` " +
                $"LEFT OUTER JOIN `Сотрудники` ON `UsersMVD`.`Login` = `Сотрудники`.`Логин` WHERE `Login` = @login"))
            {
                mySqlCommand.Parameters.AddWithValue("@login", login);

                using (DataTable table = await DBMVD.Methods.GetDataAsync(mySqlCommand))
                {
                    if (table.Rows.Count > 0)
                    {
                        bfDwnUserAdd.SelectedItem = table.Rows[0].ItemArray[0].ToString();
                        bfTBUserAddLogin.Text = table.Rows[0].ItemArray[1].ToString();
                        bfTBUNameAddUser.Text = table.Rows[0].ItemArray[2].ToString();
                        bfChBoxAddUserM.Checked = table.Rows[0].ItemArray[3].ToString() == "М";
                        bfChBoxAddUserFM.Checked = !bfChBoxAddCrimM.Checked;
                        bfTBUAddressAddUser.Text = table.Rows[0].ItemArray[4].ToString();
                        bfTBUNumberAddUser.Text = table.Rows[0].ItemArray[5].ToString();
                        bfDwnPostUserAdd.SelectedIndex = int.Parse(table.Rows[0].ItemArray[6].ToString()) - 1;
                        bfDwnRangUserAdd.SelectedIndex = int.Parse(table.Rows[0].ItemArray[7].ToString()) - 1;
                    }
                }
            }
        } //Загрузить информацию о пользователе для редактирования

        private async void LoadCriminalDataForEdit(string id)
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand(
                $"SELECT `Номер дела`, `ФИО`, DATE_FORMAT(`Дата рождения`, '%d.%m.%Y'), `Пол`, `Адрес`, `Код преступления`, `Код розыска`, " +
                $"`Состояние` FROM `Преступники` WHERE `Номер дела` = @id"))
            {
                mySqlCommand.Parameters.AddWithValue("@id", id);

                using (DataTable table = await DBMVD.Methods.GetDataAsync(mySqlCommand))
                {
                    if (table.Rows.Count > 0)
                    {
                        bfTBIDAddCrim.Text = table.Rows[0].ItemArray[0].ToString();
                        bfTBNameAddCrim.Text = table.Rows[0].ItemArray[1].ToString();
                        bfTBDataAddCrim.Text = table.Rows[0].ItemArray[2].ToString();
                        bfChBoxAddCrimM.Checked = table.Rows[0].ItemArray[3].ToString() == "М";
                        bfChBoxAddCrimFM.Checked = !bfChBoxAddCrimM.Checked;
                        bfTBAddressAddCrim.Text = table.Rows[0].ItemArray[4].ToString();
                        bfDwnAddCriminals.SelectedIndex = int.Parse(table.Rows[0].ItemArray[5].ToString()) - 1;
                        bfDwnRecordsAddCrim.SelectedIndex = int.Parse(table.Rows[0].ItemArray[6].ToString()) - 1;
                        bfDwnStateAddCrim.SelectedItem = table.Rows[0].ItemArray[7].ToString();
                    }
                }
            }
        } //Загрузить информацию о преступнике для редактирования

        private void AddUser()
        {
            if (!tabPageAddUser.Controls.OfType<Bunifu.UI.WinForms.BunifuDropdown>().Any(x => x.SelectedItem == null)
                && !tabPageAddUser.Controls.OfType<Bunifu.UI.WinForms.BunifuTextBox>().Any(x => string.IsNullOrEmpty(x.Text))
                && tabPageAddUser.Controls.OfType<Bunifu.UI.WinForms.BunifuCheckBox>().Any(x => x.Checked)
                && bfTBUNumberAddUser.Text.Length == 18)
            {
                try
                {
                    using (MySqlCommand mySqlCommand = new MySqlCommand(
                        $"INSERT INTO `Сотрудники` VALUES(@name, @login, @sex, @address, @number, @post, @rang);" +
                        $"INSERT INTO `UsersMVD`(`Permission`, `Login`, `Password`) VALUES(@perm, @login, sha2(EncryptedPassword(@pass),512))"))
                    {
                        mySqlCommand.Parameters.AddWithValue("@name", bfTBUNameAddUser.Text);
                        mySqlCommand.Parameters.AddWithValue("@login", bfTBUserAddLogin.Text);
                        mySqlCommand.Parameters.AddWithValue("@sex", bfChBoxAddUserM.Checked ? "М" : "Ж");
                        mySqlCommand.Parameters.AddWithValue("@address", bfTBUAddressAddUser.Text);
                        mySqlCommand.Parameters.AddWithValue("@number", bfTBUNumberAddUser.Text);
                        mySqlCommand.Parameters.AddWithValue("@post", bfDwnPostUserAdd.SelectedIndex + 1);
                        mySqlCommand.Parameters.AddWithValue("@rang", bfDwnRangUserAdd.SelectedIndex + 1);
                        mySqlCommand.Parameters.AddWithValue("@perm", bfDwnUserAdd.SelectedItem);
                        mySqlCommand.Parameters.AddWithValue("@pass", bfTBPassUserAdd.Text);

                        DBMVD.Methods.SetData(mySqlCommand);
                    }

                    //ClearAllOnAddUser(); //todo поменять
                    //bfScrollBarUpdateValue(bunifuVScrollBar1, dataGVDBCrimRecords);
                    //FormPages.SelectedTab = tabPageUsers;
                    Button3.PerformClick();
                }
                catch (MySqlException error)
                {
                    MBoxWarning(error.Number == 1062 ? "Пользователь с таким логином уже существует!" : error.Message, Text);
                    bfTBUserAddLogin.Focus();
                    bfTBUserAddLogin.SelectAll();
                } //Добавление пользователя в таблицу и БД, если ошибка, уведомить
            } //Если все поля заполнены
            else
            {
                MBoxWarning("Заполнены не все данные!", Text);
            }
        } //Добавить пользователя

        private void AddCriminal()
        {
            if (!tabPageAddCriminal.Controls.OfType<Bunifu.UI.WinForms.BunifuDropdown>().Any(x => x.SelectedItem == null)
                && !tabPageAddCriminal.Controls.OfType<Bunifu.UI.WinForms.BunifuTextBox>().Any(x => string.IsNullOrEmpty(x.Text))
                && tabPageAddCriminal.Controls.OfType<Bunifu.UI.WinForms.BunifuCheckBox>().Any(x => x.Checked))
            {
                try
                {
                    using (MySqlCommand mySqlCommand = new MySqlCommand(
                        $"INSERT INTO `Преступники` VALUES(@id, @name, @data, @sex, @address, @crim, @record, @state)"))
                    {
                        mySqlCommand.Parameters.AddWithValue("id", bfTBIDAddCrim.Text);
                        mySqlCommand.Parameters.AddWithValue("name", bfTBNameAddCrim.Text);
                        mySqlCommand.Parameters.AddWithValue("data", DateTime.Parse(bfTBDataAddCrim.Text));
                        mySqlCommand.Parameters.AddWithValue("sex", bfChBoxAddUserM.Checked ? "М" : "Ж");
                        mySqlCommand.Parameters.AddWithValue("address", bfTBAddressAddCrim.Text);
                        mySqlCommand.Parameters.AddWithValue("crim", bfDwnAddCriminals.SelectedIndex + 1);
                        mySqlCommand.Parameters.AddWithValue("record", bfDwnRecordsAddCrim.SelectedIndex + 1);
                        mySqlCommand.Parameters.AddWithValue("state", bfDwnStateAddCrim.SelectedIndex);

                        DBMVD.Methods.SetData(mySqlCommand);
                    }

                    //ClearAllOnAddCrim(); //todo поменять
                    //bfScrollBarUpdateValue(bunifuVScrollBar1, dataGVDBCrimRecords);
                    //FormPages.SelectedTab = tabPageCriminalRecords;
                    CriminalRecords.PerformClick();
                }
                catch (MySqlException error)
                {
                    MBoxWarning(error.Number == 1062 ? "Преступник с таким делом уже существует!" : error.Message, Text);
                    bfTBIDAddCrim.Focus();
                    bfTBIDAddCrim.SelectAll();
                } //Добавление пользователя в таблицу и БД, если ошибка, уведомить
            } //Если все поля заполнены
            else
            {
                MBoxWarning("Заполнены не все данные!", Text);
            }
        } //Добавить преступника

        private void UserDataEdit()
        {
            if (!tabPageAddUser.Controls.OfType<Bunifu.UI.WinForms.BunifuDropdown>().Any(x => x.SelectedItem == null)
                && !tabPageAddUser.Controls.OfType<Bunifu.UI.WinForms.BunifuTextBox>().Any(x => string.IsNullOrEmpty(x.Text) && !x.Name.Contains("Pass"))
                && tabPageAddUser.Controls.OfType<Bunifu.UI.WinForms.BunifuCheckBox>().Any(x => x.Checked)
                && bfTBUNumberAddUser.Text.Length == 18)
            {
                try
                {
                    using (MySqlCommand mySqlCommand = new MySqlCommand(
                        $"UPDATE `Сотрудники` SET `ФИО` = @name, `Логин` = @loginNew, `Пол` = @sex, `Адрес` = @address, `Телефон` = @number, " +
                        $"`Код должности` = @post, `Код звания` = @rang WHERE `Логин` = @loginOld;" +
                        $"UPDATE `UsersMVD` SET `Permission` = @perm, `Login` = @loginNew WHERE `Login` = @loginOld"))
                    {
                        mySqlCommand.Parameters.AddWithValue("@name", bfTBUNameAddUser.Text);
                        mySqlCommand.Parameters.AddWithValue("@loginNew", bfTBUserAddLogin.Text);
                        mySqlCommand.Parameters.AddWithValue("@sex", bfChBoxAddUserM.Checked ? "М" : "Ж");
                        mySqlCommand.Parameters.AddWithValue("@address", bfTBUAddressAddUser.Text);
                        mySqlCommand.Parameters.AddWithValue("@number", bfTBUNumberAddUser.Text);
                        mySqlCommand.Parameters.AddWithValue("@post", bfDwnPostUserAdd.SelectedIndex + 1);
                        mySqlCommand.Parameters.AddWithValue("@rang", bfDwnRangUserAdd.SelectedIndex + 1);
                        mySqlCommand.Parameters.AddWithValue("@perm", bfDwnUserAdd.SelectedItem);
                        mySqlCommand.Parameters.AddWithValue("@loginOld", dataGVUsers.CurrentRow.Cells[1].Value.ToString());

                        DBMVD.Methods.SetData(mySqlCommand);
                    }

                    //ClearAllOnAddUser(); //todo поменять
                    //FormPages.SelectedTab = tabPageUsers;
                    Button3.PerformClick();
                }
                catch (MySqlException error)
                {
                    MBoxWarning(error.Number == 1062 ? "Пользователь с таким логином уже существует!" : error.Message, Text);
                    bfTBUserAddLogin.Focus();
                    bfTBUserAddLogin.SelectAll();
                } //Отредактировать данные о пользователе, если ошибка, уведомить
            } //Если все поля заполнены
            else
            {
                MBoxWarning("Заполнены не все данные!", Text);
            }
        } //Редактировать пользователя

        private void CriminalDataEdit()
        {
            if (!tabPageAddCriminal.Controls.OfType<Bunifu.UI.WinForms.BunifuDropdown>().Any(x => x.SelectedItem == null)
                && !tabPageAddCriminal.Controls.OfType<Bunifu.UI.WinForms.BunifuTextBox>().Any(x => string.IsNullOrEmpty(x.Text))
                && tabPageAddCriminal.Controls.OfType<Bunifu.UI.WinForms.BunifuCheckBox>().Any(x => x.Checked))
            {
                try
                {
                    using (MySqlCommand mySqlCommand = new MySqlCommand(
                        $"UPDATE `Преступники` SET `Номер дела` = @IDNew, `ФИО` = @name, `Дата рождения` = @data, `Пол` = @sex, " +
                        $"`Адрес` = @address, `Код преступления` = @crim, `Код розыска` = @record, `Состояние` = @state WHERE `Номер дела` = @IDOld"))
                    {
                        mySqlCommand.Parameters.AddWithValue("@IDNew", bfTBIDAddCrim.Text);
                        mySqlCommand.Parameters.AddWithValue("@name", bfTBNameAddCrim.Text);
                        mySqlCommand.Parameters.AddWithValue("@data", DateTime.Parse(bfTBDataAddCrim.Text));
                        mySqlCommand.Parameters.AddWithValue("@sex", bfChBoxAddCrimM.Checked ? "М" : "Ж");
                        mySqlCommand.Parameters.AddWithValue("@address", bfTBAddressAddCrim.Text);
                        mySqlCommand.Parameters.AddWithValue("@crim", bfDwnAddCriminals.SelectedIndex + 1);
                        mySqlCommand.Parameters.AddWithValue("@record", bfDwnRecordsAddCrim.SelectedIndex + 1);
                        mySqlCommand.Parameters.AddWithValue("@state", bfDwnStateAddCrim.SelectedItem);
                        mySqlCommand.Parameters.AddWithValue("@IDOld", dataGVDBCrimRecords.CurrentRow.Cells[0].Value.ToString());

                        DBMVD.Methods.SetData(mySqlCommand);
                    }

                    //ClearAllOnAddCrim(); //todo поменять
                    //FormPages.SelectedTab = tabPageCriminalRecords;
                    CriminalRecords.PerformClick();
                }
                catch (MySqlException error)
                {
                    MBoxWarning(error.Number == 1062 ? "Преступник с таким делом уже существует!" : error.Message, Text);
                    bfTBIDAddCrim.Focus();
                    bfTBIDAddCrim.SelectAll();
                } //Отредактировать данные о преступнике, если ошибка, уведомить
            } //Если все поля заполнены
            else
            {
                MBoxWarning("Заполнены не все данные!", Text);
            }
        } //Редактировать преступника

        private void ClearAddTables(TabPage page)
        {
            page.Controls.OfType<Bunifu.UI.WinForms.BunifuTextBox>().ToList().ForEach(x => x.Clear());
            page.Controls.OfType<Bunifu.UI.WinForms.BunifuCheckBox>().ToList().ForEach(x => x.Checked = false);
            page.Controls.OfType<Bunifu.UI.WinForms.BunifuDropdown>().ToList().ForEach(x => { x.SelectedItem = null; x.Text = x.Tag.ToString(); });
        }

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
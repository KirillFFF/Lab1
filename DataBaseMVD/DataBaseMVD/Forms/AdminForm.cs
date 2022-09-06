using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.Net;
using System.Data.SqlTypes;

namespace DataBaseMVD
{
    public partial class AdminForm : Form
    {
        private string admin => _admin ?? (_admin = DataBaseSqlCommands.localUser);
        private string _admin;
        private int number;
        private DataTable table;
        private IconButton currentBtn;
        private Panel leftBorderBtn;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        public AdminForm()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);
            bfBtnHideSWSett.Checked = bool.Parse(Registry.CurrentUser.CreateSubKey(@"Software\DataBaseMVD\AdminSettings")?.GetValue("ButtonsHide")?.ToString());
            bfTBIpAddress.Text = Registry.CurrentUser.CreateSubKey(@"Software\DataBaseMVD\AdminSettings")?.GetValue("IPMask")?.ToString();
            StartLoadingForm();
        }

        async void AdminForm_Load(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                labelAdmin.Text = admin;
                labelAdmin.Location = new Point(panelMenuUp.Width / 2 - labelAdmin.Width / 2, labelAdmin.Location.Y);
            }); //Подготовка формы
        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
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
                    sqlCommand.Parameters.AddWithValue("@login", admin);
                    DataBaseSqlCommands.UpdInsDelSqlCommands(sqlCommand);
                }
            } //Если пользователь хочет закрыть форму
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
                        button.TextImageRelation = TextImageRelation.ImageBeforeText;
                        button.ImageAlign = ContentAlignment.MiddleLeft;
                        button.TextAlign = ContentAlignment.MiddleLeft;
                        button.Padding = new Padding(10, 0, 20, 0);

                        if (button.IconColor != Color.Gainsboro)
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
                            currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
                            currentBtn.TextImageRelation = TextImageRelation.Overlay;
                            currentBtn.TextAlign = ContentAlignment.MiddleRight;
                            currentBtn.Padding = new Padding(8, 0, 5, 0);
                        }
                    }
                }
            }
        }  //Resize buttons on PanelMenu

        private void Button1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            lblTitleChildForm.Text = CriminalRecords.Text;
            FormPages.SelectedTab = tabPageCriminalRecords;
        } //Records of criminal button

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
            FormPages.SelectedTab = tabPageUsers;
        } //Users button

        private void Button4_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            lblTitleChildForm.Text = Button4.Text;
            FormPages.SelectedTab = tabPageAccess;
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

        async void btnExit_Click(object sender, EventArgs e)
        {
            await Task.Run(() => this.Close());
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
            if (dataGVDBCrimRecords.CurrentRow != null)
            {
                if (MBox($"Хотите получить детальную информацию о преступнике\n{dataGVDBCrimRecords.CurrentRow.Cells[1].Value}?", "База данных МВД") == DialogResult.Yes)
                {
                    bfTBSearch1.Text = dataGVDBCrimRecords.CurrentRow.Cells[0].Value.ToString();
                    Information.PerformClick();
                    iconBtnSearchTBInfo.PerformClick();
                }
            }
        }  //Показать детальную информацию

        private void dataGVDBCrimRecords_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGVDBCrimRecords.CurrentRow!=null && e.Button==MouseButtons.Right 
                && MBox($"Отредактировать данные {dataGVDBCrimRecords.CurrentRow.Cells[1].Value}","Вопрос") == DialogResult.Yes)
            {
                bfBtnAddCrim.Text = "Сохранить изменения";
                LoadCriminalDataForEdit(dataGVDBCrimRecords.CurrentRow.Cells[0].Value.ToString());
                FormPages.SelectedTab = tabPageAddCriminal;
            }
        } //Редактирование преступников (информация)

        private void bfBtnCriminalAdd_Click(object sender, EventArgs e)
        {
            ClearAllOnAddCrim();
            bfBtnAddCrim.Text = "Добавить преступника";
            FormPages.SelectedTab = tabPageAddCriminal;
        } //Добавить нового преступника (вкладка)

        private void bfTBDataAddCrim_LostFocus(object sender, EventArgs e)
        {
            using (MaskedTextBox maskedText = new MaskedTextBox())
            {
                maskedText.Mask = "00/00/0000";
                maskedText.Text = bfTBDataAddCrim.Text;

                if (DateTime.TryParse(maskedText.Text, out DateTime data) && SqlDateTime.MinValue.Value <= data && data <= DateTime.Now)
                {
                    bfTBDataAddCrim.Text = data.ToShortDateString();
                }
                else if (!string.IsNullOrEmpty(bfTBDataAddCrim.Text))
                {
                    MBox("Введена некорректная дата!", "Ошибка", MessageBoxIcon.Error, MessageBoxButtons.OK);
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
            bfChBoxAddCrimFM.Enabled = bfChBoxAddCrimM.Checked ? false : true;
        } //Пол: мужской

        private void bfChBoxAddCrimFM_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            bfChBoxAddCrimM.Enabled = bfChBoxAddCrimFM.Checked ? false : true;
        } //Пол: женский
        #endregion

        #region Information Page

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

        #region Users Page

        private void tabPageUsers_Layout(object sender, LayoutEventArgs e)
        {
            using (SqlCommand sqlCommand = new SqlCommand($"SELECT Permission, Login, Desktop, HWID FROM [UsersMVD]", DataBaseSqlCommands.sqlConnection))
                dataGVUsers.DataSource = DataBaseSqlCommands.SelectSqlCommands(sqlCommand);
            Parallel.Invoke(() =>
            {
                dataGVUsers.Rows[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGVUsers.Columns.OfType<DataGridViewColumn>().AsParallel().ForAll(x =>
                {
                    dataGVUsers.Columns[x.Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                });
                dataGVUsers.Columns[0].Width = 80;
                dataGVUsers.Columns[1].Width = 110;
                dataGVUsers.Columns[2].Width = 150;
            }, //Форматирование столбцов таблицы

            () =>
            {
                bfScrollBarUpdateValue(bfVSlBarUsers, dataGVUsers);
            }); //Обновить свойства в ScrollBar
        }

        private void dataGVUsers_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGVUsers.CurrentCell != null && dataGVUsers.CurrentRow.Cells[1].Value.ToString() != admin)
            {
                if (dataGVUsers.CurrentCell.ColumnIndex == 0 && MBox($"Изменить уровень доступа у {dataGVUsers.CurrentRow.Cells[1].Value}?", "Вопрос") == DialogResult.Yes)
                {
                    using (SqlCommand sqlCommand = new SqlCommand(
                        $"UPDATE [UsersMVD] SET Permission = @perm WHERE Login = @login", DataBaseSqlCommands.sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@perm", dataGVUsers.CurrentRow.Cells[0].Value.ToString() == "admin" ? "user" : "admin");
                        sqlCommand.Parameters.AddWithValue("@login", dataGVUsers.CurrentRow.Cells[1].Value.ToString());
                        DataBaseSqlCommands.UpdInsDelSqlCommands(sqlCommand);
                    }
                    dataGVUsers.CurrentRow.Cells[0].Value = dataGVUsers.CurrentRow.Cells[0].Value.ToString() == "admin" ? "user" : "admin";
                } //Изменить доступ
                if (dataGVUsers.CurrentCell.ColumnIndex == 1 && MBox($"Удалить пользователя {dataGVUsers.CurrentRow.Cells[1].Value}?", "Вопрос") == DialogResult.Yes)
                {
                    using (SqlCommand sqlCommand = new SqlCommand(
                        $"DELETE FROM [Сотрудники] WHERE Логин = @login\n" +
                        $"DELETE FROM [UsersMVD] WHERE Login = @login", DataBaseSqlCommands.sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@login", dataGVUsers.CurrentRow.Cells[1].Value.ToString());
                        DataBaseSqlCommands.UpdInsDelSqlCommands(sqlCommand);
                    }
                    dataGVUsers.Rows.RemoveAt(dataGVUsers.CurrentRow.Index);
                    bfScrollBarUpdateValue(bfVSlBarUsers, dataGVUsers);
                } //Удалить пользователя   
            } //Проверка на null и самого себя
            else if (dataGVUsers.CurrentCell != null)
            {
                MBox("Вы не можете редактировать/удалять себя!", "Ошибка", MessageBoxIcon.Warning, MessageBoxButtons.OK);
            } //Если выбрал сам себя
        } //Редактирование пользователей (del and perm)

        private void dataGVUsers_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGVUsers.CurrentRow != null && dataGVUsers.CurrentRow.Cells[1].Value.ToString() != admin)
            {
                if (e.Button == MouseButtons.Right && MBox($"Отредактировать данные {dataGVUsers.CurrentRow.Cells[1].Value}?", "Вопрос") == DialogResult.Yes)
                {
                    bfBtnUserAdd.Text = "Сохранить изменения";
                    bfTBPassUserAdd.Visible = false;
                    LoadUserDataForEdit(dataGVUsers.CurrentRow.Cells[1].Value.ToString());
                    FormPages.SelectedTab = tabPageAddUser;
                }
            } //Проверка на null и самого себя
            else if (dataGVUsers.CurrentRow != null && e.Button == MouseButtons.Right)
            {
                MBox("Вы не можете редактировать/удалять себя!", "Ошибка", MessageBoxIcon.Warning, MessageBoxButtons.OK);
            } //Если выбрал сам себя
        } //Редактирование пользователей (информация)

        private void bfVSlBarUsers_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            bfScrollBarUpdateDataGrid(dataGVUsers, e);
        }

        private void dataGVUsers_MouseWheel(object sender, MouseEventArgs e)
        {
            dataGridViewMouseWheel(dataGVUsers, bfVSlBarUsers, e);
        } //Прокрутка в таблице

        private void dataGVUsers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Up ||
                e.KeyValue == (char)Keys.Down ||
                e.KeyValue == (char)Keys.Left ||
                e.KeyValue == (char)Keys.Right)
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

        private void bfTBUNumberAddUser_Leave(object sender, EventArgs e)
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
            ClearAllOnAddUser();
            bfTBPassUserAdd.Visible = true;
            bfBtnUserAdd.Text = "Добавить пользователя";
            FormPages.SelectedTab = tabPageAddUser;
        } //Добавить нового пользователя (вкладка)

        private void bfBtnDellUser_Click(object sender, EventArgs e)
        {
            if (dataGVUsers.CurrentRow != null && dataGVUsers.RowCount > 0)
            {
                List<string> listUsers = new List<string>();
                dataGVUsers.SelectedRows.OfType<DataGridViewRow>().OrderBy(x => x.Index).ToList().ForEach(x =>
                  {
                      listUsers.Add(x.Cells[1].Value.ToString());
                  }); //Выбранные пользователи

                if (!listUsers.Contains(admin) && MBox($"Вы уверены, что хотите удалить:\n{string.Join(Environment.NewLine, listUsers)}?", "Вопрос") == DialogResult.Yes)
                {
                    foreach (DataGridViewRow rows in dataGVUsers.SelectedRows)
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(
                            $"DELETE FROM [Сотрудники] WHERE Логин = @login\n" +
                            $"DELETE FROM [UsersMVD] WHERE Login = @login", DataBaseSqlCommands.sqlConnection))
                        {
                            sqlCommand.Parameters.AddWithValue("@login", rows.Cells[1].Value);
                            DataBaseSqlCommands.UpdInsDelSqlCommands(sqlCommand);
                        }
                        dataGVUsers.Rows.RemoveAt(rows.Index);
                    } //Удалить из таблицы и БД пользователей
                    bfScrollBarUpdateValue(bfVSlBarUsers, dataGVUsers);
                } //Удаление пользователей
                else if (listUsers.Contains(admin))
                {
                    MBox("Вы не можете редактировать/удалять себя!", "Ошибка", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                } //Запрет на удаление Администратора
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
            bfChBoxAddUserFM.Enabled = bfChBoxAddUserM.Checked ? false : true;
        } //Пол: мужской

        private void bfChBoxAddUserFM_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            bfChBoxAddUserM.Enabled = bfChBoxAddUserFM.Checked ? false : true;
        } //Пол: женский
        #endregion

        #region Access Page

        private void tabPageAccess_Layout(object sender, LayoutEventArgs e)
        {
            using (SqlCommand sqlCommand = new SqlCommand(
                $"SELECT [HWIDUsers].Login, [HWIDUsers].HWID as [Current HWID], [UsersMVD].HWID as [Reg HWID], IP FROM [HWIDUsers] LEFT JOIN [UsersMVD]\n" +
                $"ON[HWIDUsers].Login = [UsersMVD].Login WHERE Visible = 1", DataBaseSqlCommands.sqlConnection))
                dataGVAccess.DataSource = DataBaseSqlCommands.SelectSqlCommands(sqlCommand);

            Parallel.Invoke(() =>
            {
                if (dataGVAccess.RowCount > 0)
                {
                    dataGVAccess.Columns[0].Width = 120;
                    dataGVAccess.Rows[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGVAccess.Columns.OfType<DataGridViewColumn>().ToList().ForEach(x =>
                    {
                        x.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    });
                } 
            }, //Форматирование столбцов

            () =>
            {
                bfScrollBarUpdateValue(bfSBAccess, dataGVAccess);
            }); //Обновить скроллбар
        }

        private void dataGVAccess_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MBox($"Отклонить запрос на получение доступа для {dataGVAccess.Rows[e.RowIndex].Cells[0].Value}?", "Вопрос") == DialogResult.Yes)
            {
                using (SqlCommand sqlCommand = new SqlCommand($"UPDATE [HWIDUsers] SET Visible = 0 WHERE Login = @login", DataBaseSqlCommands.sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@login", dataGVAccess.Rows[e.RowIndex].Cells[0].Value);
                    DataBaseSqlCommands.UpdInsDelSqlCommands(sqlCommand);
                    dataGVAccess.Rows.RemoveAt(e.RowIndex);
                    bfScrollBarUpdateValue(bfSBAccess, dataGVAccess);
                }
            }

        } //Отказать в доступе

        private void bfBtnApproveAccess_Click(object sender, EventArgs e)
        {
            if (dataGVAccess.SelectedRows != null && dataGVAccess.RowCount > 0)
            {
                if (MBox($"Выдать доступ для {dataGVAccess.CurrentRow.Cells[0].Value}?", "Вопрос") == DialogResult.Yes)
                {
                    using (SqlCommand sqlCommand = new SqlCommand(
                        $"UPDATE [HWIDUsers] SET Visible = 0 WHERE Login = @login\n" +
                        $"UPDATE [UsersMVD] SET HWID = @hwid WHERE Login = @login", DataBaseSqlCommands.sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@login", dataGVAccess.CurrentRow.Cells[0].Value);
                        sqlCommand.Parameters.AddWithValue("@hwid", dataGVAccess.CurrentRow.Cells[1].Value);
                        DataBaseSqlCommands.UpdInsDelSqlCommands(sqlCommand);
                        dataGVAccess.Rows.Remove(dataGVAccess.CurrentRow);
                        bfScrollBarUpdateValue(bfSBAccess, dataGVAccess);
                    }
                } //Выдать доступ
            }
        } //Выдать доступ только для 1 пользователя

        private void bfBtnRejectAccess_Click(object sender, EventArgs e)
        {
            if (dataGVAccess.SelectedRows != null && dataGVAccess.RowCount > 0)
            {
                List<string> logins = new List<string>();
                dataGVAccess.SelectedRows.OfType<DataGridViewRow>().OrderBy(x => x.Index).ToList().ForEach(x =>
                {
                    logins.Add(x.Cells[0].Value.ToString());
                }); //Выбранные пользователи

                if (MBox($"Отклонить запрос на получение доступа для:\n{string.Join(Environment.NewLine,logins)}?", "Вопрос") == DialogResult.Yes)
                {
                    foreach (DataGridViewRow rows in dataGVAccess.SelectedRows)
                    {
                        using (SqlCommand sqlcommand = new SqlCommand($"UPDATE [HWIDUsers] SET Visible = 0 WHERE Login = @login",DataBaseSqlCommands.sqlConnection))
                        {
                            sqlcommand.Parameters.AddWithValue("@login", rows.Cells[0].Value);
                            DataBaseSqlCommands.UpdInsDelSqlCommands(sqlcommand);
                        }
                        dataGVAccess.Rows.RemoveAt(rows.Index);
                    } //Отказать в доступе выбранным пользователям
                    bfScrollBarUpdateValue(bfSBAccess, dataGVAccess);
                }
            }
        } //Отказать в доступе выбранным пользователям

        private void bfBtnAllAccess_Click(object sender, EventArgs e)
        {
            using (SqlCommand sqlCommand = new SqlCommand(
                $"SELECT [HWIDUsers].Login, [HWIDUsers].HWID as [Current HWID], [UsersMVD].HWID as [Reg HWID], IP FROM [HWIDUsers] LEFT JOIN [UsersMVD]\n" +
                $"ON[HWIDUsers].Login = [UsersMVD].Login\n", DataBaseSqlCommands.sqlConnection))
                dataGVAccess.DataSource = DataBaseSqlCommands.SelectSqlCommands(sqlCommand);
            bfScrollBarUpdateValue(bfSBAccess, dataGVAccess);
        } //Показать всех пользователей

        private void bfBtnNoAccess_Click(object sender, EventArgs e)
        {
            using (SqlCommand sqlCommand = new SqlCommand(
                $"SELECT [HWIDUsers].Login, [HWIDUsers].HWID as [Current HWID], [UsersMVD].HWID as [Reg HWID], IP FROM [HWIDUsers] LEFT JOIN [UsersMVD]\n" +
                $"ON[HWIDUsers].Login = [UsersMVD].Login WHERE Visible = 1", DataBaseSqlCommands.sqlConnection))
                dataGVAccess.DataSource = DataBaseSqlCommands.SelectSqlCommands(sqlCommand);
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

        private void dataGVAccess_MouseWheel(object sender, MouseEventArgs e)
        {
            dataGridViewMouseWheel(dataGVAccess, bfSBAccess, e);
        } //Прокрутка в таблице

        private void dataGVAccess_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < e.RowCount; i++)
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\DataBaseMVD\AdminSettings"))
                {
                    if (dataGVAccess.Rows[e.RowIndex + i].Cells[3].Value.ToString().Contains(key?.GetValue("IPMask")?.ToString().Substring(0, 7)))
                    {
                        dataGVAccess.Rows[e.RowIndex + i].DefaultCellStyle.ForeColor = Color.Silver;
                        dataGVAccess.Rows[e.RowIndex + i].DefaultCellStyle.SelectionForeColor = Color.Gainsboro;
                    } //Маска IP совпадает
                    else
                    {
                        dataGVAccess.Rows[e.RowIndex + i].DefaultCellStyle.ForeColor = Color.Red;
                        dataGVAccess.Rows[e.RowIndex + i].DefaultCellStyle.SelectionForeColor = Color.Red;
                    } //Маска IP не совпадает с IP запроса на доступ
                }
            }
        } //Выделить подозрительные строки для доступа

        private void bfSBAccess_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            bfScrollBarUpdateDataGrid(dataGVAccess, e);
        }
        #endregion

        #region Settings Page

        private void tabPageSettings_Layout(object sender, LayoutEventArgs e)
        {
            LoadUserDataOnSettings();
        }

        private void bfBtnHideSWSett_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {
            bfToggleSwitchCheck();
        } //Скрыть кнопки

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
                    sqlCommand.Parameters.AddWithValue("@login", admin);
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
                                    sqlCommand2.Parameters.AddWithValue("@login", admin);
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
                            } //Новый и старый пароли одинаковые
                        } //Проверка на подтверждение пароля
                        else
                        {
                            MBox("Пароли должны совпадать!", "Ошибка", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                        } //Совпадение новых паролей
                    } //Пароль правильный
                    else
                    {
                        MBox("Неправильно указан старый пароль!", "Ошибка", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    } //Неправильный пароль
                }
            } //Все поля заполнены
            else
            {
                MBox("Заполнены не все поля!", "Ошибка",MessageBoxIcon.Warning,MessageBoxButtons.OK);
            } //Не все поля заполнены
        } //Change password button on Settings

        private void bfTBIpAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && IPAddress.TryParse(bfTBIpAddress.Text, out IPAddress ip))
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\DataBaseMVD\AdminSettings"))
                {
                    key.SetValue("IPMask", ip);
                    if (key?.GetValue("IPMask")?.ToString() == ip.ToString())
                        MBox("IP-адрес сохранён!", "Информация", MessageBoxIcon.Information, MessageBoxButtons.OK);
                } //Сохранить IP в реестр
            }
            else if (e.KeyChar == (char)Keys.Enter)
                MBox("Неверный IP-адрес!", "Ошибка", MessageBoxIcon.Error, MessageBoxButtons.OK);
        } //Нажав Enter, проверить IP и сохранить в реестр

        async void bfTBIpAddress_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            await Task.Run(() =>
            {
                using (WebClient wc = new WebClient())
                    bfTBIpAddress.Text = wc.DownloadString("https://api.ipify.org");
                MBox("Импортирован IP-адрес текущего компьютера", "Информация", MessageBoxIcon.Information, MessageBoxButtons.OK);
            });
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
                this.Shown += async(s, a) =>
                {
                    while (webBrowser1.ReadyState != WebBrowserReadyState.Complete) { Application.DoEvents(); }
                    for (Opacity = 0; Opacity <= 0.98; Opacity += 0.01)
                    {
                        await Task.Delay(10);
                    }
                };
            });
        } //Плавное появление формы

        private void bfToggleSwitchCheck()
        {
            if (bfBtnHideSWSett.Checked)
                {
                    CriminalRecords.Visible = false;
                    Information.Visible = false;
                    Button3.Location = new Point(0, 168);
                    Button4.Location = new Point(0, 228);
                    Button5.Location = new Point(0, 288);
                    Button5.PerformClick();
            } //Скрыть Criminals and Infromation Pages
            else
            {
                CriminalRecords.Visible = true;
                Information.Visible = true;
                Button3.Location = new Point(0, 288);
                Button4.Location = new Point(0, 348);
                Button5.Location = new Point(0, 408);
                Button5.PerformClick();
            } //Показать Criminals and Infromation Pages
            Registry.CurrentUser.CreateSubKey(@"Software\DataBaseMVD\AdminSettings")?.SetValue("ButtonsHide", bfBtnHideSWSett.Checked);
        } //Скрыть кнопки на форме
        
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
                sqlCommand.Parameters.AddWithValue("@login", admin);
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

        async void LoadUserDataForEdit(string login)
        {
            using (SqlCommand sqlCommand = new SqlCommand(
                $"SELECT * FROM [UsersMVD] FULL OUTER JOIN [Сотрудники] ON [UsersMVD].Login = [Сотрудники].Логин WHERE Login = @login", DataBaseSqlCommands.sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@login", login);
                using (DataTable dataTable = DataBaseSqlCommands.SelectSqlCommands(sqlCommand))
                {
                    if (dataTable.Rows.Count > 0)
                        await Task.Run(() =>
                        {
                            bfTBUserAddLogin.Text = dataTable.Rows[0].ItemArray[2].ToString() ?? "";
                            bfTBUNameAddUser.Text = dataTable.Rows[0].ItemArray[5].ToString() ?? "";
                            bfTBUNumberAddUser.Text = dataTable.Rows[0].ItemArray[9].ToString() ?? "";
                            bfTBUAddressAddUser.Text = dataTable.Rows[0].ItemArray[8].ToString() ?? "";
                            bfDwnUserAdd.SelectedItem = dataTable.Rows[0].ItemArray[0].ToString() ?? "";
                            bfChBoxAddUserM.Checked = dataTable.Rows[0].ItemArray[7].ToString() == "М" ? true : false;
                            bfChBoxAddUserFM.Checked = dataTable.Rows[0].ItemArray[7].ToString() == "Ж" ? true : false;
                            try { bfDwnPostUserAdd.SelectedIndex = Convert.ToInt32(dataTable.Rows[0].ItemArray[10]) - 1; } catch { }
                            try { bfDwnRangUserAdd.SelectedIndex = Convert.ToInt32(dataTable.Rows[0].ItemArray[11]) - 1; } catch { }
                        }); //Заполнение полей данными из таблицы
                }
            }
        } //Загрузить информацию о пользователе для редактирования

        async void LoadCriminalDataForEdit(string id)
        {
            using (SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM [Преступники] WHERE [Номер дела] = @criminal", DataBaseSqlCommands.sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@criminal", id);
                using (DataTable dataTable = DataBaseSqlCommands.SelectSqlCommands(sqlCommand))
                {
                    if (dataTable.Rows.Count > 0)
                        await Task.Run(() =>
                        {
                            bfTBIDAddCrim.Text = dataTable.Rows[0].ItemArray[0].ToString();
                            bfTBNameAddCrim.Text = dataTable.Rows[0].ItemArray[1].ToString();
                            bfTBDataAddCrim.Text = dataTable.Rows[0].ItemArray[2].ToString().Substring(0, 10);
                            bfTBAddressAddCrim.Text = dataTable.Rows[0].ItemArray[4].ToString();
                            bfDwnStateAddCrim.SelectedItem = dataTable.Rows[0].ItemArray[7].ToString();
                            bfChBoxAddCrimM.Checked = dataTable.Rows[0].ItemArray[3].ToString() == "М" ? true : false;
                            bfChBoxAddCrimFM.Checked = dataTable.Rows[0].ItemArray[3].ToString() == "Ж" ? true : false;
                            try { bfDwnAddCriminals.SelectedIndex = Convert.ToInt32(dataTable.Rows[0].ItemArray[5]) - 1; } catch { }
                            try { bfDwnRecordsAddCrim.SelectedIndex = Convert.ToInt32(dataTable.Rows[0].ItemArray[6]) - 1; } catch { }
                        }); //Заполнение полей данными из таблицы
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
                    using (SqlCommand sqlCommand = new SqlCommand(
                            //$"OPEN SYMMETRIC KEY SymmetricKeyDataBaseUsersMVD\n" +
                            //$"DECRYPTION BY CERTIFICATE CertificateDataBaseUsersMVD\n" +
                            //$"INSERT INTO [UsersMVD](Permission,Login,Password) VALUES(@perm,@login,ENCRYPTBYKEY(KEY_GUID('SymmetricKeyDataBaseUsersMVD'),@pass))\n" +
                            //$"CLOSE SYMMETRIC KEY SymmetricKeyDataBaseUsersMVD\n" +
                            $"INSERT INTO [Сотрудники](ФИО, Логин, Пол, Адрес, Телефон, [Код должности], [Код звания])\n" +
                            $"VALUES (@name, @login, @sex, @address, @number, @post, @rang)\n" +
                            $"INSERT INTO [UsersMVD](Permission, Login, Password) VALUES(@perm, @login, HASHBYTES('SHA2_512', [dbo].[EncryptedPassword](@pass)))\n" +
                            $"SELECT Permission,Login,Desktop,HWID FROM [UsersMVD]", DataBaseSqlCommands.sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@perm", bfDwnUserAdd.SelectedItem);
                        sqlCommand.Parameters.AddWithValue("@login", bfTBUserAddLogin.Text);
                        sqlCommand.Parameters.AddWithValue("@pass", bfTBPassUserAdd.Text);
                        sqlCommand.Parameters.AddWithValue("@name", bfTBUNameAddUser.Text);
                        sqlCommand.Parameters.AddWithValue("@address", bfTBUAddressAddUser.Text);
                        sqlCommand.Parameters.AddWithValue("@number", bfTBUNumberAddUser.Text);
                        sqlCommand.Parameters.AddWithValue("@post", bfDwnPostUserAdd.SelectedIndex + 1);
                        sqlCommand.Parameters.AddWithValue("@rang", bfDwnRangUserAdd.SelectedIndex + 1);
                        sqlCommand.Parameters.AddWithValue("@sex", bfChBoxAddUserM.Checked ? "М" : "Ж");
                        dataGVUsers.DataSource = DataBaseSqlCommands.SelectSqlCommands(sqlCommand);
                    }
                    ClearAllOnAddUser();
                    bfScrollBarUpdateValue(bunifuVScrollBar1, dataGVDBCrimRecords);
                    FormPages.SelectedTab = tabPageUsers;
                }
                catch (SqlException error)
                {
                    MBox(error.Number == 2627 ? "Пользователь с таким логином уже существует!" : error.Message, "Ошибка", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    bfTBUserAddLogin.Focus();
                    bfTBUserAddLogin.SelectAll();
                } //Добавление пользователя в таблицу и БД, если ошибка, уведомить
            } //Если все поля заполнены
            else
            {
                MBox("Заполнены не все данные!", "Ошибка", MessageBoxIcon.Warning, MessageBoxButtons.OK);
            }
        } //Добавить пользователя

        private void UserDataEdit()
        {
            if (!tabPageAddUser.Controls.OfType<Bunifu.UI.WinForms.BunifuDropdown>().Any(x => x.SelectedItem == null)
                && !tabPageAddUser.Controls.OfType<Bunifu.UI.WinForms.BunifuTextBox>().Any(x => string.IsNullOrEmpty(x.Text) && !x.Name.Contains("Pass"))
                && tabPageAddUser.Controls.OfType<Bunifu.UI.WinForms.BunifuCheckBox>().Any(x => x.Checked)
                && bfTBUNumberAddUser.Text.Length == 18)
            {
                try
                {
                    using (SqlCommand sqlCommand = new SqlCommand(
                            $"UPDATE [Сотрудники] SET ФИО = @name, Логин = @loginNew, Пол = @sex, Адрес = @address, Телефон = @number,\n" +
                            $"[Код должности] = @post, [Код звания] = @rang WHERE Логин = @loginOld\n" +
                            $"UPDATE [UsersMVD] SET Permission = @perm, Login = @loginNew WHERE Login = @loginOld\n" +
                            $"SELECT Permission, Login, Desktop, HWID FROM [UsersMVD]", DataBaseSqlCommands.sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@perm", bfDwnUserAdd.SelectedItem);
                        sqlCommand.Parameters.AddWithValue("@loginNew", bfTBUserAddLogin.Text);
                        sqlCommand.Parameters.AddWithValue("@loginOld", dataGVUsers.CurrentRow.Cells[1].Value.ToString());
                        sqlCommand.Parameters.AddWithValue("@name", bfTBUNameAddUser.Text);
                        sqlCommand.Parameters.AddWithValue("@address", bfTBUAddressAddUser.Text);
                        sqlCommand.Parameters.AddWithValue("@number", bfTBUNumberAddUser.Text);
                        sqlCommand.Parameters.AddWithValue("@post", bfDwnPostUserAdd.SelectedIndex + 1);
                        sqlCommand.Parameters.AddWithValue("@rang", bfDwnRangUserAdd.SelectedIndex + 1);
                        sqlCommand.Parameters.AddWithValue("@sex", bfChBoxAddUserM.Checked ? "М" : "Ж");
                        dataGVUsers.DataSource = DataBaseSqlCommands.SelectSqlCommands(sqlCommand);
                    }
                    ClearAllOnAddUser();
                    FormPages.SelectedTab = tabPageUsers;
                }
                catch (SqlException error)
                {
                    MBox(error.Number == 2627 ? "Пользователь с таким логином уже существует!" : error.Message, "Ошибка", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    bfTBUserAddLogin.Focus();
                    bfTBUserAddLogin.SelectAll();
                } //Отредактировать данные о пользователе, если ошибка, уведомить
            } //Если все поля заполнены
            else
            {
                MBox("Заполнены не все данные!", "Ошибка", MessageBoxIcon.Warning, MessageBoxButtons.OK);
            }
        } //Редактировать пользователя

        private void AddCriminal()
        {
            if (!tabPageAddCriminal.Controls.OfType<Bunifu.UI.WinForms.BunifuDropdown>().Any(x => x.SelectedItem == null)
                && !tabPageAddCriminal.Controls.OfType<Bunifu.UI.WinForms.BunifuTextBox>().Any(x => string.IsNullOrEmpty(x.Text))
                && tabPageAddCriminal.Controls.OfType<Bunifu.UI.WinForms.BunifuCheckBox>().Any(x => x.Checked))
            {
                try
                {
                    using (SqlCommand sqlCommand = new SqlCommand(
                            $"INSERT INTO [Преступники] VALUES(@ID, @name, @data, @sex, @address, @crim, @record, @state)" +
                            $"SELECT [Номер дела],ФИО,[Преступления].Наименование AS Преступление, " +
                            $"[Розыск].Наименование AS Розыск,Состояние FROM [Преступники]\n" +
                            $"LEFT OUTER JOIN[Преступления]ON[Преступники].[Код преступления]=[Преступления].[Код преступления]\n" +
                            $"LEFT OUTER JOIN[Розыск]ON[Преступники].[Код розыска]=[Розыск].[Код розыска]", DataBaseSqlCommands.sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@ID", bfTBIDAddCrim.Text);
                        sqlCommand.Parameters.AddWithValue("@name", bfTBNameAddCrim.Text);
                        sqlCommand.Parameters.AddWithValue("@sex", bfChBoxAddUserM.Checked ? "М" : "Ж");
                        sqlCommand.Parameters.AddWithValue("@address", bfTBAddressAddCrim.Text);
                        sqlCommand.Parameters.AddWithValue("@crim", bfDwnAddCriminals.SelectedIndex + 1);
                        sqlCommand.Parameters.AddWithValue("@record", bfDwnRecordsAddCrim.SelectedIndex + 1);
                        sqlCommand.Parameters.AddWithValue("@state", bfDwnStateAddCrim.SelectedItem);
                        try { sqlCommand.Parameters.AddWithValue("@data", DateTime.Parse(bfTBDataAddCrim.Text)); } catch { }
                        dataGVDBCrimRecords.DataSource = DataBaseSqlCommands.SelectSqlCommands(sqlCommand);
                    }
                    ClearAllOnAddCrim();
                    bfScrollBarUpdateValue(bunifuVScrollBar1, dataGVDBCrimRecords);
                    FormPages.SelectedTab = tabPageCriminalRecords;
                }
                catch (SqlException error)
                {
                    MBox(error.Number == 2627 ? "Преступник с таким делом уже существует!" : error.Message, "Ошибка", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    bfTBIDAddCrim.Focus();
                    bfTBIDAddCrim.SelectAll();
                } //Добавление пользователя в таблицу и БД, если ошибка, уведомить
            } //Если все поля заполнены
            else
            {
                MBox("Заполнены не все данные!", "Ошибка", MessageBoxIcon.Warning, MessageBoxButtons.OK);
            }
        } //Добавить преступника

        private void CriminalDataEdit()
        {
            if (!tabPageAddCriminal.Controls.OfType<Bunifu.UI.WinForms.BunifuDropdown>().Any(x => x.SelectedItem == null)
                && !tabPageAddCriminal.Controls.OfType<Bunifu.UI.WinForms.BunifuTextBox>().Any(x => string.IsNullOrEmpty(x.Text))
                && tabPageAddCriminal.Controls.OfType<Bunifu.UI.WinForms.BunifuCheckBox>().Any(x => x.Checked))
            {
                try
                {
                    using (SqlCommand sqlCommand = new SqlCommand(
                            $"UPDATE [Преступники] SET [Номер дела] = @IDNew, ФИО = @name, [Дата рождения] = @data, Пол = @sex,\n" +
                            $"Адрес = @address, [Код преступления] = @crim, [Код розыска] = @record, Состояние = @state WHERE [Номер дела] = @IDOld\n" +
                            $"SELECT [Номер дела],ФИО,[Преступления].Наименование AS Преступление, " +
                            $"[Розыск].Наименование AS Розыск,Состояние FROM [Преступники]\n" +
                            $"LEFT OUTER JOIN[Преступления]ON[Преступники].[Код преступления]=[Преступления].[Код преступления]\n" +
                            $"LEFT OUTER JOIN[Розыск]ON[Преступники].[Код розыска]=[Розыск].[Код розыска]", DataBaseSqlCommands.sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@IDNew", bfTBIDAddCrim.Text);
                        sqlCommand.Parameters.AddWithValue("@name", bfTBNameAddCrim.Text);
                        sqlCommand.Parameters.AddWithValue("@sex", bfChBoxAddCrimM.Checked ? "М" : "Ж");
                        sqlCommand.Parameters.AddWithValue("@address", bfTBAddressAddCrim.Text);
                        sqlCommand.Parameters.AddWithValue("@crim", bfDwnAddCriminals.SelectedIndex + 1);
                        sqlCommand.Parameters.AddWithValue("@record", bfDwnRecordsAddCrim.SelectedIndex + 1);
                        sqlCommand.Parameters.AddWithValue("@state", bfDwnStateAddCrim.SelectedItem);
                        sqlCommand.Parameters.AddWithValue("@IDOld", dataGVDBCrimRecords.CurrentRow.Cells[0].Value.ToString());
                        try { sqlCommand.Parameters.AddWithValue("@data", DateTime.Parse(bfTBDataAddCrim.Text)); } catch { }
                        dataGVDBCrimRecords.DataSource = DataBaseSqlCommands.SelectSqlCommands(sqlCommand);
                    }
                    ClearAllOnAddCrim();
                    FormPages.SelectedTab = tabPageCriminalRecords;
                }
                catch (SqlException error)
                {
                    MBox(error.Number == 2627 ? "Преступник с таким делом уже существует!" : error.Message, "Ошибка", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    bfTBIDAddCrim.Focus();
                    bfTBIDAddCrim.SelectAll();
                } //Отредактировать данные о преступнике, если ошибка, уведомить
            } //Если все поля заполнены
            else
            {
                MBox("Заполнены не все данные!", "Ошибка", MessageBoxIcon.Warning, MessageBoxButtons.OK);
            }
        } //Редактировать преступника

        async void ClearAllOnAddUser()
        {
            await Task.Run(() =>
            {
                tabPageAddUser.Controls.OfType<Bunifu.UI.WinForms.BunifuTextBox>().AsParallel().ForAll(x => x.Text = "");
                tabPageAddUser.Controls.OfType<Bunifu.UI.WinForms.BunifuDropdown>().AsParallel().ForAll(x => x.SelectedItem = null);
                tabPageAddUser.Controls.OfType<Bunifu.UI.WinForms.BunifuCheckBox>().AsParallel().ForAll(x => x.Checked = false);
                bfDwnUserAdd.Text = "Выберите доступ";
                bfDwnRangUserAdd.Text = "Выберите звание";
                bfDwnPostUserAdd.Text = "Выберите должность";
            });
        }  //Стирает введённые данные (AddUser)

        async void ClearAllOnAddCrim()
        {
            await Task.Run(() =>
            {
                tabPageAddCriminal.Controls.OfType<Bunifu.UI.WinForms.BunifuTextBox>().AsParallel().ForAll(x => x.Text = "");
                tabPageAddCriminal.Controls.OfType<Bunifu.UI.WinForms.BunifuDropdown>().AsParallel().ForAll(x => x.SelectedItem = null);
                tabPageAddCriminal.Controls.OfType<Bunifu.UI.WinForms.BunifuCheckBox>().AsParallel().ForAll(x => x.Checked = false);
                bfDwnAddCriminals.Text = "Выберите преступление";
                bfDwnRecordsAddCrim.Text = "Выберите розыск";
                bfDwnStateAddCrim.Text = "Выберите состояние";
            });
        }  //Стирает введённые данные (AddCriminal)

        private DialogResult MBox(string message, string text, MessageBoxIcon img = MessageBoxIcon.Question, MessageBoxButtons btn = MessageBoxButtons.YesNo, MessageBoxDefaultButton def = MessageBoxDefaultButton.Button2)
        {
            return MessageBox.Show(message,text,btn,img,def);
        } //Вызов MessageBox
        #endregion
    }
}
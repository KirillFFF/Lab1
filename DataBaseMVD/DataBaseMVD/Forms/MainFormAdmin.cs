using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using DataBaseMVD.Properties;
using System.Diagnostics;
using System.Management;
using System.IO;
using System.Collections;
using System.Management.Instrumentation;
using yt_DesignUI.Components;
using yt_DesignUI.Controls;
using yt_DesignUI;

namespace DataBaseMVD
{
    public partial class MainFormAdmin : Form
    {
        private readonly SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseMVD"].ConnectionString);
        public string admin;
        public string DefaultPassword;

        public MainFormAdmin()
        {
            InitializeComponent();
            Animator.Start();
        }
        
        private void MainFormAdmin_Load(object sender, EventArgs e)
        {
            this.MaximumSize = new Size(this.Width, this.Height);
            this.AllowTransparency = false;
            SqlCommand sqlCommand = new SqlCommand($"SELECT Permission, Login, Desktop, HWID FROM [UsersMVD]", sqlConnection);
            DataGridViewDataBaseUsersMVD.DataSource = DataBaseSqlCommands.SelectSqlCommands(sqlCommand);
            DataGridViewDataBaseUsersMVD.CurrentCell = null;
            DataGridViewDataBaseUsersMVD.ContextMenuStrip.Opening += (object sender1, CancelEventArgs a) => { a.Cancel=true; };
            sqlCommand = new SqlCommand($"SELECT Login,HWID FROM [HWIDUsers] WHERE Visible=1", sqlConnection);
            DataGridViewHWIDUsers.DataSource = DataBaseSqlCommands.SelectSqlCommands(sqlCommand);
            DataGridViewHWIDUsers.CurrentCell = null;
            DataGridViewHWIDUsers.ContextMenuStrip.Opening += (object sender1, CancelEventArgs a) => { a.Cancel = true; };
            panelDataBase.Hide();
            checkBoxDefaultPass.Checked = true;

            foreach (DataGridViewTextBoxColumn j in DataGridViewDataBaseUsersMVD.Columns)
            {
                comboBoxFilter.Items.AddRange(new object[] { j.HeaderText});
            }
            comboBoxFilter.SelectedIndex = 0;
        }

        async void PictureBoxMenu_Click(object sender, EventArgs e)
        {
            DoubleBuffered = true;

            switch (PictureBoxMenu.Tag.ToString())
            {
                case "MainAdminFormOpenImage":
                    {
                        panelDataBase.Size = new Size(1, panelDataBase.Height);
                        panelDataBase.Show();
                        PictureBoxMenu.Image = Resources.MainAdminFormOpenGif;
                        await Task.Delay(700);
                        while (panelDataBase.Width <= 520)
                        {
                            await Task.Delay(10);
                            panelDataBase.Size = new Size(panelDataBase.Width + 20, panelDataBase.Height);
                        }
                        PictureBoxMenu.Image = Resources.MainAdminFormCloseImage;
                        PictureBoxMenu.Tag = "MainAdminFormCloseImage"; return;
                    }

                case "MainAdminFormCloseImage":
                    {
                        PictureBoxMenu.Image = Resources.MainAdminFormCloseGif;
                        await Task.Delay(1500);
                        while (panelDataBase.Width >= 1)
                        {
                            await Task.Delay(10);
                            panelDataBase.Size = new Size(panelDataBase.Width - 40, panelDataBase.Height);
                        }
                        PictureBoxMenu.Image = Resources.MainAdminFormOpenImage;
                        PictureBoxMenu.Tag = "MainAdminFormOpenImage";
                        panelDataBase.Hide(); return;
                    }
            }
        }

        private void DataGridViewDataBaseUsersMVD_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {                
                switch (e.ColumnIndex)
                {
                    case 0:
                        {
                            foreach (ToolStripMenuItem i in ContextMenuStripDataBaseUsers.Items)
                            {
                                if (i != ContextMenuStripDataBaseUsers.Items[0])
                                    i.Visible = false;
                                else i.Visible = true;
                            }
                            DataGridViewDataBaseUsersMVD.ContextMenuStrip.Opening += (object sender1, CancelEventArgs a) => 
                            { a.Cancel = false; };
                            Point position = Cursor.Position;
                            ContextMenuStripDataBaseUsers.Show(position); return;
                        }
                    case 1:
                        {
                            foreach (ToolStripMenuItem i in ContextMenuStripDataBaseUsers.Items)
                            {
                                if (i != ContextMenuStripDataBaseUsers.Items[1])
                                    i.Visible = false;
                                else i.Visible = true;
                            }
                            DataGridViewDataBaseUsersMVD.ContextMenuStrip.Opening += (object sender1, CancelEventArgs a) => 
                            { a.Cancel = false; };
                            Point position = Cursor.Position;
                            ContextMenuStripDataBaseUsers.Show(position); return;
                        }
                }
            }
        }

        private void DataGridViewDataBaseUsersMVD_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridViewDataBaseUsersMVD.ContextMenuStrip.Opening += (object sender1, CancelEventArgs a) => 
                { a.Cancel = true; };
            }
        }

        private void DataGridViewDataBaseUsersMVD_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridViewDataBaseUsersMVD.ContextMenuStrip.Opening += (object sender1, CancelEventArgs a) =>
                { a.Cancel = true; };
            }
        }

        private void btnMenuAddUser_MouseEnter(object sender, EventArgs e)
        {
            DataGridViewDataBaseUsersMVD.CurrentCell = null;
        }

        async void btnMenuAddUser_Click(object sender, EventArgs e)
        {
            comboBoxPermissionAdd.SelectedIndex = 0;
            panelAddUser.Visible = true;
            while (panelAddUser.Location.X < 0)
            {
                await Task.Delay(10);
                panelAddUser.Location = new Point(
                    panelAddUser.Location.X - panelAddUser.Location.X / 7 + 1, panelAddUser.Location.Y);
            }
        }

        async void panelDataBase_MouseEnter(object sender, EventArgs e)
        {
            if (panelAddUser.Visible && panelAddUser.Location.X==0)
            {
                while (panelAddUser.Location.X > -500)
                {
                    await Task.Delay(10);
                    panelAddUser.Location = new Point(
                        panelAddUser.Location.X - 1 + panelAddUser.Location.X / 7, panelAddUser.Location.Y);
                }
                panelAddUser.Visible = false;
            }
        }

        private void checkBoxDefaultPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDefaultPass.Checked)
            {
                textBoxDefaultPass.Enabled = false;
                DefaultPassword = " ";
            }
            else
            {
                textBoxDefaultPass.Enabled = true;
                textBoxDefaultPass.TextChanged += (object sender1, EventArgs a) =>
                { DefaultPassword = textBoxDefaultPass.Text; };
            }
        }

        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(textBoxLoginAdd.Text) && !String.IsNullOrEmpty(comboBoxPermissionAdd.Text))
                {
                    if (comboBoxPermissionAdd.Text == "admin" || comboBoxPermissionAdd.Text == "user")
                    {
                        SqlCommand sqlCommand = new SqlCommand(
                            $"OPEN SYMMETRIC KEY SymmetricKeyDataBaseUsersMVD\n" +
                            $"DECRYPTION BY CERTIFICATE CertificateDataBaseUsersMVD\n" +
                            $"INSERT INTO [UsersMVD](Permission,Login,Password) VALUES(@perm,@login,ENCRYPTBYKEY(KEY_GUID('SymmetricKeyDataBaseUsersMVD'),@pass))\n" +
                            $"CLOSE SYMMETRIC KEY SymmetricKeyDataBaseUsersMVD\n" +
                            $"SELECT Permission,Login,Desktop,HWID FROM [UsersMVD]", sqlConnection);
                        sqlCommand.Parameters.AddWithValue("@perm", comboBoxPermissionAdd.SelectedItem);
                        sqlCommand.Parameters.AddWithValue("@login", textBoxLoginAdd.Text);
                        sqlCommand.Parameters.AddWithValue("@pass", DefaultPassword);
                        DataGridViewDataBaseUsersMVD.DataSource = DataBaseSqlCommands.SelectSqlCommands(sqlCommand);
                        textBoxLoginAdd.Text = "";
                        comboBoxPermissionAdd.Text = "";
                    }
                }
            }
            catch
            {
                MessageBox.Show("Данный логин занят!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void ToolStripMenuItemAcceptAccount_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand(
                $"UPDATE [HWIDUsers] SET Visible=0 WHERE Login=@login\n" +
                $"UPDATE [UsersMVD] SET HWID=@hwid WHERE Login=@login\n" +
                $"SELECT Login,HWID FROM [HWIDUsers] WHERE Visible=1", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@login", DataGridViewHWIDUsers.CurrentRow.Cells[0].Value);
            sqlCommand.Parameters.AddWithValue("@hwid", DataGridViewHWIDUsers.CurrentRow.Cells[1].Value);
            DataGridViewHWIDUsers.DataSource = DataBaseSqlCommands.SelectSqlCommands(sqlCommand);
            sqlCommand = new SqlCommand($"SELECT Permission,Login,Desktop,HWID FROM [UsersMVD]", sqlConnection);
            DataGridViewDataBaseUsersMVD.DataSource = DataBaseSqlCommands.SelectSqlCommands(sqlCommand);
        }

        private void ToolStripMenuItemRejectAccount_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand(
                $"UPDATE [HWIDUsers] SET Visible=0 WHERE Login=@login\n" +
                $"SELECT Login,HWID FROM [HWIDUsers] WHERE Visible=1", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@login", DataGridViewHWIDUsers.CurrentRow.Cells[0].Value);
            DataGridViewHWIDUsers.DataSource = DataBaseSqlCommands.SelectSqlCommands(sqlCommand);
            sqlCommand = new SqlCommand($"SELECT Permission,Login,Desktop,HWID FROM [UsersMVD]", sqlConnection);
            DataGridViewDataBaseUsersMVD.DataSource = DataBaseSqlCommands.SelectSqlCommands(sqlCommand);
        }

        private void DataGridViewHWIDUsers_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1 && e.Button == MouseButtons.Left)
            {
                DataGridViewHWIDUsers.ContextMenuStrip.Opening += (object sender1, CancelEventArgs a) =>
                { a.Cancel = false; };
                Point position = Cursor.Position;
                ContextMenuStripHWIDUsers.Show(position);
            }
        }

        private void ToolStripMenuItemEditPermission_DropDownOpening(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem i in ToolStripMenuItemEditPermission.DropDownItems)
            {
                if (i.ToString() == DataGridViewDataBaseUsersMVD.CurrentCell.Value.ToString())
                    i.Visible = false;
                else i.Visible = true;
            }
        }

        private void ToolStripMenuItemDeleteAccount_Click(object sender, EventArgs e)
        {
            if (DataGridViewDataBaseUsersMVD.CurrentCell.Value.ToString()!=admin)
            {
                DialogResult dialogResult = MessageBox.Show(
                    $"Вы уверены, что хотите удалить пользователя " +
                    $"{DataGridViewDataBaseUsersMVD.CurrentCell.Value}?", "Подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlCommand sqlCommand = new SqlCommand($"DELETE FROM [UsersMVD] WHERE Login=@login\n" +
                        $"SELECT Permission,Login,Desktop,HWID FROM [UsersMVD]", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@login", DataGridViewDataBaseUsersMVD.CurrentCell.Value);
                    DataGridViewDataBaseUsersMVD.DataSource = DataBaseSqlCommands.SelectSqlCommands(sqlCommand);
                }
            }
            else
                MessageBox.Show("В удалении отказано!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void UserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DataGridViewDataBaseUsersMVD.Rows[DataGridViewDataBaseUsersMVD.CurrentCell.RowIndex].Cells[1].Value.ToString()!=admin)
            {
                DialogResult dialogResult = MessageBox.Show($"Вы уверены, что хотите изменить уровень доступа у пользователя " +
                $"{DataGridViewDataBaseUsersMVD.Rows[DataGridViewDataBaseUsersMVD.CurrentCell.RowIndex].Cells[1].Value} " +
                $"на 'Пользователь'?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (dialogResult == DialogResult.Yes)
                {
                    SqlCommand sqlCommand = new SqlCommand(
                        $"UPDATE [UsersMVD] SET Permission=@perm WHERE Login=@login\n" +
                        $"SELECT Permission,Login,Desktop,HWID FROM [UsersMVD]", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@perm", UserToolStripMenuItem.ToString());
                    sqlCommand.Parameters.AddWithValue("@login", DataGridViewDataBaseUsersMVD.Rows[DataGridViewDataBaseUsersMVD.CurrentCell.RowIndex].Cells[1].Value);
                    DataGridViewDataBaseUsersMVD.DataSource = DataBaseSqlCommands.SelectSqlCommands(sqlCommand);
                }
            }
            else
                MessageBox.Show("В редактировании отказано!","Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void AdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Вы уверены, что хотите изменить уровень доступа у пользователя " +
                $"{DataGridViewDataBaseUsersMVD.Rows[DataGridViewDataBaseUsersMVD.CurrentCell.RowIndex].Cells[1].Value} " +
                $"на 'Администратор'?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dialogResult == DialogResult.Yes)
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $"UPDATE [UsersMVD] SET Permission=@perm WHERE Login=@login\n" +
                    $"SELECT Permission,Login,Desktop,HWID FROM [UsersMVD]", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@perm", AdminToolStripMenuItem.ToString());
                sqlCommand.Parameters.AddWithValue("@login", DataGridViewDataBaseUsersMVD.Rows[DataGridViewDataBaseUsersMVD.CurrentCell.RowIndex].Cells[1].Value);
                DataGridViewDataBaseUsersMVD.DataSource = DataBaseSqlCommands.SelectSqlCommands(sqlCommand);
            }
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            (DataGridViewDataBaseUsersMVD.DataSource as DataTable).DefaultView.RowFilter =
                $"{comboBoxFilter.SelectedItem} like '%{textBoxFilter.Text}%'";
            if (String.IsNullOrEmpty(textBoxFilter.Text))
            {
                (DataGridViewDataBaseUsersMVD.DataSource as DataTable).DefaultView.RowFilter = "";
            }
        }

        private void bfBtnMainFormOpen_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<MainForm>().Any())
            {
                Parallel.Invoke(() =>
                {
                    DataBaseSqlCommands.localUser = admin;
                    new MainForm().Show();
                });
            }
        }

        private void MainFormAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы действительно хотите закрыть приложение?", "База Данных МВД",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dialogResult == DialogResult.No)
            {
                e.Cancel = true;
            }
            else if (dialogResult == DialogResult.Yes)
            {
                SqlCommand sqlCommand = new SqlCommand(
                    $"UPDATE [UsersMVD] SET Desktop = NULL WHERE Login = @login", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@login", admin);
                DataBaseSqlCommands.UpdInsDelSqlCommands(sqlCommand);
                Application.ExitThread();
            }
        }
    }
}
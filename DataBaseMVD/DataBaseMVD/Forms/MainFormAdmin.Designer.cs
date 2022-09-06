
namespace DataBaseMVD
{
    partial class MainFormAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFormAdmin));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            this.PictureBoxMenu = new System.Windows.Forms.PictureBox();
            this.egoldsFormStyle1 = new yt_DesignUI.Components.EgoldsFormStyle(this.components);
            this.DataGridViewDataBaseUsersMVD = new System.Windows.Forms.DataGridView();
            this.ContextMenuStripDataBaseUsers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemEditPermission = new System.Windows.Forms.ToolStripMenuItem();
            this.UserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AdminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDeleteAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.panelDataBase = new System.Windows.Forms.Panel();
            this.panelAddUser = new System.Windows.Forms.Panel();
            this.groupBoxAddUser = new System.Windows.Forms.GroupBox();
            this.buttonAddUser = new System.Windows.Forms.Button();
            this.textBoxLoginAdd = new System.Windows.Forms.TextBox();
            this.comboBoxPermissionAdd = new System.Windows.Forms.ComboBox();
            this.labelLoginAdd = new System.Windows.Forms.Label();
            this.labelPermissionAdd = new System.Windows.Forms.Label();
            this.groupBoxDefaultPass = new System.Windows.Forms.GroupBox();
            this.textBoxDefaultPass = new System.Windows.Forms.TextBox();
            this.checkBoxDefaultPass = new System.Windows.Forms.CheckBox();
            this.labelHWIDUsers = new System.Windows.Forms.Label();
            this.DataGridViewHWIDUsers = new System.Windows.Forms.DataGridView();
            this.ContextMenuStripHWIDUsers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemAcceptAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemRejectAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.labelTextDB = new System.Windows.Forms.Label();
            this.labelTextBoxFilter = new System.Windows.Forms.Label();
            this.labelComBoxFilter = new System.Windows.Forms.Label();
            this.comboBoxFilter = new System.Windows.Forms.ComboBox();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.btnMenuAddUser = new System.Windows.Forms.Button();
            this.bfBtnMainFormOpen = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewDataBaseUsersMVD)).BeginInit();
            this.ContextMenuStripDataBaseUsers.SuspendLayout();
            this.panelDataBase.SuspendLayout();
            this.panelAddUser.SuspendLayout();
            this.groupBoxAddUser.SuspendLayout();
            this.groupBoxDefaultPass.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewHWIDUsers)).BeginInit();
            this.ContextMenuStripHWIDUsers.SuspendLayout();
            this.SuspendLayout();
            // 
            // PictureBoxMenu
            // 
            this.PictureBoxMenu.BackgroundImage = global::DataBaseMVD.Properties.Resources.MainAdminFormOpenImage;
            this.PictureBoxMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxMenu.Image = global::DataBaseMVD.Properties.Resources.MainAdminFormOpenImage;
            this.PictureBoxMenu.Location = new System.Drawing.Point(24, 12);
            this.PictureBoxMenu.Name = "PictureBoxMenu";
            this.PictureBoxMenu.Size = new System.Drawing.Size(43, 39);
            this.PictureBoxMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxMenu.TabIndex = 0;
            this.PictureBoxMenu.TabStop = false;
            this.PictureBoxMenu.Tag = "MainAdminFormOpenImage";
            this.PictureBoxMenu.Click += new System.EventHandler(this.PictureBoxMenu_Click);
            // 
            // egoldsFormStyle1
            // 
            this.egoldsFormStyle1.AllowUserResize = false;
            this.egoldsFormStyle1.BackColor = System.Drawing.Color.White;
            this.egoldsFormStyle1.ContextMenuForm = null;
            this.egoldsFormStyle1.ControlBoxButtonsWidth = 60;
            this.egoldsFormStyle1.EnableControlBoxIconsLight = true;
            this.egoldsFormStyle1.EnableControlBoxMouseLight = false;
            this.egoldsFormStyle1.Form = this;
            this.egoldsFormStyle1.FormStyle = yt_DesignUI.Components.EgoldsFormStyle.fStyle.SimpleDark;
            this.egoldsFormStyle1.HeaderColor = System.Drawing.Color.Violet;
            this.egoldsFormStyle1.HeaderColorAdditional = System.Drawing.Color.RoyalBlue;
            this.egoldsFormStyle1.HeaderColorGradientEnable = true;
            this.egoldsFormStyle1.HeaderColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.egoldsFormStyle1.HeaderHeight = 48;
            this.egoldsFormStyle1.HeaderImage = null;
            this.egoldsFormStyle1.HeaderTextColor = System.Drawing.Color.White;
            this.egoldsFormStyle1.HeaderTextFont = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            // 
            // DataGridViewDataBaseUsersMVD
            // 
            this.DataGridViewDataBaseUsersMVD.AllowUserToAddRows = false;
            this.DataGridViewDataBaseUsersMVD.AllowUserToDeleteRows = false;
            this.DataGridViewDataBaseUsersMVD.AllowUserToResizeColumns = false;
            this.DataGridViewDataBaseUsersMVD.AllowUserToResizeRows = false;
            this.DataGridViewDataBaseUsersMVD.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridViewDataBaseUsersMVD.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.DataGridViewDataBaseUsersMVD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridViewDataBaseUsersMVD.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DataGridViewDataBaseUsersMVD.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.DataGridViewDataBaseUsersMVD.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewDataBaseUsersMVD.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridViewDataBaseUsersMVD.ColumnHeadersHeight = 30;
            this.DataGridViewDataBaseUsersMVD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DataGridViewDataBaseUsersMVD.ContextMenuStrip = this.ContextMenuStripDataBaseUsers;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.MenuBar;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewDataBaseUsersMVD.DefaultCellStyle = dataGridViewCellStyle4;
            this.DataGridViewDataBaseUsersMVD.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.DataGridViewDataBaseUsersMVD.EnableHeadersVisualStyles = false;
            this.DataGridViewDataBaseUsersMVD.Location = new System.Drawing.Point(12, 87);
            this.DataGridViewDataBaseUsersMVD.MultiSelect = false;
            this.DataGridViewDataBaseUsersMVD.Name = "DataGridViewDataBaseUsersMVD";
            this.DataGridViewDataBaseUsersMVD.ReadOnly = true;
            this.DataGridViewDataBaseUsersMVD.RowHeadersVisible = false;
            this.DataGridViewDataBaseUsersMVD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DataGridViewDataBaseUsersMVD.Size = new System.Drawing.Size(481, 190);
            this.DataGridViewDataBaseUsersMVD.TabIndex = 0;
            this.DataGridViewDataBaseUsersMVD.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridViewDataBaseUsersMVD_CellMouseClick);
            this.DataGridViewDataBaseUsersMVD.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridViewDataBaseUsersMVD_CellMouseDoubleClick);
            this.DataGridViewDataBaseUsersMVD.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DataGridViewDataBaseUsersMVD_MouseClick);
            // 
            // ContextMenuStripDataBaseUsers
            // 
            this.ContextMenuStripDataBaseUsers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemEditPermission,
            this.ToolStripMenuItemDeleteAccount});
            this.ContextMenuStripDataBaseUsers.Name = "contextMenuStripDataBase";
            this.ContextMenuStripDataBaseUsers.Size = new System.Drawing.Size(223, 48);
            // 
            // ToolStripMenuItemEditPermission
            // 
            this.ToolStripMenuItemEditPermission.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UserToolStripMenuItem,
            this.AdminToolStripMenuItem});
            this.ToolStripMenuItemEditPermission.Name = "ToolStripMenuItemEditPermission";
            this.ToolStripMenuItemEditPermission.Size = new System.Drawing.Size(222, 22);
            this.ToolStripMenuItemEditPermission.Text = "Изменить уровень доступа";
            this.ToolStripMenuItemEditPermission.DropDownOpening += new System.EventHandler(this.ToolStripMenuItemEditPermission_DropDownOpening);
            // 
            // UserToolStripMenuItem
            // 
            this.UserToolStripMenuItem.Name = "UserToolStripMenuItem";
            this.UserToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.UserToolStripMenuItem.Text = "user";
            this.UserToolStripMenuItem.Click += new System.EventHandler(this.UserToolStripMenuItem_Click);
            // 
            // AdminToolStripMenuItem
            // 
            this.AdminToolStripMenuItem.Name = "AdminToolStripMenuItem";
            this.AdminToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.AdminToolStripMenuItem.Text = "admin";
            this.AdminToolStripMenuItem.Click += new System.EventHandler(this.AdminToolStripMenuItem_Click);
            // 
            // ToolStripMenuItemDeleteAccount
            // 
            this.ToolStripMenuItemDeleteAccount.Name = "ToolStripMenuItemDeleteAccount";
            this.ToolStripMenuItemDeleteAccount.Size = new System.Drawing.Size(222, 22);
            this.ToolStripMenuItemDeleteAccount.Text = "Удалить пользователя";
            this.ToolStripMenuItemDeleteAccount.Click += new System.EventHandler(this.ToolStripMenuItemDeleteAccount_Click);
            // 
            // panelDataBase
            // 
            this.panelDataBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.panelDataBase.Controls.Add(this.panelAddUser);
            this.panelDataBase.Controls.Add(this.labelTextDB);
            this.panelDataBase.Controls.Add(this.DataGridViewDataBaseUsersMVD);
            this.panelDataBase.Controls.Add(this.labelTextBoxFilter);
            this.panelDataBase.Controls.Add(this.labelComBoxFilter);
            this.panelDataBase.Controls.Add(this.comboBoxFilter);
            this.panelDataBase.Controls.Add(this.textBoxFilter);
            this.panelDataBase.Controls.Add(this.btnMenuAddUser);
            this.panelDataBase.Location = new System.Drawing.Point(0, 1);
            this.panelDataBase.Name = "panelDataBase";
            this.panelDataBase.Size = new System.Drawing.Size(520, 448);
            this.panelDataBase.TabIndex = 1;
            this.panelDataBase.MouseEnter += new System.EventHandler(this.panelDataBase_MouseEnter);
            // 
            // panelAddUser
            // 
            this.panelAddUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelAddUser.Controls.Add(this.groupBoxAddUser);
            this.panelAddUser.Controls.Add(this.groupBoxDefaultPass);
            this.panelAddUser.Controls.Add(this.labelHWIDUsers);
            this.panelAddUser.Controls.Add(this.DataGridViewHWIDUsers);
            this.panelAddUser.Location = new System.Drawing.Point(0, 56);
            this.panelAddUser.Name = "panelAddUser";
            this.panelAddUser.Size = new System.Drawing.Size(500, 393);
            this.panelAddUser.TabIndex = 2;
            this.panelAddUser.Visible = false;
            // 
            // groupBoxAddUser
            // 
            this.groupBoxAddUser.Controls.Add(this.buttonAddUser);
            this.groupBoxAddUser.Controls.Add(this.textBoxLoginAdd);
            this.groupBoxAddUser.Controls.Add(this.comboBoxPermissionAdd);
            this.groupBoxAddUser.Controls.Add(this.labelLoginAdd);
            this.groupBoxAddUser.Controls.Add(this.labelPermissionAdd);
            this.groupBoxAddUser.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxAddUser.Location = new System.Drawing.Point(24, 31);
            this.groupBoxAddUser.Name = "groupBoxAddUser";
            this.groupBoxAddUser.Size = new System.Drawing.Size(212, 153);
            this.groupBoxAddUser.TabIndex = 9;
            this.groupBoxAddUser.TabStop = false;
            this.groupBoxAddUser.Text = "Добавление пользователя";
            // 
            // buttonAddUser
            // 
            this.buttonAddUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.buttonAddUser.FlatAppearance.BorderSize = 0;
            this.buttonAddUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddUser.Location = new System.Drawing.Point(65, 115);
            this.buttonAddUser.Name = "buttonAddUser";
            this.buttonAddUser.Size = new System.Drawing.Size(75, 23);
            this.buttonAddUser.TabIndex = 4;
            this.buttonAddUser.Text = "Добавить";
            this.buttonAddUser.UseVisualStyleBackColor = false;
            this.buttonAddUser.Click += new System.EventHandler(this.buttonAddUser_Click);
            // 
            // textBoxLoginAdd
            // 
            this.textBoxLoginAdd.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxLoginAdd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLoginAdd.Location = new System.Drawing.Point(81, 21);
            this.textBoxLoginAdd.Multiline = true;
            this.textBoxLoginAdd.Name = "textBoxLoginAdd";
            this.textBoxLoginAdd.Size = new System.Drawing.Size(121, 20);
            this.textBoxLoginAdd.TabIndex = 0;
            // 
            // comboBoxPermissionAdd
            // 
            this.comboBoxPermissionAdd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPermissionAdd.FormattingEnabled = true;
            this.comboBoxPermissionAdd.Items.AddRange(new object[] {
            "user",
            "admin"});
            this.comboBoxPermissionAdd.Location = new System.Drawing.Point(81, 68);
            this.comboBoxPermissionAdd.Name = "comboBoxPermissionAdd";
            this.comboBoxPermissionAdd.Size = new System.Drawing.Size(121, 23);
            this.comboBoxPermissionAdd.TabIndex = 1;
            // 
            // labelLoginAdd
            // 
            this.labelLoginAdd.AutoSize = true;
            this.labelLoginAdd.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLoginAdd.Location = new System.Drawing.Point(6, 20);
            this.labelLoginAdd.Name = "labelLoginAdd";
            this.labelLoginAdd.Size = new System.Drawing.Size(52, 19);
            this.labelLoginAdd.TabIndex = 2;
            this.labelLoginAdd.Text = "Логин";
            // 
            // labelPermissionAdd
            // 
            this.labelPermissionAdd.AutoSize = true;
            this.labelPermissionAdd.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.labelPermissionAdd.Location = new System.Drawing.Point(5, 67);
            this.labelPermissionAdd.Name = "labelPermissionAdd";
            this.labelPermissionAdd.Size = new System.Drawing.Size(58, 19);
            this.labelPermissionAdd.TabIndex = 3;
            this.labelPermissionAdd.Text = "Доступ";
            // 
            // groupBoxDefaultPass
            // 
            this.groupBoxDefaultPass.Controls.Add(this.textBoxDefaultPass);
            this.groupBoxDefaultPass.Controls.Add(this.checkBoxDefaultPass);
            this.groupBoxDefaultPass.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxDefaultPass.Location = new System.Drawing.Point(24, 227);
            this.groupBoxDefaultPass.Name = "groupBoxDefaultPass";
            this.groupBoxDefaultPass.Size = new System.Drawing.Size(165, 108);
            this.groupBoxDefaultPass.TabIndex = 8;
            this.groupBoxDefaultPass.TabStop = false;
            this.groupBoxDefaultPass.Text = "Пароль по умолчанию";
            // 
            // textBoxDefaultPass
            // 
            this.textBoxDefaultPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxDefaultPass.Location = new System.Drawing.Point(8, 28);
            this.textBoxDefaultPass.Multiline = true;
            this.textBoxDefaultPass.Name = "textBoxDefaultPass";
            this.textBoxDefaultPass.Size = new System.Drawing.Size(121, 20);
            this.textBoxDefaultPass.TabIndex = 1;
            // 
            // checkBoxDefaultPass
            // 
            this.checkBoxDefaultPass.AutoSize = true;
            this.checkBoxDefaultPass.Location = new System.Drawing.Point(8, 67);
            this.checkBoxDefaultPass.Name = "checkBoxDefaultPass";
            this.checkBoxDefaultPass.Size = new System.Drawing.Size(94, 19);
            this.checkBoxDefaultPass.TabIndex = 0;
            this.checkBoxDefaultPass.Text = "Пробел (\" \")";
            this.checkBoxDefaultPass.UseVisualStyleBackColor = true;
            this.checkBoxDefaultPass.CheckedChanged += new System.EventHandler(this.checkBoxDefaultPass_CheckedChanged);
            // 
            // labelHWIDUsers
            // 
            this.labelHWIDUsers.AutoSize = true;
            this.labelHWIDUsers.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelHWIDUsers.Location = new System.Drawing.Point(283, 15);
            this.labelHWIDUsers.Name = "labelHWIDUsers";
            this.labelHWIDUsers.Size = new System.Drawing.Size(182, 15);
            this.labelHWIDUsers.TabIndex = 7;
            this.labelHWIDUsers.Text = "Запросы на получение доступа";
            // 
            // DataGridViewHWIDUsers
            // 
            this.DataGridViewHWIDUsers.AllowUserToAddRows = false;
            this.DataGridViewHWIDUsers.AllowUserToDeleteRows = false;
            this.DataGridViewHWIDUsers.AllowUserToResizeColumns = false;
            this.DataGridViewHWIDUsers.AllowUserToResizeRows = false;
            this.DataGridViewHWIDUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridViewHWIDUsers.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.DataGridViewHWIDUsers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridViewHWIDUsers.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.DataGridViewHWIDUsers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewHWIDUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridViewHWIDUsers.ColumnHeadersHeight = 30;
            this.DataGridViewHWIDUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DataGridViewHWIDUsers.ContextMenuStrip = this.ContextMenuStripHWIDUsers;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.MenuBar;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewHWIDUsers.DefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridViewHWIDUsers.EnableHeadersVisualStyles = false;
            this.DataGridViewHWIDUsers.Location = new System.Drawing.Point(250, 31);
            this.DataGridViewHWIDUsers.Name = "DataGridViewHWIDUsers";
            this.DataGridViewHWIDUsers.ReadOnly = true;
            this.DataGridViewHWIDUsers.RowHeadersVisible = false;
            this.DataGridViewHWIDUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DataGridViewHWIDUsers.Size = new System.Drawing.Size(232, 190);
            this.DataGridViewHWIDUsers.TabIndex = 5;
            this.DataGridViewHWIDUsers.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridViewHWIDUsers_CellMouseDoubleClick);
            // 
            // ContextMenuStripHWIDUsers
            // 
            this.ContextMenuStripHWIDUsers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemAcceptAccount,
            this.ToolStripMenuItemRejectAccount});
            this.ContextMenuStripHWIDUsers.Name = "ContextMenuStripUUIDUsers";
            this.ContextMenuStripHWIDUsers.Size = new System.Drawing.Size(174, 48);
            // 
            // ToolStripMenuItemAcceptAccount
            // 
            this.ToolStripMenuItemAcceptAccount.Name = "ToolStripMenuItemAcceptAccount";
            this.ToolStripMenuItemAcceptAccount.Size = new System.Drawing.Size(173, 22);
            this.ToolStripMenuItemAcceptAccount.Text = "Выдать доступ";
            this.ToolStripMenuItemAcceptAccount.Click += new System.EventHandler(this.ToolStripMenuItemAcceptAccount_Click);
            // 
            // ToolStripMenuItemRejectAccount
            // 
            this.ToolStripMenuItemRejectAccount.Name = "ToolStripMenuItemRejectAccount";
            this.ToolStripMenuItemRejectAccount.Size = new System.Drawing.Size(173, 22);
            this.ToolStripMenuItemRejectAccount.Text = "Отклонить доступ";
            this.ToolStripMenuItemRejectAccount.Click += new System.EventHandler(this.ToolStripMenuItemRejectAccount_Click);
            // 
            // labelTextDB
            // 
            this.labelTextDB.AutoSize = true;
            this.labelTextDB.Location = new System.Drawing.Point(178, 61);
            this.labelTextDB.Name = "labelTextDB";
            this.labelTextDB.Size = new System.Drawing.Size(152, 13);
            this.labelTextDB.TabIndex = 1;
            this.labelTextDB.Text = "База данных пользователей";
            // 
            // labelTextBoxFilter
            // 
            this.labelTextBoxFilter.AutoSize = true;
            this.labelTextBoxFilter.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.labelTextBoxFilter.Location = new System.Drawing.Point(24, 352);
            this.labelTextBoxFilter.Name = "labelTextBoxFilter";
            this.labelTextBoxFilter.Size = new System.Drawing.Size(65, 17);
            this.labelTextBoxFilter.TabIndex = 6;
            this.labelTextBoxFilter.Text = "Значение";
            // 
            // labelComBoxFilter
            // 
            this.labelComBoxFilter.AutoSize = true;
            this.labelComBoxFilter.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelComBoxFilter.Location = new System.Drawing.Point(24, 312);
            this.labelComBoxFilter.Name = "labelComBoxFilter";
            this.labelComBoxFilter.Size = new System.Drawing.Size(117, 17);
            this.labelComBoxFilter.TabIndex = 5;
            this.labelComBoxFilter.Text = "Параметр поиска";
            // 
            // comboBoxFilter
            // 
            this.comboBoxFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilter.FormattingEnabled = true;
            this.comboBoxFilter.Location = new System.Drawing.Point(153, 311);
            this.comboBoxFilter.Name = "comboBoxFilter";
            this.comboBoxFilter.Size = new System.Drawing.Size(121, 21);
            this.comboBoxFilter.TabIndex = 4;
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBoxFilter.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxFilter.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxFilter.Location = new System.Drawing.Point(153, 351);
            this.textBoxFilter.Multiline = true;
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(121, 20);
            this.textBoxFilter.TabIndex = 3;
            this.textBoxFilter.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // btnMenuAddUser
            // 
            this.btnMenuAddUser.BackColor = System.Drawing.Color.Gray;
            this.btnMenuAddUser.FlatAppearance.BorderSize = 0;
            this.btnMenuAddUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuAddUser.Location = new System.Drawing.Point(324, 311);
            this.btnMenuAddUser.Name = "btnMenuAddUser";
            this.btnMenuAddUser.Size = new System.Drawing.Size(144, 23);
            this.btnMenuAddUser.TabIndex = 7;
            this.btnMenuAddUser.Text = "Добавить пользователя";
            this.btnMenuAddUser.UseVisualStyleBackColor = false;
            this.btnMenuAddUser.Click += new System.EventHandler(this.btnMenuAddUser_Click);
            this.btnMenuAddUser.MouseEnter += new System.EventHandler(this.btnMenuAddUser_MouseEnter);
            // 
            // bfBtnMainFormOpen
            // 
            this.bfBtnMainFormOpen.AllowAnimations = true;
            this.bfBtnMainFormOpen.AllowMouseEffects = true;
            this.bfBtnMainFormOpen.AllowToggling = false;
            this.bfBtnMainFormOpen.AnimationSpeed = 200;
            this.bfBtnMainFormOpen.AutoGenerateColors = false;
            this.bfBtnMainFormOpen.AutoRoundBorders = false;
            this.bfBtnMainFormOpen.AutoSizeLeftIcon = true;
            this.bfBtnMainFormOpen.AutoSizeRightIcon = true;
            this.bfBtnMainFormOpen.BackColor = System.Drawing.Color.Transparent;
            this.bfBtnMainFormOpen.BackColor1 = System.Drawing.Color.DodgerBlue;
            this.bfBtnMainFormOpen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bfBtnMainFormOpen.BackgroundImage")));
            this.bfBtnMainFormOpen.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.bfBtnMainFormOpen.ButtonText = "Открыть Базу";
            this.bfBtnMainFormOpen.ButtonTextMarginLeft = 0;
            this.bfBtnMainFormOpen.ColorContrastOnClick = 45;
            this.bfBtnMainFormOpen.ColorContrastOnHover = 45;
            this.bfBtnMainFormOpen.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.bfBtnMainFormOpen.CustomizableEdges = borderEdges1;
            this.bfBtnMainFormOpen.DialogResult = System.Windows.Forms.DialogResult.None;
            this.bfBtnMainFormOpen.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.bfBtnMainFormOpen.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.bfBtnMainFormOpen.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.bfBtnMainFormOpen.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.bfBtnMainFormOpen.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bfBtnMainFormOpen.ForeColor = System.Drawing.Color.White;
            this.bfBtnMainFormOpen.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bfBtnMainFormOpen.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.bfBtnMainFormOpen.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.bfBtnMainFormOpen.IconMarginLeft = 11;
            this.bfBtnMainFormOpen.IconPadding = 10;
            this.bfBtnMainFormOpen.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bfBtnMainFormOpen.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.bfBtnMainFormOpen.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.bfBtnMainFormOpen.IconSize = 25;
            this.bfBtnMainFormOpen.IdleBorderColor = System.Drawing.Color.DodgerBlue;
            this.bfBtnMainFormOpen.IdleBorderRadius = 35;
            this.bfBtnMainFormOpen.IdleBorderThickness = 1;
            this.bfBtnMainFormOpen.IdleFillColor = System.Drawing.Color.DodgerBlue;
            this.bfBtnMainFormOpen.IdleIconLeftImage = null;
            this.bfBtnMainFormOpen.IdleIconRightImage = null;
            this.bfBtnMainFormOpen.IndicateFocus = false;
            this.bfBtnMainFormOpen.Location = new System.Drawing.Point(616, 378);
            this.bfBtnMainFormOpen.Name = "bfBtnMainFormOpen";
            this.bfBtnMainFormOpen.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.bfBtnMainFormOpen.OnDisabledState.BorderRadius = 35;
            this.bfBtnMainFormOpen.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.bfBtnMainFormOpen.OnDisabledState.BorderThickness = 1;
            this.bfBtnMainFormOpen.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.bfBtnMainFormOpen.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.bfBtnMainFormOpen.OnDisabledState.IconLeftImage = null;
            this.bfBtnMainFormOpen.OnDisabledState.IconRightImage = null;
            this.bfBtnMainFormOpen.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.bfBtnMainFormOpen.onHoverState.BorderRadius = 35;
            this.bfBtnMainFormOpen.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.bfBtnMainFormOpen.onHoverState.BorderThickness = 1;
            this.bfBtnMainFormOpen.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.bfBtnMainFormOpen.onHoverState.ForeColor = System.Drawing.Color.White;
            this.bfBtnMainFormOpen.onHoverState.IconLeftImage = null;
            this.bfBtnMainFormOpen.onHoverState.IconRightImage = null;
            this.bfBtnMainFormOpen.OnIdleState.BorderColor = System.Drawing.Color.DodgerBlue;
            this.bfBtnMainFormOpen.OnIdleState.BorderRadius = 35;
            this.bfBtnMainFormOpen.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.bfBtnMainFormOpen.OnIdleState.BorderThickness = 1;
            this.bfBtnMainFormOpen.OnIdleState.FillColor = System.Drawing.Color.DodgerBlue;
            this.bfBtnMainFormOpen.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.bfBtnMainFormOpen.OnIdleState.IconLeftImage = null;
            this.bfBtnMainFormOpen.OnIdleState.IconRightImage = null;
            this.bfBtnMainFormOpen.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.bfBtnMainFormOpen.OnPressedState.BorderRadius = 35;
            this.bfBtnMainFormOpen.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.bfBtnMainFormOpen.OnPressedState.BorderThickness = 1;
            this.bfBtnMainFormOpen.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.bfBtnMainFormOpen.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.bfBtnMainFormOpen.OnPressedState.IconLeftImage = null;
            this.bfBtnMainFormOpen.OnPressedState.IconRightImage = null;
            this.bfBtnMainFormOpen.Size = new System.Drawing.Size(150, 39);
            this.bfBtnMainFormOpen.TabIndex = 2;
            this.bfBtnMainFormOpen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bfBtnMainFormOpen.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.bfBtnMainFormOpen.TextMarginLeft = 0;
            this.bfBtnMainFormOpen.TextPadding = new System.Windows.Forms.Padding(0);
            this.bfBtnMainFormOpen.UseDefaultRadiusAndThickness = true;
            this.bfBtnMainFormOpen.Click += new System.EventHandler(this.bfBtnMainFormOpen_Click);
            // 
            // MainFormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::DataBaseMVD.Properties.Resources.MainFormImage;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bfBtnMainFormOpen);
            this.Controls.Add(this.PictureBoxMenu);
            this.Controls.Add(this.panelDataBase);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainFormAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainFormAdmin";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormAdmin_FormClosing);
            this.Load += new System.EventHandler(this.MainFormAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewDataBaseUsersMVD)).EndInit();
            this.ContextMenuStripDataBaseUsers.ResumeLayout(false);
            this.panelDataBase.ResumeLayout(false);
            this.panelDataBase.PerformLayout();
            this.panelAddUser.ResumeLayout(false);
            this.panelAddUser.PerformLayout();
            this.groupBoxAddUser.ResumeLayout(false);
            this.groupBoxAddUser.PerformLayout();
            this.groupBoxDefaultPass.ResumeLayout(false);
            this.groupBoxDefaultPass.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewHWIDUsers)).EndInit();
            this.ContextMenuStripHWIDUsers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBoxMenu;
        private yt_DesignUI.Components.EgoldsFormStyle egoldsFormStyle1;
        private System.Windows.Forms.DataGridView DataGridViewDataBaseUsersMVD;
        private System.Windows.Forms.Panel panelDataBase;
        private System.Windows.Forms.Label labelTextDB;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStripDataBaseUsers;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemEditPermission;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDeleteAccount;
        private System.Windows.Forms.ToolStripMenuItem UserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AdminToolStripMenuItem;
        private System.Windows.Forms.Panel panelAddUser;
        private System.Windows.Forms.Label labelPermissionAdd;
        private System.Windows.Forms.Label labelLoginAdd;
        private System.Windows.Forms.ComboBox comboBoxPermissionAdd;
        private System.Windows.Forms.TextBox textBoxLoginAdd;
        private System.Windows.Forms.Button buttonAddUser;
        private System.Windows.Forms.DataGridView DataGridViewHWIDUsers;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStripHWIDUsers;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAcceptAccount;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemRejectAccount;
        private System.Windows.Forms.GroupBox groupBoxDefaultPass;
        private System.Windows.Forms.TextBox textBoxDefaultPass;
        private System.Windows.Forms.CheckBox checkBoxDefaultPass;
        private System.Windows.Forms.Label labelHWIDUsers;
        private System.Windows.Forms.GroupBox groupBoxAddUser;
        private System.Windows.Forms.ComboBox comboBoxFilter;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.Label labelTextBoxFilter;
        private System.Windows.Forms.Label labelComBoxFilter;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton bfBtnMainFormOpen;
        private System.Windows.Forms.Button btnMenuAddUser;
    }
}
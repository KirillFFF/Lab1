
namespace DataBaseMVD
{
    partial class AuthorisationForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Bunifu.UI.WinForms.BunifuAnimatorNS.Animation animation1 = new Bunifu.UI.WinForms.BunifuAnimatorNS.Animation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthorisationForm));
            Bunifu.UI.WinForms.BunifuAnimatorNS.Animation animation2 = new Bunifu.UI.WinForms.BunifuAnimatorNS.Animation();
            this.authPanel = new System.Windows.Forms.Panel();
            this.passTextCaps = new System.Windows.Forms.Label();
            this.passwordField = new yt_DesignUI.EgoldsGoogleTextBox();
            this.loginField = new yt_DesignUI.EgoldsGoogleTextBox();
            this.buttonAuth = new yt_DesignUI.yt_Button();
            this.onlineAdminsList = new System.Windows.Forms.Label();
            this.egoldsFormStyle1 = new yt_DesignUI.Components.EgoldsFormStyle(this.components);
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.PanelAnimator = new Bunifu.UI.WinForms.BunifuTransition(this.components);
            this.BarsAnimator = new Bunifu.UI.WinForms.BunifuTransition(this.components);
            this.authPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // authPanel
            // 
            this.authPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.authPanel.Controls.Add(this.passTextCaps);
            this.authPanel.Controls.Add(this.passwordField);
            this.authPanel.Controls.Add(this.loginField);
            this.authPanel.Controls.Add(this.buttonAuth);
            this.authPanel.Controls.Add(this.onlineAdminsList);
            this.BarsAnimator.SetDecoration(this.authPanel, Bunifu.UI.WinForms.BunifuTransition.DecorationType.None);
            this.PanelAnimator.SetDecoration(this.authPanel, Bunifu.UI.WinForms.BunifuTransition.DecorationType.None);
            this.authPanel.Location = new System.Drawing.Point(500, 1);
            this.authPanel.Name = "authPanel";
            this.authPanel.Size = new System.Drawing.Size(150, 417);
            this.authPanel.TabIndex = 3;
            this.authPanel.Visible = false;
            // 
            // passTextCaps
            // 
            this.passTextCaps.AutoSize = true;
            this.PanelAnimator.SetDecoration(this.passTextCaps, Bunifu.UI.WinForms.BunifuTransition.DecorationType.None);
            this.BarsAnimator.SetDecoration(this.passTextCaps, Bunifu.UI.WinForms.BunifuTransition.DecorationType.None);
            this.passTextCaps.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.passTextCaps.Location = new System.Drawing.Point(29, 188);
            this.passTextCaps.Name = "passTextCaps";
            this.passTextCaps.Size = new System.Drawing.Size(0, 13);
            this.passTextCaps.TabIndex = 11;
            // 
            // passwordField
            // 
            this.passwordField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.passwordField.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.passwordField.BorderColorNotActive = System.Drawing.Color.RoyalBlue;
            this.passwordField.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.BarsAnimator.SetDecoration(this.passwordField, Bunifu.UI.WinForms.BunifuTransition.DecorationType.None);
            this.PanelAnimator.SetDecoration(this.passwordField, Bunifu.UI.WinForms.BunifuTransition.DecorationType.None);
            this.passwordField.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.passwordField.FontTextPreview = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.passwordField.ForeColor = System.Drawing.Color.White;
            this.passwordField.Location = new System.Drawing.Point(10, 132);
            this.passwordField.Name = "passwordField";
            this.passwordField.Size = new System.Drawing.Size(128, 40);
            this.passwordField.TabIndex = 10;
            this.passwordField.TextInput = "";
            this.passwordField.TextPreview = "Пароль";
            this.passwordField.UseSystemPasswordChar = true;
            // 
            // loginField
            // 
            this.loginField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.loginField.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.loginField.BorderColorNotActive = System.Drawing.Color.RoyalBlue;
            this.loginField.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.BarsAnimator.SetDecoration(this.loginField, Bunifu.UI.WinForms.BunifuTransition.DecorationType.None);
            this.PanelAnimator.SetDecoration(this.loginField, Bunifu.UI.WinForms.BunifuTransition.DecorationType.None);
            this.loginField.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loginField.FontTextPreview = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loginField.ForeColor = System.Drawing.Color.White;
            this.loginField.Location = new System.Drawing.Point(10, 75);
            this.loginField.Name = "loginField";
            this.loginField.Size = new System.Drawing.Size(128, 40);
            this.loginField.TabIndex = 9;
            this.loginField.TextInput = "";
            this.loginField.TextPreview = "Логин";
            this.loginField.UseSystemPasswordChar = false;
            // 
            // buttonAuth
            // 
            this.buttonAuth.BackColor = System.Drawing.Color.RoyalBlue;
            this.buttonAuth.BackColorAdditional = System.Drawing.Color.Gray;
            this.buttonAuth.BackColorGradientEnabled = false;
            this.buttonAuth.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.buttonAuth.BorderColor = System.Drawing.Color.Tomato;
            this.buttonAuth.BorderColorEnabled = false;
            this.buttonAuth.BorderColorOnHover = System.Drawing.Color.Tomato;
            this.buttonAuth.BorderColorOnHoverEnabled = false;
            this.buttonAuth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PanelAnimator.SetDecoration(this.buttonAuth, Bunifu.UI.WinForms.BunifuTransition.DecorationType.None);
            this.BarsAnimator.SetDecoration(this.buttonAuth, Bunifu.UI.WinForms.BunifuTransition.DecorationType.None);
            this.buttonAuth.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAuth.ForeColor = System.Drawing.Color.White;
            this.buttonAuth.Location = new System.Drawing.Point(34, 227);
            this.buttonAuth.Name = "buttonAuth";
            this.buttonAuth.RippleColor = System.Drawing.Color.Black;
            this.buttonAuth.RoundingEnable = false;
            this.buttonAuth.Size = new System.Drawing.Size(82, 33);
            this.buttonAuth.TabIndex = 5;
            this.buttonAuth.Text = "Войти";
            this.buttonAuth.TextHover = null;
            this.buttonAuth.UseDownPressEffectOnClick = false;
            this.buttonAuth.UseRippleEffect = true;
            this.buttonAuth.UseZoomEffectOnHover = false;
            this.buttonAuth.Click += new System.EventHandler(this.buttonAuth_Click);
            // 
            // onlineAdminsList
            // 
            this.onlineAdminsList.AutoSize = true;
            this.PanelAnimator.SetDecoration(this.onlineAdminsList, Bunifu.UI.WinForms.BunifuTransition.DecorationType.None);
            this.BarsAnimator.SetDecoration(this.onlineAdminsList, Bunifu.UI.WinForms.BunifuTransition.DecorationType.None);
            this.onlineAdminsList.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.onlineAdminsList.Location = new System.Drawing.Point(4, 382);
            this.onlineAdminsList.Name = "onlineAdminsList";
            this.onlineAdminsList.Size = new System.Drawing.Size(125, 13);
            this.onlineAdminsList.TabIndex = 3;
            this.onlineAdminsList.Text = "Администрация в сети:";
            // 
            // egoldsFormStyle1
            // 
            this.egoldsFormStyle1.AllowUserResize = false;
            this.egoldsFormStyle1.BackColor = System.Drawing.Color.White;
            this.egoldsFormStyle1.ContextMenuForm = null;
            this.egoldsFormStyle1.ControlBoxButtonsWidth = 60;
            this.egoldsFormStyle1.EnableControlBoxIconsLight = false;
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
            // iconPictureBox1
            // 
            this.iconPictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.iconPictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BarsAnimator.SetDecoration(this.iconPictureBox1, Bunifu.UI.WinForms.BunifuTransition.DecorationType.None);
            this.PanelAnimator.SetDecoration(this.iconPictureBox1, Bunifu.UI.WinForms.BunifuTransition.DecorationType.None);
            this.iconPictureBox1.ForeColor = System.Drawing.Color.Gainsboro;
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.Bars;
            this.iconPictureBox1.IconColor = System.Drawing.Color.Gainsboro;
            this.iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox1.IconSize = 27;
            this.iconPictureBox1.Location = new System.Drawing.Point(604, 18);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(27, 27);
            this.iconPictureBox1.TabIndex = 12;
            this.iconPictureBox1.TabStop = false;
            this.iconPictureBox1.Click += new System.EventHandler(this.iconPictureBox1_Click);
            // 
            // PanelAnimator
            // 
            this.PanelAnimator.AnimationType = Bunifu.UI.WinForms.BunifuAnimatorNS.AnimationType.Leaf;
            this.PanelAnimator.Cursor = null;
            animation1.AnimateOnlyDifferences = true;
            animation1.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.BlindCoeff")));
            animation1.LeafCoeff = 1F;
            animation1.MaxTime = 1F;
            animation1.MinTime = 0F;
            animation1.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicCoeff")));
            animation1.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicShift")));
            animation1.MosaicSize = 0;
            animation1.Padding = new System.Windows.Forms.Padding(0);
            animation1.RotateCoeff = 0F;
            animation1.RotateLimit = 0F;
            animation1.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.ScaleCoeff")));
            animation1.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.SlideCoeff")));
            animation1.TimeCoeff = 0F;
            animation1.TransparencyCoeff = 0F;
            this.PanelAnimator.DefaultAnimation = animation1;
            // 
            // BarsAnimator
            // 
            this.BarsAnimator.AnimationType = Bunifu.UI.WinForms.BunifuAnimatorNS.AnimationType.Scale;
            this.BarsAnimator.Cursor = null;
            animation2.AnimateOnlyDifferences = true;
            animation2.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.BlindCoeff")));
            animation2.LeafCoeff = 0F;
            animation2.MaxTime = 1F;
            animation2.MinTime = 0F;
            animation2.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.MosaicCoeff")));
            animation2.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation2.MosaicShift")));
            animation2.MosaicSize = 0;
            animation2.Padding = new System.Windows.Forms.Padding(0);
            animation2.RotateCoeff = 0F;
            animation2.RotateLimit = 0F;
            animation2.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.ScaleCoeff")));
            animation2.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.SlideCoeff")));
            animation2.TimeCoeff = 0F;
            animation2.TransparencyCoeff = 0F;
            this.BarsAnimator.DefaultAnimation = animation2;
            // 
            // AuthorisationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::DataBaseMVD.Properties.Resources.AuthFormImage;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(647, 419);
            this.Controls.Add(this.iconPictureBox1);
            this.Controls.Add(this.authPanel);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.PanelAnimator.SetDecoration(this, Bunifu.UI.WinForms.BunifuTransition.DecorationType.None);
            this.BarsAnimator.SetDecoration(this, Bunifu.UI.WinForms.BunifuTransition.DecorationType.None);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(100, 50);
            this.Name = "AuthorisationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AuthorisationForm_FormClosing);
            this.Load += new System.EventHandler(this.Authorisation_Load);
            this.authPanel.ResumeLayout(false);
            this.authPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel authPanel;
        private System.Windows.Forms.Label onlineAdminsList;
        private yt_DesignUI.Components.EgoldsFormStyle egoldsFormStyle1;
        private yt_DesignUI.EgoldsGoogleTextBox loginField;
        private yt_DesignUI.EgoldsGoogleTextBox passwordField;
        private yt_DesignUI.yt_Button buttonAuth;
        private System.Windows.Forms.Label passTextCaps;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Bunifu.UI.WinForms.BunifuTransition PanelAnimator;
        private Bunifu.UI.WinForms.BunifuTransition BarsAnimator;
    }
}


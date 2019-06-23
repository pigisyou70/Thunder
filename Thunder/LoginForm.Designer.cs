namespace Thunder
{
    partial class LoginForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.LAccount = new System.Windows.Forms.Label();
            this.LPassword = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.AcceptButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.Homepic = new System.Windows.Forms.PictureBox();
            this.RegisterButton = new System.Windows.Forms.Button();
            this.ForgetButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ReadMeButton = new System.Windows.Forms.Button();
            this.GuestButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Homepic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // LAccount
            // 
            this.LAccount.AutoSize = true;
            this.LAccount.BackColor = System.Drawing.Color.Transparent;
            this.LAccount.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LAccount.ForeColor = System.Drawing.Color.White;
            this.LAccount.Location = new System.Drawing.Point(275, 485);
            this.LAccount.Name = "LAccount";
            this.LAccount.Size = new System.Drawing.Size(62, 31);
            this.LAccount.TabIndex = 0;
            this.LAccount.Text = "帳號";
            // 
            // LPassword
            // 
            this.LPassword.AutoSize = true;
            this.LPassword.BackColor = System.Drawing.Color.Transparent;
            this.LPassword.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LPassword.ForeColor = System.Drawing.Color.White;
            this.LPassword.Location = new System.Drawing.Point(275, 540);
            this.LPassword.Name = "LPassword";
            this.LPassword.Size = new System.Drawing.Size(62, 31);
            this.LPassword.TabIndex = 1;
            this.LPassword.Text = "密碼";
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(345, 480);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(100, 36);
            this.txtUserName.TabIndex = 2;
            this.txtUserName.Text = "Admin";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(345, 535);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(100, 36);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Text = "Admin";
            // 
            // AcceptButton
            // 
            this.AcceptButton.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.AcceptButton.Location = new System.Drawing.Point(455, 480);
            this.AcceptButton.Name = "AcceptButton";
            this.AcceptButton.Size = new System.Drawing.Size(75, 36);
            this.AcceptButton.TabIndex = 4;
            this.AcceptButton.Text = "登入";
            this.AcceptButton.UseVisualStyleBackColor = true;
            this.AcceptButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CancelButton.Location = new System.Drawing.Point(430, 590);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 36);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.Text = "取消";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Visible = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // Homepic
            // 
            this.Homepic.BackColor = System.Drawing.Color.Transparent;
            this.Homepic.Image = global::Thunder.Properties.Resources.LOGO;
            this.Homepic.Location = new System.Drawing.Point(293, 73);
            this.Homepic.Name = "Homepic";
            this.Homepic.Size = new System.Drawing.Size(378, 145);
            this.Homepic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Homepic.TabIndex = 6;
            this.Homepic.TabStop = false;
            // 
            // RegisterButton
            // 
            this.RegisterButton.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.RegisterButton.Location = new System.Drawing.Point(455, 535);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(75, 36);
            this.RegisterButton.TabIndex = 7;
            this.RegisterButton.Text = "註冊";
            this.RegisterButton.UseVisualStyleBackColor = true;
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // ForgetButton
            // 
            this.ForgetButton.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ForgetButton.Location = new System.Drawing.Point(536, 535);
            this.ForgetButton.Name = "ForgetButton";
            this.ForgetButton.Size = new System.Drawing.Size(123, 36);
            this.ForgetButton.TabIndex = 8;
            this.ForgetButton.Text = "忘記密碼";
            this.ForgetButton.UseVisualStyleBackColor = true;
            this.ForgetButton.Click += new System.EventHandler(this.ForgetButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::Thunder.Properties.Resources.player3;
            this.pictureBox1.Location = new System.Drawing.Point(335, 258);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(288, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // ReadMeButton
            // 
            this.ReadMeButton.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ReadMeButton.Location = new System.Drawing.Point(345, 590);
            this.ReadMeButton.Name = "ReadMeButton";
            this.ReadMeButton.Size = new System.Drawing.Size(73, 36);
            this.ReadMeButton.TabIndex = 10;
            this.ReadMeButton.Text = "說明";
            this.ReadMeButton.UseVisualStyleBackColor = true;
            this.ReadMeButton.Click += new System.EventHandler(this.ReadMeButton_Click);
            // 
            // GuestButton
            // 
            this.GuestButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.GuestButton.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GuestButton.Location = new System.Drawing.Point(535, 480);
            this.GuestButton.Name = "GuestButton";
            this.GuestButton.Size = new System.Drawing.Size(123, 36);
            this.GuestButton.TabIndex = 5;
            this.GuestButton.Text = "遊客登入";
            this.GuestButton.UseVisualStyleBackColor = true;
            this.GuestButton.Click += new System.EventHandler(this.GuestButton_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = global::Thunder.Properties.Resources.bg_3;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.ReadMeButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ForgetButton);
            this.Controls.Add(this.RegisterButton);
            this.Controls.Add(this.Homepic);
            this.Controls.Add(this.GuestButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.AcceptButton);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.LPassword);
            this.Controls.Add(this.LAccount);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1024, 768);
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "星際戰機";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Homepic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LAccount;
        private System.Windows.Forms.Label LPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private new System.Windows.Forms.Button AcceptButton;
        private new System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.PictureBox Homepic;
        private System.Windows.Forms.Button RegisterButton;
        private System.Windows.Forms.Button ForgetButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button ReadMeButton;
        private System.Windows.Forms.Button GuestButton;
    }
}


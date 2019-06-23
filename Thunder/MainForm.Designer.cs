namespace Thunder
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.BulletTimer = new System.Windows.Forms.Timer(this.components);
            this.EnemyTimer = new System.Windows.Forms.Timer(this.components);
            this.TimeNow = new System.Windows.Forms.Timer(this.components);
            this.BossBulletTimer = new System.Windows.Forms.Timer(this.components);
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.JumpButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GameTimer
            // 
            this.GameTimer.Enabled = true;
            this.GameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
            // 
            // BulletTimer
            // 
            this.BulletTimer.Tick += new System.EventHandler(this.BulletTimer_Tick);
            // 
            // EnemyTimer
            // 
            this.EnemyTimer.Interval = 500;
            this.EnemyTimer.Tick += new System.EventHandler(this.EnemyTimer_Tick);
            // 
            // TimeNow
            // 
            this.TimeNow.Enabled = true;
            this.TimeNow.Interval = 1000;
            this.TimeNow.Tick += new System.EventHandler(this.TimeNow_Tick);
            // 
            // BossBulletTimer
            // 
            this.BossBulletTimer.Enabled = true;
            this.BossBulletTimer.Interval = 450;
            this.BossBulletTimer.Tick += new System.EventHandler(this.BossBulletTimer_Tick);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "第一關",
            "第二關",
            "第三關",
            "王直接出現",
            "王直接死亡",
            "商店",
            "劇情1",
            "劇情2",
            "劇情3",
            "劇情4"});
            this.comboBox1.Location = new System.Drawing.Point(699, 283);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Visible = false;
            // 
            // JumpButton
            // 
            this.JumpButton.Enabled = false;
            this.JumpButton.Location = new System.Drawing.Point(847, 283);
            this.JumpButton.Name = "JumpButton";
            this.JumpButton.Size = new System.Drawing.Size(75, 23);
            this.JumpButton.TabIndex = 1;
            this.JumpButton.Text = "確認";
            this.JumpButton.UseVisualStyleBackColor = true;
            this.JumpButton.Visible = false;
            this.JumpButton.Click += new System.EventHandler(this.JumpButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.JumpButton);
            this.Controls.Add(this.comboBox1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1024, 768);
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "星際戰機";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.Timer BulletTimer;
        private System.Windows.Forms.Timer EnemyTimer;
        private System.Windows.Forms.Timer TimeNow;
        private System.Windows.Forms.Timer BossBulletTimer;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button JumpButton;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Windows.Forms;

namespace Thunder
{
    public partial class pauseForm : Form
    {
        public pauseForm()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();

            Image img1 = ImageManager.imgPauseLayout;
            pictureBox1.Image = img1;

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Dock = DockStyle.Fill;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.TransparencyKey = Color.Black;
            pictureBox1.BackColor = Color.Black;

            HomeButton.Parent = pictureBox1;
            ContiuneButton.Parent = pictureBox1;
            label1.Parent = pictureBox1;
            label2.Parent = pictureBox1;
            label3.Parent = pictureBox1;
            label4.Parent = pictureBox1;

            //InitializeComponent();
        }
        private void ContiuneButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void SpeakerButton_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;

            if (MainForm.VoiceEnable)
            {
                MainForm.VoiceEnable = false;
                SpeakerButton.Image = ImageManager.imgNoSpeaker;
                label4.Text = "已靜音";
            }
            else
            {
                MainForm.VoiceEnable = true;
                SpeakerButton.Image = ImageManager.imgSpeaker;
                label4.Text = "開啟聲音";
            }
        }
    }
}

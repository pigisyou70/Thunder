using System.Drawing;
using System.Windows.Forms;

namespace Thunder
{
    public partial class Victory_condition : Form
    {
        static public int slectImage = 99;

        public Victory_condition()
        {
            slectImage = 1;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();

            Image img = Image.FromFile("images/Condition"+slectImage+"_Layout.png");
            pictureBox1.Image = img;

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Dock = DockStyle.Fill;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.TransparencyKey = Color.Black;
            pictureBox1.BackColor = Color.Black;
        }

        private void Victory_condition_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X > 300 && e.X < 500 && e.Y > 400 & e.Y < 500)
                this.DialogResult = DialogResult.OK;

        }

        private void Victory_condition_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
                this.DialogResult = DialogResult.OK;
        }
    }
}

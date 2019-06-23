using System.Drawing;

namespace Thunder
{
    class Meteorite
    {
        int width = 79, height = 200;
        Point postion;
        bool blife;
        public int Width { get { return width; } set { width = value; } }
        public int Height { get { return height; } set { height = value; } }
        // 假如位置達到最下面, 回傳 True
        //public bool bLife { get { return blife; } set { blife = value; } }
        public Point Postion { get { return postion; } set { postion = value; } }
        Image imgMeteorite = ImageManager.imgMeteorite;


        public Meteorite()
        {
            blife = true;
        }

        /// <summary> Paint 隕石 </summary>
        public void Paint(Graphics g)
        {
            if (blife)
            {
                if (this.Postion.Y < MainForm.mScreenHeight + 200)
                    Move();
                g.DrawImage(imgMeteorite, postion.X, postion.Y, width, height);
            }
        }

        public void Move()
        {
            postion.Y += 18;
            if (postion.Y > 1000) blife = false;
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(this.Postion.X+15, this.Postion.Y+40, this.Width-15, this.Height-10);
        }
    }
}

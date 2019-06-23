using System.Drawing;

namespace Thunder
{
    class Victor_Condition
    {
        int imgWidth, imgHeight;
        bool visble = true;
        Image image = null;
        RectangleF destinationRect;
        RectangleF sourceRect;

        public bool Visble
        {
            get { return visble; }
            set { visble = value; }
        }
        /// <summary> int type(0-3)第幾關的勝利條件 ,int posx X座標, int posy Y座標 </summary>
        public Victor_Condition(int type, int posx, int posy)
        {
            switch (type)
            {
                case 1: image = Image.FromFile("images/Condition1_Layout.png"); break;
                case 2: image = Image.FromFile("images/Condition2_Layout.png"); break;
                case 3: image = Image.FromFile("images/Condition3_Layout.png"); break;
                default: image = Image.FromFile("images/Condition1_Layout.png"); break;
            }
            imgWidth = image.Width;
            imgHeight = image.Height;
            destinationRect = new RectangleF(posx, posy, .8f * imgWidth, .8f * imgHeight);
            sourceRect = new RectangleF(0, 0, 1f * imgWidth, 1f * imgHeight);
        }

        public void Draw(Graphics g)
        {
            if (visble)
                g.DrawImage(image, destinationRect, sourceRect, GraphicsUnit.Pixel);
        }

    }
}

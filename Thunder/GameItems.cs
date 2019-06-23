using System;
using System.Drawing;

namespace Thunder
{
    class GameItems
    {
        Random rad = new Random();
        public int X { get; set; }
        public int Y { get; set; }
        public int Type { get; set; }
        public int Speed { get; set; }
        public Direction Dir { get; set; }
        public Image ImgItem { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }


        public GameItems(int x, int y, int type, int speed, Direction dir)
        {
            X = x;
            Y = y;
            Speed = speed;
            //this.Type = rad.Next(1, 3);
            Type = type;
            ImgItem = GetType();
            Dir = dir;
            Width = ImgItem.Width;
            Height = ImgItem.Height;
        }
        public void Draw(Graphics g)
        {
            Move();
            if (Y < MainForm.mScreenHeight)
            {
                g.DrawImage(ImgItem, X, Y);
            }
        }
        public void Move()
        {
            switch (Dir)
            {
                case Direction.Down:
                    Y += Speed;
                    break;
            }
        }
        public Rectangle GetRectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }
        public new Image GetType()
        {
            switch (Type)
            {
                case 1:
                    ImgItem = ImageManager.imgPowerUp;
                    break;
                case 2:
                    ImgItem = ImageManager.imgShiled;
                    break;
                case 3:
                    //ImgItem = ImageManager.imgBoom;
                    break;

            }
            return ImgItem;
        }

    }
}

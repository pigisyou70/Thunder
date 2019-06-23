using System.Drawing;

namespace Thunder
{
    class StorePlane
    {

        Image BackGround = ImageManager.imgStoreBackGround;

        int itype;
        int cost = 50;
        public int Cost { get { return cost; } }
        public int iType { get { return itype; } set { itype = value; } }

        RectangleF destinationRect;
        RectangleF sourceRect;

        // 火力等級 & 火力按鍵----------------------------------------------------------------------------------------
        int ifirelevel = 0;
        public int iFireLevel { get { return ifirelevel; } set { ifirelevel = value; } }

        Point fireButton;
        public Point FireButton { get { return fireButton; } }
        // 速度等級 & 速度按鍵----------------------------------------------------------------------------------------
        int ispeedlevel = 0;
        public int iSpeedLevel { get { return ispeedlevel; } set { ispeedlevel = value; } }

        Point speedButton;
        public Point SpeedButton { get { return speedButton; } }

        // 射速等級 & 射速按鍵----------------------------------------------------------------------------------------
        int iratelevel = 0;
        public int iRateLevel { get { return iratelevel; } set { iratelevel = value; } }

        Point rateButton;
        public Point RateButton { get { return rateButton; } }

        // ----------------------------------------------------------------------------------------
        int change = 0;

        public StorePlane()
        {
            int width = BackGround.Width;
            int height = BackGround.Height;
            destinationRect = new RectangleF(0, 0, .99f * width, .95f * height);
            sourceRect = new RectangleF(0, 0, 1f * width, 1f * height);

        }
        public int CheckType(int PlayerType)
        {
            if (PlayerType == 0 || PlayerType == 1 || PlayerType == 2 || PlayerType == 3) { itype = 1; }// BLUE
            else if (PlayerType == 4 || PlayerType == 5 || PlayerType == 6 || PlayerType == 7) { itype = 2; }// Orange
            else if (PlayerType == 8 || PlayerType == 9 || PlayerType == 10 || PlayerType == 11) { itype = 3; }// Pink
            else if (PlayerType == 12 || PlayerType == 13 || PlayerType == 14 || PlayerType == 15) { itype = 4; }// Yellow
            change = itype;
            return itype;
        }
        public void DrawStore(Graphics g, Image imgUserPlane)
        {
            int fireY = 450, speedY = 520, rateY = 590;
            fireButton = new Point(720, fireY - 12);
            speedButton = new Point(720, speedY - 12);
            rateButton = new Point(720, rateY - 12);

            // 商店背景圖
            //g.DrawImage(BackGround, destinationRect, sourceRect, GraphicsUnit.Pixel);
            g.DrawImage(BackGround, 0, 0, 1024, 768);

            // 人物 CG 圖 , 人物是誰由 MainForm.LoadFile(讀取資料) => Player.PlanType => StorePlane.CheckType 取得
            switch (itype)
            {
                case 1: g.DrawImage(ImageManager.imgBlueCG, new Point(250, 70)); break;
                    //case 2: g.DrawImage(ImageManager.imgOrangeCG, new Point(250, 70)); break;
                    //case 3: g.DrawImage(ImageManager.imgPinkCG, new Point(250, 70)); break;
                    //case 4: g.DrawImage(ImageManager.imgYellowCG, new Point(250, 70)); break;
                    //default: g.DrawImage(ImageManager.imgBlueCG, new Point(250, 70)); break;
            }

            // 實體飛機圖片
            g.DrawImage(imgUserPlane, new Point(200, fireY + 20));

            // 火力強化
            g.FillRectangle(new SolidBrush(Color.FromArgb(128, 62, 89, 251)), new Rectangle(385, fireY - 4, 336, 50));
            g.DrawRectangle(new Pen(new SolidBrush(Color.Black)), new Rectangle(385, fireY - 4, 336, 50));
            g.DrawString("火力強化", new Font("Arian", 24), new SolidBrush(Color.White), new Point(380, fireY));
            g.DrawImage(ImageManager.imgButton, fireButton);
            if (ifirelevel == 0) { level1(fireY, g); }
            else if (ifirelevel == 1) { level2(fireY, g); }
            else if (ifirelevel == 2) { level3(fireY, g); }
            else { levelMax(fireY, g); }


            // 速度強化
            g.FillRectangle(new SolidBrush(Color.FromArgb(128, 62, 89, 251)), new Rectangle(385, speedY - 4, 336, 50));
            g.DrawRectangle(new Pen(new SolidBrush(Color.Black)), new Rectangle(385, speedY - 4, 336, 50));
            g.DrawString("速度強化", new Font("Arian", 24), new SolidBrush(Color.White), new Point(380, speedY));
            g.DrawImage(ImageManager.imgButton, speedButton);
            if (iSpeedLevel == 0) { level1(speedY, g); }
            else if (iSpeedLevel == 1) { level2(speedY, g); }
            else if (iSpeedLevel == 2) { level3(speedY, g); }
            else { levelMax(speedY, g); }


            // 射速強化 
            g.FillRectangle(new SolidBrush(Color.FromArgb(128, 62, 89, 251)), new Rectangle(385, rateY - 4, 336, 50));
            g.DrawRectangle(new Pen(new SolidBrush(Color.Black)), new Rectangle(385, rateY - 4, 336, 50));
            g.DrawString("射速強化", new Font("Arian", 24), new SolidBrush(Color.White), new Point(380, rateY));
            g.DrawImage(ImageManager.imgButton, rateButton);
            if (iRateLevel == 0) { level1(rateY, g); }
            else if (iRateLevel == 1) { level2(rateY, g); }
            else if (iRateLevel == 2) { level3(rateY, g); }
            else { levelMax(rateY, g); }
        }
        /// <summary> 透過外部給予的三個數值，更新商店要顯示的星數 </summary>
        public int Udate(int FireLevel, int SpeedLevel, int RateLevel)
        {
            ifirelevel = FireLevel;
            ispeedlevel = SpeedLevel;
            iratelevel = RateLevel;
            return 0;
        }
        /// <summary> 強化等級0 <para>0顆滿星，3顆空星</para> </summary>
        private void level1(int y, Graphics g)
        {
            g.DrawImage(ImageManager.DrawStar, new Point(530, y - 2));
            g.DrawImage(ImageManager.DrawStar, new Point(592, y - 2));
            g.DrawImage(ImageManager.DrawStar, new Point(654, y - 2));
            g.DrawImage(ImageManager.imgCoin, new Point(750, y - 6));
            cost = 100;
            g.DrawString(cost.ToString(), new Font("Arian", 24), new SolidBrush(Color.White), new Point(785, y - 6));
        }
        /// <summary> 強化等級1 <para>1顆滿星，2顆空星</para> </summary>
        private void level2(int y, Graphics g)
        {
            g.DrawImage(ImageManager.FillStar, new Point(530, y - 2));
            g.DrawImage(ImageManager.DrawStar, new Point(592, y - 2));
            g.DrawImage(ImageManager.DrawStar, new Point(654, y - 2));
            g.DrawImage(ImageManager.imgCoin, new Point(750, y - 6));
            cost = 300;
            g.DrawString(cost.ToString(), new Font("Arian", 24), new SolidBrush(Color.White), new Point(785, y - 6));
        }
        /// <summary> 強化等級2 <para>2顆滿星，1顆空星</para> </summary>
        private void level3(int y, Graphics g)
        {
            g.DrawImage(ImageManager.FillStar, new Point(530, y - 2));
            g.DrawImage(ImageManager.FillStar, new Point(592, y - 2));
            g.DrawImage(ImageManager.DrawStar, new Point(654, y - 2));
            g.DrawImage(ImageManager.imgCoin, new Point(750, y - 6));
            cost = 500;
            g.DrawString(cost.ToString(), new Font("Arian", 24), new SolidBrush(Color.White), new Point(785, y - 6));
        }
        /// <summary> 強化等級3 <para>3顆滿星，0顆空星</para> </summary>
        private void levelMax(int y, Graphics g)
        {
            g.DrawImage(ImageManager.FillStar, new Point(530, y - 2));
            g.DrawImage(ImageManager.FillStar, new Point(592, y - 2));
            g.DrawImage(ImageManager.FillStar, new Point(654, y - 2)); cost = 200;
            g.DrawString("Level Max", new Font("Arian", 20), new SolidBrush(Color.White), new Point(740, y - 6));
        }

    }
}

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Thunder
{
    public class Player
    {
        Image img = null;
        #region 橫縱坐標、寬度、高度、飛機種類、生命、火力等級、速度等級、射速等級、玩家名稱、玩家狀態
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int PlanType { get; set; }
        public int STEP { get; set; }
        public int Life { get; set; }
        public int Damage { get; set; }
        public int Guns { get; set; }
        public int FireLevel { get; set; }
        public int SpeedLevel { get; set; }
        public int RateLevel { get; set; }
        public String UserName { get; set; }
        public bool PLAN_DEATH_STATE { get; set; }
        #endregion

        public Player()
        {
        }
        public void Draw(Graphics g)
        {
            // 假如玩家狀態為死亡
            if (Life <= 0)
            {
                g.DrawImage(ImageManager.imgPlayer_Explode, this.X, this.Y);   // 繪製爆炸圖片
                PLAN_DEATH_STATE = true;
            }
            else
            {
                g.DrawImage(img, this.X, this.Y, this.Width, this.Height);
            }

        }

        /// <summary>
        /// 確認玩家的飛機種類，並讀取飛機圖片
        /// </summary>
        /// <param name="type">輸入種類</param>
        /// <returns>回傳該種類的圖片</returns>
        public Image CheckPlanType()
        {
            switch (PlanType)
            {
                case 00: img = ImageManager.Plane_Blue_1; break;
                case 01: img = ImageManager.Plane_Blue_2; ; break;
                case 02: img = ImageManager.Plane_Blue_3; break;
                case 03: img = ImageManager.Plane_Blue_4; break;
                //case 04: img = ImageManager.Plane_Orange_1; break;
                //case 05: img = ImageManager.Plane_Orange_2; break;
                //case 06: img = ImageManager.Plane_Orange_3; break;
                //case 07: img = ImageManager.Plane_Orange_4; break;
                //case 08: img = ImageManager.Plane_Pink_1; break;
                //case 09: img = ImageManager.Plane_Pink_2; break;
                //case 10: img = ImageManager.Plane_Pink_3; break;
                //case 11: img = ImageManager.Plane_Pink_4; break;
                //case 12: img = ImageManager.Plane_Yellow_1; break;
                //case 13: img = ImageManager.Plane_Yellow_2; break;
                //case 14: img = ImageManager.Plane_Yellow_3; break;
                //case 15: img = ImageManager.Plane_Yellow_4; break;
                default: img = ImageManager.Plane_Blue_1; break;
            }
            return img;
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(this.X + 36, this.Y + 27, 10, 10);
            //return new Rectangle(this.X + 36, this.Y + 27, this.Width - 36, this.Height - 27);
        }
    }
}

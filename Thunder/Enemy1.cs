using System;
using System.Drawing;
using System.Threading;

// 敵人的屬性
// Point ( 座標)
// Width , Height ( 體積)
// Image ( 圖片 )
// 存活 還是 死亡
//


// 敵人的方法
// 更新位置
// Draw 繪製 ( 存活就畫敵人、死亡就畫敵人爆炸
//
namespace Thunder
{
    // 向左邊移動的敵人
    class Enemy1
    {
        int WIDTH, HEIGHT;
        Point postion;
        //Thread thd; // 建立執行緒, 想讓每台飛機都亂飛
        Image image = Image.FromFile("images/PlaneWar/Enemy.png");
        Random rad = new Random();

        public bool Enemy1_Alive;
        bool shoot;

        public int Width { get { return WIDTH; } set { WIDTH = Width; } }
        public int Height { get { return HEIGHT; } set { HEIGHT = Height; } }
        public Image img { get { return img; } set { image = img; } }
        public Point Postion { get { return postion; } set { postion = value; } }

        public Enemy1()
        {
            Enemy1_Alive = true;
        }
        public void Draw(Graphics g)
        {
            g.DrawImage(image, postion);
        }

        public void Move()
        {
            if (postion.X < 0) postion.X += 10;
            if (shoot)
            {
                if (postion.Y != postion.Y + 100) postion.X += 100;
            }
            
        }
        void Enemy_Fire()
        {
            
            shoot = false;
        }
    }
}

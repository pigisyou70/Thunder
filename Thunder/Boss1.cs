using System.Collections.Generic;
using System.Drawing;

namespace Thunder
{
    class Boss
    {
        int prograss = 0;
        public bool AddBullet, LevelMove;
        public Point Postion = new Point(300, -200);
        //public Image imgBoss = null;
        //public Image imgBullet = null;
        public int bossPause = 0;
        public int speed = 10;
        public bool StartMove;
        public bool Alive { get; set; }
        public Image ImgBoss { get; set; }
        public Image ImgBullet { get; set; }
        public float Maxhp { get; set; }
        public float Hp { get; set; }
        public float HPunit { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Level { get; set; }




        public Boss(int MaxHP, Image BossImage, Image BossBullet)
        {
            this.ImgBoss = BossImage; // 讀取圖片
            this.ImgBullet = BossBullet; // 讀取圖片
            this.Width = ImgBoss.Width;       // 從圖片讀取寬度
            this.Height = ImgBoss.Height;     // 從圖片讀取高度
            this.Maxhp = MaxHP;
            this.Hp = this.Maxhp;                 // 設置現有血量
            this.Level = 1;
            this.Alive = false;
            // 先把HP 換算成格數，假設有 500HP  畫成1000小格，1小格就是 0.5點HP
            // 先把HP 換算成格數，假設有2000HP  畫成1000小格，1小格就是 2點HP
            this.HPunit = this.Maxhp / 1000.0f; // 算出每格是幾滴血

            // 500 / 1000 = 0.5;
        }
        public void Draw(Graphics g)
        {
            // 慢慢從最上方往下移動
            g.DrawImage(ImgBoss, Postion);

            // 先走到定位
            if (Postion.Y < 90) Postion.Y += 10;
            if (!StartMove)
            {
                if (Postion.Y == 90)
                {
                    // 顯示血條
                    g.DrawString("HP", new Font("Arial", 18), new SolidBrush(Color.White), new Point(0, 50));
                    g.DrawRectangle(new Pen(Color.Black), new Rectangle(new Point(48, 50), new Size(1024, 20)));
                    g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(new Point(48, 50), new Size(prograss, 20)));// 把它分成 1000小格, 1小格代表2滴血
          
                    if (prograss != 1000) prograss += 50;
                    if (prograss >= 1000) StartMove = true;
                }
            }
            else
            {
                CheckHP();
                g.DrawString("HP", new Font("Arial", 18), new SolidBrush(Color.White), new Point(0, 50));
                g.DrawRectangle(new Pen(Color.Black), new Rectangle(new Point(48, 50), new Size(1024, 20)));
                g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(new Point(48, 50), new Size(prograss, 20)));

                if (prograss <= 0)
                {
                    if (this.Maxhp == 500) MainForm.iScore += 2000;
                    if (this.Maxhp == 1000) MainForm.iScore += 4000;

                    this.Alive = false;
                }
            }
            this.X = Postion.X;
            this.Y = Postion.Y;
        }

        public void DrawRun(int x, int y, Graphics g)
        {
            g.DrawImage(ImgBoss, x, y);
        }
        /// <summary> 
        /// Boss 出現，Fire = false，
        /// <para>先移動到左邊 Fire = true，發射子彈 Fire = false</para> 
        /// <para>在移動到右邊 Fire = true，發射子彈 Fire = false，</para> 
        /// LOOP
        /// </summary>
        public void Move()
        {
            if (StartMove && this.Y == 90)
            {
                if (Postion.X == 0)
                {
                    speed *= -1;
                    AddBullet = true;
                }
                else if (Postion.X == 750)
                {
                    speed *= -1;
                    AddBullet = true;
                }
                if (LevelMove)
                {
                    if (Postion.X != 0 || Postion.X != 750)
                        Postion.X += speed;
                }
            }
        }

        // 魔王 和 玩家子彈碰撞偵測
        public void Collision(List<Bullet> playerBullet)
        {
            for (int i = 0; i < playerBullet.Count; i++)
            {
                if (Alive == true
                && playerBullet[i].mFacus == true
                && playerBullet[i].X + 5 > this.X
                && playerBullet[i].X + playerBullet[i].Width - 5 < this.X + this.Width
                && playerBullet[i].Y + 5 >= this.Y
                && playerBullet[i].Y + playerBullet[i].Height - 5 <= this.Y + this.Height)
                {
                    playerBullet[i].mFacus = false;     // 子彈狀態修改為失效
                    this.Hp -= MainForm.player.Damage;
                }
            }
        }
        public Rectangle GetRectangle()
        {
            return new Rectangle(this.X + 75, this.Y + 80, this.Width - 75, this.Height - 80);
        }
        public void CheckHP()
        {
            int f;
            f = (int)(this.Hp / this.HPunit);
            this.prograss = f;
        }
    }
}

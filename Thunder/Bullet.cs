using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

/* 桃紅色的子彈 */
namespace Thunder
{
    class Bullet
    {
        Image imgBullet = null;
        public bool mFacus; // 是否存活


        #region 橫縱坐標、寬度、高度、編號
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Accel { get; set; }
        public int Speed { get; set; }
        public int Number { get; set; }
        public int BulletType { get; set; }
        public Direction Dir { get; set; }
        public int Angle { get; set; }
        public int r { get; set; }
        #endregion


        /// <summary> 子彈實體化，單純讓 mFacus = true </summary>
        public Bullet(Image bullet)
        {
            Speed = 15;
            mFacus = true;
            imgBullet = bullet;
            Width = imgBullet.Width;
            Height = imgBullet.Height;
        }
        public Bullet(int type, int speed, int accel, Direction dir)
        {
            this.Speed = speed;
            this.Accel = accel;
            this.mFacus = true;
            this.BulletType = type;
            this.imgBullet = GetBulletType(type);
            this.Width = imgBullet.Width - 3;
            this.Height = imgBullet.Height - 3;
            this.Dir = dir;
        }
        public Bullet(int type, int speed, int accel, int angle, Direction dir)
        {
            this.Speed = speed;
            this.Accel = accel;
            this.mFacus = true;
            this.BulletType = type;
            this.imgBullet = GetBulletType(type);
            this.Width = imgBullet.Width - 3;
            this.Height = imgBullet.Height - 3;
            this.Angle = angle;
            this.Dir = dir;
            this.r = 100;
        }
        /// <summary> 子彈位置初始化 </summary>
        public void Init(int x, int y)
        {
            //postion = new Point(x, y);
            this.X = x;
            this.Y = y;
        }
        /// <summary> 常數子彈 </summary>
        public void DrawBullet(Graphics g)
        {
            g.DrawImage(imgBullet, this.X, this.Y);
        }
        /// <summary>
        /// 加速子彈
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            
            g.DrawImage(imgBullet, this.X, this.Y);
            Move();
        }

        /// <summary>
        /// 子彈向左邊射擊
        /// </summary>
        public void UpdateCrossLeft()
        {
            this.X -= 2;
            this.Y += 5;
        }
        /// <summary>
        /// 子彈向右邊射擊
        /// </summary>
        public void UpdateCrossRight()
        {
            this.X += 2;
            this.Y += 5;
        }
        /// <summary>
        /// 子彈向下射擊
        /// </summary>
        public void UpdateCrossStri()
        {
            this.Y += 5;
        }
        /// <summary>
        /// 一次畫出圓形子彈
        /// </summary>
        /// 從最上面中間點開始畫，順時針
        /// <param name="X">中心點X座標</param>
        /// <param name="Y">中心點Y座標</param>
        /// <param name="speed">半徑</param>
        /// <param name="accel">加速度</param>
        /// <param name="interval">每次增加的度數</param>
        /// <param name="g">畫圖</param>
        public void CreateCircle(int X, int Y, float speed, int interval, Graphics g)
        {
            this.X = X;
            this.Y = Y;
            //g.DrawImage(ImageManager.imgBallBullet1, this.X, this.Y); // 先畫出中心點，方便判別 A(480,350)
            // 看想要多大的圓, 假設大 50  B(480,400)

            Point temp;
            int angle = 0;
            while (angle < 360)
            {
                temp = Helper.GetSpeedWithAngle(angle, speed);
                angle += interval;
                g.DrawImage(ImageManager.imgBallBullet1, this.X + temp.X, this.Y + temp.Y);
            }
            //// 以下為正確示範
            //float x = r * Helper.FastCos(60); // 求出斜點的 X = r*Cos theta
            //float y = r * Helper.FastSin(60); // 求出斜點的 Y = r*Sin theta
            //g.DrawImage(ImageManager.imgBallBullet1, this.X, this.Y); // 先畫出中心點，方便判別 A(480,350)
            //g.DrawImage(ImageManager.imgRedBullet, this.X+x, this.Y+y);
        }

        public void CreateCircle2()
        {
            Point temp;
            temp = Helper.GetSpeedWithAngle(this.Angle, this.Speed);
            this.X += temp.X;
            this.Y += temp.Y;
        }

        public void CircleBullet()
        {

        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(this.X + 7, this.Y + 7, this.Width - 7, this.Height - 7);
        }
        public Image GetBulletType(int Type)
        {
            switch (Type)
            {
                case 1: imgBullet = ImageManager.imgRedBullet; break;
                case 2: imgBullet = ImageManager.imgGreenBullet; break;
                case 3: imgBullet = ImageManager.imgBlueBullet; break;
                case 8: imgBullet = ImageManager.imgEnemyBullet; break;
                case 9: imgBullet = ImageManager.imgBossBullet; break;
                case 10: imgBullet = ImageManager.imgBallBullet1; break;
            }
            return imgBullet;
        }

        /// <summary> 子彈和敵人的碰撞偵測 </summary>
        public void Collision(List<CEnemy> Enemys)
        {
            for (int j = 0; j < Enemys.Count; j++)
            {   // 如果敵人狀態為存活、 子彈為有效、
                // 子彈的 x 座標 > 敵人 x 座標 - 30 、
                // 子彈的 x 座標 < 敵人 x 座標 + 30 、
                // 子彈的 y 座標 >= 敵人 y 座標 、
                // 子彈的 y 座標 <= 敵人 y 座標 ++ 30 、
                // 敵人狀態為死亡, 子彈改為失效
                // .get() 返回此Vector中 指定位置的元素

                if (Enemys[j].Life > 0
                && mFacus == true
                && this.X > Enemys[j].X
                && this.X < Enemys[j].X + Enemys[j].Width
                && this.Y >= Enemys[j].Y
                && this.Y <= Enemys[j].Y + Enemys[j].Height)
                {
                    Enemys[j].Life -= MainForm.player.Damage;// 敵人生命減去玩家的子彈傷害
                    mFacus = false;                     // 子彈狀態修改為失效	
                }
            }
        }

        /// <summary>
        /// 對一個座標點按照一箇中心進行旋轉
        /// </summary>
        /// <param name="center">中心點</param>
        /// <param name="p1">要旋轉的點</param>
        /// <param name="angle">旋轉角度,笛卡爾直角座標</param>
        /// <returns></returns>
        public Point PointRotate(Point center, Point p1, double angle)
        {
            Point tmp = new Point();
            double angleHude = angle * Math.PI / 180;/*角度變成弧度*/
            double x1 = (p1.X - center.X) * Math.Cos(angleHude) + (p1.Y - center.Y) * Math.Sin(angleHude) + center.X;
            double y1 = -(p1.X - center.X) * Math.Sin(angleHude) + (p1.Y - center.Y) * Math.Cos(angleHude) + center.Y;
            tmp.X = (int)x1;
            tmp.Y = (int)y1;
            return tmp;
        }

        // 第一象限
        public void UpRight()
        {
            Point temp1;
            temp1 = Helper.GetSpeedWithAngle(this.Angle, this.Speed);
            this.X += temp1.X;
            this.Y -= temp1.Y;
        }
        // 第二象限
        public void UpLeftt()
        {
            Point temp1;
            temp1 = Helper.GetSpeedWithAngle(this.Angle, this.Speed);
            this.X -= temp1.X;
            this.Y -= temp1.Y;
        }
        // 第三象限
        public void DownLeft2()
        {

            Point temp1;
            temp1 = Helper.GetSpeedWithAngle(this.Angle, this.Speed);
            this.X -= temp1.X;
            this.Y += temp1.Y;
        }
        // 第四象限
        public void DownRight2()
        {
            Point temp1;
            temp1 = Helper.GetSpeedWithAngle(this.Angle, this.Speed);
            this.X += temp1.X;
            this.Y += temp1.Y;
        }

        public void Up()
        {
            this.Y -= this.Speed;
        }

        public void Move()
        {
            switch (this.Dir)
            {
                case Direction.Up: this.Y -= this.Speed; break;
                case Direction.Down: this.Y += this.Speed; break;
                case Direction.Right: this.X += this.Speed; break;
                case Direction.Left: this.X -= this.Speed; break;

                case Direction.TopLeft: this.X -= this.Speed; this.Y -= this.Speed; break;
                case Direction.TopRight: this.X += this.Speed; this.Y -= this.Speed; break;
                case Direction.DownLeft: this.X += this.Speed; this.Y += this.Speed; break;
                case Direction.DownRight: this.X -= this.Speed; this.Y += this.Speed; break;
                case Direction.TopLeft2: UpLeftt(); break;
                case Direction.TopRight2: UpRight(); break;
                case Direction.DownLeft2: DownLeft2(); break;
                case Direction.DownRight2: DownRight2(); break;
            }

            //if (this.Y > 800) this.mFacus = false;
        }
    }
}

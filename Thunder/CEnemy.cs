using System.Drawing;
using System.Windows.Forms;

// 子彈分為 敵方和我方
namespace Thunder
{
    /// <summary> 隨機X座標生成, 移動方式為持續往下方移動，不攻擊 </summary>
    class CEnemy
    {
        /// <summary> 敵人編號 </summary>
        public int mPlayID = 0;
        /// <summary> 敵人狀態為死亡 </summary>
        public bool ENEMY_DEATH_STATE;
        /// <summary> 敵人移動速度 </summary>
        //static int Speed = 5;
        /// <summary> 爆炸圖片 </summary>
        Image imgEnemy;

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Direction Dir { get; set; }
        public int Life { get; set; }
        public int EnemyType { get; set; }
        public int Speed { get; set; }
        public bool Special { get; set; }

        public CEnemy(int type, int speed, Direction dir)
        {
            imgEnemy = GetImage(type);
            //mAnimState = ENEMY_ALIVE_STATE;
            EnemyType = type;
            // 取得 敵人 寬度 和 高度
            Width = imgEnemy.Width;     // Width  = 80
            Height = imgEnemy.Height;   // Height = 56
            this.EnemyType = type;
            this.Dir = dir;
            this.Life = GetLife(type);
            this.Speed = speed;
        }



        // 根據飛機的類型 返回對應的圖片
        public Image GetImage(int type)
        {
            // 靜態函數中，只能訪問靜態成員
            switch (type)
            {
                case 0: return ImageManager.imeEnemy0; // 綠色飛機 往左邊飛 Score +300
                case 1: return ImageManager.imeEnemy1; // 綠色飛機         Score +300
                case 2: return ImageManager.imeEnemy2; // 紫色飛機         Score +500
                case 3: return ImageManager.imeEnemy3; // 深綠飛機
                case 4: return ImageManager.imeEnemySpecialRight; // 投送道具飛機 往右邊飛
                case 5: return ImageManager.imeEnemySpecialLeft; // 投送道具飛機 往左邊飛
                case 6: return ImageManager.imeEnemy4; // 藍色飛機 (移動速度超快) Score +100
            }
            return null;
        }

        // 根據飛機的類型 返回對應的生命
        public static int GetLife(int type)
        {
            switch (type)
            {
                case 0: return 3;
                case 1: return 3;
                case 2: return 5;
                case 3: return 5;
                case 4: return 8;
                case 5: return 8;
                case 6: return 1;
            }
            return 0;
        }

        /// <summary> 敵人位置初始化 </summary>
        public void init(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void Draw(PaintEventArgs g)
        {
            Move();
            // 假如敵人目前狀態為死亡, 和 目前圖片數量 < 爆炸圖片總數量
            if (this.Life <= 0)
            {
                GetScore();
                g.Graphics.DrawImage(ImageManager.imgEnemyExplore, this.X, this.Y); // 爆炸圖片
                if(MainForm.VoiceEnable) MainForm.soundExplode.controls.play();
                ENEMY_DEATH_STATE = true;

            }
            else
            {
                g.Graphics.DrawImage(imgEnemy, this.X, this.Y, imgEnemy.Width, imgEnemy.Height);
            }
        }


        /// <summary> 敵人移動 </summary>
        public void Move()
        {
            switch (this.Dir)
            {
                case Direction.Up:
                    this.Y -= Speed;
                    break;
                case Direction.Down:
                    this.Y += Speed;
                    break;
                case Direction.Left:
                    this.X -= Speed;
                    break;
                case Direction.Right:
                    this.X += Speed;
                    break;
                default:
                    this.Y += Speed;
                    break;
            }
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(this.X + 10, this.Y + 10, this.Width - 10, this.Height - 10);
        }
        public void GetScore()
        {
            switch (this.EnemyType)
            {
                case 1: MainForm.iScore += GetLife(this.EnemyType) * 100; break;
                case 2: MainForm.iScore += GetLife(this.EnemyType) * 100; break;
                case 3: MainForm.iScore += GetLife(this.EnemyType) * 100; break;
                case 4: MainForm.iScore += GetLife(this.EnemyType) * 100; break;
                case 5: MainForm.iScore += GetLife(this.EnemyType) * 100; break;
                case 6: MainForm.iScore += GetLife(this.EnemyType) * 100; break;
            }
        }
    }
}

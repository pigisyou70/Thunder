using System;
using System.Drawing;

namespace Thunder
{
    public static class Data
    {
        public static Bitmap BulletSource = null;
        public static Bitmap BackgroundSource = null;
        public static Bitmap BulletAllSource = null;
        public static Bitmap PlayerSource = null;
        public static Bitmap PlayerPointSource = null;
        public static Bitmap HpSource = null;
        public static Bitmap BombSource = null;
        public static Bitmap EnemySource = null;

        public static Bitmap Boss1Source = null;

        public static Bitmap[] BulletsSource = null;

        public static Font NormalFont = new Font("Arial", 12);

        public static Random Rnd = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// 2 * PI
        /// </summary>
        public static float TwoPI = (float)Math.PI * 2.0f;

        /// <summary>
        /// 弧度单位
        /// </summary>
        public static float Radian = (float)Math.PI / 180.0f;

        /// <summary>
        /// 不同类型的敌人位图在图库中的位置
        /// </summary>
        public static Rectangle[] EnemySourceRectangle = new Rectangle[]
        {
            new Rectangle(0, 0, 34, 31), // (1, 1) Enemy 第1排  第1个
			new Rectangle(35, 0, 31, 33), // (1, 2)
			new Rectangle(0, 33, 33, 27), // (2, 1)
			new Rectangle(67, 0, 36, 32), // (1, 3)
			new Rectangle(104, 0, 30, 36), // (1, 4)
			new Rectangle(133, 54, 38, 22), // (2, 4)
			new Rectangle(39, 34, 42, 39), // (2, 2)
			new Rectangle(82, 39, 48, 34), // (2, 3)
			new Rectangle(135, 0, 64, 53) // (1, 5)
		};

        /// <summary>
        /// 不同类型的敌人的碰撞盒子（X,Y相对于位图中心点的便宜，W,H大小）
        /// </summary>
        public static Rectangle[] EnemyBoxColliderRectangle = new Rectangle[]
        {
            new Rectangle(0, 0, 34, 31), // (1, 1) Enemy 第1排  第1个
			new Rectangle(0, 0, 31, 33), // (1, 2)
			new Rectangle(0, 0, 33, 27), // (2, 1)
			new Rectangle(0, 0, 36, 32), // (1, 3)
			new Rectangle(0, 0, 30, 36), // (1, 4)
			new Rectangle(0, 0, 38, 22), // (2, 4)
			new Rectangle(0, 0, 23, 39), // (2, 2)
			new Rectangle(0, -5, 48, 24), // (2, 3)
			new Rectangle(0, -10, 64, 37) // (1, 5)
		};

        /// <summary>
        /// 不同子弹类型的大小
        /// </summary>
        public static Size[] BulletsSize = new Size[]
        {
            new Size(10, 10),
            new Size(16, 14),
            new Size(18, 18),
            new Size(18, 18),
            new Size(9, 17),
            new Size(9, 17),
            new Size(7, 44)
        };

        public enum MouseButton
        {
            Left,
            Right,
            Middle
        }

        /// <summary>
        /// 方向
        /// </summary>
        public enum Direction
        {
            None,
            Up,
            Down,
            Left,
            Right,
            LeftUp,
            LeftDown,
            RightUp,
            RightDown
        }
        public static class DepthLayer
        {
            public const int Bottom = 0;

            public const int Background = 10; // 背景层  
            public const int Map = 20; // 地表层  
            public const int Bullet = 30; // 子弹层  
            public const int Object = 40; // 单位层  
            public const int Effect = 50; // 特效层  
            public const int UI = 90; // UI层  

            public const int Top = 100;
        }
    }
}

using System.Drawing;

namespace Thunder
{
    static class ImageManager
    {
        // 登入視窗所需要的圖片
        public static readonly Image imgLOGO = Image.FromFile("images/LOGO.png");
        public static readonly Image imgContiune = Image.FromFile("images/Contiune_icon.png");
        public static readonly Image imgHome = Image.FromFile("images/Home_icon.png");
        public static readonly Image imgBG = Image.FromFile("images/bg-3.jpg");
        public static readonly Image imgPlayer3 = Image.FromFile("images/PlaneWar/player3.png");


        // 背景圖片
        public static readonly Image imgGameBackground = Image.FromFile("images/bg-4.jpg");
        public static readonly Image imgMenuBackGround = Image.FromFile("images/bg-3.jpg");
        public static readonly Image imgStoreBackGround = Image.FromFile("images/BackgroundStore.jpg");
        public static readonly Image imgTextBackGround = Image.FromFile("images/TextBackGround.png");

        // 魔王圖片
        public static readonly Image imgBoss1 = Image.FromFile("images/PlaneWar/boss1.png");
        public static readonly Image imgBoss2 = Image.FromFile("images/PlaneWar/boss2.png");
        public static readonly Image imgBoss3 = Image.FromFile("images/PlaneWar/boss3.png");


        // 選項圖片
        public static readonly Image imgSelect = Image.FromFile("images/select.png");
        // 失敗圖片
        public static readonly Image imgDefeat = Image.FromFile("images/Defeat.png");
        // 勝利圖片
        public static readonly Image imgVictor = Image.FromFile("images/Victor.png");

        // 數字圖片
        public static readonly Image imgNumber0 = Image.FromFile("images/PlaneWar/Number_0.png"); // 黃色數字 0
        public static readonly Image imgNumber1 = Image.FromFile("images/PlaneWar/Number_1.png"); // 黃色數字 1
        public static readonly Image imgNumber2 = Image.FromFile("images/PlaneWar/Number_2.png"); // 黃色數字 2
        public static readonly Image imgNumber3 = Image.FromFile("images/PlaneWar/Number_3.png"); // 黃色數字 3
        public static readonly Image imgNumber4 = Image.FromFile("images/PlaneWar/Number_4.png"); // 黃色數字 4
        public static readonly Image imgNumber5 = Image.FromFile("images/PlaneWar/Number_5.png"); // 黃色數字 5
        public static readonly Image imgNumber6 = Image.FromFile("images/PlaneWar/Number_6.png"); // 黃色數字 6
        public static readonly Image imgNumber7 = Image.FromFile("images/PlaneWar/Number_7.png"); // 黃色數字 7
        public static readonly Image imgNumber8 = Image.FromFile("images/PlaneWar/Number_8.png"); // 黃色數字 8
        public static readonly Image imgNumber9 = Image.FromFile("images/PlaneWar/Number_9.png"); // 黃色數字 9


        // 子彈圖片
        public static readonly Image imgBossBullet = Image.FromFile("images/Bullet/BossBullet.png");
        public static readonly Image imgBallBullet1 = Image.FromFile("images/Bullet/BallBullet1-1.png");
        public static readonly Image imgPlayerBullet = Image.FromFile("images/Bullet/bullet_0.png");
        public static readonly Image imgEnemyBullet = Image.FromFile("images/Bullet/bullet_1.png");
        public static readonly Image imgRedBullet = Image.FromFile("images/Bullet/RedBullet2.png");
        public static readonly Image imgGreenBullet = Image.FromFile("images/Bullet/GreenBullet2.png");
        public static readonly Image imgBlueBullet = Image.FromFile("images/Bullet/BlueBullet2.png");


        //// 人物CG圖片
        public static readonly Image imgBlueCG = Image.FromFile("images/PlaneWar/Hero_0.jpg");
        //public static readonly Image imgPinkCG = Image.FromFile("images/PlaneWar/Hero_1.jpg");
        //public static readonly Image imgYellowCG = Image.FromFile("images/PlaneWar/Hero_3.jpg");
        //public static readonly Image imgOrangeCG = Image.FromFile("images/PlaneWar/Hero_2.jpg");
        //public static readonly Image imgKnight = Image.FromFile("images/PlaneWar/Hero_4.jpg");
        //public static readonly Image imgBird = Image.FromFile("images/PlaneWar/Hero_5.jpg");
        //public static readonly Image imgEagle = Image.FromFile("images/PlaneWar/Hero_6.jpg");

        // 玩家飛機種類
        public static readonly Image Plane_Blue_1 = Image.FromFile("images/PlaneWar/Blue_1.png"); 
        public static readonly Image Plane_Blue_2 = Image.FromFile("images/PlaneWar/Blue_2.png");
        public static readonly Image Plane_Blue_3 = Image.FromFile("images/PlaneWar/Blue_3.png");
        public static readonly Image Plane_Blue_4 = Image.FromFile("images/PlaneWar/Blue_4.png");
        //public static readonly Image Plane_Orange_1 = Image.FromFile("images/PlaneWar/Orange_1.png");
        //public static readonly Image Plane_Orange_2 = Image.FromFile("images/PlaneWar/Orange_2.png");
        //public static readonly Image Plane_Orange_3 = Image.FromFile("images/PlaneWar/Orange_3.png");
        //public static readonly Image Plane_Orange_4 = Image.FromFile("images/PlaneWar/Orange_4.png");
        //public static readonly Image Plane_Pink_1 = Image.FromFile("images/PlaneWar/Pink_1.png");
        //public static readonly Image Plane_Pink_2 = Image.FromFile("images/PlaneWar/Pink_2.png");
        //public static readonly Image Plane_Pink_3 = Image.FromFile("images/PlaneWar/Pink_3.png");
        //public static readonly Image Plane_Pink_4 = Image.FromFile("images/PlaneWar/Pink_4.png");
        //public static readonly Image Plane_Yellow_1 = Image.FromFile("images/PlaneWar/Yellow_1.png");
        //public static readonly Image Plane_Yellow_2 = Image.FromFile("images/PlaneWar/Yellow_2.png");
        //public static readonly Image Plane_Yellow_3 = Image.FromFile("images/PlaneWar/Yellow_3.png");
        //public static readonly Image Plane_Yellow_4 = Image.FromFile("images/PlaneWar/Yellow_4.png");

        // 玩家死亡圖片
        public static readonly Image imgPlayer_Explode = Image.FromFile("images/Effect.png");



        // 隕石
        public static readonly Image imgMeteorite = Image.FromFile("images/Meteorite2.png"); 

        // 敵人的圖片
        public static readonly Image imeEnemy0 = Image.FromFile("images/PlaneWar/Enemy0.png");  // 綠色飛機 往左邊飛 Score +300
        public static readonly Image imeEnemy1 = Image.FromFile("images/PlaneWar/Enemy.png");   // 綠色飛機         Score +300
        public static readonly Image imeEnemy2 = Image.FromFile("images/PlaneWar/Enemy1.png");  // 紫色飛機         Score +500
        public static readonly Image imeEnemy3 = Image.FromFile("images/PlaneWar/Enemy2.png");  // 深綠飛機
        public static readonly Image imeEnemy4 = Image.FromFile("images/PlaneWar/Enemy4.png");  // 藍色飛機 (移動速度超快) Score +100

        public static readonly Image imeEnemySpecialRight = Image.FromFile("images/PlaneWar/EnemySpecial1.png");  // 投送道具飛機 往右邊飛
        public static readonly Image imeEnemySpecialLeft = Image.FromFile("images/PlaneWar/EnemySpecial2.png");  // 投送道具飛機 往左邊飛

        // 敵人的爆炸圖片
        public static readonly Image imgEnemyExplore = Image.FromFile("images/Explore.png");


        // 空殼星
        public static readonly Image DrawStar = Image.FromFile("images/DrawStar.png");
        // 實體星
        public static readonly Image FillStar = Image.FromFile("images/FillStar.png");

        // 商店的強化按鈕
        public static readonly Image imgButton = Image.FromFile("images/PlaneWar/Button.png");
        public static readonly Image imgCoin = Image.FromFile("images/PlaneWar/Coin2.png");

        // 暫停按鈕
        public static readonly Image imgPause = Image.FromFile("images/pause.png");
        // 返回按鈕
        public static readonly Image imgReturn = Image.FromFile("images/return.png"); 


        //public static readonly Image PlayerBulletFire = Image.FromFile("images/PlaneWar/fire_4.png");

        // 道具
        public static readonly Image imgPowerUp = Image.FromFile("images/PlaneWar/PowerUp.png");
        public static readonly Image imgShiled = Image.FromFile("images/PlaneWar/Shiled.png");
        public static readonly Image imgProtect = Image.FromFile("images/PlaneWar/Protect.png");
        //public static readonly Image imgBoom = Image.FromFile("images/PlaneWar/Boom.png");

        // 說明頁面
        public static readonly Image imgInformation1 = Image.FromFile("images/Information1.png");
        public static readonly Image imgInformation2 = Image.FromFile("images/Information2.png");
        public static readonly Image imgInformation3 = Image.FromFile("images/Information3.png");
        public static readonly Image imgInformation4 = Image.FromFile("images/Information4.png");
        public static readonly Image LeftArrow = Image.FromFile("images/PlaneWar/Left.png");
        public static readonly Image RightArrow = Image.FromFile("images/PlaneWar/Right.png");

        // 暫停視窗的背景圖片
        public static readonly Image imgPauseLayout = Image.FromFile("images/layout.png");
        public static readonly Image imgSpeaker = Image.FromFile("images/Speaker.png");
        public static readonly Image imgNoSpeaker = Image.FromFile("images/NoSpeaker.png");

        // BOSS來襲
        public static readonly Image imgRed = Image.FromFile("images/PlaneWar/Red1.png");
        public static readonly Image imgRedLight = Image.FromFile("images/PlaneWar/RedLight.png");
        public static readonly Image imgWarning = Image.FromFile("images/PlaneWar/Warning.png");

    }
}

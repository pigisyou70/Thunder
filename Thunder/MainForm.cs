using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WMPLib;
// 目前遇到問題，需要把王該如何生成
namespace Thunder
{
    enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        TopLeft,
        TopRight,
        DownLeft,
        DownRight,
        TopLeft2,
        TopRight2,
        DownLeft2,
        DownRight2
    }
    public partial class MainForm : Form
    {
        Graphics g = null;
        static Random r = new Random();

        #region 遊戲背景屬性
        public static readonly int mScreenWidth = 1024, mScreenHeight = 768;
        Point mBitposY0 = new Point(0, 0);  // 背景圖1座標
        Point mBitposY1 = new Point(0, 0);  // 背景圖2座標
        int TextCount = 0;
        int Information_Page = 0;
        #endregion

        #region 玩家屬性
        public String GetName { set { userName = value; } }
        public static Player player = new Player();
        String userName;      // 從 LoginForm 那邊拿取
        #endregion

        #region UI屬性
        /// <summary> 玩家金錢 </summary>
        int iMoney = 0;
        /// <summary> 玩家分數 </summary>
        static public float iScore = 0;
        /// <summary> 玩家分數 </summary>
        String sScore;
        /// <summary> 遊戲時間 </summary>
        int timer_counts = 0, minutes, second;
        /// <summary> CG圖片替換編號 </summary>
        int CGPicture_Count = 0;
        bool cougi = false;
        /// <summary> 進度條 </summary>
        int iLoadprogram = 0;
        int PauseButton_X = 930, PauseButton_Y = 5;/** 主選單指向的座標 */
        Point pSelect = new Point(125, 395);
        Point Information = new Point(400, 550);
        /// <summary>
        /// 獲得的金錢
        /// </summary>
        int Earn;
        int tempEarn;
        #endregion

        #region 狀態選擇變數 - 可以讓系統得知, 目前是否切換介面
        public int mState = 99;
        int iStage = 99; // 第幾關卡
        bool GAME_START;
        readonly static int GAME_STAGE1 = 5;        // 遊戲狀態  5 遊戲開始
        readonly static int GAME_STAGE2 = 6;        // 遊戲狀態  6 遊戲開始
        readonly static int GAME_STAGE3 = 7;        // 遊戲狀態  7 遊戲開始
        readonly static int GAME_MENU = 1;          // 遊戲狀態  1 遊戲選單
        readonly static int GAME_SHOP = 2;         // 遊戲狀態  2 遊戲商店
        readonly static int GAME_LEAVE = 3;         // 遊戲狀態  3 遊戲離開
        readonly static int GAME_VICTOR = 4;        // 遊戲狀態  3 遊戲離開
        public readonly static int GAME_DEFEAT = 10;  // 遊戲狀態 10 遊戲失敗
        readonly static int GAME_LOADING = 8;       // 遊戲狀態  8 進場特效
        readonly static int GAME_STORY = 9;       // 遊戲狀態  9 最後一篇劇情
        readonly static int GAME_INFORMATION = 11;
        static int k;
        #endregion

        #region Sound 屬性
        public static bool VoiceEnable = true;
        public static WindowsMediaPlayer soundBackground = new WindowsMediaPlayer();      // 背景音樂
        public static WindowsMediaPlayer soundExplode = new WindowsMediaPlayer();         // Explode SFX
        public WindowsMediaPlayer soundShot = new WindowsMediaPlayer();            // Shoot SFX
        public WindowsMediaPlayer soundMenu = new WindowsMediaPlayer();            // Shoot SFX
        public WindowsMediaPlayer soundLose = new WindowsMediaPlayer();            // Shoot SFX
        public static WindowsMediaPlayer soundWarring = new WindowsMediaPlayer();            // Shoot SFX

        #endregion

        #region 小怪屬性
        List<CEnemy> ListEnemys_Type = new List<CEnemy>(); /* 第1種敵人 --- 只會飛行 */
        int ENEMY_POOL_COUNT = 10;                       /* 最多同時存在敵人數 */
        bool SpecialEnemy;
        #endregion

        #region 玩家子彈可以隨著火力等級強化 一次發射 1-4顆子彈
        List<Bullet> PlayerBullets1 = new List<Bullet>();  /* 新建子彈集合, 可隨時新增刪除數量 */
        List<Bullet> PlayerBullets2 = new List<Bullet>();  /* 新建子彈集合, 可隨時新增刪除數量 */
        List<Bullet> PlayerBullets3 = new List<Bullet>();  /* 新建子彈集合, 可隨時新增刪除數量 */
        List<Bullet> PlayerBullets4 = new List<Bullet>();  /* 新建子彈集合, 可隨時新增刪除數量 */

        Image imgPlayerBullet = null;
        int BULLET_POOL_COUNT = 15;                 /* 最多同時存在的子彈數 */
        #endregion

        #region 隕石生成
        Meteorite[] meteorite = new Meteorite[6];
        #endregion

        #region 判定移動方向
        bool bUp, bDown, bLeft, bRight;
        #endregion

        #region Timer 速度設定
        int iGameTimer = 60, iBulletTimer = 750, iEnemyTimer = 471;
        #endregion

        #region 魔王屬性
        int BossBullets = 7;
        Boss boss1 = new Boss(500, ImageManager.imgBoss1, ImageManager.imgBossBullet);
        Boss boss2 = new Boss(1000, ImageManager.imgBoss2, ImageManager.imgBossBullet);
        #endregion

        #region 子彈屬性
        List<Bullet> BossBullets1 = new List<Bullet>();
        List<Bullet> BossBullets2 = new List<Bullet>();
        List<Bullet> BossBullets3 = new List<Bullet>();
        List<Bullet> BossBullets4 = new List<Bullet>();
        List<Bullet> BossBullets5 = new List<Bullet>();
        List<Bullet> BossBullets6 = new List<Bullet>();
        List<Bullet> BossBullets7 = new List<Bullet>();
        List<Bullet> BossBullets8 = new List<Bullet>();
        List<Bullet> BossBullets9 = new List<Bullet>();
        List<Bullet> BossBullets10 = new List<Bullet>();

        List<Bullet> BossBullets11 = new List<Bullet>();
        List<Bullet> BossBullets12 = new List<Bullet>();
        List<Bullet> BossBullets13 = new List<Bullet>();
        List<Bullet> BossBullets14 = new List<Bullet>();
        List<Bullet> BossBullets15 = new List<Bullet>();
        List<Bullet> BossBullets16 = new List<Bullet>();
        List<Bullet> BossBullets17 = new List<Bullet>();
        List<Bullet> BossBullets18 = new List<Bullet>();
        List<Bullet> BossBullets19 = new List<Bullet>();
        List<Bullet> BossBullets20 = new List<Bullet>();
        List<Bullet> BossBullets21 = new List<Bullet>();

        List<Bullet> EnemyBullets1 = new List<Bullet>();

        int mSendId = 0;                            /* 子彈編號 */
        readonly int BULLET_LEFT_OFFSET = +43;      /* 子彈左偏移量 */
        readonly int BULLET_UP_OFFSET = -42;        /* 子彈上偏移量 */
        #endregion

        #region 暫停視窗
        readonly pauseForm pause = new pauseForm();
        #endregion

        #region 勝利條件 根據第一個參數的值去改變要顯示的東西 
        //Victor_Condition victor_Condition1 = new Victor_Condition(1, 290, 150);
        //Victor_Condition victor_Condition2 = new Victor_Condition(2, 290, 150);
        //Victor_Condition victor_Condition3 = new Victor_Condition(3, 290, 150);
        bool Stage1_Start;
        bool Stage2_Start;
        bool Stage3_Start;
        #endregion

        #region 商店屬性
        StorePlane store = new StorePlane();
        Image storeType = null;
        #endregion

        #region 登入畫面生成
        public bool GetLogin;
        static UserInfo ui = new UserInfo();
        readonly LoginForm login = new LoginForm(ref ui);
        #endregion

        #region 道具
        List<GameItems> Items = new List<GameItems>();
        public static int PowerUpTime = 10;
        public static int ShiledTime = 10;
        private bool ShiledStatus;

        bool PowerStatus;
        #endregion

        #region 主畫面第一件事情
        public MainForm()
        {
            login.Owner = this;
            DialogResult result = login.ShowDialog();
            // 假如點選右上角XX 關閉登入視窗，就把主視窗也關閉
            if (result == DialogResult.Cancel)
            {
                Close();
                Environment.Exit(Environment.ExitCode);
            }
            Height = mScreenHeight;    // 框架高度
            Width = mScreenWidth;      // 框架寬度
            InitializeComponent();                      // 初始組件
            InitializeSound();                          // 初始化聲音
            InitializeVariable();
            DoubleBuffered = true;                      // 防止圖像閃爍

            if (GetLogin)
            {
                userName = "Guest";
                //String str1UserName = "Guest";            // 玩家暱稱
                iMoney = 0;            // 金錢
                player.PlanType = 0;   // 飛機狀態
                //String str4PlayTime = null;            // 遊玩時間
                player.FireLevel = 0;  // 火力等級
                player.SpeedLevel = 0; // 速度等級
                player.RateLevel = 0;  // 射速等級
            }
            else
            {
                LoadFile();
                CheckPlayerGuns();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            GameTimer.Enabled = true;
            BulletTimer.Enabled = true;
            EnemyTimer.Enabled = true;

            SetPlayerStatus();  // 設定玩家屬性

            store.Udate(player.FireLevel, player.SpeedLevel, player.RateLevel); // 把玩家的數值丟到商店內, 作變更顯示

            GameTimer.Interval = iGameTimer;
            BulletTimer.Interval = iBulletTimer;    // 子彈間格時間
            EnemyTimer.Interval = iEnemyTimer;      // 敵人生成間格時間



        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics; // 建立 Graphics 物件
            g.SmoothingMode = SmoothingMode.HighQuality; // 反鋸齒功能

            // 模式 = 遊戲選單
            if (mState == GAME_MENU)
            {
                if (VoiceEnable) soundMenu.controls.play();
                g.DrawImage(ImageManager.imgStoreBackGround, 0, 0, 1024, 768);
                g.DrawImage(ImageManager.imgSelect, pSelect);

                g.DrawString("開始遊戲", new Font("Arial", 20), new SolidBrush(Color.White), 150, 400);
                g.DrawString("商店", new Font("Arial", 20), new SolidBrush(Color.White), 150, 440);
                g.DrawString("操作說明", new Font("Arial", 20), new SolidBrush(Color.White), 150, 480);
                g.DrawString("離開遊戲", new Font("Arial", 20), new SolidBrush(Color.White), 150, 520);
                TimeNow.Enabled = true;
                cougi = false;
            }
            else if (mState == GAME_INFORMATION)
            {
                g.DrawImage(ImageManager.imgBG, 0, 0, 1024, 768);
                switch (Information_Page)
                {
                    case 0: g.DrawImage(ImageManager.imgInformation1, new Point(135, 100)); break;
                    case 1: g.DrawImage(ImageManager.imgInformation2, new Point(135, 100)); break;
                    case 2: g.DrawImage(ImageManager.imgInformation3, new Point(135, 100)); break;
                    case 3: g.DrawImage(ImageManager.imgInformation4, new Point(135, 100)); break;
                }
                g.DrawImage(ImageManager.Plane_Blue_4, Information);
                g.DrawImage(ImageManager.LeftArrow, new Point(50, 300));
                g.DrawImage(ImageManager.RightArrow, new Point(900, 300));
                UIpaint(g);
            }
            else if (mState == GAME_STAGE1)
            {
                if (VoiceEnable) soundMenu.controls.stop();
                g.DrawImage(ImageManager.imgGameBackground, mBitposY0);
                g.DrawImage(ImageManager.imgGameBackground, mBitposY1);
                if (!Stage1_Start)
                {
                    g.FillRectangle(Brushes.Black, new Rectangle(0, 0, 1024, 768));
                    if (TextCount > 1) g.DrawImage(ImageManager.imgTextBackGround, new Point(0, 0));
                    if (TextCount > 2) g.DrawString("地球防衛組織：最近發現天空上有幾架飛機，看起來不像是地球的機種，", new Font("標楷體", 20), new SolidBrush(Color.White), 0, 0);
                    if (TextCount > 2) g.DrawString("感覺來者意圖不軌，距離上次星際大戰隔了2000年了...該不會外星人又來了", new Font("標楷體", 20), new SolidBrush(Color.White), 0, 50);
                    if (TextCount > 7) g.DrawString("地球防衛組織：該派出航空防衛隊去偵查，不然被媒體看到，肯定又會大肆宣揚，", new Font("標楷體", 20), new SolidBrush(Color.White), 0, 150);
                    if (TextCount > 7) g.DrawString("引起人民的恐慌 ", new Font("標楷體", 20), new SolidBrush(Color.White), 0, 200);
                    if (TextCount > 12) g.DrawString("地球防衛組織：" + userName + "你去看一看。", new Font("標楷體", 20), new SolidBrush(Color.White), 0, 300);
                    if (TextCount > 17) g.DrawString(userName + "：遵命！", new Font("標楷體", 20), new SolidBrush(Color.White), 0, 400);
                    if (TextCount > 21) g.DrawString("Press Click to Contiune !", new Font("Arial", 24), new SolidBrush(Color.White), 330, 700);
                    SetPlayerStatus();
                    k = 0;
                    // 分數歸零
                    iScore = 0;
                }
                if (Stage1_Start)
                {
                    g.DrawString("1-1", new Font("Arial", 20), Brushes.Black, 0, 700);
                    #region 初始化狀態
                    EnemyTimer.Enabled = true; // 敵人開始生成
                    BulletTimer.Enabled = true; // 子彈開始射擊
                    if (VoiceEnable) soundBackground.controls.play();// 音樂 播放
                    GAME_START = true;
                    ENEMY_POOL_COUNT = 7;   // 敵人數量

                    // 假如玩家存活 且 生命 > 0    
                    if (player.Life > 0)
                    {
                        player.Draw(g); // 顯示玩家圖片
                        // 子彈圖片 
                        DrawPlayerBullet(player.Guns);
                    }
                    #endregion
                    #region 顯示敵人
                    if (k > 1 && k < 1250)
                        for (int i = 0; i < ListEnemys_Type.Count; i++)
                        {
                            ListEnemys_Type[i].Draw(e);

                            #region 當特殊敵人死亡時，道具生成, 添加到Item List
                            if (ListEnemys_Type[i].Special && ListEnemys_Type[i].ENEMY_DEATH_STATE)
                            {
                                GameItems tempItem = new GameItems(ListEnemys_Type[i].X, ListEnemys_Type[i].Y, r.Next(1, 3), 1, Direction.Down);
                                Items.Add(tempItem);
                            }
                            // 當道具和玩家進行碰撞時，PowerStatus true 、移除物件
                            // 沒有碰撞時，顯示道具
                            for (int j = 0; j < Items.Count; j++)
                            {
                                if (Items[j].GetRectangle().IntersectsWith(player.GetRectangle()))
                                {
                                    if (Items[j].Type == 1) { PowerStatus = true; PowerUpTime = 10; }
                                    if (Items[j].Type == 2) { ShiledStatus = true; ShiledTime = 10; }

                                    Items.Remove(Items[j]);
                                }
                                else
                                    Items[j].Draw(g);
                            }
                            #endregion
                        }
                    #endregion
                    #region 時間到觸發隕石掉落事件 (隕石生成
                    if ((k >= 250 && k < 500) || (k >= 500 && k < 750) || (k >= 750 && k < 1000))
                    {
                        meteorite[0].Paint(g);
                        meteorite[1].Paint(g);
                        meteorite[2].Paint(g);
                        meteorite[3].Paint(g);
                        meteorite[4].Paint(g);
                        meteorite[5].Paint(g);
                    }
                    #endregion
                    #region 第一關結束
                    if (k >= 998 && k < 1250)
                    {
                        GAME_START = false;// 遊戲停止
                        ENEMY_POOL_COUNT = 0;   // 暫時取消敵人生成 
                        ListEnemys_Type.Clear();
                        Items.Clear();
                        BulletTimer.Enabled = false;
                        if (player.Y > 0)
                        {
                            g.DrawImage(ImageManager.imgVictor, new Point(250, 70));
                            player.Y -= 15; // 玩家往上飛, 再顯示勝利 

                            // 道具時間及狀態
                            PowerUpTime = 10;
                            ShiledTime = 10;

                            PowerStatus = false;
                            ShiledStatus = false;

                            // 假如獲勝就把分數轉換成金錢
                            if (!cougi)
                            {
                                tempEarn = (int)iScore;
                                tempEarn /= 100;
                                tempEarn *= 2;
                                iMoney += tempEarn;
                                Earn = Earn + tempEarn;
                                cougi = true;
                            }
                        }
                        else
                        {
                            TextCount = 0;
                            mState = GAME_STAGE2;
                        }
                    }
                    #endregion
                    #region 道具剩餘時間
                    if (ShiledStatus)
                    {
                        g.DrawImage(ImageManager.imgProtect, player.X - 15, player.Y - 20);
                        g.DrawImage(ImageManager.imgShiled, 10, 40, 30, 30);
                        g.DrawString("剩餘時間：" + ShiledTime, new Font("Arian", 18), Brushes.White, new Point(50, 40));
                    }
                    if (PowerStatus)
                    {
                        g.DrawImage(ImageManager.imgPowerUp, 10, 70, 32, 32);
                        g.DrawString("剩餘時間：" + PowerUpTime, new Font("Arian", 18), Brushes.White, new Point(50, 70));
                    }
                    #endregion
                    UIpaint(g);
                }
            }
            else if (mState == GAME_STAGE2)
            {
                if (VoiceEnable) soundMenu.controls.stop();
                Stage1_Start = false;
                g.DrawImage(ImageManager.imgGameBackground, mBitposY0);
                g.DrawImage(ImageManager.imgGameBackground, mBitposY1);
                if (!Stage2_Start)
                {
                    g.DrawImage(ImageManager.imgTextBackGround, new Point(0, 0));
                    if (TextCount > 1) g.DrawString(userName + "：怎麼打來打去都進是一些小兵。也不會攻擊該不會是無人偵察機吧...。", new Font("標楷體", 20), new SolidBrush(Color.White), 0, 0);
                    if (TextCount > 6) g.DrawString("地球防衛組織：你在調查看看，應該有人在控制那些無人機。", new Font("標楷體", 20), new SolidBrush(Color.White), 0, 100);
                    if (TextCount > 11) g.DrawString(userName + "：好吧，我再看看還有什麼", new Font("標楷體", 20), new SolidBrush(Color.White), 0, 200);
                    if (TextCount > 16) g.DrawString("Press Click to Contiune !", new Font("Arial", 24), new SolidBrush(Color.White), 330, 700);
                    SetPlayerStatus();
                    k = 0;
                    // 分數歸零
                    iScore = 0;
                }
                if (Stage2_Start)
                {
                    g.DrawString("1-2", new Font("Arial", 20), Brushes.Black, 0, 700);

                    #region 初始化狀態
                    EnemyTimer.Enabled = true;      // 敵人開始生成
                    BulletTimer.Enabled = true;     // 玩家子彈生成器
                    if (VoiceEnable) soundBackground.controls.play();// 背景音樂 播放
                    GAME_START = true;
                    ENEMY_POOL_COUNT = 10; // 敵人數量

                    // 假如玩家存活 且 生命 > 0    
                    if (player.Life > 0)
                    {
                        player.Draw(g); // 顯示玩家圖片
                        // 子彈圖片 
                        DrawPlayerBullet(player.Guns);
                    }
                    #endregion
                    #region 魔王尚未生成時，僅生成一般敵人及特殊敵人
                    if (k > 1 && k < 998)
                    {
                        for (int i = 0; i < ListEnemys_Type.Count; i++)
                        {
                            // 畫出敵人
                            ListEnemys_Type[i].Draw(e);
                            // 當敵人達到一個高度範圍時，會發射子彈
                            if (ListEnemys_Type[i].Y > 50 && ListEnemys_Type[i].Y < 55 || ListEnemys_Type[i].Y > 100 && ListEnemys_Type[i].Y < 105)
                            {
                                if (ListEnemys_Type[i].EnemyType == 1 || ListEnemys_Type[i].EnemyType == 2)
                                {
                                    Bullet tempBullet = new Bullet(8, 10, 1, Direction.Down)
                                    {
                                        Number = i,
                                        X = ListEnemys_Type[i].X + ListEnemys_Type[i].Width / 2 - 12,
                                        Y = ListEnemys_Type[i].Y + ListEnemys_Type[i].Height / 2 + 5
                                    };
                                    tempBullet.Width -= 5;
                                    tempBullet.Height -= 5;
                                    EnemyBullets1.Add(tempBullet);
                                }
                            }

                            #region 當特殊敵人死亡時，道具生成, 添加到Item List
                            if (ListEnemys_Type[i].Special && ListEnemys_Type[i].ENEMY_DEATH_STATE)
                            {
                                GameItems tempItem = new GameItems(ListEnemys_Type[i].X, ListEnemys_Type[i].Y, r.Next(1, 3), 1, Direction.Down);
                                Items.Add(tempItem);
                            }
                            // 當道具和玩家進行碰撞時，PowerStatus true 、移除物件
                            // 沒有碰撞時，顯示道具
                            for (int j = 0; j < Items.Count; j++)
                            {
                                if (Items[j].GetRectangle().IntersectsWith(player.GetRectangle()))
                                {
                                    if (Items[j].Type == 1) { PowerStatus = true; PowerUpTime = 10; }
                                    if (Items[j].Type == 2) { ShiledStatus = true; ShiledTime = 10; }
                                    Items.Remove(Items[j]);
                                }
                                else
                                    Items[j].Draw(g);
                            }
                            #endregion
                        }
                        // 敵人子彈
                        for (int i = 0; i < EnemyBullets1.Count; i++)
                        {
                            if (EnemyBullets1[i].mFacus)
                            {
                                EnemyBullets1[i].Draw(g);
                            }
                        }
                    }
                    #endregion

                    #region 魔王生成
                    if (k >= 998 && k < 1013)
                    {
                        ListEnemys_Type.Clear();
                        EnemyBullets1.Clear();
                        boss1.Alive = true;
                        soundWarring.controls.play();
                        //soundBackground.controls.pause();
                        g.DrawImage(ImageManager.imgRed, 0, 170, 1050, 100);
                        g.DrawImage(ImageManager.imgWarning, 350, 190, 300, 67);
                        g.DrawImage(ImageManager.imgRedLight, 0, 110);
                    }
                    #endregion
                    #region 魔王攻擊
                    if (boss1.Alive)
                    {
                        soundBackground.controls.play();
                        boss1.Draw(g);
                        // 假如位置移到左邊或右邊
                        // 就發射子彈
                        // Boss 發射子彈
                        if (boss1.StartMove)
                        {
                            for (int i = 0; i < BossBullets1.Count; i++)
                                if (BossBullets1[i].mFacus == true)
                                    BossBullets1[i].DrawBullet(g);

                            for (int i = 0; i < BossBullets2.Count; i++)
                                if (BossBullets2[i].mFacus == true)
                                    BossBullets2[i].DrawBullet(g);

                            for (int i = 0; i < BossBullets3.Count; i++)
                                if (BossBullets3[i].mFacus == true)
                                    BossBullets3[i].DrawBullet(g);

                            for (int i = 0; i < BossBullets4.Count; i++)
                                if (BossBullets4[i].mFacus == true)
                                    BossBullets4[i].DrawBullet(g);

                            for (int i = 0; i < BossBullets5.Count; i++)
                                if (BossBullets5[i].mFacus == true)
                                    BossBullets5[i].DrawBullet(g);

                            for (int i = 0; i < BossBullets6.Count; i++)
                                if (BossBullets6[i].mFacus == true)
                                    BossBullets6[i].DrawBullet(g);
                        }
                    }
                    #endregion
                    #region 假如時間超過 1 分鐘及魔王死亡，就會達成勝利條件
                    if (k > 1070 && !boss1.Alive)
                    {
                        boss1.StartMove = false;

                        GAME_START = false;
                        ENEMY_POOL_COUNT = 0;   // 暫時取消敵人生成 
                        ListEnemys_Type.Clear();
                        BulletTimer.Enabled = false;
                        Items.Clear();
                        if (boss1.Y > -100)
                        {
                            //boss1.imgBoss.RotateFlip(RotateFlipType.Rotate180FlipY);
                            boss1.Y -= 10;
                            boss1.DrawRun(boss1.X, boss1.Y, g);
                        }
                        else
                        {
                            // 玩家往上飛, 在顯示勝利 !
                            if (player.Y > 0)
                            {
                                g.DrawImage(ImageManager.imgVictor, new Point(250, 50));
                                player.Y -= 15;
                                // 道具時間及狀態
                                PowerUpTime = 10;
                                ShiledTime = 10;
                                PowerStatus = false;
                                ShiledStatus = false;
                                if (!cougi)
                                {
                                    tempEarn = (int)iScore;
                                    tempEarn /= 100;
                                    tempEarn *= 2;
                                    iMoney += tempEarn;
                                    Earn = Earn + tempEarn;
                                    cougi = true;
                                }
                            }
                            else
                            {
                                TextCount = 0;
                                mState = GAME_STAGE3;
                            }
                        }

                        BossBullets1.Clear();
                        BossBullets2.Clear();
                        BossBullets3.Clear();
                        BossBullets4.Clear();
                        BossBullets5.Clear();
                        BossBullets6.Clear();
                    }
                    #endregion
                    #region 道具剩餘時間
                    if (ShiledStatus)
                    {
                        g.DrawImage(ImageManager.imgProtect, player.X - 15, player.Y - 20);
                        g.DrawImage(ImageManager.imgShiled, 10, 40, 30, 30);
                        g.DrawString("剩餘時間：" + ShiledTime, new Font("Arian", 18), Brushes.White, new Point(50, 40));

                    }
                    if (PowerStatus)
                    {
                        g.DrawImage(ImageManager.imgPowerUp, 10, 70, 32, 32);
                        g.DrawString("剩餘時間：" + PowerUpTime, new Font("Arian", 18), Brushes.White, new Point(50, 70));
                    }
                    #endregion
                    UIpaint(g);
                }
            }
            else if (mState == GAME_STAGE3)
            {
                if (VoiceEnable) soundMenu.controls.stop();
                Stage2_Start = false;
                g.DrawImage(ImageManager.imgGameBackground, mBitposY0);
                g.DrawImage(ImageManager.imgGameBackground, mBitposY1);
                if (!Stage3_Start)
                {
                    g.DrawImage(ImageManager.imgTextBackGround, new Point(0, 0));
                    if (TextCount > 1) g.DrawString(userName + "：剛有遇到一個比較龐大的機種，不曉得是不是他在帶領著他們無人偵察機。", new Font("標楷體", 20), new SolidBrush(Color.White), 0, 0);
                    if (TextCount > 1) g.DrawString("他剛剛被我打敗，但是他逃跑了，要追上去嗎？", new Font("標楷體", 20), new SolidBrush(Color.White), 0, 50);
                    if (TextCount > 6) g.DrawString("地球防衛組織：竟然他們都來到我們的地盤，怎麼能放過呢，追！", new Font("標楷體", 20), new SolidBrush(Color.White), 0, 150);
                    if (TextCount > 11) g.DrawString(userName + "：遵命！", new Font("標楷體", 20), new SolidBrush(Color.White), 0, 250);
                    if (TextCount > 16) g.DrawString("Press Click to Contiune !", new Font("Arial", 24), new SolidBrush(Color.White), 330, 700);
                    SetPlayerStatus();
                    boss1.Alive = false;
                    boss1.StartMove = false;
                    boss2.Alive = false;
                    k = 0;
                    // 分數歸零
                    iScore = 0;
                }
                if (Stage3_Start)
                {
                    g.DrawString("1-3", new Font("Arial", 20), Brushes.Black, 0, 700);

                    #region 初始化狀態
                    EnemyTimer.Enabled = true;      // 敵人開始生成
                    BulletTimer.Enabled = true;     // 玩家子彈生成器
                    if (VoiceEnable) soundBackground.controls.play();// 背景音樂 播放
                    GAME_START = true;
                    ENEMY_POOL_COUNT = 10; // 敵人數量

                    // 假如玩家存活 且 生命 > 0    
                    if (player.Life > 0)
                    {
                        player.Draw(g); // 顯示玩家圖片
                        // 子彈圖片 
                        DrawPlayerBullet(player.Guns);
                    }
                    #endregion
                    #region 魔王尚未生成時，僅生成一般敵人及特殊敵人
                    if (k > 1 && k < 998)
                    {
                        for (int i = 0; i < ListEnemys_Type.Count; i++)
                        {
                            // 畫出敵人
                            ListEnemys_Type[i].Draw(e);
                            // 當敵人達到一個高度範圍時，會發射子彈
                            if (ListEnemys_Type[i].Y > 50 && ListEnemys_Type[i].Y < 55 || ListEnemys_Type[i].Y > 100 && ListEnemys_Type[i].Y < 105)
                            {
                                if (ListEnemys_Type[i].EnemyType == 1 || ListEnemys_Type[i].EnemyType == 2)
                                {
                                    Bullet a1 = new Bullet(8, 10, 0, 70, Direction.DownLeft2);
                                    Bullet a2 = new Bullet(8, 10, 0, 60, Direction.Down);
                                    Bullet a3 = new Bullet(8, 10, 0, 70, Direction.DownRight2);
                                    a1.X = ListEnemys_Type[i].X + ListEnemys_Type[i].Width / 2 - 12; a2.X = a1.X; a3.X = a1.X;
                                    a1.Y = ListEnemys_Type[i].Y + ListEnemys_Type[i].Height / 2 + 5; a2.Y = a1.Y; a3.Y = a1.Y;
                                    a1.Width -= 7; a2.Width = a1.Width; a3.Width = a1.Width;
                                    a1.Height -= 7; a2.Height = a1.Height; a3.Height = a1.Height;
                                    EnemyBullets1.Add(a1);
                                    EnemyBullets1.Add(a2);
                                    EnemyBullets1.Add(a3);
                                }
                            }

                            #region 當特殊敵人死亡時，道具生成, 添加到Item List
                            if (ListEnemys_Type[i].Special && ListEnemys_Type[i].ENEMY_DEATH_STATE)
                            {
                                GameItems tempItem = new GameItems(ListEnemys_Type[i].X, ListEnemys_Type[i].Y, r.Next(1, 3), 1, Direction.Down);
                                Items.Add(tempItem);
                            }
                            // 當道具和玩家進行碰撞時，PowerStatus true 、移除物件
                            // 沒有碰撞時，顯示道具
                            for (int j = 0; j < Items.Count; j++)
                            {
                                if (Items[j].GetRectangle().IntersectsWith(player.GetRectangle()))
                                {
                                    if (Items[j].Type == 1) { PowerStatus = true; PowerUpTime = 10; }
                                    if (Items[j].Type == 2) { ShiledStatus = true; ShiledTime = 10; }
                                    Items.Remove(Items[j]);
                                }
                                else
                                    Items[j].Draw(g);
                            }
                            #endregion
                        }
                        // 敵人子彈
                        for (int i = 0; i < EnemyBullets1.Count; i++)
                        {
                            if (EnemyBullets1[i].mFacus)
                            {
                                EnemyBullets1[i].Draw(g);
                            }
                        }
                    }
                    #endregion
                    #region Warring 生成BOSS
                    if (k >= 998 && k < 1013)
                    {
                        ListEnemys_Type.Clear();
                        EnemyBullets1.Clear();
                        Items.Clear();
                        PowerStatus = false;
                        ShiledStatus = false;

                        boss2.Alive = true;

                        soundWarring.controls.play();
                        //soundBackground.controls.pause();
                        g.DrawImage(ImageManager.imgRed, 0, 170, 1050, 100);
                        g.DrawImage(ImageManager.imgWarning, 350, 190, 300, 67);
                        g.DrawImage(ImageManager.imgRedLight, 0, 115);


                    }
                    if (boss2.Alive)
                    {
                        Console.WriteLine("boss1.Alive = " + boss1.Alive.ToString());
                        Console.WriteLine("boss1.StartMove = " + boss1.StartMove.ToString());
                        Console.WriteLine("boss2.Alive = " + boss2.Alive.ToString());
                        Console.WriteLine("boss2.StartMove = " + boss2.StartMove.ToString());
                        soundBackground.controls.play();
                        boss2.Draw(g);

                        for (int i = 0; i < BossBullets1.Count; i++)
                            if (BossBullets1[i].mFacus == true)
                                BossBullets1[i].DrawBullet(g);
                        for (int i = 0; i < BossBullets2.Count; i++)
                            if (BossBullets2[i].mFacus == true)
                                BossBullets2[i].DrawBullet(g);
                        for (int i = 0; i < BossBullets3.Count; i++)
                            if (BossBullets3[i].mFacus == true)
                                BossBullets3[i].DrawBullet(g);
                        for (int i = 0; i < BossBullets4.Count; i++)
                            if (BossBullets4[i].mFacus == true)
                                BossBullets4[i].DrawBullet(g);
                        for (int i = 0; i < BossBullets5.Count; i++)
                            if (BossBullets5[i].mFacus == true)
                                BossBullets5[i].DrawBullet(g);
                        for (int i = 0; i < BossBullets6.Count; i++)
                            if (BossBullets6[i].mFacus == true)
                                BossBullets6[i].DrawBullet(g);
                        for (int i = 0; i < BossBullets7.Count; i++)
                            if (BossBullets7[i].mFacus == true)
                                BossBullets7[i].DrawBullet(g);
                        for (int i = 0; i < BossBullets8.Count; i++)
                            if (BossBullets8[i].mFacus == true)
                                BossBullets8[i].DrawBullet(g);
                        for (int i = 0; i < BossBullets9.Count; i++)
                            if (BossBullets9[i].mFacus == true)
                                BossBullets9[i].DrawBullet(g);
                        for (int i = 0; i < BossBullets10.Count; i++)
                            if (BossBullets10[i].mFacus == true)
                                BossBullets10[i].DrawBullet(g);


                        // 以下測試圓形子彈
                        for (int i = 0; i < BossBullets11.Count; i++)
                            if (BossBullets11[i].mFacus == true)
                                BossBullets11[i].DrawBullet(g);
                        for (int i = 0; i < BossBullets12.Count; i++)
                            if (BossBullets12[i].mFacus == true)
                                BossBullets12[i].DrawBullet(g);
                        for (int i = 0; i < BossBullets13.Count; i++)
                            if (BossBullets13[i].mFacus == true)
                                BossBullets13[i].DrawBullet(g);
                        for (int i = 0; i < BossBullets14.Count; i++)
                            if (BossBullets14[i].mFacus == true)
                                BossBullets14[i].DrawBullet(g);
                        for (int i = 0; i < BossBullets15.Count; i++)
                            if (BossBullets15[i].mFacus == true)
                                BossBullets15[i].DrawBullet(g);
                        for (int i = 0; i < BossBullets16.Count; i++)
                            if (BossBullets16[i].mFacus == true)
                                BossBullets16[i].DrawBullet(g);
                        for (int i = 0; i < BossBullets17.Count; i++)
                            if (BossBullets17[i].mFacus == true)
                                BossBullets17[i].DrawBullet(g);
                        for (int i = 0; i < BossBullets18.Count; i++)
                            if (BossBullets18[i].mFacus == true)
                                BossBullets18[i].DrawBullet(g);
                        for (int i = 0; i < BossBullets19.Count; i++)
                            if (BossBullets19[i].mFacus == true)
                                BossBullets19[i].DrawBullet(g);
                        for (int i = 0; i < BossBullets20.Count; i++)
                            if (BossBullets20[i].mFacus == true)
                                BossBullets20[i].DrawBullet(g);
                        for (int i = 0; i < BossBullets21.Count; i++)
                            if (BossBullets21[i].mFacus == true)
                                BossBullets21[i].DrawBullet(g);
                    }
                    #endregion
                    #region 假如魔王死亡，就會達成勝利條件
                    if (k > 1090 && !boss2.Alive)
                    {
                        
                        boss2.StartMove = false;
                        GAME_START = false;
                        ENEMY_POOL_COUNT = 0;   // 暫時取消敵人生成 
                        ListEnemys_Type.Clear();
                        BulletTimer.Enabled = false;
                        // 玩家往上飛, 在顯示勝利 !
                        if (player.Y > 0)
                        {
                            Items.Clear();
                            g.DrawImage(ImageManager.imgVictor, new Point(250, 50));
                            player.Y -= 15;
                            // 道具時間及狀態
                            PowerUpTime = 10;
                            ShiledTime = 10;
                            PowerStatus = false;
                            ShiledStatus = false;
                            if (!cougi)
                            {
                                tempEarn = (int)iScore;
                                tempEarn /= 100;
                                tempEarn *= 2;
                                iMoney += tempEarn;
                                Earn = Earn + tempEarn;
                                cougi = true;
                            }
                        }
                        else
                        {
                            mState = GAME_STORY;
                        }
                        BossBullets1.Clear();
                        BossBullets2.Clear();
                        BossBullets3.Clear();
                        BossBullets4.Clear();
                        BossBullets5.Clear();
                        BossBullets6.Clear();
                        BossBullets7.Clear();
                        BossBullets8.Clear();
                        BossBullets9.Clear();
                        BossBullets10.Clear();
                        BossBullets11.Clear();
                        BossBullets12.Clear();
                        BossBullets13.Clear();
                        BossBullets14.Clear();
                        BossBullets15.Clear();
                        BossBullets16.Clear();
                        BossBullets17.Clear();
                        BossBullets18.Clear();
                        BossBullets19.Clear();
                        BossBullets20.Clear();
                        BossBullets21.Clear();
                    }
                    #endregion
                    #region 道具剩餘時間
                    if (ShiledStatus)
                    {
                        g.DrawImage(ImageManager.imgProtect, player.X - 15, player.Y - 20);
                        g.DrawImage(ImageManager.imgShiled, 10, 40, 30, 30);
                        g.DrawString("剩餘時間：" + ShiledTime, new Font("Arian", 18), Brushes.White, new Point(50, 40));

                    }
                    if (PowerStatus)
                    {
                        g.DrawImage(ImageManager.imgPowerUp, 10, 70, 32, 32);
                        g.DrawString("剩餘時間：" + PowerUpTime, new Font("Arian", 18), Brushes.White, new Point(50, 70));
                    }
                    #endregion
                    UIpaint(g);
                }
            }
            else if (mState == GAME_SHOP)
            {
                /** 先得知玩家是哪種型態的飛機
                 * 展示目前有什麼型態的飛機
                 * 強化能力就可以改變外觀
                 */
                store.CheckType(player.PlanType); // 先取得玩家的飛機型態, 並把值丟入 Store 的變數
                store.DrawStore(g, storeType);

                UIpaint(g);
            }
            else if (mState == GAME_STORY)
            {
                Stage3_Start = false;
                g.DrawImage(ImageManager.imgTextBackGround, new Point(0, 0));
                if (TextCount > 1) g.DrawString(userName + "：說！你們是誰，來地球有什麼企圖！", new Font("標楷體", 20), new SolidBrush(Color.White), 0, 0);
                if (TextCount > 6) g.DrawString("外星人：我們是獵戶座集團，沒想到你們竟然這麼厲害 ...", new Font("標楷體", 20), new SolidBrush(Color.White), 0, 100);
                if (TextCount > 11) g.DrawString(userName + "：那當然，你以為我們地球人都是好欺負的嗎", new Font("標楷體", 20), new SolidBrush(Color.White), 0, 200);
                if (TextCount > 16) g.DrawString("外星人：可惡 ... 我會再回來報仇的 ！", new Font("標楷體", 20), new SolidBrush(Color.White), 0, 300);
                if (TextCount > 21) g.DrawString(userName + "：隨時奉陪！", new Font("標楷體", 20), new SolidBrush(Color.White), 0, 400);
                if (TextCount > 26) g.DrawString("由於地球防衛組織機敏的應戰，地球再次從星際危機之中恢復和平！", new Font("標楷體", 20), new SolidBrush(Color.White), 0, 500);
                if (TextCount > 30) g.DrawString("Mission Complete!", new Font("Arial", 30), new SolidBrush(Color.White), 330, 630);
                timer_counts = 0;
            }
            else if (mState == GAME_VICTOR) PaintGameResult(g);
            else if (mState == GAME_DEFEAT) PaintGameResult(g);
            else if (mState == GAME_LOADING) RunGrassBar(g);
            UpdateBackGround();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!GetLogin) SaveFile();
        }
        #endregion

        #region 初始化變數及聲音
        /// <summary>
        /// 初始化變數
        /// </summary>
        private void InitializeVariable()
        {
            mBitposY0.Y = 0;                // 背景圖1 設置座標
            mBitposY1.Y = -mScreenHeight;   // 背景圖2 設置座標

            mState = GAME_MENU;             // 遊戲選單
            //mState = GAME_START;           // 遊戲開始
            //mState = GAME_STORE;           // 遊戲商店
            //mState = GAME_VICTOR;
            //mState = GAME_DEFEAT;
            //mState = Enter_Effect;
            //mState = GAME_STAGE1;
            //mState = GAME_STAGE2;
            //mState = GAME_STAGE3;
            //mState = Story_End;

            BossBullets = 0; // boss1

            ENEMY_POOL_COUNT = 7; // 敵人數量 
            ListEnemys_Type.Clear();
            PlayerBullets1.Clear();
            PlayerBullets2.Clear();
            PlayerBullets3.Clear();
            PlayerBullets4.Clear();
            soundBackground.controls.stop();
            iScore = 0;

            for (int i = 0; i < meteorite.Length; i++)
            {
                meteorite[i] = new Meteorite
                {
                    Postion = new Point(r.Next(0, Width), r.Next(-500, -200))
                };
            }
        }

        /// <summary>
        /// 初始化聲音
        /// </summary>
        private void InitializeSound()
        {
            _ = Application.StartupPath;
            soundBackground.settings.autoStart = false; // 預設為 true
            soundBackground.URL = (Application.StartupPath + "/music/Stage1.wav");
            soundBackground.settings.volume = 10;

            soundExplode.settings.autoStart = false;
            soundExplode.URL = (Application.StartupPath + "/music/explosion0.wav");
            soundExplode.settings.volume = 10;
            soundExplode.settings.playCount = 1;

            soundShot.settings.autoStart = false;
            soundShot.URL = (Application.StartupPath + "/music/shot3.wav");
            soundShot.settings.volume = 10;
            soundShot.settings.playCount = 1;

            soundMenu.settings.autoStart = false;
            soundMenu.URL = (Application.StartupPath + "/music/Home.wav");
            soundMenu.settings.volume = 10;
            soundMenu.settings.playCount = 1;

            soundLose.settings.autoStart = false;
            soundLose.URL = (Application.StartupPath + "/music/GAMEOVER.mp3");
            soundLose.settings.volume = 10;
            soundLose.settings.playCount = 1;

            soundWarring.settings.autoStart = false;
            soundWarring.URL = (Application.StartupPath + "/music/SysAlert.wav");
            soundWarring.settings.volume = 10;
            soundWarring.settings.playCount = 1;
        }
        #endregion

        #region 計時器
        /// <summary>
        /// 遊戲運行的主Timer速度 
        /// </summary>
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (comboBox1.Visible) ShiledStatus = true;
            if (Stage1_Start || Stage2_Start || Stage3_Start)
            {
                if (player.Life <= 0) mState = GAME_DEFEAT;
                if (mState == GAME_STAGE2) boss1.Move();
                if (mState == GAME_STAGE3) boss2.Move();
            }
            if (GAME_START) // 玩家移動
            {
                k++;
                if (bUp)
                {
                    player.Y -= player.STEP;
                    if (player.Y <= 0) player.Y = 0;
                }
                if (bDown)
                {
                    player.Y += player.STEP;
                    if (player.Y > mScreenHeight - 115) player.Y = mScreenHeight - 115;
                }
                if (bLeft)
                {
                    player.X -= player.STEP;
                    if (player.X < 0) player.X = 0;
                }
                if (bRight)
                {
                    player.X += player.STEP;
                    if (player.X >= mScreenWidth - 115) player.X = mScreenWidth - 115;
                }
            }
            #region 道具效果
            if (PowerStatus && PowerUpTime > 0)
            {
                imgPlayerBullet = ImageManager.imgPlayerBullet;
                BulletTimer.Interval = 10;
                BULLET_POOL_COUNT = 50;
            }
            else
            {
                PowerStatus = false;
                PowerUpTime = 10;
                imgPlayerBullet = ImageManager.imgPlayerBullet;
                BulletTimer.Interval = iBulletTimer;
            }
            if (ShiledStatus && ShiledTime > 0)
            {

            }
            else
            {
                ShiledStatus = false;
                ShiledTime = 10;
            }

            #endregion

            Invalidate(); // 每60ms自動發生重新繪圖
        }
        // 假如想要讓子彈成一條直線，當他有狀態時，子彈的x = 玩家x
        /// <summary>
        /// 子彈生成器
        /// </summary>
        /// 假如子彈數量 小於 子彈總數量 , 假如現在時間 - 子彈生成時間 >= 子彈生成間隔
        /// 就新增一個子彈物件 A, 把 A 位置初始化,
        /// 把 A 加入至 集合(子彈們) , 子彈生成時間設為現在 , 子彈數量+1
        private void BulletTimer_Tick(object sender, EventArgs e)
        {
            if (GAME_START)
            {
                if (player.Guns == 0)
                {
                    if (mSendId < BULLET_POOL_COUNT)
                    {
                        Bullet tempBullet = new Bullet(imgPlayerBullet);
                        tempBullet.Init(player.X + BULLET_LEFT_OFFSET, player.Y + BULLET_UP_OFFSET);
                        PlayerBullets1.Add(tempBullet);
                        mSendId++;
                    }
                    else mSendId = 0;
                }
                else if (player.Guns == 1 || player.Guns == 2)
                {
                    if (mSendId < BULLET_POOL_COUNT)
                    {
                        Bullet tempBullet = new Bullet(imgPlayerBullet);
                        Bullet tempBullet2 = new Bullet(imgPlayerBullet);
                        tempBullet.Init(player.X + BULLET_LEFT_OFFSET - 20, player.Y + BULLET_UP_OFFSET);
                        tempBullet2.Init(player.X + BULLET_LEFT_OFFSET + 20, player.Y + BULLET_UP_OFFSET);
                        PlayerBullets1.Add(tempBullet);
                        PlayerBullets2.Add(tempBullet2);
                        mSendId++;
                    }
                    else
                    {
                        mSendId = 0;
                    }
                }
                else if (player.Guns == 3)
                {
                    if (mSendId < BULLET_POOL_COUNT)
                    {
                        Bullet tempBullet = new Bullet(imgPlayerBullet);
                        Bullet tempBullet2 = new Bullet(imgPlayerBullet);
                        Bullet tempBullet3 = new Bullet(imgPlayerBullet);
                        Bullet tempBullet4 = new Bullet(imgPlayerBullet);
                        tempBullet.Init(player.X + BULLET_LEFT_OFFSET - 13, player.Y + BULLET_UP_OFFSET);
                        tempBullet2.Init(player.X + BULLET_LEFT_OFFSET + 13, player.Y + BULLET_UP_OFFSET);
                        tempBullet3.Init(player.X + BULLET_LEFT_OFFSET - 35, player.Y + BULLET_UP_OFFSET + 20);
                        tempBullet4.Init(player.X + BULLET_LEFT_OFFSET + 35, player.Y + BULLET_UP_OFFSET + 20);
                        PlayerBullets1.Add(tempBullet);
                        PlayerBullets2.Add(tempBullet2);
                        PlayerBullets3.Add(tempBullet3);
                        PlayerBullets4.Add(tempBullet4);
                        mSendId++;
                    }
                    else
                        mSendId = 0;
                }
            }
        }

        /// <summary>
        /// 敵人生成器
        /// </summary>
        private void EnemyTimer_Tick(object sender, EventArgs e)
        {
            int rand = r.Next(0, 100);
            // 假如勝利條件不顯示 產生敵人
            if (GAME_START)
            {
                if (Stage1_Start)
                {
                    #region 新增一級敵人，不會發射子彈
                    if (rand < 97)
                    {
                        if (ListEnemys_Type.Count < ENEMY_POOL_COUNT)
                        {
                            CEnemy tempEnemy = new CEnemy(6, 16, Direction.Down);
                            tempEnemy.init(UtilRandom(0, mScreenWidth - tempEnemy.Width), 0);
                            ListEnemys_Type.Add(tempEnemy);
                        }
                    }
                    else
                    {
                        if (ListEnemys_Type.Count < ENEMY_POOL_COUNT)
                        {
                            CEnemy tempEnemy = new CEnemy(4, 6, Direction.Right);
                            tempEnemy.init(-100, r.Next(0, 100));
                            ListEnemys_Type.Add(tempEnemy);
                            tempEnemy.Special = true;
                        }
                    }

                    #endregion
                }
                if (Stage2_Start)
                {
                    #region 生成敵人
                    if (k > 1 && k < 998)
                    {
                        // 75% 小飛機 移速快、血量2
                        if (rand <= 75)
                        {
                            if (rand >= 0 && rand <= 50)
                            {
                                if (ListEnemys_Type.Count < ENEMY_POOL_COUNT)
                                {
                                    CEnemy tempEnemy = new CEnemy(6, 16, Direction.Down);
                                    tempEnemy.init(UtilRandom(0, mScreenWidth - tempEnemy.Width), 0);
                                    ListEnemys_Type.Add(tempEnemy);
                                }
                            }
                            if (rand >= 51 && rand <= 75)
                            {
                                if (ListEnemys_Type.Count < ENEMY_POOL_COUNT)
                                {
                                    CEnemy tempEnemy = new CEnemy(0, 8, Direction.Left);
                                    tempEnemy.init(1200, r.Next(200, 550));
                                    ListEnemys_Type.Add(tempEnemy);
                                }
                            }
                        }
                        // 15% 中飛機 移速慢、血量4
                        else if (rand > 75 && rand <= 90)
                        {
                            if (ListEnemys_Type.Count < ENEMY_POOL_COUNT)
                            {
                                CEnemy tempEnemy = new CEnemy(2, 3, Direction.Down);
                                tempEnemy.init(UtilRandom(0, mScreenWidth - tempEnemy.Width), 0);
                                ListEnemys_Type.Add(tempEnemy);
                            }
                        }
                        // 7% 小飛機 移速極快、血量1
                        else if (rand > 90 && rand <= 97)
                        {
                            if (ListEnemys_Type.Count < ENEMY_POOL_COUNT)
                            {
                                CEnemy tempEnemy = new CEnemy(1, 8, Direction.Down);
                                tempEnemy.init(UtilRandom(0, mScreenWidth - tempEnemy.Width), 0);
                                ListEnemys_Type.Add(tempEnemy);
                            }
                        }
                        // 3% 特殊飛機 掉落道具
                        else if (rand > 97 && rand <= 100)
                        {
                            if (ListEnemys_Type.Count < ENEMY_POOL_COUNT)
                            {
                                CEnemy tempEnemy = new CEnemy(4, 6, Direction.Right);
                                tempEnemy.init(-100, r.Next(0, 100));
                                ListEnemys_Type.Add(tempEnemy);
                                tempEnemy.Special = true;
                            }
                        }
                    }
                    #endregion
                }
                if (Stage3_Start)
                {
                    #region 生成敵人
                    if (k > 1 && k < 998)
                    {
                        // 75% 小飛機 移速快、血量2
                        if (rand <= 75)
                        {
                            if (rand >= 0 && rand <= 50)
                            {
                                if (ListEnemys_Type.Count < ENEMY_POOL_COUNT)
                                {
                                    CEnemy tempEnemy = new CEnemy(6, 20, Direction.Down);
                                    tempEnemy.init(UtilRandom(0, mScreenWidth - tempEnemy.Width), 0);
                                    ListEnemys_Type.Add(tempEnemy);
                                }
                            }
                            if (rand >= 51 && rand <= 75)
                            {
                                if (ListEnemys_Type.Count < ENEMY_POOL_COUNT)
                                {
                                    CEnemy tempEnemy = new CEnemy(0, 8, Direction.Left);
                                    tempEnemy.init(1200, r.Next(200, 550));
                                    ListEnemys_Type.Add(tempEnemy);
                                }
                            }
                        }
                        // 15% 中飛機 移速慢、血量4
                        else if (rand > 75 && rand <= 90)
                        {
                            if (ListEnemys_Type.Count < ENEMY_POOL_COUNT)
                            {
                                CEnemy tempEnemy = new CEnemy(2, 3, Direction.Down);
                                tempEnemy.init(UtilRandom(0, mScreenWidth - tempEnemy.Width), 0);
                                ListEnemys_Type.Add(tempEnemy);
                            }
                        }
                        // 7% 小飛機 移速極快、血量1
                        else if (rand > 90 && rand <= 97)
                        {
                            if (ListEnemys_Type.Count < ENEMY_POOL_COUNT)
                            {
                                CEnemy tempEnemy = new CEnemy(1, 8, Direction.Down);
                                tempEnemy.init(UtilRandom(0, mScreenWidth - tempEnemy.Width), 0);
                                ListEnemys_Type.Add(tempEnemy);
                            }
                        }
                        // 3% 特殊飛機 掉落道具
                        else if (rand > 97 && rand <= 100)
                        {
                            if (ListEnemys_Type.Count < ENEMY_POOL_COUNT)
                            {
                                CEnemy tempEnemy = new CEnemy(4, 6, Direction.Right);
                                tempEnemy.init(-100, r.Next(0, 100));
                                ListEnemys_Type.Add(tempEnemy);
                                tempEnemy.Special = true;
                            }
                        }
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// Boss 子彈移動、TextCount 顯示故事計時用
        /// </summary>
        private void BossBulletTimer_Tick(object sender, EventArgs e)
        {
            if (mState == GAME_STAGE1) TextCount++;
            if (mState == GAME_STAGE2)
            {
                TextCount++;
                if (boss1.AddBullet == true)
                {
                    BossBullets = 0;
                    boss1.AddBullet = false;
                }
                if (boss1.StartMove)
                {
                    if (BossBullets < 7)
                    {
                        boss1.LevelMove = false;
                        Bullet bullet = new Bullet(9, 15, 0, 30, Direction.DownLeft2);
                        Bullet bullet1 = new Bullet(9, 15, 0, 30, Direction.Down);
                        Bullet bullet2 = new Bullet(9, 15, 0, 30, Direction.DownRight2);
                        Bullet bullet3 = new Bullet(9, 15, 0, 30, Direction.DownLeft2);
                        Bullet bullet4 = new Bullet(9, 15, 0, 30, Direction.Down);
                        Bullet bullet5 = new Bullet(9, 15, 0, 30, Direction.DownRight2);
                        bullet.Init(boss1.Postion.X, boss1.Postion.Y + 50);
                        BossBullets1.Add(bullet);
                        bullet1.Init(boss1.Postion.X, boss1.Postion.Y + 50);
                        BossBullets2.Add(bullet1);
                        bullet2.Init(boss1.Postion.X, boss1.Postion.Y + 50);
                        BossBullets3.Add(bullet2);

                        bullet3.Init(boss1.Postion.X + 230, boss1.Postion.Y + 50);
                        BossBullets4.Add(bullet3);
                        bullet4.Init(boss1.Postion.X + 230, boss1.Postion.Y + 50);
                        BossBullets5.Add(bullet4);
                        bullet5.Init(boss1.Postion.X + 230, boss1.Postion.Y + 50);
                        BossBullets6.Add(bullet5);
                        BossBullets++;
                    }
                    if (BossBullets == 7) boss1.LevelMove = true;
                }
            }
            if (mState == GAME_STAGE3)
            {
                TextCount++;

                if (boss2.AddBullet == true)
                {
                    BossBullets = 0;
                    boss2.AddBullet = false;
                }
                if (boss2.StartMove)
                {
                    // 假如魔王子彈數 少於 10 ，就增加子彈
                    if (BossBullets < 10)
                    {
                        boss2.LevelMove = false;

                        Bullet bullet1 = new Bullet(9, 15, 0, 36, Direction.DownRight2);
                        Bullet bullet2 = new Bullet(9, 15, 0, 72, Direction.DownRight2);
                        Bullet bullet3 = new Bullet(9, 15, 0, 108, Direction.DownRight2);
                        Bullet bullet4 = new Bullet(9, 15, 0, 144, Direction.DownRight2);
                        Bullet bullet5 = new Bullet(9, 15, 0, 180, Direction.DownRight2);

                        Bullet bullet6 = new Bullet(9, 15, 0, 216, Direction.DownRight2);
                        Bullet bullet7 = new Bullet(9, 15, 0, 252, Direction.DownRight2);
                        Bullet bullet8 = new Bullet(9, 15, 0, 288, Direction.DownRight2);
                        Bullet bullet9 = new Bullet(9, 15, 0, 324, Direction.DownRight2);
                        Bullet bullet10 = new Bullet(9, 15, 0, 360, Direction.DownRight2);



                        Bullet bullet11 = new Bullet(10, 8, 0, 36, Direction.Up);
                        Bullet bullet12 = new Bullet(10, 8, 0, 72, Direction.Up);
                        Bullet bullet13 = new Bullet(10, 8, 0, 108, Direction.Up);
                        Bullet bullet14 = new Bullet(10, 8, 0, 144, Direction.Up);
                        Bullet bullet15 = new Bullet(10, 8, 0, 180, Direction.Up);
                        Bullet bullet16 = new Bullet(10, 8, 0, 216, Direction.Up);
                        Bullet bullet17 = new Bullet(10, 8, 0, 252, Direction.Up);
                        Bullet bullet18 = new Bullet(10, 8, 0, 288, Direction.Up);
                        Bullet bullet19 = new Bullet(10, 8, 0, 324, Direction.Up);
                        Bullet bullet20 = new Bullet(10, 8, 0, 360, Direction.Up); // 剛建立時角度為 0 度
                        Bullet bullet21 = new Bullet(10, 8, 0, 0, Direction.Up); // 剛建立時角度為 0 度

                        bullet1.Init(boss2.Postion.X + (boss2.Width / 2) + 20, boss2.Postion.Y + (boss2.Height / 2));
                        BossBullets1.Add(bullet1);
                        bullet2.Init(boss2.Postion.X + (boss2.Width / 2) + 20, boss2.Postion.Y + (boss2.Height / 2));
                        BossBullets2.Add(bullet2);
                        bullet3.Init(boss2.Postion.X + (boss2.Width / 2) + 20, boss2.Postion.Y + (boss2.Height / 2));
                        BossBullets3.Add(bullet3);
                        bullet4.Init(boss2.Postion.X + (boss2.Width / 2) + 20, boss2.Postion.Y + (boss2.Height / 2));
                        BossBullets4.Add(bullet4);
                        bullet5.Init(boss2.Postion.X + (boss2.Width / 2) + 20, boss2.Postion.Y + (boss2.Height / 2));
                        BossBullets5.Add(bullet5);
                        bullet6.Init(boss2.Postion.X + (boss2.Width / 2) + 20, boss2.Postion.Y + (boss2.Height / 2));
                        BossBullets6.Add(bullet6);
                        bullet7.Init(boss2.Postion.X + (boss2.Width / 2) + 20, boss2.Postion.Y + (boss2.Height / 2));
                        BossBullets7.Add(bullet7);
                        bullet8.Init(boss2.Postion.X + (boss2.Width / 2) + 20, boss2.Postion.Y + (boss2.Height / 2));
                        BossBullets8.Add(bullet8);
                        bullet9.Init(boss2.Postion.X + (boss2.Width / 2) + 20, boss2.Postion.Y + (boss2.Height / 2));
                        BossBullets9.Add(bullet9);
                        bullet10.Init(boss2.Postion.X + (boss2.Width / 2) + 20, boss2.Postion.Y + (boss2.Height / 2));
                        BossBullets10.Add(bullet10);

                        //子彈初始化, 總共有20顆子彈
                        bullet11.Init(0, 800);
                        BossBullets11.Add(bullet11);
                        bullet12.Init(97, 850);
                        BossBullets12.Add(bullet12);
                        bullet13.Init(194, 900);
                        BossBullets13.Add(bullet13);
                        bullet14.Init(291, 950);
                        BossBullets14.Add(bullet14);
                        bullet15.Init(388, 1000);
                        BossBullets15.Add(bullet15);
                        bullet16.Init(485, 1000);
                        BossBullets16.Add(bullet16);
                        bullet17.Init(582, 950);
                        BossBullets17.Add(bullet17);
                        bullet18.Init(679, 900);
                        BossBullets18.Add(bullet18);
                        bullet19.Init(776, 850);
                        BossBullets19.Add(bullet19);
                        bullet20.Init(873, 800);
                        BossBullets20.Add(bullet20);
                        bullet21.Init(970, 800);
                        BossBullets21.Add(bullet21);

                        BossBullets++;
                    }
                    if (BossBullets == 10) boss2.LevelMove = true;
                }

            }
            if (mState == GAME_STORY) TextCount++;
        }

        /// <summary>
        /// 遊戲時間 計時器
        /// </summary>
        private void TimeNow_Tick(object sender, EventArgs e)
        {
            if (PowerStatus) PowerUpTime--;
            if (ShiledStatus) ShiledTime--;
            if (GAME_START) // 當按下開始
            {
                if (Stage1_Start || Stage2_Start || Stage3_Start)
                {
                    timer_counts++;

                    if (timer_counts >= 60)
                    {
                        minutes++;
                        timer_counts = 0;
                    }

                    second = timer_counts;
                }
            }
        }
        #endregion

        #region 自行設計的函數
        /// <summary>
        /// 圖片縮放
        /// </summary>
        /// 檔案大吃記憶體, 建議依照比例直接把圖片改小, 這樣整個專案檔案也比較小
        /// <param name="path">圖片路徑</param>
        /// <param name="posX">X座標</param>
        /// <param name="posY">Y座標</param>
        /// <param name="x">圖片寬度縮放倍率</param>
        /// <param name="y">圖片高度縮放倍率</param>
        public void ScalePic(String path, float posX, float posY, float x, float y)
        {
            Image img = Image.FromFile(path);
            int width = img.Width;
            int height = img.Height;
            RectangleF destinationRect = new RectangleF(posX, posY, x * width, y * height);
            RectangleF sourceRect = new RectangleF(0, 0, 1f * width, 1f * height);
            g.DrawImage(img, destinationRect, sourceRect, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// 畫出金錢數字
        /// </summary>
        private void DrawMoney()
        {
            g.DrawImage(ImageManager.imgCoin, new Point(700, 5));
            int iMoney_One = (int)(iMoney) % 10;
            int iMoney_Two = (int)(iMoney / 10) % 10;
            int iMoney_Three = (int)(iMoney / 100) % 10;
            int iMoney_Four = (int)(iMoney / 1000) % 10;
            int iMoney_Five = (int)(iMoney / 10000) % 10;
            g.DrawImage(DrawMoneyNumber(iMoney_Five), new Point(739, 5));
            g.DrawImage(DrawMoneyNumber(iMoney_Four), new Point(771, 5));
            g.DrawImage(DrawMoneyNumber(iMoney_Three), new Point(803, 5));
            g.DrawImage(DrawMoneyNumber(iMoney_Two), new Point(835, 5));
            g.DrawImage(DrawMoneyNumber(iMoney_One), new Point(867, 5));
        }

        /// <summary>
        /// 更新背景圖片
        /// <para>子彈移動</para>
        /// 子彈碰撞偵測
        /// <para>檢查子彈是否超出螢幕</para>
        /// </summary>
        public void UpdateBackGround()
        {
            mBitposY0.Y += 8;
            mBitposY1.Y += 8;
            if (mBitposY0.Y == mScreenHeight) { mBitposY0.Y = -mScreenHeight; }
            if (mBitposY1.Y == mScreenHeight) { mBitposY1.Y = -mScreenHeight; }

            // 玩家子彈
            if (player.Guns == 0)
            {
                for (int i = 0; i < PlayerBullets1.Count; i++)
                {

                    #region 子彈和敵人 碰撞偵測
                    if (GAME_START) { PlayerBullets1[i].Collision(ListEnemys_Type); }
                    #endregion
                    // 子彈超出視窗 移除
                    if (PlayerBullets1[i].Y <= 0) PlayerBullets1.RemoveAt(i);
                }
            }
            else if (player.Guns == 1 || player.Guns == 2)
            {
                for (int i = 0; i < PlayerBullets1.Count; i++)
                {

                    #region 子彈和敵人 碰撞偵測
                    if (GAME_START) { PlayerBullets1[i].Collision(ListEnemys_Type); }

                    #endregion
                    // 子彈超出視窗 移除
                    if (PlayerBullets1[i].Y <= 0) PlayerBullets1.RemoveAt(i);
                }
                for (int i = 0; i < PlayerBullets2.Count; i++)
                {

                    #region 子彈和敵人 碰撞偵測
                    if (GAME_START) { PlayerBullets2[i].Collision(ListEnemys_Type); }
                    #endregion
                    // 子彈超出視窗 移除
                    if (PlayerBullets2[i].Y <= 0) PlayerBullets2.RemoveAt(i);
                }
            }
            else if (player.Guns == 3)
            {
                for (int i = 0; i < PlayerBullets1.Count; i++)
                {
                    #region 子彈和敵人 碰撞偵測
                    if (GAME_START) { PlayerBullets1[i].Collision(ListEnemys_Type); }
                    #endregion

                    // 子彈超出視窗 移除
                    if (PlayerBullets1[i].Y <= 0) PlayerBullets1.RemoveAt(i);
                }
                for (int i = 0; i < PlayerBullets2.Count; i++)
                {
                    #region 子彈和敵人 碰撞偵測
                    if (GAME_START) { PlayerBullets2[i].Collision(ListEnemys_Type); }
                    #endregion
                    // 子彈超出視窗 移除
                    if (PlayerBullets2[i].Y <= 0) PlayerBullets2.RemoveAt(i);
                }
                for (int i = 0; i < PlayerBullets3.Count; i++)
                {
                    #region 子彈和敵人 碰撞偵測
                    if (GAME_START) { PlayerBullets3[i].Collision(ListEnemys_Type); }
                    #endregion
                    // 子彈超出視窗 移除
                    if (PlayerBullets3[i].Y <= 0) PlayerBullets3.RemoveAt(i);
                }
                for (int i = 0; i < PlayerBullets4.Count; i++)
                {
                    #region 子彈和敵人 碰撞偵測
                    if (GAME_START) { PlayerBullets4[i].Collision(ListEnemys_Type); }
                    #endregion
                    // 子彈超出視窗 移除
                    if (PlayerBullets4[i].Y <= 0) PlayerBullets4.RemoveAt(i);
                }
            }

            /** 持續確認敵人狀態並移除
             * 當敵人狀態 = 敵人死亡 、 
             * 敵人超出螢幕邊界 則移除
             */
            if (Stage1_Start)
            {
                // 敵人假如 1.超出螢幕或者 2.狀態為死亡 就移除
                for (int i = 0; i < ListEnemys_Type.Count; i++)
                {
                    // 敵人和玩家的碰撞偵測
                    if (ListEnemys_Type[i].GetRectangle().IntersectsWith(player.GetRectangle()))
                    {
                        if (!ShiledStatus)
                            player.Life -= 1;
                    }
                    // 假如敵人 超出視窗 或 狀態為死亡 就把 敵人移除
                    if (ListEnemys_Type[i].Y >= mScreenHeight || ListEnemys_Type[i].ENEMY_DEATH_STATE)
                    {
                        if (ListEnemys_Type[i].Special)
                            SpecialEnemy = true;
                        ListEnemys_Type.Remove(ListEnemys_Type[i]);
                    }
                }
                // 當達到條件，給予隕石新的座標
                for (int i = 0; i < meteorite.Length; i++)
                {
                    if (k == 250 || k == 500 || k == 750)
                    {
                        meteorite[i].Postion = new Point(r.Next(0, 1000), r.Next(-500, -200));
                    }

                    if (k > 250)
                    {
                        if (meteorite[i].GetRectangle().IntersectsWith(player.GetRectangle()))
                        {
                            if (!ShiledStatus)
                                player.Life -= 1;
                        }
                    }
                }
            }
            if (Stage2_Start)
            {
                #region 道具
                // 道具和玩家的碰撞偵測
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i].GetRectangle().IntersectsWith(player.GetRectangle()))
                    {
                        if (Items[i].Type == 1) PowerStatus = true;
                        if (Items[i].Type == 2) ShiledStatus = true;
                    }
                }
                #endregion
                #region 小飛機
                // 假如敵人移動到一個高度時，把當下的座標給予子彈，並等待幾秒後繼續移動
                // 2級敵人持續移動並給予子彈相對的座標
                if (!boss1.StartMove)
                {
                    for (int i = 0; i < ListEnemys_Type.Count; i++)
                    {
                        // 敵人和玩家的碰撞偵測
                        if (ListEnemys_Type[i].GetRectangle().IntersectsWith(player.GetRectangle()))
                        {
                            if (!ShiledStatus)
                                player.Life -= 1;
                        }
                        // 假如敵人 超出視窗 或 狀態為死亡 就把 敵人移除
                        if (ListEnemys_Type[i].Y >= mScreenHeight || ListEnemys_Type[i].ENEMY_DEATH_STATE)
                        {
                            if (ListEnemys_Type[i].Special)
                                SpecialEnemy = true;
                            ListEnemys_Type.Remove(ListEnemys_Type[i]);
                        }
                    }
                }
                // 敵人子彈 和 玩家 碰撞檢測
                for (int i = 0; i < EnemyBullets1.Count; i++)
                {
                    if (EnemyBullets1[i].GetRectangle().IntersectsWith(player.GetRectangle()))
                    {
                        if (!ShiledStatus)
                            player.Life -= 1;
                    }
                }
                #endregion
                #region BOSS
                if (boss1.StartMove && boss1.Alive)
                {
                    for (int i = 0; i < BossBullets1.Count; i++)
                    {
                        BossBullets1[i].UpdateCrossLeft();
                        BossBullets2[i].UpdateCrossRight();
                        BossBullets3[i].UpdateCrossStri();
                        BossBullets4[i].UpdateCrossLeft();
                        BossBullets5[i].UpdateCrossRight();
                        BossBullets6[i].UpdateCrossStri();
                        if (BossBullets1[i].Y > 770) BossBullets1.Remove(BossBullets1[i]);
                        if (BossBullets2[i].Y > 770) BossBullets2.Remove(BossBullets2[i]);
                        if (BossBullets3[i].Y > 770) BossBullets3.Remove(BossBullets3[i]);
                        if (BossBullets4[i].Y > 770) BossBullets4.Remove(BossBullets4[i]);
                        if (BossBullets5[i].Y > 770) BossBullets5.Remove(BossBullets5[i]);
                        if (BossBullets6[i].Y > 770) BossBullets6.Remove(BossBullets6[i]);
                        // 魔王子彈和 玩家碰撞偵測
                        if (BossBullets1[i].GetRectangle().IntersectsWith(player.GetRectangle())) if (!ShiledStatus) player.Life -= 1;
                        if (BossBullets2[i].GetRectangle().IntersectsWith(player.GetRectangle())) if (!ShiledStatus) player.Life -= 1;
                        if (BossBullets3[i].GetRectangle().IntersectsWith(player.GetRectangle())) if (!ShiledStatus) player.Life -= 1;
                        if (BossBullets4[i].GetRectangle().IntersectsWith(player.GetRectangle())) if (!ShiledStatus) player.Life -= 1;
                        if (BossBullets5[i].GetRectangle().IntersectsWith(player.GetRectangle())) if (!ShiledStatus) player.Life -= 1;
                        if (BossBullets6[i].GetRectangle().IntersectsWith(player.GetRectangle())) if (!ShiledStatus) player.Life -= 1;
                    }
                    // 魔王和 玩家子彈 碰撞偵測
                    boss1.Collision(PlayerBullets1);
                    boss1.Collision(PlayerBullets2);
                    boss1.Collision(PlayerBullets3);
                    boss1.Collision(PlayerBullets4);

                    //魔王和玩家碰撞偵測
                    if (boss1.GetRectangle().IntersectsWith(player.GetRectangle()))
                        if (!ShiledStatus) player.Life -= 1;
                }
                #endregion
            }
            if (Stage3_Start)
            {
                #region 道具
                // 道具和玩家的碰撞偵測
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i].GetRectangle().IntersectsWith(player.GetRectangle()))
                    {
                        if (Items[i].Type == 1) PowerStatus = true;
                        if (Items[i].Type == 2) ShiledStatus = true;
                    }
                }
                #endregion
                #region 小飛機
                // 假如敵人移動到一個高度時，把當下的座標給予子彈，並等待幾秒後繼續移動
                // 2級敵人持續移動並給予子彈相對的座標
                if (!boss2.StartMove)
                {
                    for (int i = 0; i < ListEnemys_Type.Count; i++)
                    {
                        // 敵人和玩家的碰撞偵測
                        if (ListEnemys_Type[i].GetRectangle().IntersectsWith(player.GetRectangle()))
                        {
                            if (!ShiledStatus) player.Life -= 1;
                        }
                        // 假如敵人 超出視窗 或 狀態為死亡 就把 敵人移除
                        if (ListEnemys_Type[i].Y >= mScreenHeight || ListEnemys_Type[i].ENEMY_DEATH_STATE)
                        {
                            if (ListEnemys_Type[i].Special) SpecialEnemy = true;
                            ListEnemys_Type.Remove(ListEnemys_Type[i]);
                        }
                    }
                }
                // 敵人子彈 和 玩家 碰撞檢測
                for (int i = 0; i < EnemyBullets1.Count; i++)
                {
                    if (EnemyBullets1[i].GetRectangle().IntersectsWith(player.GetRectangle()))
                    {
                        if (!ShiledStatus)
                            player.Life -= 1;
                    }
                }
                #endregion
                #region BOSS
                if (boss2.StartMove && boss2.Alive)
                {
                    for (int i = 0; i < BossBullets1.Count; i++)
                    {
                        BossBullets1[i].DownRight2();
                        if (BossBullets1[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈1"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets1[i].Y > 770) BossBullets1.Remove(BossBullets1[i]);
                    }
                    for (int i = 0; i < BossBullets2.Count; i++)
                    {
                        BossBullets2[i].DownRight2();
                        if (BossBullets2[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈2"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets2[i].Y > 770) BossBullets2.Remove(BossBullets2[i]);
                    }
                    for (int i = 0; i < BossBullets3.Count; i++)
                    {
                        BossBullets3[i].DownRight2();
                        if (BossBullets3[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈3"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets3[i].Y > 770) BossBullets3.Remove(BossBullets3[i]);
                    }
                    for (int i = 0; i < BossBullets4.Count; i++)
                    {
                        BossBullets4[i].DownRight2();
                        if (BossBullets4[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈4"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets4[i].Y > 770) BossBullets4.Remove(BossBullets4[i]);
                    }
                    for (int i = 0; i < BossBullets5.Count; i++)
                    {
                        BossBullets5[i].DownRight2();
                        if (BossBullets5[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈5"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets5[i].Y > 770) BossBullets5.Remove(BossBullets5[i]);
                    }
                    for (int i = 0; i < BossBullets6.Count; i++)
                    {
                        BossBullets6[i].DownRight2();
                        if (BossBullets6[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈6"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets6[i].Y > 770) BossBullets6.Remove(BossBullets6[i]);
                    }
                    for (int i = 0; i < BossBullets7.Count; i++)
                    {
                        BossBullets7[i].DownRight2();
                        if (BossBullets7[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈7"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets7[i].Y > 770) BossBullets7.Remove(BossBullets7[i]);
                    }
                    for (int i = 0; i < BossBullets8.Count; i++)
                    {
                        BossBullets8[i].DownRight2();
                        if (BossBullets8[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈8"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets8[i].Y > 770) BossBullets8.Remove(BossBullets8[i]);
                    }
                    for (int i = 0; i < BossBullets9.Count; i++)
                    {
                        BossBullets9[i].DownRight2();
                        if (BossBullets9[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈9"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets9[i].Y > 770) BossBullets9.Remove(BossBullets9[i]);
                    }
                    for (int i = 0; i < BossBullets10.Count; i++)
                    {
                        BossBullets10[i].DownRight2();
                        if (BossBullets10[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈10"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets10[i].Y > 770) BossBullets10.Remove(BossBullets10[i]);
                    }



                    for (int i = 0; i < BossBullets11.Count; i++)
                    {
                        BossBullets11[i].Up();
                        if (BossBullets11[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈11"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets11[i].Y < 0) BossBullets11.Remove(BossBullets11[i]);
                    }
                    for (int i = 0; i < BossBullets12.Count; i++)
                    {
                        BossBullets12[i].Up();
                        if (BossBullets12[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈12"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets12[i].Y < 0) BossBullets12.Remove(BossBullets12[i]);
                    }
                    for (int i = 0; i < BossBullets13.Count; i++)
                    {
                        BossBullets13[i].Up();
                        if (BossBullets13[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈13"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets13[i].Y < 0) BossBullets13.Remove(BossBullets13[i]);
                    }
                    for (int i = 0; i < BossBullets14.Count; i++)
                    {
                        BossBullets14[i].Up();
                        if (BossBullets14[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈14"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets14[i].Y < 0) BossBullets14.Remove(BossBullets14[i]);
                    }
                    for (int i = 0; i < BossBullets15.Count; i++)
                    {
                        BossBullets15[i].Up();
                        if (BossBullets15[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈15"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets15[i].Y < 0) BossBullets15.Remove(BossBullets15[i]);
                    }
                    for (int i = 0; i < BossBullets16.Count; i++)
                    {
                        BossBullets16[i].Up();
                        if (BossBullets16[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈16"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets16[i].Y < 0) BossBullets16.Remove(BossBullets16[i]);
                    }
                    for (int i = 0; i < BossBullets17.Count; i++)
                    {
                        BossBullets17[i].Up();
                        if (BossBullets17[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈17"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets17[i].Y < 0) BossBullets17.Remove(BossBullets17[i]);
                    }
                    for (int i = 0; i < BossBullets18.Count; i++)
                    {
                        BossBullets18[i].Up();
                        if (BossBullets18[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈18"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets18[i].Y < 0) BossBullets18.Remove(BossBullets18[i]);
                    }
                    for (int i = 0; i < BossBullets19.Count; i++)
                    {
                        BossBullets19[i].Up();
                        if (BossBullets19[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈19"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets19[i].Y < 0) BossBullets19.Remove(BossBullets19[i]);
                    }
                    for (int i = 0; i < BossBullets20.Count; i++)
                    {
                        BossBullets20[i].Up();
                        if (BossBullets20[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈20"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets20[i].Y < 0) BossBullets20.Remove(BossBullets20[i]);
                    }

                    for (int i = 0; i < BossBullets21.Count; i++)
                    {
                        BossBullets21[i].Up();
                        if (BossBullets21[i].GetRectangle().IntersectsWith(player.GetRectangle())) { Console.WriteLine("被王子彈21"); if (!ShiledStatus) player.Life -= 1; }
                        if (BossBullets21[i].Y < 0) BossBullets21.Remove(BossBullets21[i]);
                    }
                    // 魔王和 玩家子彈 碰撞偵測
                    boss2.Collision(PlayerBullets1);
                    boss2.Collision(PlayerBullets2);
                    boss2.Collision(PlayerBullets3);
                    boss2.Collision(PlayerBullets4);
                    //魔王和玩家碰撞偵測
                    if (boss2.GetRectangle().IntersectsWith(player.GetRectangle()))
                    {
                        if (!ShiledStatus) player.Life -= 1;
                        Console.WriteLine("被王碰瓷了");
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// 隨機返回敵人 X 座標 
        /// </summary>
        private int UtilRandom(int bottom, int top)
        {
            return ((Math.Abs(new Random().Next()) % (top - bottom)) + bottom);
        }

        /// <summary>
        /// 繪製UI介面
        /// </summary>
        private void UIpaint(Graphics g)
        {
            if (mState == GAME_STAGE1 || mState == GAME_STAGE2 || mState == GAME_STAGE3)
            {
                // 繪製生命
                g.DrawString("生命", new Font("Arial", 22), new SolidBrush(Color.White), 0, 0);
                //g.DrawString(player.Life.ToString(), new Font("Arial", 18), new SolidBrush(Color.White), 60, 0);
                g.DrawImage(player.CheckPlanType(), 70, 0, 66, 54);


                // 繪製時間
                String str1, str2;
                if (minutes < 10)
                {
                    str1 = "0" + minutes.ToString();
                }
                else
                {
                    str1 = minutes.ToString();
                }

                if (second < 10)
                {
                    str2 = "0" + second.ToString();
                }
                else
                {
                    str2 = second.ToString();
                }

                g.DrawString("時間 " + str1 + ":" + str2, new Font("Arial", 22), new SolidBrush(Color.White), new Point(220, 0));

                // 繪製分數 
                sScore = iScore.ToString();
                g.DrawString("分數", new Font("Arial", 22), new SolidBrush(Color.White), 420, 0);
                g.DrawString(sScore, new Font("Arial", 22), new SolidBrush(Color.White), 490, 0);

                // 暫停按鈕
                g.DrawImage(ImageManager.imgPause, PauseButton_X, PauseButton_Y, ImageManager.imgPause.Width, ImageManager.imgPause.Height);

                // 繪製金錢
                DrawMoney();
            }
            if (mState == GAME_SHOP)
            {
                // 繪製金錢
                DrawMoney();
                // 返回按鈕
                g.DrawImage(ImageManager.imgReturn, PauseButton_X, PauseButton_Y, ImageManager.imgReturn.Width, ImageManager.imgReturn.Height);
            }
            else if (mState == GAME_INFORMATION)
            {
                g.DrawImage(ImageManager.imgReturn, PauseButton_X, PauseButton_Y, ImageManager.imgReturn.Width, ImageManager.imgReturn.Height);
            }

        }

        /// <summary>
        /// 讀取時的畫面
        /// </summary>
        private void RunGrassBar(Graphics g)
        {
            if (iLoadprogram != 840)
            {
                g.FillRectangle(new SolidBrush(Color.WhiteSmoke), new Rectangle(0, 0, 1024, 768));                      // 背景塗黑
                ScalePic("images/PlaneWar/Hero_" + CGPicture_Count + ".jpg", 0, 70, 2f, 2f);                            // CG 圖
                g.DrawRectangle(new Pen(Color.Black), new Rectangle(115, 600, 800, 50));                                // 進度條框
                g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(115, 600, iLoadprogram, 50));                // 進度
                g.DrawString("Loading ...", new Font("Arian", 24), new SolidBrush(Color.Black), new Point(450, 550));   // 文字
                iLoadprogram += 20;     // 跑進度速度

            }
            if (iLoadprogram == 840)
            {
                g.FillRectangle(new SolidBrush(Color.WhiteSmoke), new Rectangle(0, 0, 1024, 768));                  // 背景塗黑
                ScalePic("images/PlaneWar/Hero_" + CGPicture_Count + ".jpg", 0, 70, 2f, 2f);                        // CG 圖
                g.DrawString("Complete", new Font("Arian", 24), new SolidBrush(Color.White), new Point(450, 550));  // 進度條框

                iLoadprogram = 0;
                CGPicture_Count++;

                if (CGPicture_Count == 6)
                {
                    CGPicture_Count = 0;
                }

                if (iStage == 1)
                {
                    mState = GAME_STAGE1;
                }
                else if (iStage == 2)
                {
                    mState = GAME_STAGE2;
                }
                else if (iStage == 3)
                {
                    mState = GAME_STAGE3;
                }
            }
        }

        /// <summary>
        /// 繪製出金錢數字
        /// </summary>
        /// <param name="i">輸入數字給予該數字圖片</param>
        Image DrawMoneyNumber(int i)
        {
            switch (i)
            {
                case 1: return ImageManager.imgNumber1;
                case 2: return ImageManager.imgNumber2;
                case 3: return ImageManager.imgNumber3;
                case 4: return ImageManager.imgNumber4;
                case 5: return ImageManager.imgNumber5;
                case 6: return ImageManager.imgNumber6;
                case 7: return ImageManager.imgNumber7;
                case 8: return ImageManager.imgNumber8;
                case 9: return ImageManager.imgNumber9;
                default: return ImageManager.imgNumber0;
            }
        }

        /// <summary>
        /// 繪製出玩家的子彈
        /// </summary>
        /// <param name="guns">玩家輸入火力等級，會自動判別</param>
        /// <param name="e"></param>
        /// <param name="j"></param>
        private void DrawPlayerBullet(int guns)
        {
            switch (guns)
            {
                case 1:
                    {
                        for (int i = 0; i < PlayerBullets1.Count; i++)
                            if (PlayerBullets1[i].mFacus == true)
                                PlayerBullets1[i].Draw(g);
                        for (int i = 0; i < PlayerBullets2.Count; i++)
                            if (PlayerBullets2[i].mFacus == true)
                                PlayerBullets2[i].Draw(g);
                    }
                    break;
                case 2:
                    {
                        for (int i = 0; i < PlayerBullets1.Count; i++)
                            if (PlayerBullets1[i].mFacus == true)
                                PlayerBullets1[i].Draw(g);
                        for (int i = 0; i < PlayerBullets2.Count; i++)
                            if (PlayerBullets2[i].mFacus == true)
                                PlayerBullets2[i].Draw(g);
                    }
                    break;
                case 3:
                    {
                        for (int i = 0; i < PlayerBullets1.Count; i++)
                            if (PlayerBullets1[i].mFacus == true)
                                PlayerBullets1[i].Draw(g);
                        for (int i = 0; i < PlayerBullets2.Count; i++)
                            if (PlayerBullets2[i].mFacus == true)
                                PlayerBullets2[i].Draw(g);
                        for (int i = 0; i < PlayerBullets3.Count; i++)
                            if (PlayerBullets3[i].mFacus == true)
                                PlayerBullets3[i].Draw(g);
                        for (int i = 0; i < PlayerBullets4.Count; i++)
                            if (PlayerBullets4[i].mFacus == true)
                                PlayerBullets4[i].Draw(g);
                    }
                    break;
                default:
                    {
                        for (int i = 0; i < PlayerBullets1.Count; i++)
                            if (PlayerBullets1[i].mFacus == true)
                                PlayerBullets1[i].Draw(g);
                    }
                    break;
            }
        }

        /// <summary> 
        /// 遊戲中暫停鍵的功能 
        /// </summary>
        private void PauseButtomMouseEvent(MouseEventArgs e)
        {
            // 點選暫停按鍵
            if ((e.X >= PauseButton_X && e.X <= PauseButton_X + ImageManager.imgPause.Width)
                && (e.Y >= PauseButton_Y
                && e.Y <= PauseButton_Y + ImageManager.imgPause.Height))
            {
                if (VoiceEnable) soundBackground.controls.pause();
                GameTimer.Enabled = false;
                EnemyTimer.Enabled = false;
                BulletTimer.Enabled = false;
                TimeNow.Enabled = false;
                pause.StartPosition = FormStartPosition.CenterParent;
                switch (pause.ShowDialog())
                {
                    // 重新開始
                    case DialogResult.OK:
                        {

                            break;
                        }
                    // 繼續  
                    case DialogResult.No:
                        {
                            GameTimer.Enabled = true;
                            BulletTimer.Enabled = true;     // 子彈間格時間
                            EnemyTimer.Enabled = true;      // 敵人生成間格時間
                            TimeNow.Enabled = true;

                            if (VoiceEnable) soundBackground.controls.play();
                            break;
                        }
                    // 返回主選單
                    case DialogResult.Cancel:
                        {
                            Stage1_Start = false;
                            Stage2_Start = false;
                            Stage3_Start = false;

                            ENEMY_POOL_COUNT = 0;   // 暫時取消敵人生成 

                            boss1.Alive = false;
                            boss2.Alive = false;

                            InitializeVariable();
                            SetPlayerStation();
                            ListEnemys_Type.Clear();
                            ListEnemys_Type.Clear();
                            ListEnemys_Type.Clear();
                            PlayerBullets1.Clear();
                            PlayerBullets2.Clear();
                            PlayerBullets3.Clear();
                            PlayerBullets4.Clear();

                            GameTimer.Enabled = true;
                            //BulletTimer.Enabled = false;
                            //EnemyTimer.Enabled = false;


                            TextCount = 0;
                            timer_counts = 0;
                            minutes = 0;
                            k = 0;
                            iScore = 0;
                            mState = GAME_MENU;
                            break;
                        }
                }
                bLeft = false; bRight = false; bUp = false; bDown = false;
            }
        }
        private void PauseButtomKeyBoardEvent()
        {
            // 點選暫停按鍵
            if (VoiceEnable) soundBackground.controls.pause();
            GameTimer.Enabled = false;
            EnemyTimer.Enabled = false;
            BulletTimer.Enabled = false;
            pause.StartPosition = FormStartPosition.CenterParent;
            switch (pause.ShowDialog())
            {
                // 重新開始
                case DialogResult.OK:
                    {
                        VoiceEnable = false;
                        break;
                    }
                // 繼續  
                case DialogResult.No:
                    {
                        GameTimer.Enabled = true;
                        BulletTimer.Enabled = true;     // 子彈間格時間
                        EnemyTimer.Enabled = true;      // 敵人生成間格時間

                        if (VoiceEnable) soundBackground.controls.play();
                        break;
                    }
                // 返回主選單
                case DialogResult.Cancel:
                    {
                        Stage1_Start = false;
                        Stage2_Start = false;
                        Stage3_Start = false;

                        ENEMY_POOL_COUNT = 0;   // 暫時取消敵人生成 

                        boss1.Alive = false;
                        boss2.Alive = false;

                        InitializeVariable();
                        SetPlayerStation();
                        ListEnemys_Type.Clear();
                        ListEnemys_Type.Clear();
                        ListEnemys_Type.Clear();
                        PlayerBullets1.Clear();
                        PlayerBullets2.Clear();
                        PlayerBullets3.Clear();
                        PlayerBullets4.Clear();

                        GameTimer.Enabled = true;
                        //BulletTimer.Enabled = false;
                        //EnemyTimer.Enabled = false;


                        TextCount = 0;
                        timer_counts = 0;
                        minutes = 0;
                        k = 0;
                        iScore = 0;
                        mState = GAME_MENU;
                        break;
                    }
            }
            bLeft = false; bRight = false; bUp = false; bDown = false;

        }
        /// <summary> 
        /// 其他頁面的返回功能的功能 
        /// </summary>
        private void ReturnButtomEvent(MouseEventArgs e)
        {
            if ((e.X >= PauseButton_X && e.X <= PauseButton_X + ImageManager.imgPause.Width) && (e.Y >= PauseButton_Y && e.Y <= PauseButton_Y + ImageManager.imgPause.Height))
            {
                mState = GAME_MENU;
            }
        }

        /// <summary>
        /// 繪製 GAME_VICTOR 和 GAME_OVER
        /// </summary>
        public void PaintGameResult(Graphics g)
        {
            g.DrawImage(ImageManager.imgGameBackground, mBitposY0);
            g.DrawImage(ImageManager.imgGameBackground, mBitposY1);

            PowerStatus = false;
            ShiledStatus = false;

            // 把勝利畫面初始化
            Stage1_Start = false;
            Stage2_Start = false;
            Stage3_Start = false;

            // 重新生成 王
            boss1.Alive = false;
            boss2.Alive = false;

            // 道具時間及狀態
            PowerUpTime = 10;
            ShiledTime = 10;

            // 清除敵人
            ListEnemys_Type.Clear();

            // 清除敵人子彈
            EnemyBullets1.Clear();

            // 清除玩家子彈
            PlayerBullets1.Clear();
            PlayerBullets2.Clear();
            PlayerBullets3.Clear();
            PlayerBullets4.Clear();
            Items.Clear();


            // 音樂 停止
            soundBackground.controls.stop();

            //敵人設定
            ENEMY_POOL_COUNT = 0;   // 暫時取消敵人生成 
            ListEnemys_Type.Clear();

            // 玩家設定
            BulletTimer.Enabled = false;

            // 文字事件時間
            TextCount = 0;


  

            // 遊戲時間重置
            timer_counts = 0;
            minutes = 0;

            // 遊戲狀態重置
            GAME_START = false;

            if (mState == GAME_VICTOR)
            {
                g.DrawImage(ImageManager.imgVictor, new Point(250, 150));
                //g.DrawString("Press Click to Contiune !", new Font("Arial", 20), new SolidBrush(Color.White), 350, 500);
                g.DrawString("  完", new Font("Arial", 20), new SolidBrush(Color.White), 330, 500);
            }
            else if (mState == GAME_DEFEAT)
            {
                if (!cougi)
                {
                    tempEarn = (int)iScore;
                    tempEarn /= 100;
                    tempEarn *= 2;
                    iMoney += tempEarn;
                    Earn = Earn + tempEarn;
                    cougi = true;
                }

                soundLose.controls.play();
                // 出現遊戲失敗圖片 及 文字
                g.DrawImage(ImageManager.imgDefeat, new Point(325, 50));
                g.DrawString("您在此次戰役中總共獲得了 " + Earn.ToString() + " 枚金幣", new Font("Arial", 20), Brushes.White, new Point(300, 500));
                g.DrawString("Press Click to Contiune !", new Font("Arial", 20), new SolidBrush(Color.White), 350, 600);
            }
            
        }
        #endregion

        #region 玩家相關的函數
        /// <summary>
        /// 設定玩家狀態
        /// </summary>
        private void SetPlayerStatus()
        {
            player.X = 244;
            player.Y = 600;
            player.Width = 100;
            player.Height = 80;

            player.UserName = userName;
            player.PLAN_DEATH_STATE = false;
            player.Life = 1;
            // 以下兩個屬性 互相呼應, 可能要建立一個 enum 或清單
            storeType = player.CheckPlanType(); // 把判定好的圖片, 直接給商店用

            if (player.FireLevel == 0) { player.Damage = 1; }
            else if (player.FireLevel == 1) { player.Damage = 2; }
            else if (player.FireLevel == 2) { player.Damage = 3; }
            else if (player.FireLevel == 3) { player.Damage = 4; }

            if (player.SpeedLevel == 0) { player.STEP = 20; }
            else if (player.SpeedLevel == 1) { player.STEP = 22; }
            else if (player.SpeedLevel == 2) { player.STEP = 25; }
            else if (player.SpeedLevel == 3) { player.STEP = 30; }

            if (player.RateLevel == 0) { iBulletTimer = 500; BULLET_POOL_COUNT = 15; }
            else if (player.RateLevel == 1) { iBulletTimer = 450; BULLET_POOL_COUNT = 16; }
            else if (player.RateLevel == 2) { iBulletTimer = 350; BULLET_POOL_COUNT = 19; }
            else if (player.RateLevel == 3) { iBulletTimer = 200; BULLET_POOL_COUNT = 24; }

        }
        private void CheckPlayerGuns()
        {
            player.Guns = 0;
            if (player.FireLevel == 3) { player.Guns++; }
            if (player.SpeedLevel == 3) { player.Guns++; }
            if (player.RateLevel == 3) { player.Guns++; }
        }
        /// <summary>
        /// 設定玩家位置
        /// </summary>
        private void SetPlayerStation()
        {
            player.X = 244;
            player.Y = 600;
        }
        #endregion

        #region 檔案相關的函數
        // **FileMode.Create & FileMode.CreateNew , 不會自己新增資料夾
        /// <summary>
        /// 儲存玩家資料
        /// </summary>
        /// 存檔格式: ( UserName 當作檔案名稱 username.txt )
        /// 遊戲暱稱[4 byte]  
        /// 金錢[2 byte]
        /// 飛機狀態[2 byte、2 byte、2 byte] 目前使用機種、強化等級、機種階級
        /// 遊玩時間[]
        /// 飛機強化狀態
        public void SaveFile()
        {
            // 開啟檔案
            FileStream fs = new FileStream(@"save\\" + userName + ".txt", FileMode.Create);
            Encoder ec = Encoding.UTF8.GetEncoder();
            char[] cData;
            Byte[] Bdata;

            String strUserName = userName + "\r\n";
            cData = strUserName.ToCharArray();
            Bdata = new Byte[cData.Length];
            ec.GetBytes(cData, 0, cData.Length, Bdata, 0, true);
            fs.Seek(0, SeekOrigin.Begin);
            fs.Write(Bdata, 0, Bdata.Length);

            String strmoney = iMoney.ToString() + "\r\n";
            cData = strmoney.ToCharArray();
            Bdata = new Byte[cData.Length];
            ec.GetBytes(cData, 0, cData.Length, Bdata, 0, true);
            fs.Seek(0, SeekOrigin.Current);
            fs.Write(Bdata, 0, Bdata.Length);

            String strPlaneState = player.PlanType + "\r\n";
            cData = strPlaneState.ToCharArray();
            Bdata = new Byte[cData.Length];
            ec.GetBytes(cData, 0, cData.Length, Bdata, 0, true);
            fs.Seek(0, SeekOrigin.Current);
            fs.Write(Bdata, 0, Bdata.Length);


            // 取得最後寫入(存檔時間)時間 
            //DateTime FileTime = File.GetLastWriteTime(@"save\\Account.txt");

            // 結束遊戲當下的電腦時間
            DateTime FileTime = DateTime.Now;
            // 年
            float fileYear = FileTime.Year;

            // 月
            float fileMonth = FileTime.Month; String strMonth;
            if (FileTime.Month < 10)
            {
                strMonth = "0" + fileMonth.ToString();
            }
            else
            {
                strMonth = fileMonth.ToString();
            }

            // 日
            float fileDay = FileTime.Day; String strDay;
            if (FileTime.Day < 10)
            {
                strDay = "0" + fileDay.ToString();
            }
            else
            {
                strDay = fileDay.ToString();
            }

            // 時
            float fileHour = FileTime.Hour; String strHour;
            if (FileTime.Hour < 10)
            {
                strHour = "0" + fileHour.ToString();
            }
            else
            {
                strHour = fileHour.ToString();
            }

            // 分
            float fileMinute = FileTime.Minute;
            String strMinute;
            if (FileTime.Minute < 10)
            {
                strMinute = "0" + fileMinute.ToString();
            }
            else
            {
                strMinute = fileMinute.ToString();
            }

            // 秒
            float fileSecond = FileTime.Second; String strSecond;
            if (FileTime.Second < 10)
            {
                strSecond = "0" + fileSecond.ToString();
            }
            else
            {
                strSecond = fileSecond.ToString();
            }

            // 時間整合
            String strTime;
            strTime = "Last Write Time: " + fileYear.ToString() + "\\" + strMonth + "\\" + strDay + "  " +
                                      strHour + ":" + fileMinute + ":" + strSecond + "\r\n";

            cData = strTime.ToCharArray();  // 將要 輸入的字串 轉換成 字元陣列
            Bdata = new Byte[cData.Length];  // 依據字元陣列的空間, 開啟 Byte 空間
            ec.GetBytes(cData, 0, cData.Length, Bdata, 0, true); // 取得 UTF8 的編碼
            fs.Seek(0, SeekOrigin.Current);
            fs.Write(Bdata, 0, Bdata.Length);   // 寫入 Byte 至 輸出流


            // 紀錄 火力強化 等級
            String strFirelevel = player.FireLevel.ToString() + "\r\n";
            cData = strFirelevel.ToCharArray();  // 將要 輸入的字串 轉換成 字元陣列
            Bdata = new Byte[cData.Length];  // 依據字元陣列的空間, 開啟 Byte 空間
            ec.GetBytes(cData, 0, cData.Length, Bdata, 0, true); // 取得 UTF8 的編碼
            fs.Seek(0, SeekOrigin.Current);
            fs.Write(Bdata, 0, Bdata.Length);   // 寫入 Byte 至 輸出流

            // 紀錄 速度強化 等級
            String strSpeedlevel = player.SpeedLevel.ToString() + "\r\n";
            cData = strSpeedlevel.ToCharArray();  // 將要 輸入的字串 轉換成 字元陣列
            Bdata = new Byte[cData.Length];  // 依據字元陣列的空間, 開啟 Byte 空間
            ec.GetBytes(cData, 0, cData.Length, Bdata, 0, true); // 取得 UTF8 的編碼
            fs.Seek(0, SeekOrigin.Current);
            fs.Write(Bdata, 0, Bdata.Length);   // 寫入 Byte 至 輸出流

            // 紀錄 射速強化 等級
            String strRatelevel = player.RateLevel.ToString() + "\r\n";
            cData = strRatelevel.ToCharArray();  // 將要 輸入的字串 轉換成 字元陣列
            Bdata = new Byte[cData.Length];  // 依據字元陣列的空間, 開啟 Byte 空間
            ec.GetBytes(cData, 0, cData.Length, Bdata, 0, true); // 取得 UTF8 的編碼
            fs.Seek(0, SeekOrigin.Current);
            fs.Write(Bdata, 0, Bdata.Length);   // 寫入 Byte 至 輸出流

            fs.Close();
        }

        /// <summary>
        /// 讀取玩家資料
        /// </summary>
        public void LoadFile()
        {
            try
            {
                StreamReader sr = new StreamReader(@"save\\" + userName + ".txt");

                String str1UserName = sr.ReadLine();            // 玩家暱稱
                iMoney = Int32.Parse(sr.ReadLine());            // 金錢
                player.PlanType = Int32.Parse(sr.ReadLine());   // 飛機狀態
                String str4PlayTime = sr.ReadLine();            // 遊玩時間
                player.FireLevel = Int32.Parse(sr.ReadLine());  // 火力等級
                player.SpeedLevel = Int32.Parse(sr.ReadLine()); // 速度等級
                player.RateLevel = Int32.Parse(sr.ReadLine());  // 射速等級
                sr.Close();
            }
            catch (FileNotFoundException) { SaveFile(); } // 因為進入MainForm 時，會先讀取玩家資料 , 剛註冊要先建立檔案

            if (userName == null)
            {
                userName = "Guest";
            }
        }
        #endregion

        #region 事件
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            /* 依照目前遊戲模式來分類 395 , 436 , 477 , 518 */
            if (mState == GAME_MENU)
            {
                if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
                {
                    pSelect.Y -= 41;
                    if (pSelect.Y < 395)
                    {
                        pSelect.Y = 518;
                    }
                }
                if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
                {
                    pSelect.Y += 41;
                    if (pSelect.Y > 518)
                    {
                        pSelect.Y = 395;
                    }
                }
                if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Space)
                {
                    if (pSelect.Y == 395)
                    {
                        mState = GAME_STAGE1;  // 開始遊戲
                    }
                    else if (pSelect.Y == 436)
                    {
                        mState = GAME_SHOP; // 商店頁面
                    }
                    else if (pSelect.Y == 477)
                    {
                        mState = GAME_INFORMATION; // 離開遊戲
                    }
                    else if (pSelect.Y == 518)
                    {
                        mState = GAME_LEAVE; // 離開遊戲
                    }
                }
                if (e.KeyCode == Keys.F1)
                {
                    ShiledStatus = true;
                    comboBox1.Visible = true;
                    comboBox1.Enabled = true;
                    JumpButton.Visible = true;
                    JumpButton.Enabled = true;
                }
            }
            else if (mState == GAME_STAGE1 || mState == GAME_STAGE2 || mState == GAME_STAGE3)
            {
                if (e.KeyCode == Keys.F1)
                {
                    comboBox1.Visible = true;
                    comboBox1.Enabled = true;
                    JumpButton.Visible = true;
                    JumpButton.Enabled = true;
                }
                // SKIP 對話事件
                if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Space)
                {
                    if (mState == GAME_STAGE1)
                    {
                        if (!Stage1_Start)
                        {
                            // 點擊第一次會讓所有文字都顯示出來
                            if (TextCount < 21) TextCount = 22;
                            // 第二次點擊就可以進入下一個畫面
                            if (TextCount > 23) Stage1_Start = true;
                        }
                    }
                    if (mState == GAME_STAGE2)
                    {
                        if (!Stage2_Start)
                        {
                            // 點擊第一次會讓所有文字都顯示出來
                            if (TextCount < 16) TextCount = 17;
                            // 第二次點擊就可以進入下一個畫面
                            if (TextCount > 18) Stage2_Start = true;
                        }
                    }
                    if (mState == GAME_STAGE3)
                    {
                        // 點擊第一次會讓所有文字都顯示出來
                        if (TextCount < 16) TextCount = 17;
                        // 第二次點擊就可以進入下一個畫面
                        if (TextCount > 18) Stage3_Start = true;
                    }
                }
                //人物移動
                if (Stage1_Start || Stage2_Start || Stage3_Start)
                {
                    // 往左移動
                    if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) bLeft = true;
                    // 往右移動
                    else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) bRight = true;
                    // 往上移動
                    else if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) bUp = true;
                    // 往下移動
                    else if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) bDown = true;
                    // 子彈時間
                    else if (e.KeyCode == Keys.Q)
                    {
                        GameTimer.Interval = iGameTimer * 2;
                        EnemyTimer.Interval = iEnemyTimer * 2;
                        BulletTimer.Interval = iBulletTimer * 2;
                    }
                    if (e.KeyCode == Keys.Escape)
                    {
                        PauseButtomKeyBoardEvent();
                    }
                }
            }
            else if (mState == GAME_LEAVE)
            {
                Close();
                Environment.Exit(Environment.ExitCode);
            }
            else if (mState == GAME_VICTOR)
            {
                if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Space)
                {
                    mState = GAME_MENU;
                }
            }
            else if (mState == GAME_DEFEAT)
            {
                if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Space)
                {
                    mState = GAME_MENU;
                }
            }
            else if (mState == GAME_SHOP)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    mState = GAME_MENU;
                }
            }
            else if (mState == GAME_STORY)
            {
                if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Space)
                {
                    // 點擊第一次會讓所有文字都顯示出來
                    if (TextCount < 30) TextCount = 31;
                    if (TextCount > 31) mState = GAME_MENU;
                }
            }
            else if (mState == GAME_INFORMATION)
            {
                if (e.KeyCode == Keys.Space)
                {
                    MessageBox.Show("你想要跳過說明，就早說馬！");
                    mState = GAME_MENU;
                }
                if (e.KeyCode == Keys.W)
                {
                    Information.Y -= 10;
                }

                if (e.KeyCode == Keys.A)
                {
                    Information.X -= 10;
                }

                if (e.KeyCode == Keys.S)
                {
                    Information.Y += 10;
                }

                if (e.KeyCode == Keys.D)
                {
                    Information.X += 10;
                }

                if (e.KeyCode == Keys.Return)
                {
                    MessageBox.Show("你選擇玩這款遊戲是對的！");
                }
                if (e.KeyCode == Keys.Escape)
                {
                    MessageBox.Show("時間暫停...(?)");
                }
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) bUp = false;
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) bDown = false;
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) bLeft = false;
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) bRight = false;
            if (e.KeyCode == Keys.Q)
            {
                GameTimer.Interval = iGameTimer;
                EnemyTimer.Interval = iEnemyTimer;
                BulletTimer.Interval = iBulletTimer;
            }
            if (mState == GAME_INFORMATION)
            {
                if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) Information.Y += 10;
                if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) Information.X += 10;
                if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) Information.Y -= 10;
                if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) Information.X -= 10;
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            //player.PosX = e.X;
            //player.PosY = e.Y;
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (mState == GAME_STAGE1)
            {
                if (!Stage1_Start)
                {
                    // 點擊第一次會讓所有文字都顯示出來
                    if (TextCount < 21) TextCount = 22;
                    // 第二次點擊就可以進入下一個畫面
                    if (TextCount > 23) Stage1_Start = true;
                }
                else PauseButtomMouseEvent(e);
            }
            if (mState == GAME_STAGE2)
            {
                if (!Stage2_Start)
                {
                    // 點擊第一次會讓所有文字都顯示出來
                    if (TextCount < 16) TextCount = 17;
                    // 第二次點擊就可以進入下一個畫面
                    if (TextCount > 18) Stage2_Start = true;

                }
                else PauseButtomMouseEvent(e);
            }
            if (mState == GAME_STAGE3)
            {
                if (!Stage3_Start)
                {
                    // 點擊第一次會讓所有文字都顯示出來
                    if (TextCount < 16) TextCount = 17;
                    // 第二次點擊就可以進入下一個畫面
                    if (TextCount > 18) Stage3_Start = true;

                }
                else PauseButtomMouseEvent(e);
            }
            if (mState == GAME_MENU)
            {
                if (e.X > 150 && e.X < 400 && e.Y > 400 && e.Y < 430) mState = GAME_STAGE1;
                else if (e.X > 150 && e.X < 440 && e.Y > 400 && e.Y < 470) mState = GAME_SHOP;
                else if (e.X > 150 && e.X < 440 && e.Y > 400 && e.Y < 510) mState = GAME_INFORMATION;
                else if (e.X > 150 && e.X < 440 && e.Y > 400 && e.Y < 550) Application.Exit();
            }
            else if (mState == GAME_SHOP)
            {
                // 假如按下火力強化按鈕
                if (e.X > store.FireButton.X && e.X < store.FireButton.X + 132 && e.Y > store.FireButton.Y && e.Y < store.FireButton.Y + 48)
                {
                    if (iMoney >= store.Cost && player.FireLevel < 3)
                    {
                        if (player.FireLevel == 0) iMoney -= 50;
                        if (player.FireLevel == 1) iMoney -= 100;
                        if (player.FireLevel == 2) iMoney -= 200;
                        player.FireLevel += 1;
                        store.Udate(player.FireLevel, player.SpeedLevel, player.RateLevel); // 把玩家的數值丟到商店內, 作變更顯示
                        if (player.FireLevel == 3)
                        {
                            player.PlanType += 1;
                            MessageBox.Show("外觀進化!");
                        }
                        storeType = player.CheckPlanType();
                    }
                    else if (player.FireLevel == 3)
                    {
                        MessageBox.Show("你的火力等級已經達到MAX!");
                    }
                    CheckPlayerGuns();
                }
                // 假如按下速度強化按鈕
                else if (e.X > store.SpeedButton.X && e.X < store.SpeedButton.X + 132 && e.Y > store.SpeedButton.Y && e.Y < store.SpeedButton.Y + 48)
                {
                    if (iMoney >= store.Cost && player.SpeedLevel < 3)
                    {
                        if (player.SpeedLevel == 0) iMoney -= 50;
                        if (player.SpeedLevel == 1) iMoney -= 100;
                        if (player.SpeedLevel == 2) iMoney -= 200;
                        player.SpeedLevel += 1;
                        store.Udate(player.FireLevel, player.SpeedLevel, player.RateLevel); // 把玩家的數值丟到商店內, 作變更顯示
                        if (player.SpeedLevel == 3)
                        {
                            player.PlanType += 1;
                            MessageBox.Show("外觀進化!");
                        }
                        storeType = player.CheckPlanType();
                    }
                    else if (player.SpeedLevel == 3)
                    {
                        MessageBox.Show("你的速度等級已經達到MAX!");
                    }
                    CheckPlayerGuns();

                }
                // 假如按下射速強化按鈕
                else if (e.X > store.RateButton.X && e.X < store.RateButton.X + 132 && e.Y > store.RateButton.Y && e.Y < store.RateButton.Y + 48)
                {
                    if (iMoney >= store.Cost && player.RateLevel < 3)
                    {
                        if (player.RateLevel == 0) iMoney -= 50;
                        if (player.RateLevel == 1) iMoney -= 100;
                        if (player.RateLevel == 2) iMoney -= 200;
                        player.RateLevel += 1;
                        store.Udate(player.FireLevel, player.SpeedLevel, player.RateLevel); // 把玩家的數值丟到商店內, 作變更顯示
                        if (player.RateLevel == 3)
                        {
                            player.PlanType += 1;
                            MessageBox.Show("外觀進化!");
                        }
                        storeType = player.CheckPlanType();
                    }
                    else if (player.RateLevel == 3)
                    {
                        MessageBox.Show("你的射速等級已經達到MAX!");
                    }
                    CheckPlayerGuns();
                }
                ReturnButtomEvent(e);
            }
            else if (mState == GAME_INFORMATION)
            {
                ReturnButtomEvent(e);
                if (e.X > 50 && e.X <= 150)
                {
                    Information_Page--;
                    if (Information_Page < 0) Information_Page = 3;
                }
                if (e.X > 900 && e.X <= 980)
                {
                    Information_Page++;
                    if (Information_Page > 3) Information_Page = 0;
                }
            }
            else if (mState == GAME_VICTOR) mState = GAME_MENU;
            else if (mState == GAME_DEFEAT) mState = GAME_MENU;
            else if (mState == GAME_DEFEAT) mState = GAME_MENU;
            else if (mState == GAME_STORY)
            {
                // 點擊第一次會讓所有文字都顯示出來
                if (TextCount < 30) TextCount = 31;
                // 第二次點擊就可以進入下一個畫面
                if (TextCount > 31) mState = GAME_MENU;
            }
        }

        private void JumpButton_Click(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "第一關": mState = GAME_STAGE1; break;
                case "第二關": mState = GAME_STAGE2; break;
                case "第三關": mState = GAME_STAGE3; break;
                case "王直接出現":
                    k = 998;
                    boss1.Alive = true;
                    boss2.Alive = true;
                    break;

                case "王直接死亡":
                    boss1.Alive = false;
                    boss2.Alive = false;
                    break;

                case "商店": mState = GAME_SHOP; break;
                case "劇情1": mState = GAME_STAGE1; break;
                case "劇情2": mState = GAME_STAGE2; break;
                case "劇情3": mState = GAME_STAGE3; break;
                case "劇情4": mState = GAME_STORY; break;
            }
            comboBox1.Visible = false;
            comboBox1.Enabled = false;
            JumpButton.Visible = false;
            JumpButton.Enabled = false;
        }
        #endregion
    }
}
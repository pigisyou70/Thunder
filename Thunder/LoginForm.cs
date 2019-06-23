using System;
using System.Drawing;
using System.Windows.Forms;
using WMPLib;
namespace Thunder
{
    public partial class LoginForm : Form
    {
        int nLoginCount = 0;
        const int MAX_LOGIN_COUNT = 3;
        public bool guestLogin;
        //Dictionary<String, String> mDictionary = new Dictionary<String, String>();
        UserInfo uiLogin;
        MainForm main;
        public static WindowsMediaPlayer soundBackground = new WindowsMediaPlayer();      // 背景音樂
        public LoginForm(ref UserInfo ui)
        {
            InitializeComponent();
            // Set login info to class member
            uiLogin = ui;
            uiLogin.Load(@"save\\Accounts.txt");

            soundBackground.settings.autoStart = false;
            soundBackground.URL = (Application.StartupPath + "/music/Space.mp3");
            soundBackground.settings.volume = 10;
            soundBackground.settings.playCount = 1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /** 進入登入畫面, 先讀取帳戶資料，
             *  存檔規範: 帳號、密碼、金幣、存檔時間
             */

            //MessageBox.Show("第一次遊玩的玩家，請先註冊遊戲帳號及密碼。" +
            //                "\r\n因為每個帳號都是獨立分開的遊戲資料，" +
            //                "\r\n資料的作用是要儲存玩家在遊玩時的資訊。" +
            //                "\r\n假如沒有註冊帳號，遊玩過程將不紀錄" +
            //                "\r\n如果想要試玩可以輸入以下資訊：" +
            //                "\r\n帳號: Admin\r\n密碼: Admin" +
            //                "\r\n謝謝您的合作與支持~~");
  
            soundBackground.controls.play();
        }
        private void AcceptButton_Click(object sender, EventArgs e)
        {
            bool result = false;
            result = uiLogin.checkLogin(txtUserName.Text, txtPassword.Text);
            
            if(result)
            {
                main = (MainForm)this.Owner; // 把 Login 窗口 指針賦給 MainForm
                main.GetName = txtUserName.Text;
                this.DialogResult = DialogResult.OK;
                soundBackground.controls.stop();
            }
            else
            {
                nLoginCount++;

                if (nLoginCount == MAX_LOGIN_COUNT) // Over 3 times
                {
                    MessageBox.Show("錯誤已達到三次，帳號進行鎖定");
                    MessageBox.Show("如有問題，請洽客服人員");
                    this.DialogResult = DialogResult.Cancel;
                }
            }
        }

        // 取消按鈕
        private void CancelButton_Click(object sender, EventArgs e)
        {
            main = (MainForm)this.Owner;
            main.GetLogin = false;
            this.DialogResult = DialogResult.Cancel;
        }
        private void GuestButton_Click(object sender, EventArgs e)
        {
            main = (MainForm)this.Owner; // 把 Login 窗口 指針賦給 MainForm
            main.GetLogin = true;
            main.GetName = "Guest";
            this.DialogResult = DialogResult.OK;
            soundBackground.controls.stop();
        }
        // 註冊按鈕
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            bool result = false;
            
            uiLogin.UserName = txtUserName.Text;
            uiLogin.Password = txtPassword.Text;
            if (txtUserName.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("註冊失敗");
                return;
            }
            else if(uiLogin.checkUserName(uiLogin.UserName))
            {
                
            }
            else
            {
                uiLogin.Save("Save", "Accounts.txt", uiLogin.UserName);
                uiLogin.Save("Save", "Accounts.txt", uiLogin.Password);
                uiLogin.Load(@"save\\Accounts.txt"); // 重新載入
                // 自動登入系統
                result = uiLogin.checkLogin(txtUserName.Text, txtPassword.Text);
                main = (MainForm)this.Owner; // 把 Login 窗口 指針賦給 MainForm
                main.GetName = txtUserName.Text;
                this.DialogResult = DialogResult.OK;
            }
        }
        // 忘記密碼按鈕
        private void ForgetButton_Click(object sender, EventArgs e)
        {
            uiLogin.FoundPassWord(txtUserName.Text);
        }

        private void ReadMeButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("第一次遊玩的玩家，請先註冊遊戲帳號及密碼。" +
                    "\r\n因為每個帳號都是獨立分開的遊戲資料，" +
                    "\r\n資料的作用是要儲存玩家在遊玩時的資訊。" +
                    "\r\n假如沒有註冊帳號，遊玩過程將不紀錄" +
                    "\r\n如果想要試玩可以輸入以下資訊：" +
                    "\r\n帳號: Admin\r\n密碼: Admin" +
                    "\r\n謝謝您的合作與支持~~");
        }
    }
}

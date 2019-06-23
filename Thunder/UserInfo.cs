using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Thunder
{
    public class UserInfo
    {
        //int InfoCount;
        String strUserName;
        String strPassword;
        Dictionary<String, String> mDictionary = new Dictionary<String, String>();

        /// <summary> 帳號 </summary>
        public String UserName
        {
            get { return strUserName; }
            set { strUserName = value; }
        }
        /// <summary> 密碼 </summary>
        public String Password
        {
            get { return strPassword; }
            set { strPassword = value; }
        }

        public UserInfo()
        {
            strUserName = "";
            strPassword = "";
        }

        /// <summary> 讀取帳戶文件
        /// <para>先把集合清空，</para> 
        /// <para>在一行一行讀取並添加到集合中</para> 
        /// </summary>
        public void Load(String FileName)
        {
            mDictionary.Clear();
            try
            {
                using (StreamReader sr = new StreamReader(FileName))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        String Password = sr.ReadLine();
                        mDictionary.Add(line, Password);
                    }
                }
            }
            catch (IOException e)
            {
                using (StreamWriter sw = new StreamWriter("Error.text"))
                {
                    sw.WriteLine(e.Message);
                }
            }
        }

        // 存檔時間, 可以從 DateTime filetime = FileInfo.LastWriteTime;
        // 寫入檔案, 保留之前資料, 持續寫入, 方法1
        public void Save(String FilePath, String FileName, String Data)
        {
            DirectoryInfo logDirection = new DirectoryInfo(FilePath);
            FileInfo logFile = new FileInfo(FilePath + @"\\" + FileName);
            StreamWriter Log;
            if (logDirection.Exists == false)
                logDirection.Create();
            Log = logFile.AppendText();
            Log.WriteLine(Data);
            //Log.Flush();
            Log.Close();
            Log = null;
        }

        public bool checkUserName(String n)
        {
            bool result = false;
            if (mDictionary.ContainsKey(n)) // 有找到, 代表有重複的
            {
                MessageBox.Show("已經有此帳號!");
                result = true;
            }
            else
            {
                MessageBox.Show("註冊成功");
                result = false;
            }
            return result;
        }
        public bool checkLogin(String U, String P)
        {
            bool result = false;
            if (mDictionary.ContainsKey(U))  // 先查找帳號, 假如有此帳號, 進行下一個步驟
            {
                if (mDictionary[U] == P)
                {
                    result = true;
                    MessageBox.Show("登入成功!");
                }
                else
                {
                    MessageBox.Show("密碼錯誤!");
                    result = false;
                }
            }
            else
            {
                MessageBox.Show("使用者名稱錯誤");
                result = false;
            }

            return result;
        }

        public String FoundPassWord(String n)
        {
            try
            {
                MessageBox.Show(n + "的密碼為: " + mDictionary[n].ToString());
                return mDictionary[n].ToString();
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show("此帳號並沒有註冊");
                return "";
            }
        }
    }
}

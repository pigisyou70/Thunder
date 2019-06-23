using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thunder
{

    public static class Helper
    {
        private static readonly float[] mSinValue;
        private static readonly float[] mCosValue;

        /// <summary>
        /// 建立 Theta ( 角度轉換成徑度的式子
        /// </summary>
        static Helper()
        {
            mSinValue = new float[360];
            mCosValue = new float[360];

            for (float i = 0; i < 360.0f; i++)
            {
                mSinValue[(int)i] = (float)Math.Sin(i * Data.Radian);
                mCosValue[(int)i] = (float)Math.Cos(i * Data.Radian);
            }
        }
        /// <summary>
        /// 傳回 Sin Theta
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static float FastSin(int angle)
        {
            angle %= 360;

            if (angle < 0)
            {
                angle = -angle;
                return -mSinValue[angle];
            }

            return mSinValue[angle];
        }
        /// <summary>
        /// 傳回 Cos Theta
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static float FastCos(int angle)
        {
            angle %= 360;

            if (angle < 0)
            {
                angle = -angle;
            }

            return mCosValue[angle];
        }

        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
            {
                return min;
            }
            if (value > max)
            {
                return max;
            }
            return value;
        }

        public static float GetRandomFloat(float min, float max)
        {
            return min + (float)Data.Rnd.NextDouble() * (max - min);
        }

        public static bool GetRandomBool()
        {
            return Data.Rnd.NextDouble() <= 0.5f;
        }

        public static int GetRandomInt(int min, int max)
        {
            return Data.Rnd.Next(min, max);
        }
        /// <summary>
        /// 帶入angle角度和speed半徑，求出斜點
        /// </summary>
        /// <param name="angle">角度</param>
        /// <param name="speed">半徑</param>
        /// <returns>傳回極座標</returns>
        public static Point GetSpeedWithAngle(int angle, float speed)
        {
            float x = FastCos(angle) * speed;
            float y = FastSin(angle) * speed;
            return new Point((int)x, (int)y);
        }
    }
}

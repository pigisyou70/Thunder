using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thunder
{
    class Vector2
    {
        public static readonly Vector2 Zero = new Vector2(0, 0);

        public float X;
        public float Y;

        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public Vector2(Vector2 v)
        {
            this.X = v.X;
            this.Y = v.Y;
        }

        public override string ToString()
        {
            return "(" + X.ToString() + "," + Y.ToString() + ")";
        }

        public override bool Equals(object obj)
        {
            if (object.Equals(obj, null))
            {
                return false;
            }

            Vector2 v = (Vector2)obj;

            return this.X == v.X && this.Y == v.Y;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public void Normalize()
        {
            float m = this.X * this.X + this.Y * this.Y;

            if (m < 0.00001f)
            {
                m = 1;
            }

            m = 1.0f / (float)Math.Sqrt((double)m);

            X = X * m;
            Y = Y * m;
        }

        public void Reverse()
        {
            this.X = -this.X;
            this.Y = -this.Y;
        }

        public float Length()
        {
            return (float)Math.Sqrt(X * X + Y * Y);
        }

        public float Angle()
        {
            return (float)Math.Atan2(Y, X);
        }

        public float Angle(Vector2 vec)
        {
            Vector2 s = this;
            s.Normalize();
            vec.Normalize();
            return (float)Math.Acos(Vector2.Dot(s, vec));
        }

        public void Rotate(float angle)
        {
            float x = X;
            float y = Y;

            x = X * (float)Math.Cos(angle) - Y * (float)Math.Sin(angle);
            y = X * (float)Math.Sin(angle) + Y * (float)Math.Cos(angle);

            X = x;
            Y = y;
        }

        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            if (object.Equals(v1, null) || object.Equals(v2, null))
            {
                return object.Equals(v1, v2);
            }

            if (v1.X == v2.X)
            {
                return v1.Y == v2.Y;
            }

            return false;
        }

        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            if (object.Equals(v1, null) || object.Equals(v2, null))
            {
                return !object.Equals(v1, v2);
            }

            if (v1.X != v2.X || v1.Y != v2.Y)
            {
                return true;
            }

            return false;
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X * v2.X, v1.Y * v2.Y);
        }

        public static Vector2 operator *(Vector2 v, float s)
        {
            return new Vector2(v.X * s, v.Y * s);
        }

        public static Vector2 operator *(float s, Vector2 v)
        {
            return new Vector2(v.X * s, v.Y * s);
        }

        public static Vector2 operator /(Vector2 v, float s)
        {
            if (Math.Abs(s) < float.MinValue)
            {
                return Vector2.Zero;
            }

            return new Vector2(v.X / s, v.Y / s);
        }

        public static Vector2 operator /(float s, Vector2 v)
        {
            if (Math.Abs(s) < float.MinValue)
            {
                return Vector2.Zero;
            }

            return new Vector2(v.X / s, v.Y / s);
        }

        //public static Vector2 operator -(Vector2 v)
        //{
            //Vector2 vec = new Vector2();
            //vec.X = -v.X;
            //vec.Y = -v.Y;
            //return vec;
        //}

        public static float Distance(Vector2 v1, Vector2 v2)
        {
            float x = v1.X - v2.X;
            float y = v1.Y - v2.Y;
            float total = x * x + y * y;
            return (float)Math.Sqrt((double)total);
        }

        public static float Dot(Vector2 v1, Vector2 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        //public static Vector2 Lerp(Vector2 min, Vector2 max, float value)
        //{
            //Vector2 v;
            //v.X = min.X + (max.X - min.X) * value;
            //v.Y = min.Y + (max.Y - min.Y) * value;
            //return v;
        //}
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using System.Diagnostics;

namespace HerobotGalaxy.Game.Physics
{

    public class BasicCalc
    {
        static float PI = (float)(22.0 / 7.0);

        public static float Sin_Deg(float degree)
        {
            return (float)Math.Sin(degree / 180.0 * PI);
        }

        public static float Cos_Deg(float degree)
        {
            return (float)Math.Cos(degree / 180.0 * PI);
        }

        public static float Tan_Deg(float degree)
        {
            return (float)Math.Tan(degree / 180.0 * PI);
        }

        public static float Csc_Deg(float degree)
        {
            return (float)(1.0 / Math.Sin(degree / 180.0 * PI));
        }

        public static float Sec_Deg(float degree)
        {
            return (float)(1.0 / (Math.Cos(degree / 180.0 * PI)));
        }

        public static float Cot_Deg(float degree)
        {
            return (float)(1.0 / (Math.Tan(degree / 180.0 * PI)));
        }

        public static void Swap(ref float a, ref float b)
        {
            float temp = a;
            a = b;
            b = temp;
        }

    }


    public class LinearEquation
    {
        const int DEFAULT_SIZE = 1234;
        float x1, y1, x2, y2, gradient;

        public LinearEquation(float gradient)
        {
            this.gradient = gradient;

            this.x1 = 0;
            this.x2 = 0;
            this.y1 = 0;
            this.y2 = 0;
        }

        public void SetGradientFromDegree(float degree)
        {
            this.gradient = BasicCalc.Tan_Deg(degree);
        }

        public LinearEquation(float x1, float y1, float x2, float y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            if (x2 == x1)
            {
                gradient = 1;
            }
            else if (y1 == y2)
            {
                gradient = 0;
            }
            else
            {
                this.gradient = (y2 - y1) / (x2 - x1);
            }


        }


        public float GetGradient()
        {
            return gradient;
        }

        public List<Vector2> GetVectorPoint(float gap)
        {
            List<Vector2> list = new List<Vector2>(DEFAULT_SIZE);


            float x_gap = gap / (float)(Math.Sqrt(gradient * gradient + 1));
            x_gap = (x1 > x2) ? -x_gap : x_gap;
            for (float x = x1; x <= x2; x += x_gap)
            {
                list.Add(new Vector2(x, gradient * x + y1));
            }

            return list;
        }

        public List<Vector2> GetPoint(float gap, float length)
        {
            List<Vector2> list = new List<Vector2>(DEFAULT_SIZE);

            float x_gap = (float)(gap / (Math.Sqrt(gradient * gradient + 1)));
            x_gap = (x1 > x2) ? -x_gap : x_gap;
            float limit = (float)(x1 + length / (Math.Sqrt(gradient * gradient + 1)));
            for (float x = x1; x <= limit; x += x_gap)
            {
                list.Add(new Vector2(x, gradient * x + y1));
            }
            return list;
        }

    }

    public class GravityProjection
    {

        const float GRAVITY = 9.8f;
        const int DEFAULT_SIZE = 123;

        bool ReversedY { get; set; }
        bool ReversedX { get; set; }

        Vector2 VectorStart { get; set; }
        public float Velocity { get; set; }
        float Acceleration { get; set; }
        public float Degree { get; set; }
        float Time { get; set; }

        float ConstantA
        {
            get { return (BasicCalc.Tan_Deg(Degree)); }
            set { this.ConstantA = value; }
        }
        float ConstantB
        {
            get { return (Acceleration / (2 * Velocity * Velocity * BasicCalc.Cos_Deg(Degree) * BasicCalc.Cos_Deg(Degree))); }
            set { this.ConstantB = value; }
        }

        Vector2 HighestPoint
        {
            get { return VectorStart + (new Vector2(GetXAt(TimeAtHighestPoint), GetYAt(TimeAtHighestPoint))); }
            set { this.HighestPoint = value; }
        }
        Vector2 FurthestPoint
        {
            get { return VectorStart + (new Vector2(GetXAt(TimeAtFurthestPoint), GetYAt(TimeAtFurthestPoint))); }
            set { this.FurthestPoint = value; }
        }
        
        float TimeAtHighestPoint
        {
            get { return Velocity * BasicCalc.Sin_Deg(Degree) / Acceleration; }
            set { this.TimeAtHighestPoint = value; }
        }
        
        float TimeAtFurthestPoint
        {
            get { return 2 * Velocity * BasicCalc.Sin_Deg(Degree) / -Acceleration; }
            set { this.TimeAtFurthestPoint = value; }
        }

        public GravityProjection(float velocity, float acceleration, float degree, float time) :
            this(Vector2.Zero, velocity, acceleration, degree, time) { }

        public GravityProjection(Vector2 vecStart, float velocity, float acceleration, float degree, float time) :
            this(vecStart, velocity, acceleration, degree, time, false, true) { }

        public GravityProjection(Vector2 vecStart, float velocity, float acceleration, float degree, float time, bool revX, bool revY)
        {
            this.VectorStart = vecStart;
            this.Velocity = velocity;
            this.Acceleration = acceleration;
            this.Degree = degree;
            this.Time = time;
            this.ReversedX = revX;
            this.ReversedY = revY;
            
        }

        public float GetXAt(float time)
        {
            float nowX = Velocity * BasicCalc.Cos_Deg(Degree) * time;
            if (ReversedX) {
                nowX = -nowX;
            }
            return Velocity * BasicCalc.Cos_Deg(Degree) * time;
        }

        public float GetYAt(float time)
        {
            float nowY = Velocity * BasicCalc.Sin_Deg(Degree) * time - 0.5f * Acceleration * time * time;

            

            //Debug.WriteLine("nowY" + nowY);
            if (ReversedY)
            {
                nowY = -nowY;
            }
            
            return nowY;
        }

        public float GetVectorGradientAt(float x, float y)
        {
            return (ConstantA + 2 * ConstantB * x);
        }

        public List<Tuple<Vector2, float>> GetMotionVector(float gap)
        {
            List<Tuple<Vector2, float>> list = new List<Tuple<Vector2, float>>(DEFAULT_SIZE);
            float limit = TimeAtFurthestPoint;
            float nowX, nowY;
            
            for (float t = 0; t <= limit; t += gap)
            {
                nowX = GetXAt(t);
                nowY = GetYAt(t);
                
                list.Add(new Tuple<Vector2, float>(VectorStart + (new Vector2(nowX, nowY)), GetVectorGradientAt(nowX, nowY)));
            }
            
            return list;
        }

    }

}

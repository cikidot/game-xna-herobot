using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace HerobotGalaxy.Game.Physics
{
    public class VectorFunc
    {
        public static List<Vector2> ReverseVectorX(float width, List<Vector2> list)
        {
            int size = list.Count;
            for (int n = 0; n < size; n++)
            {
                list[n] = new Vector2(width - list[n].X, list[n].Y);
            }
            return list;
        }
        public static List<Vector2> ReverseVectorY(float height, List<Vector2> list)
        {
            int size = list.Count;
            for (int n = 0; n < size; n++)
            {
                list[n] = new Vector2(list[n].X, height - list[n].Y);
            }
            return list;
        }

        public static List<Vector2> ReverseVectorXY(int width, int height, List<Vector2> list)
        {            
            return ReverseVectorY(height, ReverseVectorX(width, list));
        }
    }
}

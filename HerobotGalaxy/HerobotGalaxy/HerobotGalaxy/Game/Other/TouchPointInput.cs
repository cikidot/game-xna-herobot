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
using System.Diagnostics;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;

using HerobotGalaxy.Game.Physics;

namespace HerobotGalaxy.Game.Other
{
    public class TouchPointInput
    {
        public Vector2 startLocation{get; set;}
        public Vector2 releaseLocation { get; set; }
        public bool onReleased = false;
        public bool onPressed = false;

        public TouchPointInput(float x, float y)
        {
            startLocation = new Vector2(x, y);
        }

        public void update()
        {
            TouchCollection touchLocations = TouchPanel.GetState();
            foreach (TouchLocation touchLocation in touchLocations)
            {
                if (touchLocation.State == TouchLocationState.Released)
                {
                    releaseLocation = touchLocation.Position;
                    onReleased = true;
                    onPressed = false ;
                }
                else if (touchLocation.State == TouchLocationState.Pressed)
                {
                    onReleased = false;
                    onPressed = true;
                    //startLocation = touchLocation.Position;
                }
                else
                {
                    onPressed = false;
                }
            }
        }

        public float getDegree()
        {
            if (!onReleased) return -1;

            
            float degree = (float)Math.Atan(-(releaseLocation.Y - 250)/ (releaseLocation.X - 50.0+0.0));
            Debug.WriteLine("input" + degree + " " + (degree * 180 * 7 / 22.0));
            onReleased = false;
            return (degree*180*7/22.0f);
        }
    }
}

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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using HerobotGalaxy.Game.Sprites;
using HerobotGalaxy.Game.Physics;

namespace HerobotGalaxy.Game.Other
{
    public class Projectile : Sprite
    {
        private float time = 0;
        public Vector2 startVec { get; set; }
        private GravityProjection gp;
        public bool start { get; set; }
        public bool onDecay { get; set; }

        public int HitDamage;

        public int Damage { get; set; }

        public Projectile(Texture2D texture, Vector2 pos, Microsoft.Xna.Framework.Color color)
            : base(texture, pos, color)
        {
            startVec = pos;
            gp = new GravityProjection(400, 500, 0, 10);
            onDecay = false;
        }

        public void setVelocity(float v)
        {
            gp.Velocity += v;
        }

        public override void Update(GameTimerEventArgs gameTime)
        {
            base.Update(gameTime);
            if (Position.Y >= 480)
            {
                //if (!onDecay) time = 0;
                onDecay = true;
                start = false;
                time = 0;
            }

            if (start)
            {
                time += (float)gameTime.ElapsedTime.TotalSeconds;
                base.Position = new Vector2(startVec.X + gp.GetXAt(time), startVec.Y + gp.GetYAt(time));
                //Debug.WriteLine("gerak" + ( gp.GetXAt(time)) + " " + (gp.GetYAt(time)) +" "+time);
            }
        }

        public void SetStart(float Degree)
        {
            start = true;
            gp.Degree = Degree;

        }

        public void Renew()
        {
            onDecay = false;
            gp.Velocity = 80;
        }

        public Projectile Clone()
        {
            return new Projectile(Texture2D, startVec, base.Color);
        }
    }
}

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

namespace HerobotGalaxy.Game.Other
{
    public class Bar : Sprite
    {
        private float time = 0;
        private bool start = false,reverse=false;
        private Microsoft.Xna.Framework.Rectangle barmeter;
        public int metervalue { get; set; }
        private int width = 0;
        private RenderTarget2D rt2d;

        public Bar(Texture2D texture, Vector2 position,GraphicsDevice graphicsDevice) :
            base(texture, new Vector2(-100,-100), Microsoft.Xna.Framework.Color.White, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f)
        {
            rt2d = new RenderTarget2D(graphicsDevice, texture.Width - 1, texture.Height - 1);
            graphicsDevice.SetRenderTarget(rt2d);
            graphicsDevice.Clear(Microsoft.Xna.Framework.Color.Red);
            graphicsDevice.SetRenderTarget(null);

            metervalue = 1;
            barmeter = new Microsoft.Xna.Framework.Rectangle((int)position.X, (int)position.Y, 0, (int)texture.Height - 2);
            width = texture.Width - 2;
        }

        public void setStart()
        {
            reverse = false;
            start = true;
            Position =new Vector2(barmeter.X - 1,barmeter.Y - 1);
        }

        public void setStop()
        {
            start = false ;
            Position =new Vector2( -100,-100);
            barmeter.Width= metervalue = 0;
            reverse = false;
        }

        public override void Update(GameTimerEventArgs gameTime)
        {
            base.Update(gameTime);
            if (start)
            {
                time += (float)gameTime.ElapsedTime.TotalSeconds;
                if (time > 0.02)
                {
                    metervalue += (reverse)? -1 : 1;
                    barmeter.Width = metervalue;
                    
                    //Debug.WriteLine(metervalue + "meter"+time);
                    time = 0;
                }
                if (metervalue > width)
                {
                    start = false;
                    //reverse = true;
                }
                //if (metervalue == 0) start = false;

            }
        }

        public override void Draw(GameTimerEventArgs gameTime, SpriteBatch batch)
        {
            base.Draw(gameTime, batch);
            batch.Draw(rt2d, barmeter, Microsoft.Xna.Framework.Color.Red);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace HerobotGalaxy.Game.Sprites
{
    public class AnimatedSprite : Sprite
    {
        public Texture2D texture;

        public int CurrentFrame, first, last;
        public bool increment = true;
        
        public int Column { get; set; }
        public int Row { get; set; }
        public Rectangle[] area;

        public int animatedtime = 100;


        bool stop = false; int stopFrame = 0;
        /*
        public override Rectangle? DrawArea
        {
            get
            {
                if (FPS <= 0) return Atlas.GetFrame(CurrentRow,0);
                int periode = 1000 / FPS;
                int frame = CurrentTime / periode;
                frame = frame % Atlas.GetRowFrame(CurrentRow);                
                return Atlas.GetFrame(CurrentRow, frame);
            }
            set
            {
            }
        }
        */

        private int CurrentTime;


        public AnimatedSprite(Texture2D texture)
            : this(texture, 1, 1, Vector2.Zero, 100)
        {
        }

        public AnimatedSprite(Texture2D texture, int row, int column, Vector2 position, int milianimatedtime)
            : base(texture, position, Color.White)
        {
            this.CurrentFrame = 0;

            animatedtime = milianimatedtime;
            area = new Rectangle[row * column];
            int posiX = 0, posiY = 0, width = texture.Width / column, height = texture.Height / row;
            for (int ii = 0; ii < row * column; ii++)
            {
                area[ii] = new Rectangle(posiX, posiY, width, height);
                if (posiX + width >= texture.Width)
                {
                    posiY += height;
                    posiX = 0;
                }
                else posiX += width;
            }
            last = row * column;
            first = 0;

        }

        public void animate(int first, int last)
        {
            this.first = first;
            this.last = last;
        }


        public override void Update(GameTimerEventArgs gameTime)
        {
            this.CurrentTime += gameTime.ElapsedTime.Milliseconds;
            if (CurrentTime > animatedtime)
            {
                CurrentTime -= animatedtime;
                    if (increment)
                    {
                        if (++CurrentFrame >= last) CurrentFrame = first;
                    }
                    else
                    {
                        if (--CurrentFrame < first) CurrentFrame = last - 1;
                    }

                    if (stop && CurrentFrame == stopFrame)
                    {
                        stop = false;
                        this.animate(stopFrame, stopFrame+1);
                    }
                
            }
            base.DrawArea = area[CurrentFrame];
        }

        public void stopanimationAt(int frame)
        {           
                stop = true;
                stopFrame = frame;           
        }

       

        public override object Clone()
        {
            AnimatedSprite ret = new AnimatedSprite(texture, Row, Column, Position, animatedtime);
            return ret;
        }

        public override void Draw(GameTimerEventArgs gameTime, SpriteBatch batch)
        {
            base.Draw(gameTime, batch);
        }

    }
}

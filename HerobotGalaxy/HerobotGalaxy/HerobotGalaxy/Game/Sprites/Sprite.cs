using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections;
using System.Collections.Generic;
using HerobotGalaxy.Game.Other;


namespace HerobotGalaxy.Game.Sprites
{
    /// <summary>
    /// This class handle the simple 2D Sprite System. </p>
    /// The sprite has no animation.
    /// </summary>
    public class Sprite
    {
        #region Properties

        public enum SpriteEnum { Font, Image };

        public SpriteEnum Type { get; set; }
        public bool IsVisible { get; set; }

        public TimeSpan LifeTime { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public SpriteFont SpriteFont { get; set; }
        public String Text { get; set; }
        public Texture2D Texture2D { get; set; }

        public float Rotation { get; set; }

        public Rectangle RecArea { get; set; }

        public float Width
        {
            get;
            set;
        }

        public float Height
        {
            get;
            set;
        }

        public Vector2 Scale { get; set; }
        public float XScale
        {
            get { return this.Scale.X; }
            set { this.Scale = new Vector2(value, Scale.Y); }
        }
        public float YScale
        {
            get { return this.Scale.Y; }
            set { this.Scale = new Vector2(Scale.X, value); }
        }

        public Vector2 Position { get; set; }
        public float X
        {
            get { return Position.X; }
            set { Position = new Vector2(value, Position.Y); }
        }
        public float Y
        {
            get { return Position.Y; }
            set { Position = new Vector2(Position.X, value); }
        }

        public Vector2 Origin { get; set; }
        public float XOrigin
        {
            get { return Origin.X; }
            set { Origin = new Vector2(value, Origin.Y); }
        }
        public float YOrigin
        {
            get { return Origin.Y; }
            set { Origin = new Vector2(Origin.X, value); }
        }

        public float Depth { get; set; }
        public SpriteEffects Effects { get; set; }
        public Color Color { get; set; }
        public virtual Rectangle? DrawArea { get; set; }

        #endregion

        #region Initialization

        #region Text
        public Sprite(string text, SpriteFont font, Vector2 position) :
            this(text, font, position, Color.Black, TimeSpan.Zero) { }

        public Sprite(string text, SpriteFont font, Vector2 position, Color color, TimeSpan lifeTime)
        {
            this.Text = text;
            this.SpriteFont = font;
            this.Position = position;
            this.Color = color;
            this.Type = SpriteEnum.Font;
            this.LifeTime = lifeTime;
            this.RecArea = new Rectangle((int)position.X, (int)position.Y, (int)font.MeasureString(text).X, (int)font.LineSpacing);
        }
        #endregion

        #region Picture
        public Sprite(Texture2D texture) :
            this(texture, Vector2.Zero, Color.White) { }

        public Sprite(Texture2D texture, Vector2 position, Color color) :
            this(texture, position, color, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f) { }

        public Sprite(
            Texture2D texture,
            Vector2 position,
            Color color,
            float rotation,
            Vector2 origin,
            Vector2 scale,
            SpriteEffects effects,
            float depth) :
            this(texture, position, color, rotation, origin, scale, effects, depth, null, TimeSpan.Zero)
        {
        }

        public Sprite(
            Texture2D texture,
            Vector2 position,
            Color color,
            float rotation,
            Vector2 origin,
            Vector2 scale,
            SpriteEffects effects,
            float depth,
            Rectangle? drawArea, TimeSpan lifeTime)
        {
            this.Texture2D = texture;
            this.Position = position;
            this.Color = color;
            this.Rotation = rotation;
            this.Origin = origin;
            this.Scale = scale;
            this.Effects = effects;
            this.Depth = depth;
            this.DrawArea = drawArea;
            this.Type = SpriteEnum.Image;
            this.LifeTime = lifeTime;
            this.RecArea = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            
        }
        #endregion


        #endregion

        #region Method(s)

        public virtual bool EventTap(TouchPointInput touch) {
            if (touch.OnReleased && this.RecArea.Contains((int) touch.releaseLocation.X, (int) touch.releaseLocation.Y)) {
                return true;
            }
            return false;
        }

        public virtual bool IsCollided(Sprite sprite)
        {
            return (sprite.RecArea.Intersects(this.RecArea));
        }

        public virtual bool IsCollided(Rectangle rectangle)
        {
            return (rectangle.Intersects(this.RecArea));
        }


        public virtual bool IsValid(GameTimerEventArgs gameTime)
        {
            // If LifeTime == 0, always valid //
            if (LifeTime.Equals(TimeSpan.Zero))
            {
                return true;
            }
            // If LifeTime already passed, return false //
            if ((gameTime.ElapsedTime.Seconds - StartTime.Seconds) > LifeTime.Seconds)
            {
                return false;
            }

            // Default //
            return true;
        }

        public virtual void Update(GameTimerEventArgs gameTime)
        {

        }



        public virtual void Draw(GameTimerEventArgs gameTime, SpriteBatch batch)
        {

            if (Type == SpriteEnum.Image)
            {
                batch.Draw(
                    this.Texture2D,
                    this.Position,
                    this.DrawArea,
                    this.Color,
                    this.Rotation,
                    this.Origin,
                    this.Scale,
                    this.Effects,
                    this.Depth
                    );
            }
            else if (Type == SpriteEnum.Font)
            {
                batch.DrawString(SpriteFont, Text, Position, Color);
            }

        }

        #endregion

        public virtual object Clone()
        {
            Sprite sprite = new Sprite(Texture2D, Position, Color, Rotation, Origin, Scale, SpriteEffects.None, Depth);

            return sprite;
        }



    }
}

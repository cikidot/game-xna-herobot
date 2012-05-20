using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HerobotGalaxy.Game.Other;


namespace HerobotGalaxy.Game.Sprites
{

    class SpriteList
    {

        public List<Sprite> spriteList;
        SpriteBatch spriteBatch;
        const int DEFAULT_SIZE = 1000;

        public SpriteList() {
            this.spriteList = new List<Sprite>(DEFAULT_SIZE);
        }

        public SpriteList(int size) {
            this.spriteList = new List<Sprite>(size);
        }

        public SpriteList(SpriteBatch spriteBatch)
        {
            spriteList = new List<Sprite>(DEFAULT_SIZE);
            this.spriteBatch = spriteBatch;
        }
        public SpriteList(SpriteBatch spriteBatch, int size)
        {
            spriteList = new List<Sprite>(size);
            this.spriteBatch = spriteBatch;
        }

        public void AddSprite(Sprite sprite)
        {
            spriteList.Add(sprite);
            
        }
        public void RemoveSprite(Sprite sprite)
        {
            spriteList.Remove(sprite);
        }

        public bool RemoveSpriteAt(int index) {
            if (spriteList.Count - 1 < index) {
                return false;
            }
            spriteList.RemoveAt(index);
            return true;
        }

        public void Clear()
        {
            spriteList.Clear();
        }

        public void Draw(GameTimerEventArgs gameTime)
        {
            
            for (int n = 0; n < spriteList.Count; n++)
            {
                if (spriteList[n].IsValid(gameTime))
                {
                    spriteList[n].Draw(gameTime, spriteBatch);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTimerEventArgs gameTime) {
            
            for (int n = 0; n < spriteList.Count; n++)
            {
                if (spriteList[n].IsValid(gameTime))
                {
                    spriteList[n].Draw(gameTime, spriteBatch);
                }
            }
        }

        public void Update(GameTimerEventArgs gameTime) {
           
            for (int n = 0; n < spriteList.Count; n++)
            {
                spriteList[n].Update(gameTime);
            }
        }
       
        public void UpdateAndDraw(GameTimerEventArgs gameTime)
        {
            
            for (int n = 0; n < spriteList.Count; n++)
            {
                spriteList[n].Update(gameTime);
                if (spriteList[n].IsValid(gameTime))
                {
                    spriteList[n].Draw(gameTime, spriteBatch);
                }
            }
        }

    }
}

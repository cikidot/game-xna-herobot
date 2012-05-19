using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace HerobotGalaxy.Game.Sprites
{
    public class TextureAtlas
    {
        const int DEFAULT_SIZE = 123;

        private List<List<Rectangle>> frames;
        public Texture2D Texture { get; private set; }
        public int Count { get { return frames.Count; } }

        int width;
        int height;
        int column;
        int row;

        #region Initialize

        public TextureAtlas(Texture2D texture) : this(texture, 1, 1) { }

        public TextureAtlas(Texture2D texture, int row, int column)
        {
            this.frames = new List<List<Rectangle>>(row);
            this.Texture = texture;
            this.width = texture.Width/column;
            this.height = texture.Height/row;
            // Add one by one row //
            for (int y = 0; y < row; y++)
            {
                frames.Add(new List<Rectangle>(column));
                for (int x = 0; x < column; x++)
                {
                    frames[y].Add(new Rectangle(x * width, y * height, width, height));
                }
            }

        }

        #endregion

        #region Method(s)

        public Rectangle GetFrame(int row, int column)
        {            
            return frames[row][column];
        }

        public int GetRowFrame(int row) {
            return frames[row].Count;
        }

        

        #endregion
    }
}
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
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HerobotGalaxy.Game.Sprites
{
    public class TextureDict
    {
        const int DEFAULT_SIZE = 1234;
        
        Dictionary<string, Texture2D> dict;

        public TextureDict() {
            dict = new Dictionary<string, Texture2D>(DEFAULT_SIZE);
        }

        public TextureDict(int size) {
            dict = new Dictionary<string, Texture2D>(size);
        }

        public bool Add(string input, Texture2D texture) {
            if (dict.ContainsKey(input)) {
                return false;
            }
            dict.Add(input, texture);
            return true;
        }

        

        public List<Texture2D> GetAllTexture() {
            List<Texture2D> list = new List<Texture2D>(dict.Count);
            foreach (KeyValuePair<string, Texture2D> key in dict) {
                list.Add(key.Value);
            }
            return list;
        }

        public Texture2D Get(string input) {
            return dict[input];
        }

        public void Set(string input, Texture2D texture) {
            if (dict.ContainsKey(input))
            {
                dict[input] = texture;
            }
            else {
                dict.Add(input, texture);
            }
        }

        public void Clear() {
            dict.Clear();
        }

        public bool Remove(string input) {
            return dict.Remove(input);
        }

    }
}

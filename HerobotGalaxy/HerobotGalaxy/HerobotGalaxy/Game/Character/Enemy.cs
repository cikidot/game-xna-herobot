using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using HerobotGalaxy.Game.Physics;
using HerobotGalaxy.Game;
using HerobotGalaxy.Game.Sprites;


namespace HerobotGalaxy.Game
{
    public class Enemy : AnimatedSprite
    {
        #region EnemyInitialCondition
        // Data parameter : Type, HitPoint, Velocity //
        static Dictionary<string, Tuple<int, float, Vector2>> EnemyDict = new Dictionary<string, Tuple<int, float, Vector2>>() { 
            {"Enemy_Ball", new Tuple<int, float, Vector2>(30, 0.5f, new Vector2(800, 410))}
        };
        
        
        
        #endregion


        float HitPoint { get; set; }
        float Velocity { get; set; }
        
        bool IsAlive
        {
            get { if (HitPoint > 0.0f) return true; else return false; }
            set { }
        }

        bool IsStunned
        {
            get;
            set;
        }

        public Enemy(string type, Texture2D texture)
            : base(texture, 1, 1, EnemyDict[type].Item3,100) 
        {
            this.HitPoint = EnemyDict[type].Item1;
            this.Velocity = EnemyDict[type].Item2;
            this.IsStunned = false;
        }


        public override void Update(GameTimerEventArgs gameTime)
        {
            // Animate same way with normal AnimatedSprite //
            //base.Update(gameTime);
            
            if (IsAlive && !IsStunned) {                
                this.X = this.X-Velocity;
                Debug.WriteLine("Moved from"+Position);
            }
        }
        
    }
}

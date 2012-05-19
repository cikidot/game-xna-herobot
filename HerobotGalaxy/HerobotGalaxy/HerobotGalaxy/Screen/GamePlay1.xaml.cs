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
using HerobotGalaxy.Game.Other;

namespace HerobotGalaxy.Screen
{
    public partial class GamePlay1 : PhoneApplicationPage
    {
        ContentManager contentManager;
        GameTimer timer;
        GameTime gameTime;
        SpriteBatch spriteBatch;

        //Input
        TouchPointInput touchPointInput;

        //Projectile
        ProjectileBase projectileBase;
        Projectile projectile;
        Bar powerbar;

        /* UI Renderer */
        UIElementRenderer hudRenderer;

        #region GamePlay1
        public GamePlay1()
        {
            InitializeComponent();

            // Get the content manager from the application
            contentManager = (Application.Current as App).Content;

            // Create a timer for this page
            timer = new GameTimer();
            timer.UpdateInterval = TimeSpan.FromTicks(333333);
            timer.Update += OnUpdate;
            timer.Draw += OnDraw;

            // Event binding for XNA Rendering //
            LayoutUpdated += XNARendering;
        }
        #endregion

        #region OnNavigatedTo
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Set the sharing mode of the graphics device to turn on XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(true);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(SharedGraphicsDeviceManager.Current.GraphicsDevice);

            // TODO: use this.content to load your game content here
            Init();
            // Start the timer
            timer.Start();

            base.OnNavigatedTo(e);
        }
        #endregion

        #region OnNavigatedFrom
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Stop the timer
            timer.Stop();

            // Set the sharing mode of the graphics device to turn off XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(false);

            base.OnNavigatedFrom(e);
        }
        #endregion

        /// <summary>
        /// Allows the page to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        private void OnUpdate(object sender, GameTimerEventArgs e)
        {
            touchPointInput.update();
            projectileBase.onUpdate(e);
            if (touchPointInput.onReleased && projectile != null)
            {
                /*
                //Debug.WriteLine("masuk update");
                projectile.setVelocity(powerbar.metervalue * 250 / 48.0f);
                powerbar.setStop();
                //Debug.WriteLine(powerbar.metervalue * 250 / 48.0f + " " + powerbar.metervalue);
                projectile.setStart(touchPointInput.getDegree());
                */
                projectileBase.launchProjectile(touchPointInput.getDegree());
            }
            if (touchPointInput.onPressed)
            {
                projectileBase.prepareProjectile();
                //powerbar.setStart();
            }

        }

        #region onDraw
        /// <summary>
        /// Allows the page to draw itself.
        /// </summary>
        private void OnDraw(object sender, GameTimerEventArgs e)
        {
            SharedGraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            // Must call HUDRenderer //
            LayoutUpdated += XNARendering;
            hudRenderer.Render();

            spriteBatch.Begin();
            spriteList.UpdateAndDraw(e);
            spriteBatch.End();

        }
        #endregion

        #region XNARendering
        /* Additional For XNA Rendering */
        private void XNARendering(object sender, EventArgs e)
        {
            if (hudRenderer != null &&
            hudRenderer.Texture != null &&
            hudRenderer.Texture.Width == (int)ActualWidth &&
            hudRenderer.Texture.Height == (int)ActualHeight)
            {
                return;
            }

            if (hudRenderer != null)
                hudRenderer.Dispose();

            if (ActualHeight > 0 || ActualWidth > 0)
            {
                hudRenderer = new UIElementRenderer(this, (int)ActualWidth, (int)ActualHeight);
            }
        }
        #endregion

        #region Init
        /* Init */
        SpriteList spriteList;
        TextureDict textDict;
        public void Init()
        {
            spriteList = new SpriteList(spriteBatch);
            textDict = new TextureDict();

            textDict.Add("box_debug", contentManager.Load<Texture2D>("Image/Debug/box_debug"));
            textDict.Add("block_face", contentManager.Load<Texture2D>("Image/Debug/block_face"));
            textDict.Add("bar", contentManager.Load<Texture2D>("Image/bar/bar"));
            touchPointInput = new TouchPointInput(150, 100);
            StartGame();

        }
        #endregion

        /* Draw */


        public void StartGame()
        {

            /*
            Sprite test;
            LinearEquation linear = new LinearEquation(40, 40, 100, 200);
            List<Vector2> tempList = linear.GetVectorPoint(20.0f);
            for (int n = 0; n < tempList.Count; n++) {
                spriteList.AddSprite(new Sprite(textDict.Get("box_debug"), tempList[n], Color.White));
            }
            spriteList.AddSprite(new AnimatedSprite(textDict.Get("block_face"), 30, 10, new Vector2(500, 100)));
            GravityProjection pro1 = new GravityProjection(new Vector2(200, 200),50,-9.8f,30,0);
            List<Tuple<Vector2, float>> tempList2 = new List<Tuple<Vector2, float>>(123);
            tempList2 = pro1.GetMotionVector(1.0f);
            Debug.WriteLine("Debug Start");
            Debug.WriteLine("Length "+tempList2.Count);
            for (int n = 0; n < tempList2.Count; n++)
            {
                spriteList.AddSprite(new Sprite(textDict.Get("box_debug"), tempList2[n].Item1, Color.White));
                Debug.WriteLine(tempList2[n].Item1.ToString()+" "+tempList2[n].Item2);
            }
             */

            powerbar = new Bar(textDict.Get("bar"), new Vector2(100, 100), SharedGraphicsDeviceManager.Current.GraphicsDevice);
            spriteList.AddSprite(powerbar);
            projectileBase = new ProjectileBase(50, 250, powerbar);
            for (int i = 0; i < 50; i++)
            {
                projectile = new Projectile(textDict.Get("box_debug"), new Vector2(-100, -100), Color.Black);
                spriteList.AddSprite(projectile);
                projectileBase.addProjectile(projectile);
            }
        }

    }
}
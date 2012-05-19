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
using HerobotGalaxy.Game.Database;

namespace HerobotGalaxy.Screen
{
    public partial class MainGame : PhoneApplicationPage
    {
        ContentManager contentManager;
        GameTimer timer;

        SpriteBatch spriteBatch;

        //Game control //
        public static bool _IsPlay = true;
        public static bool IsPlay { get { return _IsPlay; } set { _IsPlay = value; } }

        //Input
        TouchPointInput touchPointInput;

        //Projectile
        ProjectileBase projectileBase;
        Projectile projectile;
        Bar powerbar;

        /* UI Renderer */
        UIElementRenderer hudRenderer;

        #region MainMenu
        public MainGame()
        {
            InitializeComponent();

            // Get the content manager from the application
            contentManager = (Application.Current as App).Content;

            // Create a timer for this page
            timer = new GameTimer();
            timer.UpdateInterval = TimeSpan.FromTicks(333333);
            timer.Update += OnUpdate;
            timer.Draw += OnDraw;


            Application.Current.Host.Settings.EnableFrameRateCounter = true;


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
            InitResources();
            // Start the timer
            timer.Start();

            // After all of resources load and timer start, StartGame //

            InitGame();

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
                //debug.writeline("masuk update");
                projectile.setvelocity(powerbar.metervalue * 250 / 48.0f);
                powerbar.setstop();
                //debug.writeline(powerbar.metervalue * 250 / 48.0f + " " + powerbar.metervalue);
                projectile.setstart(touchpointinput.getdegree());
                */
                projectileBase.launchProjectile(touchPointInput.getDegree());
            }
            if (touchPointInput.onPressed)
            {
                projectileBase.PrepareProjectile();
                //powerbar.setstart();
            }

            // Summon enemy //
            if (IsPlay)
            {
                spriteList.Update(e);
            }
            else {
                pauseSpriteList.Update(e);
            }

            SummonEnemyAtTime(e);

        }

        #region OnDraw
        /// <summary>
        /// Allows the page to draw itself.
        /// </summary>
        private void OnDraw(object sender, GameTimerEventArgs e)
        {
            SharedGraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.Aqua);

            // TODO: Add your drawing code here
            // Must call HUDRenderer //
            //LayoutUpdated += XNARendering;
            hudRenderer.Render();

            spriteBatch.Begin();
            if (IsPlay)
            {
                spriteList.Draw(e);
            }
            else {
                pauseSpriteList.Draw(e);
            }

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
        List<Tuple<string, TimeSpan>> EnemySpawnTime;
        SpriteList pauseSpriteList;
        public void InitResources()
        {
            // Init SpriteList //
            spriteList = new SpriteList(spriteBatch);
            // Init All Texture //
            textDict = new TextureDict();

            textDict.Add("box_debug", contentManager.Load<Texture2D>("Image/Debug/box_debug"));
            textDict.Add("block_face", contentManager.Load<Texture2D>("Image/Debug/block_face"));
            textDict.Add("bar", contentManager.Load<Texture2D>("Image/Bar/bar"));
            textDict.Add("Enemy_Ball", contentManager.Load<Texture2D>("Image/Character/Enemy_Ball"));
            textDict.Add("Enemy_Tube", contentManager.Load<Texture2D>("Image/Character/Enemy_Tube"));
            textDict.Add("hero", contentManager.Load<Texture2D>("Image/Character/hero"));
            textDict.Add("plane", contentManager.Load<Texture2D>("Image/Character/baseplane"));
            textDict.Add("nut", contentManager.Load<Texture2D>("Image/Other/nut_real"));
            textDict.Add("background", contentManager.Load<Texture2D>("Image/Background/BG_play_witP"));

            textDict.Add("pause_bg", contentManager.Load<Texture2D>("Image/Background/pause-bg"));
            textDict.Add("pausebtn_close", contentManager.Load<Texture2D>("Image/Button/pausebtn_close"));
            textDict.Add("pausebtn_restart", contentManager.Load<Texture2D>("Image/Button/pausebtn_restart"));
            textDict.Add("pausebtn_resume",contentManager.Load<Texture2D>("Image/Button/pausebtn_resume"));
            textDict.Add("pausebtn_button", contentManager.Load<Texture2D>("Image/Button/pausebtn_button"));
            touchPointInput = new TouchPointInput(150, 100);

            // Init Pause Sprite List //
            pauseSpriteList = new SpriteList(spriteBatch);

            pauseSpriteList.AddSprite(new Sprite(textDict.Get("pause_bg"), Vector2.Zero,Color.White));
            pauseSpriteList.AddSprite(new Sprite(textDict.Get("pausebtn_close"), Vector2.Zero, Color.White));
            pauseSpriteList.AddSprite(new Sprite(textDict.Get("pausebtn_restart"), Vector2.Zero, Color.White));
            pauseSpriteList.AddSprite(new Sprite(textDict.Get("pausebtn_resume"), Vector2.Zero, Color.White));
            //pauseSpriteList.AddSprite(new Sprite(textDict.Get("pausebtn_button"), new Vector2(360, 10), Color.White));

            // Generate enemy spawn time //
            EnemySpawnTime = DataRandom.GetEnemySpawnAtLevel(1);

        }
        #endregion

        /* Draw */


        public void InitGame()
        {
            

            Sprite backsprite = new Sprite(textDict.Get("background"));
            spriteList.AddSprite(backsprite);
            spriteList.AddSprite(new Sprite(textDict.Get("pausebtn_button")));
            powerbar = new Bar(textDict.Get("bar"), new Vector2(100, 100), SharedGraphicsDeviceManager.Current.GraphicsDevice);
            spriteList.AddSprite(powerbar);
            AnimatedSprite hero = new AnimatedSprite(textDict.Get("hero"), 1, 7, new Vector2(50, 200), 100);
            spriteList.AddSprite(hero);
            AnimatedSprite plane = new AnimatedSprite(textDict.Get("plane"), 1, 5, new Vector2(10, 300), 100);
            spriteList.AddSprite(plane);
            
            

            projectileBase = new ProjectileBase(90, 240, powerbar, hero, plane);
            for (int i = 0; i < 1; i++)
            {
                projectile = new Projectile(textDict.Get("nut"), new Vector2(-100, -100), Color.White);
                spriteList.AddSprite(projectile);
                projectileBase.AddProjectile(projectile);
            }


        }

        public void StartGame() {
            IsPlay = true;
        }
        public void PauseGame() {
            IsPlay = false;
            // Put transparent //
            
        }

        int EnemyListIdx = 0;
        public void SummonEnemyAtTime(GameTimerEventArgs e)
        {

            if (EnemyListIdx < EnemySpawnTime.Count && e.TotalTime > EnemySpawnTime[EnemyListIdx].Item2)
            {
                spriteList.AddSprite(new Enemy(EnemySpawnTime[EnemyListIdx].Item1, textDict.Get("Enemy_Ball")));

                //Debug.WriteLine("Spawn New Enemy at " + e.TotalTime + " " + EnemySpawnTime[EnemyListIdx].Item2.TotalSeconds);
                EnemyListIdx++;
            }
        }

    }
}
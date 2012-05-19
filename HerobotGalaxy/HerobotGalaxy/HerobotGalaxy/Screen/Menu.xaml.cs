using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Windows.Navigation;

namespace HerobotGalaxy.Screen
{
    public partial class Menu : PhoneApplicationPage
    {
        public bool IsClick = false;
        GameTimer timer;

        public Menu()
        {
            InitializeComponent();
            timer = new GameTimer();
            timer.UpdateInterval = TimeSpan.FromTicks(333333);
            timer.Update += OnUpdate;

            this.image_PlayGame.Tap += new EventHandler<GestureEventArgs>(image_PlayGame_Tap);
        }

        protected void image_PlayGame_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Screen/MainGame.xaml", UriKind.RelativeOrAbsolute)); 
        }

        #region OnNavigatedTo
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
          
            timer.Start();

            base.OnNavigatedTo(e);
        }
        #endregion

        #region OnNavigatedFrom
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Stop the timer
            timer.Stop();

            
            base.OnNavigatedFrom(e);
        }
        #endregion

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //base.OnBackKeyPress(e);
            if (this.NavigationService.CanGoBack)
            {
                while (this.NavigationService.RemoveBackEntry() !=null)
                {
                    //this.NavigationService.RemoveBackEntry();
                }
            }
        }

        private void OnUpdate(object sender, GameTimerEventArgs e)
        {
           
        }


        
       
    }
}
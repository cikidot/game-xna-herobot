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

namespace HerobotGalaxy.Screen
{
    public partial class SplashScreen : PhoneApplicationPage
    {
        public SplashScreen()
        {
            InitializeComponent();
            MoveToMainMenu();
        }

        public void MoveToMainMenu() {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 3);
            timer.Tick += (delegate { 
                NavigationService.Navigate(new Uri("/Screen/Menu.xaml", UriKind.RelativeOrAbsolute));
                timer.Stop();
            });
            timer.Start();
        }

        private void image_SplashScreen_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }
    }
}
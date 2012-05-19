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

namespace HerobotGalaxy.Game
{
    public class Tuple<T1, T2>
    {
        public T1 Item1 {get; set;}
        public T2 Item2 {get; set;}

        public Tuple(T1 Item1, T2 Item2) {
            this.Item1 = Item1;
            this.Item2 = Item2;
        }

    }
    public class Tuple<T1, T2, T3>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public Tuple(T1 Item1, T2 Item2, T3 Item3)
        {
            this.Item1 = Item1;
            this.Item2 = Item2;
            this.Item3 = Item3;
        }

    }
}

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
using System.Diagnostics;

namespace HerobotGalaxy.Game.Database
{
    
    
    public class DataRandom
    {
        static double[] MinDelay = {0, 1.25 };
        static double[] MaxDelay = {0, 4.5 };
        static int[] LimitEnemy = {0, 23 };
        const int DEFAULT_SIZE = 1234;
        static List<Tuple<string, TimeSpan>> list = new List<Tuple<string, TimeSpan>>(DEFAULT_SIZE);
        
        static Random rand;

        public static List<Tuple<string, TimeSpan>> GetEnemySpawnAtLevel(int level) {
            list.Clear();
            rand = new Random();
            double temp_d;
            double range = MaxDelay[level]-MinDelay[level];

            list.Add(new Tuple<string, TimeSpan>("Enemy_Ball", TimeSpan.Zero));
            for (int nn = 1; nn<LimitEnemy[level]; nn++) {
                temp_d = (rand.NextDouble() * (range)+MinDelay[level]) + list[nn-1].Item2.TotalSeconds;
                Debug.WriteLine("temp d : " + temp_d);
                list.Add(new Tuple<string, TimeSpan>("Enemy_Ball", new TimeSpan(Sec100NS(temp_d))));
            }

            return list;
        }

        public static long Sec100NS(double input) {
            return (long) ((double)input* (double)10000000);
        }

        public static double NS100Sec(long input) {
            return (double)input / (double)10000000;
        }
    }
}

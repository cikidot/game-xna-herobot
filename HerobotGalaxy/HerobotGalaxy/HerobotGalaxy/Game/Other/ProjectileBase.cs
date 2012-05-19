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
using HerobotGalaxy.Game.Sprites;

namespace HerobotGalaxy.Game.Other
{
    public class ProjectileBase
    {
        Bar powerbar;
        Vector2 position;
        bool wait = false;
        bool isLaunching = false;
        float time = 0;
        Projectile projectile;
        AnimatedSprite hero;
        AnimatedSprite plane;

        public ProjectileBase(float x, float y, Bar powerbar, AnimatedSprite hero, AnimatedSprite plane)
        {
            time = 0;
            this.powerbar = powerbar;
            position = new Vector2(x, y);
            this.hero = hero;
            this.plane = plane;
            hero.increment = false;
            hero.animate(7, 7);
        }

        public void AddProjectile(Projectile projectile)
        {
            this.projectile = projectile;
            this.projectile.onDecay = true;
        }

        public void onUpdate(GameTimerEventArgs e)
        {
            if (wait) time += (float)e.ElapsedTime.TotalSeconds;
            if (time > 0.5)
            {
                time = 0;
                wait = false;
            }

        }

        public void launchProjectile(float Degree)
        {
            if (!isLaunching) return;
            projectile.setVelocity(powerbar.metervalue * 400 / 48.0f);
            powerbar.setStop();
            projectile.SetStart(Degree);
            wait = true;
            isLaunching = false;
            hero.animate(7, 7);
            //projectile = null;
        }

        public void PrepareProjectile()
        {
            if (!wait && projectile.onDecay)
            {
                isLaunching = true;
                powerbar.setStart();
                projectile.Renew();
                projectile.Position = projectile.startVec = this.position;
                hero.animate(0, 7);
                hero.stopanimationAt(0);
            }
        }
    }
}

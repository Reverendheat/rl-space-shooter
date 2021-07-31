using System;
using System.Collections.Generic;
using Raylib_Adventures.Entities;
using Raylib_Adventures.Interfaces;
using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Timers;

namespace Raylib_Adventures
{
    public class EnemySpawner: ISpawner
    {
        public List<Enemy> Enemies = new List<Enemy>();
        public Timer e_timer; 
        public Random r = new Random();

        public void SetupTimer(double d)
        {
            e_timer = new Timer(d);
            e_timer.Elapsed += Spawn;
            e_timer.Enabled = true;
        }
        public void Spawn(Object source, ElapsedEventArgs e)
        {
            float rf = (float)r.Next(0,GetScreenWidth());
            Enemy en = new Enemy();
            en.Sprite = new Rectangle(rf, -15f,en.Width, en.Height);
            Enemies.Add(en);
        }
    }
}

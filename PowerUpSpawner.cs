using System;
using System.Collections.Generic;
using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Timers;
using Raylib_Adventures.Interfaces;
using Raylib_Adventures.Entities;


namespace Raylib_Adventures
{
    public class PowerUpSpawner:ISpawner
    {
        public List<PowerUp> PowerUps = new List<PowerUp>();
        public Timer p_timer;
        public Random r = new Random();

        public void SetupTimer(double d)
        {
            p_timer = new Timer(d);
            p_timer.Elapsed += Spawn;
            p_timer.Enabled = true;
        }
        public void Spawn(Object source, ElapsedEventArgs e)
        {
            float rf = (float)r.Next(0, GetScreenWidth());
            int rp = r.Next(0, 2);
            PowerUp pu = new PowerUp();
            pu.PowerType = pu.Enhancement[rp];
            pu.UpdateColor();
            pu.Sprite = new Rectangle(rf, -15f, pu.Width, pu.Height);
            PowerUps.Add(pu);
            Console.WriteLine($"Spawing Power up with type {pu.PowerType}");
        }
    }
}

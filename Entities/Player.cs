using System;
using System.Collections.Generic;
using System.Numerics;
using System.Timers;
using Raylib_cs;
using static Raylib_cs.Color;
using static Raylib_cs.Raylib;

namespace Raylib_Adventures.Entities
{
    public class Player
    {
        public float BaseSpeed { get; set; } = 4.0f;
        public float CurrentSpeed { get; set; } = 4.0f;
        public int Health { get; set; } = 3;
        public Color Sprite_Color { get; set; } = BLUE;
        public Rectangle Sprite { get; set; }
        public List<Bullet> Bullets { get; set; } = new List<Bullet>();
        public int Height { get; set; }
        public int Width { get; set; }
        public int Score { get; set; } = 0;
        public Timer p_timer { get; set; } = new Timer(1000);
        public int PowerUpResetDuration { get; set; } = 5;
  
        public void Move(string direction)
        {
            var px = Sprite.x;
            var py = Sprite.y;
            switch (direction)
            {
                case "LEFT":
                    px -= CurrentSpeed;
                    break;
                case "RIGHT":
                    px += CurrentSpeed;
                    break;
                case "UP":
                    py -= CurrentSpeed;
                    break;
                case "DOWN":
                    py += CurrentSpeed;
                    break;
            }

            Sprite = new Rectangle(px, py, Width, Height);
            KeepWithinBounds();
        }

        public void Shoot()
        {
            Bullet b = new Bullet();
            b.Sprite = new Rectangle(Sprite.x + 17, Sprite.y, b.Width, b.Height);
            Bullets.Add(b);
        }

        public void TakeDamage(int d)
        {
            Health -= d;
            switch (Health)
            {
                case 2:
                    Sprite_Color = ORANGE;
                    break;
                case 1:
                    Sprite_Color = RED;
                    break;
                default:
                    Sprite_Color = BLUE;
                    break;
            }
        }
        public void PowerUpCountDown(Object source, ElapsedEventArgs e)
        {
            PowerUpResetDuration -= 1;
            if (PowerUpResetDuration <= 0) {
                PowerUpReset();
            }
        }

        public void AcquirePowerUp(PowerUp pu)
        {
            switch (pu.PowerType)
            {
                case "Speed":
                    BaseSpeed = 8.0f;
                    break;
                case "BulletSize":
                    Console.WriteLine("IDK YET");
                    break;
            }
            PowerUpResetDuration = 5;
            p_timer.Elapsed += PowerUpCountDown;
            p_timer.Enabled = true;
        }

        public void PowerUpReset()
        {
            p_timer.Enabled = false;
            BaseSpeed = 4.0f;
        }



        public void KeepWithinBounds()
        {
            float px = Sprite.x;
            float py = Sprite.y;
            if (Sprite.x >= (GetScreenWidth() - Width))
            {
                px -= 9;
            }
            else if (Sprite.y >= (GetScreenHeight() - Width))
            {
                py -= 9;
            }
            else if (Sprite.x <= 0) {
                px += 9;
            }
            else if (Sprite.y <= 0)
            {
                py += 9;
            }
            Sprite = new Rectangle(px, py, Width, Height);
        }

    }
}

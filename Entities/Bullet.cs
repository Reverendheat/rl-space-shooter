using System;
using System.Collections.Generic;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Raylib_Adventures.Entities
{
    public class Bullet
    {
        public Rectangle Sprite { get; set; }
        public int Speed { get; set; }
        public int Width { get; set; } = 15;
        public int Height { get; set; } = 15;

        public bool IsOnScreen()
        {
            if (Sprite.x >= GetScreenWidth())
            {
                return false;
            }
            else if (Sprite.y >= GetScreenHeight())
            {
                return false;
            }
            else if (Sprite.x <= 0)
            {
                return false;
            }
            else if (Sprite.y <= 0)
            {
                return false;
            }
            return true;
        }
    }
}

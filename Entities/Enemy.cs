using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Raylib_Adventures.Entities
{
    public class Enemy
    {
        public int Speed { get; set; } = 4;
        //public Vector2 Speed { get; set; };
        public Rectangle Sprite { get; set; }
        public int Height { get; set; } = 40;
        public int Width { get; set; } = 40;

        public void Move()
        {
            Sprite = new Rectangle(Sprite.x, Sprite.y + Speed, Width, Height);
        }
    }
}

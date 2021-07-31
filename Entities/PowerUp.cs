using System;
using System.Collections.Generic;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Raylib_Adventures.Entities
{
    public class PowerUp
    {
        public List<string> Enhancement = new List<string> {
            "Speed",
            "BulletSize",
        };
        public Color s_color { get; set; }
        public int Speed { get; set; } = 6;
        public Rectangle Sprite { get; set; }
        public int Height { get; set; } = 30;
        public int Width { get; set; } = 30;
        public string PowerType { get; set; }

        public void Move()
        {
            Sprite = new Rectangle(Sprite.x, Sprite.y + Speed, Width, Height);
        }

        public void UpdateColor()
        {
            if (PowerType == "Speed")
            {
                s_color = Color.GREEN;
            } else if (PowerType == "BulletSize")
            {
                s_color = Color.GOLD;
            }
        }
    }
}

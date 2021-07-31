using Raylib_cs;  
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static Raylib_cs.KeyboardKey;

using Raylib_Adventures.Entities;

namespace Raylib_Adventures
{
    public class RaylibSpaceShooter
    {
        public static int Main()
        {
            // Initialization
            //---------------------------------------------------------
            const int screenWidth = 1200;
            const int screenHeight = 700;
            var ES = new EnemySpawner();
            var PS = new PowerUpSpawner();
            PS.SetupTimer(10000);
            ES.SetupTimer(3000);
            var P1 = new Player();
            P1.Height = 50;
            P1.Width = 50;
            
            
            InitWindow(screenWidth, screenHeight, "Space Shooter - Garbage Edition");
            
            P1.Sprite = new Rectangle((GetScreenWidth() / 2) - P1.Width / 2, GetScreenHeight() - (P1.Height + 10), P1.Width, P1.Height);
            
            bool pause = false;
            int framesCounter = 0;

            SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
            //----------------------------------------------------------

            // Main game loop
            while (!WindowShouldClose())    // Detect window close button or ESC key
            {
                // Update
                //-----------------------------------------------------
                if (IsKeyPressed(KEY_P))
                {
                    pause = !pause;
                }

                if (!pause)
                {
                    //Listen for Input
                    if (IsKeyDown(KEY_LEFT)) P1.Move("LEFT");
                    if (IsKeyDown(KEY_RIGHT)) P1.Move("RIGHT");
                    if (IsKeyDown(KEY_UP)) P1.Move("UP");
                    if (IsKeyDown(KEY_DOWN)) P1.Move("DOWN");
                    if (IsKeyPressed(KEY_SPACE)) P1.Shoot();
                    // Check for player Sprinting
                    if (IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT))
                    {
                        P1.CurrentSpeed = P1.BaseSpeed * 2;
                    }
                    else
                    {
                        P1.CurrentSpeed = P1.BaseSpeed;
                    }

                    for (int i = 0; i < ES.Enemies.Count; i++)
                    {
                        ES.Enemies[i].Move();
                    }

                    for (int i = 0; i < PS.PowerUps.Count; i++)
                    {
                        PS.PowerUps[i].Move();
                    }

                    for (int i = 0; i < P1.Bullets.Count; i++)
                    {
                        var by = P1.Bullets[i].Sprite.y - 20;
                        P1.Bullets[i].Sprite = new Rectangle(P1.Bullets[i].Sprite.x, by, P1.Bullets[i].Width, P1.Bullets[i].Height);
                    }

                    //Check for collision
                    for (int e = 0; e < ES.Enemies.Count; e++)
                    {
                        if (CheckCollisionRecs(P1.Sprite, ES.Enemies[e].Sprite))
                        {
                            P1.TakeDamage(1);
                            ES.Enemies.RemoveAt(e);
                        }
                        for (int i = 0; i < P1.Bullets.Count; i++)
                        {
                            if (CheckCollisionRecs(P1.Bullets[i].Sprite, ES.Enemies[e].Sprite))
                            {
                                P1.Bullets.RemoveAt(i);
                                ES.Enemies.RemoveAt(e);
                                P1.Score += 10;
                            }
                        }
                    }
                    for (int p = 0; p < PS.PowerUps.Count; p++)
                    {
                        if (CheckCollisionRecs(P1.Sprite, PS.PowerUps[p].Sprite))
                        {
                            P1.AcquirePowerUp(PS.PowerUps[p]);
                            PS.PowerUps.RemoveAt(p);
                        }
                    }
                }
                else
                {
                    framesCounter += 1;
                }
                //-----------------------------------------------------

                // Draw
                //-----------------------------------------------------
                BeginDrawing();
                ClearBackground(BLACK);
                DrawText("Space Shooter - A Poor Mans Galaga", 10, GetScreenHeight() - 25, 20, LIGHTGRAY);
                DrawText($"Score: {P1.Score}", GetScreenWidth()-150, 25, 20, BLUE);
                //Draw Player Sprite
                DrawRectangleRec(P1.Sprite, P1.Sprite_Color);

                //Draw Player Bullets
                if (P1.Bullets.Count > 0)
                {
                    for (int i = 0; i < P1.Bullets.Count; i++)
                    {
                        if (P1.Bullets[i].IsOnScreen())
                        {
                            DrawRectangleRec(P1.Bullets[i].Sprite, Color.RED);
                        }
                        else
                        {
                            P1.Bullets.RemoveAt(i);
                        }
                    }
                }

                //Draw Enemies
                if (ES.Enemies.Count > 0)
                {
                    for (int i = 0; i < ES.Enemies.Count; i++)
                    {
                        ES.Enemies[i].Move();
                        DrawRectangleRec(ES.Enemies[i].Sprite, Color.RED);
                    }
                }

                //Draw PowerUps
                if (PS.PowerUps.Count > 0)
                {
                    for (int i = 0; i < PS.PowerUps.Count; i++)
                    {
                        PS.PowerUps[i].Move();
                        DrawRectangleRec(PS.PowerUps[i].Sprite, PS.PowerUps[i].s_color);
                    }
                }


                // On pause, we draw a blinking message
                if (pause && ((framesCounter / 30) % 2) == 0)
                {
                    DrawText("PAUSED", (GetScreenWidth() / 2) - 50, (GetScreenHeight() / 2), 30, GRAY);
                } 
                DrawFPS(10, 10);

                EndDrawing();
                //-----------------------------------------------------
            }

            // De-Initialization
            //---------------------------------------------------------
            CloseWindow();        // Close window and OpenGL context
            //----------------------------------------------------------

            return 0;
        }
    }
}

global using Raylib_cs;
using System.Numerics;
using System.Runtime.CompilerServices;

Raylib.InitWindow(1380, 1000, "Koimoúntai");
Raylib.SetTargetFPS(60);
Player player = new();

Body bd = new();
List<Body> eq = new();
eq.Add(new Skateboard());
eq.Add(new Jetpack());
float gravity = 1f;
player.velocity = Vector2.Zero;
Rectangle arm = new Rectangle(600, 600, 16, 3);





while (Raylib.WindowShouldClose() == false)
{
    Raylib.BeginDrawing();
    
    Raylib.DrawRectangleRec(new Rectangle(0, 700, 2000, 3), Color.BLACK);
    if (player.isOn == false)
    {

        player.newHorizontal = bd.Movement();
        player.newVertical = bd.Rise(player);
    }

    player.isOn = false;
    foreach (Body item in eq)
    {
        if (Raylib.CheckCollisionRecs(player.hitbox, item.boundBox))
        {
            if (!player.bodies.Contains(item))
            {
                item.IsOn(player);
                player.isOn = true;
                
                player.bodies.Add(item);
            }
        }
        else
        {
            player.newHorizontal = bd.Movement();
        }


        player.velocity.Y += player.gravity;

    }


    player.Update(bd);

arm.X = player.hitbox.X + 5;
    arm.Y = player.hitbox.Y + 5;

    if (player.hitbox.Y >= player.outOfBoundsY - player.height)
    {
        player.hitbox.Y = player.outOfBoundsY - player.height;
        // player.hitbox.Y -= player.velocity.Y;
        player.velocity.Y = 0;
    }



    Raylib.DrawRectanglePro(arm, new Vector2(0, 2.5f), player.rotation, Color.BLACK);
    

    foreach (Body s in eq)
    {
        s.Draw();
        s.boundBox.Y += player.gravity;
        if (s.boundBox.Y >= player.outOfBoundsY - s.boundBox.Height)
        {
        s.boundBox.Y = 700 - s.boundBox.Height;
        }

        
       
    }

    Raylib.ClearBackground(Color.WHITE);
    player.Draw();
    






    Raylib.EndDrawing();


}


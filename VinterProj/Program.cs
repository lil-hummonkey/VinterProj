global using Raylib_cs;
global using System.Text.Json;
using System.Numerics;
using System.Runtime.CompilerServices;


string lText = File.ReadAllText("level.json");
Level l = JsonSerializer.Deserialize<Level>(lText);
Raylib.InitWindow(1920, 1000, "Koimoúntai");
Raylib.SetTargetFPS(60);
Player player = new();
Body bd = new();
List<Spiderman> spidyWeb = new();
List<Body> eq = new();
eq.Add(new Skateboard());
eq.Add(new Jetpack());
float gravity;
float screenWidth = Raylib.GetScreenWidth();
float screenHeight = Raylib.GetScreenHeight();
float pullSpeed;
player.velocity = Vector2.Zero;
Vector2 webVel = new Vector2();
int checkCol = 0;
int checkObjCol = 0;



while (Raylib.WindowShouldClose() == false)
{
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.SKYBLUE);
    foreach (Body s in eq)
    {
        s.boundBox.Y += s.objGravity;
        checkCol = 0;
        checkObjCol = 0;
        player.gravity = 1;

        foreach (var r in l.boxes)
        {
            if (Raylib.CheckCollisionRecs(new Rectangle(player.hitbox.X + 6, player.hitbox.Y + (player.height * 0.75f) + 5, player.width - 9, player.height / 4), new Rectangle(r.X, r.Y, r.Width, r.Height)))
            {
                player.hitbox.Y = r.Y - player.height;
                player.velocity.Y = 0;
                checkCol++;
            }
            if (Raylib.CheckCollisionRecs(new Rectangle(player.hitbox.X + 3, player.hitbox.Y - (player.height / 2) + 4, player.width - 9, player.height / 4), new Rectangle(r.X, r.Y, r.Width - 3, r.Height + 3)))
            {

                player.hitbox.Y = r.Y + r.Height;

            }
            if (Raylib.CheckCollisionRecs(new Rectangle(player.hitbox.X - 2, player.hitbox.Y - (player.height / 2) + 18, player.width - 8, player.height - 14), new Rectangle(r.X, r.Y, r.Width, r.Height)))
            {
                player.hitbox.X = r.X + r.Width;

            }
            if (Raylib.CheckCollisionRecs(new Rectangle(player.hitbox.X + 10, player.hitbox.Y - (player.height / 2) + 18, player.width - 8, player.height - 14), new Rectangle(r.X, r.Y, r.Width, r.Height)))
            {

                player.hitbox.X = r.X - 14;



            }
            if (Raylib.CheckCollisionRecs(s.boundBox, new Rectangle(r.X, r.Y, r.Width, r.Height)))
            {

                checkObjCol++;
            }

            WebSwingCheck(player, spidyWeb, r);

        }
        if (checkObjCol > 0) s.objGravity = 0;
        else s.objGravity = 1;
        if (checkCol > 0)
        {
            player.isStanding = true;

        }
        else
        {
            player.isStanding = false;
        }
        IsWalking(player, bd);
        OnObject(player, eq);
    }
    if (player.hitbox.Y >= player.outOfBoundsY - player.height)
    {
        player.hitbox.Y = player.outOfBoundsY - player.height;
        player.velocity.Y = 0;
    }
    player.Update(bd);
    Vector2 mouseWorldPos = CameraSetup(player, screenWidth, screenHeight);

    WebShootDirection(player, spidyWeb, webVel, mouseWorldPos);

    if (Raylib.IsKeyPressed(KeyboardKey.KEY_Q))
    {
        spidyWeb.Clear();
    }
    foreach (Body s in eq)
    {
        s.Draw();
    }
    foreach (var r in l.boxes)
    {
        r.Draw();
    }
    foreach (Spiderman b in spidyWeb)
    {
        b.Draw(player);
        b.Update();
    }
    player.Draw();


    Raylib.EndMode2D();
    Raylib.EndDrawing();

}

static void WebShootDirection(Player player, List<Spiderman> spidyWeb, Vector2 webVel, Vector2 mouseWorldPos)
{
    if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
    {
        player.rotation = MathF.Atan2
        (player.hitbox.Y - mouseWorldPos.Y, player.hitbox.X - mouseWorldPos.X) * Raylib.RAD2DEG;
        player.rotation += 180;
        webVel.Y = (float)Math.Sin(player.rotation * Raylib.DEG2RAD) * 20;
        webVel.X = (float)Math.Cos(player.rotation * Raylib.DEG2RAD) * 20;
        spidyWeb.Clear();
        spidyWeb.Add(new Spiderman(new Vector2(player.hitbox.X, player.hitbox.Y), webVel));
    }
    else
    {
        player.rotation = 0;
    }
}

static void IsWalking(Player player, Body bd)
{
    if (player.isOn == false)
    {
        player.newHorizontal = bd.Movement();
        player.newVertical = bd.Rise(player);
    }
    player.isOn = false;
}

static void OnObject(Player player, List<Body> eq)
{
    foreach (Body item in eq)
    {
        if (Raylib.CheckCollisionRecs(player.hitbox, item.boundBox))
        {
            if (!player.bodies.Contains(item))
            {

                player.isOn = true;
                player.bodies.Add(item);
            }
        }
    }
}

static Vector2 CameraSetup(Player player, float screenWidth, float screenHeight)
{
    player.camera.Zoom = 1.2f;
    player.camera.Target = new Vector2(player.hitbox.X, screenHeight / 2);
    player.camera.Offset = new Vector2(screenWidth / 2, screenHeight / 2);
    player.camera.Rotation = 0;
    Raylib.BeginMode2D(player.camera);
    Vector2 mouseScreenPos = new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY());
    Vector2 mouseWorldPos = Raylib.GetScreenToWorld2D(mouseScreenPos, player.camera);
    return mouseWorldPos;
}

static void WebSwingCheck(Player player, List<Spiderman> spidyWeb, Level.SuperRectangle r)
{
    foreach (Spiderman b in spidyWeb)
    {
        if (Raylib.CheckCollisionRecs(b.webs, new Rectangle(r.X, r.Y, r.Width, r.Height)))
        {
            if (b.pos.X > r.X && b.pos.X < r.X + r.Width && b.pos.Y > r.Y + 14 && b.pos.Y < r.Y + r.Height - 14)
            {
                if (b.pos.X > r.X + r.Width / 2)
                {
                    b.pos.X = r.X + r.Width - 1;
                }
                if (b.pos.X < r.X + r.Width / 2)
                {
                    b.pos.X = r.X;
                }
                if (b.pos.Y > r.Y + r.Width / 2)
                {
                    b.pos.X = r.X + r.Width - 1;
                }
            }
            b.speed = Vector2.Zero;
            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_RIGHT))
            {
                Vector2 direction = new Vector2(b.webs.X - player.hitbox.X, b.webs.Y - player.hitbox.Y);
                direction = Vector2.Normalize(direction);
                player.hitbox.X += direction.X * 2 + 5 * direction.X;
                player.hitbox.Y += direction.Y * 3 + 5 * direction.Y;
                player.gravity = 0;
                player.velocity = Vector2.Zero;


            }

        }
    }
}
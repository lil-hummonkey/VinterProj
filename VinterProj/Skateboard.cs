public class Skateboard : Body
{
    float accelRate;
    float accelSpeed = 5f;
    float forwardVel;
    public Skateboard()
    {
        width = 18;
        height = 7;
        boundBox = new Rectangle(80, 600, width, height);
        accelRate = 10 / accelSpeed;

    }
    public override void IsOn(Player player)
    {
        boundBox = new Rectangle(player.hitbox.X - 5, player.hitbox.Y + 10, 20, 5);
    }
    public virtual float Rise(Player player)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            jump = -15;
        }
        else
        {
            jump = 0;

        }
        return jump;
    }
    public override float Movement()
    {
        {
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                forwardVel += accelRate * Raylib.GetFrameTime();
                forwardVel = MathF.Min(forwardVel, 5f);
            }
            else
            {
                forwardVel = 0;
            }
            return forwardVel;
        }

    }
    public override void Draw()
    {
        Raylib.DrawRectangleRec(boundBox, Color.BLUE);
    }
}





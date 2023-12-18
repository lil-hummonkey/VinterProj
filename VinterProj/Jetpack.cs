public class Jetpack : Body
{
    float fly;
    Body body;
    public Jetpack()
    {
        width = 7;
        height = 18;
        boundBox = new Rectangle(500, 680, width, height);
    }
    public override void IsOn(Player player)
    {
        boundBox = new Rectangle(player.hitbox.X - 5, player.hitbox.Y, width, height);
    }
    public override float Rise(Player player)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && player.isStanding == true)
        {
            fly = -5;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) && player.isStanding != true)
        {
            fly = -1f;
        }
        else
        {
            fly = 0.5f;

        }
        return fly;
    }
    public override void Draw()
    {

        Raylib.DrawRectangleRec(boundBox, Color.ORANGE);
    }
}
public class Jetpack : Body
{

    float fly;

    public Jetpack()
    {
        width = 7;
        height = 18;
        boundBox = new Rectangle(500, 680, width, height);
        
    }
    public override void IsOn(Player player)
    {
        boundBox = new Rectangle(player.hitbox.X - 5, player.hitbox.Y, width, height);
        // boundBox.Y += player.gravity;
    }


    public override float Rise(Player player)
    {

        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
        {
            fly = -2.5f;
        }
        else
        {
            fly = 1;

        }
        return fly;
    }

    public override void Draw()
    {

        Raylib.DrawRectangleRec(boundBox, Color.ORANGE);
    }
}
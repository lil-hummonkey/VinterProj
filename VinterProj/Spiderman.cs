using System.Numerics;

public class Spiderman
{
    Player player;
    public Vector2 pos;
    public Vector2 speed;
    public bool killme = false;
    public Rectangle webs;

    public Spiderman(Vector2 pos, Vector2 speed)
    {
        this.pos = pos;
        this.speed = speed;
        webs = new Rectangle((int)pos.X - 2, (int)pos.Y - 2, 4, 4);
    }
    public void Update()
    {
        pos += speed;
        webs.X = pos.X;
        webs.Y = pos.Y;

    }
    public void Draw(Player player)
    {
        Raylib.DrawRectangleRec(webs, Color.BLACK);
        Raylib.DrawLine((int)player.hitbox.X, (int)player.hitbox.Y, (int)pos.X, (int)pos.Y, Color.BLACK);
    }
}

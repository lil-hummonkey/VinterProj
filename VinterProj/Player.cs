using System.Numerics;

public class Player
{
    Body body;
    public bool isOn = false;
    
    public int outOfBoundsY = 700;
    public List<Body> bodies = new();

    Texture2D avatarImg = Raylib.LoadTexture(@"avatar1.png");
    public Rectangle hitbox;

    public float newHorizontal;

    public float newVertical;
    public Vector2 velocity;
    public int width = 10;
    public int height = 15;
    public float gravity = 1f;

    public float rotation;

    public Player()
    {
    hitbox = new Rectangle(0, 700, width, height);
    }
    public void Update(Body body)
    {
rotation = MathF.Atan2
    (hitbox.Y - Raylib.GetMousePosition().Y
    , hitbox.X - Raylib.GetMousePosition().X) * Raylib.RAD2DEG;
    rotation += 180;

        velocity.X = 0;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            velocity.X = 5;
        }

        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            velocity.X = -5;
        }

        foreach (Body item in bodies)
        {
            item.IsOn(this);
            newHorizontal = item.Movement();
            newVertical = item.Rise(this);
        }
        velocity.Y += newVertical;
        velocity.X *= newHorizontal;
        hitbox.Y += velocity.Y;
        hitbox.X += velocity.X;


        if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
        {
            if (bodies.Count() != 0)
            bodies.Clear();
            hitbox.X += 50;
            outOfBoundsY = 700;
        }

          if (Raylib.IsKeyPressed(KeyboardKey.KEY_O))
        {
            if (bodies.Count() != 0)
            bodies.Clear();
            hitbox.X -= 50;
            outOfBoundsY = 700;
        }



    }



    public void Draw()
    {
            Raylib.DrawTexture(avatarImg,(int)hitbox.X,(int)hitbox.Y,Color.WHITE);
        
    }





}
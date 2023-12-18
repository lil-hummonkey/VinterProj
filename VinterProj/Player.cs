using System.Numerics;

public class Player
{
    Body body;

    Platforms pl;
    public bool isOn = false;

    public Camera2D camera = new Camera2D();
    private bool turnedRight;
    public int outOfBoundsY = 1050;
    public List<Body> bodies = new();

    public Texture2D avatarImg = Raylib.LoadTexture(@"avatar1.png");
    public Rectangle hitbox;
    Rectangle footbox;
    Rectangle Sidebox;

    public float newHorizontal;

    public float newVertical;
    public float newGravity;
    public Vector2 velocity;
    public int width = 10;
    public int height = 15;
    public float gravity;

    public float rotation;

    public bool isStanding;

    public Player()
    {
        hitbox = new Rectangle(1380 / 2 - width, 650, width, height);

    }


    public void Update(Body body)
    {



        Sidebox = new Rectangle(hitbox.X + 10, hitbox.Y - (height / 2) + 18, width - 8, height - 14);
        footbox = new Rectangle(hitbox.X + 3, hitbox.Y + (height * 0.75f) + 5, width - 9, height / 4);


        velocity.X = 0;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            velocity.X = -5;
            turnedRight = false;


        }

        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            velocity.X = 5;
            turnedRight = true;

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


        if (Raylib.IsKeyPressed(KeyboardKey.KEY_O))
        {
            if (bodies.Count() != 0)
            {
                bodies.Clear();
                velocity.X += 50;

            }
        }

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
        {
            if (bodies.Count() != 0)
            {
                bodies.Clear();
                velocity.X -= 50;

            }
        }


    }



    public void Draw()
    {
        Raylib.DrawTexture(avatarImg, (int)hitbox.X, (int)hitbox.Y, Color.WHITE);
        Raylib.DrawRectangleRec(footbox, Color.DARKBLUE);
        Raylib.DrawRectangleRec(Sidebox, Color.DARKBLUE);
    }






}
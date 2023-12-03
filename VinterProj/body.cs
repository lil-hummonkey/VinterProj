using System.Numerics;

public class Body
{
    public int width;
    public int height;
    public float jump;

    public bool isOn;

    public virtual void Draw() {}

    public virtual void IsOn(Player player)
    {

    }



    public Rectangle boundBox;
    public virtual float Movement()
    {

        return 0.5f;
    }

    public virtual float Rise(Player player)
    {

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            jump = -15;
        }
        else
        {
            jump = 1;

        }

        return jump;

    }
}
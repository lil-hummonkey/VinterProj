public class Level
{
    public int Difficulty { get; set; }
    public List<SuperRectangle> boxes { get; set; }

    public class SuperRectangle
    {
        public Rectangle rect;

        public Color Col { get; set; }

        public float X { get => rect.X; set => rect.X = value; }
        public float Y { get => rect.Y; set => rect.Y = value; }
        public float Width { get => rect.Width; set => rect.Width = value; }
        public float Height { get => rect.Height; set => rect.Height = value; }

        public SuperRectangle(float x, float y, float width, float height)
        {
            rect = new Rectangle(x, y, width, height);
        }

        public void Draw()
        {
            Raylib.DrawRectangleRec(rect, Color.BLACK);
        }

    }
}

//  {
//     "X": ,
//     "Y": ,
//     "Width": ,
//     "Height":     
//     },
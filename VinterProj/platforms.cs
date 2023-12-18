public class Platforms{
    public List<Rectangle> platform = new List<Rectangle>();
    string lText = File.ReadAllText("level.json");
    
    public Platforms()
    {
        Level l = JsonSerializer.Deserialize<Level>(lText);
        foreach(Level.SuperRectangle rect in l.boxes)
        {
        platform.Add(new Rectangle(rect.X, rect.Y, rect.Width,rect.Height));

        }
    }
}
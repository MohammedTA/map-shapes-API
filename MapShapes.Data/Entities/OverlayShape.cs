namespace MapShapes.Data.Entities
{
    public class OverlayShape : KeyInt32
    {
        protected OverlayShape()
        {
        }

        public OverlayShape(string title, string properties, ShapeType type)
        {
            this.Title = title;
            this.Type = type;
            this.Properties = properties;
        }

        public string Properties { get; private set; }
        public string Title { get; private set; }

        public ShapeType Type { get; private set; }

        public void Edit(string title, string properties, ShapeType type)
        {
            this.Title = title;
            this.Properties = properties;
            this.Type = type;
        }
    }
}
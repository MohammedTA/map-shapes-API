namespace MapShapes.Data.Entities
{
    public class OverlayShape : KeyInt32
    {
        protected OverlayShape()
        {
        }

        public OverlayShape(string title, string properties, int shapeTypeId)
        {
            this.Title = title;
            this.ShapeTypeId = shapeTypeId;
            this.Properties = properties;
        }

        public string Properties { get; private set; }

        public virtual ShapeType ShapeType { get; private set; }

        public int ShapeTypeId { get; private set; }
        public string Title { get; private set; }

        public void Edit(string title, string properties, int shapeTypeId)
        {
            this.Title = title;
            this.Properties = properties;
            this.ShapeTypeId = shapeTypeId;
        }
    }
}
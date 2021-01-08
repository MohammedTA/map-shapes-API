namespace MapShapes.Domain.Models
{
    using MapShapes.Data.Entities;

    public class OverlayShapeModel
    {
        public OverlayShapeModel(OverlayShape overlayShape)
        {
            this.Id = overlayShape.Id;
            this.Title = overlayShape.Title;
            this.Properties = overlayShape.Properties;
            this.Type = overlayShape.Type;
        }

        public int Id { get; set; }

        public string Properties { get; set; }
        public ShapeType Type { get; set; }
        public string Title { get; set; }
    }
}
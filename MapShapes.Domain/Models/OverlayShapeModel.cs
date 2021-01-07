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
            this.ShapeTypeId = overlayShape.ShapeTypeId;
        }

        public int Id { get; set; }

        public string Properties { get; set; }
        public int ShapeTypeId { get; set; }
        public string Title { get; set; }
    }
}
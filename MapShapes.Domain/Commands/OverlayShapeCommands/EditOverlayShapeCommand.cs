namespace MapShapes.Domain.Commands.OverlayShapeCommands
{
    using MapShapes.Data.Entities;

    public class EditOverlayShapeCommand : CommandBase<object>
    {
        public int Id { get; set; }
        public string Properties { get; set; }
        public ShapeType Type { get; set; }
        public string Title { get; set; }
    }
}
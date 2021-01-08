namespace MapShapes.Domain.Commands.OverlayShapeCommands
{
    using MapShapes.Data.Entities;

    public class AddOverlayShapeCommand : CommandBase<object>
    {
        public string Properties { get; set; }
        public ShapeType Type { get; set; }
        public string Title { get; set; }
    }
}
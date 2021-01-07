namespace MapShapes.Domain.Commands.OverlayShapeCommands
{
    public class AddOverlayShapeCommand : CommandBase<object>
    {
        public string Properties { get; set; }
        public int ShapeTypeId { get; set; }
        public string Title { get; set; }
    }
}
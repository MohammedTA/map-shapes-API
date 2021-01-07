namespace MapShapes.Domain.Commands.OverlayShapeCommands
{
    public class EditOverlayShapeCommand : CommandBase<object>
    {
        public int Id { get; set; }
        public string Properties { get; set; }
        public int ShapeTypeId { get; set; }
        public string Title { get; set; }
    }
}
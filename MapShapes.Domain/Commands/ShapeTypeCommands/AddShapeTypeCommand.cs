namespace MapShapes.Domain.Commands.ShapeTypeCommands
{
    public class AddShapeTypeCommand : CommandBase<object>
    {
        public string Name { get; set; }
    }
}
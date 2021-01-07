namespace MapShapes.Domain.Commands.ShapeTypeCommands
{
    public class EditShapeTypeCommand : CommandBase<object>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
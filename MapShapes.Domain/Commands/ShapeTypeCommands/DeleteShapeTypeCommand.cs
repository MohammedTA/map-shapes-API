namespace MapShapes.Domain.Commands.ShapeTypeCommands
{
    public class DeleteShapeTypeCommand : CommandBase<object>
    {
        public DeleteShapeTypeCommand(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
    }
}
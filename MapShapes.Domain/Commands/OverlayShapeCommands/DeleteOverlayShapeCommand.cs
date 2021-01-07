namespace MapShapes.Domain.Commands.OverlayShapeCommands
{
    public class DeleteOverlayShapeCommand : CommandBase<object>
    {
        public DeleteOverlayShapeCommand(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
    }
}
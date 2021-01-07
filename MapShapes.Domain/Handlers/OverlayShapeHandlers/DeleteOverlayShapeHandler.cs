namespace MapShapes.Domain.Handlers.OverlayShapeHandlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MapShapes.Data.DataAccess;
    using MapShapes.Domain.Commands.OverlayShapeCommands;

    public class DeleteOverlayShapeHandler : HandlerBaseAsync<DeleteOverlayShapeCommand, object>
    {
        private readonly CoreDbContext context;

        public DeleteOverlayShapeHandler(CoreDbContext context)
        {
            this.context = context;
        }

        public override async Task<object> Handle(DeleteOverlayShapeCommand request, CancellationToken cancellationToken)
        {
            var overlayShape = await this.context.OverlayShapes
                .SingleOrExceptionAsync(t => t.Id == request.Id, cancellationToken: cancellationToken);

            this.context.OverlayShapes.Remove(overlayShape);
            await this.context.SaveChangesAsync(cancellationToken);
            return overlayShape.Id;
        }
    }
}
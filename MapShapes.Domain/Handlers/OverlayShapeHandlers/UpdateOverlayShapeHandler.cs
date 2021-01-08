namespace MapShapes.Domain.Handlers.OverlayShapeHandlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MapShapes.Data.DataAccess;
    using MapShapes.Domain.Commands.OverlayShapeCommands;

    public class UpdateOverlayShapeHandler : HandlerBaseAsync<EditOverlayShapeCommand, object>
    {
        private readonly CoreDbContext context;

        public UpdateOverlayShapeHandler(CoreDbContext context)
        {
            this.context = context;
        }

        public override async Task<object> Handle(EditOverlayShapeCommand request, CancellationToken cancellationToken)
        {
            var overlayShape = await this.context.OverlayShapes
                .SingleOrExceptionAsync(
                    t => t.Id == request.Id,
                    cancellationToken: cancellationToken);

            overlayShape.Edit(request.Title, request.Properties, request.Type);
            await this.context.SaveChangesAsync(cancellationToken);
            return overlayShape.Id;
        }
    }
}
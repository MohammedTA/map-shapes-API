namespace MapShapes.Domain.Handlers.OverlayShapeHandlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MapShapes.Data.DataAccess;
    using MapShapes.Data.Entities;
    using MapShapes.Domain.Commands.OverlayShapeCommands;

    public class AddOverlayShapeHandler : HandlerBaseAsync<AddOverlayShapeCommand, object>
    {
        private readonly CoreDbContext context;

        public AddOverlayShapeHandler(CoreDbContext context)
        {
            this.context = context;
        }

        public override async Task<object> Handle(AddOverlayShapeCommand request, CancellationToken cancellationToken)
        {
            var overlayShape = new OverlayShape(request.Title, request.Properties, request.ShapeTypeId);

            await this.context.OverlayShapes.AddAsync(overlayShape, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);
            return overlayShape.Id;
        }
    }
}
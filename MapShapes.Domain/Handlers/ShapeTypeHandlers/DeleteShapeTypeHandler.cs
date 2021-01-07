namespace MapShapes.Domain.Handlers.ShapeTypeHandlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MapShapes.Data.DataAccess;
    using MapShapes.Domain.Commands.ShapeTypeCommands;

    public class DeleteShapeTypeHandler : HandlerBaseAsync<DeleteShapeTypeCommand, object>
    {
        private readonly CoreDbContext context;

        public DeleteShapeTypeHandler(CoreDbContext context)
        {
            this.context = context;
        }

        public override async Task<object> Handle(DeleteShapeTypeCommand request, CancellationToken cancellationToken)
        {
            var shapeType = await this.context.ShapeTypes
                .SingleOrExceptionAsync(t => t.Id == request.Id, cancellationToken: cancellationToken);

            this.context.ShapeTypes.Remove(shapeType);
            await this.context.SaveChangesAsync(cancellationToken);
            return shapeType.Id;
        }
    }
}
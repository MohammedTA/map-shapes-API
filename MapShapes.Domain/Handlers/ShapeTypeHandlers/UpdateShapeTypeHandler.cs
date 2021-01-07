namespace MapShapes.Domain.Handlers.ShapeTypeHandlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MapShapes.Data.DataAccess;
    using MapShapes.Domain.Commands.ShapeTypeCommands;

    public class UpdateShapeTypeHandler : HandlerBaseAsync<EditShapeTypeCommand, object>
    {
        private readonly CoreDbContext context;

        public UpdateShapeTypeHandler(CoreDbContext context)
        {
            this.context = context;
        }

        public override async Task<object> Handle(EditShapeTypeCommand request, CancellationToken cancellationToken)
        {
            var shapeType = await this.context.ShapeTypes
                .SingleOrExceptionAsync(
                    t => t.Id == request.Id,
                    cancellationToken: cancellationToken);

            shapeType.Edit(request.Name);
            await this.context.SaveChangesAsync(cancellationToken);
            return shapeType.Id;
        }
    }
}
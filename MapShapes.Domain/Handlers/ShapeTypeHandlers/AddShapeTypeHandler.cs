namespace MapShapes.Domain.Handlers.ShapeTypeHandlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MapShapes.Data.DataAccess;
    using MapShapes.Data.Entities;
    using MapShapes.Domain.Commands.ShapeTypeCommands;

    public class AddShapeTypeHandler : HandlerBaseAsync<AddShapeTypeCommand, object>
    {
        private readonly CoreDbContext context;

        public AddShapeTypeHandler(CoreDbContext context)
        {
            this.context = context;
        }

        public override async Task<object> Handle(AddShapeTypeCommand request, CancellationToken cancellationToken)
        {
            var shapeType = new ShapeType(request.Name);
            await this.context.ShapeTypes.AddAsync(shapeType, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);
            return shapeType.Id;
        }
    }
}
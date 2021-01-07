namespace MapShapes.Domain.Handlers.ShapeTypeHandlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MapShapes.Data.DataAccess;
    using MapShapes.Domain.Models;
    using MapShapes.Domain.Queries.ShapeTypeQueries;

    public class GetShapeTypeHandler : HandlerBaseAsync<GetShapeTypeQuery, ShapeTypeModel>
    {
        private readonly CoreDbContext context;

        public GetShapeTypeHandler(CoreDbContext context)
        {
            this.context = context;
        }

        public override async Task<ShapeTypeModel> Handle(GetShapeTypeQuery request, CancellationToken cancellationToken)
        {
            var shapeType = await this.context
                .ShapeTypes
                .SingleOrExceptionAsync(t => t.Id == request.Id, cancellationToken: cancellationToken);

            return new ShapeTypeModel(shapeType);
        }
    }
}
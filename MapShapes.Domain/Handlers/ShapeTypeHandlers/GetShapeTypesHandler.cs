namespace MapShapes.Domain.Handlers.ShapeTypeHandlers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MapShapes.Data.DataAccess;
    using MapShapes.Domain.Models;
    using MapShapes.Domain.Queries.ShapeTypeQueries;

    public class GetShapeTypesHandler : HandlerBaseAsync<GetShapeTypesQuery, PaginatedResultModel<ShapeTypeModel>>
    {
        private readonly CoreDbContext context;

        public GetShapeTypesHandler(CoreDbContext context)
        {
            this.context = context;
        }

        public override async Task<PaginatedResultModel<ShapeTypeModel>> Handle(GetShapeTypesQuery request, CancellationToken cancellationToken)
        {
            var query = this.context.ShapeTypes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(t => t.Name.Contains(request.Name));
            }

            return await query
                .OrderByDescending(a => a.Id)
                .PaginateAsync(
                    t => new ShapeTypeModel(t),
                    request,
                    cancellationToken: cancellationToken);
        }
    }
}
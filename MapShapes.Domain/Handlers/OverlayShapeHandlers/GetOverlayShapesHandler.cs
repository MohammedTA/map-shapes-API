namespace MapShapes.Domain.Handlers.OverlayShapeHandlers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MapShapes.Data.DataAccess;
    using MapShapes.Domain.Models;
    using MapShapes.Domain.Queries.OverlayShapeQueries;

    public class GetOverlayShapesHandler : HandlerBaseAsync<GetOverlayShapesQuery, PaginatedResultModel<OverlayShapeModel>>
    {
        private readonly CoreDbContext context;

        public GetOverlayShapesHandler(CoreDbContext context)
        {
            this.context = context;
        }

        public override async Task<PaginatedResultModel<OverlayShapeModel>> Handle(GetOverlayShapesQuery request, CancellationToken cancellationToken)
        {
            var query = this.context.OverlayShapes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(t => t.Title.Contains(request.Title));
            }

            return await query
                .OrderByDescending(a => a.Id)
                .PaginateAsync(
                    t => new OverlayShapeModel(t),
                    request,
                    cancellationToken: cancellationToken);
        }
    }
}
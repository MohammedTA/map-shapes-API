namespace MapShapes.Domain.Handlers.OverlayShapeHandlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MapShapes.Data.DataAccess;
    using MapShapes.Domain.Models;
    using MapShapes.Domain.Queries.OverlayShapeQueries;

    public class GetOverlayShapeHandler : HandlerBaseAsync<GetOverlayShapeQuery, OverlayShapeModel>
    {
        private readonly CoreDbContext context;

        public GetOverlayShapeHandler(CoreDbContext context)
        {
            this.context = context;
        }

        public override async Task<OverlayShapeModel> Handle(GetOverlayShapeQuery request, CancellationToken cancellationToken)
        {
            var shapeType = await this.context
                .OverlayShapes
                .SingleOrExceptionAsync(t => t.Id == request.Id, cancellationToken: cancellationToken);

            return new OverlayShapeModel(shapeType);
        }
    }
}
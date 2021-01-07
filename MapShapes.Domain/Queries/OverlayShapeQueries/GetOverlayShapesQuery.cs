namespace MapShapes.Domain.Queries.OverlayShapeQueries
{
    using MapShapes.Domain.Models;

    public class GetOverlayShapesQuery : PaginatedQueryBase<OverlayShapeModel>
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }
}
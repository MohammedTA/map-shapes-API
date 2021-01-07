namespace MapShapes.Domain.Queries.OverlayShapeQueries
{
    using MapShapes.Domain.Models;

    public class GetOverlayShapeQuery : QueryBase<OverlayShapeModel>
    {
        public GetOverlayShapeQuery()
        {
        }

        public GetOverlayShapeQuery(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
    }
}
namespace MapShapes.Domain.Queries.ShapeTypeQueries
{
    using MapShapes.Domain.Models;

    public class GetShapeTypeQuery : QueryBase<ShapeTypeModel>
    {
        public GetShapeTypeQuery()
        {
        }

        public GetShapeTypeQuery(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
    }
}
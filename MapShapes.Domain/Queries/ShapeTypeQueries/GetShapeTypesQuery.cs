namespace MapShapes.Domain.Queries.ShapeTypeQueries
{
    using MapShapes.Domain.Models;

    public class GetShapeTypesQuery : PaginatedQueryBase<ShapeTypeModel>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
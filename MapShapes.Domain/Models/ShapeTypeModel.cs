namespace MapShapes.Domain.Models
{
    using MapShapes.Data.Entities;

    public class ShapeTypeModel
    {
        public ShapeTypeModel(ShapeType brand)
        {
            this.Id = brand.Id;
            this.Name = brand.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
namespace MapShapes.Data.DataAccess.Mapping
{
    using MapShapes.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ShapeTypeMap : IEntityTypeConfiguration<ShapeType>
    {
        public void Configure(EntityTypeBuilder<ShapeType> entity)
        {
            entity.HasKey(c => c.Id);
        }
    }
}
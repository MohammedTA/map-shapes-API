namespace MapShapes.Data.DataAccess.Mapping
{
    using MapShapes.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OverlayShapeMap : IEntityTypeConfiguration<OverlayShape>
    {
        public void Configure(EntityTypeBuilder<OverlayShape> entity)
        {
            entity.HasKey(c => c.Id);
        }
    }
}
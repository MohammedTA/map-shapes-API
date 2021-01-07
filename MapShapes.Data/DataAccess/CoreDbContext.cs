namespace MapShapes.Data.DataAccess
{
    using MapShapes.Data.DataAccess.Mapping;
    using MapShapes.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    public class CoreDbContext : BaseDbContext
    {
        public CoreDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<OverlayShape> OverlayShapes { get; set; }

        public virtual DbSet<ShapeType> ShapeTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ShapeTypeMap());
            builder.ApplyConfiguration(new OverlayShapeMap());
        }
    }
}
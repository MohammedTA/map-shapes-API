namespace MapShapes.Data.DataAccess
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public abstract class BaseDbContext : DbContext
    {
        protected BaseDbContext(
            DbContextOptions options,
            string baseSchema = "dbo") : base(options)
        {
            this.BaseSchema = baseSchema;
        }

        public string BaseSchema { get; }

        public override int SaveChanges()
        {
            var result = base.SaveChanges();

            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
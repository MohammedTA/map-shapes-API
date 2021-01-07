namespace MapShapes.Infrastructure
{
    using MapShapes.Data.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class Extensions
    {
        public static void ConfigureDataAccess(this IServiceCollection services)
        {
            var conn = ConfigurationReader.GetConnectionString();
            services.AddDbContext<CoreDbContext>(t => t.UseSqlServer(conn));
        }
    }
}
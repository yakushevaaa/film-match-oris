using FilmMatch.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FilmMatch.Persistence.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            //services.AddRepositories();
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IDbContext,ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        }

        private static void AddRepositories(this IServiceCollection services)
        {
            /*  services
                  .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
                  .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                  .AddTransient<IOrderRepository, OrderRepository>()*/
        }
    }
}

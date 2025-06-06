using FilmMatch.Persistence.Configurations.FilmMatch.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SocialNetwork.Persistence.Extensions
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
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDBContext>(options =>
               options.UseNpgsql(connectionString,
                   builder => builder.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)));

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

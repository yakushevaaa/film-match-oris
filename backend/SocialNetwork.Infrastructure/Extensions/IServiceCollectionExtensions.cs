using FilmMatch.Application.Interfaces.Services;
using FilmMatch.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using ProFSB.Application.Interfaces.Services;

namespace FilmMatch.Infrastructure.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddServices();
    }
 
    private static void AddServices(this IServiceCollection services)
    {
        // services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddHttpContextAccessor();
    }
}
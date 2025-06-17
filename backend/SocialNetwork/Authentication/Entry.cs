using FilmMatch.Application.Interfaces;

namespace FilmMatch.Authentication
{
    public static class Entry
    {
        public static IServiceCollection AddUserContext(this IServiceCollection services) =>
            services
                .AddScoped<IUserContext, UserContext>();
    }
}
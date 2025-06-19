using FilmMatch.Domain.Entities;

namespace FilmMatch.Application.Interfaces.Services ;

    public interface IJwtService
    {
        public string GenerateToken(User user, IList<string> roles);
    }
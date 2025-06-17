using System.Security.Claims;
using FilmMatch.Application.Interfaces;

namespace FilmMatch.Authentication ;

    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public string? GetUserEmail()
        {
            return _httpContextAccessor.HttpContext?
                .User?
                .Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }

        public Guid GetUserId()
        {
            var id = _httpContextAccessor.HttpContext?
                .User?
                .Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(id ?? throw new UnauthorizedAccessException("User is not authenticated."));
        }
    }
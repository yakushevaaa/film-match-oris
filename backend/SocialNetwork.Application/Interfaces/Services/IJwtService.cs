using FilmMatch.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ProFSB.Application.Interfaces.Services ;

    public interface IJwtService
    {
        public string GenerateToken(User user, string? role);
    }
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FilmMatch.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProFSB.Application.Interfaces.Services;

namespace FilmMatch.Infrastructure.Services ;

    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        
        public JwtService(IConfiguration configuration)
            => _configuration = configuration;
        
        public string GenerateToken(User user, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityKey = Encoding.ASCII.GetBytes(
                _configuration["Jwt:Key"]!);
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            if (roles != null)
            {
                foreach (var r in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, r));
                }
            }
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"],
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(
                    _configuration["Jwt:AccessTokenLifetimeInMinutes"]!)),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(jwtSecurityKey),
                        SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
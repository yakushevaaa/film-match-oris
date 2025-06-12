using System.Security.Claims;
using FilmMatch.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace FilmMatch.Infrastructure.Services ;

    /// <inheritdoc />
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;

        public UserService(UserManager<IdentityUser<Guid>> userManager, SignInManager<IdentityUser<Guid>> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        public Task<IdentityResult> RegisterUserAsync(IdentityUser<Guid> user, string password)
            => _userManager.CreateAsync(user, password);
        
        public async Task<IdentityUser<Guid>?> FindUserByEmailAsync(string email)
            => await _userManager.FindByEmailAsync(email);

        public Task<IdentityResult> AddRoleAsync(IdentityUser<Guid> user, string role)
            => _userManager.AddToRoleAsync(user, role);

        public Task<IdentityResult> AddClaimsAsync(IdentityUser<Guid> user, IEnumerable<Claim> claims)
            => _userManager.AddClaimsAsync(user, claims);

        public Task<bool> CheckPasswordAsync(IdentityUser<Guid> user, string password)
            => _userManager.CheckPasswordAsync(user, password);

        public Task<IList<string>> GetRolesAsync(IdentityUser<Guid> user)
            => _userManager.GetRolesAsync(user);
        
        public async Task<string?> GetRoleAsync(IdentityUser<Guid> user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            return claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        }

        public Task<IdentityResult> ResetPasswordAsync(IdentityUser<Guid> user, string token, string newPassword)
            => _userManager.ResetPasswordAsync(user, token, newPassword);
        
        public Task<string> GeneratePasswordResetTokenAsync(IdentityUser<Guid> user)
            => _userManager.GeneratePasswordResetTokenAsync(user);

        public Task<IdentityUser<Guid>?> GetUserById(Guid id)
            => _userManager.FindByIdAsync(id.ToString());

        public async Task<bool> IsGodAsync(IdentityUser<Guid> user)
        {
            return await _userManager.IsInRoleAsync(user, FilmMatch.Domain.Constants.RoleConstants.God);
        }

        public async Task<bool> IsAdminAsync(IdentityUser<Guid> user)
        {
            return await _userManager.IsInRoleAsync(user, FilmMatch.Domain.Constants.RoleConstants.Admin);
        }

        public async Task<bool> IsUserAsync(IdentityUser<Guid> user)
        {
            return await _userManager.IsInRoleAsync(user, FilmMatch.Domain.Constants.RoleConstants.User);
        }

        /// <summary>
        /// Создать админа (только для бога)
        /// </summary>
        public async Task<IdentityResult> CreateAdminAsync(string email, string password)
        {
            var user = new IdentityUser<Guid> { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, FilmMatch.Domain.Constants.RoleConstants.Admin);
            }
            return result;
        }

        public async Task<IdentityResult> MakeAdminAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            if (await _userManager.IsInRoleAsync(user, FilmMatch.Domain.Constants.RoleConstants.Admin))
                return IdentityResult.Failed(new IdentityError { Description = "User is already an admin" });

            return await _userManager.AddToRoleAsync(user, FilmMatch.Domain.Constants.RoleConstants.Admin);
        }
    }
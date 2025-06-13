using System.Security.Authentication;
using FilmMatch.Application.Contracts.Requests.UserRequests.LoginUser;
using FilmMatch.Application.Interfaces;
using FilmMatch.Application.Interfaces.Services;
using MediatR;
using ProFSB.Application.Interfaces.Services;

namespace FilmMatch.Application.Features.Users.Queries.LoginUser
{
    public class LoginUserCommandHandler
        : IRequestHandler<LoginUserQuery, LoginUserResponse>
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly IDbContext _dbContext;
        
        public LoginUserCommandHandler(IUserService userService, IJwtService jwtService, IDbContext dbContext)
        {
            _userService = userService;
            _jwtService = jwtService;
            _dbContext = dbContext;
        }

        public async Task<LoginUserResponse> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindUserByEmailAsync(request.Email);
            var currentUser = _dbContext.Users.FirstOrDefault(x => x.IdentityUserId == user.Id) ?? throw new AuthenticationException();

            if (user == null || !await _userService.CheckPasswordAsync(user, request.Password))
                throw new AuthenticationException("Неверные логин или пароль");

            var roles = await _userService.GetRolesAsync(user);
            
            return new LoginUserResponse(_jwtService.GenerateToken(currentUser, roles));
        }
    }
}
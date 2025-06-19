using FilmMatch.Application.Contracts.Responses.Users.UserToAdmin;
using FilmMatch.Application.Interfaces;
using FilmMatch.Application.Interfaces.Services;
using FilmMatch.Domain.Constants;
using MediatR;

namespace FilmMatch.Application.Features.Users.UserToAdmin
{
    public class UserToAdminCommandHandler : IRequestHandler<UserToAdminCommand, UserToAdminResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserService _userService;

        public UserToAdminCommandHandler(IDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<UserToAdminResponse> Handle(UserToAdminCommand request, CancellationToken cancellationToken)
        {
            var identityUserId = _dbContext.Users.FirstOrDefault(u => u.Id == request.UserId)?.IdentityUserId
                ?? throw new InvalidOperationException("No user found");
            var user = await _userService.GetUserById(identityUserId);
            return new UserToAdminResponse(await _userService.AddRoleAsync(user, RoleConstants.Admin));
        }
    }
}
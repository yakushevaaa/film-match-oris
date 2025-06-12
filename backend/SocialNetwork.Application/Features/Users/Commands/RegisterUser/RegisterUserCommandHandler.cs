using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using FilmMatch.Application.Contracts.Requests.UserRequests.RegisterUser;
using FilmMatch.Application.Interfaces;
using FilmMatch.Application.Interfaces.Services;
using FilmMatch.Domain.Constants;
using FilmMatch.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FilmMatch.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly IUserService _userServiceIdentity;
        private readonly IDbContext _dbContext;

        public RegisterUserCommandHandler(IUserService userServiceIdentity, IDbContext dbContext)
        {
            _userServiceIdentity = userServiceIdentity;
            _dbContext = dbContext;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var isUserExists = await _userServiceIdentity.FindUserByEmailAsync(request.Email);

            if (isUserExists != null)
                throw new ValidationException("Пользователь с такой почтой уже существует");

            var user = new IdentityUser<Guid>
            {
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userServiceIdentity.RegisterUserAsync(user, request.Password);

            if (!result.Succeeded)
                // Не создаем доп. записи, если регистрация неуспешна
                return new RegisterUserResponse(result);

            var biznesUserId = Guid.NewGuid();

            _dbContext.Users.Add(new User
            {
                Id = biznesUserId,
                IdentityUserId = user.Id,
                Name = request.Name
            });

            var claims = new List<Claim>
            {
                new(ClaimTypes.Role, RoleConstants.User),
                new(ClaimTypes.Authentication, biznesUserId.ToString())
            };

            await _userServiceIdentity.AddClaimsAsync(user, claims);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return new RegisterUserResponse(result);
        }
    }
}
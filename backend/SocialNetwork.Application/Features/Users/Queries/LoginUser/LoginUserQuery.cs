using FilmMatch.Application.Contracts.Requests.UserRequests.LoginUser;
using MediatR;

namespace FilmMatch.Application.Features.Users.Queries.LoginUser
{
    public class LoginUserQuery : IRequest<LoginUserResponse>
    {
        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }  = default!;
        
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = default!;
    }
}
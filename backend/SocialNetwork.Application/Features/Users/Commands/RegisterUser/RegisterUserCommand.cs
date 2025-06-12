using System.ComponentModel.DataAnnotations;
using FilmMatch.Application.Contracts.Requests.UserRequests.RegisterUser;
using MediatR;

namespace FilmMatch.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<RegisterUserResponse>
    {
        /// <summary>
        /// Почта
        /// </summary>
        [Required]
        public string Email { get; set; } = default!;

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        public string Password { get; set; } = default!;

        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        public string Name { get; set; } = default!;
    }
}
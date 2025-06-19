using FilmMatch.Application.Contracts.Responses.Users.UserToAdmin;
using MediatR;

namespace FilmMatch.Application.Features.Users.UserToAdmin
{
    public class UserToAdminCommand : IRequest<UserToAdminResponse>
    {
        public Guid UserId { get; set; }

        public UserToAdminCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}
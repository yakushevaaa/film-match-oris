using MediatR;
using FilmMatch.Application.Contracts.Requests.UserRequests.GetUsernameByIdResponse;

namespace FilmMatch.Application.Features.Users.Queries.GetUsernameById
{
    public class GetUsernameByIdQuery : IRequest<GetUsernameByIdResponse?>
    {
        public Guid UserId { get; set; }
        public GetUsernameByIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
} 
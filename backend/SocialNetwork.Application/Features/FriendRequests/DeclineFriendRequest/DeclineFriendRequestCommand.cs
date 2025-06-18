using FilmMatch.Application.Contracts.Responses.FriendRequests.DeclineFriendRequest;
using MediatR;

namespace FilmMatch.Application.Features.FriendRequests.DeclineFriendRequest
{
    public class DeclineFriendRequestCommand : IRequest<DeclineFriendRequestResponse>
    {
        public Guid FriendRequestId { get; set; }
    }
}
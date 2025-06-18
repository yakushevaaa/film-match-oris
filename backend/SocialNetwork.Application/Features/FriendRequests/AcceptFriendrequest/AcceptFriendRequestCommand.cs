using FilmMatch.Application.Contracts.Responses.FriendRequests.AcceptFriendRequest;
using MediatR;

namespace FilmMatch.Application.Features.FriendRequests.AcceptFriendrequest
{
    public class AcceptFriendRequestCommand : IRequest<AcceptFriendRequestResponse>
    {
        public Guid RequestId { get; set; }
    }
}
using FilmMatch.Application.Contracts.Responses.FriendRequests.GetAllFriendRequests;
using MediatR;

namespace FilmMatch.Application.Features.FriendRequests.GetAllFriendRequests
{
    public class GetAllFriendRequestsQuery : IRequest<GetAllFriendRequestsResponse>
    {
        public bool IsSend { get; set; }
    }
}
using FilmMatch.Application.Contracts.Responses.FriendRequests.GetAllFriendRequests;
using FilmMatch.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.FriendRequests.GetAllFriendRequests
{
    public class GetAllFriendRequestsQueryHandler : IRequestHandler<GetAllFriendRequestsQuery, GetAllFriendRequestsResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;

        public GetAllFriendRequestsQueryHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task<GetAllFriendRequestsResponse> Handle(GetAllFriendRequestsQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId();
            var friendRequests = await _dbContext.FriendRequests.Where(x => !x.IsAccepted
                && ((request.IsSend && x.SenderId == userId) || (!request.IsSend && x.ReceiverId == userId)))
                .Select(x => new GetAllFriendRequestsResponseItem
                {
                    Id = x.Id,
                    Receiver = x.ReceiverId,
                    Sender = x.SenderId,
                    SenderName = x.Sender!.Name,
                    ReceiverName = x.Receiver!.Name,
                })
                .ToListAsync(cancellationToken: cancellationToken);

            return new GetAllFriendRequestsResponse
            {
                Requests = friendRequests,
            };
        }
    }
}
using FilmMatch.Application.Contracts.Responses.FriendRequests.AcceptFriendRequest;
using FilmMatch.Application.Interfaces;
using FilmMatch.Application.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.FriendRequests.AcceptFriendrequest
{
    public class AcceptFriendRequestCommandHandler : IRequestHandler<AcceptFriendRequestCommand, AcceptFriendRequestResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IFriendsService _friendsService;

        public AcceptFriendRequestCommandHandler(IDbContext dbContext, IFriendsService friendsService)
        {
            _dbContext = dbContext;
            _friendsService = friendsService;
        }

        public async Task<AcceptFriendRequestResponse> Handle(AcceptFriendRequestCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var friendRequest = await _dbContext.FriendRequests.FirstOrDefaultAsync(x => x.Id == request.RequestId,
                    cancellationToken)
                                    ?? throw new InvalidOperationException("Friend request not found");

                friendRequest.IsAccepted = true;
                _dbContext.FriendRequests.Remove(friendRequest);
                await _friendsService.AddFriendAsync(friendRequest.SenderId);
                
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return new AcceptFriendRequestResponse
                {
                    IsSuccessed = true,
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return new AcceptFriendRequestResponse
                {
                    IsSuccessed = false,
                };
            }
        }
    }
}
using FilmMatch.Application.Contracts.Responses.FriendRequests.SendFriendRequest;
using FilmMatch.Application.Interfaces;
using FilmMatch.Application.Interfaces.Services;
using FilmMatch.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.Friends.SendFriendRequest
{
    public class SendFriendRequestCommandHandler : IRequestHandler<SendFriendRequestCommand, SendFriendRequestResponse>
    {
        private readonly INotificationService _notificationService;
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;

        public SendFriendRequestCommandHandler(INotificationService notificationService, IDbContext dbContext, IUserContext userContext)
        {
            _notificationService = notificationService;
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task<SendFriendRequestResponse> Handle(SendFriendRequestCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId();
            if (request.ReceiverId == Guid.Empty || request.ReceiverId == userId)
                throw new InvalidOperationException("Wrong parameter");
            
            if(await _dbContext.FriendRequests
                   .FirstOrDefaultAsync(x => x.ReceiverId == request.ReceiverId && x.SenderId == userId
                       || x.SenderId == request.ReceiverId && x.ReceiverId == userId,
                       cancellationToken: cancellationToken) != null)
                throw new InvalidOperationException("This request already exists");
            
            var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
               await _notificationService.SendNotificationAsync(request.ReceiverId, request.Message);

                var friendRequest = new FriendRequest
                {
                    CreatedDate = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    SenderId = userId,
                    ReceiverId= request.ReceiverId,
                    IsAccepted = false,
                };

                _dbContext.FriendRequests.Add(friendRequest);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return new SendFriendRequestResponse
                {
                    IsSuccessed = true
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return new SendFriendRequestResponse
                {
                    IsSuccessed = false,
                };
            }
        }
    }
}
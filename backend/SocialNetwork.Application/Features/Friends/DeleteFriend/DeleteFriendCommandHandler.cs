using MediatR;
using FilmMatch.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.Friends.DeleteFriend
{
    public class DeleteFriendCommandHandler : IRequestHandler<DeleteFriendCommand, bool>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;
        public DeleteFriendCommandHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }
        public async Task<bool> Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _userContext.GetUserId();
            var friendship = await _dbContext.UserFriends.FirstOrDefaultAsync(
                f => (f.UserId == currentUserId && f.FriendId == request.FriendId) ||
                     (f.UserId == request.FriendId && f.FriendId == currentUserId),
                cancellationToken);
            if (friendship == null)
                return false;
            _dbContext.UserFriends.Remove(friendship);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
} 
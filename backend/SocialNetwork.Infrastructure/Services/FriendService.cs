using FilmMatch.Application.Interfaces;
using FilmMatch.Application.Interfaces.Services;
using FilmMatch.Domain.Entities;

namespace FilmMatch.Infrastructure.Services
{
    public class FriendService : IFriendsService
    {
        private readonly IUserContext _userContext;
        private readonly IDbContext _dbContext;

        public FriendService(IUserContext userContext, IDbContext dbContext)
        {
            _userContext = userContext;
            _dbContext = dbContext;
        }

        public async Task<int> AddFriendAsync(Guid friendId)
        {
            var currentUserId = _userContext.GetUserId();
            
            if(_dbContext.UserFriends.Any(x => x.UserId == friendId && x.FriendId == currentUserId 
                || x.UserId == currentUserId && x.FriendId == friendId))
                return 0;
            
            var friendship = new UserFriend
            {
                Id = Guid.NewGuid(),
                UserId = _userContext.GetUserId(),
                FriendId = friendId,
            };
            
            await _dbContext.UserFriends.AddAsync(friendship);
            return await _dbContext.SaveChangesAsync();
        }
        
        public IQueryable<User> GetFriends(Guid userId)
        {
            var friends =  _dbContext.UserFriends.Where(x => x.UserId == userId || x.FriendId == userId);
            return _dbContext.Users.Where(x => friends.Any(f => 
                f.UserId == x.Id && f.UserId != userId
                || f.FriendId == x.Id && f.FriendId != userId));
        }
    }
}
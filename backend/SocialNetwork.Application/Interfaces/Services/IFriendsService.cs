using FilmMatch.Domain.Entities;

namespace FilmMatch.Application.Interfaces.Services
{
    public interface IFriendsService
    {
        public Task<int> AddFriendAsync(Guid friendId);

        public IQueryable<User> GetFriends(Guid userId);
    }
}
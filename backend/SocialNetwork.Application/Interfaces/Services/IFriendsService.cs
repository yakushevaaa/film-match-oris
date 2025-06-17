namespace FilmMatch.Application.Interfaces.Services
{
    public interface IFriendsService
    {
        public Task<int> AddFriendAsync(Guid friendId);
    }
}
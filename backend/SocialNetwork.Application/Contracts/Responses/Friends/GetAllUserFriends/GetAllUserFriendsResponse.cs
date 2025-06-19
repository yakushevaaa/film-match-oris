using FilmMatch.Domain.Entities;

namespace FilmMatch.Application.Contracts.Responses.Friends.GetAllUserFriends
{
    public class GetAllUserFriendsResponse
    {
        public GetAllUserFriendsResponse(List<GetAllPossibleFriendsUserDto> friends)
        {
            Friends = friends;
        }
        
        public List<GetAllPossibleFriendsUserDto> Friends { get; set; } = new();
    }
}
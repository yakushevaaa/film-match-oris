using FilmMatch.Domain.Entities;

namespace FilmMatch.Application.Contracts.Responses.Friends.GetAllUserFriends
{
    public class GetAllUserFriendsResponse
    {
        public GetAllUserFriendsResponse(List<GetAllUserFriendsFriendDto> friends)
        {
            Friends = friends;
        }
        
        public List<GetAllUserFriendsFriendDto> Friends { get; set; } = new();
    }
}
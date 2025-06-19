namespace FilmMatch.Application.Contracts.Responses.Friends.GetAllPossibleFriends
{
    public class GetAllPossibleFriendsResponse
    {
        public GetAllPossibleFriendsResponse(List<GetAllPossibleFriendsUserDto> users)
        {
            Users = users;
        }
        
        public List<GetAllPossibleFriendsUserDto> Users { get; set; } = new();
    }
}
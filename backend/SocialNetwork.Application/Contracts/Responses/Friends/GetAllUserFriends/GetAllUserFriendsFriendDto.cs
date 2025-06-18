namespace FilmMatch.Application.Contracts.Responses.Friends.GetAllUserFriends
{
    public class GetAllUserFriendsFriendDto
    {
        public Guid Id { get; set; }
        public Guid FriendId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
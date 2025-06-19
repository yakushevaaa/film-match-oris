namespace FilmMatch.Application.Contracts.Responses.FriendRequests.GetAllFriendRequests
{
    public class GetAllFriendRequestsResponse
    {
        public List<GetAllFriendRequestsResponseItem> Requests { get; set; } = new();
    }
}
namespace FilmMatch.Application.Contracts.Requests.UserRequests.GetUsernameByIdResponse
{
    public class GetUsernameByIdResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public GetUsernameByIdResponse(Guid id, string username)
        {
            Id = id;
            Username = username;
        }
    }
} 
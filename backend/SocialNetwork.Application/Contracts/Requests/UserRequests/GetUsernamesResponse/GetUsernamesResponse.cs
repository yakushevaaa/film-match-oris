namespace FilmMatch.Application.Contracts.Requests.UserRequests.GetUsernamesResponse
{
    public class GetUsernamesResponse
    {
        public List<GetUsernamesResponseItem> Useranames { get; set; }

        public GetUsernamesResponse(List<GetUsernamesResponseItem> usernames)
        {
            Useranames = usernames;
        }
    }
}
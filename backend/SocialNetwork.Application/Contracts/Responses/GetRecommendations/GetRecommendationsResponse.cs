using FilmMatch.Application.Contracts.Requests.UserRequests.GetUsernamesResponse;

namespace FilmMatch.Application.Contracts.Responses.GetRecommendations
{
    public class GetRecommendationsResponse
    {
        public List<GetRecommendationsResponseItem> Recommendations { get; set; } = new();
        // если null - то все хорошо, если false - то пользователь не лайкнул
        // ни одного фильма, если true то пользователь уже отреагировал на все фильмы
        public bool? IsEverythingReacted { get; set; }

        public GetRecommendationsResponse()
        {
        }
        
        public GetRecommendationsResponse(List<GetRecommendationsResponseItem> recommendations)
        {
            Recommendations = recommendations;
        }
    }
}
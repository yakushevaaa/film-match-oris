using FilmMatch.Application.Contracts.Responses.GetRecommendations;
using MediatR;

namespace FilmMatch.Application.Features.Films.GetRecommendations
{
    public class GetRecommendationsQuery : IRequest<GetRecommendationsResponse>
    {
    }
}
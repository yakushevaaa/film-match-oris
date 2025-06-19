using MediatR;
using FilmMatch.Application.Contracts.Responses.Films.LikeFilm;

namespace FilmMatch.Application.Features.Films.LikeFilm
{
    public class GetLikedFilmsQuery : IRequest<GetLikedFilmsResponse>
    {
    }
} 
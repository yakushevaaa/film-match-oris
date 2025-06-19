using MediatR;
using FilmMatch.Application.Contracts.Responses.Films.DislikeFilm;

namespace FilmMatch.Application.Features.Films.DislikeFilm
{
    public class GetDislikedFilmsQuery : IRequest<GetDislikedFilmsResponse>
    {
    }
} 
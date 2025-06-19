using MediatR;
using FilmMatch.Application.Contracts.Responses.Films.GetAllFilms;

namespace FilmMatch.Application.Features.Films.GetAllFilms
{
    public class GetAllFilmsQuery : IRequest<GetAllFilmsResponse>
    {
    }
} 
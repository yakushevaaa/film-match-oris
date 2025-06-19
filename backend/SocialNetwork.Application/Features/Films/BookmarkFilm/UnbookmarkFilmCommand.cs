using MediatR;
using FilmMatch.Application.Contracts.Responses.Films.BookmarkFilm;

namespace FilmMatch.Application.Features.Films.BookmarkFilm
{
    public class UnbookmarkFilmCommand : IRequest<UnbookmarkFilmResponse>
    {
        public Guid FilmId { get; set; }
        public UnbookmarkFilmCommand(Guid filmId)
        {
            FilmId = filmId;
        }
    }
} 
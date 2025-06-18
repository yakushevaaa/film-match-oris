using MediatR;
using System;
using FilmMatch.Application.Contracts.Responses.Films.BookmarkFilm;

namespace FilmMatch.Application.Features.Films.BookmarkFilm
{
    public class BookmarkFilmCommand : IRequest<BookmarkFilmResponse>
    {
        public Guid FilmId { get; set; }
        public BookmarkFilmCommand(Guid filmId)
        {
            FilmId = filmId;
        }
    }
} 
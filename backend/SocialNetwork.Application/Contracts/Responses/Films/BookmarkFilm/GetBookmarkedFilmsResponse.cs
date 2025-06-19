using System.Collections.Generic;

namespace FilmMatch.Application.Contracts.Responses.Films.BookmarkFilm
{
    public class GetBookmarkedFilmsResponse
    {
        public List<BookmarkedFilmDto> Films { get; set; } = new();
    }
} 
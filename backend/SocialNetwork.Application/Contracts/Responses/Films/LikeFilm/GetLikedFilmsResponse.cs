using System.Collections.Generic;

namespace FilmMatch.Application.Contracts.Responses.Films.LikeFilm
{
    public class GetLikedFilmsResponse
    {
        public List<LikedFilmDto> Films { get; set; } = new();
    }
} 
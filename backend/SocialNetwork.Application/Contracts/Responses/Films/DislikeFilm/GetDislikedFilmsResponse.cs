using System.Collections.Generic;

namespace FilmMatch.Application.Contracts.Responses.Films.DislikeFilm
{
    public class GetDislikedFilmsResponse
    {
        public List<DislikedFilmDto> Films { get; set; } = new();
    }
} 
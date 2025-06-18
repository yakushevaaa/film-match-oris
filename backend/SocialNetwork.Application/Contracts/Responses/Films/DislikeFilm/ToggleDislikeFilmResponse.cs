namespace FilmMatch.Application.Contracts.Responses.Films.DislikeFilm
{
    public class ToggleDislikeFilmResponse
    {
        public bool IsDisliked { get; set; }
        public string? Message { get; set; }
    }
} 
namespace FilmMatch.Application.Contracts.Responses.Films.LikeFilm
{
    public class ToggleLikeFilmResponse
    {
        public bool IsLiked { get; set; }
        public string? Message { get; set; }
    }
} 
namespace FilmMatch.Application.Contracts.Responses.Films.GetAllFilms
{
    public class GetAllFilmsResponse
    {
        public List<GetAllFilmsDto> Films { get; set; } = new();
    }
} 
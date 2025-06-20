using MediatR;
using Microsoft.AspNetCore.Http;

namespace FilmMatch.Application.Features.Films.UpdateFilm
{
    public class UpdateFilmCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public IFormFile? Image { get; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }
        public Guid CategoryId { get; set; }
    }
} 
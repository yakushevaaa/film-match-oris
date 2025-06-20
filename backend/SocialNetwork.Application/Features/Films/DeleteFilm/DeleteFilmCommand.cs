using MediatR;

namespace FilmMatch.Application.Features.Films.DeleteFilm
{
    public class DeleteFilmCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteFilmCommand(Guid id)
        {
            Id = id;
        }
    }
} 
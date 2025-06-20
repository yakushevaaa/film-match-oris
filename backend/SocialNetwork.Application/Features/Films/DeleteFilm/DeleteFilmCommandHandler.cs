using MediatR;
using FilmMatch.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.Films.DeleteFilm
{
    public class DeleteFilmCommandHandler : IRequestHandler<DeleteFilmCommand, bool>
    {
        private readonly IDbContext _dbContext;
        public DeleteFilmCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Handle(DeleteFilmCommand request, CancellationToken cancellationToken)
        {
            var film = await _dbContext.Films.FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
            if (film == null)
                return false;
            film.IsDeleted = true;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
} 
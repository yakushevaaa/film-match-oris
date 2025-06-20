using MediatR;
using FilmMatch.Application.Interfaces;
using FilmMatch.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.Films.UpdateFilm
{
    public class UpdateFilmCommandHandler : IRequestHandler<UpdateFilmCommand, bool>
    {
        private readonly IDbContext _dbContext;
        private readonly IS3Service _s3Service;
        public UpdateFilmCommandHandler(IDbContext dbContext, IS3Service s3Service)
        {
            _dbContext = dbContext;
            _s3Service = s3Service;
        }
        public async Task<bool> Handle(UpdateFilmCommand request, CancellationToken cancellationToken)
        {
            var film = await _dbContext.Films.FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
            if (film == null)
                return false;
            film.Title = request.Title;
            film.ReleaseDate = request.ReleaseDate;
            film.LongDescription = request.LongDescription;
            film.ShortDescription = request.ShortDescription;
            film.CategoryId = request.CategoryId;
            if (request.Image != null && request.Image.Length > 0)
            {
                var url = await _s3Service.UploadAsync(request.Image, cancellationToken);
                film.ImageUrl = url;
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
} 
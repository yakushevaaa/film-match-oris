using FilmMatch.Application.Interfaces;
using FilmMatch.Application.Interfaces.Services;
using FilmMatch.Domain.Entities;
using MediatR;

namespace FilmMatch.Application.Features.Films.CreateFilm
{
    public class CreateFilmCommandHandler : IRequestHandler<CreateFilmCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IS3Service _s3Service;

        public CreateFilmCommandHandler(IDbContext dbContext, IUserContext userContext, IS3Service s3Service)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _s3Service = s3Service;
        }

        public async Task Handle(CreateFilmCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                if (request.Image.Length == 0)
                    throw new NullReferenceException("Please provide an image.");

                var url = await _s3Service.UploadAsync(request.Image, cancellationToken);

                var film = new Film
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow,
                    Title = request.Title,
                    ImageUrl = url,
                    LongDescription = request.LongDescription,
                    ShortDescription = request.ShortDescription,
                    CategoryId = request.CategoryId,
                    ReleaseDate = request.ReleaseDate,
                };

                _dbContext.Films.Add(film);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
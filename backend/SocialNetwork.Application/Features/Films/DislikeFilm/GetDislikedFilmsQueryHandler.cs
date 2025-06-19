using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FilmMatch.Application.Interfaces;
using FilmMatch.Application.Contracts.Responses.Films.DislikeFilm;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FilmMatch.Application.Features.Films.DislikeFilm
{
    public class GetDislikedFilmsQueryHandler : IRequestHandler<GetDislikedFilmsQuery, GetDislikedFilmsResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;
        public GetDislikedFilmsQueryHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }
        public async Task<GetDislikedFilmsResponse> Handle(GetDislikedFilmsQuery request, CancellationToken cancellationToken)
        {
            var userId = request.UserId ?? _userContext.GetUserId();
            var films = await _dbContext.UserDislikedFilm
                .Where(x => x.UserId == userId)
                .Include(x => x.Film).ThenInclude(f => f.Category)
                .Select(x => x.Film)
                .ToListAsync(cancellationToken);

            var response = new GetDislikedFilmsResponse
            {
                Films = films.Select(f => new DislikedFilmDto
                {
                    Id = f.Id,
                    Title = f.Title,
                    ReleaseDate = f.ReleaseDate,
                    ImageUrl = f.ImageUrl,
                    LongDescription = f.LongDescription,
                    ShortDescription = f.ShortDescription,
                    Category = f.Category == null ? null : new DislikedCategoryDto
                    {
                        Id = f.Category.Id,
                        Name = f.Category.Name
                    }
                }).ToList()
            };
            return response;
        }
    }
} 
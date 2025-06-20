using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FilmMatch.Application.Interfaces;
using FilmMatch.Application.Contracts.Responses.Films.LikeFilm;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FilmMatch.Application.Features.Films.LikeFilm
{
    public class GetLikedFilmsQueryHandler : IRequestHandler<GetLikedFilmsQuery, GetLikedFilmsResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;
        public GetLikedFilmsQueryHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }
        public async Task<GetLikedFilmsResponse> Handle(GetLikedFilmsQuery request, CancellationToken cancellationToken)
        {
            var userId = request.UserId ?? _userContext.GetUserId();
            var films = await _dbContext.UserLikedFilm
                .Where(x => x.UserId == userId)
                .Include(x => x.Film).ThenInclude(f => f.Category)
                .Select(x => x.Film)
                .ToListAsync(cancellationToken);

            var response = new GetLikedFilmsResponse
            {
                Films = films.Select(f => new LikedFilmDto
                {
                    Id = f.Id,
                    Title = f.Title,
                    ReleaseDate = f.ReleaseDate,
                    ImageUrl = f.ImageUrl,
                    LongDescription = f.LongDescription,
                    ShortDescription = f.ShortDescription,
                    Category = f.Category == null ? null : new LikedCategoryDto
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
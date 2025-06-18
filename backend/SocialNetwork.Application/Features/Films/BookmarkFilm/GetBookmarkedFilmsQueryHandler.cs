using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FilmMatch.Application.Interfaces;
using FilmMatch.Application.Contracts.Responses.Films.BookmarkFilm;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FilmMatch.Application.Features.Films.BookmarkFilm
{
    public class GetBookmarkedFilmsQueryHandler : IRequestHandler<GetBookmarkedFilmsQuery, GetBookmarkedFilmsResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;
        public GetBookmarkedFilmsQueryHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }
        public async Task<GetBookmarkedFilmsResponse> Handle(GetBookmarkedFilmsQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId();
            var films = await _dbContext.UserBookmarkedFilm
                .Where(x => x.UserId == userId)
                .Include(x => x.Film).ThenInclude(f => f.Category)
                .Select(x => x.Film)
                .ToListAsync(cancellationToken);

            var response = new GetBookmarkedFilmsResponse
            {
                Films = films.Select(f => new BookmarkedFilmDto
                {
                    Id = f.Id,
                    Title = f.Title,
                    ReleaseDate = f.ReleaseDate,
                    ImageUrl = f.ImageUrl,
                    LongDescription = f.LongDescription,
                    ShortDescription = f.ShortDescription,
                    Category = f.Category == null ? null : new CategoryDto
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
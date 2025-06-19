using MediatR;
using FilmMatch.Application.Interfaces;
using FilmMatch.Application.Contracts.Responses.Films.GetAllFilms;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.Films.GetAllFilms
{
    public class GetAllFilmsQueryHandler : IRequestHandler<GetAllFilmsQuery, GetAllFilmsResponse>
    {
        private readonly IDbContext _dbContext;
        public GetAllFilmsQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<GetAllFilmsResponse> Handle(GetAllFilmsQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Films.Include(f => f.Category).AsQueryable();
            if (request.CategoryId.HasValue)
            {
                query = query.Where(f => f.CategoryId == request.CategoryId.Value);
            }
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var searchLower = request.Search.ToLower();
                query = query.Where(f => f.Title.ToLower().Contains(searchLower));
            }
            var films = await query
                .Select(f => new GetAllFilmsDto
                {
                    Id = f.Id,
                    Title = f.Title,
                    ReleaseDate = f.ReleaseDate,
                    ImageUrl = f.ImageUrl,
                    LongDescription = f.LongDescription,
                    ShortDescription = f.ShortDescription,
                    Category = f.Category == null ? null : new GetAllFilmsCategoryDto
                    {
                        Id = f.Category.Id,
                        Name = f.Category.Name
                    }
                })
                .ToListAsync(cancellationToken);
            return new GetAllFilmsResponse { Films = films };
        }
    }
} 
using MediatR;
using FilmMatch.Application.Interfaces;
using FilmMatch.Application.Contracts.Responses.Categories.GetAllCategories;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.Categories.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, GetAllCategoriesResponse>
    {
        private readonly IDbContext _dbContext;
        public GetAllCategoriesQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<GetAllCategoriesResponse> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _dbContext.Categories
                .Select(c => new GetAllCategoriesDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    ImageUrl = c.ImageUrl,
                })
                .ToListAsync(cancellationToken);
            return new GetAllCategoriesResponse { Categories = categories };
        }
    }
} 
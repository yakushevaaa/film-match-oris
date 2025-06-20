using MediatR;
using FilmMatch.Application.Interfaces;
using FilmMatch.Domain.Entities;

namespace FilmMatch.Application.Features.Categories.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly IDbContext _dbContext;
        public CreateCategoryCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                ImageUrl = request.ImageUrl,
                CreatedDate = DateTime.UtcNow,
            };
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return category.Id;
        }
    }
} 
using MediatR;
using FilmMatch.Application.Interfaces;
using FilmMatch.Domain.Entities;
using FilmMatch.Application.Interfaces.Services;

namespace FilmMatch.Application.Features.Categories.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly IDbContext _dbContext;
        private readonly IS3Service _s3Service;
        public CreateCategoryCommandHandler(IDbContext dbContext, IS3Service s3Service)
        {
            _dbContext = dbContext;
            _s3Service = s3Service;
        }
        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.Image == null || request.Image.Length == 0)
                throw new ArgumentException("Image is required");
            var url = await _s3Service.UploadAsync(request.Image, cancellationToken);
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                ImageUrl = url,
                CreatedDate = DateTime.UtcNow,
            };
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return category.Id;
        }
    }
} 
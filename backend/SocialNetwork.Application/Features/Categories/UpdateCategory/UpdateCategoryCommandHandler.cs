using MediatR;
using FilmMatch.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.Categories.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly IDbContext _dbContext;
        public UpdateCategoryCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (category == null)
                return false;
            category.Name = request.Name;
            category.ImageUrl = request.ImageUrl;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
} 
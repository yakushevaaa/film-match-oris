using MediatR;
using FilmMatch.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.Categories.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IDbContext _dbContext;
        public DeleteCategoryCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (category == null)
                return false;
            category.IsDeleted = true;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
} 
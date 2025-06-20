using MediatR;

namespace FilmMatch.Application.Features.Categories.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteCategoryCommand(Guid id)
        {
            Id = id;
        }
    }
} 
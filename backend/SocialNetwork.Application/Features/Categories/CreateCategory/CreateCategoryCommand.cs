using MediatR;

namespace FilmMatch.Application.Features.Categories.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
} 
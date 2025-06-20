using MediatR;
using Microsoft.AspNetCore.Http;

namespace FilmMatch.Application.Features.Categories.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
    }
} 
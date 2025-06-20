using MediatR;
using Microsoft.AspNetCore.Http;

namespace FilmMatch.Application.Features.Categories.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
} 
using MediatR;
using FilmMatch.Application.Contracts.Responses.Categories.GetAllCategories;

namespace FilmMatch.Application.Features.Categories.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<GetAllCategoriesResponse>
    {
    }
} 
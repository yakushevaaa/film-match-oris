namespace FilmMatch.Application.Contracts.Responses.Categories.GetAllCategories
{
    public class GetAllCategoriesResponse
    {
        public List<GetAllCategoriesDto> Categories { get; set; } = new();
    }
} 
namespace FilmMatch.Application.Contracts.Responses.Categories.GetAllCategories
{
    public class GetAllCategoriesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
} 
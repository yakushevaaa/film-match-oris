namespace FilmMatch.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAlt { get; set; }
        public int CategoryId { get; set; }
    }
}

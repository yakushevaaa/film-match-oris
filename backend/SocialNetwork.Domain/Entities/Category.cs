using FilmMatch.Domain.Entities.Common;

namespace FilmMatch.Domain.Entities
{
     public class Category: BaseAuditableEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        // Навигационное свойство
        public ICollection<Film> Films { get; set; } = new List<Film>();
    }
}

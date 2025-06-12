using Core.Entities.Common;
using FilmMatch.Domain.Entities.Common;

namespace FilmMatch.Domain.Entities
{
    public class Film: BaseAuditableEntity
    {
        public string Title { get; set; }

        public DateTime? ReleaseDate { get; set; }
        public string ImageUrl { get; set; }

        public string LongDescription { get; set; }

        public string ShortDescription { get; set; }

        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        // Навигационные свойства для связей с пользователями
        public ICollection<UserLikedFilm> LikedByUsers { get; set; } = new List<UserLikedFilm>();
        public ICollection<UserDislikedFilm> DislikedByUsers { get; set; } = new List<UserDislikedFilm>();
        public ICollection<UserBookmarkedFilm> BookmarkedByUsers { get; set; } = new List<UserBookmarkedFilm>();
    }
}

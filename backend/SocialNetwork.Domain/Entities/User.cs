using FilmMatch.Domain.Entities.Common;

namespace FilmMatch.Domain.Entities
{
    public class User : BaseAuditableEntity
    {
        public Guid IdentityUserId { get; set; }
        
        public string Name { get; set; }

        public bool HasSubscription { get; set; }

        public string Email { get; set; } = String.Empty;

        // Many-to-many self-reference: друзья
        public ICollection<UserFriend> Friends { get; set; } = new List<UserFriend>();
        public ICollection<UserFriend> FriendOf { get; set; } = new List<UserFriend>();

        // Many-to-many с фильмами
        public ICollection<UserLikedFilm> LikedFilms { get; set; } = new List<UserLikedFilm>();
        public ICollection<UserBookmarkedFilm> BookmarkedFilms { get; set; } = new List<UserBookmarkedFilm>();
        public ICollection<UserDislikedFilm> DislikedFilms { get; set; } = new List<UserDislikedFilm>();
    }
}
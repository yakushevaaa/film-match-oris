using FilmMatch.Domain.Entities.Common;

namespace FilmMatch.Domain.Entities;

public class UserBookmarkedFilm: BaseAuditableEntity
{
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public Guid FilmId { get; set; }
    public Film? Film { get; set; }
}
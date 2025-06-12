using Core.Entities.Common;

namespace FilmMatch.Domain.Entities;

public class UserFriend: BaseAuditableEntity
{
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public Guid FriendId { get; set; }
    public User? Friend { get; set; }
}
using FilmMatch.Domain.Entities.Common;

namespace FilmMatch.Domain.Entities
{
    public class FriendRequest : BaseAuditableEntity
    {
        public Guid SenderId { get; set; }
        public User? Sender { get; set; }
        public Guid ReceiverId { get; set; }
        public User? Receiver { get; set; }
        public bool IsAccepted { get; set; }
    }
}
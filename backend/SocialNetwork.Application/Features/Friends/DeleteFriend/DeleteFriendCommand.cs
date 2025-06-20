using MediatR;

namespace FilmMatch.Application.Features.Friends.DeleteFriend
{
    public class DeleteFriendCommand : IRequest<bool>
    {
        public Guid FriendId { get; set; }
        public DeleteFriendCommand(Guid friendId)
        {
            FriendId = friendId;
        }
    }
} 
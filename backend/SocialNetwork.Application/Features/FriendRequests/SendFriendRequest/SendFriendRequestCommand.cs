using FilmMatch.Application.Contracts.Responses.FriendRequests.SendFriendRequest;
using MediatR;

namespace FilmMatch.Application.Features.Friends.SendFriendRequest
{
    public class SendFriendRequestCommand : IRequest<SendFriendRequestResponse>
    {
        public Guid ReceiverId { get; set; }
        public string Message { get; set; }  = "friend request"; // Тут с фронта сама сообщение придумаешь
    }
}
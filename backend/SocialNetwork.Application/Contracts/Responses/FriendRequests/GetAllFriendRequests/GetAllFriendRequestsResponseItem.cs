namespace FilmMatch.Application.Contracts.Responses.FriendRequests.GetAllFriendRequests
{
    public class GetAllFriendRequestsResponseItem
    {
        public Guid Id { get; set; }
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
    }
}
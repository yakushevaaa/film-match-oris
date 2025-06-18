using FilmMatch.Application.Contracts.Responses.FriendRequests.DeclineFriendRequest;
using FilmMatch.Application.Interfaces;
using MediatR;

namespace FilmMatch.Application.Features.FriendRequests.DeclineFriendRequest
{
    public class DeclineFriendRequestCommandHandler : IRequestHandler<DeclineFriendRequestCommand, DeclineFriendRequestResponse>
    {
        private readonly IDbContext _context;

        public DeclineFriendRequestCommandHandler(IDbContext context, IUserContext userContext)
        {
            _context = context;
        }

        public async Task<DeclineFriendRequestResponse> Handle(DeclineFriendRequestCommand request, CancellationToken cancellationToken)
        {
            var friendRequest = await _context.FriendRequests.FindAsync(request.FriendRequestId, cancellationToken);
            if(friendRequest == null)
                throw new InvalidOperationException("Friend request not found");
            
            _context.FriendRequests.Remove(friendRequest);
            await _context.SaveChangesAsync(cancellationToken);
            
            return new DeclineFriendRequestResponse
            {
                IsSucceded = true,
            };
        }
    }
}
using FilmMatch.Application.Contracts.Responses.Friends.GetAllUserFriends;
using FilmMatch.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.Friends.GetAllUserFriends
{
    public class GetAllUserFriendsQueryHandler : IRequestHandler<GetAllUserFriendsQuery, GetAllUserFriendsResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;

        public GetAllUserFriendsQueryHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task<GetAllUserFriendsResponse> Handle(GetAllUserFriendsQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId();
            var friends = await _dbContext.UserFriends
                .Where(f => f.FriendId == userId || f.UserId == userId)
                .Select(x => new GetAllPossibleFriendsUserDto
                {
                    Id = x.Id,
                    FriendId = x.UserId != userId ? x.UserId : x.FriendId,
                    Name = x.UserId != userId ? x.User!.Name : x.Friend!.Name,
                    Email = x.UserId != userId ? x.User!.Email : x.Friend!.Email,
                })
                .ToListAsync(cancellationToken: cancellationToken);
            
            return new GetAllUserFriendsResponse(friends);
        }
    }
}
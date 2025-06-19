using FilmMatch.Application.Contracts.Responses.Friends.GetAllPossibleFriends;
using FilmMatch.Application.Interfaces;
using FilmMatch.Application.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.Friends.GetAllPossibleFriends
{
    public class GetAllPossibleFriendsQueryHandler : IRequestHandler<GetAllPossibleFriendsQuery, GetAllPossibleFriendsResponse>
    {
        private readonly IUserContext _userContext;
        private readonly IDbContext _dbContext;
        private readonly IFriendsService _friendsService;

        public GetAllPossibleFriendsQueryHandler(IUserContext userContext, IDbContext dbContext, IFriendsService friendsService)
        {
            _userContext = userContext;
            _dbContext = dbContext;
            _friendsService = friendsService;
        }

        public async Task<GetAllPossibleFriendsResponse> Handle(GetAllPossibleFriendsQuery request, CancellationToken cancellationToken)
        {
            var excludeIds = _friendsService.GetFriends(_userContext.GetUserId());
            var users = _dbContext.Users.Where(x => x.Id != _userContext.GetUserId()
                                                          && !excludeIds.Contains(x));
            var response = await users.Select(x => new GetAllPossibleFriendsUserDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
            }).ToListAsync(cancellationToken: cancellationToken);

            return new GetAllPossibleFriendsResponse(response);
        }
    }
}
using FilmMatch.Application.Contracts.Requests.UserRequests.GetUsernamesResponse;
using FilmMatch.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.Users.Queries.GetUsernames
{
    public  class GetUsernamesQueryHandler : IRequestHandler<GetUsernamesQuery, GetUsernamesResponse>
    {
        private readonly IDbContext _context;
        private readonly IUserContext _userContex;

        public GetUsernamesQueryHandler(IUserContext userContex, IDbContext context)
        {
            _userContex = userContex;
            _context = context;
        }

        public async Task<GetUsernamesResponse> Handle(GetUsernamesQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContex.GetUserId();
            if (request.Query == null || request.Query.Trim().Length == 0)
                return new GetUsernamesResponse(await _context.Users.Where(x => x.Id != userId)
                    .Select(x => new GetUsernamesResponseItem
                    {
                        Id = x.Id,
                        Username = x.Name
                    })
                    .ToListAsync(cancellationToken: cancellationToken));
            
            return new GetUsernamesResponse(await _context.Users.Where(x => x.Id != userId && x.Name.ToLower().Contains(request.Query.ToLower()))
                .Select(x => new GetUsernamesResponseItem
                {
                    Id = x.Id,
                    Username = x.Name
                })
                .ToListAsync(cancellationToken: cancellationToken));
        }
    }
}
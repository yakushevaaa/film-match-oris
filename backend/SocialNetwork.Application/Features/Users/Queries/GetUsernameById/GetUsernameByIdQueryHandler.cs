using FilmMatch.Application.Contracts.Requests.UserRequests.GetUsernameByIdResponse;
using FilmMatch.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.Users.Queries.GetUsernameById
{
    public class GetUsernameByIdQueryHandler : IRequestHandler<GetUsernameByIdQuery, GetUsernameByIdResponse?>
    {
        private readonly IDbContext _dbContext;
        public GetUsernameByIdQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<GetUsernameByIdResponse?> Handle(GetUsernameByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
            if (user == null)
                return null;
            return new GetUsernameByIdResponse(user.Id, user.Name);
        }
    }
} 
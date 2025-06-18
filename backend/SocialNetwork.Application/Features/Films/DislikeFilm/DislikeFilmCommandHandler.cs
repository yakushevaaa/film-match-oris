using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FilmMatch.Application.Interfaces;
using FilmMatch.Domain.Entities;
using FilmMatch.Application.Contracts.Responses.Films.DislikeFilm;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.Films.DislikeFilm
{
    public class DislikeFilmCommandHandler : IRequestHandler<DislikeFilmCommand, DislikeFilmResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;
        public DislikeFilmCommandHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }
        public async Task<DislikeFilmResponse> Handle(DislikeFilmCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId();
            var exists = await _dbContext.UserDislikedFilm
                .AnyAsync(x => x.FilmId == request.FilmId && x.UserId == userId, cancellationToken);
            if (exists)
                return new DislikeFilmResponse { IsSuccessed = false, Message = "Already disliked" };
            _dbContext.UserDislikedFilm.Add(new UserDislikedFilm { FilmId = request.FilmId, UserId = userId });
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new DislikeFilmResponse { IsSuccessed = true };
        }
    }
} 
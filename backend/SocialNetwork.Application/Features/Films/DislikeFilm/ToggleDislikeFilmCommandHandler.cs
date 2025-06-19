using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FilmMatch.Application.Interfaces;
using FilmMatch.Domain.Entities;
using FilmMatch.Application.Contracts.Responses.Films.DislikeFilm;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.Films.DislikeFilm
{
    public class ToggleDislikeFilmCommandHandler : IRequestHandler<ToggleDislikeFilmCommand, ToggleDislikeFilmResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;
        public ToggleDislikeFilmCommandHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }
        public async Task<ToggleDislikeFilmResponse> Handle(ToggleDislikeFilmCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId();
            var dislike = await _dbContext.UserDislikedFilm
                .FirstOrDefaultAsync(x => x.FilmId == request.FilmId && x.UserId == userId, cancellationToken);
            if (dislike != null)
            {
                _dbContext.UserDislikedFilm.Remove(dislike);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return new ToggleDislikeFilmResponse { IsDisliked = false, Message = "Dislike removed" };
            }
            _dbContext.UserDislikedFilm.Add(
                new UserDislikedFilm
                {
                    Id = Guid.NewGuid(),
                    FilmId = request.FilmId,
                    UserId = userId
                });
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new ToggleDislikeFilmResponse { IsDisliked = true, Message = "Film disliked" };
        }
    }
} 
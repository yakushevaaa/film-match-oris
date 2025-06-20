using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FilmMatch.Application.Interfaces;
using FilmMatch.Domain.Entities;
using FilmMatch.Application.Contracts.Responses.Films.LikeFilm;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.Films.LikeFilm
{
    public class ToggleLikeFilmCommandHandler : IRequestHandler<ToggleLikeFilmCommand, ToggleLikeFilmResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;
        public ToggleLikeFilmCommandHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }
        public async Task<ToggleLikeFilmResponse> Handle(ToggleLikeFilmCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId();
            var like = await _dbContext.UserLikedFilm
                .FirstOrDefaultAsync(x => x.FilmId == request.FilmId && x.UserId == userId, cancellationToken);
            if (like != null)
            {
                _dbContext.UserLikedFilm.Remove(like);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return new ToggleLikeFilmResponse { IsLiked = false, Message = "Like removed" };
            }
            var dislike = await _dbContext.UserDislikedFilm.FirstOrDefaultAsync(x => x.FilmId == request.FilmId && x.UserId == userId, cancellationToken);
            if (dislike != null)
            {
                _dbContext.UserDislikedFilm.Remove(dislike);
            }
            _dbContext.UserLikedFilm.Add(new UserLikedFilm
            {
                Id = Guid.NewGuid(),
                FilmId = request.FilmId,
                UserId = userId
            });
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new ToggleLikeFilmResponse { IsLiked = true, Message = "Film liked" };
        }
    }
} 
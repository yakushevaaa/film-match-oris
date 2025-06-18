using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FilmMatch.Application.Interfaces;
using FilmMatch.Domain.Entities;
using FilmMatch.Application.Contracts.Responses.Films.DislikeFilm;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.Films.DislikeFilm
{
    public class UndislikeFilmCommandHandler : IRequestHandler<UndislikeFilmCommand, UndislikeFilmResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;
        public UndislikeFilmCommandHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }
        public async Task<UndislikeFilmResponse> Handle(UndislikeFilmCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId();
            var dislike = await _dbContext.UserDislikedFilm
                .FirstOrDefaultAsync(x => x.FilmId == request.FilmId && x.UserId == userId, cancellationToken);
            if (dislike == null)
                return new UndislikeFilmResponse { IsSuccessed = false, Message = "Not disliked" };
            _dbContext.UserDislikedFilm.Remove(dislike);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new UndislikeFilmResponse { IsSuccessed = true };
        }
    }
} 
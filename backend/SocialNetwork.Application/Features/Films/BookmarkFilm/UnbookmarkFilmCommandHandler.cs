using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FilmMatch.Application.Interfaces;
using FilmMatch.Domain.Entities;
using FilmMatch.Application.Contracts.Responses.Films.BookmarkFilm;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.Films.BookmarkFilm
{
    public class UnbookmarkFilmCommandHandler : IRequestHandler<UnbookmarkFilmCommand, UnbookmarkFilmResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;
        public UnbookmarkFilmCommandHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }
        public async Task<UnbookmarkFilmResponse> Handle(UnbookmarkFilmCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId();
            var bookmark = await _dbContext.UserBookmarkedFilm
                .FirstOrDefaultAsync(x => x.FilmId == request.FilmId && x.UserId == userId, cancellationToken);
            if (bookmark == null)
                return new UnbookmarkFilmResponse { IsSuccessed = false, Message = "Not bookmarked" };
            _dbContext.UserBookmarkedFilm.Remove(bookmark);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new UnbookmarkFilmResponse { IsSuccessed = true };
        }
    }
} 
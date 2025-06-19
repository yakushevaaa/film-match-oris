using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FilmMatch.Application.Interfaces;
using FilmMatch.Domain.Entities;
using FilmMatch.Application.Contracts.Responses.Films.BookmarkFilm;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.Films.BookmarkFilm
{
    public class BookmarkFilmCommandHandler : IRequestHandler<BookmarkFilmCommand, BookmarkFilmResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;
        public BookmarkFilmCommandHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }
        public async Task<BookmarkFilmResponse> Handle(BookmarkFilmCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId();
            var exists = await _dbContext.UserBookmarkedFilm
                .AnyAsync(x => x.FilmId == request.FilmId && x.UserId == userId, cancellationToken);
            if (exists)
                return new BookmarkFilmResponse { IsSuccessed = false, Message = "Already bookmarked" };
            _dbContext.UserBookmarkedFilm.Add(new UserBookmarkedFilm { FilmId = request.FilmId, UserId = userId });
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new BookmarkFilmResponse { IsSuccessed = true };
        }
    }
} 
using MediatR;
using FilmMatch.Application.Contracts.Responses.Films.BookmarkFilm;
using System.Collections.Generic;

namespace FilmMatch.Application.Features.Films.BookmarkFilm
{
    public class GetBookmarkedFilmsQuery : IRequest<GetBookmarkedFilmsResponse>
    {
    }
} 
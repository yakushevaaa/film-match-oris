using MediatR;
using FilmMatch.Application.Contracts.Responses.Films.LikeFilm;
using System;

namespace FilmMatch.Application.Features.Films.LikeFilm
{
    public class GetLikedFilmsQuery : IRequest<GetLikedFilmsResponse>
    {
        public Guid? UserId { get; set; }
        public GetLikedFilmsQuery(Guid? userId = null)
        {
            UserId = userId;
        }
        public GetLikedFilmsQuery() {}
    }
} 
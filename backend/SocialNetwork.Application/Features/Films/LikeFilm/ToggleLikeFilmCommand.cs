using MediatR;
using FilmMatch.Application.Contracts.Responses.Films.LikeFilm;
using System;

namespace FilmMatch.Application.Features.Films.LikeFilm
{
    public class ToggleLikeFilmCommand : IRequest<ToggleLikeFilmResponse>
    {
        public Guid FilmId { get; set; }
        public ToggleLikeFilmCommand(Guid filmId)
        {
            FilmId = filmId;
        }
    }
} 
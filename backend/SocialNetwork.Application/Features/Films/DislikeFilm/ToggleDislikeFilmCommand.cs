using MediatR;
using FilmMatch.Application.Contracts.Responses.Films.DislikeFilm;
using System;

namespace FilmMatch.Application.Features.Films.DislikeFilm
{
    public class ToggleDislikeFilmCommand : IRequest<ToggleDislikeFilmResponse>
    {
        public Guid FilmId { get; set; }
        public ToggleDislikeFilmCommand(Guid filmId)
        {
            FilmId = filmId;
        }
    }
} 
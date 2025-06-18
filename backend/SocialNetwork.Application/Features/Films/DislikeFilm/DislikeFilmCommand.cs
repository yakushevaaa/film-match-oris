using MediatR;
using FilmMatch.Application.Contracts.Responses.Films.DislikeFilm;
using System;

namespace FilmMatch.Application.Features.Films.DislikeFilm
{
    public class DislikeFilmCommand : IRequest<DislikeFilmResponse>
    {
        public Guid FilmId { get; set; }
        public DislikeFilmCommand(Guid filmId)
        {
            FilmId = filmId;
        }
    }
} 
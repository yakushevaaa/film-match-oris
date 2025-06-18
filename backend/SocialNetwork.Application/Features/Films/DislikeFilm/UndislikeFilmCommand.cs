using MediatR;
using FilmMatch.Application.Contracts.Responses.Films.DislikeFilm;
using System;

namespace FilmMatch.Application.Features.Films.DislikeFilm
{
    public class UndislikeFilmCommand : IRequest<UndislikeFilmResponse>
    {
        public Guid FilmId { get; set; }
        public UndislikeFilmCommand(Guid filmId)
        {
            FilmId = filmId;
        }
    }
} 
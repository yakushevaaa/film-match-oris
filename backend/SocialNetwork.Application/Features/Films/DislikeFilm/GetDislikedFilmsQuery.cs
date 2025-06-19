using MediatR;
using FilmMatch.Application.Contracts.Responses.Films.DislikeFilm;

namespace FilmMatch.Application.Features.Films.DislikeFilm
{
    public class GetDislikedFilmsQuery : IRequest<GetDislikedFilmsResponse>
    {
        public Guid? UserId { get; set; }
        public GetDislikedFilmsQuery(Guid? userId = null)
        {
            UserId = userId;
        }
        public GetDislikedFilmsQuery() {}
    }
} 
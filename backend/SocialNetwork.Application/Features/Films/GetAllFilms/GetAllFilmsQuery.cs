using MediatR;
using FilmMatch.Application.Contracts.Responses.Films.GetAllFilms;

namespace FilmMatch.Application.Features.Films.GetAllFilms
{
    public class GetAllFilmsQuery : IRequest<GetAllFilmsResponse>
    {
        public Guid? CategoryId { get; set; }
        public string? Search { get; set; }
        public GetAllFilmsQuery(Guid? categoryId = null, string? search = null)
        {
            CategoryId = categoryId;
            Search = search;
        }
        public GetAllFilmsQuery() {}
    }
} 
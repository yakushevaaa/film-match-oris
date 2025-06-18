using FilmMatch.Application.Contracts.Requests.UserRequests.GetUsernamesResponse;
using MediatR;

namespace FilmMatch.Application.Features.Users.Queries.GetUsernames
{
    public class GetUsernamesQuery : IRequest<GetUsernamesResponse>
    {
        public string? Query { get; set; }

        public GetUsernamesQuery(string? query)
        {
            Query = query;
        }
    }
}
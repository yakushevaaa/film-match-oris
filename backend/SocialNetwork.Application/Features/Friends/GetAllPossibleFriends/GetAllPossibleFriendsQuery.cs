using FilmMatch.Application.Contracts.Responses.Friends.GetAllPossibleFriends;
using MediatR;

namespace FilmMatch.Application.Features.Friends.GetAllPossibleFriends
{
    public class GetAllPossibleFriendsQuery : IRequest<GetAllPossibleFriendsResponse>
    {
    }
}
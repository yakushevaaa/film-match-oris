using FilmMatch.Application.Contracts.Responses.Friends.GetAllUserFriends;
using MediatR;

namespace FilmMatch.Application.Features.Friends.GetAllUserFriends
{
    public class GetAllUserFriendsQuery : IRequest<GetAllUserFriendsResponse>
    {
    }
}
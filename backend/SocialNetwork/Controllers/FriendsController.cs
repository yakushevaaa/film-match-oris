using FilmMatch.Application.Features.Friends.GetAllUserFriends;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FilmMatch.Controllers
{
    public class FriendsController(IMediator mediator) : Controller
    {
        [HttpGet("AllUserFriends")]
        public async Task<IActionResult> GetAllUserFriends()
        {
            return Ok(await mediator.Send(new GetAllUserFriendsQuery()));
        }
    }
}
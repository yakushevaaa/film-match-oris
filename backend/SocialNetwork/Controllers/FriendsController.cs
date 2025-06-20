using FilmMatch.Application.Features.Friends.GetAllPossibleFriends;
using FilmMatch.Application.Features.Friends.GetAllUserFriends;
using FilmMatch.Application.Features.Friends.DeleteFriend;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmMatch.Controllers
{
    [Authorize]
    public class FriendsController(IMediator mediator) : Controller
    {
        [HttpGet("AllUserFriends")]
        public async Task<IActionResult> GetAllUserFriends()
        {
            return Ok(await mediator.Send(new GetAllUserFriendsQuery()));
        }

        [HttpGet("AllPossibleFriends")]
        public async Task<IActionResult> GetAllPossibleFriends()
        {
            return Ok(await mediator.Send(new GetAllPossibleFriendsQuery()));
        }

        [HttpDelete("DeleteFriend/{friendId}")]
        public async Task<IActionResult> DeleteFriend(Guid friendId)
        {
            var result = await mediator.Send(new DeleteFriendCommand(friendId));
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
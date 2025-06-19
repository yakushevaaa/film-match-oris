using FilmMatch.Application.Features.FriendRequests.AcceptFriendrequest;
using FilmMatch.Application.Features.FriendRequests.DeclineFriendRequest;
using FilmMatch.Application.Features.FriendRequests.GetAllFriendRequests;
using FilmMatch.Application.Features.Friends.SendFriendRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FilmMatch.Controllers
{
    public class FriendRequestsController(IMediator mediator) : Controller
    {
        [HttpPost("friendRequest")]
        public async Task<IActionResult> SendRequest([FromQuery] Guid receiverId, string? message)
        {
            var result = await mediator.Send(new SendFriendRequestCommand
            {
                ReceiverId = receiverId,
                Message = message ?? string.Empty
            });
            return Ok(result);
        }

        [HttpGet("allFriendRequests")]
        public async Task<IActionResult> GetAllFriendRequests(GetAllFriendRequestsQuery query)
        {
            return Ok(await mediator.Send(query));
        }
        
        [HttpPost("accept")]
        public async Task<IActionResult> AcceptFriendRequest(AcceptFriendRequestCommand command)
        {
            return Ok(await mediator.Send(command));
        }
        
        [HttpPost("decline")]
        public async Task<IActionResult> DeclineFriendRequest(DeclineFriendRequestCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
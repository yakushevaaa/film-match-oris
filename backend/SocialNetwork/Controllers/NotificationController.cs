using FilmMatch.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmMatch.Controllers
{
    public class NotificationController(INotificationService notificationService) : ControllerBase
    {
        [HttpGet("/notification")]
        public async Task<IActionResult> SendNotification([FromQuery] Guid userId)
        {
            await notificationService.SendNotificationAsync(userId, "test");
            return Ok();
        }
    }
}
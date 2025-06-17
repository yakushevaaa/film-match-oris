using System.Security.Claims;
using FilmMatch.Application.Interfaces;
using FilmMatch.Application.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

namespace FilmMatch.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IUserContext _userContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNotificationAsync(Guid userId)
        {
            if(userId == Guid.Empty)
                throw new ArgumentNullException(nameof(userId));
            await _hubContext.Clients.Group(userId.ToString()).SendAsync("ReceiveNotification", userId);
        }
    }
}
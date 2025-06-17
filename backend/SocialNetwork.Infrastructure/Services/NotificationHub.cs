using FilmMatch.Application.Interfaces;
using FilmMatch.Application.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

namespace FilmMatch.Infrastructure.Services
{
    public class NotificationHub : Hub
    {
        private readonly IUserContext _userContext;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationHub(IUserService userService, IHubContext<NotificationHub> hubContext, IUserContext userContext)
        {
            _hubContext = hubContext;
            _userContext = userContext;
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "users");
            await Groups.AddToGroupAsync(Context.ConnectionId, _userContext.GetUserId().ToString());
            await base.OnConnectedAsync();
        }
    }
}
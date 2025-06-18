namespace FilmMatch.Application.Interfaces.Services
{
    public interface INotificationService
    {
        public Task SendNotificationAsync(Guid userId, string message);
    }
}
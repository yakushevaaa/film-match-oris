namespace FilmMatch.Application.Interfaces ;

    public interface IUserContext
    {
        public string? GetUserEmail();
        
        public Guid GetUserId();
    }
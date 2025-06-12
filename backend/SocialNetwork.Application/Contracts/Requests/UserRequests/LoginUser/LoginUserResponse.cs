namespace FilmMatch.Application.Contracts.Requests.UserRequests.LoginUser ;

    /// <summary>
    /// Ответ на запрос <see cref="LoginUserRequest"/>
    /// </summary>
    public class LoginUserResponse
    {
        public LoginUserResponse(string token)
        {
            Token = token;
        }
        
        /// <summary>
        /// Токен
        /// </summary>
        public string Token { get;}
    }
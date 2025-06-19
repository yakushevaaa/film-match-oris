using Microsoft.AspNetCore.Identity;

namespace FilmMatch.Application.Contracts.Responses.Users.UserToAdmin
{
    public class UserToAdminResponse
    {
        public IdentityResult Result { get; set; }

        public UserToAdminResponse(IdentityResult result)
        {
            Result = result;
        }
    }
}
//for future
/* 
using HamsterCoin.Domain;

namespace HamsterCoin.Auth
{
    public class AuthorizationRequest
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string? Promocode { get; set; }
    }

    public class AuthorizationResponse
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string? Promocode { get; set; }
    }

    public static class AuthorizationMappingExtensions
    {
        public static AuthorizationResponse ToResponsee(this User user)
        {
            return new AuthorizationResponse
            {
                Mail = user.Mail,
                Password = user.Password,
                Nickname = user.Nickname,
            };
        }

        public static User FromRequest(this AuthorizationRequest user)
        {
            return new User
            {
                Mail = user.Mail,
                Password = user.Password
            };
        }
    }
}
*/
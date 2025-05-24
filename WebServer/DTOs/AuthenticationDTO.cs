using HamsterCoin.Domain;

namespace HamsterCoin.DTO
{
    public class AuthenticationRequest
    {
        public string Mail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public static class AuthenticationMappingExtensions
    {
        public static User FromRequest(this AuthenticationRequest user)
        {
            return new User
            {
                Email = user.Mail,
                Password = user.Password
            };
        }
    }
}

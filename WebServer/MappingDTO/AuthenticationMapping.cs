using HamsterCoin.Domain;

namespace HamsterCoin.Mapping
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
                Mail = user.Mail,
                Password = user.Password
            };
        }
    }
}

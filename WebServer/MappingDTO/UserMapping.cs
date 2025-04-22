using HamsterCoin.Domain;

namespace HamsterCoin.Mapping
{
    public class UserRequest
    {
        public string Mail { get; set; }
        public string Password { get; set; }
    }

    public class UserResponse
    {
        public string Mail { get; set; }
        public string Password { get; set; }
    }

    public static class UserMappingExtensions
    {
        public static UserResponse ToResponse(this User user)
        {
            return new UserResponse
            {
                Mail = user.Mail,
                Password = user.Password
            };
        }

        public static User FromRequest(this UserRequest user)
        {
            return new User
            {
                Mail = user.Mail,
                Password = user.Password
            };
        }
    }
}

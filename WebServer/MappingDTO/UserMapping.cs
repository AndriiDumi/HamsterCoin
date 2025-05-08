using HamsterCoin.Domain;

namespace HamsterCoin.Mapping
{
    public class UserRequest
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string? Promocode { get; set; }
        public DateOnly BirthDate { get; set; }
    }

    public class UserResponse
    {
        public long Id { get; set; }
        public string Mail { get; set; }
        public string Nickname { get; set; }
        public string? Promocode { get; set; }
        public decimal Balance { get; set; }
        public DateOnly BirthDate { get; set; }
    }

    public static class UserMappingExtensions
    {
        public static UserResponse ToResponse(this User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Mail = user.Mail,
                Nickname = user.Nickname,
                Promocode = user.Promocode,
                Balance = user.Balance,
                BirthDate = user.BirthDate
            };
        }

        public static User FromRequest(this UserRequest user)
        {
            return new User
            {
                Mail = user.Mail,
                Password = user.Password,
                Nickname = user.Nickname,
                Promocode = user.Promocode,
                BirthDate = user.BirthDate
            };
        }
    }
}

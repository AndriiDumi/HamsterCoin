using HamsterCoin.Domain;

namespace HamsterCoin.DTO
{
    public class UserRequest
    {
        public required string Mail { get; set; }
        public required string Password { get; set; }
        public required string Nickname { get; set; }
        public DateOnly BirthDate { get; set; }
    }

    public class UserResponse
    {
        public long Id { get; set; }
        public required string Mail { get; set; }
        public required string Nickname { get; set; }
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
                Mail = user.Email,
                Nickname = user.Nickname,
                Balance = user.Balance,
                BirthDate = user.BirthDate
            };
        }

        public static User FromRequest(this UserRequest user)
        {
            return new User
            {
                Email = user.Mail,
                Password = user.Password,
                Nickname = user.Nickname,
                BirthDate = user.BirthDate
            };
        }
    }
}

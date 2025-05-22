using Microsoft.EntityFrameworkCore;
using HamsterCoin.Domain;

namespace HamsterCoin.Mapping
{
    public class LoginDto
    {
        public string Nick { get; set; } = null!;
        public decimal balance { get; set; }
        public string email { get; set; } = null!;
        public string accessToken { get; set; } = null!;
        public string refreshToken { get; set; } = null!;
    }
}

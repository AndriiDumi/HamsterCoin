using System.IdentityModel.Tokens.Jwt;
using HamsterCoin.Services.Interfaces;

namespace HamsterCoin.Services.Implementations
{
    public class JwtService() : IJwtService
    {
        public long GetUserId(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == "userId")?.Value;

            if (long.TryParse(userIdClaim, out long userId)) return userId;

            throw new Exception("Token don't contain userId or failed TryParse"); 
        }

 
    }
}


using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using HamsterCoin.Domain;
using System.Security.Cryptography;

namespace HamsterCoin.Auth
{
    public static class JwtTokenGenerator
    {

        public static string GenerateToken(long userId, string secretKey,  IConfiguration config, int expireMinutes = 60)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: config["JWT:Issuer"],
                audience: config["JWT:audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static RefreshToken GenerateRefreshToken(long userId, string secretKey, int expireDays = 7)
        {
            var token = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiresAt = DateTime.UtcNow.AddDays(expireDays),
                UserId = userId
            };

            return token;
        }
    }
}

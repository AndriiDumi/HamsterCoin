using HamsterCoin.Database;
using Microsoft.EntityFrameworkCore;
using HamsterCoin.Domain;
using HamsterCoin.Security;
using System.Drawing;

namespace HamsterCoin.Auth
{ 
    public class AuthenticationService(ApplicationDbContext dbContext, IPasswordEncoder passwordEncoder) : IAuthenticationService
    {
        public async Task<User> AuthenticateByUser(User user)
        {
            var userFound = await dbContext.Set<User>()
                .FirstOrDefaultAsync(u => u.Mail == user.Mail);

            if (userFound == null || !passwordEncoder.Verify(user.Password, userFound.Password))
            {
                throw new Exception("Invalid email or password");
            }         
            
            return userFound;
        }

        public async Task<RefreshToken> FindRefreshTokenByTokenAsync(string refreshToken)
        {
            var tokenFound = await dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshToken);
            if (tokenFound == null)
            {
                throw new Exception("Refresh token not found");
            }

            return tokenFound;
        }

        public async Task SaveRefreshTokenAsync(RefreshToken refreshToken)
        {
            dbContext.RefreshTokens.Add(refreshToken);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateRefreshTokenAsync(RefreshToken updatedRefreshToken)
        {
            dbContext.RefreshTokens.Update(updatedRefreshToken);
            await dbContext.SaveChangesAsync();
        }

        public async Task LogoutAsync(string refreshToken)
        {
            var token = await dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshToken);
            
            if (token == null || token.IsRevoked) 
            {
                throw new Exception("Token is already revoked or does not exist.");
            }

            token.IsRevoked = true;
            await dbContext.SaveChangesAsync();
        }

    }
}

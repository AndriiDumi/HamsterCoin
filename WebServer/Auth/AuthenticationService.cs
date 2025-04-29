using HamsterCoin.Database;
using Microsoft.EntityFrameworkCore;
using HamsterCoin.Domain;
using HamsterCoin.Security;

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
                throw new Exception("Invalid email or password.");
            }         
            
            return userFound;
        }

    }
}

using HamsterCoin.Database;
using Microsoft.EntityFrameworkCore;
using HamsterCoin.Domain;
using HamsterCoin.Services.Interfaces;

namespace HamsterCoin.Auth
{ 
    public class AuthenticationService(ApplicationDbContext dbContext) : IAuthenticationService
    {
        public async Task<User> AuthenticateByUser(User user)
        {
            var userFound = await dbContext.Set<User>()
                .FirstOrDefaultAsync(u => u.Mail == user.Mail && u.Password == user.Password);
            return userFound;
        }

    }
}

using HamsterCoin.Database;
using HamsterCoin.Services.Interfaces;
using HamsterCoin.Domain;
using Microsoft.EntityFrameworkCore;
using HamsterCoin.OperateException;
using HamsterCoin.Security;

namespace HamsterCoin.Services.Implementations
{
    public class UserService(ApplicationDbContext dbContext, IPasswordEncoder passwordEncoder) : IUserService
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task CreateAsync(User user)
        {
            user.Password = passwordEncoder.Encode(user.Password);
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(long id, User newEntity)
        {
            var existingUser = await _dbContext.Users.FindAsync(id);
            if (existingUser == null)
            {
                throw new NotFoundException($"User with ID {id} not found.");
            }

            _dbContext.Entry(existingUser).CurrentValues.SetValues(newEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateBalanceByUserIdAsync(decimal balance, long userId)
        {
            var existingUser = await _dbContext.Users.FindAsync(userId);
            if (existingUser == null) throw new Exception($"User with ID {userId} not found");

            existingUser.Balance = balance;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }
    }
}


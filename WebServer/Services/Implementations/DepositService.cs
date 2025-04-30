using HamsterCoin.Database;
using HamsterCoin.Services.Interfaces;
using HamsterCoin.Domain;
using Microsoft.EntityFrameworkCore;

namespace HamsterCoin.Services.Implementations
{
    public class DepositService(ApplicationDbContext dbContext) : IDepositService
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task CreateAsync(DepositHistory deposit)
        {
           _dbContext.DepHistory.Add(deposit);
           await _dbContext.SaveChangesAsync();
        }

        public async Task<List<DepositHistory>> GetHistoryDep(long UserId)
        {
            var entity = await _dbContext.DepHistory
                .Where(deposit => deposit.UserId == UserId)
                .ToListAsync(); 
            return entity ?? throw new Exception("no records found");
        }
    }
}
using HamsterCoin.Database;
using HamsterCoin.Services.Interfaces;
using HamsterCoin.Domain;
using HamsterCoin.Mapping;
using Microsoft.EntityFrameworkCore;

namespace HamsterCoin.Services.Implementations
{
    public class WithDrawService(ApplicationDbContext dbContext) : IWithDrawService
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task CreateAsync(WithdrawHistory withdraw)
        {
                _dbContext.WithdrawHistory.Add(withdraw);
                await _dbContext.SaveChangesAsync();
        }

        public async Task<List<WithdrawHistory>> GetAllHistoryWithdrawAsync(long UserId)
        {
            var entity = await _dbContext.WithdrawHistory
                .Where(withdraw => withdraw.UserId == UserId)
                .ToListAsync();
            return entity ?? throw new Exception("no records found");
        }
    }
}

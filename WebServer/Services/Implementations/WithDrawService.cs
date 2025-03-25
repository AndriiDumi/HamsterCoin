using HamsterCoin.Database;
using HamsterCoin.Services.Interfaces;
using HamsterCoin.Domain;
using HamsterCoin.DTO;
using Microsoft.EntityFrameworkCore;

namespace HamsterCoin.Services.Implementations
{
    public class WithDrawService(ApplicationDbContext dbContext) : IWithDrawService
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task CreateRecordAsync(WithdrawDTO withdraw)
        {
            using (var context = _dbContext)
            {
                var Withdraw = new WithdrawHistory
                {
                    UserId = withdraw.UserId,
                    SumWithdraw = withdraw.SumWithdraw,
                    User = withdraw.User,
                    DateWithdraw = withdraw.DateWithdraw
                };

                await context.WithdrawHistory.AddAsync(Withdraw);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<WithdrawHistory>> GetAllHistoryWithdrawAsync(long IdUser)
        {
            return await _dbContext.WithdrawHistory
                .Where(withdraw => withdraw.UserId == IdUser)
                .ToListAsync();
        }
    }
}

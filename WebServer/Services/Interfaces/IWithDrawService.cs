using HamsterCoin.Domain;
using HamsterCoin.Mapping;

namespace HamsterCoin.Services.Interfaces
{
    public interface IWithDrawService
    {
        Task CreateAsync(WithdrawHistory withdraw);
        Task<List<WithdrawHistory>> GetAllHistoryWithdrawAsync(long UserId);
    }
}

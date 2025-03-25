using HamsterCoin.Domain;
using HamsterCoin.DTO;

namespace HamsterCoin.Services.Interfaces
{
    public interface IWithDrawService
    {
        Task CreateRecordAsync(WithdrawDTO withdraw);
        Task<List<WithdrawHistory>> GetAllHistoryWithdrawAsync(long IdUser);
    }
}

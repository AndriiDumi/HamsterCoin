using HamsterCoin.Domain;

namespace HamsterCoin.Services.Interfaces
{
    public interface IDepositService{
        Task CreateAsync(DepositHistory deposit);
        Task <List<DepositHistory>> GetHistoryDep(long UserId);
    }
}
using HamsterCoin.Domain;

namespace HamsterCoin.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(User user);
        Task UpdateAsync(long id, User newEntity);

    }
}

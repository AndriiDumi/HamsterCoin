using HamsterCoin.Domain;

namespace HamsterCoin.Services.Interfaces
{
    public interface IGameService
    {
        Task CreateAsync(Game user);
        Task UpdateAsync(long id, Game newEntity);
    }
}
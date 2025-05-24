using HamsterCoin.Domain;

namespace HamsterCoin.Services.Interfaces
{
    public interface IGameService
    {
        Task UpdateAsync(long id, Game newEntity);
    }
}

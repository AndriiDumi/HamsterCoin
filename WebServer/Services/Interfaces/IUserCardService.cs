using HamsterCoin.Domain;

namespace HamsterCoin.Services.Interfaces
{
    public interface IUserCardService
    {
        Task CreateAsync(long userId, string numberCard); 
    }
}

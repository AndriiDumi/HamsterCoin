using HamsterCoin.Domain;

namespace HamsterCoin.Services.Interfaces
{
    public interface ICardService{
        Task CreateAsync(Card deposit);

        Task DeleteCard(string NumberCard);
    }
}
using HamsterCoin.Database;
using HamsterCoin.Domain;

public interface IPromocodeService
{
    Task<List<Promocode>> GetAllAsync();
    Task<Promocode?> GetByIdAsync(long userId);
    Task AddAsync(Promocode promocode);
    Task<bool> UpdateAsync(Promocode updated);
    Task<bool> DeleteAsync(long userId, string code);
}

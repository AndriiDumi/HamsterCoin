using HamsterCoin.Domain;

namespace HamsterCoin.Services.Interfaces
{
    public interface IUserDetailsService
    {
        Task CreateAsync(UserDetails entity);
        
        Task UpdateAsync(long id, UserDetails newEntity);
    }
    
}

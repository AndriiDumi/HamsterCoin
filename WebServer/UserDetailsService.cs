using HamsterCoin.Database;
using HamsterCoin.Services.Interfaces;
using HamsterCoin.Domain;

namespace HamsterCoin.Services.Implementations
{
    public class UserDetailsService(ApplicationDbContext dbContext) : IUserDetailsService
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task CreateAsync(UserDetails entity)
        {
            _dbContext.UserDetails.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(long id, UserDetails newEntity)
        {
            var oldEntity = await GetByIdAsync(id);
            
            UpdateValues(oldEntity, newEntity);
            _dbContext.Set<UserDetails>().Update(oldEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserDetails> GetByIdAsync(long id)
        {
            var entity = _dbContext.Set<UserDetails>().FindAsync(id);
            return await entity ?? throw new Exception("GetByIdAsync -> id is null");
        }

        private void UpdateValues(UserDetails oldEntity, UserDetails newEntity)
        {
            oldEntity.Nickname = newEntity.Nickname;
            oldEntity.Promocode = newEntity.Promocode;
            oldEntity.Balance = newEntity.Balance;
            oldEntity.BirthDate = newEntity.BirthDate;
        }
    }
}

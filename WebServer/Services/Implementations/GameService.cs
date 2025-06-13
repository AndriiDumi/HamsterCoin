using HamsterCoin.Database;
using HamsterCoin.Services.Interfaces;
using HamsterCoin.Domain;

namespace HamsterCoin.Services.Implementations
{
    public class GameService(ApplicationDbContext dbContext) : IGameService
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task UpdateAsync(long id, Game newEntity)
        {
            var oldEntity = await GetByIdAsync(id);
            
            UpdateValues(oldEntity, newEntity);
            _dbContext.Set<Game>().Update(oldEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Game> GetByIdAsync(long id)
        {
            var entity = _dbContext.Set<Game>().FindAsync(id);
            return await entity ?? throw new Exception("GetByIdAsync -> id is null");
        }

        private void UpdateValues(Game oldEntity, Game newEntity)
        {
            oldEntity.Name = newEntity.Name;
        }
    }
}

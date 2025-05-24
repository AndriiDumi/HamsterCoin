using HamsterCoin.Database;
using HamsterCoin.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using HamsterCoin.Domain;
using HamsterCoin.OperateException;

namespace HamsterCoin.Services.Implementations
{
    public class PromocodeService(ApplicationDbContext dbContext) : IPromocodeService
    {
        private readonly ApplicationDbContext _context = dbContext;

        public async Task<List<Promocode>> GetAllAsync()
        {
            return await _context.Promocodes.ToListAsync();
        }

        public async Task<Promocode?> GetByIdAsync(long userId)
        {
            return await _context.Promocodes
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task AddAsync(Promocode promocode)
        {
            _context.Promocodes.Add(promocode);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Promocode updated)
        {
            var existing = await _context.Promocodes
                .FirstOrDefaultAsync(p => p.UserId == updated.UserId && p.promocode == updated.promocode);

            if (existing == null)
                throw new NotFoundException("Not Found User with this promocode");

            existing.promocode = updated.promocode;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(long userId, string code)
        {
            var promocode = await _context.Promocodes
                .FirstOrDefaultAsync(p => p.UserId == userId && p.promocode == code);

            if (promocode == null)
                return false;

            _context.Promocodes.Remove(promocode);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

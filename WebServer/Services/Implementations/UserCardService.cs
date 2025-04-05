using HamsterCoin.Database;
using HamsterCoin.Services.Interfaces;
using HamsterCoin.Domain;
using Microsoft.EntityFrameworkCore;

namespace HamsterCoin.Services.Implementations
{
    public class UserCardService(ApplicationDbContext dbContext) : IUserCardService
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task CreateAsync(long userId, string numberCard)
        {
            var card = await _dbContext.Cards.FirstOrDefaultAsync(c => c.Number == numberCard);

            if (card == null)
                throw new Exception("Картку не знайдено");

            var userCard = new UserCard
            {
                UserId = userId,
                CardId = card.Id
            };

            await _dbContext.UserCards.AddAsync(userCard);
            await _dbContext.SaveChangesAsync();
        }

    }
}

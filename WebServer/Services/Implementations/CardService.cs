using HamsterCoin.Database;
using HamsterCoin.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using HamsterCoin.Domain;

namespace HamsterCoin.Services.Implementations
{
    public class CardService(ApplicationDbContext dbContext) : ICardService
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task CreateAsync(Card card)
        {
            _dbContext.Cards.Add(card);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCard(string numberCard)
        {
            var card = await _dbContext.Cards.FirstOrDefaultAsync(c => c.Number == numberCard);

            if (card != null)
            {
                _dbContext.Cards.Remove(card);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Card> GetCard(long userId)
        {
            var card = await _dbContext.UserCards
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .Select(c => new Card
                {
                    Number = c.Card.Number,
                    Date = c.Card.Date,
                    Cvv = c.Card.Cvv
                })
                .FirstOrDefaultAsync();
            return card;
        }

    }
}

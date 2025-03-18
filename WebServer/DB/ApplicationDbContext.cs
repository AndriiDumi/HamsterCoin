using Microsoft.EntityFrameworkCore;

namespace HamsterCoin.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public required DbSet<User> Users { get; set; }
        public required DbSet<UserDetails> UserDetails { get; set; }
        public required DbSet<Game> Games { get; set; }
        public required DbSet<DepHistory> DepHistory { get; set; }
        public required DbSet<DepWithdraw> DepWithdraw { get; set; }
        public required DbSet<Card> Cards { get; set; }

        //Maybe implement User_Cards
    }
}
using Microsoft.EntityFrameworkCore;
using HamsterCoin.Domain;
using HamsterCoin.DbConfiguration;

namespace HamsterCoin.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public required DbSet<User> Users { get; set; }
        public required DbSet<Game> Games { get; set; }
        public required DbSet<DepositHistory> DepHistory { get; set; }
        public required DbSet<WithdrawHistory> WithdrawHistory { get; set; }
        public required DbSet<Card> Cards { get; set; }
        public required DbSet<UserCard> UserCards { get; set; }
        public required DbSet<RefreshToken> RefreshTokens { get; set; }
        public required DbSet<Promocode> Promocodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new DepositHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new WithdrawHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new UserCardConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        //TODO
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseExceptionProcessor();
        }
    }
}

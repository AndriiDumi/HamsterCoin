using Microsoft.EntityFrameworkCore;
using HamsterCoin.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HamsterCoin.DbConfiguration;

namespace HamsterCoin.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public required DbSet<User> Users { get; set; }
        public required DbSet<UserDetails> UserDetails { get; set; }
        public required DbSet<Game> Games { get; set; }
        public required DbSet<DepositHistory> DepHistory { get; set; }
        public required DbSet<WithdrawHistory> WithdrawHistory { get; set; }
        public required DbSet<Card> Cards { get; set; }

        //Maybe implement User_Cards

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new DepositHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new WithdrawHistoryConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        //TODO
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseExceptionProcessor();
        }
    }
}

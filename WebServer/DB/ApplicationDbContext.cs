using Microsoft.EntityFrameworkCore;
using HamsterCoin.Domain;

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
            modelBuilder.Entity<UserDetails>()
                .HasOne(ud => ud.User)
                .WithMany() //оскільки в User немає властивості UserDetails, ми просто вказуємо .WithMany() без параметрів
                .HasForeignKey(ud => ud.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DepositHistory>()
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<WithdrawHistory>()
                .HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        //TODO
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseExceptionProcessor();
        }
    }
}
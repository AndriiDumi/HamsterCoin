using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HamsterCoin.Domain;

namespace HamsterCoin.DbConfiguration
{
    public class WithdrawHistoryConfiguration : IEntityTypeConfiguration<WithdrawHistory>
    {
        public void Configure(EntityTypeBuilder<WithdrawHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder
                .HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
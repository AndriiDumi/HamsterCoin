using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HamsterCoin.Domain;

namespace HamsterCoin.DbConfiguration
{
    public class DepositHistoryConfiguration : IEntityTypeConfiguration<DepositHistory>
    {
        public void Configure(EntityTypeBuilder<DepositHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HamsterCoin.Domain;

namespace HamsterCoin.DbConfiguration
{
        public class UserDetailsConfiguration : IEntityTypeConfiguration<UserDetails>
        {
            public void Configure(EntityTypeBuilder<UserDetails> builder)
            {
                builder.HasKey(x => x.Id);
                builder
                    .HasOne(ud => ud.User)
                    .WithMany() //оскільки в User немає властивості UserDetails, ми просто вказуємо .WithMany() без параметрів
                    .HasForeignKey(ud => ud.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
            }
        }
}

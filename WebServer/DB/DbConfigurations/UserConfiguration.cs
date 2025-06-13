using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HamsterCoin.Domain;

namespace HamsterCoin.DbConfiguration
{
        public class UserConfiguration : IEntityTypeConfiguration<User>
        {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(i => i.Email).IsUnique();   
        }
        }
}

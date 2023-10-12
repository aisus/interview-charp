using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using src.Models;

namespace src.Data.Configuration
{
    public class BalanceConfiguration : IEntityTypeConfiguration<Balance>
    {
        public void Configure(EntityTypeBuilder<Balance> builder)
        {
            builder.HasKey(b => b.Id);
            builder.HasOne(b => b.User)
            .WithOne(u => u.Balance)
            .HasForeignKey<Balance>(b => b.UserId);
        }
    }
}
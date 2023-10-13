using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.DAL.Enums;
using Sample.DAL.Models;
using Sample.Extensions.Utility.DAL;

namespace Sample.DAL.Configuration
{
    public class BalanceConfiguration : IEntityTypeConfiguration<Balance>
    {
        public void Configure(EntityTypeBuilder<Balance> builder)
        {
            builder.HasGuidColumn();
            builder.HasOne(x => x.User).WithOne(x => x.Balance).HasForeignKey<Balance>(x => x.UserId);
            builder.HasMany(x => x.Operations).WithOne(x => x.Balance).HasForeignKey(x => x.BalanceId);
        }
    }

    public class OperationeConfiguration : IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> builder)
        {
            builder.HasGuidColumn();
            builder.Property(x => x.Type).HasConversion(
                t => t.ToString(),
                t => (OperationType)Enum.Parse(typeof(OperationType), t));
            builder.HasOne(x => x.Balance).WithMany(x => x.Operations).HasForeignKey(x => x.BalanceId);
        }
    }
}
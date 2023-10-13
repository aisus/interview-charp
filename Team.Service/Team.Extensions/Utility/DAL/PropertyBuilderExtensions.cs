using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Team.Extensions.Utility.DAL
{
    public static class PropertyBuilderExtensions
    {
        public static PropertyBuilder<Guid> IsOracleGuid(this PropertyBuilder<Guid> prop)
        {
            return prop
                .HasMaxLength(16)
                .HasConversion(
                    new ValueConverter<Guid, byte[]>(
                        x => x.ToOracleByteArray(),
                        x => new Guid(x.ReverseGuidEndianess())));
        }

        public static PropertyBuilder<Guid?> IsOracleGuid(this PropertyBuilder<Guid?> prop)
        {
            return prop
                .HasMaxLength(16)
                .HasConversion(
                    new ValueConverter<Guid?, byte[]>(
                        x => x.HasValue ? x.Value.ToOracleByteArray() : null,
                        x => x == null ? (Guid?) null : new Guid(x.ReverseGuidEndianess())));
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Sample.Extensions.Infrastrcture;

namespace Sample.Extensions.Utility.DAL
{
    public static class EntityTypeBuilderExtensions
    {
        public static void HasGuidColumn<T>(this EntityTypeBuilder<T> builder) where T : class, IEntity
        {
            builder
                .Property(x => x.Id)
                .HasColumnName("GUID")
                .HasValueGenerator<GuidValueGenerator>();
        }
    }
}
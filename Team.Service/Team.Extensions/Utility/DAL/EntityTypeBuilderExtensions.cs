using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Team.Extensions.Infrastrcture;

namespace Team.Extensions.Utility.DAL
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
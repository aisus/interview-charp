using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Sample.DAL;

namespace Sample.Service.Configuration
{
    public static class ApplicationBuilderExtensions
    {
        public static void InitializeDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<SampleDbContext>();

            context.Database.EnsureCreated();

            // if (!context.Database.GetService<IRelationalDatabaseCreator>().Exists())
            // {
                context.Database.Migrate();
            // }

            //context.Database.EnsureDeleted();
        }
    }
}
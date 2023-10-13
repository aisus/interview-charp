using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using Sample.DAL.Models;
using System.Reflection;

namespace Sample.DAL
{
    public class SampleDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<Balance> Balances { get; set; }
        public DbSet<Operation> Operations { get; set; }

        public SampleDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
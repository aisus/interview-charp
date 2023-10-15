using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sample.DAL;

public static class TestDbContextInitializer
{
    public static SampleDbContext GetMemoryContext()
    {
        var options = new DbContextOptionsBuilder<SampleDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;

        IOptions<OperationalStoreOptions> opStoreOptions = Options.Create(new OperationalStoreOptions());

        return new SampleDbContext(options, opStoreOptions);
    }
}


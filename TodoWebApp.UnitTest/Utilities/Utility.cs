using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoWebApp.Data;

namespace TodoWebApp.UnitTest.Utilities;

public static class Utility
{
    public static DbContextOptions<AppDbContext> TestDbContextOptions()
    {
        // Create a new service provider to create a new in-memory database.
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        // Create a new options instance using an in-memory database and 
        // IServiceProvider that the context should resolve all of its services from.
        var builder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("InMemoryDb")
            .UseInternalServiceProvider(serviceProvider);

        return builder.Options;
    }
}

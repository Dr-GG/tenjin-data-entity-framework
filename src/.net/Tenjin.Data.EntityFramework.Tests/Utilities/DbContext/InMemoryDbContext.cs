using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Tenjin.Data.EntityFramework.Tests.Utilities.DbContext.Models;

namespace Tenjin.Data.EntityFramework.Tests.Utilities.DbContext;

public class InMemoryDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    private static DbContextOptions Options =>
        new DbContextOptionsBuilder<InMemoryDbContext>()
            .UseInMemoryDatabase("InMemoryDbContext")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

    public InMemoryDbContext(): base(Options)
    { }

    public DbSet<ComplexPersonModel> Persons { get; set; } = null!;
}
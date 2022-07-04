using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Tenjin.Data.EntityFramework.Extensions;
using Tenjin.Data.EntityFramework.Tests.Utilities.DbContext;
using Tenjin.Data.EntityFramework.Tests.Utilities.DbContext.Models;

namespace Tenjin.Data.EntityFramework.Tests.ExtensionsTests;

[TestFixture]
public class DbContextExtensionsTests
{
    [Test]
    public async Task AttachAsModified_WhenProvidingAnEntityWithoutPredicate_SetsTheStateAsModified()
    {
        await using var dbContext = new InMemoryDbContext();
        var newPerson = GetDefaultNewEntity();

        await dbContext.AttachAsModified(newPerson);

        Assert.AreEqual(EntityState.Modified, dbContext.Entry(newPerson).State);
    }

    [Test]
    public async Task AttachAsModified_WhenProvidingAnEntityWithPredicateThatFindsTheCorrectLocalInstance_RemovesTheLocalAndSetsTheNewStateAsModified()
    {
        await using var dbContext = new InMemoryDbContext();
        var newPerson = GetDefaultNewEntity();
        var existingPerson = AddDefaultNewEntity(dbContext);

        Assert.AreEqual(EntityState.Unchanged, dbContext.Entry(existingPerson).State);

        await dbContext.AttachAsModified(newPerson, c => c.LastName == newPerson.LastName && c.FirstName == newPerson.FirstName);

        Assert.AreEqual(EntityState.Modified, dbContext.Entry(newPerson).State);
        Assert.AreEqual(EntityState.Detached, dbContext.Entry(existingPerson).State);
    }

    [Test]
    public async Task AttachAsModified_WhenProvidingAnEntityWithPredicateThatDoesNotFindTheCorrectLocalInstance_RemovesTheLocalAndSetsTheNewStateAsModified()
    {
        await using var dbContext = new InMemoryDbContext();
        var newPerson = GetDefaultNewEntity();
        var existingPerson = AddDefaultNewEntity(dbContext);

        Assert.AreEqual(EntityState.Unchanged, dbContext.Entry(existingPerson).State);

        await dbContext.AttachAsModified(newPerson, c => c.LastName == newPerson.FirstName && c.FirstName == newPerson.LastName);

        Assert.AreEqual(EntityState.Modified, dbContext.Entry(newPerson).State);
        Assert.AreEqual(EntityState.Unchanged, dbContext.Entry(existingPerson).State);
    }

    private static ComplexPersonModel GetDefaultNewEntity()
    {
        return new ComplexPersonModel
        {
            FirstName = "Developer",
            LastName = "X"
        };

    }

    private static ComplexPersonModel AddDefaultNewEntity(InMemoryDbContext dbContext)
    {
        var newPerson = GetDefaultNewEntity();

        dbContext.Persons.Add(newPerson);
        dbContext.SaveChanges();

        return newPerson;
    }
}
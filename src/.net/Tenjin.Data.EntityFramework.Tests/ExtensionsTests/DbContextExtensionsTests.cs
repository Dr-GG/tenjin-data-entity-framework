using System.Threading.Tasks;
using FluentAssertions;
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

        dbContext.Entry(newPerson).State.Should().Be(EntityState.Modified);
    }

    [Test]
    public async Task AttachAsModified_WhenProvidingAnEntityWithPredicateThatFindsTheCorrectLocalInstance_RemovesTheLocalAndSetsTheNewStateAsModified()
    {
        await using var dbContext = new InMemoryDbContext();
        var newPerson = GetDefaultNewEntity();
        var existingPerson = AddDefaultNewEntity(dbContext);

        dbContext.Entry(existingPerson).State.Should().Be(EntityState.Unchanged);

        await dbContext.AttachAsModified(newPerson, c => c.LastName == newPerson.LastName && c.FirstName == newPerson.FirstName);

        dbContext.Entry(newPerson).State.Should().Be(EntityState.Modified);
        dbContext.Entry(existingPerson).State.Should().Be(EntityState.Detached);
    }

    [Test]
    public async Task AttachAsModified_WhenProvidingAnEntityWithPredicateThatDoesNotFindTheCorrectLocalInstance_RemovesTheLocalAndSetsTheNewStateAsModified()
    {
        await using var dbContext = new InMemoryDbContext();
        var newPerson = GetDefaultNewEntity();
        var existingPerson = AddDefaultNewEntity(dbContext);

        dbContext.Entry(existingPerson).State.Should().Be(EntityState.Unchanged);

        await dbContext.AttachAsModified(newPerson, c => c.LastName == newPerson.FirstName && c.FirstName == newPerson.LastName);

        dbContext.Entry(newPerson).State.Should().Be(EntityState.Modified);
        dbContext.Entry(existingPerson).State.Should().Be(EntityState.Unchanged);
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
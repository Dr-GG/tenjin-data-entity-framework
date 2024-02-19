using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Tenjin.Data.EntityFramework.Extensions;

namespace Tenjin.Data.EntityFramework.Tests.ExtensionsTests;

[TestFixture]
public class DbSetExtensionsTests
{
    [Test]
    public async Task RemoveAsync_WhenInvoked_InvokesTheRemove()
    {
        var mockDbSet = new Mock<DbSet<object>>();
        var dbSet = mockDbSet.Object;
        var toRemoveObject = new object();

        await dbSet.RemoveAsync(toRemoveObject);

        mockDbSet.Verify(m => m.Remove(toRemoveObject), Times.Once);
        mockDbSet.VerifyNoOtherCalls();
    }

    [Test]
    public async Task RemoveRangeAsync_WhenInvokedWithANullCollection_InvokesTheRemoveRange()
    {
        var mockDbSet = new Mock<DbSet<object>>();
        var dbSet = mockDbSet.Object;

        await dbSet.RemoveRangeAsync(null);

        mockDbSet.VerifyNoOtherCalls();
    }

    [Test]
    public async Task RemoveRangeAsync_WhenInvokedWithANonNullCollection_InvokesTheRemoveRange()
    {
        var mockDbSet = new Mock<DbSet<object>>();
        var dbSet = mockDbSet.Object;
        IEnumerable<object> toRemoveObjects = new[]
        {
            new object(),
            new object(),
            new object()
        };

        await dbSet.RemoveRangeAsync(toRemoveObjects);

        mockDbSet.Verify(m => m.RemoveRange(toRemoveObjects), Times.Once);
        mockDbSet.VerifyNoOtherCalls();
    }
}
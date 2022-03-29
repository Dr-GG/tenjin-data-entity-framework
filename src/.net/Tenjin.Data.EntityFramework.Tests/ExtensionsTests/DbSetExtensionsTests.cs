using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Tenjin.Data.EntityFramework.Extensions;

namespace Tenjin.Data.EntityFramework.Tests.ExtensionsTests
{
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
    }
}

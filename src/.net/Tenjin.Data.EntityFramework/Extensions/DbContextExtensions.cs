using Microsoft.EntityFrameworkCore;

namespace Tenjin.Data.EntityFramework.Extensions;

public static class DbContextExtensions
{
    public static Task AttachAsModified<TEntity>(this DbContext dbContext,
        TEntity entity,
        Func<TEntity, bool>? removeExistingPredicate = null) where TEntity : class
    {
        if (removeExistingPredicate != null)
        {
            var local = dbContext.Set<TEntity>()
                .Local
                .SingleOrDefault(removeExistingPredicate);

            if (local != null)
            {
                dbContext.Entry(local).State = EntityState.Detached;
            }
        }

        dbContext.Entry(entity).State = EntityState.Modified;

        return Task.CompletedTask;
    }
}
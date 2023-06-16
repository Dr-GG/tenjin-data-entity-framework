using Microsoft.EntityFrameworkCore;

namespace Tenjin.Data.EntityFramework.Extensions;

/// <summary>
/// A collection of extension methods for the DbContext instance.
/// </summary>
public static class DbContextExtensions
{
    /// <summary>
    /// Attaches an entity in a DbContext as modified.
    /// </summary>
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
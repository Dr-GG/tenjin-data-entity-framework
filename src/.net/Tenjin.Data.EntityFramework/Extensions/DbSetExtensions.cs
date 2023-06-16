using Microsoft.EntityFrameworkCore;

namespace Tenjin.Data.EntityFramework.Extensions;

/// <summary>
/// A collection of extension methods for the DbSet instance.
/// </summary>
public static class DbSetExtensions
{
    /// <summary>
    /// Removes a single entity from a DbSet instance.
    /// </summary>
    public static Task RemoveAsync<TEntity>(this DbSet<TEntity> dbSet, TEntity entity) where TEntity : class
    {
        dbSet.Remove(entity);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Removes a range of entities from a DbSet instance.
    /// </summary>
    public static Task RemoveRangeAsync<TEntity>(this DbSet<TEntity> dbSet, IEnumerable<TEntity>? entities) where TEntity : class
    {
        if (entities == null)
        {
            return Task.CompletedTask;
        }

        dbSet.RemoveRange(entities);

        return Task.CompletedTask;
    }
}
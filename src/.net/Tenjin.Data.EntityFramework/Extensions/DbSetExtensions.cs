using Microsoft.EntityFrameworkCore;

namespace Tenjin.Data.EntityFramework.Extensions
{
    public static class DbSetExtensions
    {
        public static Task RemoveAsync<TEntity>(this DbSet<TEntity> dbSet, TEntity entity) where TEntity : class
        {
            dbSet.Remove(entity);

            return Task.CompletedTask;
        }
    }
}

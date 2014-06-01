using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Blog.Domain.Infrastructure.Persistence
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        TEntity Find(params object[] id);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> All();
        TEntity Add(TEntity entity);
        TEntity Update(TEntity updatedEntity, object key);
        void UpdatePartial(TEntity updatedEntity, params Expression<Func<TEntity, object>>[] properties);
        void Delete(TEntity entity);
    }
}
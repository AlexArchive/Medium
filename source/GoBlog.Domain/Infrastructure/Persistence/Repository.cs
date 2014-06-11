using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GoBlog.Domain.Infrastructure.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class 
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public Repository()
            : this (new BlogDatabase())
        {
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public TEntity Find(params object[] id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).ToList();
        }

        public IQueryable<TEntity> All()
        {
            return _context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public TEntity Attach(TEntity entity)
        {
            var dbSet = _context.Set<TEntity>();
            var entityKeys = _context.GetPrimaryKeyValues(entity).ToArray();
            var storeEntity = dbSet.Find(entityKeys);

            if (storeEntity != null)
            {
                _context.Entry(storeEntity).State = EntityState.Detached;
                return _context.Set<TEntity>().Attach(entity);
            }
            return entity;
        }

        public TEntity Update(TEntity updatedEntity, object key)
        {
            var existing = Find(key);

            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(updatedEntity);
                _context.SaveChanges();
            }
            return existing;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
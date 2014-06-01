using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Blog.Domain.Infrastructure.Persistence
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

        public void UpdatePartial(TEntity updatedEntity, params Expression<Func<TEntity, object>>[] properties)
        {
            _context.Configuration.ValidateOnSaveEnabled = false;

            _context.Set<TEntity>().Attach(updatedEntity);
            var entry = _context.Entry(updatedEntity);

            foreach (var selector in properties)
                entry.Property(selector).IsModified = true;

            _context.SaveChanges();

            _context.Configuration.ValidateOnSaveEnabled = true;
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
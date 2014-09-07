using GoBlog.Domain;
using GoBlog.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace GoBlog.UnitTest.Support
{
    public class DatabaseContextDouble : IDatabaseContext
    {
        public DbSet<Post> Posts { get; private set; }

        public DatabaseContextDouble()
        {
            Posts = new PostDbSet();
        }

        public int SaveChangesCount { get; private set; }
        public int SaveChanges()
        {
            SaveChangesCount++;
            return 1; 
        }
    }

    public class PostDbSet : FakeDbSet<Post>
    {
        public override Post Find(params object[] keyValues)
        {
            return this.SingleOrDefault(post => post.Slug == (string)keyValues.Single());
        }
    }

    #region EF Infrastructure



    // Attribution: http://msdn.microsoft.com/en-gb/data/dn314431
    
    public class FakeDbSet<TEntity> : 
        DbSet<TEntity>, 
        IQueryable,
        IEnumerable<TEntity>, 
        IDbAsyncEnumerable<TEntity>
        where TEntity : class
    {
        private readonly ObservableCollection<TEntity> collection;
        private readonly IQueryable queryable;

        public FakeDbSet()
        {
            collection = new ObservableCollection<TEntity>();
            queryable = collection.AsQueryable();
        }

        public override TEntity Add(TEntity item)
        {
            collection.Add(item);
            return item;
        }

        public override IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                collection.Add(entity);
            return collection.AsEnumerable();
        }

        public override TEntity Remove(TEntity item)
        {
            collection.Remove(item);
            return item;
        }

        public override TEntity Attach(TEntity item)
        {
            collection.Add(item);
            return item;
        }

        public override TEntity Create()
        {
            return Activator.CreateInstance<TEntity>();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public override ObservableCollection<TEntity> Local
        {
            get { return collection; }
        }

        Type IQueryable.ElementType
        {
            get { return queryable.ElementType; }
        }

        Expression IQueryable.Expression
        {
            get { return queryable.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new TestDbAsyncQueryProvider<TEntity>(queryable.Provider); }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        IDbAsyncEnumerator<TEntity> IDbAsyncEnumerable<TEntity>.GetAsyncEnumerator()
        {
            return new TestDbAsyncEnumerator<TEntity>(collection.GetEnumerator());
        }
    }

    internal class TestDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal TestDbAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestDbAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestDbAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute(expression));
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }
    }

    internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
    {
        public TestDbAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public TestDbAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return GetAsyncEnumerator();
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new TestDbAsyncQueryProvider<T>(this); }
        }
    }

    internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestDbAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_inner.MoveNext());
        }

        public T Current
        {
            get { return _inner.Current; }
        }

        object IDbAsyncEnumerator.Current
        {
            get { return Current; }
        }
    } 

    #endregion
}
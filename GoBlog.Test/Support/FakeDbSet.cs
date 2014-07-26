using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GoBlog.Test.Support
{
    public class FakeDbSet<T> : IDbSet<T> where T : class
    {
        private Func<T, object, bool> keySelector;
        private readonly HashSet<T> data;
        private readonly IQueryable query;

        public FakeDbSet(Func<T, object, bool> keySelector)
        {
            this.keySelector = keySelector;
            data = new HashSet<T>();
            query = data.AsQueryable();
        }

        public T Add(T entity)
        {
            data.Add(entity);
            return entity;
        }

        public T Attach(T entity)
        {
            data.Add(entity);
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public virtual T Find(params object[] keyValues)
        {
            return data.SingleOrDefault(item => keySelector(item, keyValues.First()));
        }

        public System.Collections.ObjectModel.ObservableCollection<T> Local
        {
            get { return new System.Collections.ObjectModel.ObservableCollection<T>(data); }
        }

        public T Remove(T entity)
        {
            data.Remove(entity);
            return entity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        public Type ElementType
        {
            get { return query.ElementType; }
        }

        public Expression Expression
        {
            get { return query.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return query.Provider; }
        }
    }
}
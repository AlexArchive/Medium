using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;

namespace GoBlog.Domain.Infrastructure.Persistence
{
    public static class DbContextExtensions
    {
        public static IList<string> GetPrimaryKeyNames<TEntity>(this DbContext context) where TEntity : class
        {
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;
            ObjectSet<TEntity> set = objectContext.CreateObjectSet<TEntity>();

            return set.EntitySet.ElementType
                .KeyMembers
                .Select(k => k.Name)
                .ToList();
        }

        public static IList<object> GetPrimaryKeyValues<TEntity>(this DbContext context, TEntity entity) where TEntity : class
        {
            var valueList = new List<object>();
            IList<string> primaryKeyNames = context.GetPrimaryKeyNames<TEntity>();

            foreach (PropertyInfo propertyInfo in entity.GetType().GetProperties())
            {
                if (primaryKeyNames.Contains(propertyInfo.Name))
                {
                    valueList.Add(propertyInfo.GetValue(entity));
                }
            }

            return valueList;
        }
    }
}
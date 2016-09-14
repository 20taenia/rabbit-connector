using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Data.DataAccess
{

    public static class ExtensionMethods
    {
        public static IEnumerable<T> Except<T, TKey>(this IEnumerable<T> items, IEnumerable<T> other,
                                                                                Func<T, TKey> getKey)
        {
            return from item in items
                    join otherItem in other on getKey(item)
                    equals getKey(otherItem) into tempItems
                    from temp in tempItems.DefaultIfEmpty()
                    where ReferenceEquals(null, temp) || temp.Equals(default(T))
                    select item;

        }        

        /// <summary>
        /// Extension method to make the Object Context available
        /// ref: http://blog.magnusmontin.net/2013/05/30/generic-dal-using-entity-framework/
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static ObjectContext ToObjectContext(this DbContext dbContext)
        {
            return (dbContext as IObjectContextAdapter).ObjectContext;
        }

        /// <summary>
        /// Used in preference to the FirstOrDefault() to ensure an instantiated object is returned
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static T FirstOrNew<T>(this IEnumerable<T> enumerable) where T : new()
        {
            var val = enumerable.FirstOrDefault<T>();
            if (val == null)
            {
                val = new T();
            }
            return val;
        }
    }    
}

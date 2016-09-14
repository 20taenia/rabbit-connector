using Charon.Core.Entities;
using Charon.Core.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Data.DataAccess
{
    public interface IDataAccessor<T> where T : EntityBase
    {
        /// <summary>
        /// Returns a Queryable interface
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Select();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includeSoftDeleted"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        IList<T> GetEntities(Expression<Func<T, bool>> filter, bool includeSoftDeleted, params Expression<Func<T, object>>[] navigationProperties);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        IList<T> GetEntities(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] navigationProperties);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includeSoftDeleted"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        T GetEntity(Expression<Func<T, bool>> filter, bool includeSoftDeleted, params Expression<Func<T, object>>[] navigationProperties);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        T GetEntity(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] navigationProperties);

        /// <summary>
        /// Provides a function for inserting or updating entities
        /// </summary>
        /// <param name="softDelete"></param>
        /// <param name="entities"></param>
        void InsertOrUpdate(bool softDelete, List<T> entities);

        /// <summary>
        /// Provides a function for inserting or updating entities
        /// </summary>
        /// <param name="entities"></param>
        void InsertOrUpdate(List<T> entities);

        /// <summary>
        /// Provides a function for inserting or updating entities
        /// </summary>
        /// <param name="softDelete"></param>
        /// <param name="entities"></param>
        void InsertOrUpdate<T2>(bool softDelete, List<T2> entities) where T2 : EntityBase;

        /// <summary>
        /// Provides a function for updating related entity data
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <param name="parent"></param>
        /// <param name="collection"></param>
        /// <param name="state"></param>
        /// <param name="children"></param>
        void ModifyRelatedEntities<T2>(T parent, Expression<Func<T, object>> collection, EntityState state, List<T2> children) where T2 : EntityBase;

        /// <summary>
        /// Provides a function for updating related entity data
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <param name="parent"></param>
        /// <param name="collectionName"></param>
        /// <param name="state"></param>
        /// <param name="children"></param>
        void ModifyRelatedEntities<T2>(T parent, string collectionName, EntityState state, List<T2> children) where T2 : EntityBase;

        /// <summary>
        /// Removes an entity or entities
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="softDelete"></param>
        void Delete(bool softDelete, List<T> entities);

        /// <summary>
        /// Removes an entity or entities
        /// </summary>
        /// <param name="entities"></param>
        void Delete(List<T> entities);

        /// <summary>
        /// Removes an entity or entities
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="softDelete"></param>
        void Delete<T2>(bool softDelete, List<T2> entities) where T2 : EntityBase;

        /// <summary>
        /// Removes an entity or entities
        /// </summary>
        /// <param name="entities"></param>
        void Delete<T2>(List<T2> entities) where T2 : EntityBase;

        /// <summary>
        /// Removes an entity or entities
        /// </summary>
        /// <param name="softDelete"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        void Delete(bool softDelete, Func<T, bool> filter);

        /// <summary>
        /// Commits pending data changes
        /// </summary>
        void SaveChanges();
    }
}

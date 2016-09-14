
#region Using Directives
using Charon.Core.Entities;
using Charon.Core.Messaging;
using Charon.Data.DataAccess.HelperClasses;
using Charon.Data.EntityFramework;
using FastMember;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace Charon.Data.DataAccess
{
    /// <summary>
    /// This is the base Data Access class
    /// </summary>
    public class DataAccessor<T> : BaseAccessor, IDataAccessor<T>
        where T : EntityBase
    {
        #region Constructors

        /// <summary>
        /// Default constructor - passes to the base class for DbContext instantiation
        /// </summary>
        public DataAccessor(string user) : base( user)
        {       
        }

        /// <summary>
        /// Sort of like a copy constructor where we reuse an existing DbContext with a new Data Accessor class
        /// </summary>
        /// <param name="existing">A sort of copy constructor where an existing accessor's DbContext is used by this class instance</param>
        public DataAccessor(BaseAccessor existing, string user) : base (user)
        {
            DataContext = existing.DataContext;
            Transaction = existing.Transaction;
        }

        /// <summary>
        /// Sort of like a copy constructor where we reuse an existing DbContext with a new Data Accessor class
        /// </summary>
        /// <param name="access">An existing DbContext</param>
        internal DataAccessor(CharonContext access, string user) : base (user)
        {
            DataContext = access;
        }

        #endregion      

        #region Public Functions

        /// <summary>
        /// A Function which returns multiple matching entities of type T
        /// </summary>
        /// <param name="filter">The query filter (criteria)</param>
        /// <param name="navigationProperties">Navigation properties to include from the base entity</param>
        /// <returns>A collection of matching entities</returns>
        public virtual IList<T> GetEntities(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] navigationProperties)
        {
            return GetEntities(filter, false, navigationProperties);
        }

        /// <summary>
        /// A Function which returns multiple matching entitiesof type T
        /// </summary>
        /// <param name="filter">The query filter (criteria)</param>
        ///<param name="includeSoftDeleted">Include soft deleted entities</param>
        /// <param name="navigationProperties">Navigation properties to include from the base entity</param>
        /// <returns>A collection of matching entities</returns>
        public virtual IList<T> GetEntities(Expression<Func<T, bool>> filter, bool includeSoftDeleted, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = DataContext.Set<T>();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
            {
                query = query.Include<T, object>(navigationProperty);
                var expVal = navigationProperty.ToString();
            }

            Expression<Func<T, bool>> expression = filter;

            //If our filter query includes IsDeleted column we must override the parameter includeSoftDeleted to true
            var expressionString = expression.ToString().ToLower();
            if (expressionString.Contains("isdeleted"))
                includeSoftDeleted = true;

            //Ignore soft deleted items if necessary
            if (!includeSoftDeleted)
                expression = expression.AndAlso<T>(a => !a.IsDeleted);

            return query.Where(expression).ToList<T>();
        }

        /// <summary>
        /// A Function which returns multiple matching entities of type T
        /// </summary>
        /// <param name="filter">The query filter (criteria)</param>
        /// <param name="navigationProperties">Navigation properties to include from the base entity</param>
        /// <returns>A collection of matching entities</returns>
        public virtual CollectionPage<T> GetPagedEntities(int page, int itemsPerPage, Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] navigationProperties)
        {
            return GetPagedEntities(page, itemsPerPage, filter, false, navigationProperties);
        }

        /// <summary>
        /// A Function which returns multiple matching entitiesof type T
        /// </summary>
        /// <param name="filter">The query filter (criteria)</param>
        ///<param name="includeSoftDeleted">Include soft deleted entities</param>
        /// <param name="navigationProperties">Navigation properties to include from the base entity</param>
        /// <returns>A collection of matching entities</returns>
        public virtual CollectionPage<T> GetPagedEntities(int page, int itemsPerPage, Expression<Func<T, bool>> filter, bool includeSoftDeleted, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = DataContext.Set<T>();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
            {
                query = query.Include<T, object>(navigationProperty);
            }

            Expression<Func<T, bool>> expression = filter;

            //If our filter query includes IsDeleted column we must override the parameter includeSoftDeleted to true
            var expressionString = expression.ToString().ToLower();
            if (expressionString.Contains("isdeleted"))
                includeSoftDeleted = true;

            if (!includeSoftDeleted)
                expression = expression.AndAlso<T>(a => !a.IsDeleted);

            CollectionPage<T> collection = new CollectionPage<T>();
            collection.TotalItems = query.Count(expression);
            collection.ItemsPerPage = itemsPerPage;
            collection.Items = query.Where(expression)
                .OrderBy(t => t.ModifiedDate)
                .Skip(itemsPerPage * (page - 1)).Take(itemsPerPage)
                .ToList();

            return collection;
        }


        /// <summary>
        /// A Function to return a single entity of type T
        /// </summary>
        /// <param name="filter">The query filter (criteria)</param>
        /// <param name="navigationProperties">Navigation properties to include from the base entity</param>
        /// <returns>A matching entity or NULL</returns>
        public virtual T GetEntity(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] navigationProperties)
        {   
            return GetEntity(filter, false, navigationProperties);            
        }


        /// <summary>
        /// A Function to return a single entity of type T
        /// </summary>
        /// <param name="filter">The query filter (criteria)</param>
        ///<param name="includeSoftDeleted">Include soft deleted entities</param>
        /// <param name="navigationProperties">Navigation properties to include from the base entity</param>
        /// <returns>A matching entity or NULL</returns>
        public virtual T GetEntity(Expression<Func<T, bool>> filter, bool includeSoftDeleted, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = DataContext.Set<T>();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
            {
                query = query.Include<T, object>(navigationProperty);
            }

            Expression<Func<T, bool>> expression = filter;

            if (!includeSoftDeleted)
                expression = expression.AndAlso<T>(a => !a.IsDeleted);

            return query.FirstOrDefault(filter);
        }


        /// <summary>
        /// Forces pending changes to be committed to the Data Store
        /// </summary>
        public void SaveChanges()
        {
            try
            {
                DataContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                HandleException(ex, true);
            }
            catch (DbEntityValidationException ex)
            {
                HandleException(ex, true);
            }
            catch (Exception ex)
            {
                HandleException(ex, true);
            }
        }

        private void HandleException(Exception ex, bool rethrow)
        {
            Trace.WriteLine(ex.Message);

            if (ex.InnerException != null)
            {
                Trace.WriteLine(ex.InnerException.Message);
            }

            if (rethrow)
                throw ex;
        }

        /// <summary>
        /// Allows freeform data queries from outside the Data Access classes, without exposing the data context directly
        /// Will include soft deleted entities.
        /// </summary>        
        /// <returns>An IQueryable of type T</returns>
        public IQueryable<T> CreateQuery()
        {
            IQueryable<T> _query = DataContext.Set<T>().AsQueryable<T>();
            return _query;
        }

        /// <summary>
        /// Allows freeform data queries from outside the Data Access classes, without exposing the data context directly
        /// A hard limit of 10000 rows is applied, and will include soft deleted entities
        /// </summary>  
        /// <param name="maxRows">Max rows to return</param>
        /// <returns>An IQueryable of type T</returns>
        public IQueryable<T> CreateQuery(int maxRows)
        {
            IQueryable<T> _query = DataContext.Set<T>().AsQueryable<T>();

            if (maxRows < 0 || maxRows > 10000)
            {
                maxRows = 10000; 
            }

            _query = _query.Take(maxRows);
            return _query;
        }      

        #endregion

        #region Protected Functions

        /// <summary>
        /// Returns a base query with ** no filters or restrictions ** applied
        /// </summary>
        /// <returns>An IQueryable with no limitations</returns>
        public virtual IQueryable<T> Select()
        {
            return DataContext.Set<T>().AsQueryable<T>();
        }

        /// <summary>
        /// Determines if the specified entity exists in the *local* Db Context cache
        /// Note: does not check the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected bool ExistsLocal(T entity)
        {
            return DataContext.Set<T>().Local.Any(e => e == entity);
        }

        /// <summary>
        /// Determines if the specified entity exists in the *local* Db Context cache
        /// Note: does not check the database
        /// </summary>
        /// <typeparam name="T2">The type of the entity</typeparam>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        protected bool ExistsLocal<T2>(T2 entity) where T2 : EntityBase
        {
            return DataContext.Set<T2>().Local.Any(e => e == entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <param name="entity"></param>
        protected void DetachLocal<T2>(T2 entity) where T2 : EntityBase
        {
            var existing = DataContext.Set<T2>().Local.Any(e => e == entity);

            if (existing)
            {
                DataContext.Set<T2>().Local.Remove(entity);
            }

            DataContext.Set<T2>().Attach(entity);            
        }

        /// <summary>
        /// Determines if an entity exists in the Data Context or in the physical data store
        /// Source (partial): http://stackoverflow.com/questions/6018711/generic-way-to-check-if-entity-exists-in-entity-framework
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns>Returns null if not found, else returns the located entity which may not be the same as the entity passed in</returns>
        protected T Exists(T entity)
        {
            var objContext = ((IObjectContextAdapter)this.DataContext).ObjectContext;
            var objSet = objContext.CreateObjectSet<T>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            DbSet<T> set = DataContext.Set<T>();
            var keys = (from x in entityKey.EntityKeyValues
                        select x.Value).ToArray();

            //Remember, there can by surrogate keys, so don't assume there's just one column/one value
            //If a surrogate key isn't ordered properly, the Set<T>().Find() method will fail, use attributes on the entity to determien the proper order.

            //context.Configuration.AutoDetectChangesEnabled = false;
            //http://stackoverflow.com/questions/11686225/dbset-find-method-ridiculously-slow-compared-to-singleordefault-on-id

            return set.Find(keys);
        }

        /// <summary>
        /// Determines if an entity exists in the Data Context or in the physical data store
        /// Source (partial): http://stackoverflow.com/questions/6018711/generic-way-to-check-if-entity-exists-in-entity-framework
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns>Returns null if not found, else returns the located entity which may not be the same as the entity passed in</returns>
        protected T2 Exists<T2>(T2 entity) where T2 :  EntityBase
        {
            var objContext = ((IObjectContextAdapter)this.DataContext).ObjectContext;
            var objSet = objContext.CreateObjectSet<T2>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            DbSet<T2> set = DataContext.Set<T2>();
            var keys = (from x in entityKey.EntityKeyValues
                        select x.Value).ToArray();

            //Remember, there can by surrogate keys, so don't assume there's just one column/one value
            //If a surrogate key isn't ordered properly, the Set<T>().Find() method will fail, use attributes on the entity to determien the proper order.

            //context.Configuration.AutoDetectChangesEnabled = false;
            //http://stackoverflow.com/questions/11686225/dbset-find-method-ridiculously-slow-compared-to-singleordefault-on-id

            return set.Find(keys);
        }

        /// <summary>
        /// Inserts or updates bulk entities. Entities marked for deletion will be soft deleted.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">Entities for updating</param>
        public virtual void InsertOrUpdate(List<T> entities)
        {
            ApplyStateChanges(true, entities);
        }

        /// <summary>
        /// Inserts or updates bulk entities
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="softDelete">Soft delete entities</param>
        /// <param name="entities">Entities for updating</param>
        public virtual void InsertOrUpdate(bool softDelete, List<T> entities)
        {
            ApplyStateChanges(softDelete, entities);
        }

        /// <summary>
        /// Inserts or Updates based on a defined type. Entities marked for deletion will be soft deleted.
        /// </summary>
        /// <typeparam name="T2">The type of entities</typeparam>
        /// <param name="entities">The entity or entities to process</param>
        public virtual void InsertOrUpdate<T2>(List<T2> entities) where T2 : EntityBase
        {
            ApplyStateChanges<T2>(true, entities);
        }

        /// <summary>
        /// Inserts or Updates based on a defined type
        /// </summary>
        /// <typeparam name="T2">The type of entities</typeparam>
        /// <param name="softDelete">Soft delete entities</param>
        /// <param name="entities">The entity or entities to process</param>
        public virtual void InsertOrUpdate<T2>(bool softDelete, List<T2> entities) where T2 : EntityBase
        {
            ApplyStateChanges<T2>(softDelete, entities);
        }


        /// <summary>
        ///This method loads navigation properties where we find a foreign key set but with a null value in the actual navigation property. 
        ///This happens after an update as we are only setting the foreign key id, and returning the same entity.
        /// </summary>
        /// <typeparam name="T2">The type of entities</typeparam>
        /// <param name="items">The entity or entities to process</param>
        /// <returns></returns>
        public List<T2> RefreshEntities<T2>(List<T2> items) where T2 : EntityBase
        {
            //Loop through entities fields and load navigation properties if we have an ID set with no related object field
            TypeAccessor accessor = TypeAccessor.Create(typeof(T2));
            var memberSet = accessor.GetMembers();

            ObjectContext objectContext = ((IObjectContextAdapter)DataContext).ObjectContext;
            ObjectSet<T2> set = objectContext.CreateObjectSet<T2>();
            var foreignKeys = set.EntitySet.ElementType.NavigationProperties.SelectMany(n => n.GetDependentProperties());
            var navProperties = set.EntitySet.ElementType.NavigationProperties.Select(np => np.Name);

            foreach (var item in items)
            {
                foreach (var foreignKey in foreignKeys)
                {
                    var foreignKeyField = foreignKey.Name;
                    var navProp = set.EntitySet.ElementType.NavigationProperties.SingleOrDefault(np => np.GetDependentProperties().Contains(foreignKey));

                    if (navProp == null)
                        continue;

                    var navPropField = navProp.Name;
                    var foreignKeyVal = accessor[item, foreignKeyField];
                    var isForeignKeyNull = IsNullEquivalent(memberSet.SingleOrDefault(member => member.Name == foreignKeyField), foreignKeyVal);
                    var navPropVal = accessor[item, navPropField];

                    if (!isForeignKeyNull && navPropVal == null)
                    {
                        var entityBaseItem = (item as EntityBase);

                        //If the item was unchanged we'll need to attach it first 
                        if (entityBaseItem.State == ObjectState.Unchanged)
                            set.Attach(item);

                        DataContext.Entry(item).Reference(navPropField).Load();
                    }
                }
            }

            return items;
        }

        private bool IsNullEquivalent(Member member, object value)
        {
            if (value == null)
                return true;
            else if (member.Type == typeof(string))
                return string.IsNullOrEmpty(value as string);
            else if (member.Type == typeof(int))
                return (int)value == 0;
            else if (IsNullableType(member.Type))
            {
                var convertType = Nullable.GetUnderlyingType(member.Type) == null ? member.Type : Nullable.GetUnderlyingType(member.Type);
                value = Convert.ChangeType(value, convertType);
                return ((int)value == 0);
            }
            else
                return false;
        }

        private bool IsNullableType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return true;

            return !type.IsValueType;
        }

        /// <summary>
        /// Allows for the modification of a many-to-many relationship
        /// </summary>
        /// <typeparam name="T2">The type of children entities</typeparam>
        /// <param name="parent">The parent entity</param>
        /// <param name="collectionName">The navigation property name for the relationship to edit</param>
        /// <param name="state">The entity state to change to</param>
        /// <param name="children">A collection of children of type T2</param>
        public void ModifyRelatedEntities<T2>(T parent, string collectionName, EntityState state, List<T2> children)
            where T2 : EntityBase
        {
            DataContext.Set<T>().Attach(parent);
            ObjectContext obj = DataContext.ToObjectContext();
            foreach (T2 child in children)
            {
                DataContext.Set<T2>().Attach(child);
                obj.ObjectStateManager.ChangeRelationshipState(parent, child, collectionName, state);
            }
        }

        /// <summary>
        /// Allows for the modification of a many-to-many relationship
        /// </summary>
        /// <typeparam name="T2">The type of children entities</typeparam>
        /// <param name="parent">The parent entity</param>
        /// <param name="collection">The navigation collection of entity types</param>
        /// <param name="state">The entity state to change to</param>
        /// <param name="children">A collection of children of type T2</param>
        public void ModifyRelatedEntities<T2>(T parent, Expression<Func<T, object>> collection, EntityState state, List<T2> children)       
            where T2 : EntityBase
        {
            try
            {
                DataContext.Set<T>().Attach(parent);
                ObjectContext obj = DataContext.ToObjectContext();
                foreach (T2 child in children)
                {
                    DataContext.Set<T2>().Attach(child);
                    obj.ObjectStateManager.ChangeRelationshipState(parent, child, collection, state);
                }
            }
            catch (NotSupportedException ex)
            {
                Debug.WriteLine(String.Format("An attempt was made to try and modify a relationship which is not supported.  Error: {0}", ex.Message));
            }
        }

        /// <summary>
        /// Sets an entity or entities to be soft deleted on next save changes call
        /// </summary>        
        /// <param name="entities">Entities to delete</param>        
        public virtual void Delete(List<T> entities)
        {
            Delete(true, entities);
        }

        /// <summary>
        /// Sets an entity or entities to be removed on the next Save Changes call
        /// </summary>
        /// <param name="softDelete">Soft delete entities</param>
        /// <param name="entities">Entities to delete</param>        
        public virtual void Delete(bool softDelete, List<T> entities)
        {
            foreach (T entity in entities.Where(x => x.State != ObjectState.Deleted))
            {
                if (softDelete)
                {
                    entity.IsDeleted = true;
                    entity.State = ObjectState.Modified;
                }
                else
                    entity.State = ObjectState.Deleted;
            }

            ApplyStateChanges(softDelete, entities);
        }

        /// <summary>
        /// Soft deletes the entities supplied
        /// </summary>
        /// <typeparam name="T2">The type of the entities</typeparam>
        /// <param name="entities">Entities to delete</param>
        public void Delete<T2>(List<T2> entities) where T2 : EntityBase
        {
            Delete<T2>(true, entities);
        }

        /// <summary>
        /// Deletes the entities supplied
        /// </summary>
        /// <typeparam name="T2">The type of the entities</typeparam>
        /// <param name="softDelete"></param>
        /// <param name="entities">Entities to delete</param>
        public void Delete<T2>(bool softDelete, List<T2> entities) where T2 : EntityBase
        {
            foreach(T2 entity in entities.Where(x => x.State != ObjectState.Deleted))
            {
                if (softDelete)
                {
                    entity.IsDeleted = true;
                    entity.State = ObjectState.Modified;
                }
                else
                    entity.State = ObjectState.Deleted;

            }

            ApplyStateChanges<T2>(softDelete, entities);
        }


        /// <summary>
        /// Soft delete entities by filter
        /// </summary>
        /// <param name="filter">Entities to delete</param>
        public void Delete(Func<T, bool> filter)
        {
            Delete(true, filter);
        }


        /// <summary>
        /// Delete entities by filter
        /// </summary>
        /// <param name="softDelete">Soft delete entities</param>
        /// <param name="filter">Entities to delete</param>
        public void Delete(bool softDelete, Func < T, bool> filter)
        {
            List<T> entitiesToDelete = new List<T>();

            foreach (T entity in DataContext.Set<T>().Where(filter))
            {
                if (softDelete)
                {
                    entity.IsDeleted = true;
                    entity.State = ObjectState.Modified;
                }
                else
                    entity.State = ObjectState.Deleted;

                entitiesToDelete.Add(entity);
            }

            ApplyStateChanges<T>(softDelete, entitiesToDelete);
        }

        #endregion      

        #region Private Functions

        /// <summary>
        /// Processes entities of the parent type
        /// </summary>        
        /// <param name="softDelete">Soft delete entities marked for deletion</param>
        /// <param name="items">A collection of entities</param>
        private void ApplyStateChanges(bool softDelete, List<T> items)
        {
            ApplyStateChanges<T>(softDelete, items);
        }

        /// <summary>
        /// Loops through entities and determines if they need to be attached, added and then sets the entity state accordingly
        /// </summary>
        /// <typeparam name="T2">Type of items to process</typeparam>
        /// <param name="softDelete">Soft delete entities marked for deletion</param>
        /// <param name="items">A collection of entities to process</param>
        private void ApplyStateChanges<T2>(bool softDelete, List<T2> items) where T2: EntityBase
        {
            var results = new List<ValidationResult>();
            bool isValid = true;

            foreach (T2 item in items)
            {
                if (!Validator.TryValidateObject(item, new ValidationContext(item, null, null), results, true))
                    isValid = false;
            }

            if (!isValid)
            {
                string errorMessages = results.Select(result => result.ErrorMessage).Aggregate((a, b) => a + ", " + b);                                        
                throw new ValidationException(string.Format("Can not apply current changes, validation has failed for one or more entities: {0}", errorMessages));
            }

            try
            {
                DbSet<T2> dbSet = DataContext.Set<T2>();
                List<T2> processedItems = new List<T2>();

                foreach (T2 item in items)
                {
                    processedItems.Add(ProcessEntityState<T2>(softDelete, dbSet, item));
                }

                //Replace items with processed
                items.RemoveAll(x => true);
                items.AddRange(processedItems);

                //Now process all changes
                var dbEntityEntries = DataContext.ChangeTracker.Entries<EntityBase>();

                foreach (DbEntityEntry<EntityBase> entry in dbEntityEntries)
                {
                    //Get dbEntity
                    var dbEntity = DataContext.Entry(entry.Entity);

                    //Update audit fields 
                    UpdateAuditFields(softDelete, entry.Entity);

                    //Update state of entity from object state
                    dbEntity.State = HelperFunctions.ConvertState(entry.Entity.State);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, true);
            }
        }

        /// <summary>
        /// This function checks the state of the entity (of type T2) and handles it accordingly
        /// Note: refactored out of the ApplyStateChanges function, for readability
        /// </summary>
        /// <typeparam name="T2">Type of the parameter arguments</typeparam>
        /// <param name="softDelete">Soft delete entities</param>
        /// <param name="dbSet">The DataContext set of type (T2)</param>
        /// <param name="item">The item of type (T2) to process</param>
        private T2 ProcessEntityState<T2>(bool softDelete, DbSet<T2> dbSet, T2 item) where T2 : EntityBase
        {
            //Items should always be disconnected (coming from messagebus)
            //So don't need to check the state first
            if (item.State == ObjectState.Added)
            {
                dbSet.Add(item);
            }
            else
            {
                if (item.State == ObjectState.Modified)
                {
                    if (!ExistsLocal<T2>(item))
                    {
                        dbSet.Attach(item);
                    }
                }
                else if (item.State == ObjectState.Deleted)
                {
                    //we need to ensure the item actually exists, by loading it explicitly
                    var existing = Exists<T2>(item);
                    if (existing != null)
                    {
                        if (softDelete)
                        {
                            existing.IsDeleted = true;
                            existing.State = ObjectState.Modified;
                            item = existing;
                        }
                        else
                            dbSet.Remove(existing);
                    }

                }
            }

            return item;
        }

        /// <summary>
        /// Updated audit fields on record
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <param name="softDelete">Soft deleting entities</param>
        /// <param name="item">Entity to update</param>
        protected void UpdateAuditFields<T2>(bool softDelete, T2 item) where T2 : EntityBase
        {
            switch (item.State)
            {
                case ObjectState.Added:
                    item.CreatedBy = User;
                    item.CreatedDate = DateTime.Now;
                    item.ModifiedBy = User;
                    item.ModifiedDate = DateTime.Now;
                    break;
                case ObjectState.Deleted:
                    if (softDelete)
                    {
                        item.ModifiedBy = User;
                        item.ModifiedDate = DateTime.Now;
                    }
                    break;
                case ObjectState.Modified:
                    item.ModifiedBy = User;
                    item.ModifiedDate = DateTime.Now;
                    break;
                case ObjectState.Unchanged:
                    //Don't change anything on items marked as unchanged
                    break;
            }
        }

        #endregion        
    }


    public static class ExpressionExtensions
    {
        /// <summary>
        /// Nifty way of combining to two where clauses via expressions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1">First expression</param>
        /// <param name="expr2">Second expression</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> AndAlso<T>( this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
        }

        /// <summary>
        /// Nifty way of combining to two where clauses via expressions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1">First expression</param>
        /// <param name="expr2">Second expression</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left, right), parameter);
        }

        private class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                    return _newValue;
                return base.Visit(node);
            }
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Charon.Core.Entities;
using Charon.Core.Messaging;
using System.Threading.Tasks;

namespace Charon.Core.Services
{
    public interface IPersistenceService: IDisposable
    {
        void AddAllEntityTypesUpdatedHandler(Action<object> onResponse);
        void AddEntityTypeUpdatedHandler<T>(Action<EntitiesChangedResponse<T>> onResponse) where T : EntityBase;
        Task<EntityListResponse<T>> GetAllEntitiesAsync<T>() where T : EntityBase;
        Guid GetEntities<T>(Action<EntityListResponse<T>> onResponse, Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] navigationProperties) where T : EntityBase;
        Task<EntityListResponse<T>> GetEntitiesAsync<T>(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] navigationProperties) where T : EntityBase;
        Guid GetPagedEntities<T>(Action<PagedEntityListResponse<T>> onResponse, int page, int itemsPerPage, Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] navigationProperties) where T : EntityBase;
        Task<PagedEntityListResponse<T>> GetPagedEntitiesAsync<T>(int page, int itemsPerPage, Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] navigationProperties) where T : EntityBase;
        Guid PushListings<T>(Action<List<T>> onResponse, List<T> Entities) where T : EntityBase;
        Task<List<T>> PushListingsAsync<T>(Action<List<T>> onResponse, List<T> Entities) where T : EntityBase;
        Guid UpdateEntities<T>(List<T> entities) where T : EntityBase;
        Guid UpdateEntities<T>(Action<EntitiesChangedResponse<T>> onResponse, List<T> entities) where T : EntityBase;
        Task<EntitiesChangedResponse<T>> UpdateEntitiesAsync<T>(List<T> entities) where T : EntityBase;
    }
}
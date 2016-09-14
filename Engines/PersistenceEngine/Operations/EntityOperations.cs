using Charon.Core.Messaging;
using Charon.Core.Entities;
using EasyNetQ;
using System;
using System.Linq;
using Charon.Data.DataAccess;
using System.Diagnostics;
using Charon.Messaging;
using System.Collections.Generic;
using System.Linq.Expressions;
using Charon.Data.EntityFramework;
using NLog;
using FastMember;
using System.Collections;
using System.Runtime.Serialization;
using System.Reflection;

namespace Charon.Engines.PersistenceEngine
{
    public class EntityOperations<T> where T : EntityBase
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private BusManager _busManager = null;

        public EntityOperations(BusManager busManager)
        {
            _busManager = busManager;
        }

        public void UpdateEntities(EntityChangeRequest<T> request)
        {
            EntitiesChangedResponse<T> response = null;
            var result = false;

            //Use generic data accessor
            using (var dataAccessor = new DataAccessor<T>(request.User))
            {
                try
                {
                    dataAccessor.BeginTransaction();
                    dataAccessor.InsertOrUpdate(request.Entities);
                    dataAccessor.SaveChanges();
                    dataAccessor.CommitTransaction();
                    dataAccessor.RefreshEntities(request.Entities);
                    result = true;
                }
                catch (Exception ex)
                {
                    dataAccessor.RollbackTransaction();
                    _logger.Log(LogLevel.Error, ex, string.Format("UpdateEntities request Id {0} failed.", request.Id));
                    result = false;
                    response = new EntitiesChangedResponse<T> { Errors = new string[] { ex.Message }.ToList(), EntitiesUpdated = new List<T>(), Id = request.Id, Status = ResponseStatus.Failure };
                }
            }

            if (result)
            {
                _logger.Log(LogLevel.Info, string.Format("UpdateEntities request Id {0} completed successfully.", request.Id));
                response = new EntitiesChangedResponse<T> { EntitiesUpdated = request.Entities.ToList(), Id = request.Id, Status = ResponseStatus.Success };
            }

            EntitiesUpdated(response, request.PrivateResponseRequested);
        }

        public void GetEntities(EntityListRequest<T> request)
        {
            var responseQueue = request.PrivateResponseQueue;
            var filter = request.Filter.GetFilter();
            var navigationProperties = request.NavigationProperties.GetNavigationProperties();
            var user = request.User;
            var requestId = request.Id;

            EntityListResponse<T> response = null;
            IList<T> entities = null;
            var result = false;

            //Use generic data accessor
            using (var dataAccessor = new DataAccessor<T>(user))
            {
                try
                {
                    entities = dataAccessor.GetEntities(filter, navigationProperties);
                    result = true;
                }
                catch (Exception ex)
                {
                    //Cast as our own exception to get base message
                    var exception = new BaseDbException(ex);
                    _logger.Log(LogLevel.Error, ex);
                    result = false;
                    response = new EntityListResponse<T> { Errors = new string[] { exception.Message }.ToList(), Id = requestId, NavigationProperties = request.NavigationProperties, Status = ResponseStatus.Failure };
                }
            }

            if (result)
            {
                _logger.Log(LogLevel.Info, string.Format("GetEntities request Id {0} completed successfully.", requestId));
                response = new EntityListResponse<T> { Entities = entities.ToList(), Id = requestId, NavigationProperties = request.NavigationProperties, Status = ResponseStatus.Success };
            }

            EntitiesGot(response);
        }

        public void GetPagedEntities(PagedEntityListRequest<T> request)
        {
            var responseQueue = request.PrivateResponseQueue;
            var filter = request.Filter.GetFilter();
            var navigationProperties = request.NavigationProperties.GetNavigationProperties();
            var page = request.Page;
            var itemsPerPage = request.ItemsPerPage;
            var user = request.User;
            var requestId = request.Id;

            PagedEntityListResponse<T> response = null;
            CollectionPage<T> collectionPage = null;
            var result = false;

            //Use generic data accessor
            using (var dataAccessor = new DataAccessor<T>(user))
            {
                try
                {
                    collectionPage = dataAccessor.GetPagedEntities(page, itemsPerPage, filter, navigationProperties);
                    result = true;
                }
                catch (Exception ex)
                {
                    //Cast as our own exception to get base message
                    var exception = new BaseDbException(ex);
                    _logger.Log(LogLevel.Error, ex);
                    result = false;
                    response = new PagedEntityListResponse<T> { Errors = new string[] { exception.Message }.ToList(), Id = requestId, NavigationProperties = request.NavigationProperties, Status = ResponseStatus.Failure };
                }
            }

            if (result)
            {
                _logger.Log(LogLevel.Info, string.Format("GetPagedEntities request Id {0} completed successfully.", requestId));
                response = new PagedEntityListResponse<T> { CollectionPage = collectionPage, Id = requestId, NavigationProperties = request.NavigationProperties, Status = ResponseStatus.Success };
            }

            PagedEntitiesGot(response);
        }

        private void EntitiesUpdated(EntitiesChangedResponse<T> entitiesChangedResponse, bool privateResponseRequested)
        {
            //Publish to entities updated fanout exchange for anyone listening
             _busManager.Publish(_busManager.EntitiesUpdatedFanoutExchange, entitiesChangedResponse);

            //If we've been asked for private response - do that as well
            if (privateResponseRequested)
            {
                //response queue won't be in our management bus queue cache so create it first (even though queue should exist on rabbit mq)
                _busManager.CreateQueue(_busManager.TopicExchange, entitiesChangedResponse.PrivateResponseQueue);
                _busManager.Publish(_busManager.TopicExchange, entitiesChangedResponse.PrivateResponseQueue, entitiesChangedResponse);
            }
        }

        private void EntitiesGot(EntityListResponse<T> entitiesGot)
        {
            //response queue won't be in our management bus queue cache so create it first (even though queue should exist on rabbit mq)
            _busManager.CreateQueue(_busManager.TopicExchange, entitiesGot.PrivateResponseQueue);
            _busManager.Publish(_busManager.TopicExchange, entitiesGot.PrivateResponseQueue, entitiesGot);
        }

        private void PagedEntitiesGot(PagedEntityListResponse<T> entitiesGot)
        {
            //response queue won't be in our management bus queue cache so create it first (even though queue should exist on rabbit mq)
            _busManager.CreateQueue(_busManager.TopicExchange, entitiesGot.PrivateResponseQueue);
            _busManager.Publish(_busManager.TopicExchange, entitiesGot.PrivateResponseQueue, entitiesGot);
        }
    }
}

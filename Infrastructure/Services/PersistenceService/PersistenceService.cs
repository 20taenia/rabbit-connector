using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Charon.Core.Entities;
using Charon.Core.Messaging;
using Charon.Core.Services;
using Charon.Messaging;
using EasyNetQ;
using EasyNetQ.Topology;
using System.Threading.Tasks;
using System.Linq;
using Charon.Core.Utilities;
using FastMember;

namespace Charon.Infrastructure.Services
{
    public class PersistenceService : IPersistenceService
    {
        private BusManager _busManager;
        private string _user = null;
        private string _password = null;
        private IDictionary<Guid, object> _pendingRequests = null;
        private IList<string> _ownedFanoutQueues = null;
        private IList<string> _ownedTemporaryQueues = null;
        private IList<Action<object>> _enitiesUpdatedResponseActions = null;

        public PersistenceService(): this("red","Tastyred1") { }

        public PersistenceService(string username, string password)
        {
            PersistenceServiceConfiguration config = null;
            string rabbitMQAddress = null;

            try
            {
                config = PersistenceServiceConfiguration.Configuration;
                rabbitMQAddress = config.RabbitMQAddress;
            }
            catch(Exception ex)
            {
                throw new NullReferenceException("No RabbitMQ configuration information provided in application or web config. Persistence service cannot continue.", ex);
            }

            _busManager = new BusManager(username, password, config.RabbitMQAddress);
            _pendingRequests = new Dictionary<Guid, object>();
            _ownedFanoutQueues = new List<string>();
            _ownedTemporaryQueues = new List<string>();
            _enitiesUpdatedResponseActions = new List<Action<object>>();
            _user = username;
            _password = password;
        }

        public Guid GetPagedEntities<T>(Action<PagedEntityListResponse<T>> onResponse, int page, int itemsPerPage, Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] navigationProperties) where T : EntityBase
        {
            //Create request
            PagedEntityListRequest<T> request = new PagedEntityListRequest<T>(Guid.NewGuid());
            request.Page = page;
            request.ItemsPerPage = itemsPerPage;
            request.NavigationProperties = new NavigationPropertiesHandle<T>(navigationProperties);
            request.Filter = new FilterHandle<T>(filter);
            request.User = _user;

            //Add to pending requests
            _pendingRequests.Add(request.Id, request);

            //Setup and subscribe to result queue
            IQueue resultQueue = _busManager.CreateQueue(_busManager.TopicExchange, request.PrivateResponseQueue);
            _busManager.Subscribe<PagedEntityListResponse<T>>(_busManager.TopicExchange, request.PrivateResponseQueue, (response, info) => OnGetPagedEntitesResponse(response, info, onResponse));
            _ownedTemporaryQueues.Add(request.PrivateResponseQueue);

            //Send request
            _busManager.Publish(_busManager.TopicExchange, TopicQueues.ProductEntitiesList, request);

            //Return request guid
            return request.Id;
        }

        private void OnGetPagedEntitesResponse<T>(PagedEntityListResponse<T> response, MessageReceivedInfo info, Action<PagedEntityListResponse<T>> onResponse) where T : EntityBase
        {
            //Check if we're passing on the result and its one of ours
            if (_pendingRequests.ContainsKey(response.Id) && onResponse != null)
            {
                //Remove from pending
                _pendingRequests.Remove(response.Id);

                //Unsubscribe from temporary queue and delete it
                //Doesn't appear to be valid way to unsubscribe so just delete
                var queueName = response.PrivateResponseQueue;
                _busManager.DeleteQueue(_busManager.TopicExchange, queueName);
                _ownedTemporaryQueues.Remove(_ownedTemporaryQueues.SingleOrDefault(q => q == queueName));

                //Invoke action method
                onResponse.Invoke(response);
            }
        }


        public async Task<PagedEntityListResponse<T>> GetPagedEntitiesAsync<T>(int page, int itemsPerPage, Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] navigationProperties) where T : EntityBase
        {
            //Create request
            PagedEntityListRequest<T> request = new PagedEntityListRequest<T>(Guid.NewGuid());
            request.Page = page;
            request.ItemsPerPage = itemsPerPage;
            request.NavigationProperties = new NavigationPropertiesHandle<T>(navigationProperties);
            request.Filter = new FilterHandle<T>(filter);
            request.User = _user;

            //Add to pending requests
            _pendingRequests.Add(request.Id, request);

            //Create tcs for request
            var tcs = new TaskCompletionSource<PagedEntityListResponse<T>>();

            //Setup and subscribe to result queue
            IQueue resultQueue = _busManager.CreateQueue(_busManager.TopicExchange, request.PrivateResponseQueue);
            _busManager.Subscribe<PagedEntityListResponse<T>>(_busManager.TopicExchange, request.PrivateResponseQueue, (response, info) => OnGetPagedEntitesResponse(response, info, tcs));
            _ownedTemporaryQueues.Add(request.PrivateResponseQueue);

            //Send request
            _busManager.Publish(_busManager.TopicExchange, TopicQueues.ProductEntitiesList, request);

            //Await task return 
            return await tcs.Task;
        }

        private void OnGetPagedEntitesResponse<T>(PagedEntityListResponse<T> response, MessageReceivedInfo info, TaskCompletionSource<PagedEntityListResponse<T>> tcs) where T : EntityBase
        {
            //Check if we're passing on the result and its one of ours
            if (_pendingRequests.ContainsKey(response.Id) && tcs != null)
            {
                //Remove from pending
                _pendingRequests.Remove(response.Id);

                //Unsubscribe from temporary queue and delete it
                //Doesn't appear to be valid way to unsubscribe so just delete
                var queueName = response.PrivateResponseQueue;
                _busManager.DeleteQueue(_busManager.TopicExchange, queueName);
                _ownedTemporaryQueues.Remove(_ownedTemporaryQueues.SingleOrDefault(q => q == queueName));

                //Invoke action method
                tcs.SetResult(response);
            }
        }

        public Guid GetEntities<T>(Action<EntityListResponse<T>> onResponse, Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] navigationProperties)where T : EntityBase
        {
            //Create request
            EntityListRequest<T> request = new EntityListRequest<T>(Guid.NewGuid());
            request.NavigationProperties = new NavigationPropertiesHandle<T>(navigationProperties);
            request.Filter = new FilterHandle<T>(filter);
            request.User = _user;

            //Add to pending requests
            _pendingRequests.Add(request.Id, request);

            //Setup and subscribe to result queue
            IQueue resultQueue = _busManager.CreateQueue(_busManager.TopicExchange, request.PrivateResponseQueue);
            _busManager.Subscribe<EntityListResponse<T>>(_busManager.TopicExchange, request.PrivateResponseQueue, (response, info) => OnGetEntitiesResponse(response, info, onResponse));
            _ownedTemporaryQueues.Add(request.PrivateResponseQueue);

            //Send request
            _busManager.Publish(_busManager.TopicExchange, TopicQueues.ProductEntitiesList, request);

            //Return request guid
            return request.Id;
        }

        private void OnGetEntitiesResponse<T>(EntityListResponse<T> response, MessageReceivedInfo info, Action<EntityListResponse<T>> onResponse) where T : EntityBase
        {
            //Check if we're passing on the result and its one of ours
            if (_pendingRequests.ContainsKey(response.Id) && onResponse != null)
            {
                //Remove from pending
                _pendingRequests.Remove(response.Id);

                //Unsubscribe from temporary queue and delete it
                //Doesn't appear to be valid way to unsubscribe so just delete
                var queueName = response.PrivateResponseQueue;
                _busManager.DeleteQueue(_busManager.TopicExchange, queueName);
                _ownedTemporaryQueues.Remove(_ownedTemporaryQueues.SingleOrDefault(q => q == queueName));

                //Invoke action method
                onResponse.Invoke(response);
            }
        }

        public async Task<EntityListResponse<T>> GetAllEntitiesAsync<T>() where T : EntityBase
        {
            return await GetEntitiesAsync<T>(x => true, null);
        }

        public async Task<EntityListResponse<T>> GetEntitiesAsync<T>(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] navigationProperties) where T:EntityBase
        {
            //Create request
            EntityListRequest<T> request = new EntityListRequest<T>(Guid.NewGuid());
            request.NavigationProperties = new NavigationPropertiesHandle<T>(navigationProperties);
            request.Filter = new FilterHandle<T>(filter);
            request.User = _user;

            //Add to pending requests
            _pendingRequests.Add(request.Id, request);

            //Create tcs for request
            var tcs = new TaskCompletionSource<EntityListResponse<T>>();

            //Setup and subscribe to result queue
            IQueue resultQueue = _busManager.CreateQueue(_busManager.TopicExchange, request.PrivateResponseQueue);
            _busManager.Subscribe<EntityListResponse<T>>(_busManager.TopicExchange, request.PrivateResponseQueue, (response, info) => OnGetEntitiesResponse(response, info, tcs));
            _ownedTemporaryQueues.Add(request.PrivateResponseQueue);

            //Send request
            _busManager.Publish(_busManager.TopicExchange, TopicQueues.ProductEntitiesList, request);

            //Await tcs task
            return await tcs.Task;
        }

        private void OnGetEntitiesResponse<T>(EntityListResponse<T> response, MessageReceivedInfo info, TaskCompletionSource<EntityListResponse<T>> tcs) where T : EntityBase
        {
            //Check if we're passing on the result and its one of ours
            if (_pendingRequests.ContainsKey(response.Id) && tcs != null)
            {
                //Remove from pending
                _pendingRequests.Remove(response.Id);

                //Unsubscribe from temporary queue and delete it
                //Doesn't appear to be valid way to unsubscribe so just delete
                var queueName = response.PrivateResponseQueue;
                _busManager.DeleteQueue(_busManager.TopicExchange, queueName);
                _ownedTemporaryQueues.Remove(_ownedTemporaryQueues.SingleOrDefault(q => q == queueName));

                //Set result on task completion source
                tcs.SetResult(response);
            }
        }

        public Guid UpdateEntities<T>(List<T> entities) where T : EntityBase
        {
            return UpdateEntities(null, entities);
        }

        public Guid UpdateEntities<T>(Action<EntitiesChangedResponse<T>> onResponse, List<T> entities) where T : EntityBase
        {
            //Create request
            EntityChangeRequest<T> request = new EntityChangeRequest<T>(Guid.NewGuid());
            request.Entities = entities;
            request.User = _user;

            //Subscribe to temp response queue if not null
            if (onResponse != null)
            {
                //Update request to say we want a private response
                request.PrivateResponseRequested = true;

                //Add to pending requests
                _pendingRequests.Add(request.Id, request);

                //Setup and subscribe to temp response queue
                IQueue resultQueue = _busManager.CreateQueue(_busManager.TopicExchange, request.PrivateResponseQueue);
                _busManager.Subscribe<EntitiesChangedResponse<T>>(_busManager.TopicExchange, request.PrivateResponseQueue, (response, info) => OnUpdateEntitiesResponse(response, info, onResponse));
                _ownedTemporaryQueues.Add(request.PrivateResponseQueue);
            }

            //Send request
            _busManager.Publish(_busManager.TopicExchange, TopicQueues.ProductEntitiesUpdate, request);

            //Return request guid
            return request.Id;
        }

        private void OnUpdateEntitiesResponse<T>(EntitiesChangedResponse<T> response, MessageReceivedInfo info, Action<EntitiesChangedResponse<T>> onResponse) where T : EntityBase
        {
            //Check if we're passing on the result and its one of ours
            if (_pendingRequests.ContainsKey(response.Id) && onResponse != null)
            {
                //Remove from pending
                _pendingRequests.Remove(response.Id);

                //Unsubscribe from temporary queue and delete it
                //Doesn't appear to be valid way to unsubscribe so just delete
                var queueName = response.PrivateResponseQueue;
                _busManager.DeleteQueue(_busManager.TopicExchange, queueName);
                _ownedTemporaryQueues.Remove(_ownedTemporaryQueues.SingleOrDefault(q => q == queueName));

                //Invoke response handler
                onResponse.Invoke(response);
            }
        }

        public async Task<EntitiesChangedResponse<T>> UpdateEntitiesAsync<T>(List<T> entities) where T : EntityBase
        {
            //Create request
            EntityChangeRequest<T> request = new EntityChangeRequest<T>(Guid.NewGuid());
            request.Entities = entities;
            request.User = _user;

            //Update request to say we want a private response
            request.PrivateResponseRequested = true;

            //Add to pending requests
            _pendingRequests.Add(request.Id, request);

            //Create tcs for request
            var tcs = new TaskCompletionSource<EntitiesChangedResponse<T>>();

            //Setup and subscribe to temp response queue
            IQueue resultQueue = _busManager.CreateQueue(_busManager.TopicExchange, request.PrivateResponseQueue);
            _busManager.Subscribe<EntitiesChangedResponse<T>>(_busManager.TopicExchange, request.PrivateResponseQueue, (response, info) => OnUpdateEntitiesResponse(response, info, tcs));
            _ownedTemporaryQueues.Add(request.PrivateResponseQueue);

            //Send request
            _busManager.Publish(_busManager.TopicExchange, TopicQueues.ProductEntitiesUpdate, request);

            //Return request guid
            return await tcs.Task;
        }

        private void OnUpdateEntitiesResponse<T>(EntitiesChangedResponse<T> response, MessageReceivedInfo info, TaskCompletionSource<EntitiesChangedResponse<T>> tcs) where T : EntityBase
        {
            //Check if we're passing on the result and its one of ours
            if (_pendingRequests.ContainsKey(response.Id) && tcs != null)
            {
                //Remove from pending
                _pendingRequests.Remove(response.Id);

                //Unsubscribe from temporary queue and delete it
                //Doesn't appear to be valid way to unsubscribe so just delete
                var queueName = response.PrivateResponseQueue;
                _busManager.DeleteQueue(_busManager.TopicExchange, queueName);
                _ownedTemporaryQueues.Remove(_ownedTemporaryQueues.SingleOrDefault(q => q == queueName));

                //Invoke response handler
                tcs.SetResult(response);
            }
        }

        public Guid PushListings<T>(Action<List<T>> onResponse, List<T> Entities) where T : EntityBase
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> PushListingsAsync<T>(Action<List<T>> onResponse, List<T> Entities) where T : EntityBase
        {
            throw new NotImplementedException();
        }

        public void AddEntityTypeUpdatedHandler<T>(Action<EntitiesChangedResponse<T>> onResponse) where T : EntityBase
        {
            //Create fanout queue for product updates of type T (potentially initiated by other nodes)
            Type specificType = onResponse.GetType().GetGenericArguments()[0];
            string queueName = string.Format("{0}.{1}.{2}.{3}", TopicQueues.ProductEntitiesUpdatedPrefix, specificType.Name, _user, Guid.NewGuid());

            //Add to list so we can remove it on dispose
            _ownedFanoutQueues.Add(queueName);

            //Create fanout queue and subscribe
            IQueue updatesQueue = _busManager.CreateQueue(_busManager.EntitiesUpdatedFanoutExchange, queueName);
            _busManager.Subscribe<EntitiesChangedResponse<T>>(_busManager.TopicExchange, queueName, (response, info) => onResponse(response));
        }

        public void AddAllEntityTypesUpdatedHandler(Action<object> onResponse)
        {
            //Create fanout queue for all product updates (potentially initiated by other nodes)
            string queueName = string.Format("{0}.{1}.{2}", TopicQueues.ProductEntitiesUpdatedPrefix, _user, Guid.NewGuid());

            //Add to list so we can remove it on dispose
            _ownedFanoutQueues.Add(queueName);

            //Create fanout queue and subscribe
            IQueue updatesQueue = _busManager.CreateQueue(_busManager.EntitiesUpdatedFanoutExchange, queueName);

            //Add our response actions to list
            _enitiesUpdatedResponseActions.Add(onResponse);

            //Subscribe to queues/types
            SubscribeToProductEntitiesUpdatedQueue(updatesQueue.Name);
        }

        private void SubscribeToProductEntitiesUpdatedQueue(string queueName)
        {
            var responseType = typeof(EntitiesChangedResponse<>);

            //This is where we store the handlers to pass to busManager
            HandlerManager handlerManager = new HandlerManager();

            //Loop through entity types and create QueueMessageHandler of type 
            foreach (var entityType in EntityTypes.EntityBaseTypes)
            {
                //Get onRequest method with appropriate request and entity type
                var entityResponseType = responseType.MakeGenericType(entityType);
                var onResponseMethod = GenericHelpers.GetMethodExt(this.GetType(), "OnAllEntitiesUpdatedResponse", typeof(void), new Type[] { entityResponseType, typeof(MessageReceivedInfo) });
                var onResponseMethodTyped = onResponseMethod.MakeGenericMethod(entityType);

                //Create action delegate from typed onRequest method
                var actionType = typeof(Action<,>).MakeGenericType(entityResponseType, typeof(MessageReceivedInfo));
                var action = Delegate.CreateDelegate(actionType, this, onResponseMethodTyped);

                //Create QueueMessageHandler of approriate type and pass in action and queueName
                var queueMessageHandlerType = typeof(QueueMessageHandler<>).MakeGenericType(entityResponseType);
                var accessor = TypeAccessor.Create(queueMessageHandlerType);
                var queueMessageHandler = accessor.CreateNew();
                accessor[queueMessageHandler, "Handler"] = action;
                accessor[queueMessageHandler, "QueueName"] = queueName;

                //Get AddHandler method from handlerManager to add appropriate request type and invoke
                var addHandlerMethod = GenericHelpers.GetMethodExt(handlerManager.GetType(), "AddHandler", typeof(void), new Type[] { queueMessageHandlerType });
                var addHandlerMethodTyped = addHandlerMethod.MakeGenericMethod(entityResponseType);
                addHandlerMethodTyped.Invoke(handlerManager, new object[] { queueMessageHandler });
            }

            //Subscribe with our filled up handlerManager
            _busManager.Subscribe(_busManager.TopicExchange, queueName, handlerManager);
        }

        private void OnAllEntitiesUpdatedResponse<T>(EntitiesChangedResponse<T> response, MessageReceivedInfo info) where T : EntityBase
        {
            foreach(var responseAction in _enitiesUpdatedResponseActions)
            {
                responseAction.Invoke(response);
            }
        }

        public void Dispose()
        {
            //Delete all our fanout queues
            foreach (var queueName in _ownedFanoutQueues)
            {
                _busManager.DeleteQueue(_busManager.EntitiesUpdatedFanoutExchange, queueName);
            }

            //Delete all our temporary queues (that weren't responded to and therefore deleted)
            foreach (var queueName in _ownedTemporaryQueues)
            {
                _busManager.DeleteQueue(_busManager.TopicExchange, queueName);
            }

            _busManager.Dispose();
        }
    }
}

using System;
using System.Diagnostics;
using EasyNetQ;
using EasyNetQ.Topology;
using Charon.Core.Entities;
using Charon.Core.Messaging;
using Charon.Messaging;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using NLog;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using Charon.Core.Utilities;
using FastMember;
using Charon.Engines.Common;

namespace Charon.Engines.ProductEngine
{
    public class Engine
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private ExecutionManager _executionManager = null;
        private BusManager _busManager;

        public void Start()
        {
            _logger.Log(LogLevel.Info, message: "Initialising ProductEngine");
            _executionManager = new ExecutionManager();

            _logger.Log(LogLevel.Info, "Initialisation messaging");
            EngineConfiguration config = EngineConfiguration.Configuration;
            _busManager = new BusManager("productengine", "Tastyproducts1", config.RabbitMQAddress, config.QueueDataTearDown);

            _logger.Log(LogLevel.Info, "Subscribing to queues");
            SubscribeToEntityQueues();

            _logger.Log(LogLevel.Info, "Listening");
        }

        private void SubscribeToEntityQueues()
        {
            //Add message handlers for all request types and entity types
            //foreach (var requestType in EntityTypes.EntityRequestTypes)
            //{
            //    //This is where we store the handlers to pass to busManager
            //    HandlerManager handlerManager = new HandlerManager();

            //    //Do one queue/request type at a time
            //    string requestQueue = null;

            //    foreach (var entityType in EntityTypes.EntityBaseTypes)
            //    {
            //        //Get onRequest method with appropriate request and entity type
            //        var entityRequestType = requestType.MakeGenericType(entityType);
            //        var onRequestMethod = GenericHelpers.GetMethodExt(this.GetType(), "OnRequest", typeof(void), new Type[] { requestType, typeof(MessageReceivedInfo) });
            //        var onRequestMethodTyped = onRequestMethod.MakeGenericMethod(entityType);

            //        //Create action delegate from typed onRequest method
            //        var actionType = typeof(Action<,>).MakeGenericType(entityRequestType, typeof(MessageReceivedInfo));
            //        var action = Delegate.CreateDelegate(actionType, this, onRequestMethodTyped);

            //        //Instantiate a request of appropriate type to get at its request queue
            //        if (requestQueue == null)
            //        {
            //            var requestAccessor = TypeAccessor.Create(entityRequestType);
            //            var request = requestAccessor.CreateNew();
            //            requestQueue = (string)requestAccessor[request, "RequestQueue"];
            //        }

            //        //Create QueueMessageHandler of approriate type and pass in action and queueName
            //        var queueMessageHandlerType = typeof(QueueMessageHandler<>).MakeGenericType(entityRequestType);
            //        var accessor = TypeAccessor.Create(queueMessageHandlerType);
            //        var queueMessageHandler = accessor.CreateNew();
            //        accessor[queueMessageHandler, "Handler"] = action;
            //        accessor[queueMessageHandler, "QueueName"] = requestQueue;

            //        //Get AddHandler method from handlerManager to add appropriate request type
            //        var addHandlerMethod = GenericHelpers.GetMethodExt(handlerManager.GetType(), "AddHandler", typeof(void), new Type[] { queueMessageHandlerType });
            //        var addHandlerMethodTyped = addHandlerMethod.MakeGenericMethod(entityRequestType);
            //        addHandlerMethodTyped.Invoke(handlerManager, new object[] { queueMessageHandler });
            //    }

            //    //Subscribe with our filled up handlerManager
            //    _busManager.Subscribe(_busManager.TopicExchange, requestQueue, handlerManager);
            //}
        }

        //public void OnRequest<T>(EntityChangeRequest<T> request, MessageReceivedInfo info) where T : EntityBase
        //{
        //    EntityChangeRequestTask<T> task = new EntityChangeRequestTask<T>(_busManager, request);
        //    _executionManager.AddTask(task);
        //    _logger.Log(LogLevel.Info, "Added update request to executor: Id " + request.Id.ToString());
        //}

        //public void OnRequest<T>(EntityListRequest<T> request, MessageReceivedInfo info) where T : EntityBase
        //{
        //    EntityListRequestTask<T> task = new EntityListRequestTask<T>(_busManager, request);
        //    _executionManager.AddTask(task);
        //    _logger.Log(LogLevel.Info,"Added list request to executor queue: Id " + request.Id.ToString());
        //}

        //public void OnRequest<T>(PagedEntityListRequest<T> request, MessageReceivedInfo info) where T : EntityBase
        //{
        //    PagedEntityListRequestTask<T> task = new PagedEntityListRequestTask<T>(_busManager, request);
        //    _executionManager.AddTask(task);
        //    _logger.Log(LogLevel.Info, "Added paged list request to executor queue: Id " + request.Id.ToString());
        //}

        public void Stop()
        {
            _logger.Log(LogLevel.Info, "ProductEngine is shutting down");
            _busManager.Dispose();
        }
    }
}




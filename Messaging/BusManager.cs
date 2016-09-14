using Charon.Core.Entities;
using Charon.Core.Messaging;
using Charon.Messaging.EasyNetQCustomisation;
using EasyNetQ;
using EasyNetQ.Consumer;
using EasyNetQ.Management.Client;
using EasyNetQ.Management.Client.Model;
using EasyNetQ.Topology;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Charon.Core.Utilities;
using FastMember;
using System.Collections.Concurrent;

namespace Charon.Messaging
{
   
    public class BusManager : IDisposable, IBusManager
    {
        private const string _defaultHost = "rabbit-hole.westeurope.cloudapp.azure.com";
        private readonly string _virtualhost = "/";
        private readonly int _port = 5672;
        private readonly int _timeout = 300;
        private readonly string _adminUser = "admin";
        private readonly string _adminPassword = "Tastyalfalfa1";

        private string _connectionString = null;

        private IBus _bus;
        private IAdvancedBus _advancedBus;
        private Vhost _vhost;
        private IExchange _topicExchange;
        private IExchange _entitiesUpdatedFanoutExchange;

        private ConcurrentDictionary<string, IDisposable> _queueSubscriptions;
        private ConcurrentDictionary<IQueue, byte> _cachedQueues = null;

        private ManagementClient _manageClient = null;
        private DTOConverter _dtoConverter = null;
        private HandlerManager _handlerManager = null;

        //RabbitMQ client (that sits under EasynetQ) is not thread safe
        //Use locker to lock on messagebus activites
        private object _locker = null; 

        public IExchange TopicExchange { get { return _topicExchange; } }
        public IExchange EntitiesUpdatedFanoutExchange { get { return _entitiesUpdatedFanoutExchange; } }

        public BusManager(string username, string password, string host = _defaultHost, bool tearDownQueues = false)
        {
            _locker = new object();
            _dtoConverter = new DTOConverter();
            _handlerManager = new HandlerManager();
            _queueSubscriptions = new ConcurrentDictionary<string, IDisposable>();
            _connectionString = string.Format("host={0};port={1};username={2};password={3};virtualHost={4};timeout={5};requestedHeartbeat=0", host, _port.ToString(), username, password, _virtualhost, _timeout.ToString());

            try
            {
                //Connect to bus
                Connect();

                //Setup management client
                _manageClient = new ManagementClient(host, _adminUser, _adminPassword);
                _vhost = _manageClient.GetVhost(_virtualhost);

                //Create cached queues list
                _cachedQueues = new ConcurrentDictionary<IQueue, byte>();

                //Have to do this whatever happens
                InitialiseExchanges();

                //Initialise queues
                InitialiseQueues(tearDownQueues);
                   
            }
            catch (Exception ex)
            {
                HandleException(ex, true);
            }
        }

        private void Connect()
        {
            //Connect and set bus properties
            lock(_locker)
            {
                _bus = RabbitHutch.CreateBus(_connectionString, serviceRegister => serviceRegister
                    .Register<ISerializer>(serviceProvider => new CustomSerializer(serviceProvider.Resolve<ITypeNameSerializer>())));
                _advancedBus = _bus.Advanced;
            }
        }

        private void InitialiseExchanges()
        {
            lock(_locker)
            {
                _topicExchange = _advancedBus.ExchangeDeclare(Exchanges.Topic, EasyNetQ.Topology.ExchangeType.Topic);
                _entitiesUpdatedFanoutExchange = _advancedBus.ExchangeDeclare(Exchanges.ProductEntitiesUpdatedFanout, EasyNetQ.Topology.ExchangeType.Fanout);
            }
        }

        private void InitialiseQueues(bool tearDownQueues = false)
        {
            //TODO - JUST FOR DEBUGGING - Don't leave this fucker here!
            if (tearDownQueues)
                DeleteAllQueues();

            InitialiseTopicQueues();
        }

        private void InitialiseTopicQueues()
        {
            foreach (var queueName in TopicQueues.AllTopicQueues())
            {
                IQueue queue = null;

                lock(_locker)
                {
                    //Create queues (if they don't exist) and bind to queues
                    queue = _advancedBus.QueueDeclare(queueName, false, true);
                    _advancedBus.Bind(_topicExchange, queue, queueName);
                }

                //Add to our cached queues
                _cachedQueues.TryAdd(queue, 0);
            }
        }

        //JUST FOR DEBUGGING - REMOVE IN PRODUCTION
        private void DeleteAllQueues()
        {
            var allQueueNames = TopicQueues.AllTopicQueues().ToList();

            foreach (var manageQueue in _manageClient.GetQueues())
            {
                if (allQueueNames.Contains(manageQueue.Name))
                    DeleteQueue(_topicExchange, manageQueue.Name, false);
                else if (manageQueue.Name.StartsWith(TopicQueues.ProductEntitiesListPrefix) || manageQueue.Name.StartsWith(TopicQueues.ProductEntitiesUpdatedPrefix))
                    DeleteQueue(_topicExchange, manageQueue.Name, false);
            }
        }

        public void Subscribe(IExchange exchange, string queueName, HandlerManager queueHandlerManager)
        {
            //Loop through request and entity types adding queueMessageHandlers to our internal _handlerManager
            foreach (Type requestType in EntityTypes.EntityRequestAndResponseTypes)
            {
                foreach (Type entityType in EntityTypes.EntityBaseTypes)
                {
                    var requestWithEntityType = requestType.MakeGenericType(entityType);
                    var queueMessageHandlerType = typeof(QueueMessageHandler<>).MakeGenericType(requestWithEntityType);
                    var queueMessageHandlerList = GenericHelpers.InvokeGenericMethod(queueHandlerManager, requestWithEntityType, typeof(IList), "GetHandlersForType", queueName);
                    var list = queueMessageHandlerList as IList;

                    if (list != null && list.Count > 0)
                    {
                        foreach(var item in list)
                        {
                            GenericHelpers.InvokeGenericMethod(_handlerManager, requestWithEntityType, typeof(void), "AddHandler", new object[] { item });
                        }
                    }
                }
            }

            //Call subscribe internal 
            SubscribeInternal(exchange, queueName);
        }

        public void Subscribe<T>(IExchange exchange, string queueName, Action<T, MessageReceivedInfo> handler) where T: class
        {
            //Need to handle our subscribes differently as we're translating the message objects to/from DTOs
            //So we need to intercept message/translate and then pass to handlers
            //So add our own handlers to consume - then call convert and invoke the handler passed in here with the result
            var messageType = typeof(T);
            var entityType = messageType.GetGenericArguments().Single();
            var requestType = messageType.GetGenericTypeDefinition();

            Debug.WriteLine(string.Format("Subscribing to queue {0} for message type {1}", queueName, messageType.FullName));

            //Add the handler passed in to our handlerManager
            //Filtered by type
            _handlerManager.AddHandler(new QueueMessageHandler<T> { QueueName = queueName, Handler = handler });

            //Subscribe to bus internally
            SubscribeInternal(exchange, queueName);
        }

        private void SubscribeInternal(IExchange exchange, string queueName)
        {
            var queue = GetQueue(exchange, queueName);

            IDisposable subscription;
            if (_queueSubscriptions.TryGetValue(queueName, out subscription))
            {
                subscription.Dispose();
                _queueSubscriptions.Remove(queueName);
            }

            lock (_locker)
            {
                subscription = _advancedBus.Consume(queue, regHandler => AddHandlers(regHandler, queue));
            }

            _queueSubscriptions.Add(queueName, subscription);
        }

        private IHandlerRegistration AddHandlers(IHandlerRegistration handlerReg, IQueue queue)
        {
            //This is gonna be a bit NASTY
            //For this queue we need to find all the types we're currently holding handlers for
            //then add the internal onMessageReceived handler for each type
            //The reason for all this is calling consume on a queue with existing handlers for types
            //wipes them out and we have to add all type handlers again - so each time a type is added to be subscribed to
            //the whole previous set need to be re-added

            foreach (Type requestType in EntityTypes.EntityRequestAndResponseTypes)
            { 
                foreach (Type entityType in EntityTypes.EntityBaseTypes)
                {
                    var requestWithEntityType = requestType.MakeGenericType(entityType);
                    var queueMessageHandlerType = typeof(QueueMessageHandler<>).MakeGenericType(requestWithEntityType);
                    var queueMessageHandlerList = GenericHelpers.InvokeGenericMethod(_handlerManager, requestWithEntityType, typeof(IList), "GetHandlersForType", queue.Name);
                    var list = queueMessageHandlerList as IList;

                    if (list != null && list.Count > 0)
                    {
                        var messageType = typeof(IMessage<>).MakeGenericType(requestWithEntityType);
                        var addParamType = typeof(Action<,>).MakeGenericType(messageType, typeof(MessageReceivedInfo));
                        var addMethod = GenericHelpers.GetMethodExt(handlerReg.GetType(), "Add", handlerReg.GetType(), addParamType);
                        var addMethodTyped = addMethod.MakeGenericMethod(requestWithEntityType);

                        var onMessageMethod = GenericHelpers.GetMethodExt(this.GetType(), "OnMessageReceived", typeof(void), new Type[] { messageType, typeof(MessageReceivedInfo) });
                        var onMessageMethodTyped = onMessageMethod.MakeGenericMethod(requestWithEntityType);

                        var action = Delegate.CreateDelegate(addParamType, this, onMessageMethodTyped);

                        handlerReg = (IHandlerRegistration)addMethodTyped.Invoke(handlerReg, new object[] { action });
                    }

                }
            }

            return handlerReg;
        }

        private void OnMessageReceived<T>(IMessage<T> message, MessageReceivedInfo info) where T : class
        {

            Type messageType = typeof(T);

            //Get our response from message
            var response = message.Body;

            //Grab handers for our type
            var handlers = _handlerManager.GetHandlersForType<T>(info.Queue);

            //Invoke handlers with converted response
            var queueHandlerList = (List<QueueMessageHandler<T>>)handlers;
            queueHandlerList.ForEach(x =>
            {
                x.Handler.Invoke(response, info);
            });
        }

        public void Publish<T>(IExchange exchange, T message) where T : class
        {
            Publish(exchange, string.Empty, message);
        }

        public void Publish<T>(IExchange exchange, string queueName, T message) where T : class
        {
            //Do the translation
            var convertedMessage = _dtoConverter.ConvertToDTO(message);

            //Publish
            PublishInternal<T>(exchange, queueName, (T)convertedMessage);
        }

        private void PublishInternal<T>(IExchange exchange, string queueName, T message) where T: class
        {
            //Wrap message in IMessage
            IMessage<T> wrappedMessage = new Message<T>(message);

            lock (_locker)
            {
                _advancedBus.Publish(exchange, queueName, false, wrappedMessage);
            }
        }

        public void DeleteQueue(IExchange exchange, string queueName, bool throwIfNotCached = true)
        {
            var queue = GetQueue(exchange, queueName, throwIfNotCached);

            if (queue == null)
                throw new BusQueueDoesNotExistException(queueName);

            //Delete it - sometimes get a timeout at busy times do give it 3 retries
            ExceptionHandling.RetryForExceptionType<Exception>(() => DeleteQueueInternal(queue), 3, 500);
            _cachedQueues.Remove(queue);
        }

        public IQueue GetQueue(IExchange exchange, string queueName, bool throwIfNotCached = true)
        {
            //Grab from cache
            IQueue queue = _cachedQueues.SingleOrDefault(q => q.Key.Name == queueName).Key;

            if (queue == null && throwIfNotCached)
                throw new BusQueueDoesNotExistException(queueName);
            else if (queue == null)
                queue = CreateQueue(exchange, queueName);

            return queue;
        }

        public IQueue CreateQueue(IExchange exchange, string queueName)
        {
            //Check it doesn't exist in the cache first
            IQueue queue = _cachedQueues.SingleOrDefault(q => q.Key.Name == queueName).Key;
            
            //Create it
            if (queue == null)
            {
                //Flakey queue creation - when loads of bus operations are going on queue creation sometimes results in 
                //The definition of stupidity is repeatedly doing the same thing and expecting a different result - so lets do it 2 more times if it fails
                ExceptionHandling.RetryForExceptionType<Exception>(() => queue = CreateQueueInternal(exchange, queueName), 3, 500);

                //Add it to cache and return 
                _cachedQueues.TryAdd(queue, 0);
                return queue;
            }

            return queue;
        }

        private IQueue CreateQueueInternal(IExchange exchange, string queueName)
        {
            IQueue queue = null;

            lock (_locker)
            {
                queue = _advancedBus.QueueDeclare(queueName);
                _advancedBus.Bind(exchange, queue, queueName);
            }

            return queue;
        }

        private void DeleteQueueInternal(IQueue queue)
        {
            lock (_locker)
            {
                _advancedBus.QueueDelete(queue);
            }
        }

        private void HandleException(Exception ex, bool rethrow)
        {
            Trace.WriteLine(ex.Message);

            if (ex.InnerException != null)
            {
                Trace.WriteLine(ex.InnerException.Message);
            }

            if (ex is TimeoutException)
            {
                ex = new BusConnectionException(ex as TimeoutException);
            }

            if (rethrow)
                throw ex;
        }

        public void Dispose()
        {
            _bus.Dispose();
        }
    }

    public class BusConnectionException : Exception 
    {
        public BusConnectionException(TimeoutException innerException): base(null, innerException) {}

        public override string Message
        {
            get
            {
                return "A timeout occurred attempting to connect to the message bus. Your credentials may have been incorrect or the host may be unavailable.";
            }
        }
    }

    public class BusQueueDoesNotExistException : Exception
    {
        private string _queueName;

        public BusQueueDoesNotExistException(string queueName) : base(null) { _queueName = queueName; }

        public override string Message
        {
            get
            {
                return string.Format("The queue you are trying to connect to does not exist: {0} ", _queueName);
            }
        }

        public string QueueName { get { return _queueName; } set { _queueName = value; } }
    }

    public static class ExceptionHandling
    {
        public static void RetryForExceptionType<TExceptionType>(Action action, int numRetries, int retryWait)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            while (true)
            {
                try
                {
                    action();
                    return;
                }
                catch (Exception e)
                {
                    if (--numRetries <= 0 || !typeof(TExceptionType).IsAssignableFrom(e.GetType()))
                        throw;

                    if (retryWait > 0)
                        System.Threading.Thread.Sleep(retryWait);
                }
            }
        }
    }
}

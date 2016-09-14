using System;
using EasyNetQ;
using EasyNetQ.Topology;

namespace Charon.Messaging
{
    public interface IBusManager: IDisposable
    {
        IExchange EntitiesUpdatedFanoutExchange { get; }
        IExchange TopicExchange { get; }

        IQueue CreateQueue(IExchange exchange, string queueName);
        void DeleteQueue(IExchange exchange, string queueName, bool throwIfNotCached = true);
        IQueue GetQueue(IExchange exchange, string queueName, bool throwIfNotCached = true);

        void Publish<T>(IExchange exchange, T message) where T : class;
        void Publish<T>(IExchange exchange, string queueName, T message) where T : class;

        void Subscribe(IExchange exchange, string queueName, HandlerManager queueHandlerManager);
        void Subscribe<T>(IExchange exchange, string queueName, Action<T, MessageReceivedInfo> handler) where T : class;
    }
}
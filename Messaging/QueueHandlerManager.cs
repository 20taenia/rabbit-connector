using EasyNetQ;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Messaging
{
    public class HandlerManager
    {
        private ConcurrentDictionary<Type, IList> _handlersByType = new ConcurrentDictionary<Type, IList>();

        public void AddHandler<T>(QueueMessageHandler<T> handler)
        {
            IList list;

            //Types
            if (_handlersByType.TryGetValue(typeof(T), out list))
            {
                list.Add(handler);
            }
            else
            {
                var handlers = new List<QueueMessageHandler<T>>();
                handlers.Add(handler);
                list = handlers;
                _handlersByType.Add(typeof(T), list);
            }
        }

        public IList GetHandlersForType<T>(string queueName)
        {
            IList list;

            if (_handlersByType.TryGetValue(typeof(T), out list))
            {
                var result = (List<QueueMessageHandler<T>>)list;
                result = result.Where(x => x.QueueName == queueName).ToList();
                return result;
            }

            return null;
        }
    }

    public class QueueMessageHandler<T>
    {
        public Action<T, MessageReceivedInfo> Handler { get; set; }
        public string QueueName { get; set; }
    }

}

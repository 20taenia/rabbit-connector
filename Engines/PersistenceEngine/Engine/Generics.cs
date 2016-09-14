using Charon.Core.Entities;
using Charon.Core.Messaging;
using EasyNetQ;
using EasyNetQ.Topology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


//****** This is not in use - just keeping it for reference *****
namespace Charon.Engines.ProductEngine.Generics
{ 
    public delegate void ActionDelegate<T>(IMessage<EntityChangeRequest<T>> message, MessageReceivedInfo info) where T : EntityBase;

    public class Generics
    {
        IAdvancedBus _messageBusAdvanced = null;


        private void AttachConsumers()
        {
            IQueue productEntityUpdateQueue = null;

            //Attach to message bus for all o
            foreach (Type type in EntityTypes.ProductEntityTypes)
            {
                //Create needed generic types
                Type entityRequestType = typeof(EntityChangeRequest<>).MakeGenericType(new Type[] { type });
                Type messageType = typeof(IMessage<>).MakeGenericType(new Type[] { entityRequestType });
                Type genericMessageType = messageType.GetGenericTypeDefinition();
                Type delegateType = typeof(ActionDelegate<>).MakeGenericType(new Type[] { type });

                //Create our action delegate (called by the consume method on advanced message bus to process messages)
                MethodInfo consumeMethodInfo = GetType().GetGenericMethod(BindingFlags.Instance | BindingFlags.NonPublic, "OnEntityUpdate", new Type[] { genericMessageType, typeof(MessageReceivedInfo) }).MakeGenericMethod(type);
                Type actionType = typeof(Action<,>).MakeGenericType(new Type[] { messageType, typeof(MessageReceivedInfo) });
                Delegate actionDelegate = Delegate.CreateDelegate(actionType, this, consumeMethodInfo);

                //Set up call to consume method on advanced message bus
                Type genericActionType = actionType.GetGenericTypeDefinition();
                Type[] typeArgs = new Type[] { typeof(IQueue), genericActionType };
                var method = _messageBusAdvanced.GetType().GetGenericMethod("Consume", typeArgs);
                var methodRef = method.MakeGenericMethod(entityRequestType);
                object[] parameters = new object[] { productEntityUpdateQueue, actionDelegate };

                //Invoke consume method
                methodRef.Invoke(_messageBusAdvanced, parameters);
            }
        }
    }

    public static class TypeExtensions
    {
        public static MethodInfo GetGenericMethod(this Type type, BindingFlags bindingFlags, string name, params Type[] parameterTypes)
        {
            return type.GetMethods(bindingFlags)
                .Where(m => m.Name == name && parameterTypes.SequenceEqual(ParameterTypeProjection(m)))
                .SingleOrDefault();
        }

        public static MethodInfo GetGenericMethod(this Type type, string name, params Type[] parameterTypes)
        {
            return type.GetMethods()
                .Where(m => m.Name == name && parameterTypes.SequenceEqual(ParameterTypeProjection(m)))
                .SingleOrDefault();
        }

        private static IEnumerable<Type> ParameterTypeProjection(MethodInfo method)
        {
            List<Type> types = new List<Type>();

            foreach (var parameter in method.GetParameters())
            {
                if (parameter.ParameterType.IsGenericType)
                    types.Add(parameter.ParameterType.GetGenericTypeDefinition());
                else
                    types.Add(parameter.ParameterType);
            }
            return types;
        }
    }
}

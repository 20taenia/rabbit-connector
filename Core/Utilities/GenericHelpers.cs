using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Core.Utilities
{
    public static class GenericHelpers
    {
        public static async Task<object> InvokeGenericMethodAsync(object objectContainingMethod, Type genericType, Type returnType, string methodName, params object[] args)
        {
            var method = GetMethodInternal(objectContainingMethod, null, genericType, returnType, methodName, null, args);
            return await InvokeInternalAsync(method, objectContainingMethod, args);
        }

        public static async Task<object> InvokeGenericMethodAsync(object objectContainingMethod, Type genericType, Type returnType, string methodName, Type[] parameterTypes, params object[] args)
        {
            var method = GetMethodInternal(objectContainingMethod, null, genericType, returnType, methodName, parameterTypes, args);
            return await InvokeInternalAsync(method, objectContainingMethod, args);
        }

        public static object InvokeGenericMethod(object objectContainingMethod, Type genericType, Type returnType, string methodName, params object[] args)
        {
            var method = GetMethodInternal(objectContainingMethod, null, genericType, returnType, methodName, null, args);
            return InvokeInternal(method, objectContainingMethod, args);
        }

        public static object InvokeGenericStaticMethod(Type typeContainingMethod, Type genericType, Type returnType, string methodName, Type[] parameterTypes, params object[] args)
        {
            var method = GetMethodInternal(null, typeContainingMethod, genericType, returnType, methodName, parameterTypes, args);
            return InvokeInternal(method, null, args);
        }

        public static object InvokeGenericStaticMethod(Type typeContainingMethod, Type genericType, Type returnType, string methodName, params object[] args)
        {
            var method = GetMethodInternal(null, typeContainingMethod, genericType, returnType, methodName, null, args);
            return InvokeInternal(method, null, args);
        }

        private static MethodInfo GetMethodInternal(object objectContainingMethod, Type typeContainingMethod, Type genericType, Type returnType, string methodName, Type[] parameterTypes, params object[] args)
        {
            if (objectContainingMethod == null && typeContainingMethod == null)
                throw new ArgumentNullException("Both objectContainingMethod and typeContainingMethod cannot be null.");
            else if (typeContainingMethod == null)
                typeContainingMethod = objectContainingMethod.GetType();

            if (parameterTypes == null)
                parameterTypes = args.Select(x => x.GetType()).ToArray();

            var method = GetMethodExt(typeContainingMethod, methodName, returnType, parameterTypes);

            if (method == null)
                throw new ArgumentException(string.Format("{0} method not found for specified input and return types.", methodName));

            var genericMethod = method.MakeGenericMethod(genericType);
            return genericMethod;
        }

        private static object InvokeInternal(MethodInfo genericMethod, object objectContainingMethod, params object[] args)
        {
            return genericMethod.Invoke(objectContainingMethod, args);
        }

        private static async Task<object> InvokeInternalAsync(MethodInfo genericMethod, object objectContainingMethod, params object[] args)
        {
            return await (dynamic) genericMethod.Invoke(objectContainingMethod, args);
        }
    
        /// Search for a method by name and parameter types.  
        /// Unlike GetMethod(), does 'loose' matching on generic
        /// parameter types, and searches base interfaces.
        /// </summary>
        /// <exception cref="AmbiguousMatchException"/>
        public static MethodInfo GetMethodExt(this Type thisType,
                                                string name,
                                                Type returnType,
                                                params Type[] parameterTypes)
        {
            return GetMethodExt(thisType,
                                name,
                                BindingFlags.Instance
                                | BindingFlags.Static
                                | BindingFlags.Public
                                | BindingFlags.NonPublic
                                | BindingFlags.FlattenHierarchy,
                                returnType,
                                parameterTypes);
        }

        /// <summary>
        /// Search for a method by name, parameter types, and binding flags.  
        /// Unlike GetMethod(), does 'loose' matching on generic
        /// parameter types, and searches base interfaces.
        /// </summary>
        /// <exception cref="AmbiguousMatchException"/>
        public static MethodInfo GetMethodExt(this Type thisType,
                                                string name,
                                                BindingFlags bindingFlags,
                                                Type returnType,
                                                params Type[] parameterTypes)
        {
            MethodInfo matchingMethod = null;

            // Check all methods with the specified name, including in base classes
            GetMethodExt(ref matchingMethod, thisType, name, bindingFlags, returnType, parameterTypes);

            // If we're searching an interface, we have to manually search base interfaces
            if (matchingMethod == null && thisType.IsInterface)
            {
                foreach (Type interfaceType in thisType.GetInterfaces())
                    GetMethodExt(ref matchingMethod,
                                 interfaceType,
                                 name,
                                 bindingFlags,
                                 returnType,
                                 parameterTypes);
            }

            return matchingMethod;
        }

        private static void GetMethodExt(ref MethodInfo matchingMethod,
                                            Type type,
                                            string name,
                                            BindingFlags bindingFlags,
                                            Type returnType,
                                            params Type[] parameterTypes)
        {
            // Check all methods with the specified name, including in base classes
            foreach (MethodInfo methodInfo in type.GetMember(name,
                                                             MemberTypes.Method,
                                                             bindingFlags))
            {
                // Check that the parameter counts and types match, 
                // with 'loose' matching on generic parameters
                ParameterInfo[] parameterInfos = methodInfo.GetParameters();
                if (parameterInfos.Length == parameterTypes.Length)
                {
                    int i = 0;
                    for (; i < parameterInfos.Length; ++i)
                    {
                        if (!(parameterInfos[i].ParameterType.IsSimilarType(parameterTypes[i]) && (methodInfo.ReturnType.IsSimilarType(returnType)))) 
                            break;
                    }
                    if (i == parameterInfos.Length)
                    {
                        if (matchingMethod == null)
                            matchingMethod = methodInfo;
                        else
                            throw new AmbiguousMatchException(
                                   "More than one matching method found!");
                    }
                }
            }
        }

        /// <summary>
        /// Special type used to match any generic parameter type in GetMethodExt().
        /// </summary>
        public class T
        { }

        /// <summary>
        /// Determines if the two types are either identical, or are both generic 
        /// parameters or generic types with generic parameters in the same
        ///  locations (generic parameters match any other generic paramter,
        /// but NOT concrete types).
        /// </summary>
        private static bool IsSimilarType(this Type thisType, Type thatType)
        {
            // Ignore any 'ref' types
            if (thisType.IsByRef)
                thisType = thisType.GetElementType();
            if (thatType.IsByRef)
                thatType = thatType.GetElementType();

            // Handle array types
            if (thisType.IsArray && thatType.IsArray)
                return thisType.GetElementType().IsSimilarType(thatType.GetElementType());

            //Handle generic parameter contraints - currently only dealing with 1st generic param
            Type thisConstraint = null;
            Type thatConstraint = null;
            if (thisType.IsGenericParameter)
            {
                var thisConstraints = thisType.GetGenericParameterConstraints();

                if(thisConstraints.Count() > 0)
                    thisConstraint = thisConstraints.First();
            }

            if (thatType.IsGenericParameter)
            {
                var thatConstraints = thatType.GetGenericParameterConstraints();

                if (thatConstraints.Count() > 0)
                    thatConstraint = thatConstraints.First();
            }

            Type thisGenericType = null;
            Type thatGenericType = null;
            if (thisType.IsGenericType)
                thisGenericType = thisType.GetGenericTypeDefinition();
            if (thatType.IsGenericType)
                thatGenericType = thatType.GetGenericTypeDefinition();


            // If the types are identical, or they're both generic parameters 
            // or the special 'T' type, treat as a match
            if (thisType == thatType || 
                ((thisType.IsGenericParameter || thisType == typeof(T)) && (thatType.IsGenericParameter || thatType == typeof(T))) ||
                ((thisConstraint != null && thisConstraint.IsAssignableFrom(thatType)) || (thatConstraint != null && thatConstraint.IsAssignableFrom(thisType))) ||
                (thisType.IsGenericParameter && thisType.GetGenericParameterConstraints().Count() == 0 ) ||
                (thisType.IsGenericType && thatType.IsGenericType && thisGenericType == thatGenericType) ||
                ((thisType.IsInterface && thisType.IsAssignableFrom(thatType)) || thatType.IsInterface && thatType.IsAssignableFrom(thisType))) 
                return true;

            return false;
        }
    }

}

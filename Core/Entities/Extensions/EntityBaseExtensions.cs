using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Core.Entities
{
    public static class EntityBaseExtensions
    {
        public static bool IsAssignableFromEntityBase(this object input)
        {
            return input.GetType().IsAssignableFromEntityBase();
        }

        public static bool IsAssignableFromEntityBase(this Type inputType)
        {
            return typeof(EntityBase).IsAssignableFrom(inputType);
        }

        public static bool IsCollectionAssignableFromEntityBase(this object input)
        {
            return input.GetType().IsCollectionAssignableFromEntityBase();
        }

        public static bool IsCollectionAssignableFromEntityBase(this Type inputType)
        {
            if (inputType.IsGenericType)
            {
                var genericCollectionType = inputType.GetGenericTypeDefinition();
                var entityType = inputType.GetGenericArguments().Single();

                return IsAssignableFromEntityBase(entityType) &&
                    (typeof(ICollection).IsAssignableFrom(genericCollectionType) || typeof(ICollection<>).IsAssignableFrom(genericCollectionType));
            }

            return false;
        }
    }
}

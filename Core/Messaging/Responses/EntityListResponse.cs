using Charon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Charon.Core.Messaging
{
    public class EntityListResponse<T> : ResponseBase, IDTOListIncludeProperties, IDTOList where T : EntityBase
    {
        public List<T> Entities { get; set; }

        public string PrivateResponseQueue { get { return TopicQueues.ProductEntitiesListPrefix + "." + Id; } }
        public NavigationPropertiesHandle<T> NavigationProperties { get; set; }

        object[] IDTOList.Entities
        {
            get
            {
                if (Entities != null)
                    return Entities.ToArray();

                return null;
            }

            set
            {
                var entities = new List<T>();

                if (value != null && value.Count() > 0)
                {
                    foreach (var item in value)
                    {
                        var entity = item as T;
                        if (entity != null)
                        {
                            entities.Add(entity);
                        }
                    }
                }

                Entities = entities;
            }
        }

        object[] IDTOListIncludeProperties.IncludeProperties
        {
            get
            {
                var navProps = NavigationProperties.GetNavigationProperties();
                var list = new List<object>();

                if (navProps != null && navProps.Length > 0)
                {
                    foreach (var navProp in navProps)
                    {
                        list.Add(navProp);
                    }

                    return list.ToArray();
                }

                return null;
            }
        }

        IList<string> IDTOListIncludeProperties.Errors
        {
            get { return Errors; }
            set { Errors = value; }
        }
    }
}

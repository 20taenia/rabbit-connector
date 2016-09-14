using Charon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Charon.Core.Messaging
{
    public class EntityChangeRequest<T> : RequestBase, IPrivateResponseAvailable, IDTOUpdate where T : EntityBase
    {
        private bool _privateResponseRequested = false;

        public EntityChangeRequest() { }
        public EntityChangeRequest(Guid id) { Id = id; }

        public List<T> Entities { get; set; }

        public override string RequestQueue
        {
            get { return TopicQueues.ProductEntitiesUpdate; }
        }

        public string PrivateResponseQueue
        {
            get
            {
                if (Id == null)
                    throw new ArgumentNullException("Id");
                return TopicQueues.ProductEntitiesUpdatedPrefix + "." + Id;
            }
        }

        public bool PrivateResponseRequested
        {
            get { return _privateResponseRequested;  }
            set { _privateResponseRequested = value; }
        }

        object[] IDTOUpdate.Entities
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
    }
}

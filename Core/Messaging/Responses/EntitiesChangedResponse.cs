using Charon.Core.Entities;
using System;
using System.Collections.Generic;

namespace Charon.Core.Messaging
{
    public class EntitiesChangedResponse<T> : ResponseBase where T : EntityBase
    {
        public List<T> EntitiesUpdated { get; set; }
        public string PrivateResponseQueue { get { return TopicQueues.ProductEntitiesUpdatedPrefix + "." + Id; } }
    }
}

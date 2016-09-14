using Charon.Core.Entities;
using Charon.Core.Messaging;
using Charon.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Engines.PersistenceEngine
{
    public class EntityChangeRequestTask<T> : PersistenceRequestTaskBase where T : EntityBase
    {
        public EntityChangeRequestTask(BusManager busManager, RequestBase request) : base(busManager, request) { }

        public override void Execute()
        {
            var entityOperation = new EntityOperations<T>(_busManager);
            entityOperation.UpdateEntities(TypedRequest);
        }

        public EntityChangeRequest<T> TypedRequest
        {
            get { return _request as EntityChangeRequest<T>; }
        }
    }
}

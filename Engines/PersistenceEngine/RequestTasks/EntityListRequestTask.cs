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
    public class EntityListRequestTask<T> : PersistenceRequestTaskBase where T : EntityBase
    {
        public EntityListRequestTask(BusManager busManager, RequestBase request) : base(busManager, request) { }

        public override bool IsAsyncRequest
        {
            get { return false; }
        }

        public override void Execute()
        {
            var entityOperation = new EntityOperations<T>(_busManager);
            entityOperation.GetEntities(TypedRequest);
        }

        public EntityListRequest<T> TypedRequest
        {
            get { return _request as EntityListRequest<T>; }
        }
    }
}

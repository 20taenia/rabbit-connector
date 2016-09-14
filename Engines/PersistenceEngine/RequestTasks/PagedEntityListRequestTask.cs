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
    public class PagedEntityListRequestTask<T> : PersistenceRequestTaskBase where T : EntityBase
    {
        public PagedEntityListRequestTask(BusManager busManager, RequestBase request) : base(busManager, request) { }

        public override bool IsAsyncRequest
        {
            get { return false; }
        }

        public override void Execute()
        {
            var entityOperation = new EntityOperations<T>(_busManager);
            entityOperation.GetPagedEntities(TypedRequest);
        }

        public PagedEntityListRequest<T> TypedRequest
        {
            get { return _request as PagedEntityListRequest<T>; }
        }
    }
}

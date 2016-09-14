using Charon.Core.Entities;
using Charon.Engines.Common;
using Charon.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Engines.PersistenceEngine
{
    public abstract class PersistenceRequestTaskBase: IRequestTask
    {
        protected RequestBase _request = null;
        protected BusManager _busManager = null;

        public PersistenceRequestTaskBase(BusManager busManager, RequestBase request)
        {
            _busManager = busManager;
            _request = request;
        }

        public RequestBase Request
        {
            get { return _request; }
        }

        public virtual bool IsAsyncRequest
        {
            get { return true; }
        }

        public abstract void Execute();
    }
}

using Charon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Engines.Common
{
    public interface IRequestTask
    {
        void Execute();
        RequestBase Request { get; }
        bool IsAsyncRequest { get; }
    }
}

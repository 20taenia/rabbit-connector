using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Core.Entities
{
    public abstract class RequestBase: IRequestBase
    {
        protected Type _dtoType = null;

        public RequestBase() {}
        public RequestBase(Guid id) {Id = id; }

        public Guid Id { get; set; }
        public string User { get; set; }

        public abstract string RequestQueue { get; }
    }
}

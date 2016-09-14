using Charon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Core.Messaging
{
    public class PagedEntityListRequest<T> : EntityListRequest<T>, IPrivateResponseAvailable where T : EntityBase
    {
        public PagedEntityListRequest() : base() {}
        public PagedEntityListRequest(Guid id) : base(id) {}

        public int Page { get; set; }
        public int ItemsPerPage { get; set;  }
    }
}

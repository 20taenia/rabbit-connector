using Charon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Core.Entities
{
    public abstract class ResponseBase : IResponseBase
    {
        public ResponseBase()
        {
            Errors = new List<string>();
        }

        public Guid Id { get; set; }
        public ResponseStatus Status { get; set; }
        public IList<string> Errors { get; set; }
    }
}


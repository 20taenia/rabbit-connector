using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Core.Entities
{
    public interface IResponseBase
    {
        Guid Id { get; set; }
        IList<string> Errors { get; set; }
        ResponseStatus Status { get; set; }
    }
}

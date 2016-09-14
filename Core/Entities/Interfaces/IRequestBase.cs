using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Core.Entities
{
    public interface IRequestBase
    {
        Guid Id { get; }
        string User { get; set; }
    }
}

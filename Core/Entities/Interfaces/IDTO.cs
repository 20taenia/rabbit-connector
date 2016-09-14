using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Core.Entities
{
    public interface IDTOList
    {
        object[] Entities { get; set; }
    }

    public interface IDTOListIncludeProperties
    {
        object[] IncludeProperties { get; }
        IList<string> Errors { get; set; }
    }

    public interface IDTOUpdate
    {
        object[] Entities { get; set; }
    }
}

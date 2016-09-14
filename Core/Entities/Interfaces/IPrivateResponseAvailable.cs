using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Core.Entities
{
    public interface IPrivateResponseAvailable
    {
        bool PrivateResponseRequested { get; set; }
        string PrivateResponseQueue { get; }
    }
}

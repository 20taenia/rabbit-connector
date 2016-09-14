using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Core.Utilities
{
    public class TypeSwitch
    {
        Dictionary<Type, Action<object>> matches = new Dictionary<Type, Action<object>>();
        public TypeSwitch Case<R>(Action<R> action) { matches.Add(typeof(R), (x) => action((R)x)); return this; }
        public void SwitchByObject(object x) { matches[x.GetType()](x); }
        public void SwitchByType<X>() { matches[typeof(X)](default(X)); }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Core.Entities
{
    public class NavigationPropertiesHandle<T>
    {
        private Expression<Func<T, object>>[] _navigationProperties = null;

        public NavigationPropertiesHandle() { }
        public NavigationPropertiesHandle(Expression<Func<T, object>>[] navigationProperties) { _navigationProperties = navigationProperties; }

        public Expression<Func<T, object>>[] GetNavigationProperties()
        {
            return _navigationProperties;
        }
    }
}

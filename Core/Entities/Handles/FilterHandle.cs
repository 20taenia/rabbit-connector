using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Core.Entities
{
    public class FilterHandle<T>
    {
        private Expression<Func<T, bool>> _filter = null;

        public FilterHandle() {}
        public FilterHandle(Expression<Func<T, bool>> filter) { _filter = filter; }

        public Expression<Func<T, bool>> GetFilter()
        {
            return _filter;
        }
    }
}

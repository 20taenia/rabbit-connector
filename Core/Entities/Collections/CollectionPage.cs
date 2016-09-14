using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Core.Entities
{
    public class CollectionPage<T>
    {
        private IList<T> _items = null;

        public CollectionPage()
        {
            _items = new List<T>();
        }

        public IList<T> Items
        {
            get { return _items; }
            set { _items = value; }
        }
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }

        public int TotalPages()
        {
            return (int)(Math.Ceiling((Decimal)this.TotalItems / (Decimal)this.ItemsPerPage));
        }
    }
}

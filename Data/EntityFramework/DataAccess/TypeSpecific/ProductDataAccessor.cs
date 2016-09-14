using Charon.Core.Entities;
using Charon.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Data.DataAccess.TypeSpecific
{
    //This is not currently used.
    public class ProductDataAccessor: DataAccessor<Product>
    {
        public ProductDataAccessor(string user) : base (user) { }

        public override void InsertOrUpdate(List<Product> entities)
        {
            base.InsertOrUpdate(entities);
        }

        public Product GetByBarcode(string barcode)
        {
            return DataContext.Products.Where(p => p.Barcode == barcode).SingleOrDefault();
        }
    }
}

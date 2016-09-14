using System;
using System.Collections.Generic;

namespace Charon.Core.Entities
{
    public partial class ProductPhysicalAttribute: EntityBase
    {
        public ProductPhysicalAttribute()
        {
            ProductsPhysicalAttributes = new List<Product>();
        }

        public int PhysicalAttributeID { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public decimal? PackagingLength { get; set; }
        public decimal? PackagingWidth { get; set; }
        public decimal? PackagingHeight { get; set; }
        public decimal? Weight { get; set; }
        public decimal? PackagedWeight { get; set; }

        public virtual ICollection<Product> ProductsPhysicalAttributes { get; set; }

        public override object Key()
        {
            return PhysicalAttributeID;
        }
    }
}

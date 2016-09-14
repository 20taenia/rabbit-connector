using System.Collections.Generic;

namespace Charon.Core.Entities
{
    public partial class Product: EntityBase
    {
        public Product()
        {
            FactoryProducts = new List<FactoryProduct>();
            MarketplaceProducts = new List<MarketplaceProduct>();
            WarehouseProducts = new List<WarehouseProduct>();
        }

        public int ProductID { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OwnerID { get; set; }
        public int? PhysicalAttributeID { get; set; }
        public int? CategoryID { get; set; }
        public string ProductAttribute1 { get; set; }
        public string ProductAttribute2 { get; set; }
        public string ProductAttribute3 { get; set; }
        public string ProductAttribute4 { get; set; }
        public string ProductAttribute5 { get; set; }
        public string ProductAttribute6 { get; set; }
        public string ProductAttribute7 { get; set; }
        public string ProductAttribute8 { get; set; }
        public string ProductAttribute9 { get; set; }
        public string ProductAttribute10 { get; set; }
        public int? QuantityInBox { get; set; }
        public int? QuantityInnerBox { get; set; }
        public bool IsDiscontinued { get; set; }

        public virtual ICollection<FactoryProduct> FactoryProducts { get; set; }
        public virtual ICollection<MarketplaceProduct> MarketplaceProducts { get; set; }
        public virtual ICollection<WarehouseProduct> WarehouseProducts { get; set; }

        public virtual Owner Owner { get; set; }
        public virtual ProductPhysicalAttribute PhysicalAttribute { get; set; }
        public virtual ProductCategory Category { get; set; }

        public override object Key()
        {
            return ProductID;
        }
    }
}

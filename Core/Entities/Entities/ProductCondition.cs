using System.Collections.Generic;

namespace Charon.Core.Entities
{
    public partial class ProductCondition: EntityBase
    {
        public ProductCondition()
        {
            MarketplaceProductsConditions = new List<MarketplaceProduct>();
        }

        public int ProductConditionID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsNew { get; set; }
        public bool IsRefurbished { get; set; }
        public bool IsUsed { get; set; }

        public virtual ICollection<MarketplaceProduct> MarketplaceProductsConditions { get; set; }

        public override object Key()
        {
            return ProductConditionID;
        }
    }
}

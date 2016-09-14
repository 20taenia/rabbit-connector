using System.Collections.Generic;

namespace Charon.Core.Entities
{
    public partial class Warehouse: EntityBase
    {
        public Warehouse()
        {
            FulfilmentWarehouseMarketplaceProducts = new List<MarketplaceProduct>();
            WarehouseWarehouseProducts = new List<WarehouseProduct>();
            FBAWarehouseMarketplaces = new List<Marketplace>();
        }

        public int WarehouseID { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public bool IsCharonWarehouse { get; set; }
        public bool IsDistributionCentre { get; set; }
        public bool IsAmazonWarehouse { get; set; }
        public int WarehouseAddressID { get; set; }

        public virtual ICollection<MarketplaceProduct> FulfilmentWarehouseMarketplaceProducts { get; set;}
        public virtual ICollection<WarehouseProduct> WarehouseWarehouseProducts { get; set; }
        public virtual ICollection<Marketplace> FBAWarehouseMarketplaces { get; set; }
        public virtual Address WarehouseAddress { get; set; }

        public override object Key()
        {
            return WarehouseID;
        }
    }
}

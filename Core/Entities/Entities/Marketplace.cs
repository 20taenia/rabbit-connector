using System.Collections.Generic;

namespace Charon.Core.Entities
{
    public partial class Marketplace : EntityBase
    {
        public Marketplace()
        {
            MarketplaceProducts = new List<MarketplaceProduct>();
        }

        public int MarketplaceID { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public string LanguageID { get; set; }
        public string CountryID { get; set; }
        public string CurrencyID { get; set; }
        public int IntegrationTypeID { get; set; }
        public int? FBAWarehouseID { get; set; }

        public virtual ICollection<MarketplaceProduct> MarketplaceProducts { get; set; }
        public virtual Language Language { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Country Country { get; set; }
        public virtual IntegrationType IntegrationType { get; set; }
        public virtual Warehouse FBAWarehouse { get; set; }

        public override object Key()
        {
            return MarketplaceID;
        }
    }
}

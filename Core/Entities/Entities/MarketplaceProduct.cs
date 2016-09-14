using System;

namespace Charon.Core.Entities
{
    public partial class MarketplaceProduct: EntityBase
    {
        public int MarketplaceProductID { get; set; }
        public string Reference { get; set; }
        public string SKU { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? RecommendedRetailPrice { get; set; }
        public decimal? StrikeOutPrice { get; set; }
        public DateTime? StrikeOutStartDate { get; set; }
        public DateTime? StrikeOutEndDate { get; set; }

        public int ProductID { get; set; }
        public int MarketplaceID { get; set; }
        public int FulfilmentWarehouseID { get; set; }
        public int? ProductConditionID { get; set; }
        public int? VariationThemeID { get; set; }
        public int? ImageMedia1ID { get; set; }
        public int? ImageMedia2ID { get; set; }
        public int? ImageMedia3ID { get; set; }
        public int? ImageMedia4ID { get; set; }
        public int? ImageMedia5ID { get; set; }
        public int? ImageMedia6ID { get; set; }
        public int? ImageMedia7ID { get; set; }
        public int? ImageMedia8ID { get; set; }
        public int? ImageMedia9ID { get; set; }
        public int? ImageMedia10ID { get; set; }

        public string KeyFeature1 { get; set; }
        public string KeyFeature2 { get; set; }
        public string KeyFeature3 { get; set; }
        public string KeyFeature4 { get; set; }
        public string KeyFeature5 { get; set; }
        public string KeyFeature6 { get; set; }
        public string KeyFeature7 { get; set; }
        public string KeyFeature8 { get; set; }
        public string KeyFeature9 { get; set; }
        public string KeyFeature10 { get; set; }
        public string Keyword1 { get; set; }
        public string Keyword2 { get; set; }
        public string Keyword3 { get; set; }
        public string Keyword4 { get; set; }
        public string Keyword5 { get; set; }
        public string Keyword6 { get; set; }
        public string Keyword7 { get; set; }
        public string Keyword8 { get; set; }
        public string Keyword9 { get; set; }
        public string Keyword10 { get; set; }
        public string BrowseNode1 { get; set; }
        public string BrowseNode2 { get; set; }
        public string BrowseNode3 { get; set; }
        public string BrowseNode4 { get; set; }
        public string BrowseNode5 { get; set; }
        public string VariationTheme1 { get; set; }
        public string VariationTheme2 { get; set; }
        public string VariationTheme3 { get; set; }
        public string VariationTheme4 { get; set; }
        public string VariationTheme5 { get; set; }
        public bool DoNotResend { get; set; }

        public virtual Marketplace Marketplace { get; set; }
        public virtual Media ImageMedia1 { get; set; }
        public virtual Media ImageMedia2 { get; set; }
        public virtual Media ImageMedia3 { get; set; }
        public virtual Media ImageMedia4 { get; set; }
        public virtual Media ImageMedia5 { get; set; }
        public virtual Media ImageMedia6 { get; set; }
        public virtual Media ImageMedia7 { get; set; }
        public virtual Media ImageMedia8 { get; set; }
        public virtual Media ImageMedia9 { get; set; }
        public virtual Media ImageMedia10 { get; set; }
        public virtual ProductCondition ProductCondition { get; set; }
        public virtual Product Product { get; set; }
        public virtual Warehouse FulfilmentWarehouse { get; set; }
        public virtual VariationTheme VariationTheme { get; set; }

        public override object Key()
        {
            return MarketplaceProductID;
        }
    }
}

using System.Collections.Generic;

namespace Charon.Core.Entities
{
    public partial class VariationTheme:EntityBase
    {
        public VariationTheme()
        {
            MarketplaceProductsVariationThemes = new List<MarketplaceProduct>();
        }

        public int VariationThemeID { get; set; }
        public string Reference { get; set; }
        public string Title { get; set; }
        public string ProductType { get; set; }
        public string VariationThemeTypeName { get; set; }
        public string VariationTheme1Name { get; set; }
        public string VariationTheme2Name { get; set; }
        public string VariationTheme3Name { get; set; }
        public string VariationTheme4Name { get; set; }
        public string VariationTheme5Name { get; set; }

        public virtual ICollection<MarketplaceProduct> MarketplaceProductsVariationThemes { get; set; }
        public virtual Media VariationThemeMedia { get; set; }

        public override object Key()
        {
            return VariationThemeID;
        }
    }
}

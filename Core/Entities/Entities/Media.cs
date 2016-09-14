using System.Collections.Generic;

namespace Charon.Core.Entities
{
    public partial class Media: EntityBase
    {
        public Media()
        {
            FactoryProductsArtworkAIMedia = new List<FactoryProduct>();
            FactoryProductsArtworkPDFMedia = new List<FactoryProduct>();
            MarketplaceProductsImageMedia1 = new List<MarketplaceProduct>();
            MarketplaceProductsImageMedia2 = new List<MarketplaceProduct>();
            MarketplaceProductsImageMedia3 = new List<MarketplaceProduct>();
            MarketplaceProductsImageMedia4 = new List<MarketplaceProduct>();
            MarketplaceProductsImageMedia5 = new List<MarketplaceProduct>();
            MarketplaceProductsImageMedia6 = new List<MarketplaceProduct>();
            MarketplaceProductsImageMedia7 = new List<MarketplaceProduct>();
            MarketplaceProductsImageMedia8 = new List<MarketplaceProduct>();
            MarketplaceProductsImageMedia9 = new List<MarketplaceProduct>();
            MarketplaceProductsImageMedia10 = new List<MarketplaceProduct>();
            VariationThemesImageMedia = new List<VariationTheme>();
        }

        public int MediaID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string MimeType { get; set; }

        public virtual ICollection<FactoryProduct> FactoryProductsArtworkAIMedia { get; set; }
        public virtual ICollection<FactoryProduct> FactoryProductsArtworkPDFMedia { get; set; }
        public virtual ICollection<MarketplaceProduct> MarketplaceProductsImageMedia1 { get; set; }
        public virtual ICollection<MarketplaceProduct> MarketplaceProductsImageMedia2 { get; set; }
        public virtual ICollection<MarketplaceProduct> MarketplaceProductsImageMedia3 { get; set; }
        public virtual ICollection<MarketplaceProduct> MarketplaceProductsImageMedia4 { get; set; }
        public virtual ICollection<MarketplaceProduct> MarketplaceProductsImageMedia5 { get; set; }
        public virtual ICollection<MarketplaceProduct> MarketplaceProductsImageMedia6 { get; set; }
        public virtual ICollection<MarketplaceProduct> MarketplaceProductsImageMedia7 { get; set; }
        public virtual ICollection<MarketplaceProduct> MarketplaceProductsImageMedia8 { get; set; }
        public virtual ICollection<MarketplaceProduct> MarketplaceProductsImageMedia9 { get; set; }
        public virtual ICollection<MarketplaceProduct> MarketplaceProductsImageMedia10 { get; set; }
        public virtual ICollection<VariationTheme> VariationThemesImageMedia { get; set; }

        public override object Key()
        {
            return MediaID;
        }
    }
}

namespace Charon.Core.Entities
{
    public partial class FactoryProduct: EntityBase
    {
        public int FactoryProductID { get; set; }
        public int FactoryID { get; set; }
        public int ProductID { get; set; }
        public string Reference { get; set; }
        public decimal? ProductionCost { get; set; }
        public decimal? MaterialCost { get; set; }
        public decimal? LabourCost { get; set; }
        public decimal? CrossBorderCost { get; set; }
        public int? ManufacturingDuration { get; set; }
        public string CommodityDescription { get; set; }
        public int? MinimumOrderQuantity { get; set; }
        public bool IsDiscontinued { get; set; }

        public virtual Media ArtworkPDFMedia { get; set; }
        public virtual Media ArtworkAIMedia { get; set; }
        public virtual Product Product { get; set; }
        public virtual Factory Factory { get; set; }

        public override object Key()
        {
            return FactoryProductID;
        }
    }
}

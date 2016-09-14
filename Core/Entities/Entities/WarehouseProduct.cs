namespace Charon.Core.Entities
{
    public partial class WarehouseProduct: EntityBase
    {
        public int WarehouseProductID { get; set; }
        public int ProductID { get; set; }
        public int WarehouseID { get; set; }
        public int? DaysofBuffer { get; set; }
        public string HarmonisedTariffCode { get; set; }
        public decimal? SeaFreightCost { get; set; }
        public decimal? AirFreightCost { get; set; }
        public decimal? DutyCost { get; set; }

        public bool IsDiscontinued { get; set; }

        public virtual Product Product { get; set; }
        public virtual Warehouse Warehouse { get; set; }

        public override object Key()
        {
            return WarehouseProductID;
        }
    }
}

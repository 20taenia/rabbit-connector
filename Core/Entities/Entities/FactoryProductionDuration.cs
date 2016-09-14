namespace Charon.Core.Entities
{
    public partial class FactoryProductionDuration: EntityBase
    {
        public int ProductionDurationID { get; set; }
        public int NoOfUnits { get; set; }
        public int DaysToProduction { get; set; }

        public virtual Factory Factory { get; set; }

        public override object Key()
        {
            return ProductionDurationID;
        }
    }
}

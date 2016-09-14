using System.Collections.Generic;

namespace Charon.Core.Entities
{
    public partial class Factory: EntityBase
    {
        public Factory()
        {
            FactoryProductionDurations = new List<FactoryProductionDuration>();
            FactoryProducts = new List<FactoryProduct>();
        }

        public int FactoryID { get; set; }
        public string Reference { get; set; }
        public string Name { get; set; }
        public decimal? Deposit { get; set; }
        public int? FactoryAddressID { get; set; }

        public virtual Address FactoryAddress { get; set; }
        public virtual ICollection<FactoryProductionDuration> FactoryProductionDurations { get; set; }
        public virtual ICollection<FactoryProduct> FactoryProducts { get; set; }

        public override object Key()
        {
            return FactoryID;
        }
    }
}

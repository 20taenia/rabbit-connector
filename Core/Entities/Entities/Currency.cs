using System.Collections.Generic;

namespace Charon.Core.Entities
{
    public partial class Currency: EntityBase
    {
        public Currency()
        {
            Marketplaces = new List<Marketplace>();
        }

        public string CurrencyID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Marketplace> Marketplaces { get; set; }

        public override object Key()
        {
            return CurrencyID;
        }
    }
}

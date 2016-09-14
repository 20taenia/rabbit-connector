using System.Collections.Generic;

namespace Charon.Core.Entities
{
    public partial class StateProvince : EntityBase
    {
        public StateProvince()
        {
            Addresses = new List<Address>();
        }

        public int StateProvinceID { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual Country Country { get; set; }

        public override object Key()
        {
            return StateProvinceID;
        }
    }
}

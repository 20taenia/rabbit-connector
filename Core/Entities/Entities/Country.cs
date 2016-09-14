using System.Collections.Generic;

namespace Charon.Core.Entities
{
    public partial class Country: EntityBase
    {
        public Country()
        {
            Marketplaces = new List<Marketplace>();
            StateProvinces = new List<StateProvince>();
            Addresses = new List<Address>();
        }

        public string CountryID { get; set; }
        public string Name { get; set; }
        public string ISO3Code { get; set; }
        public short? NumericCode { get; set; }

        public virtual ICollection<Marketplace> Marketplaces { get; set; }
        public virtual ICollection<StateProvince> StateProvinces { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }

        public override object Key()
        {
            return CountryID;
        }
    }
}

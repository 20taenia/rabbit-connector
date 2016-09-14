using System.Collections.Generic;

namespace Charon.Core.Entities
{
    public partial class Address: EntityBase
    {
        public Address()
        {
            Factories = new List<Factory>();
            Warehouses = new List<Warehouse>();
        }

        public int AddressID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string ZipPostalCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? StateProvinceID { get; set; }
        public string CountryID { get; set; }

        public virtual StateProvince StateProvince { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Factory> Factories { get; set; }
        public virtual ICollection<Warehouse> Warehouses { get; set; }

        public override object Key()
        {
            return AddressID;
        }
    }
}

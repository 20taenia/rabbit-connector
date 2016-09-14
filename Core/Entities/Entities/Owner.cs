using System.Collections.Generic;

namespace Charon.Core.Entities
{
    public partial class Owner: EntityBase
    {
        public Owner()
        {
            Products = new List<Product>();
        }

        public int OwnerID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCharonCompany { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public override object Key()
        {
            return OwnerID;
        }
    }
}

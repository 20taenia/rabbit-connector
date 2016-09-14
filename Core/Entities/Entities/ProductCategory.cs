using System.Collections.Generic;

namespace Charon.Core.Entities
{
    public partial class ProductCategory: EntityBase
    {
        public ProductCategory()
        {
            CategoryProducts = new List<Product>();
        }

        public int ProductCategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductAttribute1Name { get; set; }
        public string ProductAttribute2Name { get; set; }
        public string ProductAttribute3Name { get; set; }
        public string ProductAttribute4Name { get; set; }
        public string ProductAttribute5Name { get; set; }
        public string ProductAttribute6Name { get; set; }
        public string ProductAttribute7Name { get; set; }
        public string ProductAttribute8Name { get; set; }
        public string ProductAttribute9Name { get; set; }
        public string ProductAttribute10Name { get; set; }
        public int? ParentCategoryID { get; set; }

        public virtual ProductCategory ParentCategory { get; set; }

        public virtual ICollection<Product> CategoryProducts { get; set; }

        public override object Key()
        {
            return ProductCategoryID;
        }
    }
}

using System.Collections.Generic;

namespace Charon.Core.Entities
{
    public partial class Language : EntityBase
    {
        public Language()
        {
            Marketplaces = new List<Marketplace>();
        }

        public string LanguageID { get; set; }
        public string NameEN { get; set; }
        public string NameFR { get; set; }
        public string NameDE { get; set; }

        public virtual ICollection<Marketplace> Marketplaces { get; set; }

        public override object Key()
        {
            return LanguageID;
        }
    }
}

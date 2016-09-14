using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Core.Entities
{
    public class IntegrationType: EntityBase
    {
        public IntegrationType()
        {
            Marketplaces = new List<Marketplace>();
        }

        public int IntegrationTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Marketplace> Marketplaces { get; set; }

        public override object Key()
        {
            return IntegrationTypeID;
        }
    }
}

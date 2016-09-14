using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Infrastructure.Services
{
    public class PersistenceServiceConfiguration : ConfigurationSection
    {
        private static PersistenceServiceConfiguration _configuration = ConfigurationManager.GetSection("persistenceServiceConfiguration") as PersistenceServiceConfiguration;

        public static PersistenceServiceConfiguration Configuration
        {
            get
            {
                return _configuration;
            }
        }

        [ConfigurationProperty("rabbitMQAddress", DefaultValue = "rabbit-hole.westeurope.cloudapp.azure.com", IsRequired = true)]
        public string RabbitMQAddress
        {
            get { return (string)this["rabbitMQAddress"]; }
            set { this["rabbitMQAddress"] = value; }
        }
    }
}

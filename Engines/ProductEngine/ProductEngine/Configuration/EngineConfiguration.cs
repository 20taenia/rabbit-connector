using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Engines.ProductEngine
{
    public class EngineConfiguration : ConfigurationSection
    {
        private static EngineConfiguration _configuration = ConfigurationManager.GetSection("engineConfiguration") as EngineConfiguration;

        public static EngineConfiguration Configuration
        {
            get
            {
                return _configuration;
            }
        }

        [ConfigurationProperty("asyncProcessingInterval", DefaultValue = 1000, IsRequired = true)]
        [IntegerValidator(MinValue = 1, MaxValue = 360000)]
        public int AsyncProcessingInterval
        {
            get { return (int)this["asyncProcessingInterval"]; }
            set { this["asyncProcessingInterval"] = value; }
        }

        [ConfigurationProperty("asyncProcessingThreadCount", DefaultValue = 10, IsRequired = true)]
        [IntegerValidator(MinValue = 1, MaxValue = 500)]
        public int AsyncProcessingThreadCount
        {
            get { return (int)this["asyncProcessingThreadCount"]; }
            set { this["asyncProcessingThreadCount"] = value; }
        }

        [ConfigurationProperty("queueDataTearDown", DefaultValue = false, IsRequired = true)]
        public bool QueueDataTearDown
        {
            get { return (bool)this["queueDataTearDown"]; }
            set { this["queueDataTearDown"] = value; }
        }

        [ConfigurationProperty("rabbitMQAddress", DefaultValue = "rabbit-hole.westeurope.cloudapp.azure.com", IsRequired = true)]
        public string RabbitMQAddress
        {
            get { return (string)this["rabbitMQAddress"]; }
            set { this["rabbitMQAddress"] = value; }
        }
    }
}

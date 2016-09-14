using System;
using System.Collections;
using System.Text;
using System.Configuration;
using System.Xml;

namespace Charon.Messaging
{
    public class RabbitMQConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("host")]
        public HostElement Host
        {
            get
            {
                return (HostElement)this["host"];
            }
            set
            { this["host"] = value; }
        }

        [ConfigurationProperty("port")]
        public PortElement Port
        {
            get
            {
                return (PortElement)this["port"];
            }
            set
            { this["port"] = value; }
        }

        [ConfigurationProperty("virtualhost")]
        public VirtualHostElement VirtualHost
        {
            get
            {
                return (VirtualHostElement)this["virtualhost"];
            }
            set
            { this["virtualhost"] = value; }
        }
    }

    public class HostElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return this["name"] as string;
            }
            set
            { this["name"] = value; }
        }
    }

    public class PortElement : ConfigurationElement
    {
        [ConfigurationProperty("number", IsRequired = true)]
        public int Number
        {
            get
            {
                int port;
                if (int.TryParse(this["number"].ToString(), out port))
                    return port;
                else
                    return -1;
            }
            set
            { this["number"] = value.ToString(); }
        }
    }

    public class VirtualHostElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return this["name"] as string;
            }
            set
            { this["name"] = value; }
        }
    }

}

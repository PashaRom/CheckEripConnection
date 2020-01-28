using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CheckERIPConnection
{
     public class Erip : ConfigurationSection
    {
        [ConfigurationProperty("networkAdapterName", IsRequired = true)]
        public string AdapterName
        {
            get
            {
                return (string)this["networkAdapterName"];
            }
        }
        [ConfigurationProperty("login", IsRequired = true)]
        public string Login
        {
            get
            {
                return (string)this["login"];
            }
        }

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get
            {
                return (string)this["password"];
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Infrustructure.Configuration
{
    public class BOLog : ConfigurationElement
    {
        [ConfigurationProperty("className", IsRequired = true)]
        public string ClassName
        {
            get
            {
                return (string)base["className"];
            }
            set
            {
                base["className"] = value;
            }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get
            {
                return (string)base["type"];
            }
            set
            {
                base["type"] = value;
            }
        }

        [ConfigurationProperty("module", IsRequired = true)]
        public string Module
        {
            get
            {
                return (string)base["module"];
            }
            set
            {
                base["module"] = value;
            }
        }

        [ConfigurationProperty("method", IsRequired = true)]
        public string Method
        {
            get
            {
                return (string)base["method"];
            }
            set
            {
                base["method"] = value;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Infrustructure.Configuration
{
    public class BOLogSettings : ConfigurationSection
    {
        private static BOLogSettings _sInstance;
        private static bool _sInstanceLoaded;

        public static BOLogSettings Instance
        {
            get
            {
                if (!_sInstanceLoaded)
                {
                    lock (typeof(BOLogSettings))
                    {
                        if (!_sInstanceLoaded)
                        {
                            _sInstance = ConfigurationManager.GetSection("boLogSettings") as BOLogSettings;
                            _sInstanceLoaded = true;
                        }
                    }
                }

                return _sInstance;
            }
        }

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public BOLogCollection BOLogs
        {
            get
            {
                return (BOLogCollection)base[""];
            }
        }
    }
}

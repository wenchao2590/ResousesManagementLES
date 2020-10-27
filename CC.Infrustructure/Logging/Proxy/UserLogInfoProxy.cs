using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrustructure.Logging.Proxy
{
    public class UserLogInfoProxy
    {
        public Guid Logid { get; set; }
        public string Plantcode { get; set; }
        public string Modulecode { get; set; }

        public string Userid { get; set; }
        public string Eventtype { get; set; }
        public string Eventlevel { get; set; }
        public DateTime OccurTime { get; set; }
        public string Msgcode { get; set; }

        public string Eventdescription { get; set; }

        public string Para1 { get; set; }

        public string Para2 { get; set; }

        public string Para3 { get; set; }
    }
}

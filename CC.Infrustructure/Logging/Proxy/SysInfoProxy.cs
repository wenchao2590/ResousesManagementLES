using System;

namespace Infrustructure.Logging.Proxy
{
    public class SysInfoProxy
    {
        public Guid Logid { get; set; }
        public string Plantcode { get; set; }
        public string Modulecode { get; set; }
        public string Eventtype { get; set; }
        public string Eventlevel { get; set; }
        public string Exceptioncode { get; set; }
        public DateTime OccurTime { get; set; }
        public string Eventname { get; set; }
        public string Eventdescription { get; set; }
        public string Para1 { get; set; }
        public string Para2 { get; set; }
        public string Para3 { get; set; }
    }
}

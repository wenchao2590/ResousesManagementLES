using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrustructure.Logging.Proxy
{
    public class InterfaceLogInfoProxy
    {
        public Guid LogID { get; set; }
        public Guid? OutSysCode { get; set; }
        public string EventType { get; set; }
        public string EventLevel { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string Creator { get; set; }
        public DateTime? CreateTime { get; set; }

    }
}

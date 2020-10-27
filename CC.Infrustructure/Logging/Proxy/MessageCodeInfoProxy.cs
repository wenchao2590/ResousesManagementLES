using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrustructure.Logging.Proxy
{
    public class MessageCodeInfoProxy
    {
        public string Msgcode { get; set; }
        public string Modulecode { get; set; }
        public string Msgtype { get; set; }
        public string Defaultlevel { get; set; }
        public string Chncontent { get; set; }
        public string Engcontent { get; set; }
        public string Parameter1 { get; set; }
        public string Parameter2 { get; set; }
        public string Parameter3 { get; set; }
        public string Creator { get; set; }
        public DateTime? Createtime { get; set; }
        public string Lastmodifier { get; set; }
        public DateTime? Lastmodifytime { get; set; }


    }
}

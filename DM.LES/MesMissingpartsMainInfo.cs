using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DM.LES
{
    public partial class MesMissingpartsMainInfo
    {
        [XmlElement("DTLS")]
        public List<MesMissingpartsDetailInfo> DTLS;

    }
}

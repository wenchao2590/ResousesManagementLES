using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace WS.QMIS.OutboundDataService
{
    [XmlRoot(Namespace = "")]
    public class AuthenticationToken : SoapHeader
    {
        public AuthenticationToken() { }
        public AuthenticationToken(string userName,string pass)
        {
            this.username = userName;
            this.password = pass;

        }

        public string username;
        public string password;
    }
}

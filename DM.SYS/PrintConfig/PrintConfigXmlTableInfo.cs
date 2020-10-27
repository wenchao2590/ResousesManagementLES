using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DM.SYS
{
    [XmlRoot("Table")]
    public class PrintConfigXmlTableInfo
    {
        //private string tableName;
        //private string printFileName;
        [XmlAttribute]
        public string TableName { get; set; }
        [XmlAttribute]
        public string PrintFileName { get; set; }
        [XmlAttribute]
        public string AutoPrintFlag { get; set; }
        [XmlAttribute]
        public int IsEspecial { get; set; }
    }
}

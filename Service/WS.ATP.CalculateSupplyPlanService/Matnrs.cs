using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DM.LES
{
    [XmlRoot("Matnrs")]//标记根节点的名字
    public class Matnrs
    {
        ///MATNR物料、BDMNG数量、AENNR更改单号
        ///EBORT工位、LIFNR供应商、PLATFORM平台


        public string Matnr { get; set; }
        public string Bdmng { get; set; }
        public string Aennr { get; set; }
        public string Ebort { get; set; }
        public string Lifnr { get; set; }
        public string Platform { get; set; }
    }
}

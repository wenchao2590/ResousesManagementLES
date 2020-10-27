using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DM.SYS
{
    [XmlRoot("Details")]
    public class PrintConfigXmlDetailsInfo
    {
        //List<PrintConfigXmlDetailInfo> printConfigXmlDetailInfos;
        //private string tableName;
        //private int maxRow;
        //private int maxColumn;
        //private bool fillEmpty;
        //private string relationFields;
        //private string templateLabel;
        public List<PrintConfigXmlDetailInfo> PrintConfigXmlDetailInfos { get ; set ; }
        [XmlAttribute]
        public int MaxRow { get ; set ; }
        [XmlAttribute]
        public string TableName { get ; set ; }
        [XmlAttribute]
        public bool FillEmpty { get ; set ; }
        [XmlAttribute]
        public string RelationFields { get ; set ; }
        [XmlAttribute]
        public int MaxColumn { get ; set ; }
        [XmlAttribute]
        public string TemplateLabel { get; set; }
    }
}

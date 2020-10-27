using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DM.SYS
{
    [XmlRoot("Detail")]
    public class PrintConfigXmlDetailInfo
    {
        //private string titleName;
        //private string fieldName;
        //private string stringFormat;
        //private int columnIndex;
        //private string templateLabel;
        [XmlAttribute]
        public string FieldName { get; set; }

        [XmlAttribute]
        public int ColumnIndex { get ; set ; }
        [XmlAttribute]
        public string TitleName { get ; set; }
        [XmlAttribute]
        public string StringFormat { get; set ; }
        [XmlAttribute]
        public string TemplateLabel { get ; set ; }
    }
}

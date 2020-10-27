namespace DM.SYS
{
    using System.Xml.Serialization;
    [XmlRoot("Field")]
    public class PrintConfigXmlFieldInfo
    {

        private string dataType;
        private string fieldName;
        private string stringFormat;
        private string excelRow;
        private string excelColumn;
        private string templateLabel;
        /// <summary>
        /// 数据类型
        /// </summary>
        [XmlAttribute]
        public string DataType { get => dataType; set => dataType = value; }
        /// <summary>
        /// 字段名称
        /// </summary>
        [XmlAttribute]
        public string FieldName { get => fieldName; set => fieldName = value; }
        /// <summary>
        /// 显示格式
        /// </summary>
        [XmlAttribute]
        public string StringFormat { get => stringFormat; set => stringFormat = value; }
        /// <summary>
        /// EXCEL行号
        /// </summary>
        [XmlAttribute]
        public string ExcelRow { get => excelRow; set => excelRow = value; }
        /// <summary>
        /// EXCEL列名
        /// </summary>
        [XmlAttribute]
        public string ExcelColumn { get => excelColumn; set => excelColumn = value; }
        /// <summary>
        /// 替换标识
        /// </summary>
        [XmlAttribute]
        public string TemplateLabel { get => templateLabel; set => templateLabel = value; }
    }
}

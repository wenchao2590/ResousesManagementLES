using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WS.VMI.OutboundDataService
{
    /// <summary>
    /// VMI 供货计划中间表
    /// </summary>
    [XmlRoot("DOC")]
    public class BFDAVmiSupplyPlanInfo
    {
        
        /// <summary>
        /// 供应商代码
        /// </summary>
        [XmlElement("SUPPLIERCODE")]
        public string SupplierCode;

        /// <summary>
        /// 物料编号
        /// </summary>
        [XmlElement("PARTNO")]
        public string PartNo;

        /// <summary>
        /// 需求日期
        /// </summary>
        [XmlElement("REQUIREDATE")]
        public string RequireDate;

        /// <summary>
        /// 数量
        /// </summary>
        [XmlElement("PARTQTY")]
        public decimal PartQty;

        /// <summary>
        /// VMI仓库代码
        /// </summary>
        [XmlElement("VMIWAREHOUSECODE")]
        public string VmiWarehouseCode;

        /// <summary>
        /// 工厂代码
        /// </summary>
        [XmlElement("WERKS")]
        public string Werks;

    }
}

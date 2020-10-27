using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WS.VMI.OutboundDataService
{

    /// <summary>
    /// LES-WMS-008	VMI供应商物料关系 LW005
    /// 对应中间表 TI_IFM_VMI_SUPPLIER_PART
    /// </summary>
    [XmlRoot("DOC")]
    public class BFDAVmiSupplierPartInfo
    {
        /// <summary>
        /// VMI仓库代码
        /// </summary>
        [XmlElement("VMIWAREHOUSECODE")]
        public string VmiWarehouseCode;

        /// <summary>
        ///  VMI仓库名称
        /// </summary>
        [XmlElement("VMIWAREHOUSENAME")]
        public string VmiWarehouseName;

        /// <summary>
        ///供应商代码
        /// </summary>
        [XmlElement("SUPPLIERCODE")]
        public string SupplierCode;

        /// <summary>
        /// 供应商名称
        /// </summary>
        [XmlElement("SUPPLIERNAME")]
        public string SupplierName;

        /// <summary>
        ///  物料编号
        /// </summary>
        [XmlElement("PARTNO")]
        public string PartNo;

        /// <summary>
        /// 物料中文描述
        /// </summary>
        [XmlElement("PARTCNAME")]
        public string PartCName;

        /// <summary>
        ///   删除标记
        /// </summary>
        [XmlElement("DELETEFLAG")]
        public string DeleteFlag;

        /// <summary>
        ///器具代码
        /// </summary>
        [XmlElement("CARTONCODE")]
        public string Cartoncode;

        /// <summary>
        /// 分装数量
        /// </summary>
        [XmlElement("CARTONQTY")]
        public string Cartonqty;

        /// <summary>
        /// 工厂代码
        /// </summary>
        [XmlElement("WERKS")]
        public string Werks;	
    }
}

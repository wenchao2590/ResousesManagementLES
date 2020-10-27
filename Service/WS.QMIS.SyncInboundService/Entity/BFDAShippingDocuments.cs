using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WS.QMIS.SyncInboundService
{
    /// <summary>
    /// 交货单据(子表)
    /// </summary>
    [XmlRoot("ShippingDocumentsList")]
    public class BFDAShippingDocuments
    {
        /// <summary>
        /// 物料编号
        /// </summary>
        public string PartNo;
        /// <summary>
        /// 物料数量
        /// </summary>
        public string PartQty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark;
    }
}

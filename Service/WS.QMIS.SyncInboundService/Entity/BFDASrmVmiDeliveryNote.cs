using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WS.QMIS.SyncInboundService
{
    /// <summary>
    /// 交付单
    /// </summary>
    public class BFDASrmVmiDeliveryNote
    {   
        /// <summary>
        /// 发货单号 1
        /// </summary>
        public string ShippingCode;
        /// <summary>
        /// 供应商代码 2
        /// </summary>
        public string SupplierCode;
        /// <summary>
        /// 到货时间 3
        /// </summary>
        public string DeliveryTime;
        /// <summary>
        /// VMI仓库代码 4
        /// </summary>
        public string VmiWarehouseCode;
        /// <summary>
        /// 交货单据(多条)
        /// </summary>
        public List<BFDAShippingDocuments> shippingDocumentsList;
    }

}

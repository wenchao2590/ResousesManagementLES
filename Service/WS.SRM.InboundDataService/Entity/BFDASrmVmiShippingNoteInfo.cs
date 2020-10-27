using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.SRM.InboundDataService
{
    /// <summary>
    /// SRM-LES-003 物料发货单 中间表  对应LES.TI_IFM_SRM_VMI_SHIPPING_NOTE 表
    /// </summary>
    public class BFDASrmShippingNoteInfo
    {

        

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string Plant;

        /// <summary>
        /// 是否删除
        /// </summary>
        public string DeleteFlag; 

        /// <summary>
        /// 发货单号
        /// </summary>
        public string ShippingCode;

        /// <summary>
        /// 供应商代码
        /// </summary>
        public string SupplierCode;

        /// <summary>
        /// 到货时间 20180522112305 年月日时分秒  14位
        /// </summary>
        public string DeliveryTime;

        /// <summary>
        /// VMI仓库代码
        /// </summary>
        public string VmiWarehouseCode;

      
       public List<BFDASrmVmiShippingNotePartInfo> listNotepartInfos;
    }
}
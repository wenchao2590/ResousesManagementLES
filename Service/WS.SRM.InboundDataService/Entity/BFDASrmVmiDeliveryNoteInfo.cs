using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.SRM.InboundDataService
{
    /// <summary>
    /// 物料送货单   IFM_SRM_VMI_DELIVERY_NOTE
    ///             BFDASrmVmiDeliveryNoteInfo
    /// </summary>
    public class BFDASrmVmiDeliveryNoteInfo
    {
        /// <summary>
        /// 送货单号
        /// </summary>
        public string OrderCode;
        /// <summary>
        /// 工厂
        /// </summary>
        public string Plant;

        /// <summary>
        /// 原始单据类型
        /// </summary>
        public string SourceOrderType;

        /// <summary>
        /// 道口
        /// </summary>
        public string Dock;

        /// <summary>
        /// 发单时间
        /// </summary>
        public string PublishTime;

        /// <summary>
        /// 供应商编码
        /// </summary>
        public string SupplierCode;
        
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName;

        /// <summary>
        /// 来源存储区
        /// </summary>
        public string SourceZoneNo;
        
        /// <summary>
        /// 目标存储区
        /// </summary>
        public string TargetZoneNo;

        /// <summary>
        /// 保管员
        /// </summary>
        public string Keeper;

        /// <summary>
        /// 预计发货时间
        /// </summary>
        public string PlanShippingTime;

        /// <summary>
        /// 预计到货时间
        /// </summary>
        public string PlanDeliveryTime;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark;

        /// <summary>
        /// 是否紧急
        /// </summary>
        public string EmergencyFlag;

        /// <summary>
        /// 删除标记
        /// </summary>
        public string DeleteFlag;

        /// <summary>
        /// 物料明细 List 集合
        /// </summary>
       public List<BFDASrmPartDetailInfo> DetailInfo;


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WS.VMI.OutboundDataService
{
    /// <summary>
    /// 排序拉动单中间表
    /// </summary>
    public class BFDAVmiJisPullOrderInfo
    {
        [XmlElement("WERKS")]
        public string Werks;

        [XmlElement("KEEPER")]
        public string Keeper;

        [XmlElement("ORDERCODE")]
        public string OrderCode;         ///拉动单号		

        [XmlElement("DOCK")]
        public string Dock;              ///道口 	

        [XmlElement("SEQUENCENUMBE")]
        public string SequenceNumbe;     ///当日单据顺序号

        [XmlElement("PUBLISHTIME")]
        public string PublishTime;       ///发单时间 		

        [XmlElement("PARTBOXCODE")]
        public string PartBoxCode;       ///零件类代码 		

        [XmlElement("PARTBOXNAME")]
        public string PartBoxName;       ///零件类名称		

        [XmlElement("SUPPLIERCODE")]
        public string SupplierCode;      ///供应商代码	
            
        [XmlElement("SUPPLIERNAME")]
        public string SupplierName;      ///供应商名称 	
            
        [XmlElement("SOURCEZONENO")]
        public string SourceZoneNo;      ///来源存储区代码	

        [XmlElement("TARGETZONENO")]
        public string TargetZoneNo;      ///目标存储区代码	

        [XmlElement("STARTINFOPOINTTIME")]
        public string StartInfoPointTime;///开始过点时间	

        [XmlElement("PLANDELIVERYTIME")]
        public string PlanDeliveryTime; ///预计到货时间	

        [XmlElement("STARTVEHICLESEQNO")]
        public string StartVehicleSeqNo;///开始车辆序号	

        [XmlElement("ENDVEHICLESEQNO")]
        public string EndVehicleSeqNo;  ///结束车辆序号	

        [XmlElement("LOCATION")]
        public string Location;         ///工位			

        [XmlElement("REMARK")]
        public string Remark;           ///备注		
            
        [XmlElement("DELETEFLAG")]
        public string DeleteFlag;       ///删除标记		

        [XmlElement("DTLS")]
        public BFDAVmiJisPullOrderDetailInfos OrderDetail;      /// 排序拉动单详情表



    }
}

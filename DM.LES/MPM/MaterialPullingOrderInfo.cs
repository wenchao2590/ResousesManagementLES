using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.LES
{
    public class MaterialPullingOrderInfo
    {
        /// <summary>
        /// 拉动单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 工厂代码
        /// </summary>
        public string Plant { get; set; }
        /// <summary>
        /// 供应商代码
        /// </summary>
        public string SupplierNum { get; set; }
        /// <summary>
        /// 来源存储区代码
        /// </summary>
        public string SourceZoneNo { get; set; }
        /// <summary>
        /// 保管员
        /// </summary>
        public string Keeper { get; set; }
        /// <summary>
        /// 目标存储区代码
        /// </summary>
        public string TargetZoneNo { get; set; }
        /// <summary>
        /// 目标道口
        /// </summary>
        public string TargetDock { get; set; }
        /// <summary>
        /// 零件类代码
        /// </summary>
        public string PartBoxCode { get; set; }
        /// <summary>
        /// 单据类型
        /// </summary>
        public int OrderType { get; set; }
        /// <summary>
        /// 发单时间
        /// </summary>
        public DateTime? PublishTime { get; set; }
        /// <summary>
        /// 零件类名称
        /// </summary>
        public string PartBoxName { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }
        /// <summary>
        /// 供应商类型
        /// </summary>
        public int SupplierType { get; set; }
        /// <summary>
        /// 预计发货时间
        /// </summary>
        public DateTime? PlanShippingTime { get; set; }
        /// <summary>
        /// 预计到货时间
        /// </summary>
        public DateTime? PlanDeliveryTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// 是否允许编辑ASN
        /// </summary>
        public bool AsnFlag { get; set; }
        /// <summary>
        /// 是否紧急
        /// </summary>
        public bool? EmergencyFlag { get; set; }
        /// <summary>
        /// 是否检验
        /// </summary>
        public bool? InspectFlag { get; set; }
        /// <summary>
        /// 明细List
        /// </summary>
        public List<MaterialPullingOrderDetailInfo> MaterialPullingOrderDetailInfos { get; set; }
        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        public bool ValidFlag { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 目标仓库编码
        /// </summary>
        public string TargetWmNo { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 来源仓库代码
        /// </summary>
        public string SourceWmNo { get; set; }
        /// <summary>
        /// 来源理货员
        /// </summary>
        public string SourceKeeper { get; set; }
        /// <summary>
        /// 流水线代码
        /// </summary>
        public string AssemblyLine { get; set; }
        /// <summary>
        /// 车间代码
        /// </summary>
        public string Workshop { get; set; }
        /// <summary>
        /// 送货路径
        /// </summary>
        public string Route { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        public int ProcessFlag { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? ProcessTime { get; set; }
        /// <summary>
        /// 拉动方式
        /// </summary>
        public int PullMode { get; set; }
        /// <summary>
        /// 工段
        /// </summary>
        public string WorkshopSection { get; set; }
        /// <summary>
        /// 工位
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 拉动单号
        /// </summary>
        public string RunsheetNo { get; set; }
        /// <summary>
        /// 开始车号
        /// </summary>
        public int? StartVehicheNo { get; set; }
        /// <summary>
        /// 结束车号
        /// </summary>
        public int? EndVehicheNo { get; set; }
        /// <summary>
        /// 包装编号
        /// </summary>
        public string PackageModel { get; set; }
        /// <summary>
        /// 单包装数量
        /// </summary>
        public decimal? Package { get; set; }
        /// <summary>
        /// 车型编号
        /// </summary>
        public string VehicheModelNo { get; set; }
        /// <summary>
        /// 当日车辆顺序号
        /// </summary>
        public int? DayVehicheSeqNo { get; set; }

        /// <summary>
        /// 生产单号
        /// </summary>
        public string ProduceNo { get; set; }
        /// <summary>
        /// 入库单创建来源类型
        /// </summary>
        public int SourceCreateType { get; set; }
    }
}

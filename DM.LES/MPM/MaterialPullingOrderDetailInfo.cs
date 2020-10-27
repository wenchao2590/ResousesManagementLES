using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.LES
{
    public class MaterialPullingOrderDetailInfo
    {
        /// <summary>
        /// 拉动单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        ///物料号
        /// </summary>
        public string PartNo { get; set; }
        /// <summary>
        /// 工厂代码
        /// </summary>
        public string Plant { get; set; }
        /// <summary>
        /// 供应商代码
        /// </summary>
        public string SupplierNum { get; set; }
        /// <summary>
        /// 物料中文描述
        /// </summary>
        public string PartCname { get; set; }
        /// <summary>
        /// 单包装数
        /// </summary>
        public decimal PackageQty { get; set; }
        /// <summary>
        /// 需求数量
        /// </summary>
        public decimal RequirePartQty { get; set; }
        /// <summary>
        /// 包装型号
        /// </summary>
        public string PackageModel { get; set; }
        /// <summary>
        /// 目标仓库编码
        /// </summary>
        public string TargetWmNo { get; set; }
        /// <summary>
        /// 目标存储区代码
        /// </summary>
        public string TargetZoneNo { get; set; }
        /// <summary>
        /// 目标库位代码
        /// </summary>
        public string TargetDloc { get; set; }
        /// <summary>
        /// 物料英文描述
        /// </summary>
        public string PartEname { get; set; }
        /// <summary>
        /// 目标道口
        /// </summary>
        public string TargetDock { get; set; }
        /// <summary>
        /// 零件类代码
        /// </summary>
        public string PartBoxCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// 来源仓库代码
        /// </summary>
        public string SourceWmNo { get; set; }
        /// <summary>
        /// 来源存储区
        /// </summary>
        public string SourceZoneNo { get; set; }
        /// <summary>
        /// 来源库位代码
        /// </summary>
        public string SourceDloc { get; set; }
        /// <summary>
        /// 需求包装数
        /// </summary>
        public int RequirePackageQty { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string Uom { get; set; }
        /// <summary>
        /// 是否检验
        /// </summary>
        public int InspectMode { get; set; }
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
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }
        /// <summary>
        /// 已冻结库存标记
        /// </summary>
        public bool FrozenStockFlag { get; set; }
        /// <summary>
        /// 工段代码
        /// </summary>
        public string WorkshopSection { get; set; }
        /// <summary>
        /// 工位代码
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 拉动单号
        /// </summary>
        public string RunsheetNo { get; set; }
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
        /// VIN
        /// </summary>
        public string Vin { get; set; }
    }
}

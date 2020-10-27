using DAL.LES;
using DAL.SYS;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BLL.LES
{
    /// <summary>
    /// OutputDetailBLL
    /// </summary>
    public class OutputDetailBLL
    {
        #region Common
        /// <summary>
        /// OutputDetailDAL
        /// </summary>
        OutputDetailDAL dal = new OutputDetailDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<OutputDetailInfo></returns>
        public List<OutputDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OutputDetailInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(OutputDetailInfo info)
        {
            OutputInfo outputInfo = new OutputDAL().GetInfo(info.OutputFid.GetValueOrDefault());
            if (outputInfo == null)
                throw new Exception("MC:0x00000084");///数据错误
            //if (outputInfo.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Created)
            //    throw new Exception("MC:0x00000137");///出库单为已创建状态时才能添加材料

            if (dal.GetList("[OUTPUT_FID] = N'" + info.OutputFid + "' and [PART_NO] = N'" + info.PartNo + "' and [SUPPLIER_NUM] = N'" + info.SupplierNum + "'", string.Empty).Count > 0)
                throw new Exception("MC:0x00000464");///同物料号供应商不能一致

            ///出库单是否按供应商类型校验
            string outputOrderValidSupplierTypeFlag = new ConfigDAL().GetValueByCode("OUTPUT_ORDER_VALID_SUPPLIER_TYPE_FLAG");
            if (outputOrderValidSupplierTypeFlag.ToLower() == "true")
            {
                ///储运供应商
                if (outputInfo.SupplierType.GetValueOrDefault() == (int)SupplierTypeConstants.LogisticsSupplier)
                {
                    int cnt = dal.GetCounts("[PART_NO] = N'" + info.PartNo + "' and [SUPPLIER_NUM] = N'" + info.SupplierNum + "' and [OUTPUT_FID] = N'" + info.OutputFid.GetValueOrDefault() + "'");
                    if (cnt > 0)
                        throw new Exception("MC:0x00000142");///同一出库单下不能出现相同供应商的物料编码
                }
                ///物料供应商
                if (outputInfo.SupplierType.GetValueOrDefault() == (int)SupplierTypeConstants.MaterialSupplier)
                {
                    if (info.SupplierNum != outputInfo.SupplierNum)
                        throw new Exception("MC:0x00000143");///出库单的物料供应商与出库明细的供应商不一致

                    int cnt = dal.GetCounts("[PART_NO] = N'" + info.PartNo + "' and [OUTPUT_FID] = N'" + info.OutputFid.GetValueOrDefault() + "'");
                    if (cnt > 0)
                        throw new Exception("MC:0x00000144");///同一出库单下不能出现相同的物料编码
                }
                ///其它情况目前则为内部移库的情况，没有供应商只有来源和目标仓库存储区
            }
            ///创建出库单时实发数量等于需求数量
            string createOutputActualQtyEqualsRequired = new ConfigDAL().GetValueByCode("CREATE_OUTPUT_ACTUAL_QTY_EQUALS_REQUIRED");
            if (createOutputActualQtyEqualsRequired.ToLower() == "true")
            {
                if (info.ActualQty == null) info.ActualQty = info.RequiredQty;
            }
            ///有单包装数量时需要计算
            if (info.Package.GetValueOrDefault() > 0)
            {
                if (info.RequiredQty.GetValueOrDefault() > 0)
                    info.RequiredBoxNum = decimal.ToInt32(info.RequiredQty.GetValueOrDefault() / info.Package.GetValueOrDefault());
                else
                    info.RequiredBoxNum = null;

                if (info.ActualQty.GetValueOrDefault() > 0)
                    info.ActualBoxNum = decimal.ToInt32(info.ActualQty.GetValueOrDefault() / info.Package.GetValueOrDefault());
                else
                    info.ActualBoxNum = null;
            }
            ///明细中供应商缺失时，从单据上获取
            if (string.IsNullOrEmpty(info.SupplierNum)) info.SupplierNum = outputInfo.SupplierNum;
            ///单据号
            if (string.IsNullOrEmpty(info.TranNo)) info.TranNo = outputInfo.OutputNo;
            ///拉动单号
            if (string.IsNullOrEmpty(info.RunsheetNo)) info.RunsheetNo = outputInfo.RunsheetNo;
            ///工厂
            if (string.IsNullOrEmpty(info.Plant)) info.Plant = outputInfo.Plant;
            ///仓库
            if (string.IsNullOrEmpty(info.WmNo)) info.WmNo = outputInfo.WmNo;
            if (string.IsNullOrEmpty(info.TargetWm)) info.TargetWm = outputInfo.WmNo;
            ///存储区
            if (string.IsNullOrEmpty(info.ZoneNo)) info.ZoneNo = outputInfo.ZoneNo;
            if (string.IsNullOrEmpty(info.TargetZone)) info.TargetZone = outputInfo.ZoneNo;
            ///
            return dal.Add(info);
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            int cnt = new OutputDAL().GetCounts("[STATUS] = " + (int)WmmOrderStatusConstants.Created + " and [FID] in (select [OUTPUT_FID] from [LES].[TT_WMM_OUTPUT_DETAIL] with(nolock) where [VALID_FLAG] = 1 and [ID] = " + id + ")");
            if (cnt == 0)
                throw new Exception("MC:0x00000145");///出库单状态为已创建时才能删除材料

            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            string outputFid = CommonBLL.GetFieldValue(fields, "OUTPUT_FID");
            int cnt = new OutputDAL().GetCounts("[STATUS] = " + (int)WmmOrderStatusConstants.Created + " and [FID] = N'" + outputFid + "'");
            OutputInfo outputInfo = new OutputDAL().GetInfo(Guid.Parse(outputFid));
            if (outputInfo == null)
                throw new Exception("MC:0x00000084");///出库单数据错误

            //if (outputInfo.Status == (int)WmmOrderStatusConstants.Completed
            //    || outputInfo.Status == (int)WmmOrderStatusConstants.Closed)
            //    throw new Exception("MC:0x00000412");///出库单状态为已关闭或已完成时不能修改其内容

            //if (outputInfo.Status == (int)WmmOrderStatusConstants.Published)
            //{
                OutputDetailInfo outputDetailInfo = dal.GetInfo(id);
                ///实收数量
                string actualQty = CommonBLL.GetFieldValue(fields, "ACTUAL_QTY");
                if (string.IsNullOrEmpty(actualQty)) actualQty = "NULL";
                if (Convert.ToInt32(actualQty) > outputDetailInfo.RequiredQty)
                    throw new Exception("MC:0x00000424");///实收数不能大于需求数

                ///实收箱数
                string actualBoxNum = CommonBLL.GetFieldValue(fields, "ACTUAL_BOX_NUM");
                if (string.IsNullOrEmpty(actualBoxNum)) actualBoxNum = "NULL";
                ///修改用户
                string loginUser = CommonBLL.GetFieldValue(fields, "MODIFY_USER");

                fields = "[ACTUAL_QTY] = " + actualQty + ",[ACTUAL_BOX_NUM] = " + actualBoxNum + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' ";

            //}
            ///出库单是否按供应商类型校验
            string outputOrderValidSupplierTypeFlag = new ConfigDAL().GetValueByCode("OUTPUT_ORDER_VALID_SUPPLIER_TYPE_FLAG");
            if (outputOrderValidSupplierTypeFlag.ToLower() == "true")
            {
                string supplierNum = CommonBLL.GetFieldValue(fields, "SUPPLIER_NUM");
                string partNo = CommonBLL.GetFieldValue(fields, "PART_NO");
                ///需要校验入库单的供应商是否为储运供应商
                cnt = new SupplierDAL().GetCounts("[SUPPLIER_TYPE] = " + (int)SupplierTypeConstants.LogisticsSupplier + " and [SUPPLIER_NUM] in (select [SUPPLIER_NUM] from [LES].[TT_WMM_OUTPUT] with(nolock) where [FID] = N'" + outputFid + "' and [VALID_FLAG] = 1)");

                ///储运供应商标记
                bool logisticsSupplierFlag = cnt == 0 ? false : true;
                if (logisticsSupplierFlag)
                {
                    cnt = dal.GetCounts("[ID] <> " + id + " and [PART_NO] = N'" + partNo + "' and [SUPPLIER_NUM] = N'" + supplierNum + "' and [OUTPUT_FID] = N'" + outputFid + "'");
                    if (cnt > 0)
                        throw new Exception("MC:0x00000142");///同一出库单下不能出现相同供应商的物料编码
                }
                else
                {
                    ///出库单明细与出库单不是同一家供应商
                    cnt = new OutputDAL().GetCounts("([SUPPLIER_NUM] = N'" + supplierNum + "' or len(isnull([SUPPLIER_NUM],'')) = 0) and [FID] = N'" + outputFid + "'");
                    if (cnt == 0)
                        throw new Exception("MC:0x00000143");///出库单的物料供应商与出库明细的供应商不一致

                    cnt = dal.GetCounts("[ID] <> " + id + " and [PART_NO] = N'" + partNo + "' and [OUTPUT_FID] = N'" + outputFid + "'");
                    if (cnt > 0)
                        throw new Exception("MC:0x00000144");///同一出库单下不能出现相同的物料编码
                }
            }

            using (TransactionScope trans = new TransactionScope())
            {
                if (dal.UpdateInfo(fields, id) == 0)
                    return false;
                CommonDAL.ExecuteNonQueryBySql("update [LES].[TT_WMM_OUTPUT] "
                    + "set [SUM_PART_QTY] = (select sum([ACTUAL_QTY]) from [LES].[TT_WMM_OUTPUT_DETAIL] with(nolock) where [OUTPUT_FID] = N'" + outputFid + "' and [VALID_FLAG] = 1)"
                    + ",[SUM_OF_PRICE] = (select sum([PART_PRICE]) from [LES].[TT_WMM_OUTPUT_DETAIL] with(nolock) where [OUTPUT_FID] = N'" + outputFid + "' and [VALID_FLAG] = 1) "
                    + "where [FID] = N'" + outputFid + "'");
                trans.Complete();
            }
            return true;
        }
        #endregion

        #region Interface
        /// <summary>
        /// Create OutputDetailInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>OutputDetailInfo</returns>
        public static OutputDetailInfo CreateOutputDetailInfo(string loginUser)
        {
            OutputDetailInfo info = new OutputDetailInfo();
            ///FID,
            info.Fid = Guid.NewGuid();
            ///VALID_FLAG,数据有效标记
            info.ValidFlag = true;
            ///CREATE_USER,创建用户
            info.CreateUser = loginUser;
            ///CREATE_DATE,创建时间
            info.CreateDate = DateTime.Now;
            return info;
        }
        /// <summary>
        /// PartsStockInfo -> OutputDetailInfo
        /// </summary>
        /// <param name="partsStockInfo"></param>
        /// <param name="info"></param>
        public static void GetOutputDetailInfo(PartsStockInfo partsStockInfo, ref OutputDetailInfo info)
        {
            if (partsStockInfo == null) return;
            ///TARGET_DLOC,目的库位
            info.TargetDloc = partsStockInfo.Dloc;
            ///PART_CNAME,车辆模型_零件中文名
            info.PartCname = partsStockInfo.PartCname;
            ///PACKAGE,单包装数
            info.Package = partsStockInfo.InboundPackage;
            ///PACKAGE_MODEL,包装型号
            info.PackageModel = partsStockInfo.InboundPackageModel;
            ///MEASURING_UNIT_NO,单位
            info.MeasuringUnitNo = partsStockInfo.PartUnits;
            ///PART_ENAME,车辆模型_零件德文名
            info.PartEname = partsStockInfo.PartEname;
            ///INHOUSE_PACKAGE,上线包装数量
            info.InhousePackage = partsStockInfo.InhousePackage;
            ///INHOUSE_PACKAGE_MODEL,上线包装型号
            info.InhousePackageModel = partsStockInfo.InhousePackageModel;
            ///PART_CLS,零件类别
            info.PartCls = partsStockInfo.PartCls;
        }
        /// <summary>
        /// OutputInfo -> OutputDetailInfo
        /// </summary>
        /// <param name="outputInfo"></param>
        /// <param name="info"></param>
        public static void GetOutputDetailInfo(OutputInfo outputInfo, ref OutputDetailInfo info)
        {
            if (outputInfo == null) return;
            ///OUTPUT_FID,出库单外键
            info.OutputFid = Guid.Parse( outputInfo.OutputId.ToString());
            ///PLANT,工厂模型_工厂
            info.Plant = outputInfo.PlanNo;
            ///WM_NO,仓库编码
            info.WmNo = outputInfo.WmNo;
            ///ZONE_NO,存贮区编码
            info.ZoneNo = outputInfo.ZoneNo;
            ///TRAN_NO,交易编码
            info.TranNo = outputInfo.OutputNo;
            ///TARGET_WM,目的仓库
            info.TargetWm = outputInfo.WmNo;
            ///TARGET_ZONE,目的存贮区
            info.TargetZone = outputInfo.ZoneNo;
            ///DOCK,工厂模型_DOCK
            //info.Dock = outputInfo.TDock;
            ///ASSEMBLY_LINE,工厂模型_流水线
            info.AssemblyLine = outputInfo.AssemblyLine;
            ///BOX_PARTS,基础数据_零件类
           // info.BoxParts = outputInfo.PartBoxCode;
        }
        /// <summary>
        /// MaintainPartsInfo -> OutputDetailInfo
        /// </summary>
        /// <param name="maintainPartsInfo"></param>
        /// <param name="info"></param>
        public static void GetOutputDetailInfo(MaintainPartsInfo maintainPartsInfo, ref OutputDetailInfo info)
        {
            if (maintainPartsInfo == null) return;
            ///ORIGIN_PLACE,产地
           // info.OriginPlace = maintainPartsInfo.OriginPlace;
            ///SALE_UNIT_PRICE,销售单价
           // info.SaleUnitPrice = maintainPartsInfo.SaleUnitPrice;
        }
        /// <summary>
        /// PackageApplianceInfo -> OutputDetailInfo
        /// </summary>
        /// <param name="packageApplianceInfo"></param>
        /// <param name="info"></param>
        public static void GetOutputDetailInfo(PackageApplianceInfo packageApplianceInfo, ref OutputDetailInfo info)
        {
            if (packageApplianceInfo == null) return;
            ///PACKAGE_LENGTH,包装长
            info.PackageLength = packageApplianceInfo.PackageLength;
            ///PACKAGE_WIDTH,包装宽
            info.PackageWidth = packageApplianceInfo.PackageWidth;
            ///PACKAGE_HEIGHT,包装高
            info.PackageHeight = packageApplianceInfo.PackageHeight;
            ///PERPACKAGE_GROSS_WEIGHT,单包装毛重
            info.PerpackageGrossWeight = packageApplianceInfo.MaxWeight;
        }
        /// <summary>
        /// OutputDetailInfo
        /// </summary>
        /// <param name="info"></param>
        public static void GetOutputDetailInfo(ref OutputDetailInfo info)
        {
            ///PACKAGE_VOLUME,单包装体积
            info.PackageVolume = info.PackageLength.GetValueOrDefault() * info.PackageWidth.GetValueOrDefault() * info.PackageHeight.GetValueOrDefault();
            ///PART_PRICE,物料金额
            info.PartPrice = info.SaleUnitPrice.GetValueOrDefault() * info.RequiredQty.GetValueOrDefault();
            ///SUM_WEIGHT,合计毛重
            info.SumWeight = info.PerpackageGrossWeight.GetValueOrDefault() * info.RequiredQty.GetValueOrDefault();
            ///SUM_VOLUME,合计体积
            info.SumVolume = info.PackageVolume.GetValueOrDefault() * info.RequiredQty.GetValueOrDefault();
            ///REQUIRED_BOX_NUM,需求包装数
            if (info.Package.GetValueOrDefault() > 0)
                info.RequiredBoxNum = Convert.ToInt32(Math.Ceiling(info.RequiredQty.GetValueOrDefault() / info.Package.GetValueOrDefault()));
        }
        /// <summary>
        /// VmiOutputDetailInfo -> OutputDetailInfo
        /// </summary>
        /// <param name="vmiOutputDetailInfo"></param>
        /// <param name="info"></param>
        public static void GetOutputDetailInfo(VmiOutputDetailInfo vmiOutputDetailInfo, ref OutputDetailInfo info)
        {
            if (vmiOutputDetailInfo == null) return;
            ///ID,出库单明细ID
            info.Id = vmiOutputDetailInfo.Id;
            ///FID,
            info.Fid = vmiOutputDetailInfo.Fid;
            ///OUTPUT_FID,出库单外键
            info.OutputFid = vmiOutputDetailInfo.OutputFid;
            ///PLANT,工厂模型_工厂
            info.Plant = vmiOutputDetailInfo.Plant;
            ///SUPPLIER_NUM,基础数据_供应商
            info.SupplierNum = vmiOutputDetailInfo.SupplierNum;
            ///WM_NO,仓库编码
            info.WmNo = vmiOutputDetailInfo.WmNo;
            ///ZONE_NO,存贮区编码
            info.ZoneNo = vmiOutputDetailInfo.ZoneNo;
            ///DLOC,库位
            info.Dloc = vmiOutputDetailInfo.Dloc;
            ///TRAN_NO,交易编码
            info.TranNo = vmiOutputDetailInfo.TranNo;
            ///TARGET_WM,目的仓库
            info.TargetWm = vmiOutputDetailInfo.TargetWm;
            ///TARGET_ZONE,目的存贮区
            info.TargetZone = vmiOutputDetailInfo.TargetZone;
            ///TARGET_DLOC,目的库位
            info.TargetDloc = vmiOutputDetailInfo.TargetDloc;
            ///PART_NO,车辆模型_零件号
            info.PartNo = vmiOutputDetailInfo.PartNo;
            ///PART_CNAME,车辆模型_零件中文名
            info.PartCname = vmiOutputDetailInfo.PartCname;
            ///REQUIRED_BOX_NUM,需求包装数
            info.RequiredBoxNum = vmiOutputDetailInfo.RequiredBoxNum;
            ///REQUIRED_QTY,需求数量
            info.RequiredQty = vmiOutputDetailInfo.RequiredQty;
            ///ACTUAL_BOX_NUM,实际包装数
            info.ActualBoxNum = vmiOutputDetailInfo.ActualBoxNum;
            ///ACTUAL_QTY,实际数量
            info.ActualQty = vmiOutputDetailInfo.ActualQty;
            ///PACKAGE,单包装数
            info.Package = vmiOutputDetailInfo.Package;
            ///PACKAGE_MODEL,包装型号
            info.PackageModel = vmiOutputDetailInfo.PackageModel;
            ///BARCODE_DATA,条码
            info.BarcodeData = vmiOutputDetailInfo.BarcodeData;
            ///MEASURING_UNIT_NO,单位
            info.MeasuringUnitNo = vmiOutputDetailInfo.MeasuringUnitNo;
            ///IDENTIFY_PART_NO,车辆模型_标识零件号
            info.IdentifyPartNo = vmiOutputDetailInfo.IdentifyPartNo;
            ///PART_ENAME,车辆模型_零件德文名
            info.PartEname = vmiOutputDetailInfo.PartEname;
            ///DOCK,工厂模型_DOCK
            info.Dock = vmiOutputDetailInfo.Dock;
            ///ASSEMBLY_LINE,工厂模型_流水线
            info.AssemblyLine = vmiOutputDetailInfo.AssemblyLine;
            ///BOX_PARTS,基础数据_零件类
            info.BoxParts = vmiOutputDetailInfo.BoxParts;
            ///SEQUENCE_NO,排序号
            info.SequenceNo = vmiOutputDetailInfo.SequenceNo;
            ///PICKUP_SEQ_NO,捡料顺序号
            info.PickupSeqNo = vmiOutputDetailInfo.PickupSeqNo;
            ///RDC_DLOC,供应商库位
            info.RdcDloc = vmiOutputDetailInfo.RdcDloc;
            ///INHOUSE_PACKAGE,上线包装数量
            info.InhousePackage = vmiOutputDetailInfo.InhousePackage;
            ///INHOUSE_PACKAGE_MODEL,上线包装型号
            info.InhousePackageModel = vmiOutputDetailInfo.InhousePackageModel;
            ///SUPPLIER_NUM_SHEET,基础数据组单_供应商
            info.SupplierNumSheet = vmiOutputDetailInfo.SupplierNumSheet;
            ///BOX_PARTS_SHEET,基础数据_零件类组单
            info.BoxPartsSheet = vmiOutputDetailInfo.BoxPartsSheet;
            ///ORDER_NO,订单号
            info.OrderNo = vmiOutputDetailInfo.OrderNo;
            ///ITEM_NO,ITEM号
            info.ItemNo = vmiOutputDetailInfo.ItemNo;
            ///RUNSHEET_NO,拉动单号
            info.RunsheetNo = vmiOutputDetailInfo.RunsheetNo;
            ///REPACKAGE_FLAG,翻包处理标记
            info.RepackageFlag = vmiOutputDetailInfo.RepackageFlag;
            ///ROW_NO,行号
            info.RowNo = vmiOutputDetailInfo.RowNo;
            ///ORIGIN_PLACE,产地
            info.OriginPlace = vmiOutputDetailInfo.OriginPlace;
            ///SALE_UNIT_PRICE,销售单价
            info.SaleUnitPrice = vmiOutputDetailInfo.SaleUnitPrice;
            ///PART_PRICE,物料金额
            info.PartPrice = vmiOutputDetailInfo.PartPrice;
            ///PART_CLS,零件类别
            info.PartCls = vmiOutputDetailInfo.PartCls;
            ///PICKUP_NUM,拣配包装数
            info.PickupNum = vmiOutputDetailInfo.PickupNum;
            ///PICKUP_QTY,拣配数量
            info.PickupQty = vmiOutputDetailInfo.PickupQty;
            ///IS_SCAN_BOX,是否扫箱
            info.IsScanBox = vmiOutputDetailInfo.IsScanBox;
            ///PACKAGE_LENGTH,包装长
            info.PackageLength = vmiOutputDetailInfo.PackageLength;
            ///PACKAGE_WIDTH,包装宽
            info.PackageWidth = vmiOutputDetailInfo.PackageWidth;
            ///PACKAGE_HEIGHT,包装高
            info.PackageHeight = vmiOutputDetailInfo.PackageHeight;
            ///PERPACKAGE_GROSS_WEIGHT,单包装毛重
            info.PerpackageGrossWeight = vmiOutputDetailInfo.PerpackageGrossWeight;
            ///COMMENTS,备注
            info.Comments = vmiOutputDetailInfo.Comments;
            ///VALID_FLAG,逻辑删除标记
            info.ValidFlag = vmiOutputDetailInfo.ValidFlag;
            ///CREATE_USER,创建人
            info.CreateUser = vmiOutputDetailInfo.CreateUser;
            ///CREATE_DATE,创建时间
            info.CreateDate = vmiOutputDetailInfo.CreateDate;
            ///MODIFY_USER,最后修改人
            info.ModifyUser = vmiOutputDetailInfo.ModifyUser;
            ///MODIFY_DATE,最后修改时间
            info.ModifyDate = vmiOutputDetailInfo.ModifyDate;
            ///PACKAGE_VOLUME,单包装体积
            info.PackageVolume = vmiOutputDetailInfo.PackageVolume;
            ///SUM_WEIGHT,合计毛重
            info.SumWeight = vmiOutputDetailInfo.SumWeight;
            ///SUM_VOLUME,合计体积
            info.SumVolume = vmiOutputDetailInfo.SumVolume;
            ///FROZEN_STOCK_FLAG,已冻结库存标记
            info.FrozenStockFlag = vmiOutputDetailInfo.FrozenStockFlag;
        }
        /// <summary>
        /// GetOutputDetailInfos
        /// </summary>
        /// <param name="vmiOutputDetailInfos"></param>
        /// <returns></returns>
        public static List<OutputDetailInfo> GetOutputDetailInfos(List<VmiOutputDetailInfo> vmiOutputDetailInfos)
        {
            List<OutputDetailInfo> outputDetailInfos = new List<OutputDetailInfo>();
            foreach (var vmiOutputDetailInfo in vmiOutputDetailInfos)
            {
                OutputDetailInfo outputDetailInfo = CreateOutputDetailInfo(string.Empty);
                GetOutputDetailInfo(vmiOutputDetailInfo, ref outputDetailInfo);
                outputDetailInfos.Add(outputDetailInfo);
            }
            return outputDetailInfos;
        }
        #endregion

    }
}


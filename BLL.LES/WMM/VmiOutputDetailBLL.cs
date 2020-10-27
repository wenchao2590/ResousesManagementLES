namespace BLL.LES
{
    using DAL.LES;
    using DAL.SYS;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Transactions;
    /// <summary>
    /// VmiOutputDetailBLL
    /// </summary>
    public partial class VmiOutputDetailBLL
    {

        #region Common
        /// <summary>
        /// OutputDetailDAL
        /// </summary>
        VmiOutputDetailDAL dal = new VmiOutputDetailDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<OutputDetailInfo></returns>
        public List<VmiOutputDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VmiOutputDetailInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(VmiOutputDetailInfo info)
        {
            VmiOutputInfo outputInfo = new VmiOutputDAL().GetInfo(info.OutputFid.GetValueOrDefault());
            if (outputInfo == null)
                throw new Exception("MC:0x00000084");///数据错误

            if (outputInfo.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Created)
                throw new Exception("MC:0x00000506");///出库单为已创建状态才能添加物料

            ///
            if (dal.GetList("[OUTPUT_FID] = N'" + info.OutputFid + "' and [PART_NO] = N'" + info.PartNo + "' and [SUPPLIER_NUM] = N'" + info.SupplierNum + "'", string.Empty).Count > 0)
                throw new Exception("MC:0x00000464");///同物料号供应商不能一致

            ///
            if (info.RequiredQty.GetValueOrDefault() <= 0)
                throw new Exception("MC:0x00000507");///物料需求数量不能小于等于零

            ///有单包装数量时需要计算
            if (info.Package.GetValueOrDefault() > 0)
                info.RequiredBoxNum = Convert.ToInt32(Math.Ceiling(info.RequiredQty.GetValueOrDefault() / info.Package.GetValueOrDefault()));

            ///单据号
            if (string.IsNullOrEmpty(info.TranNo)) info.TranNo = outputInfo.OutputNo;
            ///仓库
            if (string.IsNullOrEmpty(info.WmNo)) info.WmNo = outputInfo.WmNo;
            if (string.IsNullOrEmpty(info.TargetWm)) info.TargetWm = outputInfo.TWmNo;
            ///存储区
            if (string.IsNullOrEmpty(info.ZoneNo)) info.ZoneNo = outputInfo.ZoneNo;
            if (string.IsNullOrEmpty(info.TargetZone)) info.TargetZone = outputInfo.TZoneNo;
            ///创建VMI出库单时实发数量等于需求数量
            string create_vmi_output_actual_qty_equals_required = new ConfigDAL().GetValueByCode("CREATE_VMI_OUTPUT_ACTUAL_QTY_EQUALS_REQUIRED");
            if (!string.IsNullOrEmpty(create_vmi_output_actual_qty_equals_required) && create_vmi_output_actual_qty_equals_required.ToLower() == "true")
            {
                if (info.ActualQty == null) info.ActualQty = info.RequiredQty;
                if (info.ActualBoxNum == null) info.ActualBoxNum = info.RequiredBoxNum;
            }
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
            int cnt = new VmiOutputDAL().GetCounts("" +
                "[STATUS] = " + (int)WmmOrderStatusConstants.Created + " and " +
                "[FID] in (select [OUTPUT_FID] from [LES].[TT_WMM_VMI_OUTPUT_DETAIL] with(nolock) where [VALID_FLAG] = 1 and [ID] = " + id + ")");
            if (cnt == 0)
                throw new Exception("MC:0x00000145");///出库单状态为已创建时才能删除物料

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
            VmiOutputDetailInfo vmiOutputDetailInfo = dal.GetInfo(id);
            if (vmiOutputDetailInfo == null)
                throw new Exception("MC:0x00000084");///数据错误

            VmiOutputInfo vmiOutputInfo = new VmiOutputDAL().GetInfo(vmiOutputDetailInfo.OutputFid.GetValueOrDefault());
            if (vmiOutputInfo == null)
                throw new Exception("MC:0x00000084");///出库单数据错误

            if (vmiOutputInfo.Status == (int)WmmOrderStatusConstants.Completed
                || vmiOutputInfo.Status == (int)WmmOrderStatusConstants.Closed)
                throw new Exception("MC:0x00000412");///出库单状态为已关闭或已完成时不能修改其内容

            ///修改用户
            string loginUser = CommonBLL.GetFieldValue(fields, "MODIFY_USER");
            if (vmiOutputInfo.Status == (int)WmmOrderStatusConstants.Published)
            {
                ///实收数量
                string actualQty = CommonBLL.GetFieldValue(fields, "ACTUAL_QTY");
                if (string.IsNullOrEmpty(actualQty)) actualQty = "NULL";
                if (Convert.ToDecimal(actualQty) > vmiOutputDetailInfo.RequiredQty.GetValueOrDefault())
                    throw new Exception("MC:0x00000424");///实收数不能大于需求数

                ///实收箱数
                string actualBoxNum = CommonBLL.GetFieldValue(fields, "ACTUAL_BOX_NUM");
                if (string.IsNullOrEmpty(actualBoxNum)) actualBoxNum = "NULL";

                fields = "[ACTUAL_QTY] = " + actualQty + ",[ACTUAL_BOX_NUM] = " + actualBoxNum + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' ";
                ///
                return dal.UpdateInfo(fields, id) > 0 ? true : false;
            }
            ///
            string partNo = CommonBLL.GetFieldValue(fields, "PART_NO");
            string supplierNum = CommonBLL.GetFieldValue(fields, "SUPPLIER_NUM");
            ///
            if (dal.GetList("[OUTPUT_FID] = N'" + vmiOutputInfo.Fid.GetValueOrDefault() + "' and " +
                "[PART_NO] = N'" + partNo + "' and " +
                "[SUPPLIER_NUM] = N'" + supplierNum + "' and " +
                "[ID] <> " + id + "", string.Empty).Count > 0)
                throw new Exception("MC:0x00000464");///同物料号供应商不能一致

            ///实收数量
            string requiredQty = CommonBLL.GetFieldValue(fields, "REQUIRED_QTY");
            if (string.IsNullOrEmpty(requiredQty)) requiredQty = "0";
            if (Convert.ToDecimal(requiredQty) <= 0)
                throw new Exception("MC:0x00000507");///物料需求数量不能小于等于零

            ///创建VMI出库单时实发数量等于需求数量
            string create_vmi_output_actual_qty_equals_required = new ConfigDAL().GetValueByCode("CREATE_VMI_OUTPUT_ACTUAL_QTY_EQUALS_REQUIRED");
            ///实收数量
            string package = CommonBLL.GetFieldValue(fields, "PACKAGE");
            if (string.IsNullOrEmpty(package)) package = "0";
            ///有单包装数量时需要计算
            if (Convert.ToDecimal(package) > 0)
            {
                int requiredBoxNum = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(requiredQty) / Convert.ToDecimal(package)));
                fields = CommonBLL.SetFieldValue(fields, "REQUIRED_BOX_NUM", requiredBoxNum.ToString(), false);
                if (!string.IsNullOrEmpty(create_vmi_output_actual_qty_equals_required) && create_vmi_output_actual_qty_equals_required.ToLower() == "true")
                    fields = CommonBLL.SetFieldValue(fields, "ACTUAL_BOX_NUM", requiredBoxNum.ToString(), false);
            }
            if (!string.IsNullOrEmpty(create_vmi_output_actual_qty_equals_required) && create_vmi_output_actual_qty_equals_required.ToLower() == "true")
                fields = CommonBLL.SetFieldValue(fields, "ACTUAL_QTY", requiredQty.ToString(), false);

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        #endregion

        #region Interface
        /// <summary>
        /// Create VmiOutputDetailInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>VmiOutputDetailInfo</returns>
        public static VmiOutputDetailInfo CreateVmiOutputDetailInfo(string loginUser)
        {
            VmiOutputDetailInfo info = new VmiOutputDetailInfo();
            ///FID,
            info.Fid = Guid.NewGuid();
            ///VALID_FLAG,逻辑删除标记
            info.ValidFlag = true;
            ///CREATE_USER,创建人
            info.CreateUser = loginUser;
            ///CREATE_DATE,创建时间
            info.CreateDate = DateTime.Now;


            ///PLANT,工厂模型_工厂
            info.Plant = null;
            ///DLOC,库位
            info.Dloc = null;
            ///BARCODE_DATA,条码
            info.BarcodeData = null;
            ///IDENTIFY_PART_NO,车辆模型_标识零件号
            info.IdentifyPartNo = null;
            ///ASSEMBLY_LINE,工厂模型_流水线
            info.AssemblyLine = null;
            ///SEQUENCE_NO,排序号
            info.SequenceNo = null;
            ///PICKUP_SEQ_NO,捡料顺序号
            info.PickupSeqNo = null;
            ///RDC_DLOC,供应商库位
            info.RdcDloc = null;
            ///SUPPLIER_NUM_SHEET,基础数据组单_供应商
            info.SupplierNumSheet = null;
            ///BOX_PARTS_SHEET,基础数据_零件类组单
            info.BoxPartsSheet = null;
            ///ORDER_NO,订单号
            info.OrderNo = null;
            ///ITEM_NO,ITEM号
            info.ItemNo = null;
            ///REPACKAGE_FLAG,翻包处理标记
            info.RepackageFlag = null;
            ///ORIGIN_PLACE,产地
            info.OriginPlace = null;
            ///SALE_UNIT_PRICE,销售单价
            info.SaleUnitPrice = null;
            ///PART_PRICE,物料金额
            info.PartPrice = null;
            ///PICKUP_NUM,拣配包装数
            info.PickupNum = null;
            ///PICKUP_QTY,拣配数量
            info.PickupQty = null;
            ///IS_SCAN_BOX,是否扫箱
            info.IsScanBox = null;
            ///MODIFY_USER,最后修改人
            info.ModifyUser = null;
            ///MODIFY_DATE,最后修改时间
            info.ModifyDate = null;
            ///FROZEN_STOCK_FLAG,
            info.FrozenStockFlag = null;
            return info;
        }
        /// <summary>
        /// VmiShippingPartInfo -> VmiOutputDetailInfo
        /// </summary>
        /// <param name="vmiShippingPartInfo"></param>
        /// <param name="info"></param>
        public static void GetVmiOutputDetailInfo(VmiShippingPartInfo vmiShippingPartInfo, ref VmiOutputDetailInfo info)
        {
            if (vmiShippingPartInfo == null) return;
            ///SUPPLIER_NUM,基础数据_供应商
            info.SupplierNum = vmiShippingPartInfo.SupplierNum;
            ///PART_NO,车辆模型_零件号
            info.PartNo = vmiShippingPartInfo.PartNo;
            ///PART_CNAME,车辆模型_零件中文名
            info.PartCname = vmiShippingPartInfo.PartCname;
            ///REQUIRED_QTY,需求数量
            info.RequiredQty = vmiShippingPartInfo.RequiredPartQty;
            ///RUNSHEET_NO,拉动单号
            info.RunsheetNo = vmiShippingPartInfo.OrderCode;
            ///COMMENTS,备注
            info.Comments = vmiShippingPartInfo.Comments;
        }
        /// <summary>
        /// VmiOutputInfo -> VmiOutputDetailInfo
        /// </summary>
        /// <param name="vmiOutputInfo"></param>
        /// <param name="info"></param>
        public static void GetVmiOutputDetailInfo(VmiOutputInfo vmiOutputInfo, ref VmiOutputDetailInfo info)
        {
            if (vmiOutputInfo == null) return;
            ///OUTPUT_FID,出库单外键
            info.OutputFid = vmiOutputInfo.Fid;
            ///WM_NO,仓库编码
            info.WmNo = vmiOutputInfo.WmNo;
            ///ZONE_NO,存贮区编码
            info.ZoneNo = vmiOutputInfo.ZoneNo;
            ///TRAN_NO,交易编码
            info.TranNo = vmiOutputInfo.OutputNo;
            ///TARGET_WM,目的仓库
            info.TargetWm = vmiOutputInfo.TWmNo;
            ///TARGET_ZONE,目的存贮区
            info.TargetZone = vmiOutputInfo.TZoneNo;
            ///DOCK,工厂模型_DOCK
            info.Dock = vmiOutputInfo.TDock;
            ///BOX_PARTS,基础数据_零件类
            info.BoxParts = vmiOutputInfo.PartBoxCode;
        }
        /// <summary>
        /// PartsStockInfo -> VmiOutputDetailInfo
        /// </summary>
        /// <param name="partsStockInfo"></param>
        /// <param name="info"></param>
        public static void GetVmiOutputDetailInfo(PartsStockInfo partsStockInfo, ref VmiOutputDetailInfo info)
        {
            if (partsStockInfo == null) return;
            ///TARGET_DLOC,目的库位
            info.TargetDloc = partsStockInfo.Dloc;
            ///PACKAGE,单包装数
            info.Package = partsStockInfo.InboundPackage;
            ///PACKAGE_MODEL,包装型号
            info.PackageModel = partsStockInfo.InboundPackageModel;
            ///MEASURING_UNIT_NO,单位
            info.MeasuringUnitNo = partsStockInfo.PartUnits;
            ///INHOUSE_PACKAGE,上线包装数量
            info.InhousePackage = partsStockInfo.InhousePackage;
            ///INHOUSE_PACKAGE_MODEL,上线包装型号
            info.InhousePackageModel = partsStockInfo.InhousePackageModel;
            ///PART_CLS,零件类别
            info.PartCls = partsStockInfo.PartCls;
            ///PART_ENAME,车辆模型_零件德文名
            info.PartEname = partsStockInfo.PartEname;
        }
        /// <summary>
        /// PackageApplianceInfo -> VmiOutputDetailInfo
        /// </summary>
        /// <param name="packageApplianceInfo"></param>
        /// <param name="info"></param>
        public static void GetVmiOutputDetailInfo(PackageApplianceInfo packageApplianceInfo, ref VmiOutputDetailInfo info)
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
            ///PACKAGE_VOLUME,单包装体积
            info.PackageVolume = info.PackageLength.GetValueOrDefault() * info.PackageWidth.GetValueOrDefault() * info.PackageHeight.GetValueOrDefault();
        }
        /// <summary>
        /// -> VmiOutputDetailInfo
        /// </summary>
        /// <param name="info"></param>
        public static void GetVmiOutputDetailInfo(ref VmiOutputDetailInfo info)
        {
            ///REQUIRED_BOX_NUM,需求包装数
            if (info.Package.GetValueOrDefault() > 0)
                info.RequiredBoxNum = Convert.ToInt32(Math.Ceiling(info.RequiredQty.GetValueOrDefault() / info.Package.GetValueOrDefault()));
            ///SUM_WEIGHT,合计毛重
            info.SumWeight = info.RequiredBoxNum.GetValueOrDefault() * info.PerpackageGrossWeight.GetValueOrDefault();
            ///SUM_VOLUME,合计体积
            info.SumVolume = info.RequiredBoxNum.GetValueOrDefault() * info.PackageVolume.GetValueOrDefault();
        }
        #endregion

    }
}

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
    public partial class ReceiveDetailBLL
    {
        #region Common
        ReceiveDetailDAL dal = new ReceiveDetailDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<ReceiveDetailInfo></returns>
        public List<ReceiveDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>ReceiveDetailInfo Collection </returns>
		public List<ReceiveDetailInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);

        }

        /// <summary>
        /// Get data collection
        /// </summary>
        /// <param name="textWhere">Conditon</param>
        /// <param name="orderText">Sort</param>
        /// <returns>ReceiveDetailInfo Collection </returns>
        public List<ReceiveDetailInfo> GetListBySql(string sql)
        {
            return dal.GetList(sql);

        }

        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReceiveDetailInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(ReceiveDetailInfo info)
        {
            ReceiveInfo receiveInfo = new ReceiveDAL().GetInfo(info.ReceiveFid.GetValueOrDefault());
            if (receiveInfo == null)
                throw new Exception("MC:0x00000084");///数据错误

            if (dal.GetList("[RECEIVE_FID] = N'" + info.ReceiveFid + "' and [PART_NO] = N'" + info.PartNo + "' and [SUPPLIER_NUM] = N'" + info.SupplierNum + "'", string.Empty).Count > 0)
                throw new Exception("MC:0x00000464");///同物料号供应商不能一致

            //if (receiveInfo.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Created)
            //    throw new Exception("MC:0x00000152");///入库单处理已创建状态时才能添加材料

            int cnt = 0;
            ///入库单是否按供应商类型校验
            string receiveOrderValidSupplierTypeFlag = new ConfigDAL().GetValueByCode("RECEIVE_ORDER_VALID_SUPPLIER_TYPE_FLAG");
            if (receiveOrderValidSupplierTypeFlag.ToLower() == "true")
            {
                ///需要校验入库单的供应商是否为储运供应商
                cnt = new SupplierDAL().GetCounts("[SUPPLIER_TYPE] = " + (int)SupplierTypeConstants.LogisticsSupplier + " and [SUPPLIER_NUM] = N'" + receiveInfo.SupplierNum + "'");

                ///储运供应商标记
                bool logisticsSupplierFlag = cnt == 0 ? false : true;
                if (logisticsSupplierFlag)
                {
                    cnt = dal.GetCounts("[PART_NO] = N'" + info.PartNo + "' and [SUPPLIER_NUM] = N'" + info.SupplierNum + "' and [RECEIVE_FID] = N'" + info.ReceiveFid.GetValueOrDefault() + "'");
                    if (cnt > 0)
                        throw new Exception("MC:0x00000175");///同一入库单下不能出现相同供应商的物料编码
                }
                else
                {
                    ///入库单明细与入库单不是同一家供应商
                    cnt = new ReceiveDAL().GetCounts("[SUPPLIER_NUM] = N'" + info.SupplierNum + "' and [FID] = N'" + info.ReceiveFid.GetValueOrDefault() + "'");
                    if (cnt == 0)
                        throw new Exception("MC:0x00000176");///入库单的物料供应商与入库明细的供应商不一致

                    cnt = dal.GetCounts("[PART_NO] = N'" + info.PartNo + "' and [RECEIVE_FID] = N'" + info.ReceiveFid.GetValueOrDefault() + "'");
                    if (cnt > 0)
                        throw new Exception("MC:0x00000177");///同一入库单下不能出现相同的物料编码
                }
            }

            ///明细中供应商缺失时，从单据上获取
            if (string.IsNullOrEmpty(info.SupplierNum)) info.SupplierNum = receiveInfo.SupplierNum;
            ///单据号
            if (string.IsNullOrEmpty(info.TranNo)) info.TranNo = receiveInfo.ReceiveNo;
            ///拉动单号
            if (string.IsNullOrEmpty(info.RunsheetNo)) info.RunsheetNo = receiveInfo.RunsheetNo;
            ///工厂
            if (string.IsNullOrEmpty(info.Plant)) info.Plant = receiveInfo.Plant;
            ///仓库
            if (string.IsNullOrEmpty(info.TargetWm)) info.TargetWm = receiveInfo.WmNo;
            ///存储区
            if (string.IsNullOrEmpty(info.TargetZone)) info.TargetZone = receiveInfo.ZoneNo;

            ///来源
            ///仓库
            //if (string.IsNullOrEmpty(info.WmNo)) info.WmNo = receiveInfo.SourceWmNo;
            /////存储区
            //if (string.IsNullOrEmpty(info.ZoneNo)) info.ZoneNo = receiveInfo.SourceZoneNo;

            ///创建入库单时实收数量等于需求数量
            string createReceiveActualQtyEqualsRequired = new ConfigDAL().GetValueByCode("CREATE_RECEIVE_ACTUAL_QTY_EQUALS_REQUIRED");
            if (createReceiveActualQtyEqualsRequired.ToLower() == "true")
            {
                info.ActualBoxNum = info.RequiredBoxNum;
                info.ActualQty = info.RequiredQty;
            }
            ///如果需求箱数大于零
            if (info.RequiredBoxNum.GetValueOrDefault() > 0)
            {
                ///如果未填写单包装毛重，但是填写了总毛重，需要计算
                if (info.PerpackageGrossWeight.GetValueOrDefault() == 0 && info.SumWeight.GetValueOrDefault() > 0)
                    info.PerpackageGrossWeight = info.SumWeight.GetValueOrDefault() / info.RequiredBoxNum.GetValueOrDefault();
                ///体积也是如此处理
                if (info.PackageVolume.GetValueOrDefault() == 0 && info.SumVolume.GetValueOrDefault() > 0)
                    info.PackageVolume = info.SumVolume.GetValueOrDefault() / info.RequiredBoxNum.GetValueOrDefault();
                ///件数也是如此处理
                if (info.Package.GetValueOrDefault() == 0 && info.RequiredQty.GetValueOrDefault() > 0)
                    info.Package = Math.Ceiling(info.RequiredQty.GetValueOrDefault() / info.RequiredBoxNum.GetValueOrDefault());
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
            int cnt = new ReceiveDAL().GetCounts("[STATUS] = " + (int)WmmOrderStatusConstants.Created + " and [FID] in (select [RECEIVE_FID] from [LES].[TT_WMM_RECEIVE_DETAIL] with(nolock) where [VALID_FLAG] = 1 and [ID] = " + id + ")");
            if (cnt == 0)
                throw new Exception("MC:0x00000178");///入库单状态为已创建时才能删除材料

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
            string receiveFid = CommonBLL.GetFieldValue(fields, "RECEIVE_FID");
            ReceiveInfo receiveInfo = new ReceiveDAL().GetInfo(Guid.Parse(receiveFid));
            if (receiveInfo == null)
                throw new Exception("MC:0x00000252");///入库单数据错误

            //if (receiveInfo.Status == (int)WmmOrderStatusConstants.Completed
            //    || receiveInfo.Status == (int)WmmOrderStatusConstants.Closed)
            //    throw new Exception("MC:0x00000253");///入库单状态为已关闭或已完成时不能修改其内容

            //if (receiveInfo.Status == (int)WmmOrderStatusConstants.Created)
            //{
            //    ///入库单是否按供应商类型校验
            //    string receiveOrderValidSupplierTypeFlag = new ConfigDAL().GetValueByCode("RECEIVE_ORDER_VALID_SUPPLIER_TYPE_FLAG");
            //    if (receiveOrderValidSupplierTypeFlag.ToLower() == "true")
            //    {
            //        string supplierNum = CommonBLL.GetFieldValue(fields, "SUPPLIER_NUM");
            //        string partNo = CommonBLL.GetFieldValue(fields, "PART_NO");
            //        ///需要校验入库单的供应商是否为储运供应商
            //        int cnt = new SupplierDAL().GetCounts("[SUPPLIER_TYPE] = " + (int)SupplierTypeConstants.LogisticsSupplier + " and [SUPPLIER_NUM] in (select [SUPPLIER_NUM] from [LES].[TT_WMM_RECEIVE] with(nolock) where [FID] = N'" + receiveFid + "' and [VALID_FLAG] = 1)");

            //        ///储运供应商标记
            //        bool logisticsSupplierFlag = cnt == 0 ? false : true;
            //        if (logisticsSupplierFlag)
            //        {
            //            cnt = dal.GetCounts("[ID] <> " + id + " and [PART_NO] = N'" + partNo + "' and [SUPPLIER_NUM] = N'" + supplierNum + "' and [RECEIVE_FID] = N'" + receiveFid + "'");
            //            if (cnt > 0)
            //                throw new Exception("MC:0x00000175");///同一入库单下不能出现相同供应商的物料编码
            //        }
            //        else
            //        {
            //            ///入库单明细与入库单不是同一家供应商
            //            cnt = new ReceiveDAL().GetCounts("[SUPPLIER_NUM] = N'" + supplierNum + "' and [FID] = N'" + receiveFid + "'");
            //            if (cnt == 0)
            //                throw new Exception("MC:0x00000176");///入库单的物料供应商与入库明细的供应商不一致

            //            cnt = dal.GetCounts("[ID] <> " + id + " and [PART_NO] = N'" + partNo + "' and [RECEIVE_FID] = N'" + receiveFid + "'");
            //            if (cnt > 0)
            //                throw new Exception("MC:0x00000177");///同一入库单下不能出现相同的物料编码
            //        }
            //    }
            //}
            //else
            //{
                ReceiveDetailInfo receiveDetailInfo = dal.GetInfo(id);

                ///实收数量
                string actualQty = CommonBLL.GetFieldValue(fields, "ACTUAL_QTY");
                if (string.IsNullOrEmpty(actualQty)) actualQty = "NULL";
                if (Convert.ToInt32(actualQty) > receiveDetailInfo.RequiredQty)
                    throw new Exception("MC:0x00000424");///实收数不能大于需求数

                ///实收箱数
                string actualBoxNum = CommonBLL.GetFieldValue(fields, "ACTUAL_BOX_NUM");
                if (string.IsNullOrEmpty(actualBoxNum)) actualBoxNum = "NULL";
                ///修改用户
                string loginUser = CommonBLL.GetFieldValue(fields, "MODIFY_USER");

                fields = "[ACTUAL_QTY] = " + actualQty + ",[ACTUAL_BOX_NUM] = " + actualBoxNum + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' ";
           // }

            string requiredBoxNum = CommonBLL.GetFieldValue(fields, "REQUIRED_BOX_NUM");
            int.TryParse(requiredBoxNum, out int intRequiredBoxNum);
            ///如果需求箱数大于零
            if (intRequiredBoxNum > 0)
            {
                string perpackageGrossWeight = CommonBLL.GetFieldValue(fields, "PERPACKAGE_GROSS_WEIGHT");
                decimal.TryParse(perpackageGrossWeight, out decimal decimalPerpackageGrossWeight);
                string sumWeight = CommonBLL.GetFieldValue(fields, "SUM_WEIGHT");
                decimal.TryParse(sumWeight, out decimal decimalSumWeight);
                ///如果未填写单包装毛重，但是填写了总毛重，需要计算
                if (decimalPerpackageGrossWeight == 0 && decimalSumWeight > 0)
                {
                    decimalPerpackageGrossWeight = decimalSumWeight / intRequiredBoxNum;
                    fields = CommonBLL.SetFieldValue(fields, "PERPACKAGE_GROSS_WEIGHT", decimalPerpackageGrossWeight.ToString(), false);
                }

                string packageVolume = CommonBLL.GetFieldValue(fields, "PACKAGE_VOLUME");
                decimal.TryParse(packageVolume, out decimal decimalPackageVolume);
                string sumVolume = CommonBLL.GetFieldValue(fields, "SUM_VOLUME");
                decimal.TryParse(sumVolume, out decimal decimalSumVolume);
                ///体积也是如此处理
                if (decimalPackageVolume == 0 && decimalSumVolume > 0)
                {
                    decimalPackageVolume = decimalSumVolume / intRequiredBoxNum;
                    fields = CommonBLL.SetFieldValue(fields, "PACKAGE_VOLUME", decimalPackageVolume.ToString(), false);
                }

                string package = CommonBLL.GetFieldValue(fields, "PACKAGE");
                decimal.TryParse(package, out decimal decimalPackage);
                string requiredQty = CommonBLL.GetFieldValue(fields, "REQUIRED_QTY");
                decimal.TryParse(requiredQty, out decimal decimalRequiredQty);
                ///件数也是如此处理
                if (decimalPackage == 0 && decimalRequiredQty > 0)
                {
                    decimalPackage = Math.Ceiling(decimalRequiredQty / intRequiredBoxNum);
                    fields = CommonBLL.SetFieldValue(fields, "PACKAGE", decimalPackage.ToString(), false);
                }
            }

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion

        #region Interface
        /// <summary>
        /// Create ReceiveDetailInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>ReceiveDetailInfo</returns>
        public static ReceiveDetailInfo CreateReceiveDetailInfo(string loginUser)
        {
            ReceiveDetailInfo info = new ReceiveDetailInfo();
            ///FID,FID
            info.Fid = Guid.NewGuid();
            ///VALID_FLAG,逻辑删除标记
            info.ValidFlag = true;
            ///CREATE_USER,COMMON_CREATE_USER
            info.CreateUser = loginUser;
            ///CREATE_DATE,COMMON_CREATE_DATE
            info.CreateDate = DateTime.Now;

            ///QUALIFIED_QTY,合格数量 
            info.QualifiedQty = null;
            ///INSPECTION_STATUS,检验状态 10未检验 20 合格 30不合格
            info.InspectionStatus = null;
            ///FROZEN_STOCK_FLAG,已冻结库存标记
            info.FrozenStockFlag = null;
            return info;
        }
        /// <summary>
        /// VmiReceiveDetailInfo  -> ReceiveDetailInfo
        /// </summary>
        /// <param name="vmiReceiveDetailInfo"></param>
        /// <param name="info"></param>
        public static void GetReceiveDetailInfo(VmiReceiveDetailInfo vmiReceiveDetailInfo, ref ReceiveDetailInfo info)
        {
            ///ID,明细流水号
            info.Id = vmiReceiveDetailInfo.Id;
            ///FID,FID
            info.Fid = vmiReceiveDetailInfo.Fid;
            ///RECEIVE_FID,
            info.ReceiveFid = vmiReceiveDetailInfo.ReceiveFid;
            ///PLANT,工厂模型_工厂
            info.Plant = vmiReceiveDetailInfo.Plant;
            ///SUPPLIER_NUM,基础数据_供应商
            info.SupplierNum = vmiReceiveDetailInfo.SupplierNum;
            ///WM_NO,仓库编码
            info.WmNo = vmiReceiveDetailInfo.WmNo;
            ///ZONE_NO,存贮区编码
            info.ZoneNo = vmiReceiveDetailInfo.ZoneNo;
            ///DLOC,库位
            info.Dloc = vmiReceiveDetailInfo.Dloc;
            ///TARGET_WM,目的仓库
            info.TargetWm = vmiReceiveDetailInfo.TargetWm;
            ///TARGET_ZONE,目的存储区
            info.TargetZone = vmiReceiveDetailInfo.TargetZone;
            ///TARGET_DLOC,目的库位
            info.TargetDloc = vmiReceiveDetailInfo.TargetDloc;
            ///PART_NO,车辆模型_零件号
            info.PartNo = vmiReceiveDetailInfo.PartNo;
            ///PART_CNAME,车辆模型_零件中文名
            info.PartCname = vmiReceiveDetailInfo.PartCname;
            ///PART_ENAME,车辆模型_零件德文名
            info.PartEname = vmiReceiveDetailInfo.PartEname;
            ///MEASURING_UNIT_NO,单位
            info.MeasuringUnitNo = vmiReceiveDetailInfo.MeasuringUnitNo;
            ///IDENTIFY_PART_NO,车辆模型_标识零件号
            info.IdentifyPartNo = vmiReceiveDetailInfo.IdentifyPartNo;
            ///PACKAGE_MODEL,包装型号
            info.PackageModel = vmiReceiveDetailInfo.PackageModel;
            ///PACKAGE,单包装数
            info.Package = vmiReceiveDetailInfo.Package;
            ///PART_TYPE,零件类型
            info.PartType = vmiReceiveDetailInfo.PartType;
            ///REQUIRED_BOX_NUM,需求包装数
            info.RequiredBoxNum = vmiReceiveDetailInfo.RequiredBoxNum;
            ///REQUIRED_QTY,需求数量
            info.RequiredQty = vmiReceiveDetailInfo.RequiredQty;
            ///ACTUAL_BOX_NUM,实际包装数
            info.ActualBoxNum = vmiReceiveDetailInfo.ActualBoxNum;
            ///ACTUAL_QTY,实际数量
            info.ActualQty = vmiReceiveDetailInfo.ActualQty;
            ///BARCODE_DATA,条码
            info.BarcodeData = vmiReceiveDetailInfo.BarcodeData;
            ///TRAN_NO,交易编码
            info.TranNo = vmiReceiveDetailInfo.TranNo;
            ///DOCK,工厂模型_DOCK
            info.Dock = vmiReceiveDetailInfo.Dock;
            ///ASSEMBLY_LINE,工厂模型_流水线
            info.AssemblyLine = vmiReceiveDetailInfo.AssemblyLine;
            ///BOX_PARTS,基础数据_零件类
            info.BoxParts = vmiReceiveDetailInfo.BoxParts;
            ///SEQUENCE_NO,排序号
            info.SequenceNo = vmiReceiveDetailInfo.SequenceNo;
            ///PICKUP_SEQ_NO,捡料顺序号
            info.PickupSeqNo = vmiReceiveDetailInfo.PickupSeqNo;
            ///RDC_DLOC,供应商库位
            info.RdcDloc = vmiReceiveDetailInfo.RdcDloc;
            ///INHOUSE_PACKAGE,上线包装数量
            info.InhousePackage = vmiReceiveDetailInfo.InhousePackage;
            ///INHOUSE_PACKAGE_MODEL,上线包装型号
            info.InhousePackageModel = vmiReceiveDetailInfo.InhousePackageModel;
            ///RUNSHEET_NO,拉动单号
            info.RunsheetNo = vmiReceiveDetailInfo.RunsheetNo;
            ///SUPPLIER_NUM_SHEET,基础数据组单_供应商
            info.SupplierNumSheet = vmiReceiveDetailInfo.SupplierNumSheet;
            ///BOX_PARTS_SHEET,基础数据_零件类组单
            info.BoxPartsSheet = vmiReceiveDetailInfo.BoxPartsSheet;
            ///RETURN_REPORT_FLAG,return_report_flag
            info.ReturnReportFlag = vmiReceiveDetailInfo.ReturnReportFlag;
            ///ORDER_NO,订单号
            info.OrderNo = vmiReceiveDetailInfo.OrderNo;
            ///ITEM_NO,ITEM号
            info.ItemNo = vmiReceiveDetailInfo.ItemNo;
            ///CURRENT_BOX_NUM,
            info.CurrentBoxNum = vmiReceiveDetailInfo.CurrentBoxNum;
            ///CURRENT_QTY,
            info.CurrentQty = vmiReceiveDetailInfo.CurrentQty;
            ///FINAL_WM,最终仓库
            info.FinalWm = vmiReceiveDetailInfo.FinalWm;
            ///FINAL_ZONE,最终存储区
            info.FinalZone = vmiReceiveDetailInfo.FinalZone;
            ///FINAL_DLOC,最终库位
            info.FinalDloc = vmiReceiveDetailInfo.FinalDloc;
            ///IS_SCAN_BOX,是否扫箱
            info.IsScanBox = vmiReceiveDetailInfo.IsScanBox;
            ///ROW_NO,行号
            info.RowNo = vmiReceiveDetailInfo.RowNo;
            ///ORIGIN_PLACE,产地
            info.OriginPlace = vmiReceiveDetailInfo.OriginPlace;
            ///PURCHASE_UNIT_PRICE,采购单价
            info.PurchaseUnitPrice = vmiReceiveDetailInfo.PurchaseUnitPrice;
            ///PART_PRICE,金额
            info.PartPrice = vmiReceiveDetailInfo.PartPrice;
            ///PART_CLS,零件类别
            info.PartCls = vmiReceiveDetailInfo.PartCls;
            ///COMMENTS,COMMON_备注
            info.Comments = vmiReceiveDetailInfo.Comments;
            ///VALID_FLAG,逻辑删除标记
            info.ValidFlag = vmiReceiveDetailInfo.ValidFlag;
            ///CREATE_USER,COMMON_CREATE_USER
            info.CreateUser = vmiReceiveDetailInfo.CreateUser;
            ///CREATE_DATE,COMMON_CREATE_DATE
            info.CreateDate = vmiReceiveDetailInfo.CreateDate;
            ///MODIFY_USER,COMMON_MODIFY_USER
            info.ModifyUser = vmiReceiveDetailInfo.ModifyUser;
            ///MODIFY_DATE,COMMON_MODIFY_DATE
            info.ModifyDate = vmiReceiveDetailInfo.ModifyDate;
            ///PACKAGE_LENGTH,包装长
            info.PackageLength = vmiReceiveDetailInfo.PackageLength;
            ///PACKAGE_WIDTH,包装宽
            info.PackageWidth = vmiReceiveDetailInfo.PackageWidth;
            ///PACKAGE_HEIGHT,包装高
            info.PackageHeight = vmiReceiveDetailInfo.PackageHeight;
            ///PERPACKAGE_GROSS_WEIGHT,单箱随货毛重
            info.PerpackageGrossWeight = vmiReceiveDetailInfo.PerpackageGrossWeight;
            ///INSPECTION_MODE,检验模式
            info.InspectionMode = vmiReceiveDetailInfo.InspectionMode;
            ///PACKAGE_VOLUME,单包装体积
            info.PackageVolume = vmiReceiveDetailInfo.PackageVolume;
            ///SUM_WEIGHT,合计毛重
            info.SumWeight = vmiReceiveDetailInfo.SumWeight;
            ///SUM_VOLUME,合计体积
            info.SumVolume = vmiReceiveDetailInfo.SumVolume;

        }
        /// <summary>
        /// VmiReceiveDetailInfo  -> ReceiveDetailInfo
        /// </summary>
        /// <param name="vmiReceiveDetailInfos"></param>
        /// <returns></returns>
        public static List<ReceiveDetailInfo> GetReceiveDetailInfos(List<VmiReceiveDetailInfo> vmiReceiveDetailInfos)
        {
            List<ReceiveDetailInfo> receiveDetailInfos = new List<ReceiveDetailInfo>();
            foreach (var vmiReceiveDetailInfo in vmiReceiveDetailInfos)
            {
                ReceiveDetailInfo receiveDetailInfo = CreateReceiveDetailInfo(string.Empty);
                GetReceiveDetailInfo(vmiReceiveDetailInfo, ref receiveDetailInfo);
                receiveDetailInfos.Add(receiveDetailInfo);
            }
            return receiveDetailInfos;
        }
        public static void GetReceiveDetailInfo(ref ReceiveDetailInfo receiveDetailInfo)
        {

        }
        #endregion

    }
}


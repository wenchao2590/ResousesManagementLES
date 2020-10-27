namespace BLL.LES
{
    using DAL.LES;
    using DAL.SYS;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// VmiReceiveDetailBLL
    /// </summary>
    public class VmiReceiveDetailBLL
    {
        #region Common
        /// <summary>
        /// VmiReceiveDetailDAL
        /// </summary>
        VmiReceiveDetailDAL dal = new VmiReceiveDetailDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<ReceiveDetailInfo></returns>
        public List<VmiReceiveDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VmiReceiveDetailInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(VmiReceiveDetailInfo info)
        {
            VmiReceiveInfo receiveInfo = new VmiReceiveDAL().GetInfo(info.ReceiveFid.GetValueOrDefault());
            if (receiveInfo == null)
                throw new Exception("MC:0x00000084");///数据错误

            if (receiveInfo.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Created)
                throw new Exception("MC:0x00000152");///入库单处理已创建状态时才能添加材料

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
            ///仓库
            if (string.IsNullOrEmpty(info.WmNo)) info.WmNo = receiveInfo.SourceWmNo;
            ///存储区
            if (string.IsNullOrEmpty(info.ZoneNo)) info.ZoneNo = receiveInfo.SourceZoneNo;
            ///创建VMI入库单时实收数量等于需求数量
            string create_vmi_receive_actual_qty_equals_required = new ConfigDAL().GetValueByCode("CREATE_VMI_RECEIVE_ACTUAL_QTY_EQUALS_REQUIRED");
            if (!string.IsNullOrEmpty(create_vmi_receive_actual_qty_equals_required) && create_vmi_receive_actual_qty_equals_required.ToLower() == "true")
            {
                if (info.ActualBoxNum == null) info.ActualBoxNum = info.RequiredBoxNum;
                if (info.ActualQty == null) info.ActualQty = info.RequiredQty;
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
            int cnt = new VmiReceiveDAL().GetCounts("" +
                "[STATUS] = " + (int)WmmOrderStatusConstants.Created + " and " +
                "[FID] in (select [RECEIVE_FID] from [LES].[TT_WMM_VMI_RECEIVE_DETAIL] with(nolock) where [VALID_FLAG] = 1 and [ID] = " + id + ")");
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
            VmiReceiveDetailInfo vmiReceiveDetailInfo = dal.GetInfo(id);
            if (vmiReceiveDetailInfo == null)
                throw new Exception("MC:0x00000084");///数据错误

            VmiReceiveInfo receiveInfo = new VmiReceiveDAL().GetInfo(vmiReceiveDetailInfo.ReceiveFid.GetValueOrDefault());
            if (receiveInfo == null)
                throw new Exception("MC:0x00000252");///入库单数据错误

            if (receiveInfo.Status.GetValueOrDefault() == (int)WmmOrderStatusConstants.Completed || receiveInfo.Status.GetValueOrDefault() == (int)WmmOrderStatusConstants.Closed)
                throw new Exception("MC:0x00000253");///入库单状态为已关闭或已完成时不能修改其内容

            ///实收数量
            string actualQty = CommonBLL.GetFieldValue(fields, "ACTUAL_QTY");
            ///VMI入库单明细校验实收数量小于等于需求数量
            string vmi_receive_detail_valid_actual_qty_less_required = new ConfigDAL().GetValueByCode("VMI_RECEIVE_DETAIL_VALID_ACTUAL_QTY_LESS_EQUAL_REQUIRED");
            if (!string.IsNullOrEmpty(vmi_receive_detail_valid_actual_qty_less_required) && vmi_receive_detail_valid_actual_qty_less_required.ToLower() == "true")
            {
                decimal.TryParse(actualQty, out decimal dActualQty);
                if (dActualQty > vmiReceiveDetailInfo.RequiredQty.GetValueOrDefault())
                    throw new Exception("MC:0x00000490");///物料实收数量不允许大于需求数量
            }
            ///如果入库状态为已发布，则只能更新实收数量和实收箱数
            if (receiveInfo.Status.GetValueOrDefault() == (int)WmmOrderStatusConstants.Published)
            {
                if (string.IsNullOrEmpty(actualQty)) actualQty = "NULL";
                ///实收箱数
                string actualBoxNum = CommonBLL.GetFieldValue(fields, "ACTUAL_BOX_NUM");
                if (string.IsNullOrEmpty(actualBoxNum)) actualBoxNum = "NULL";
                ///修改用户
                string loginUser = CommonBLL.GetFieldValue(fields, "MODIFY_USER");
                fields = "[ACTUAL_QTY] = " + actualQty + ",[ACTUAL_BOX_NUM] = " + actualBoxNum + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' ";
                return dal.UpdateInfo(fields, id) > 0 ? true : false;
            }

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
        /// Create VmiReceiveDetailInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static VmiReceiveDetailInfo CreateVmiReceiveDetailInfo(string loginUser)
        {
            VmiReceiveDetailInfo vmiReceiveDetailInfo = new VmiReceiveDetailInfo
            {
                ///FID
                Fid = Guid.NewGuid(),
                ///VALID_FLAG
                ValidFlag = true,
                ///CREATE_DATE
                CreateDate = DateTime.Now,
                ///CREATE_USER
                CreateUser = loginUser,
                ///IDENTIFY_PART_NO
                IdentifyPartNo = string.Empty,
                ///PART_TYPE
                PartType = null,
                ///BARCODE_DATA
                BarcodeData = string.Empty,
                ///BOX_PARTS
                BoxParts = string.Empty,
                ///SEQUENCE_NO
                SequenceNo = null,
                ///PICKUP_SEQ_NO
                PickupSeqNo = null,
                ///RDC_DLOC
                RdcDloc = string.Empty,
                ///SUPPLIER_NUM_SHEET
                SupplierNumSheet = string.Empty,
                ///RETURN_REPORT_FLAG
                ReturnReportFlag = null,
                ///ORDER_NO
                OrderNo = string.Empty,
                ///ITEM_NO
                ItemNo = string.Empty,
                ///CURRENT_BOX_NUM
                CurrentBoxNum = null,
                ///CURRENT_QTY
                CurrentQty = null,
                ///FINAL_DLOC
                FinalDloc = string.Empty,
                ///IS_SCAN_BOX
                IsScanBox = null,
                ///ROW_NO
                RowNo = null,
                ///SUM_WEIGHT
                SumWeight = null,
                ///SUM_VOLUME
                SumVolume = null,
                ///DLOC
                Dloc = string.Empty
            };
            ///
            return vmiReceiveDetailInfo;
        }
        /// <summary>
        /// VmiReceiveInfo -> VmiReceiveDetailInfo
        /// </summary>
        /// <param name="vmiReceiveInfo"></param>
        /// <param name="vmiReceiveDetailInfo"></param>
        public static void GetVmiReceiveDetailInfo(VmiReceiveInfo vmiReceiveInfo, ref VmiReceiveDetailInfo vmiReceiveDetailInfo)
        {
            if (vmiReceiveInfo == null) return;
            ///RECEIVE_FID
            vmiReceiveDetailInfo.ReceiveFid = vmiReceiveInfo.Fid;
            ///PLANT
            vmiReceiveDetailInfo.Plant = vmiReceiveInfo.Plant;
            ///SUPPLIER_NUM
            vmiReceiveDetailInfo.SupplierNum = vmiReceiveInfo.SupplierNum;
            ///WM_NO
            vmiReceiveDetailInfo.WmNo = vmiReceiveInfo.SourceWmNo;
            ///ZONE_NO
            vmiReceiveDetailInfo.ZoneNo = vmiReceiveInfo.SourceZoneNo;
            ///TARGET_WM
            vmiReceiveDetailInfo.TargetWm = vmiReceiveInfo.WmNo;
            ///TARGET_ZONE
            vmiReceiveDetailInfo.TargetZone = vmiReceiveInfo.ZoneNo;
            ///TRAN_NO
            vmiReceiveDetailInfo.TranNo = vmiReceiveInfo.ReceiveNo;
            ///DOCK
            vmiReceiveDetailInfo.Dock = vmiReceiveInfo.Dock;
            ///RUNSHEET_NO
            vmiReceiveDetailInfo.RunsheetNo = vmiReceiveInfo.RunsheetNo;
        }
        /// <summary>
        /// PartsStockInfo -> VmiReceiveDetailInfo
        /// </summary>
        /// <param name="partsStockInfo"></param>
        /// <param name="vmiReceiveDetailInfo"></param>
        public static void GetVmiReceiveDetailInfo(PartsStockInfo partsStockInfo, ref VmiReceiveDetailInfo vmiReceiveDetailInfo)
        {
            if (partsStockInfo == null) return;
            ///PACKAGE_MODEL
            vmiReceiveDetailInfo.PackageModel = partsStockInfo.InboundPackageModel;
            ///PACKAGE
            vmiReceiveDetailInfo.Package = partsStockInfo.InboundPackage;
            ///ASSEMBLY_LINE
            vmiReceiveDetailInfo.AssemblyLine = partsStockInfo.AssemblyLine;
            ///INHOUSE_PACKAGE_MODEL
            vmiReceiveDetailInfo.PackageModel = partsStockInfo.InhousePackageModel;
            ///INHOUSE_PACKAGE
            vmiReceiveDetailInfo.Package = partsStockInfo.InhousePackage;
            /////FINAL_WM
            //vmiReceiveDetailInfo.FinalWm = partsStockInfo.SynchronousWmNo;
            /////FINAL_ZONE
            //vmiReceiveDetailInfo.FinalZone = partsStockInfo.SynchronousZoneNo;
            ///TARGET_DLOC
            vmiReceiveDetailInfo.TargetDloc = partsStockInfo.Dloc;
        }
        /// <summary>
        /// MaintainPartsInfo -> VmiReceiveDetailInfo
        /// </summary>
        /// <param name="maintainPartsInfo"></param>
        /// <param name="vmiReceiveDetailInfo"></param>
        public static void GetVmiReceiveDetailInfo(MaintainPartsInfo maintainPartsInfo, ref VmiReceiveDetailInfo vmiReceiveDetailInfo)
        {
            if (maintainPartsInfo == null) return;
            ///PART_NO
            vmiReceiveDetailInfo.PartNo = maintainPartsInfo.PartNo;
            ///PART_CNAME
            vmiReceiveDetailInfo.PartCname = maintainPartsInfo.PartCname;
            ///PART_ENAME
            vmiReceiveDetailInfo.PartEname = maintainPartsInfo.PartEname;
            ///MEASURING_UNIT_NO
            vmiReceiveDetailInfo.MeasuringUnitNo = maintainPartsInfo.PartUnits;
            ///ORIGIN_PLACE
            //vmiReceiveDetailInfo.OriginPlace = maintainPartsInfo.OriginPlace;
            /////PURCHASE_UNIT_PRICE
            //vmiReceiveDetailInfo.PurchaseUnitPrice = maintainPartsInfo.PurchaseUnitPrice;
            ///PART_CLS
            vmiReceiveDetailInfo.PartCls = maintainPartsInfo.PartCls;
        }
        /// <summary>
        /// PackageApplianceInfo -> VmiReceiveDetailInfo
        /// </summary>
        /// <param name="packageApplianceInfo"></param>
        /// <param name="vmiReceiveDetailInfo"></param>
        public static void GetVmiReceiveDetailInfo(PackageApplianceInfo packageApplianceInfo, ref VmiReceiveDetailInfo vmiReceiveDetailInfo)
        {
            if (packageApplianceInfo == null) return;
            ///PACKAGE_LENGTH
            vmiReceiveDetailInfo.PackageLength = packageApplianceInfo.PackageLength;
            ///PACKAGE_WIDTH
            vmiReceiveDetailInfo.PackageWidth = packageApplianceInfo.PackageWidth;
            ///PACKAGE_HEIGHT
            vmiReceiveDetailInfo.PackageHeight = packageApplianceInfo.PackageHeight;
            ///PERPACKAGE_GROSS_WEIGHT
            vmiReceiveDetailInfo.PerpackageGrossWeight = packageApplianceInfo.MaxWeight;
            ///PACKAGE_VOLUME
            vmiReceiveDetailInfo.PackageVolume = vmiReceiveDetailInfo.PackageLength * vmiReceiveDetailInfo.PackageWidth * vmiReceiveDetailInfo.PackageHeight;
        }
        /// <summary>
        /// SrmVmiShippingNoteDetailInfo -> VmiReceiveDetailInfo
        /// </summary>
        /// <param name="vmiShippingNoteDetailInfo"></param>
        /// <param name="vmiReceiveDetailInfo"></param>
        public static void GetVmiReceiveDetailInfo(SrmVmiShippingNoteDetailInfo vmiShippingNoteDetailInfo, ref VmiReceiveDetailInfo vmiReceiveDetailInfo)
        {
            if (vmiShippingNoteDetailInfo == null) return;
            ///PART_NO
            vmiReceiveDetailInfo.PartNo = vmiShippingNoteDetailInfo.Partno;
            ///REQUIRED_QTY
            vmiReceiveDetailInfo.RequiredQty = vmiShippingNoteDetailInfo.Partqty;
            ///REQUIRED_BOX_NUM
            if (vmiReceiveDetailInfo.Package.GetValueOrDefault() > 0)
                vmiReceiveDetailInfo.RequiredBoxNum = Convert.ToInt32(Math.Ceiling(vmiShippingNoteDetailInfo.Partqty.GetValueOrDefault() / vmiReceiveDetailInfo.Package.GetValueOrDefault()));
            ///COMMENTS
            vmiReceiveDetailInfo.Comments = vmiShippingNoteDetailInfo.Remark;
        }
        #endregion
    }
}


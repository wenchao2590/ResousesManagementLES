using DAL.LES;
using DAL.SYS;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BLL.LES
{
    /// <summary>
    /// BarcodeBLL
    /// </summary>
    public partial class BarcodeBLL
    {
        #region Common
        /// <summary>
        /// BarcodeDAL
        /// </summary>
        BarcodeDAL dal = new BarcodeDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<BarcodeInfo></returns>
        public List<BarcodeInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BarcodeInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(BarcodeInfo info)
        {
            return dal.Add(info);
        }
        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool UpdateInfo(BarcodeInfo info)
        {
            return dal.Update(info) > 0 ? true : false;
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
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
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion

        #region Cancel
        /// <summary>
        /// 拣配
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CancelInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<BarcodeInfo> barcodeInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") ", string.Empty);
            if (barcodeInfos.Count == 0)
                throw new Exception("MC:0x00000276");///标签信息错误

            ///TODO:需要校验单据状态
            return CancelBarcodes(barcodeInfos, loginUser);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="barcodeInfos"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CancelBarcodes(List<BarcodeInfo> barcodeInfos, string loginUser)
        {
            List<BarcodeStatusInfo> barcodeStatusInfos = new BarcodeStatusDAL().GetList("[BARCODE_DATA] in ('" + string.Join(",", barcodeInfos.Select(d => d.BarcodeData).ToArray()) + "')", string.Empty);

            string sql = string.Empty;
            foreach (var barcodeInfo in barcodeInfos)
            {
                if (barcodeInfo.BarcodeStatus.GetValueOrDefault() == (int)BarcodeStatusConstants.Created)
                    continue;
                if (barcodeInfo.BarcodeStatus.GetValueOrDefault() == (int)BarcodeStatusConstants.Invalid)
                    throw new Exception("MC:0x00000277");///标签已作废
                if (barcodeInfo.BarcodeStatus.GetValueOrDefault() == (int)BarcodeStatusConstants.Frozen)
                    throw new Exception("MC:0x00000361");///标签已冻结
                if (barcodeInfo.BarcodeStatus.GetValueOrDefault() == (int)BarcodeStatusConstants.Inbound)
                    throw new Exception("MC:0x00000362");///标签已入库
                if (barcodeInfo.BarcodeStatus.GetValueOrDefault() == (int)BarcodeStatusConstants.Outbound)
                    throw new Exception("MC:0x00000364");///标签已出库
                if (barcodeInfo.BarcodeStatus.GetValueOrDefault() == (int)BarcodeStatusConstants.Shiped)
                    throw new Exception("MC:0x00000365");///标签已发货

                List<BarcodeStatusInfo> barcodeStatuses = barcodeStatusInfos.Where(d => d.BarcodeFid.GetValueOrDefault() == barcodeInfo.Fid.GetValueOrDefault()).OrderByDescending(d => d.Id).ToList();
                if (barcodeStatuses.Count == 0)
                    throw new Exception("MC:0x00000276");///标签信息错误
                if (barcodeStatuses.Count == 1)
                    throw new Exception("MC:0x00000276");///标签信息错误

                ///获取上一个条码状态
                BarcodeStatusInfo barcodeStatusInfo = barcodeStatuses.FirstOrDefault();
                sql += BarcodeDAL.GetBarcodeUpdateSql(barcodeStatusInfo.BarcodeStatus.GetValueOrDefault()
                           , barcodeStatusInfo.WmNo
                           , barcodeStatusInfo.ZoneNo
                           , barcodeStatusInfo.Dloc
                           , barcodeStatusInfo.AsnRunsheetNo
                           , barcodeStatusInfo.BarcodeFid.GetValueOrDefault()
                           , loginUser);
            }
            ///执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql))
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        #endregion

        /// <summary>
        /// WMM-006 获取标签信息
        /// </summary>
        /// <param name="barcodeData">箱条码</param>
        /// <returns>返回条码表实体</returns>
        public BarcodeInfo GetBarcode(string barcodeData, string asnRunsheetNo, BarcodeStatusConstants scanType, string loginUser, int scanMode = 1)
        {
            List<BarcodeInfo> barcodeInfos = dal.GetList("[BARCODE_DATA] = N'" + barcodeData + "' ", string.Empty);
            if (barcodeInfos.Count == 0)
                throw new Exception("MC:0x00000276");///标签信息错误

            BarcodeInfo barcodeInfo = barcodeInfos.FirstOrDefault();
            if (barcodeInfo == null)
                throw new Exception("MC:0x00000276");///标签信息错误

            if (barcodeInfo.BarcodeStatus.GetValueOrDefault() == (int)BarcodeStatusConstants.Invalid)
                throw new Exception("MC:0x00000277");///标签已作废

            ///单号需要改变时则会有内容传输进行
            if (!string.IsNullOrEmpty(asnRunsheetNo))
                barcodeInfo.AsnRunsheetNo = asnRunsheetNo;

            ///校验标签创建单据是否与当前扫描单据相同
            string validBarcodeCreateOrderSameAsAsnrunsheeno = new ConfigDAL().GetValueByCode("VALID_BARCODE_CREATE_ORDER_SAME_AS_ASNRUNSHEETNO");
            if (validBarcodeCreateOrderSameAsAsnrunsheeno.ToLower() == "true")
            {
                if (scanType == BarcodeStatusConstants.Scaned && scanMode == 1)
                {
                    int cnt = new ReceiveDetailDAL().GetCounts("[FID] = N'" + barcodeInfo.CreateSourceFid.GetValueOrDefault() + "' and [TRAN_NO] = N'" + asnRunsheetNo + "'");
                    if (cnt == 0)
                        throw new Exception("MC:0x00000278");///条码不属于本单据
                }
            }

            ///物料交接，使用出库单进行目标库区的收货操作
            if (scanMode == 2)
            {
                OutputDetailInfo outputDetailInfo = new OutputDetailDAL().GetInfo(barcodeInfo.CreateSourceFid.GetValueOrDefault());
                if (outputDetailInfo != null)
                {
                    barcodeInfo.WmNo = outputDetailInfo.TargetWm;
                    barcodeInfo.ZoneNo = outputDetailInfo.TargetZone;
                    barcodeInfo.Dloc = outputDetailInfo.TargetDloc;
                }
            }


            ///是否在客户端扫描标签条码后更新状态为已扫描
            string clientScanedBarcodeUpdateBarcodeStatusFlag = new ConfigDAL().GetValueByCode("CLIENT_SCANED_BARCODE_UPDATE_BARCODE_STATUS_FLAG");
            if (clientScanedBarcodeUpdateBarcodeStatusFlag.ToLower() == "true")
            {
                string sql = BarcodeDAL.GetBarcodeUpdateSql((int)scanType
                        , barcodeInfo.WmNo
                        , barcodeInfo.ZoneNo
                        , barcodeInfo.Dloc
                        , barcodeInfo.AsnRunsheetNo
                        , barcodeInfo.Fid.GetValueOrDefault()
                        , loginUser);
                if (!CommonDAL.ExecuteNonQueryBySql(sql))
                    throw new Exception("MC:0x00000276");///标签信息错误
            }
            return barcodeInfo;
        }

        /// <summary>
        /// 撤销条码状态
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool UndoBarcodeStatus(string barcode, string loginUser)
        {
            List<BarcodeStatusInfo> barcodeStatusInfos = new BarcodeStatusDAL().GetListByPage("[BARCODE_DATA] = N'" + barcode + "'", "[ID] desc", 1, 2);
            if (barcodeStatusInfos.Count == 0)
                throw new Exception("MC:0x00000276");///标签信息错误
            if (barcodeStatusInfos.Count == 1)
                throw new Exception("MC:0x00000276");///标签信息错误

            ///获取上一个条码状态
            BarcodeStatusInfo barcodeStatusInfo = barcodeStatusInfos[1];
            string sql = BarcodeDAL.GetBarcodeUpdateSql(barcodeStatusInfo.BarcodeStatus.GetValueOrDefault()
                        , barcodeStatusInfo.WmNo
                        , barcodeStatusInfo.ZoneNo
                        , barcodeStatusInfo.Dloc
                        , barcodeStatusInfo.AsnRunsheetNo
                        , barcodeStatusInfo.BarcodeFid.GetValueOrDefault()
                        , loginUser);
            if (!CommonDAL.ExecuteNonQueryBySql(sql))
                throw new Exception("MC:0x00000276");///标签信息错误
            return true;
        }

        #region Print
        /// <summary>
        /// 获取打印数据
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public DataSet GetPrintDatas(List<string> rowsKeyValues, string loginUser)
        {
            if (rowsKeyValues.Count == 0) return null;
            string sql = "select * from [LES].[TT_WMM_BARCODE] with(nolock) where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            return CommonDAL.ExecuteDataSetBySql(sql);
        }

        /// <summary>
        /// 标签打印回调函数
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool PrintBarcodeCallBack(List<string> rowsKeyValues, string loginUser)
        {
            string sql = "update [LES].[TT_WMM_BARCODE] set " +
                "[PRINT_DATE] = GETDATE()," +
                "[PRINT_TIMES] = isnull([PRINT_TIMES],0) + 1," +
                "[PRINTED_USER] = N'" + loginUser + "' where " +
                "[ID] in (" + string.Join(",", rowsKeyValues) + ")";
            ///TODO:是否需要记录BARCODE_STATUS？
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        #endregion

        #region Scan
        /// <summary>
        /// WMM-006 标签扫描
        /// </summary>
        /// <param name="ScanInfos">扫描</param>
        /// <returns></returns>
        public bool ScanInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<BarcodeInfo> barcodeInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") ", string.Empty);
            if (barcodeInfos.Count == 0)
                throw new Exception("MC:0x00000276");///标签信息错误            

            string sql = string.Empty;
            foreach (var barcodeInfo in barcodeInfos)
            {
                if (barcodeInfo.BarcodeStatus.GetValueOrDefault() == (int)BarcodeStatusConstants.Invalid)
                    throw new Exception("MC:0x00000277");///标签已作废
                if (barcodeInfo.BarcodeStatus.GetValueOrDefault() == (int)BarcodeStatusConstants.Frozen)
                    throw new Exception("MC:0x00000361");///标签已冻结
                if (barcodeInfo.BarcodeStatus.GetValueOrDefault() == (int)BarcodeStatusConstants.Inbound)
                    throw new Exception("MC:0x00000362");///标签已入库
                if (barcodeInfo.BarcodeStatus.GetValueOrDefault() == (int)BarcodeStatusConstants.PickedUp)
                    throw new Exception("MC:0x00000363");///标签已拣配

                sql += BarcodeDAL.GetBarcodeUpdateSql((int)BarcodeStatusConstants.Scaned
                            , barcodeInfo.WmNo
                            , barcodeInfo.ZoneNo
                            , barcodeInfo.Dloc
                            , barcodeInfo.AsnRunsheetNo
                            , barcodeInfo.Fid.GetValueOrDefault()
                            , loginUser);
            }
            ///是否在客户端扫描标签条码后更新状态为已扫描
            string client_scaned_barcode_update_barcode_status_flag = new ConfigDAL().GetValueByCode("CLIENT_SCANED_BARCODE_UPDATE_BARCODE_STATUS_FLAG");
            ///执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql) && client_scaned_barcode_update_barcode_status_flag.ToLower() == "true")
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        #endregion

        #region Pickup
        /// <summary>
        /// WMM-006 标签拣配
        /// </summary>
        /// <param name="PickupInfos"></param>
        /// <returns></returns>
        public bool PickupInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<BarcodeInfo> barcodeInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") ", string.Empty);
            if (barcodeInfos.Count == 0)
                throw new Exception("MC:0x00000276");///标签信息错误

            string sql = string.Empty;
            foreach (var barcodeInfo in barcodeInfos)
            {
                if (barcodeInfo.BarcodeStatus.GetValueOrDefault() == (int)BarcodeStatusConstants.Invalid)
                    throw new Exception("MC:0x00000277");///标签已作废
                if (barcodeInfo.BarcodeStatus.GetValueOrDefault() == (int)BarcodeStatusConstants.Frozen)
                    throw new Exception("MC:0x00000361");///标签已冻结
                if (barcodeInfo.BarcodeStatus.GetValueOrDefault() == (int)BarcodeStatusConstants.Outbound)
                    throw new Exception("MC:0x00000364");///标签已出库
                if (barcodeInfo.BarcodeStatus.GetValueOrDefault() == (int)BarcodeStatusConstants.Shiped)
                    throw new Exception("MC:0x00000365");///标签已发货
                if (barcodeInfo.BarcodeStatus.GetValueOrDefault() == (int)BarcodeStatusConstants.Scaned)
                    throw new Exception("MC:0x00000366");///标签已扫描

                sql += BarcodeDAL.GetBarcodeUpdateSql((int)BarcodeStatusConstants.PickedUp
                        , barcodeInfo.WmNo
                        , barcodeInfo.ZoneNo
                        , barcodeInfo.Dloc
                        , barcodeInfo.AsnRunsheetNo
                        , barcodeInfo.Fid.GetValueOrDefault()
                        , loginUser);
            }
            ///是否在客户端扫描标签条码后更新状态为已扫描
            string client_scaned_barcode_update_barcode_status_flag = new ConfigDAL().GetValueByCode("CLIENT_SCANED_BARCODE_UPDATE_BARCODE_STATUS_FLAG");
            ///执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql) && client_scaned_barcode_update_barcode_status_flag.ToLower() == "true")
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        #endregion

        #region Interface
        /// <summary>
        /// Create BarcodeInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>BarcodeInfo</returns>
        public static BarcodeInfo CreateBarcodeInfo(string loginUser)
        {
            BarcodeInfo info = new BarcodeInfo();
            ///FID
            info.Fid = Guid.NewGuid();
            ///BARCODE_TYPE
            info.BarcodeType = (int)BarcodeTypeConstants.Package;
            ///BARCODE_STATUS
            info.BarcodeStatus = (int)BarcodeStatusConstants.Created;
            ///VALID_FLAG
            info.ValidFlag = true;
            ///CREATE_USER
            info.CreateUser = loginUser;
            ///CREATE_DATE
            info.CreateDate = DateTime.Now;



            ///PRINT_TIMES
            info.PrintTimes = null;
            ///PRINTED_USER
            info.PrintedUser = null;
            ///PRINT_DATE
            info.PrintDate = null;
            ///IDENTIFY_PART_NO
            info.IdentifyPartNo = null;
            ///PLANT
            info.Plant = null;
            ///ASSEMBLY_LINE
            info.AssemblyLine = null;
            ///LOCATION
            info.Location = null;
            ///DOCK
            info.Dock = null;
            ///BOX_PARTS
            info.BoxParts = null;
            ///RUNSHEET_NO
            info.RunsheetNo = null;
            ///PICKUP_SEQ_NO
            info.PickupSeqNo = null;
            ///RDC_DLOC
            info.RdcDloc = null;
            ///INNER_LOCATION
            info.InnerLocation = null;
            ///WMS_SEND_TIME
            info.WmsSendTime = null;
            ///WMS_SEND_STATUS
            info.WmsSendStatus = null;
            ///REQUIRED_DATE
            info.RequiredDate = null;
            ///BATTH_NO
            info.BatthNo = null;
            ///PRODUCTION_BATCH_NO
            info.ProductionBatchNo = null;
            ///PACHAGE_TYPE
            info.PachageType = null;
            ///TIME_ZONE
            info.TimeZone = null;
            ///RFID_NO
            info.RfidNo = null;
            ///CREATE_SOURCE_FID
            info.CreateSourceFid = null;
            ///MODIFY_USER
            info.ModifyUser = null;
            ///MODIFY_DATE
            info.ModifyDate = null;
            ///
            return info;
        }
        /// <summary>
        /// SrmBarcodeInfo -> BarcodeInfo
        /// </summary>
        /// <param name="srmBarcodeInfo"></param>
        /// <param name="barcodeInfo"></param>
        public static void GetBarcodeInfo(SrmBarcodeInfo srmBarcodeInfo, ref BarcodeInfo barcodeInfo)
        {
            if (srmBarcodeInfo == null) return;
            ///BARCODE_DATA
            barcodeInfo.BarcodeData = srmBarcodeInfo.PackageBarcode;
            ///ASN_RUNSHEET_NO
            barcodeInfo.AsnRunsheetNo = srmBarcodeInfo.SourceOrderCode;
            ///PART_NO
            barcodeInfo.PartNo = srmBarcodeInfo.PartNo;
            ///PART_CNAME
            barcodeInfo.PartCname = srmBarcodeInfo.PartCname;
            ///CURRENT_QTY
            barcodeInfo.CurrentQty = srmBarcodeInfo.PartQty;
            ///DLOC
            barcodeInfo.Dloc = srmBarcodeInfo.TargetSlcode;
            ///PACKAGE_MODEL
            barcodeInfo.PackageModel = srmBarcodeInfo.PackageCode;
            ///PACKAGE
            barcodeInfo.Package = srmBarcodeInfo.Snp;
            ///COMMENTS
            barcodeInfo.Comments = srmBarcodeInfo.Remark;
        }
        /// <summary>
        /// SupplierInfo -> BarcodeInfo
        /// </summary>
        /// <param name="supplierInfo"></param>
        /// <param name="barcodeInfo"></param>
        public static void GetBarcodeInfo(SupplierInfo supplierInfo, ref BarcodeInfo barcodeInfo)
        {
            if (supplierInfo == null) return;
            ///SUPPLIER_NUM
            barcodeInfo.SupplierNum = supplierInfo.SupplierNum;
            ///SUPPLIER_NAME
            barcodeInfo.SupplierName = supplierInfo.SupplierName;
            ///SUPPLIER_SNAME
            barcodeInfo.SupplierSname = supplierInfo.SupplierSname;
        }
        /// <summary>
        /// PackageApplianceInfo -> BarcodeInfo
        /// </summary>
        /// <param name="packageApplianceInfo"></param>
        /// <param name="barcodeInfo"></param>
        public static void GetBarcodeInfo(PackageApplianceInfo packageApplianceInfo, ref BarcodeInfo barcodeInfo)
        {
            if (packageApplianceInfo == null) return;
            ///PACKAGE_LENGTH
            barcodeInfo.PackageLength = packageApplianceInfo.PackageLength;
            ///PACKAGE_WIDTH
            barcodeInfo.PackageWidth = packageApplianceInfo.PackageWidth;
            ///PACKAGE_HEIGHT
            barcodeInfo.PackageHeight = packageApplianceInfo.PackageHeight;
            ///PERPACKAGE_GROSS_WEIGHT
            barcodeInfo.PerpackageGrossWeight = packageApplianceInfo.MaxWeight;
            ///PACKAGE_VOLUME
            barcodeInfo.PackageVolume = packageApplianceInfo.PackageLength.GetValueOrDefault() * packageApplianceInfo.PackageWidth.GetValueOrDefault() * packageApplianceInfo.PackageHeight.GetValueOrDefault();
            ///NET_WEIGHT
            barcodeInfo.NetWeight = packageApplianceInfo.MaxLoadWeight;
        }
        /// <summary>
        /// PartsStockInfo -> BarcodeInfo
        /// </summary>
        /// <param name="partsStockInfo"></param>
        /// <param name="barcodeInfo"></param>
        public static void GetBarcodeInfo(PartsStockInfo partsStockInfo, ref BarcodeInfo barcodeInfo)
        {
            if (partsStockInfo == null) return;
            ///PART_NICKNAME
            barcodeInfo.PartNickname = partsStockInfo.PartNickname;
            ///MEASURING_UNIT_NO
            barcodeInfo.MeasuringUnitNo = partsStockInfo.PartUnits;
            ///WM_NO
            barcodeInfo.WmNo = partsStockInfo.WmNo;
            ///ZONE_NO
            barcodeInfo.ZoneNo = partsStockInfo.ZoneNo;
            ///LINE_POSITION
            barcodeInfo.LinePosition = partsStockInfo.LineSiteDloc;
            ///SUPERMARKET_ADDRESS
            barcodeInfo.SupermarketAddress = partsStockInfo.SupperZoneDloc;
        }
        /// <summary>
        /// ReceiveDetailInfo -> BarcodeInfo
        /// </summary>
        /// <param name="receiveDetailInfo"></param>
        /// <param name="barcodeInfo"></param>
        public static void GetBarcodeInfo(ReceiveDetailInfo receiveDetailInfo, ref BarcodeInfo barcodeInfo)
        {
            if (receiveDetailInfo == null) return;
            barcodeInfo.PartNo = receiveDetailInfo.PartNo;
            barcodeInfo.PartCname = receiveDetailInfo.PartCname;
            barcodeInfo.PackageModel = receiveDetailInfo.PackageModel;
            barcodeInfo.Package = receiveDetailInfo.Package;
            barcodeInfo.IdentifyPartNo = receiveDetailInfo.IdentifyPartNo;
            barcodeInfo.MeasuringUnitNo = receiveDetailInfo.MeasuringUnitNo;
            barcodeInfo.SupplierNum = receiveDetailInfo.SupplierNum;
            barcodeInfo.Plant = receiveDetailInfo.Plant;
            barcodeInfo.AssemblyLine = receiveDetailInfo.AssemblyLine;
            barcodeInfo.Dock = receiveDetailInfo.Dock;
            barcodeInfo.WmNo = receiveDetailInfo.TargetWm;
            barcodeInfo.ZoneNo = receiveDetailInfo.TargetZone;
            barcodeInfo.Dloc = receiveDetailInfo.TargetDloc;
            barcodeInfo.BoxParts = receiveDetailInfo.BoxParts;
            barcodeInfo.RunsheetNo = receiveDetailInfo.RunsheetNo;
            barcodeInfo.AsnRunsheetNo = receiveDetailInfo.TranNo;
            barcodeInfo.PickupSeqNo = receiveDetailInfo.PickupSeqNo;
            barcodeInfo.RdcDloc = receiveDetailInfo.RdcDloc;
            barcodeInfo.Comments = receiveDetailInfo.Comments;
            barcodeInfo.PackageLength = receiveDetailInfo.PackageLength;
            barcodeInfo.PackageWidth = receiveDetailInfo.PackageWidth;
            barcodeInfo.PackageHeight = receiveDetailInfo.PackageHeight;
            barcodeInfo.PerpackageGrossWeight = receiveDetailInfo.PerpackageGrossWeight;
            barcodeInfo.CreateSourceFid = receiveDetailInfo.Fid;
            barcodeInfo.PackageVolume = receiveDetailInfo.PackageVolume;
        }
        /// <summary>
        /// OutputDetailInfo -> BarcodeInfo
        /// </summary>
        /// <param name="outputDetailInfo"></param>
        /// <param name="barcodeInfo"></param>
        public static void GetBarcodeInfo(OutputDetailInfo outputDetailInfo, ref BarcodeInfo barcodeInfo)
        {
            if (outputDetailInfo == null) return;
            barcodeInfo.PartNo = outputDetailInfo.PartNo;
            barcodeInfo.PartCname = outputDetailInfo.PartCname;
            barcodeInfo.PackageModel = outputDetailInfo.PackageModel;
            barcodeInfo.Package = outputDetailInfo.Package;
            barcodeInfo.IdentifyPartNo = outputDetailInfo.IdentifyPartNo;
            barcodeInfo.MeasuringUnitNo = outputDetailInfo.MeasuringUnitNo;
            barcodeInfo.SupplierNum = outputDetailInfo.SupplierNum;
            barcodeInfo.Plant = outputDetailInfo.Plant;
            barcodeInfo.AssemblyLine = outputDetailInfo.AssemblyLine;
            barcodeInfo.Dock = outputDetailInfo.Dock;
            barcodeInfo.WmNo = outputDetailInfo.WmNo;
            barcodeInfo.ZoneNo = outputDetailInfo.ZoneNo;
            barcodeInfo.Dloc = outputDetailInfo.Dloc;
            barcodeInfo.BoxParts = outputDetailInfo.BoxParts;
            barcodeInfo.RunsheetNo = outputDetailInfo.RunsheetNo;
            barcodeInfo.AsnRunsheetNo = outputDetailInfo.TranNo;
            barcodeInfo.PickupSeqNo = outputDetailInfo.PickupSeqNo;
            barcodeInfo.RdcDloc = outputDetailInfo.RdcDloc;
            barcodeInfo.Comments = outputDetailInfo.Comments;
            barcodeInfo.PackageLength = outputDetailInfo.PackageLength;
            barcodeInfo.PackageWidth = outputDetailInfo.PackageWidth;
            barcodeInfo.PackageHeight = outputDetailInfo.PackageHeight;
            barcodeInfo.PerpackageGrossWeight = outputDetailInfo.PerpackageGrossWeight;
            barcodeInfo.CreateSourceFid = outputDetailInfo.Fid;
            barcodeInfo.PackageVolume = outputDetailInfo.PackageVolume;
        }
        #endregion

    }
}


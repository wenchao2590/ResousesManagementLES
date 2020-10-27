using DAL.LES;
using DAL.SYS;
using DM.LES;
using Infrustructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BLL.LES
{
    public class ReceiveAndOutputBLL
    {
        ReceiveAndOutputDAL dal = new ReceiveAndOutputDAL();
        /// <summary>
        /// WMM-027 获取出库单号
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public List<ReceiveAndOutputInfo> GetOutOrder(string orderNo)
        {
            ///TODO:单据类型更换为枚举项
            return dal.GetList(string.Format("[ORDER_NO] = '{0}' AND [OPERATION_TYPE] = 20", orderNo), string.Empty);
        }

        /// <summary>
        /// WMM-022 根据单号获取出\入库明细列表
        /// </summary>
        /// <param name="orderNo">单号</param>
        /// <returns>返回DataTable明细列表</returns>
        public List<ReceiveAndOutputInfo> GetReceiveAndOutOrder(string orderNo)
        {
            return dal.GetList(string.Format("[ORDER_NO] = '{0}'", orderNo), string.Empty);
        }
        /// <summary>
        /// WMM-007 提交入库单
        /// </summary>
        /// <param name="orderlist"></param>
        /// <param name="barCodeList"></param>
        /// <returns></returns>
        //public bool SubmitReceiveOrder(List<ReceiveAndOutputInfo> importOrderList, List<BarcodeInfo> barCodeList, bool emergencyFlag, string loginUser)
        //{
        //    ///TODO:条码状态使用枚举
        //    StringBuilder SqlStrBuilder = new StringBuilder();
        //    string receiveSqlStr = string.Empty;
        //    string receiveDetilsSqlStr = string.Empty;
        //    string barCodeSqlStr = string.Empty;
        //    List<ReceiveInfo> orderList = new ReceiveDAL().GetList(string.Format("[ID] IN ({0})", string.Join(",", importOrderList.Select(w => w.OrderId).ToArray())), string.Empty);
        //    List<ReceiveDetailInfo> detailList = new ReceiveDetailDAL().GetList(string.Format("[ID] IN ({0})", string.Join(",", importOrderList.Select(w => w.DetailId).ToArray())), string.Empty);
        //    ///获取明细零件所有的检验模式
        //    List<PartInspectionModeInfo> qmischeckmodeinfolist = new PartInspectionModeDAL().GetList(string.Format("[PART_NO] IN ({0})", string.Join("','", detailList.GroupBy(w => new { w.PartNo }).Select(w => w.Key.PartNo).ToArray())), string.Empty);
        //    ///获取检验模式变更数据
        //    var differencelist = from e in detailList
        //                         join o in qmischeckmodeinfolist on new { e.PartNo, B = e.SupplierNum } equals new { o.PartNo, B = o.SupplierNum } into ords
        //                         from o in ords.DefaultIfEmpty()
        //                         select new
        //                         {
        //                             e.Fid,
        //                             e.Id,
        //                             e.InspectionMode
        //                         };

        //    foreach (ReceiveInfo receiveInfo in orderList)
        //    {
        //        #region 更新入库单状态
        //        receiveSqlStr = @"UPDATE [LES].[TT_WMM_RECEIVE] SET [STATUS] = 50 ,[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'{0}' WHERE [ID] ={1};"; //入库单Sql脚本拼接
        //        receiveSqlStr = string.Format(receiveSqlStr, loginUser, receiveInfo.Id);
        //        SqlStrBuilder.AppendLine(receiveSqlStr);
        //        #endregion
        //        foreach (ReceiveDetailInfo receiveDetailInfo in detailList.Where(w => w.ReceiveFid.GetValueOrDefault() == receiveInfo.Fid.GetValueOrDefault()))
        //        {
        //            ReceiveAndOutputInfo orderItem = importOrderList.FirstOrDefault(w => w.DetailId == receiveDetailInfo.Id);
        //            if (orderItem != null)
        //            {
        //                #region 更新实际箱数和实际数 为差异做准备
        //                receiveDetailInfo.ActualBoxNum = orderItem.ActualBoxNum;
        //                receiveDetailInfo.ActualQty = orderItem.ActualQty;
        //                #endregion

        //                #region 扫箱更新条码信息
        //                if (orderItem.IsScanBox)
        //                {
        //                    foreach (BarcodeInfo barcodeItem in barCodeList.Where(w => w.AsnRunsheetNo == receiveDetailInfo.OrderNo && w.PartNo == receiveDetailInfo.PartNo && w.SupplierNum == receiveDetailInfo.SupplierNum && w.RunsheetNo == receiveDetailInfo.RunsheetNo))
        //                    {
        //                        barCodeSqlStr = @"UPDATE [LES].[TT_WMM_BARCODE] SET [BARCODE_STATUS] = 20,[WM_NO] = '{0}',[ZONE_NO] = '{1}',[DLOC] = '{2}', [MODIFY_USER] = N'{3}',[MODIFY_DATE] = GETDATE() WHERE [ID] = {4};";//条码表明细拼接
        //                        barCodeSqlStr = string.Format(barCodeSqlStr, orderItem.TargetWm, orderItem.TargetZone, orderItem.TargetDloc, loginUser, barcodeItem.Id);

        //                        SqlStrBuilder.AppendLine(barCodeSqlStr);
        //                    }
        //                }
        //                #endregion

        //                #region 更新入库单明细信息
        //                receiveDetilsSqlStr = @"UPDATE [LES].[TT_WMM_RECEIVE_DETAIL] SET [ACTUAL_BOX_NUM] = {0} , [ACTUAL_QTY] = {1} , [MODIFY_USER] = N'{2}',MODIFY_DATE = GETDATE() WHERE ID = {3};";//入库单明细Sql脚本拼接
        //                                                                                                                                                                                               //更新入库单明细实际箱数、实际数量、修改人、修改时间
        //                receiveDetilsSqlStr = string.Format(receiveDetilsSqlStr, orderItem.ActualBoxNum, orderItem.ActualQty, loginUser, receiveDetailInfo.Id);
        //                SqlStrBuilder.AppendLine(receiveDetilsSqlStr);
        //                #endregion
        //            }
        //            else
        //            {
        //                #region 更新入库单明细信息
        //                receiveDetilsSqlStr = @"UPDATE [LES].[TT_WMM_RECEIVE_DETAIL] SET [MODIFY_USER] = N'{0}',MODIFY_DATE = GETDATE() WHERE ID = {1};";//入库单明细Sql脚本拼接
        //                                                                                                                                                 //更新入库单明细实际箱数、实际数量、修改人、修改时间
        //                receiveDetilsSqlStr = string.Format(receiveDetilsSqlStr, loginUser, receiveDetailInfo.Id);
        //                SqlStrBuilder.AppendLine(receiveDetilsSqlStr);
        //                #endregion
        //            }

        //            #region 新增交易记录
        //            TranDetailsInfo tranInfo = new TranDetailsInfo();
        //            new BLL.LES.TranDetailsBLL().GetTranDetailsInfo(receiveInfo, ref tranInfo);
        //            new BLL.LES.TranDetailsBLL().GetTranDetailsInfo(receiveDetailInfo, ref tranInfo);
        //            string tranSqlStr = new TranDetailsDAL().GetInsertSql(tranInfo);
        //            SqlStrBuilder.AppendLine(tranSqlStr);
        //            #endregion
        //        }
        //        #region 若检验模式有变化则需要将变化的物料提交至QMIS检验任务中间表，并生成同步数据任务
        //        string receiveMaterialRecheckInspectMode = new ConfigDAL().GetValueByCode("RECEIVE_MATERIAL_RECHECK_INSPECT_MODE");
        //        if (bool.TryParse(receiveMaterialRecheckInspectMode, out bool receiveMaterialRecheckInspectModeTF))
        //            throw new Exception("MC:1x00000031");///系统配置错误
        //        if (receiveMaterialRecheckInspectModeTF)
        //        {
        //            #region 插入脚本初始化
        //            string qmisSqlStr = @"INSERT INTO [LES].[TI_IFM_QMIS_ASN_PULL_SHEET] (
        //FID,
        //LOG_FID,
        //PLANT,
        //ASN_NO,
        //ORDER_NO,
        //PART_NO,
        //SUPPLIER_NO,
        //TOTAL_NO,
        //CHECK_MODE,
        //ARRIVAL_DATE,
        //ZS_FLAG,
        //VALID_FLAG,
        //PROCESS_FLAG,
        //PROCESS_TIME,
        //CREATE_USER,
        //CREATE_DATE,
        //MODIFY_USER,
        //MODIFY_DATE			
        //            )
        //             VALUES ({0});";
        //            #endregion
        //            long[] ids = differencelist.Where(w => w.Fid.GetValueOrDefault() == receiveInfo.Fid.GetValueOrDefault()).Select(w => w.Id).ToArray();
        //            if (detailList.Where(w => ids.Contains(w.Id)).Count() > 0)
        //            {
        //                #region 生成日志
        //                Guid logFid = Guid.NewGuid();///定义日志关联键
        //                SqlStrBuilder.AppendLine(CommonBLL.GetCreateOutboundLogSql("QMIS", logFid, "LES-QMIS-002", receiveInfo.ReceiveNo, loginUser));
        //                #endregion

        //                foreach (ReceiveDetailInfo receiveDetailInfo in detailList.Where(w => ids.Contains(w.Id)))
        //                {
        //                    #region 生成检验中间表
        //                    string sqltiifmqmischeckmode = "NEWID()"///FID
        //                                                + ",N'" + logFid.ToString() + "'"///LOG_FID
        //                                                + ",N'" + receiveInfo.Plant + "'"///Plant
        //                                                + ",N'" + receiveInfo.ReceiveNo + "'"///ASN_NO
        //                                                + ",N'" + receiveDetailInfo.RunsheetNo + "'"///ORDER_NO
        //                                                + ",N'" + receiveDetailInfo.PartNo + "'"///PART_NO
        //                                                + ",N'" + receiveDetailInfo.SupplierNum + "'"///SUPPLIER_NO
        //                                                + ",N'" + receiveDetailInfo.ActualQty + "'"///TOTAL_NO
        //                                                + ",'" + differencelist.FirstOrDefault(w => w.Id == receiveDetailInfo.Id).InspectionMode + "'" ///CHECK_MODE
        //                                                + ",null"///ARRIVAL_DATE
        //                                                + ",null"///ZS_FLAG
        //                                                + ",1"///VALID_FLAG
        //                                                + "," + (int)ProcessFlagResuitsConstants.Created///ORDER_STATUS
        //                                                + ",null"
        //                                                + ",N'" + loginUser + "'"
        //                                                + ",GETDATE()"///CREATE_DATE
        //                                                + ",null"///MODIFY_USER
        //                                                + ",null";///MODIFY_DATE 
        //                    #endregion

        //                    SqlStrBuilder.AppendLine(string.Format(qmisSqlStr, sqltiifmqmischeckmode));
        //                }
        //            }
        //        }
        //        #endregion

        //        #region 生成出库单
        //        //将入库明细中是否产生出库单标记㊵为true的数据过滤出来，系统配置中SAME_ZONE_SAME_FINAL_ZONE_VALID_FLAG相同存储区相同中转存储区验证标记，
        //        //默认为true，控制了同一张入库单的明细中不会出现不同的出库目标存储区㊷，
        //        //所以此时只需直接根据入库单及明细复制出相应的出库单及明细，并以出库目标存储区㊷作为出库单的目标存储区入库实际数量⑱作为出库需求数量，
        //        //若系统配置标记为false，则将过滤出来的入库明细数据根据其出库目标存储区进行分组，并按分组情况生成多个出库单，出库单状态为已发布WMM - 011
        //        string sameZoneSameFinalZoneValidFlag = new ConfigDAL().GetValueByCode("SAME_ZONE_SAME_FINAL_ZONE_VALID_FLAG");
        //        if (bool.TryParse(sameZoneSameFinalZoneValidFlag, out bool sameZoneSameFinalZoneValidFlagTF))
        //            throw new Exception("MC:1x00000031");///系统配置错误

        //        bool isoutput = receiveInfo.IsOutput ?? false;
        //        if (sameZoneSameFinalZoneValidFlagTF)
        //        {
        //            #region 根据入库单赋值出库单
        //            ///出库单号
        //            string outputNo = new SeqDefineDAL().GetCurrentCode("OUTPUT_NO");
        //            int rowNo = 0;
        //            if (isoutput)
        //            {
        //                Guid outputFid = Guid.NewGuid();
        //                string finalWm = string.Empty;
        //                string finalZone = string.Empty;
        //                string fargetDloc = string.Empty;
        //                foreach (ReceiveDetailInfo receiveDetailInfo in detailList.Where(w => w.ReceiveFid.GetValueOrDefault() == receiveInfo.Fid.GetValueOrDefault()))
        //                {
        //                    ReceiveAndOutputInfo orderItem = importOrderList.FirstOrDefault(w => w.DetailId == receiveDetailInfo.Id);
        //                    rowNo++;
        //                    if (orderItem != null)
        //                    {
        //                        finalWm = receiveDetailInfo.FinalWm;
        //                        finalZone = receiveDetailInfo.FinalZone;
        //                        fargetDloc = receiveDetailInfo.Dloc;

        //                        string detailsStr = "insert into LES.[TT_WMM_OUTPUT_DETAIL] "
        //                        + "(FID, OUTPUT_FID, PLANT, SUPPLIER_NUM, WM_NO, ZONE_NO, DLOC, TRAN_NO, TARGET_WM, TARGET_ZONE, TARGET_DLOC, PART_NO, PART_CNAME, REQUIRED_BOX_NUM, REQUIRED_QTY, ACTUAL_BOX_NUM, ACTUAL_QTY, PACKAGE, PACKAGE_MODEL, BARCODE_DATA, MEASURING_UNIT_NO, IDENTIFY_PART_NO, PART_ENAME, DOCK, ASSEMBLY_LINE, BOX_PARTS, SEQUENCE_NO, PICKUP_SEQ_NO, RDC_DLOC, INHOUSE_PACKAGE, INHOUSE_PACKAGE_MODEL, SUPPLIER_NUM_SHEET, BOX_PARTS_SHEET, ORDER_NO, ITEM_NO, RUNSHEET_NO, REPACKAGE_FLAG, ROW_NO, ORIGIN_PLACE, SALE_UNIT_PRICE, PART_PRICE, PART_CLS, PICKUP_NUM, PICKUP_QTY, IS_SCAN_BOX, PACKAGE_LENGTH, PACKAGE_WIDTH, PACKAGE_HEIGHT, PERPACKAGE_GROSS_WEIGHT, COMMENTS, VALID_FLAG, CREATE_USER, CREATE_DATE) select "
        //                        + "'" + receiveDetailInfo.Fid
        //                        + "', N'"
        //                        + outputFid
        //                        + "', PLANT, SUPPLIER_NUM, N'"
        //                        + receiveDetailInfo.TargetWm
        //                        + "', N'"
        //                        + receiveDetailInfo.TargetZone
        //                        + "',  N'"
        //                        + receiveDetailInfo.TargetDloc
        //                        + "', N'"
        //                        + outputNo + "', N'"
        //                        + receiveDetailInfo.FinalWm
        //                        + "', N'"
        //                        + receiveDetailInfo.FinalZone
        //                        + "', N'"
        //                        + receiveDetailInfo.Dloc + "'"
        //                        + ", PART_NO, PART_CNAME, "
        //                        + orderItem.ActualBoxNum + ", "
        //                        + orderItem.ActualQty + ", NULL, NULL, PACKAGE, PACKAGE_MODEL, NULL, MEASURING_UNIT_NO, IDENTIFY_PART_NO, PART_ENAME, DOCK, ASSEMBLY_LINE, BOX_PARTS, SEQUENCE_NO, PICKUP_SEQ_NO, RDC_DLOC, INHOUSE_PACKAGE, INHOUSE_PACKAGE_MODEL, SUPPLIER_NUM_SHEET, BOX_PARTS_SHEET, ORDER_NO, ITEM_NO" + ", N'"
        //                        + receiveInfo.ReceiveNo + "', NULL, "
        //                        + rowNo + ", ORIGIN_PLACE, NULL, NULL, PART_CLS, NULL, NULL, IS_SCAN_BOX, PACKAGE_LENGTH, PACKAGE_WIDTH, PACKAGE_HEIGHT, PERPACKAGE_GROSS_WEIGHT"
        //                        + ", NULL, 1, N'"
        //                        + loginUser + "', GETDATE() "
        //                        + "from LES.TT_WMM_RECEIVE_DETAIL with(nolock) where [FID] = N'" + receiveDetailInfo.Fid.GetValueOrDefault() + "';";

        //                        SqlStrBuilder.AppendLine(detailsStr);
        //                    }
        //                }

        //                string outPutOrderStr = "insert into LES.[TT_WMM_OUTPUT] "
        //                + "(FID, OUTPUT_NO, PLANT, SUPPLIER_NUM, WM_NO, ZONE_NO, T_WM_NO, T_ZONE_NO, T_DOCK, PART_BOX_CODE, SEND_TIME, OUTPUT_TYPE, TRAN_TIME, OUTPUT_REASON, BOOK_KEEPER, CONFIRM_FLAG, PLAN_NO, ASN_NO, RUNSHEET_NO, ASSEMBLY_LINE, PLANT_ZONE, WORKSHOP, TRANS_SUPPLIER_NUM, PART_TYPE, SUPPLIER_TYPE, RUNSHEET_CODE, ERP_FLAG, LOGICAL_PK, BUSINESS_PK, ROUTE, REQUEST_TIME, CUST_CODE, CUST_NAME, COST_CENTER, ORGANIZATION_FID, CONFIRM_USER, CONFIRM_DATE, LIABLE_USER, LIABLE_DATE, FINANCE_USER, FINANCE_DATE, SUM_PART_QTY, SUM_OF_PRICE, STATUS, CONVEYANCE, CARRIER_TEL, SUM_WEIGHT, SUM_VOLUME, PLAN_SHIPPING_TIME, PLAN_DELIVERY_TIME, COMMENTS, VALID_FLAG, CREATE_USER, CREATE_DATE) select "
        //                + "N'"
        //                + outputFid
        //                + "', N'"
        //                + outputNo
        //                + "', PLANT, SUPPLIER_NUM, N'"
        //                + receiveInfo.WmNo
        //                + "', N'"
        //                + receiveInfo.ZoneNo
        //                + "', N'"
        //                + finalWm
        //                + "', N'"
        //                + finalZone
        //                + "'"
        //                + ", DOCK, PART_BOX_CODE, GETDATE(), "
        //                + (int)OutboundTypeConstants.NormalOutbound
        //                + ", NULL, NULL, BOOK_KEEPER, NULL, NULL, NULL, N'"
        //                + receiveInfo.ReceiveNo
        //                + "'"
        //                + ", NULL, NULL, NULL, NULL, NULL, SUPPLIER_TYPE, NULL, NULL, NULL, NULL"
        //                + ", ROUTE, NULL, CUST_CODE, CUST_NAME, COST_CENTER, N'"
        //                + receiveInfo.OrganizationFid + "', NULL, NULL, NULL, NULL, NULL, NULL"
        //                + ", "
        //                + importOrderList.Where(w => w.OrderId == receiveInfo.Id).Sum(w => w.ActualQty).ToString("F0")
        //                + ", NULL, "
        //                + (int)WmmOrderStatusConstants.Published + ", N'"
        //                + string.Empty + "', N'" ///conveyance
        //                + string.Empty + "'" ///carrierTel
        //                + ", "
        //                + receiveInfo.SumWeight + ", "
        //                + receiveInfo.SumVolume + ", N'"
        //                + receiveInfo.PlanShippingTime + "', N'"
        //                + receiveInfo.PlanDeliveryTime + "', NULL, 1, N'"
        //                + loginUser + "', GETDATE() "
        //                + "from LES.[TT_WMM_RECEIVE] with(nolock) where [FID] = N'"
        //                + receiveInfo.Fid.GetValueOrDefault() + "';";

        //                SqlStrBuilder.AppendLine(outPutOrderStr);
        //            }
        //            #endregion
        //        }
        //        else
        //        {
        //            #region 根据入库单分单并复制出库单
        //            if (isoutput)
        //            {
        //                var list = detailList.Where(w => w.ReceiveFid.GetValueOrDefault() == receiveInfo.Fid.GetValueOrDefault()).GroupBy(w => new { w.FinalWm, w.FinalZone, w.FinalDloc }).Select(w => new { w.Key.FinalWm, w.Key.FinalZone, w.Key.FinalDloc });

        //                foreach (var item in list)
        //                {
        //                    Guid outputFid = Guid.NewGuid();
        //                    string finalWm = string.Empty;
        //                    string finalZone = string.Empty;
        //                    string fargetDloc = string.Empty;
        //                    int rowNo = 0;
        //                    string outputNo = new SeqDefineDAL().GetCurrentCode("OUTPUT_NO");

        //                    foreach (var receiveDetailInfo in detailList.Where(w => w.FinalWm == item.FinalWm && w.FinalZone == item.FinalZone && w.Dloc == item.FinalDloc))
        //                    {
        //                        ReceiveAndOutputInfo orderItem = importOrderList.FirstOrDefault(w => w.DetailId == receiveDetailInfo.Id);
        //                        rowNo++;
        //                        if (orderItem != null)
        //                        {
        //                            finalWm = receiveDetailInfo.FinalWm;
        //                            finalZone = receiveDetailInfo.FinalZone;
        //                            fargetDloc = receiveDetailInfo.Dloc;

        //                            string detailsStr = "insert into LES.[TT_WMM_OUTPUT_DETAIL] "
        //                            + "(FID, OUTPUT_FID, PLANT, SUPPLIER_NUM, WM_NO, ZONE_NO, DLOC, TRAN_NO, TARGET_WM, TARGET_ZONE, TARGET_DLOC, PART_NO, PART_CNAME, REQUIRED_BOX_NUM, REQUIRED_QTY, ACTUAL_BOX_NUM, ACTUAL_QTY, PACKAGE, PACKAGE_MODEL, BARCODE_DATA, MEASURING_UNIT_NO, IDENTIFY_PART_NO, PART_ENAME, DOCK, ASSEMBLY_LINE, BOX_PARTS, SEQUENCE_NO, PICKUP_SEQ_NO, RDC_DLOC, INHOUSE_PACKAGE, INHOUSE_PACKAGE_MODEL, SUPPLIER_NUM_SHEET, BOX_PARTS_SHEET, ORDER_NO, ITEM_NO, RUNSHEET_NO, REPACKAGE_FLAG, ROW_NO, ORIGIN_PLACE, SALE_UNIT_PRICE, PART_PRICE, PART_CLS, PICKUP_NUM, PICKUP_QTY, IS_SCAN_BOX, PACKAGE_LENGTH, PACKAGE_WIDTH, PACKAGE_HEIGHT, PERPACKAGE_GROSS_WEIGHT, COMMENTS, VALID_FLAG, CREATE_USER, CREATE_DATE) select "
        //                            + "'" + receiveDetailInfo.Fid
        //                            + "', N'"
        //                            + outputFid
        //                            + "', PLANT, SUPPLIER_NUM, N'"
        //                            + receiveDetailInfo.TargetWm
        //                            + "', N'"
        //                            + receiveDetailInfo.TargetZone
        //                            + "',  N'"
        //                            + receiveDetailInfo.TargetDloc
        //                            + "', N'"
        //                            + outputNo + "', N'"
        //                            + receiveDetailInfo.FinalWm
        //                            + "', N'"
        //                            + receiveDetailInfo.FinalZone
        //                            + "', N'"
        //                            + receiveDetailInfo.Dloc + "'"
        //                            + ", PART_NO, PART_CNAME, "
        //                            + orderItem.ActualBoxNum + ", "
        //                            + orderItem.ActualQty + ", NULL, NULL, PACKAGE, PACKAGE_MODEL, NULL, MEASURING_UNIT_NO, IDENTIFY_PART_NO, PART_ENAME, DOCK, ASSEMBLY_LINE, BOX_PARTS, SEQUENCE_NO, PICKUP_SEQ_NO, RDC_DLOC, INHOUSE_PACKAGE, INHOUSE_PACKAGE_MODEL, SUPPLIER_NUM_SHEET, BOX_PARTS_SHEET, ORDER_NO, ITEM_NO" + ", N'"
        //                            + receiveInfo.ReceiveNo + "', NULL, "
        //                            + rowNo + ", ORIGIN_PLACE, NULL, NULL, PART_CLS, NULL, NULL, IS_SCAN_BOX, PACKAGE_LENGTH, PACKAGE_WIDTH, PACKAGE_HEIGHT, PERPACKAGE_GROSS_WEIGHT"
        //                            + ", NULL, 1, N'"
        //                            + loginUser + "', GETDATE() "
        //                            + "from LES.TT_WMM_RECEIVE_DETAIL with(nolock) where [FID] = N'" + receiveDetailInfo.Fid.GetValueOrDefault() + "';";

        //                            SqlStrBuilder.AppendLine(detailsStr);
        //                        }
        //                    }

        //                    string outPutOrderStr = "insert into LES.[TT_WMM_OUTPUT] "
        //                    + "(FID, OUTPUT_NO, PLANT, SUPPLIER_NUM, WM_NO, ZONE_NO, T_WM_NO, T_ZONE_NO, T_DOCK, PART_BOX_CODE, SEND_TIME, OUTPUT_TYPE, TRAN_TIME, OUTPUT_REASON, BOOK_KEEPER, CONFIRM_FLAG, PLAN_NO, ASN_NO, RUNSHEET_NO, ASSEMBLY_LINE, PLANT_ZONE, WORKSHOP, TRANS_SUPPLIER_NUM, PART_TYPE, SUPPLIER_TYPE, RUNSHEET_CODE, ERP_FLAG, LOGICAL_PK, BUSINESS_PK, ROUTE, REQUEST_TIME, CUST_CODE, CUST_NAME, COST_CENTER, ORGANIZATION_FID, CONFIRM_USER, CONFIRM_DATE, LIABLE_USER, LIABLE_DATE, FINANCE_USER, FINANCE_DATE, SUM_PART_QTY, SUM_OF_PRICE, STATUS, CONVEYANCE, CARRIER_TEL, SUM_WEIGHT, SUM_VOLUME, PLAN_SHIPPING_TIME, PLAN_DELIVERY_TIME, COMMENTS, VALID_FLAG, CREATE_USER, CREATE_DATE) select "
        //                    + "N'"
        //                    + outputFid
        //                    + "', N'"
        //                    + outputNo
        //                    + "', PLANT, SUPPLIER_NUM, N'"
        //                    + receiveInfo.WmNo
        //                    + "', N'"
        //                    + receiveInfo.ZoneNo
        //                    + "', N'"
        //                    + finalWm
        //                    + "', N'"
        //                    + finalZone
        //                    + "'"
        //                    + ", DOCK, PART_BOX_CODE, GETDATE(), "
        //                    + (int)OutboundTypeConstants.NormalOutbound
        //                    + ", NULL, NULL, BOOK_KEEPER, NULL, NULL, NULL, N'"
        //                    + receiveInfo.ReceiveNo
        //                    + "'"
        //                    + ", NULL, NULL, NULL, NULL, NULL, SUPPLIER_TYPE, NULL, NULL, NULL, NULL"
        //                    + ", ROUTE, NULL, CUST_CODE, CUST_NAME, COST_CENTER, N'"
        //                    + receiveInfo.OrganizationFid + "', NULL, NULL, NULL, NULL, NULL, NULL"
        //                    + ", "
        //                    + importOrderList.Where(w => w.OrderId == receiveInfo.Id).Sum(w => w.ActualQty).ToString("F0")
        //                    + ", NULL, "
        //                    + (int)WmmOrderStatusConstants.Published + ", N'"
        //                    + string.Empty + "', N'" ///conveyance
        //                    + string.Empty + "'" ///carrierTel
        //                    + ","
        //                    + receiveInfo.SumWeight + ", "
        //                    + receiveInfo.SumVolume + ", N'"
        //                    + receiveInfo.PlanShippingTime + "', N'"
        //                    + receiveInfo.PlanDeliveryTime + "', NULL, 1, N'"
        //                    + loginUser + "', GETDATE() "
        //                    + "from LES.[TT_WMM_RECEIVE] with(nolock) where [FID] = N'"
        //                    + receiveInfo.Fid.GetValueOrDefault() + "';";

        //                    SqlStrBuilder.AppendLine(outPutOrderStr);
        //                }
        //            }
        //            #endregion
        //        }

        //        #endregion

        //        #region 器具包装随货入库交易数据
        //        //系统配置ENABLE_PACKAGE_MANAGEMENT_FLAG是否启用器具管理标记，默认为true，⑰以及包装型号⑲等数据产生器具包装随货入库交易数据PKG-000
        //        string enablePackageManagementFlag = new ConfigDAL().GetValueByCode("ENABLE_PACKAGE_MANAGEMENT_FLAG");
        //        if (bool.TryParse(enablePackageManagementFlag, out bool enablePackageManagementFlagTF))
        //            throw new Exception("MC:1x00000031");///系统配置错误
        //        if (enablePackageManagementFlagTF)
        //        {

        //        }
        //        #endregion

        //        #region 不满足需求物料生成新拉动单
        //        if (emergencyFlag)
        //        {
        //            ///TODO:
        //        }
        //        #endregion
        //    }

        //    using (TransactionScope trans = new TransactionScope())
        //    {
        //        if (!CommonDAL.ExecuteNonQueryBySql(SqlStrBuilder.ToString()))
        //            throw new Exception("MC:3x00000013");///TODO:提示是信息修改
        //        trans.Complete();
        //    }
        //    return true;
        //}
        /// <summary>
        /// WMM-024 出库单提交
        /// </summary>
        /// <param name="orderJsonStr"></param>
        /// <param name="barJsonStr"></param>
        /// <param name="emergencyFlag"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        //public bool SubmitOutputOrder(List<ReceiveAndOutputInfo> importOrderList, List<BarcodeInfo> barCodeList, bool emergencyFlag, string loginUser)
        //{
        //    ///TODO:条码状态使用枚举
        //    StringBuilder SqlStrBuilder = new StringBuilder();
        //    string outSqlStr = string.Empty;
        //    string outDetilsSqlStr = string.Empty;
        //    string barCodeSqlStr = string.Empty;
        //    List<OutputInfo> orderList = new OutputDAL().GetList(string.Format("[ID] IN ({0})", string.Join(",", importOrderList.Select(w => w.OrderId).ToArray())), string.Empty);
        //    List<OutputDetailInfo> detailList = new OutputDetailDAL().GetList(string.Format("[ID] IN ({0})", string.Join(",", importOrderList.Select(w => w.DetailId).ToArray())), string.Empty);

        //    foreach (OutputInfo outputInfo in orderList)
        //    {
        //        outSqlStr = @"UPDATE [LES].[TT_WMM_OUTPUT] SET [STATUS] = 50 ,[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'{0}' WHERE [ID] ={1};";
        //        outSqlStr = string.Format(outSqlStr, loginUser, outputInfo.Id);
        //        SqlStrBuilder.AppendLine(outSqlStr);
        //        ///TODO:并更新对应的拉动单或ASN单据状态
        //        foreach (OutputDetailInfo outDetailInfo in detailList.Where(w => w.Fid == outputInfo.Fid))
        //        {
        //            ReceiveAndOutputInfo orderItem = importOrderList.FirstOrDefault(w => w.DetailId == outputInfo.Id);
        //            if (orderItem != null)
        //            {
        //                #region 更新实际箱数和实际数 为差异做准备
        //                outDetailInfo.ActualBoxNum = orderItem.ActualBoxNum;
        //                outDetailInfo.ActualQty = orderItem.ActualQty;
        //                #endregion

        //                #region 扫箱更新条码信息
        //                if (orderItem.IsScanBox)
        //                {
        //                    foreach (BarcodeInfo barcodeItem in barCodeList.Where(w => w.AsnRunsheetNo == orderItem.OrderNo && w.PartNo == orderItem.PartNo && w.SupplierNum == orderItem.SupplierNum))
        //                    {
        //                        barCodeSqlStr = "UPDATE [LES].[TT_WMM_BARCODE] SET [BARCODE_STATUS] = 40,[WM_NO] = '{0}',[ZONE_NO] = '{1}',[DLOC] = '{2}', [MODIFY_USER] = '{3}',[MODIFY_DATE] = GETDATE() WHERE [BARCODE_DATA] = '{4}'";//条码表明细拼接
        //                        barCodeSqlStr = string.Format(barCodeSqlStr, orderItem.TargetWm, orderItem.TargetZone, orderItem.TargetDloc, loginUser, barcodeItem.BarcodeData);

        //                        SqlStrBuilder.AppendLine(barCodeSqlStr);
        //                    }
        //                }
        //                #endregion

        //                #region 新增交易记录
        //                TranDetailsInfo tranInfo = new TranDetailsInfo();
        //                new BLL.LES.TranDetailsBLL().GetTranDetailsInfo(outputInfo, ref tranInfo);
        //                new BLL.LES.TranDetailsBLL().GetTranDetailsInfo(outDetailInfo, ref tranInfo);
        //                string tranSqlStr = new TranDetailsDAL().GetInsertSql(tranInfo);
        //                SqlStrBuilder.AppendLine(tranSqlStr);
        //                #endregion
        //            }
        //        }
        //    }
        //    using (TransactionScope trans = new TransactionScope())
        //    {
        //        if (!CommonDAL.ExecuteNonQueryBySql(SqlStrBuilder.ToString()))
        //            throw new Exception("MC:0x00000173");///TODO:提示信息修改
        //        trans.Complete();
        //    }
        //    return true;
        //}
        /// <summary>
        /// WMM-028 拣配提交
        /// </summary>
        /// <param name="outputIds"></param>
        /// <param name="barcodeIds"></param>
        /// <returns></returns>
        public bool SubmitPickupData(List<long> outputIds, List<long> barcodeIds, string loginUser)
        {
            ///TODO:状态替换为枚举
            StringBuilder stringBuilder = new StringBuilder();
            string outSqlStr = string.Empty;
            string outDetilsSqlStr = string.Empty;
            string barCodeSqlStr = string.Empty;
            List<OutputInfo> orderList = new OutputDAL().GetList(string.Format("[ID] IN ({0})", string.Join(",", outputIds)), string.Empty);
            List<OutputDetailInfo> detailList = new OutputDetailDAL().GetList(string.Format("[OUTPUT_FID] IN ('{0}')", string.Join("','", orderList.Select(w => w.Fid).ToArray())), string.Empty);
            List<BarcodeInfo> barcodelist = new BarcodeDAL().GetList(string.Format("[ID] IN ({0})", string.Join(",", barcodeIds)), string.Empty);
            foreach (OutputInfo outputinfo in orderList)
            {
                #region 更新出库单
                outSqlStr = "UPDATE [LES].[TT_WMM_OUTPUT] SET [STATUS] = 30 ,[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = '{0}' WHERE [ID] ={1};";
                outSqlStr = string.Format(outSqlStr, loginUser, outputinfo.OutputId);
                stringBuilder.AppendLine(outSqlStr);
                #endregion

                foreach (OutputDetailInfo outputdetailinfo in detailList)
                {
                    int actualBoxNum = (outputdetailinfo.ActualBoxNum ?? 0);
                    decimal actualQty = (outputdetailinfo.ActualQty ?? 0);
                    foreach (BarcodeInfo barcodeinfo in barcodelist.Where(w => w.AsnRunsheetNo == outputinfo.OutputNo
                                                                            && w.RunsheetNo == outputdetailinfo.RunsheetNo
                                                                            && w.PartNo == outputdetailinfo.PartNo
                                                                            && w.SupplierNum == outputdetailinfo.SupplierNum
                                                                          ))
                    {
                        #region 累加实出箱数和实出数量
                        actualBoxNum += 1;
                        actualQty += (barcodeinfo.CurrentQty ?? 0);
                        #endregion

                        #region 更新标签表
                        barCodeSqlStr = @"UPDATE [LES].[TT_WMM_BARCODE] SET [BARCODE_STATUS] = 30,[MODIFY_USER] = N'{0}',[MODIFY_DATE] = GETDATE() WHERE [ID] = {1};";//条码表明细拼接
                        barCodeSqlStr = string.Format(barCodeSqlStr, loginUser, barcodeinfo.Id);

                        stringBuilder.AppendLine(barCodeSqlStr);
                        #endregion
                    }

                    #region 更新出库单明细
                    outDetilsSqlStr = "UPDATE [LES].[TT_WMM_OUTPUT_DETAIL] SET [PICKUP_NUM] = {0},[PICKUP_QTY] = {1},[MODIFY_DATE]= GETDATE() ,[MODIFY_USER] = N'{2}' WHERE ID = {3};";
                    outDetilsSqlStr = string.Format(outDetilsSqlStr, actualBoxNum, actualQty, loginUser, outputdetailinfo.Id);

                    stringBuilder.AppendLine(outDetilsSqlStr);
                    #endregion
                }
            }

            using (TransactionScope trans = new TransactionScope())
            {
                if (!CommonDAL.ExecuteNonQueryBySql(stringBuilder.ToString()))
                    throw new Exception("MC:0x00000173");///TODO:提示信息修改
                trans.Complete();
            }
            return true;
        }

        /// <summary>
        /// 根据入库明细集合更新
        /// </summary>
        /// <param name="importOrderList"></param>
        /// <returns></returns>
        public bool UpdateTheActualNumberForReceive(List<ReceiveAndOutputInfo> receiveAndOutputInfoList, string loginUser)
        {
            StringBuilder sqlStringBuilder = new StringBuilder();
            string sqlStr = "UPDATE [LES].[TT_WMM_RECEIVE_DETAIL] SET [ACTUAL_BOX_NUM] = {0},[ACTUAL_QTY] = {1},[MODIFY_USER] = '{2}',[MODIFY_DATE] = GETDATE() WHERE [ID] = {3};";
            foreach (ReceiveAndOutputInfo receiveandoutputinfo in receiveAndOutputInfoList)
            {
                sqlStringBuilder.AppendLine(string.Format(sqlStr,
                    receiveandoutputinfo.ActualBoxNum,
                    receiveandoutputinfo.ActualQty,
                    loginUser,
                    receiveandoutputinfo.DetailId));
            }

            using (TransactionScope trans = new TransactionScope())
            {
                if (!CommonDAL.ExecuteNonQueryBySql(sqlStringBuilder.ToString()))
                    throw new Exception("MC:0x00000173");///TODO:提示信息修改
                trans.Complete();
            }
            return true;
        }

        public bool UpdateTheActualNumberForOutPut(List<ReceiveAndOutputInfo> receiveAndOutputInfoList, string loginUser)
        {
            StringBuilder sqlStringBuilder = new StringBuilder();
            string sqlStr = "UPDATE [LES].[TT_WMM_OUTPUT_DETAIL] SET [ACTUAL_BOX_NUM] = {0},[ACTUAL_QTY] = {1},[MODIFY_USER] = '{2}',[MODIFY_DATE] = GETDATE() WHERE [ID] = {3};";
            foreach (ReceiveAndOutputInfo receiveandoutputinfo in receiveAndOutputInfoList)
            {
                sqlStringBuilder.AppendLine(string.Format(sqlStr,
                    receiveandoutputinfo.ActualBoxNum,
                    receiveandoutputinfo.ActualQty,
                    loginUser,
                    receiveandoutputinfo.DetailId));
            }

            using (TransactionScope trans = new TransactionScope())
            {
                if (!CommonDAL.ExecuteNonQueryBySql(sqlStringBuilder.ToString()))
                    throw new Exception("MC:0x00000173");///TODO:提示信息修改
                trans.Complete();
            }
            return true;
        }
    }
}

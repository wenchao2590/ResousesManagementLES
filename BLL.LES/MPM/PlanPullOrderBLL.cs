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
    /// PlanPullOrderBLL
    /// </summary>
    public class PlanPullOrderBLL
    {
        #region Common
        /// <summary>
        /// PlanPullOrderDAL
        /// </summary>
        PlanPullOrderDAL dal = new PlanPullOrderDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<PlanPullOrderInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PlanPullOrderInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(PlanPullOrderInfo info)
        {
            ///ϵͳ�Զ����ݹ��򴴽�
            info.OrderCode = new SeqDefineDAL().GetCurrentCode("PLAN_PULL_ORDER_CODE");
            ///У����ͬ�ͻ���ί�б�Ų����ظ�
            if (!string.IsNullOrEmpty(info.CustTrustNo))
            {
                int cnt = dal.GetCounts("[CUST_CODE] = N'" + info.CustCode + "' and [CUST_TRUST_NO] = N'" + info.CustTrustNo + "'");
                if (cnt > 0)
                    throw new Exception("MC:0x00000460");///�˿ͻ�����ͬί�б���Ѵ���
            }
            return dal.Add(info);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            PlanPullOrderInfo info = dal.GetInfo(id);
            if (info.OrderStatus.GetValueOrDefault() != (int)PullOrderStatusConstants.Created)
                throw new Exception("MC:0x00000683");///״̬����Ϊ�Ѵ���
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            PlanPullOrderInfo info = dal.GetInfo(id);
            if (info.OrderStatus.GetValueOrDefault() != (int)PullOrderStatusConstants.Created)
                throw new Exception("MC:0x00000683");///״̬����Ϊ�Ѵ���

            ///�����д�˿ͻ�ί�б������Ҫ�ڴ�ʱ���¶�Ӧ��ⵥ�����ⵥ��Ӧ�ա�Ӧ���е�ί�б��
            string custTrustNo = CommonBLL.GetFieldValue(fields, "CUST_TRUST_NO");
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(custTrustNo) && custTrustNo != info.CustTrustNo)
            {
                string loginUser = CommonBLL.GetFieldValue(fields, "MODIFY_USER");
                sql += "update [LES].[TT_WMM_RECEIVE] set [ASN_NO] = N'" + custTrustNo + "',[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() where [RUNSHEET_NO] = N'" + info.OrderCode + "' and [VALID_FLAG] = 1;";
                sql += "update [LES].[TT_WMM_OUTPUT] set [ASN_NO] = N'" + custTrustNo + "',[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() where [RUNSHEET_NO] = N'" + info.OrderCode + "' and [VALID_FLAG] = 1;";
                sql += "update [LES].[TT_FIM_BUSINESS_EXPENSE_IN] set [CUST_TRUST_NO] = N'" + custTrustNo + "',[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() where [ORDER_NO] = N'" + info.OrderCode + "' and [VALID_FLAG] = 1;";
                sql += "update [LES].[TT_FIM_BUSINESS_EXPENSE_OUT] set [CUST_TRUST_NO] = N'" + custTrustNo + "',[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() where [ORDER_NO] = N'" + info.OrderCode + "' and [VALID_FLAG] = 1;";
            }
            ///ִ��
            using (TransactionScope trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql))
                    CommonDAL.ExecuteNonQueryBySql(sql);
                if (dal.UpdateInfo(fields, id) == 0)
                    return false;
                trans.Complete();
            }
            return true;
        }
        #endregion

        #region Release
        /// <summary>
        /// �ύ(����)
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ReleaseInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///��ⵥ
            List<PlanPullOrderInfo> planPullOrderInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            if (planPullOrderInfos.Count == 0)
                throw new Exception("MC:0x00000084");///���ݴ���

            StringBuilder @string = new StringBuilder();
            foreach (var planPullOrderInfo in planPullOrderInfos)
            {
                if (planPullOrderInfo.OrderStatus.GetValueOrDefault() != (int)PullOrderStatusConstants.Created)
                    throw new Exception("MC:0x00000683");///״̬����Ϊ�Ѵ���

                ///������ⵥ����������Ϊ��ʱ�����ݿͻ�ί�б�Ÿ��³���ⵥ�Ľ��ֱ��
                if (!string.IsNullOrEmpty(planPullOrderInfo.CustTrustNo))
                {
                    List<ReceiveInfo> receiveInfos = new ReceiveDAL().GetList("" +
                        "[ASN_NO] = N'" + planPullOrderInfo.CustTrustNo + "' and " +
                        "LEN(ISNULL([RUNSHEET_NO],'')) = 0", string.Empty);
                    List<ReceiveDetailInfo> receiveDetailInfos = new List<ReceiveDetailInfo>();
                    if (receiveInfos.Count > 0)
                        receiveDetailInfos = new ReceiveDetailDAL().GetList("" +
                            "[RECEIVE_FID] in ('" + string.Join("','", receiveInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
                    List<BarcodeInfo> barcodeInfos = new List<BarcodeInfo>();
                    if (receiveDetailInfos.Count > 0)
                        barcodeInfos = new BarcodeDAL().GetList("" +
                            "[CREATE_SOURCE_FID] in ('" + string.Join("','", receiveDetailInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
                    foreach (var receiveInfo in receiveInfos)
                    {
                        @string.AppendLine("update [LES].[TT_WMM_RECEIVE] " +
                        "set [RUNSHEET_NO] = N'" + planPullOrderInfo.OrderCode + "' " +
                        "where [ReceiveId] = " + receiveInfo.ReceiveId + ";");
                        foreach (var receiveDetailInfo in receiveDetailInfos)
                        {
                            @string.AppendLine("update [LES].[TT_WMM_RECEIVE_DETAIL] " +
                            "set [RUNSHEET_NO] = N'" + planPullOrderInfo.OrderCode + "' " +
                            "where [FID] = " + receiveDetailInfo.Fid + ";");
                            List<BarcodeInfo> barcodes = barcodeInfos.Where(d => d.CreateSourceFid.GetValueOrDefault() == receiveDetailInfo.Fid.GetValueOrDefault()).ToList();
                            foreach (var barcode in barcodes)
                            {
                                @string.AppendLine("update [LES].[TT_WMM_BARCODE] " +
                                "set [RUNSHEET_NO] = N'" + planPullOrderInfo.OrderCode + "' " +
                                "where [ID] = " + barcode.Id + ";");
                            }
                        }

                    }
                    List<OutputInfo> outputInfos = new OutputDAL().GetList("[ASN_NO] = N'" + planPullOrderInfo.CustTrustNo + "' and LEN(ISNULL([RUNSHEET_NO],'')) = 0", string.Empty);
                    foreach (var outputInfo in outputInfos)
                    {
                        @string.AppendLine("update [LES].[TT_WMM_OUTPUT] " +
                        "set [RUNSHEET_NO] = N'" + planPullOrderInfo.OrderCode + "' " +
                        "where [ID] = " + outputInfo.OutputId + ";");
                        @string.AppendLine("update [LES].[TT_WMM_OUTPUT_DETAIL] " +
                        "set [RUNSHEET_NO] = N'" + planPullOrderInfo.OrderCode + "' " +
                        "where [OUTPUT_FID] = N'" + outputInfo.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1 and LEN(ISNULL([RUNSHEET_NO],'')) = 0;");
                    }
                }
            }
            @string.AppendLine("update [LES].[TT_MPM_PLAN_PULL_ORDER] set " +
                    "[ORDER_STATUS] = " + (int)PullOrderStatusConstants.Released + "," +
                    "[PUBLISH_TIME] = GETDATE()," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() where " +
                    "[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ");");

            ///ִ��
            using (TransactionScope trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return true;
        }
        #endregion

        #region Cancel
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CancelInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///��ⵥ
            List<PlanPullOrderInfo> planPullOrderInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            if (planPullOrderInfos.Count == 0)
                throw new Exception("MC:0x00000084");///���ݴ���
            foreach (var planPullOrderInfo in planPullOrderInfos)
            {
                if (planPullOrderInfo.OrderStatus.GetValueOrDefault() != (int)PullOrderStatusConstants.Released)
                    throw new Exception("MC:0x00000457");///״̬����Ϊ�ѷ���
            }
            string sql = "update [LES].[TT_MPM_PLAN_PULL_ORDER] set " +
                    "[ORDER_STATUS] = " + (int)PullOrderStatusConstants.Created + "," +
                    "[PUBLISH_TIME] = NULL," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() where " +
                    "[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ");";
            ///ִ��
            using (TransactionScope trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql))
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        #endregion

        #region Invalid
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool InvalidInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///�ƻ�������״̬�����Ѵ���
            List<PlanPullOrderInfo> info = dal.GetList("[ID] IN (" + string.Join(",", rowsKeyValues) + ")", string.Empty);
            if (info.Count == 0)
                throw new Exception("MC:0x00000033");///�ƻ�������������
            string sql = string.Empty;
            foreach (var item in info)
            {
                if (item == null)
                    throw new Exception("MC:0x00000033");///�ƻ�������������
                if (item.OrderStatus != (int)PullOrderStatusConstants.Created)
                    throw new Exception("MC:0x00000670");///�ƻ�������״̬����Ϊ�Ѵ���

                ///�������ʱ����״̬Ϊ90.�����ϣ�������ͬ��ŵ���ⵥ����ⵥ��Ҫͬ������
                sql += "update [LES].[TT_WMM_RECEIVE] "
                   + "set [STATUS] = " + (int)PullOrderStatusConstants.Invalid + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() "
                   + "where [RECEIVE_NO] = N'" + item.OrderCode + "';"
                   + "update [LES].[TT_WMM_OUTPUT] "
                   + "set [STATUS] = " + (int)PullOrderStatusConstants.Invalid + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() "
                   + "where [OUTPUT_NO] = N'" + item.OrderCode + "';"
                   + "update [LES].[TT_MPM_PLAN_PULL_ORDER_DETAIL] "
                   + "set [ORDER_STATUS] = " + (int)PullOrderStatusConstants.Invalid + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() "
                   + "where [ORDER_FID] = N'" + item.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1;"
                   + "update [LES].[TT_MPM_PLAN_PULL_ORDER] "
                   + "set [ORDER_STATUS] = " + (int)PullOrderStatusConstants.Invalid + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() "
                   + "where [ID] = " + item.Id + ";";
            }
            using (TransactionScope trans = new TransactionScope())
            {
                CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        #endregion

        #region Print
        /// <summary>
        /// ��ȡ��ӡ����
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public DataSet GetPrintDatas(List<string> rowsKeyValues, string loginUser)
        {
            List<PlanPullOrderInfo> info = new PlanPullOrderDAL().GetList("[ID] IN (" + string.Join(",", rowsKeyValues) + ")", string.Empty);
            string sql = "select [T1].*,[T2].[ITEM_NAME] from ( select * from [LES].[TT_MPM_PLAN_PULL_ORDER]  with(nolock)  where  [VALID_FLAG] = 1 and "
                 + " [ID] IN (" + string.Join(",", rowsKeyValues) + ")) T1 "
                 + "left join ("
                 + "select [TS_SYS_CODE_ITEM].[ITEM_NAME],[TS_SYS_CODE_ITEM].[ITEM_VALUE] from [TS_SYS_CODE_ITEM] left join [TS_SYS_CODE] on  [TS_SYS_CODE_ITEM].[CODE_FID] = [TS_SYS_CODE].[FID] where  CODE_NAME = N'PULL_ORDER_TYPE' and [TS_SYS_CODE_ITEM].[VALID_FLAG] = 1"
                 + ") T2 on [T1].[ORDER_TYPE]= [T2].[ITEM_VALUE]";
            if (info.Count() > 0)
            {
                sql += "select row_number() over(order by ID) as ROW_NO,* from [LES].[TT_MPM_PLAN_PULL_ORDER_DETAIL]  with(nolock) where [VALID_FLAG] = 1 and  [ORDER_FID] IN ('" + string.Join("','", info.Select(w => w.Fid).ToArray()) + "')";
            }
            return DAL.SYS.CommonDAL.ExecuteDataSetBySql(sql);
        }
        /// <summary>
        /// ��ӡ�ص�����
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        public void GetPrintCallBack(List<string> rowsKeyValues, string loginUser)
        {
            string sql = string.Empty;
            DataTable dt = DAL.SYS.CommonDAL.ExecuteDataTableBySql("select * from [TS_SYS_PRINT_CONFIG] where [VALID_FLAG] = 1 and [PRINT_CONFIG_CODE] = 'BFDA_PLAN_PULL_ORDER'");
            sql += "update [LES].[TT_MPM_PLAN_PULL_ORDER] set [LAST_PRINT_DATE] =  GETDATE(),[PRINT_TIMES] =isnull([PRINT_TIMES],0)+" + dt.Rows[0]["PRINT_COPIES"] + ",[LAST_PRINT_USER] = N'" + loginUser + "' where [ID] in (" + string.Join(",", rowsKeyValues) + ")";
            DAL.SYS.CommonDAL.ExecuteScalar(sql);
        }
        #endregion

        #region Object Convert
        /// <summary>
        /// Create PlanPullOrderInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static PlanPullOrderInfo CreatePlanPullOrderInfo(string loginUser)
        {
            PlanPullOrderInfo planPullOrderInfo = new PlanPullOrderInfo
            {
                ///Fid
                Fid = Guid.NewGuid(),
                ///ValidFlag
                ValidFlag = true,
                ///CreateDate
                CreateDate = DateTime.Now,
                ///CreateUser
                CreateUser = loginUser
            };
            ///
            return planPullOrderInfo;
        }
        /// <summary>
        /// ReceiveInfo -> PlanPullOrderInfo
        /// </summary>
        /// <param name="receiveInfo"></param>
        /// <param name="planPullOrderInfo"></param>
        public static void GetPlanPullOrderInfo(ReceiveInfo receiveInfo, ref PlanPullOrderInfo planPullOrderInfo)
        {
            ///Fid
            planPullOrderInfo.Fid = receiveInfo.Fid;
            ///ORDER_CODE
            planPullOrderInfo.OrderCode = new SeqDefineDAL().GetCurrentCode("PLAN_PULL_ORDER_CODE");
            ///PART_BOX_CODE
            //planPullOrderInfo.PartBoxCode = receiveInfo.PartBoxCode;
            /////PART_BOX_NAME
            //planPullOrderInfo.PartBoxName = receiveInfo.PartBoxName;
            ///PLANT
            planPullOrderInfo.Plant = receiveInfo.Plant;
            ///SUPPLIER_NUM
            planPullOrderInfo.SupplierNum = receiveInfo.SupplierNum;
            ///SOURCE_ZONE_NO
            //planPullOrderInfo.SourceZoneNo = receiveInfo.SourceZoneNo;
            /////SOURCE_WM_NO
            //planPullOrderInfo.SourceWmNo = receiveInfo.SourceWmNo;
            ///TARGET_ZONE_NO
            planPullOrderInfo.TargetZoneNo = receiveInfo.ZoneNo;
            ///TARGET_WM_NO
            planPullOrderInfo.TargetWmNo = receiveInfo.WmNo;
            ///PUBLISH_TIME
            planPullOrderInfo.PublishTime = DateTime.Now;
            ///ORDER_TYPE
            planPullOrderInfo.OrderType = (int)PullOrderTypeConstants.Emergency;
            ///DOCK
            planPullOrderInfo.Dock = receiveInfo.Dock;
            ///SUGGEST_DELIVERY_TIME
            planPullOrderInfo.SuggestDeliveryTime = DateTime.Now;
            ///ORDER_STATUS
            planPullOrderInfo.OrderStatus = (int)PullOrderStatusConstants.Released;
            ///ASN_FLAG
            planPullOrderInfo.AsnFlag = false;
            ///KEEPER
            planPullOrderInfo.Keeper = receiveInfo.BookKeeper;
            ///ROUTE_CODE
            //planPullOrderInfo.RouteCode = receiveInfo.Route;
            /////ROUTE_NAME
            //planPullOrderInfo.RouteName = receiveInfo.RouteName;
            /////CUST_CODE
            //planPullOrderInfo.CustCode = receiveInfo.CustCode;
            /////CUST_SNAME
            //planPullOrderInfo.CustSname = receiveInfo.CustName;
        }
        /// <summary>
        /// PlanPartBoxInfo -> PlanPullOrderInfo
        /// </summary>
        /// <param name="planPartBoxInfo"></param>
        /// <param name="planPullOrderInfo"></param>
        public static void GetPlanPullOrderInfo(PlanPartBoxInfo planPartBoxInfo, ref PlanPullOrderInfo planPullOrderInfo)
        {
            if (planPartBoxInfo == null) return;
            ///WORKSHOP
            planPullOrderInfo.Workshop = planPartBoxInfo.Workshop;
            ///ASSEMBLY_LINE
            planPullOrderInfo.AssemblyLine = planPartBoxInfo.AssemblyLine;
            ///EXPECTED_ARRIVAL_TIME
            if (planPullOrderInfo.SuggestDeliveryTime != null)
                planPullOrderInfo.ExpectedArrivalTime = planPullOrderInfo.SuggestDeliveryTime.GetValueOrDefault().AddMinutes(
                    planPartBoxInfo.PickUpTime.GetValueOrDefault() + planPartBoxInfo.DeliveryTime.GetValueOrDefault());
        }
        #endregion

        #region Emergency
        /// <summary>
        /// ��ȡ��ⵥת�����ƻ�������ִ�����
        /// </summary>
        /// <param name="receiveInfo"></param>
        /// <param name="receiveDetailInfos"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string GetEmergencyPullSql(ReceiveInfo receiveInfo, List<ReceiveDetailInfo> receiveDetailInfos, string loginUser)
        {
            StringBuilder @string = new StringBuilder();
            ///�����ƻ�������
            PlanPullOrderInfo planPullOrderInfo = CreatePlanPullOrderInfo(loginUser);
            ///����ⵥ��Ϣ���ƻ�������
            GetPlanPullOrderInfo(receiveInfo, ref planPullOrderInfo);
            ///PlanPartBoxInfo -> PlanPullOrderInfo
            //PlanPartBoxInfo planPartBoxInfo = new PlanPartBoxDAL().GetInfo(receiveInfo.PartBoxCode);
            PlanPartBoxInfo planPartBoxInfo = new  PlanPartBoxInfo();

            GetPlanPullOrderInfo(planPartBoxInfo, ref planPullOrderInfo);
            ///
            int sumPackageQty = 0;
            decimal sumPartQty = 0;
            foreach (var receiveDetailInfo in receiveDetailInfos)
            {
                if (receiveDetailInfo.RequiredQty.GetValueOrDefault() <= receiveDetailInfo.ActualQty.GetValueOrDefault()) continue;
                ///�����ƻ���������ϸ
                PlanPullOrderDetailInfo planPullOrderDetailInfo = PlanPullOrderDetailBLL.CreatePlanPullOrderDetailInfo(loginUser);
                ///ReceiveDetailInfo -> PlanPullOrderDetailInfo
                PlanPullOrderDetailBLL.GetPlanPullOrderDetailInfo(receiveDetailInfo, ref planPullOrderDetailInfo);
                ///PlanPullOrderInfo -> PlanPullOrderDetailInfo
                PlanPullOrderDetailBLL.GetPlanPullOrderDetailInfo(planPullOrderInfo, ref planPullOrderDetailInfo);
                ///
                @string.AppendLine(PlanPullOrderDetailDAL.GetInsertSql(planPullOrderDetailInfo));
                sumPackageQty += planPullOrderDetailInfo.RequiredPackageQty.GetValueOrDefault();
                sumPartQty += planPullOrderDetailInfo.RequiredPartQty.GetValueOrDefault();
            }
            if (@string.Length == 0) return string.Empty;
            ///SUM_PACKAGE_QTY
            planPullOrderInfo.SumPackageQty = sumPackageQty;
            ///SUM_PART_QTY
            planPullOrderInfo.SumPartQty = sumPartQty;
            @string.AppendLine(PlanPullOrderDAL.GetInsertSql(planPullOrderInfo));
            return @string.ToString();
        }

        #endregion

        #region Plan Object Convert 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static PlanPullOrderInfo CreatePlanPullOrder(string loginUser)
        {
            PlanPullOrderInfo info = new PlanPullOrderInfo();
            ///FID,
            info.Fid = Guid.NewGuid();
            ///VALID_FLAG,�߼�ɾ�����
            info.ValidFlag = true;
            ///CREATE_DATE,����ʱ��
            info.CreateDate = DateTime.Now;
            ///CREATE_USER,�����û�
            info.CreateUser = loginUser;
            ///ORDER_STATUS,������״̬
            info.OrderStatus = (int)PullOrderStatusConstants.Released;
            ///ORDER_CODE,��������
            info.OrderCode = new SeqDefineDAL().GetCurrentCode("PLAN_PULL_ORDER_CODE"); ///TODO: ��̬������
            ///PUBLISH_TIME,����ʱ��
            info.PublishTime = DateTime.Now;
            ///KEEPER,����Ա
            info.Keeper = null;
            return info;
        }

        /// <summary>
        /// PlanPartBoxInfo -> PlanPullOrderInfo
        /// </summary>
        /// <param name="planPartBoxInfo"></param>
        /// <param name="planPullOrderInfo"></param>
        public static void GetPlanPullOrder(PlanPartBoxInfo planPartBoxInfo, ref PlanPullOrderInfo planPullOrderInfo)
        {
            if (planPartBoxInfo == null) return;
            ///PartBoxCode
            planPullOrderInfo.PartBoxCode = planPartBoxInfo.PartBoxCode;
            ///PartBoxName
            planPullOrderInfo.PartBoxName = planPartBoxInfo.PartBoxName;
            ///Plant
            planPullOrderInfo.Plant = planPartBoxInfo.Plant;
            ///Workshop
            planPullOrderInfo.Workshop = planPartBoxInfo.Workshop;
            ///AssemblyLine
            planPullOrderInfo.AssemblyLine = planPartBoxInfo.AssemblyLine;
            ///SupplierNum
            planPullOrderInfo.SupplierNum = planPartBoxInfo.SupplierNum;
            ///SourceWmNo
            planPullOrderInfo.SourceWmNo = planPartBoxInfo.SourceWmNo;
            ///TargetWmNo
            planPullOrderInfo.TargetWmNo = planPartBoxInfo.TargetWmNo;
            ///TargetZoneNo
            planPullOrderInfo.TargetZoneNo = planPartBoxInfo.TargetZoneNo;
            ///Dock
            planPullOrderInfo.Dock = planPartBoxInfo.Dock;
            ///ExpectedArrivalTime
            if (planPullOrderInfo.SuggestDeliveryTime != null)
                planPullOrderInfo.ExpectedArrivalTime = planPullOrderInfo.SuggestDeliveryTime.GetValueOrDefault().AddMinutes(
                    planPartBoxInfo.PickUpTime.GetValueOrDefault() + planPartBoxInfo.DeliveryTime.GetValueOrDefault());
            ///WindowTimes? TODO:����������ô����
            planPullOrderInfo.WindowTimes = null;
            ///TimeZone
            planPullOrderInfo.TimeZone = null;
            ///InspectionFlag
            planPullOrderInfo.InspectionFlag = null;
        }
        #endregion

    }
}

